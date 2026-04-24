using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.VisualBasic;
using System.Runtime.InteropServices;
using System.ComponentModel;

using Nicosu.Profile;
using Nicosu.Encode;

namespace X61GX42H1ST
{
    public static partial class Mx_sub
    {
        private const  LayoutKind        layoutKind          = LayoutKind.Sequential;
        private const  CharSet           charSet             = CharSet.Ansi;
        private const  int               packSize            = 1;
        private const  string            dllName             = "MELSEC_API.dll";
        private const  CallingConvention callingConvention   = CallingConvention.StdCall;

        // プライベート変数
        // ＰＬＣ通信設定情報
        private static byte              m_stno;                 // ＰＬＣ側局番号
        private static byte              m_networkno;            // ＰＬＣ側ネットワーク番号
        private static byte              m_pcno;                 // ＰＬＣ側ＰＣ番号
        private static string            m_ipaddr;               // ＰＬＣ側ＩＰアドレス
        private static UInt16            m_portno;               // ＰＬＣ側ポート番号


        // ＭＣプロトコル通信ＡＰＩ関連変数
        private static UInt32            m_MelsecInst;           // ＭＣプロトコル通信インスタンス番号
        private static MELSEC_PARM       m_MelsecInit;           // ＭＣプロトコル通信初期化パラメータ


        // グローバル変数宣言
        public static UInt16[]   Com01_Tbl01;

        // 計算機リンクチャネルの共通変数
        public static UInt16[]   Buf;        // 読み出しデータの格納バッファ

        static Mx_sub()
        {
            // ソケットインタフェース初期化
            WSAData wsaData;

            WSAStartup(MAKEWORD(2, 2), out wsaData); // Winsock 2.2の初期化
        }

        // ＭＣプロトコル通信ＡＰＩ初期化
        public static bool Initialize()
        {
            Com01_Tbl01 = new UInt16[129];
            Buf         = new UInt16[129];

            // 通信初期化パラメータの設定
            m_MelsecInit = new MELSEC_PARM(null);

            m_MelsecInit.CpuType       = MELSEC_CPU_iQR;          // ＣＰＵ種類
            m_MelsecInit.FrameType     = MELSEC_FRAME_3E;         // 伝送フレーム
            m_MelsecInit.Protocol      = MELSEC_PRTCL0;           // 通信形式
            m_MelsecInit.Code          = MELSEC_CODE_BINARY;      // 伝送コード形式
            m_MelsecInit.Retry         = 0;                       // 通信リトライ回数(通信失敗時)
            m_MelsecInit.RecvTimeOut   = 3000;                    // 受信タイムアウト時間[msec]
            m_MelsecInit.SendTimeOut   = 3000;                    // 送信タイムアウト時間[msec]
            m_MelsecInit.RetryWaitTime = 100;                     // 通信リトライ時の再送信待ち時間[msec]
            m_MelsecInit.SumCheck      = false;                   // サムチェック有無

            // ＭＥＬＳＥＣ通信ＡＰＩの初期化
            m_MelsecInst = DllInitialize(ref m_MelsecInit, Program.GetWindow().Handle);

            if (m_MelsecInst >= 0) {
                // 通信パラメータの読込み
                {
                    CProfile profile = new CProfile(Program.GetDirectory(), Program.FN_PROFILE);

                    // プロファイルからＰＬＣ通信設定を読込む!!
                    string szAppName = "PLC";

                    m_stno      = 0;                                                                       // ＰＬＣ側局番号
                    m_networkno = (byte )profile.GetValue(szAppName,  "NETWORK",       0);                 // ＰＬＣ側ネットワーク番号
                    m_pcno      = (byte )profile.GetValue(szAppName,  "NO",          255);                 // ＰＬＣ側ＰＣ番号
                    m_portno    = (UInt16)profile.GetValue(szAppName, "PORT",       4096);                 // ＰＬＣ側ポート番号

                    m_ipaddr    = profile.GetString(szAppName, "IP-ADDR", "192.168.3.100");            // ＰＬＣ側ＩＰアドレス
                }

                return true;
            }

            // 通信ＡＰＩの解放
            DllRelease(m_MelsecInst);

            return false;
        }

        // ＭＣプロトコル通信ＡＰＩ解放
        public static bool Release()
        {
            // 通信ＡＰＩの解放
            if (DllRelease(m_MelsecInst) == 0)
            {
                // ソケットインタフェースクリーンナップ
                WSACleanup();

                return true;
            }
            else return false;
        }

        // ＭＣプロトコル通信接続
        public static PLC_ERR Com1_OPEN()
        {
            return ((PLC_ERR)MelsecConnect(m_ipaddr, m_portno));
        }
        
        // ＭＣプロトコル通信切断
        public static PLC_ERR Com1_CLOSE()
        {
            return ((PLC_ERR)MelsecClose());
        }

        // ＭＣプロトコル通信状態
        public static bool IsConnect()
        {
            return MelsecIsConnect();
        }

        // ＰＬＣ一括書込み
        // ＰＬＣへ書込み処理(デバイスコード、先頭デバイス、デバイス点数,点数分データ)
        public static PLC_ERR DatSend(UInt16 DCODE, UInt32 SDEVS, UInt16 DLENS, UInt16[] DAT) {
            PLC_ERR  Ret;
            IntPtr   ptr;
            int      size;
            byte[]   bwrk;

            Ret  = 0;
            ptr  = IntPtr.Zero;
            size = DLENS * sizeof(UInt16);
            bwrk = new byte[size];

            try
            {
                if (MelsecIsConnect() == true)
                {
                    // アンマネージド配列のメモリ確保
                    ptr = Marshal.AllocHGlobal(size);

                    // データコピー
                    Buffer.BlockCopy(DAT, 0, bwrk, 0, size);
                    Marshal.Copy(bwrk, 0, ptr, size);

                    // ＭＥＬＳＥＣワードデバイス一括書込み
                    Ret = (PLC_ERR)MelsecWriteWords(m_stno,                         // 局番号
                                                    m_networkno,                    // ネットワーク番号
                                                    m_pcno,                         // ＰＣ番号
                                                    DCODE,                          // デバイス種別
                                                    SDEVS,                          // 先頭レジスタ番号
                                                    DLENS,                          // 書込みワード数
                                                    ptr);                           // データ格納バッファポインタ
                }
            }
            catch
            {
                // Ethernet回線データ送信異常（送信に失敗）
                Ret = PLC_ERR.ERR_0052;
            }
            finally
            {
                // アンマネージドのメモリを解放 
                if (ptr != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(ptr);
                }
            }

            return (Ret);
        }

        // ＰＬＣ一括読込み
        // ＰＬＣ読み込み処理(デバイスコード、先頭デバイス、デバイス点数)
        public static PLC_ERR DatRecv(UInt16 DCODE, UInt32 SDEVS, UInt16 DLENS) {
            PLC_ERR  Ret;
            IntPtr   ptr;
            int      size;
            byte[]   bwrk;

            Ret  = 0;
            ptr  = IntPtr.Zero;
            size = DLENS * sizeof(UInt16);
            bwrk = new byte[size];

            try
            {
                if (MelsecIsConnect() == true)
                {
                    // アンマネージド配列のメモリ確保
                    ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(UInt16)) * DLENS);

                    // ＭＥＬＳＥＣワードデバイス一括書込み
                    Ret = (PLC_ERR)MelsecReadWords(m_stno,                         // 局番号
                                                   m_networkno,                    // ネットワーク番号
                                                   m_pcno,                         // ＰＣ番号
                                                   DCODE,                          // デバイス種別
                                                   SDEVS,                          // 先頭レジスタ番号
                                                   DLENS,                          // 読込みワード数
                                                   ptr);                           // データ格納バッファポインタ

                    if (Ret == 0)
                    {
                        // データコピー
                        Marshal.Copy(ptr, bwrk, 0, size);
                        Buffer.BlockCopy(bwrk, 0, Buf, 0, size);

                        for (int i = 0; i <= Conversion.Val(DLENS); i++) {
                            Com01_Tbl01[i] = Buf[i];
                        }
                    }
                }
            }
            catch
            {
                // Ethernet回線データ受信異常（受信に失敗）
                Ret = PLC_ERR.ERR_0064;
            }
            finally
            {
                // アンマネージドのメモリを解放 
                if (ptr != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(ptr);
                }
            }

            return (Ret);
        }
        
//[2026/04/06][p.hoi][Add]=============================================================>>
//  PLCデータ読込、照合追加
//---------------------------------------------------------------------------------------
        // ＰＬＣ読み込み処理(デバイスコード、先頭デバイス、デバイス点数)
        public static PLC_ERR TagInfoRecv(UInt16 DCODE, UInt32 SDEVS, ref TAG_INFO dat) {
            PLC_ERR  Ret;
            IntPtr   ptr;
            int      size;

            Ret  = 0;
            ptr  = IntPtr.Zero;
            size = Marshal.SizeOf(typeof(TAG_INFO));

            try
            {
                if (MelsecIsConnect() == true)
                {
                    // アンマネージド配列のメモリ確保
                    ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(TAG_INFO)));

                    // ＭＥＬＳＥＣワードデバイス一括書込み
                    Ret = (PLC_ERR)MelsecReadWords(m_stno,                         // 局番号
                                                   m_networkno,                    // ネットワーク番号
                                                   m_pcno,                         // ＰＣ番号
                                                   DCODE,                          // デバイス種別
                                                   SDEVS,                          // 先頭レジスタ番号
                                                   (ushort)(size / 2),             // 読込みワード数
                                                   ptr);                           // データ格納バッファポインタ

                    if (Ret == 0)
                    {
                        // データコピー
                        dat = new TAG_INFO(Marshal.PtrToStructure<TAG_INFO>(ptr));
                    }
                }
            }
            catch
            {
                // Ethernet回線データ受信異常（受信に失敗）
                Ret = PLC_ERR.ERR_0064;
            }
            finally
            {
                // アンマネージドのメモリを解放 
                if (ptr != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(ptr);
                }
            }

            return (Ret);
        }

        // ＰＬＣ読み込み処理(デバイスコード、先頭デバイス、デバイス点数)
        public static PLC_ERR VehicleInfoRecv(UInt16 DCODE, UInt32 SDEVS, ref VEHICLE_INFO dat) {
            PLC_ERR  Ret;
            IntPtr   ptr;
            int      size;

            Ret  = 0;
            ptr  = IntPtr.Zero;
            size = Marshal.SizeOf(typeof(VEHICLE_INFO));

            try
            {
                if (MelsecIsConnect() == true)
                {
                    // アンマネージド配列のメモリ確保
                    ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(VEHICLE_INFO)));

                    // ＭＥＬＳＥＣワードデバイス一括書込み
                    Ret = (PLC_ERR)MelsecReadWords(m_stno,                         // 局番号
                                                   m_networkno,                    // ネットワーク番号
                                                   m_pcno,                         // ＰＣ番号
                                                   DCODE,                          // デバイス種別
                                                   SDEVS,                          // 先頭レジスタ番号
                                                   (ushort)(size / 2),             // 読込みワード数
                                                   ptr);                           // データ格納バッファポインタ

                    if (Ret == 0)
                    {
                        // データコピー
                        dat = new VEHICLE_INFO(Marshal.PtrToStructure<VEHICLE_INFO>(ptr));
                    }
                }
            }
            catch
            {
                // Ethernet回線データ受信異常（受信に失敗）
                Ret = PLC_ERR.ERR_0064;
            }
            finally
            {
                // アンマネージドのメモリを解放 
                if (ptr != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(ptr);
                }
            }

            return (Ret);
        }
//<<=====================================================================================
        

        // 文字列をアスキーコードへ変換
        public static string WcovAsc(string WDAT, int Wlen)
        {
            string rc;
            int    i;

            rc = string.Empty;

            for(i = 1; i <= Wlen; i += 2)
            {
                rc += Conversion.Hex(Strings.Asc(Strings.Mid(WDAT, i + 1, 1)));
                rc += Conversion.Hex(Strings.Asc(Strings.Mid(WDAT, i, 1)));
            }

            return rc;
        }

        // 通信エラーの通知
        public static string GetErrMsg(PLC_ERR err_code) {
            switch (err_code)
            {
                case PLC_ERR.ERR_0001: return ("初期化パラメータ内容不正");
                case PLC_ERR.ERR_0002: return ("初期化失敗（空きのインスタンス不足）");
                case PLC_ERR.ERR_0003: return ("API処理失敗（インスタンス未初期化）");
                case PLC_ERR.ERR_0004: return ("API解放失敗（インスタンス番号不正）");
                case PLC_ERR.ERR_0005: return ("API解放失敗（インスタンス未初期化）");

                case PLC_ERR.ERR_0040: return ("Ethernet回線接続異常（初期化伝送フレーム設定異常）");
                case PLC_ERR.ERR_0041: return ("Ethernet回線接続異常（既に回線オープン中）");
                case PLC_ERR.ERR_0042: return ("Ethernet回線接続異常（ソケット作成に失敗）");
                case PLC_ERR.ERR_0043: return ("Ethernet回線接続異常（イベント登録に失敗）");
                case PLC_ERR.ERR_0044: return ("Ethernet回線接続異常（イベント選択に失敗）");
                case PLC_ERR.ERR_0045: return ("Ethernet回線接続異常（接続監視タイムアウト）");
                case PLC_ERR.ERR_0046: return ("Ethernet回線接続異常（イベント種類抽出に失敗）");
                case PLC_ERR.ERR_0047: return ("Ethernet回線接続異常（イベント結果不正）");
                       
                case PLC_ERR.ERR_0051: return ("Ethernet回線データ送信異常（回線未接続状態）");
                case PLC_ERR.ERR_0052: return ("Ethernet回線データ送信異常（送信に失敗）");
                                     
                case PLC_ERR.ERR_0061: return ("Ethernet回線データ受信異常（回線未接続状態）");
                case PLC_ERR.ERR_0062: return ("Ethernet回線データ受信異常（回線切断検出）");
                case PLC_ERR.ERR_0063: return ("Ethernet回線データ受信異常（受信タイムアウト）");
                case PLC_ERR.ERR_0064: return ("Ethernet回線データ受信異常（受信に失敗）");
                                     
                case PLC_ERR.ERR_0090: return ("回線クロース異常（初期化伝送フレーム設定異常）");
                case PLC_ERR.ERR_0091: return ("シリアル回線クローズ異常（回線未オープン状態）");
                case PLC_ERR.ERR_0092: return ("Ethernet回線切断異常（回線未接続状態）");
                case PLC_ERR.ERR_0093: return ("Ethernet回線切断異常（shutdown命令に失敗）");
                case PLC_ERR.ERR_0094: return ("Ethernet回線切断異常（closesocket命令に失敗）");

                case PLC_ERR.ERR_0100: return ("コマンド編集異常（該当フレーム未サポート）");
                case PLC_ERR.ERR_0101: return ("コマンド編集異常（指定点数オーバー）");
                case PLC_ERR.ERR_0102: return ("コマンド編集異常（デバイス指定不正）");
                case PLC_ERR.ERR_0103: return ("コマンド編集異常（CPU型式指定不正）");
                case PLC_ERR.ERR_0104: return ("コマンド編集異常（ラベル名長不正）");
                case PLC_ERR.ERR_0105: return ("コマンド編集異常（ラベル名配列指定不正）");

                case PLC_ERR.ERR_0200: return ("レスポンス受信に失敗");
                case PLC_ERR.ERR_0201: return ("レスポンス内容異常（先頭コード不正）");
                case PLC_ERR.ERR_0202: return ("レスポンス内容異常（サブヘッダ不正）");
                case PLC_ERR.ERR_0203: return ("レスポンス内容異常（ブロック番号／シリアル番号不一致）");
                case PLC_ERR.ERR_0204: return ("レスポンス内容異常（局番号不一致）");
                case PLC_ERR.ERR_0205: return ("レスポンス内容異常（PC番号不一致）");
                case PLC_ERR.ERR_0206: return ("レスポンス内容異常（ETXコード不正）");
                case PLC_ERR.ERR_0207: return ("レスポンス内容異常（サムチェック不一致）");
                case PLC_ERR.ERR_0208: return ("レスポンス内容異常（ネットワーク番号不一致）");
                case PLC_ERR.ERR_0209: return ("レスポンス内容異常（要求先ユニットI/O番号不一致）");
                case PLC_ERR.ERR_0210: return ("レスポンス内容異常（要求先ユニット局番号不一致）");
                case PLC_ERR.ERR_0211: return ("レスポンス内容異常（応答データ長不一致）");
                case PLC_ERR.ERR_0212: return ("レスポンス内容異常（フレーム識別番号不正）");
                case PLC_ERR.ERR_0213: return ("レスポンス内容異常（自局番号不一致）");
                case PLC_ERR.ERR_0214: return ("レスポンス内容異常（応答識別コード不正）");
                case PLC_ERR.ERR_0215: return ("レスポンス内容異常（固定値不正）");
                case PLC_ERR.ERR_0216: return ("レスポンス内容異常（配列点数不正）");
                case PLC_ERR.ERR_0217: return ("レスポンス内容異常（データ型ID不正）");
                case PLC_ERR.ERR_0218: return ("レスポンス内容異常（読出し単位指定不正）");
                case PLC_ERR.ERR_0219: return ("レスポンス内容異常（読出し配列データ長不正）");
                case PLC_ERR.ERR_0220: return ("レスポンス内容異常（シリアル番号不一致）");
                case PLC_ERR.ERR_0221: return ("レスポンス内容異常（CR,LFコード不正）");
                case PLC_ERR.ERR_0222: return ("レスポンス内容異常（読出しデータ部不良）");

                case PLC_ERR.ERR_0300: return ("通信ログエクスポート異常（ファイルオープンに失敗）");
                case PLC_ERR.ERR_0301: return ("通信ログエクスポート異常（ファイルアクセス障害発生）");

                case PLC_ERR.ERR_0400: return ("自動運転中の為転送できません。");

                default  : if ((int)err_code > 1000) { return ("レスポンス内容異常（PLCエラー応答）err code = " + ((int)err_code - 1000)); } break;
            }

            return string.Empty;
        }

//[2026/04/06][p.hoi][Add]=============================================================>>
//  PLCデータ読込、照合追加

//---------------------------------------------------------------------------------------
        /*******************************************************************************

	        構造体定義

        *******************************************************************************/
        /***********************************************************************
            タグ情報造体
        ***********************************************************************/
        [StructLayout(layoutKind, CharSet = charSet, Pack = packSize)]
        public struct TAG_INFO
        {
            public  UInt16    JigType;                  // 治具タイプ
            public  UInt16    JigLR;                    // 治具 LH/RH
            public  UInt16    JigStock;                 // 治具ｽﾄｯｸ段
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public  UInt16[]  Dmy;

            // コンストラクタ
            public TAG_INFO(Nullable<TAG_INFO> buf)
            {
                Dmy      = new UInt16[2];

                if (buf.HasValue == true) {
                    // コピーする
                    for (int ii = 0; ii < Dmy.Length; ii++) { Dmy[ii] = buf.Value.Dmy[ii];}

                    JigType  = buf.Value.JigType;
                    JigLR    = buf.Value.JigLR;
                    JigStock = buf.Value.JigStock;
                }
                else {
                    // クリアする
                    for (int ii = 0; ii < Dmy.Length;      ii++) { Dmy[ii]      = 0; }
                    JigType  = 0;
                    JigLR    = 0;
                    JigStock = 0;
                }
            }

            // 空のバッファを取得する
            public static TAG_INFO Empty
            {
                get
                {
                    return (new TAG_INFO(null));
                }
            }

            public bool IsEmpty()
            {
                if((JigLR    == 0) &&
                   (JigStock == 0)) {
                    return true;
                }

                return false;
            }
        }

        /***********************************************************************
            車種情報構造体
        ***********************************************************************/

        [StructLayout(layoutKind, CharSet = charSet, Pack = packSize)]
        public struct VEHICLE_INFO
        {
            public  HEAD_DAT  Head;                     // ヘッダー部（共通項目）
            public  DATA      LhDat;                    // ＬＨ詳細部
            public  DATA      RhDat;                    // ＲＨ詳細部

            // コンストラクタ
            public VEHICLE_INFO(Nullable<VEHICLE_INFO> buf)
            {
                Head  = new HEAD_DAT(null);
                LhDat = new DATA(null);
                RhDat = new DATA(null);

                if (buf.HasValue == true) {
                    // コピーする
                    Head  = new HEAD_DAT(buf.Value.Head);
                    LhDat = new DATA(buf.Value.LhDat);
                    RhDat = new DATA(buf.Value.RhDat);
                }
                else {
                    // クリアする
                    Head  = HEAD_DAT.Empty;
                    LhDat = DATA.Empty;
                    RhDat = DATA.Empty;
                }
            }

            // 空のバッファを取得する
            public static VEHICLE_INFO Empty
            {
                get
                {
                    return (new VEHICLE_INFO(null));
                }
            }
            
            public bool IsEmpty()
            {
                if(string.IsNullOrEmpty(Head.IdenCode)) {
                    return true;
                }

                return false;
            }
        }

        /***********************************************************************
            ヘッダー部（共通項目）構造体
        ***********************************************************************/

        [StructLayout(layoutKind, CharSet = charSet, Pack = packSize)]
        public struct HEAD_DAT
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            private byte[]	  idenCode;                 // 識別コード
            public  UInt16    JigType;                  // 治具タイプ
            public  UInt16    JigOrder;                 // 治具切出し順
            public  UInt16    Jig1st;                   // １ｓｔ治具ｽﾄｯｸ段
            public  UInt16    Jig2nd;                   // ２ｎｄ治具ｽﾄｯｸ段
            public  UInt16    Dest;                     // 向先
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 25)]
            public  UInt16[]  Dmy;

            // コンストラクタ
            public HEAD_DAT(Nullable<HEAD_DAT> buf)
            {
                idenCode = new byte[20];
                Dmy      = new UInt16[25];

                if (buf.HasValue == true) {
                    // コピーする
                    for (int ii = 0; ii < idenCode.Length; ii++) { idenCode[ii] = buf.Value.idenCode[ii]; }
                    for (int ii = 0; ii < Dmy.Length;      ii++) { Dmy[ii]      = buf.Value.Dmy[ii];      }

                    JigType  = buf.Value.JigType;
                    JigOrder = buf.Value.JigOrder;
                    Jig1st   = buf.Value.Jig1st;
                    Jig2nd   = buf.Value.Jig2nd;
                    Dest     = buf.Value.Dest;
                }
                else {
                    // クリアする
                    for (int ii = 0; ii < idenCode.Length; ii++) { idenCode[ii] = 0; }
                    for (int ii = 0; ii < Dmy.Length;      ii++) { Dmy[ii]      = 0; }

                    JigType  = 0;
                    JigOrder = 0;
                    Jig1st   = 0;
                    Jig2nd   = 0;
                    Dest     = 0;
                }
            }

            // 空のバッファを取得する
            public static HEAD_DAT Empty
            {
                get
                {
                    return (new HEAD_DAT(null));
                }
            }

            public string IdenCode { set {  byte[] wrk = Encoding.ASCII.GetBytes(value);
                                            Array.Copy(wrk, idenCode, Math.Min(20, wrk.Length)); }
                                     get { return (Encoding.ASCII.GetString(idenCode).Trim('\0', ' ')); } }
        }

        /***********************************************************************
            詳細部
        ***********************************************************************/

        [StructLayout(layoutKind, CharSet = charSet, Pack = packSize)]
        public struct DATA
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public UInt16[]  St01;                                  // ＳＴ０１
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public UInt16[]  St02;                                  // ＳＴ０２
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public UInt16[]  St03;                                  // ＳＴ０３
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public UInt16[]  St04;                                  // ＳＴ０４
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public UInt16[]  St05;                                  // ＳＴ０５
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public UInt16[]  St06;                                  // ＳＴ０６
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public UInt16[]  St07;                                  // ＳＴ０７
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public UInt16[]  St08;                                  // ＳＴ０８
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public UInt16[]  St09;                                  // ＳＴ０９
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public UInt16[]  St10;                                  // ＳＴ１０
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public UInt16[]  St11;                                  // ＳＴ１１
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public UInt16[]  St12;                                  // ＳＴ１２
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public UInt16[]  St13;                                  // ＳＴ１３
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 25)]
            public UInt16[]  St14;                                  // ＳＴ１４
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public UInt16[]  St15;                                  // ＳＴ１５
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public UInt16[]  St16;                                  // ＳＴ１６

            public UInt16    SeatType;                  // シートタイプ
            public UInt16    AgType;                    // AGタイプ
            public UInt16    Heater;                    // ヒーター
            public UInt16    Buckle;                    // バックル
            public UInt16    Headrest;                  // ヘッドレスト
            public UInt16    SeatSen;                   // 着座センサー
            public UInt16    AirCond;                   // 空調
            public UInt16    Upholstery;                // 表皮材
            public UInt16    TorqueWrench;              // トルクレンチ(色)
            public UInt16    Lumbar;                    // ランバー
            public UInt16    BackPock;                  // 背面ポケット
            public UInt16    FootwellLamp;              // フットウェルランプ
            public UInt16    Ottoman;                   //オットマン-- new
            public UInt16    ConvHook;                  //コンビニフック--new
            public UInt16    Site_Table;                //サイトデーブル--new
            public UInt16    Robot;                     //ロボット--new
            public UInt16    Backboard;                 //バックポード--new
            public UInt16    QRG;                       //QRG--new
            public UInt16    ArmRest;                   // ｱｰﾑﾚｽﾄ--new
            public UInt16    ISOFIX;                    // ISOFIX--new
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)]
            public UInt16[] dmy;


            // コンストラクタ
            public DATA(Nullable<DATA> buf)
            {
                St01 = new UInt16[10];
                St02 = new UInt16[10];
                St03 = new UInt16[10];
                St04 = new UInt16[10];
                St05 = new UInt16[10];
                St06 = new UInt16[10];
                St07 = new UInt16[10];
                St08 = new UInt16[10];
                St09 = new UInt16[10];
                St10 = new UInt16[10];
                St11 = new UInt16[10];
                St12 = new UInt16[20];
                St13 = new UInt16[20];
                St14 = new UInt16[25];
                St15 = new UInt16[10];
                St16 = new UInt16[10];
                dmy  = new UInt16[15];

                if (buf.HasValue == true) {
                    // コピーする
                    for (int ii = 0; ii < St01.Length; ii++) { St01[ii] = buf.Value.St01[ii]; }
                    for (int ii = 0; ii < St02.Length; ii++) { St02[ii] = buf.Value.St02[ii]; }
                    for (int ii = 0; ii < St03.Length; ii++) { St03[ii] = buf.Value.St03[ii]; }
                    for (int ii = 0; ii < St04.Length; ii++) { St04[ii] = buf.Value.St04[ii]; }
                    for (int ii = 0; ii < St05.Length; ii++) { St05[ii] = buf.Value.St05[ii]; }
                    for (int ii = 0; ii < St06.Length; ii++) { St06[ii] = buf.Value.St06[ii]; }
                    for (int ii = 0; ii < St07.Length; ii++) { St07[ii] = buf.Value.St07[ii]; }
                    for (int ii = 0; ii < St08.Length; ii++) { St08[ii] = buf.Value.St08[ii]; }
                    for (int ii = 0; ii < St09.Length; ii++) { St09[ii] = buf.Value.St09[ii]; }
                    for (int ii = 0; ii < St10.Length; ii++) { St10[ii] = buf.Value.St10[ii]; }
                    for (int ii = 0; ii < St11.Length; ii++) { St11[ii] = buf.Value.St11[ii]; }
                    for (int ii = 0; ii < St12.Length; ii++) { St12[ii] = buf.Value.St12[ii]; }
                    for (int ii = 0; ii < St13.Length; ii++) { St13[ii] = buf.Value.St13[ii]; }
                    for (int ii = 0; ii < St14.Length; ii++) { St14[ii] = buf.Value.St14[ii]; }
                    for (int ii = 0; ii < St15.Length; ii++) { St15[ii] = buf.Value.St15[ii]; }
                    for (int ii = 0; ii < St16.Length; ii++) { St16[ii] = buf.Value.St16[ii]; }
                    for (int ii = 0; ii < dmy.Length;  ii++) { dmy[ii]  = buf.Value.dmy[ii];  }

                    SeatType     = buf.Value.SeatType    ;
                    AgType       = buf.Value.AgType      ;
                    Heater       = buf.Value.Heater      ;
                    Buckle       = buf.Value.Buckle      ;
                    Headrest     = buf.Value.Headrest    ;
                    SeatSen      = buf.Value.SeatSen     ;
                    AirCond      = buf.Value.AirCond     ;
                    Upholstery   = buf.Value.Upholstery  ;
                    TorqueWrench = buf.Value.TorqueWrench;
                    Lumbar       = buf.Value.Lumbar      ;
                    BackPock     = buf.Value.BackPock    ;
                    FootwellLamp = buf.Value.FootwellLamp;
                    Ottoman      = buf.Value.Ottoman     ;
                    ConvHook     = buf.Value.ConvHook    ;
                    Site_Table   = buf.Value.Site_Table  ;
                    Robot        = buf.Value.Robot       ;
                    Backboard    = buf.Value.Backboard   ;
                    QRG          = buf.Value.QRG         ;
                    ArmRest      = buf.Value.ArmRest     ;
                    ISOFIX       = buf.Value.ISOFIX      ;


                }
                else {
                    // クリアする
                    for (int ii = 0; ii < St01.Length; ii++) { St01[ii] = 0; }
                    for (int ii = 0; ii < St02.Length; ii++) { St02[ii] = 0; }
                    for (int ii = 0; ii < St03.Length; ii++) { St03[ii] = 0; }
                    for (int ii = 0; ii < St04.Length; ii++) { St04[ii] = 0; }
                    for (int ii = 0; ii < St05.Length; ii++) { St05[ii] = 0; }
                    for (int ii = 0; ii < St06.Length; ii++) { St06[ii] = 0; }
                    for (int ii = 0; ii < St07.Length; ii++) { St07[ii] = 0; }
                    for (int ii = 0; ii < St08.Length; ii++) { St08[ii] = 0; }
                    for (int ii = 0; ii < St09.Length; ii++) { St09[ii] = 0; }
                    for (int ii = 0; ii < St10.Length; ii++) { St10[ii] = 0; }
                    for (int ii = 0; ii < St11.Length; ii++) { St11[ii] = 0; }
                    for (int ii = 0; ii < St12.Length; ii++) { St12[ii] = 0; }
                    for (int ii = 0; ii < St13.Length; ii++) { St13[ii] = 0; }
                    for (int ii = 0; ii < St14.Length; ii++) { St14[ii] = 0; }
                    for (int ii = 0; ii < St15.Length; ii++) { St15[ii] = 0; }
                    for (int ii = 0; ii < St16.Length; ii++) { St16[ii] = 0; }
                    for (int ii = 0; ii < dmy.Length;  ii++) { dmy[ii]  = 0; }
                    SeatType     = 0;
                    AgType       = 0;
                    Heater       = 0;
                    Buckle       = 0;
                    Headrest     = 0;
                    SeatSen      = 0;
                    AirCond      = 0;
                    Upholstery   = 0;
                    TorqueWrench = 0;
                    Lumbar       = 0;
                    BackPock     = 0;
                    FootwellLamp = 0;
                    Ottoman      = 0;
                    ConvHook     = 0;
                    Site_Table   = 0;
                    Robot        = 0;
                    Backboard    = 0;
                    QRG          = 0;
                    ArmRest      = 0;
                    ISOFIX       = 0;

                }
            }

            // 空のバッファを取得する
            public static DATA Empty
            {
                get
                {
                    return (new DATA(null));
                }
            }

            public int GetRecipeNo(int stno, int idx) {      // レシピNo
                int ret;

                ret = 0;
                
                switch (stno)
                {
                    case  1: if(idx < St01.Length) ret = St01[idx]; break;
                    case  2: if(idx < St02.Length) ret = St02[idx]; break;
                    case  3: if(idx < St03.Length) ret = St03[idx]; break;
                    case  4: if(idx < St04.Length) ret = St04[idx]; break;
                    case  5: if(idx < St05.Length) ret = St05[idx]; break;
                    case  6: if(idx < St06.Length) ret = St06[idx]; break;
                    case  7: if(idx < St07.Length) ret = St07[idx]; break;
                    case  8: if(idx < St08.Length) ret = St08[idx]; break;
                    case  9: if(idx < St09.Length) ret = St09[idx]; break;
                    case 10: if(idx < St10.Length) ret = St10[idx]; break;
                    case 11: if(idx < St11.Length) ret = St11[idx]; break;
                    case 12: if(idx < St12.Length) ret = St12[idx]; break;
                    case 13: if(idx < St13.Length) ret = St13[idx]; break;
                    case 14: if(idx < St14.Length) ret = St14[idx]; break;
                    case 15: if(idx < St15.Length) ret = St15[idx]; break;
                    case 16: if(idx < St16.Length) ret = St16[idx]; break;
                }

                // 下位12ﾋﾞｯﾄ
                ret = ret & 0x0FFF; 

                return (ret); 
            }

            public UInt16 GetSt(int stno, int idx) {      // 回数
                UInt16 ret;

                ret = 0;

                switch (stno)
                {
                    case  1: if(idx < St01.Length) ret = St01[idx]; break;
                    case  2: if(idx < St02.Length) ret = St02[idx]; break;
                    case  3: if(idx < St03.Length) ret = St03[idx]; break;
                    case  4: if(idx < St04.Length) ret = St04[idx]; break;
                    case  5: if(idx < St05.Length) ret = St05[idx]; break;
                    case  6: if(idx < St06.Length) ret = St06[idx]; break;
                    case  7: if(idx < St07.Length) ret = St07[idx]; break;
                    case  8: if(idx < St08.Length) ret = St08[idx]; break;
                    case  9: if(idx < St09.Length) ret = St09[idx]; break;
                    case 10: if(idx < St10.Length) ret = St10[idx]; break;
                    case 11: if(idx < St11.Length) ret = St11[idx]; break;
                    case 12: if(idx < St12.Length) ret = St12[idx]; break;
                    case 13: if(idx < St13.Length) ret = St13[idx]; break;
                    case 14: if(idx < St14.Length) ret = St14[idx]; break;
                    case 15: if(idx < St15.Length) ret = St15[idx]; break;
                    case 16: if(idx < St16.Length) ret = St16[idx]; break;
                }

                // 上位4ビット
                ret = (UInt16)(ret >> 12); 

                return (ret); 
            }


            public void SetSt(int stno, int idx, UInt16 dat)
            {
                switch (stno)
                {
                    case  1: if (idx < St01.Length) St01[idx] = dat; break;
                    case  2: if (idx < St02.Length) St02[idx] = dat; break;
                    case  3: if (idx < St03.Length) St03[idx] = dat; break;
                    case  4: if (idx < St04.Length) St04[idx] = dat; break;
                    case  5: if (idx < St05.Length) St05[idx] = dat; break;
                    case  6: if (idx < St06.Length) St06[idx] = dat; break;
                    case  7: if (idx < St07.Length) St07[idx] = dat; break;
                    case  8: if (idx < St08.Length) St08[idx] = dat; break;
                    case  9: if (idx < St09.Length) St09[idx] = dat; break;
                    case 10: if (idx < St10.Length) St10[idx] = dat; break;
                    case 11: if (idx < St11.Length) St11[idx] = dat; break;
                    case 12: if (idx < St12.Length) St12[idx] = dat; break;
                    case 13: if (idx < St13.Length) St13[idx] = dat; break;
                    case 14: if (idx < St14.Length) St14[idx] = dat; break;
                    case 15: if (idx < St15.Length) St15[idx] = dat; break;
                    case 16: if (idx < St16.Length) St16[idx] = dat; break;
                }
            }
        }
//<<=====================================================================================


        /***********************************************************************

            ＭＣプロトコル通信メソッド

        ***********************************************************************/
        /***********************************************************************
            ＭＣプロトコル通信回線接続
        ***********************************************************************/
        private static UInt32 MelsecConnect(
            string      ip_addr,        // ＰＬＣ側ＩＰアドレス
            UInt16      port_no         // ＰＬＣ側ポート番号
        )
        // [戻り値]
        //  TRUE  : 成功
        //  FALSE : 失敗
        {

            MELSEC_CONNECT_PARM param;

            // 通信回線接続パラメータの初期化
            param = new MELSEC_CONNECT_PARM(null);

            byte[] dmy = CEncode.GetBytes(ip_addr.Trim());
            for(int i = 0; (i < param.IpAddr.Count()) && (i < dmy.Count()); i++) { param.IpAddr[i] = dmy[i]; }
            param.PortNo     = port_no;
            param.ConTimeOut = 3000;
            param.Protocol   = MELSEC_TCP_IP;

            // イーサネット通信回線の接続

            return (DllConnect(m_MelsecInst, ref param));
        }

        /***********************************************************************
            ＭＣプロトコル通信回線切断
        ***********************************************************************/
        private static UInt32 MelsecClose(
        )
        // [戻り値]
        //  TRUE  : 成功
        //  FALSE : 失敗
        {
            // 通信回線のクローズ
            return (DllClose(m_MelsecInst));
        }

        /***********************************************************************
            ＭＣプロトコル通信回線状況チェック
        ***********************************************************************/
        private static bool MelsecIsConnect(
            // パラメータなし
        )
        // [戻り値]
        //  TRUE  : 接続中
        //  FALSE : 切断中(未接続)
        {
            bool result;
            bool rc;

            result = false;

            // 通信回線接続状態の獲得
            rc = DllIsOpen(m_MelsecInst);
            if (rc == true)
            {
                result = true;
            }

            return (result);
        }

        /***********************************************************************
            ＭＣプロトコルビットデバイス一括読込み
        ***********************************************************************/
        private static UInt32 MelsecReadBits(
            byte    st_no,          // 局番号
            byte    network_no,     // ネットワーク番号
            byte    pc_no,          // ＰＣ番号
            UInt16  dev_type,       // デバイス種別
            UInt32  dev_no,         // 先頭レジスタ番号
            UInt16  dev_cnt,        // 読込みビット数
            IntPtr  dev_dat         // データ格納バッファ
        )
        // [戻り値]
        //  TRUE  : 成功
        //  FALSE : 失敗
        {
            MELSEC_DEV      dev;
            MELSEC_COM_PARM parm;

            parm    = new MELSEC_COM_PARM(null);

            // ＭＣプロトコル通信パラメータの初期化
            DllGetComParm(m_MelsecInst, ref parm);

            parm.StNo   = st_no;         // 局番号
            parm.NetNo  = network_no;    // ネットワーク番号
            parm.PcNo   = pc_no;         // ＰＣ番号

            // ビットデバイスの一括読込み
            dev = new MELSEC_DEV(null);
            DllDevCs(dev_type, dev_no, ref dev);

            return (DllReadBits(m_MelsecInst, ref parm, dev, dev_cnt, dev_dat));
        }

        /***********************************************************************
            ＭＣプロトコルワードデバイス一括読込み
        ***********************************************************************/
        private static UInt32 MelsecReadWords(
            byte    st_no,          // 局番号
            byte    network_no,     // ネットワーク番号
            byte    pc_no,          // ＰＣ番号
            UInt16  dev_type,       // デバイス種別
            UInt32  dev_no,         // 先頭レジスタ番号
            UInt16  dev_cnt,        // 読込みワード数
            IntPtr  dev_dat         // データ格納バッファ
        )
        // [戻り値]
        //  TRUE  : 成功
        //  FALSE : 失敗
        {
            MELSEC_DEV      dev;
            MELSEC_COM_PARM parm;
            
            parm    = new MELSEC_COM_PARM(null);

            // ＭＣプロトコル通信パラメータの初期化
            DllGetComParm(m_MelsecInst, ref parm);

            parm.StNo   = st_no;         // 局番号
            parm.NetNo  = network_no;    // ネットワーク番号
            parm.PcNo   = pc_no;         // ＰＣ番号


            // ワードデバイスの一括読込み
            dev = new MELSEC_DEV(null);
            DllDevCs(dev_type, dev_no, ref dev);

            return (DllReadWords(m_MelsecInst, ref parm, dev, dev_cnt, dev_dat));
        }

        /***********************************************************************
            ＭＣプロトコルビットデバイス一括書込み
        ***********************************************************************/
        private static UInt32 MelsecWriteBits(
            byte    st_no,          // 局番号
            byte    network_no,     // ネットワーク番号
            byte    pc_no,          // ＰＣ番号
            UInt16  dev_type,       // デバイス種別
            UInt32  dev_no,         // 先頭レジスタ番号
            UInt16  dev_cnt,        // 書込みビット数
            IntPtr  dev_dat         // データ格納バッファポインタ
        )
        // [戻り値]
        //  TRUE  : 成功
        //  FALSE : 失敗
        {
            MELSEC_DEV      dev;
            MELSEC_COM_PARM parm;
            
            parm    = new MELSEC_COM_PARM(null);

            // ＭＣプロトコル通信パラメータの初期化
            DllGetComParm(m_MelsecInst, ref parm);

            parm.StNo  = st_no;         // 局番号
            parm.NetNo = network_no;    // ネットワーク番号
            parm.PcNo  = pc_no;         // ＰＣ番号

            // ビットデバイスの一括書込み
            dev = new MELSEC_DEV(null);
            DllDevCs(dev_type, dev_no, ref dev);

            return (DllWriteBits(m_MelsecInst, ref parm, dev, dev_cnt, dev_dat));
        }

        /***********************************************************************
            ＭＣプロトコルワードデバイス一括書込み
        ***********************************************************************/
        private static UInt32 MelsecWriteWords(
            byte    st_no,          // 局番号
            byte    network_no,     // ネットワーク番号
            byte    pc_no,          // ＰＣ番号
            UInt16  dev_type,       // デバイス種別
            UInt32  dev_no,         // 先頭レジスタ番号
            UInt16  dev_cnt,        // 書込みワード数
            IntPtr  dev_dat         // データ格納バッファポインタ
        )
        // [戻り値]
        //  TRUE  : 成功
        //  FALSE : 失敗
        {
            MELSEC_DEV      dev;
            MELSEC_COM_PARM parm;
            
            parm    = new MELSEC_COM_PARM(null);

            // ＭＣプロトコル通信パラメータの初期化
            DllGetComParm(m_MelsecInst, ref parm);

            parm.StNo  = st_no;         // 局番号
            parm.NetNo = network_no;    // ネットワーク番号
            parm.PcNo  = pc_no;         // ＰＣ番号

            // ワードデバイスの一括書込み
            dev = new MELSEC_DEV(null);
            DllDevCs(dev_type, dev_no, ref dev);

            return (DllWriteWords(m_MelsecInst, ref parm, dev, dev_cnt, dev_dat));
        }
        
        
        //
        // エラー情報定義
        public enum PLC_ERR {
            //  エラーメッセージ一                                                  エラーコード
            ERR_0001 = 1,           // 初期化パラメータ内容不正
            ERR_0002 = 2,           // 初期化失敗（空きのインスタンス不足）
            ERR_0003 = 3,           // API処理失敗（インスタンス未初期化）
            ERR_0004 = 4,           // API解放失敗（インスタンス番号不正）
            ERR_0005 = 5,           // API解放失敗（インスタンス未初期化）
                                    // 
            ERR_0040 = 40,          // Ethernet回線接続異常（初期化伝送フレーム設定異常）
            ERR_0041 = 41,          // Ethernet回線接続異常（既に回線オープン中）
            ERR_0042 = 42,          // Ethernet回線接続異常（ソケット作成に失敗）
            ERR_0043 = 43,          // Ethernet回線接続異常（イベント登録に失敗）
            ERR_0044 = 44,          // Ethernet回線接続異常（イベント選択に失敗）
            ERR_0045 = 45,          // Ethernet回線接続異常（接続監視タイムアウト）
            ERR_0046 = 46,          // Ethernet回線接続異常（イベント種類抽出に失敗）
            ERR_0047 = 47,          // Ethernet回線接続異常（イベント結果不正）
                                    // 
            ERR_0051 = 51,          // Ethernet回線データ送信異常（回線未接続状態）
            ERR_0052 = 52,          // Ethernet回線データ送信異常（送信に失敗）
                                    // 
            ERR_0061 = 61,          // Ethernet回線データ受信異常（回線未接続状態）
            ERR_0062 = 62,          // Ethernet回線データ受信異常（回線切断検出）
            ERR_0063 = 63,          // Ethernet回線データ受信異常（受信タイムアウト）
            ERR_0064 = 64,          // Ethernet回線データ受信異常（受信に失敗）
                                    // 
            ERR_0090 = 90,          // 回線クロース異常（初期化伝送フレーム設定異常）
            ERR_0091 = 91,          // シリアル回線クローズ異常（回線未オープン状態）
            ERR_0092 = 92,          // Ethernet回線切断異常（回線未接続状態）
            ERR_0093 = 93,          // Ethernet回線切断異常（shutdown命令に失敗）
            ERR_0094 = 94,          // Ethernet回線切断異常（closesocket命令に失敗）
                                    // 
            ERR_0100 = 100,         // コマンド編集異常（該当フレーム未サポート）
            ERR_0101 = 101,         // コマンド編集異常（指定点数オーバー）
            ERR_0102 = 102,         // コマンド編集異常（デバイス指定不正）
            ERR_0103 = 103,         // コマンド編集異常（CPU型式指定不正）
            ERR_0104 = 104,         // コマンド編集異常（ラベル名長不正）
            ERR_0105 = 105,         // コマンド編集異常（ラベル名配列指定不正）
                                    // 
            ERR_0200 = 200,         // レスポンス受信に失敗
            ERR_0201 = 201,         // レスポンス内容異常（先頭コード不正）
            ERR_0202 = 202,         // レスポンス内容異常（サブヘッダ不正）
            ERR_0203 = 203,         // レスポンス内容異常（ブロック番号／シリアル番号不一致）
            ERR_0204 = 204,         // レスポンス内容異常（局番号不一致）
            ERR_0205 = 205,         // レスポンス内容異常（PC番号不一致）
            ERR_0206 = 206,         // レスポンス内容異常（ETXコード不正）
            ERR_0207 = 207,         // レスポンス内容異常（サムチェック不一致）
            ERR_0208 = 208,         // レスポンス内容異常（ネットワーク番号不一致）
            ERR_0209 = 209,         // レスポンス内容異常（要求先ユニットI/O番号不一致）
            ERR_0210 = 210,         // レスポンス内容異常（要求先ユニット局番号不一致）
            ERR_0211 = 211,         // レスポンス内容異常（応答データ長不一致）
            ERR_0212 = 212,         // レスポンス内容異常（フレーム識別番号不正）
            ERR_0213 = 213,         // レスポンス内容異常（自局番号不一致）
            ERR_0214 = 214,         // レスポンス内容異常（応答識別コード不正）
            ERR_0215 = 215,         // レスポンス内容異常（固定値不正）
            ERR_0216 = 216,         // レスポンス内容異常（配列点数不正）
            ERR_0217 = 217,         // レスポンス内容異常（データ型ID不正）
            ERR_0218 = 218,         // レスポンス内容異常（読出し単位指定不正）
            ERR_0219 = 219,         // レスポンス内容異常（読出し配列データ長不正）
            ERR_0220 = 220,         // レスポンス内容異常（シリアル番号不一致）
            ERR_0221 = 221,         // レスポンス内容異常（CR,LFコード不正）
            ERR_0222 = 222,         // レスポンス内容異常（読出しデータ部不良）

            ERR_0300 = 300,         // 通信ログエクスポート異常（ファイルオープンに失敗）
            ERR_0301 = 301,         // 通信ログエクスポート異常（ファイルアクセス障害発生）

            ERR_0400 = 400,         // 自動運転中の為転送できません。

            ERR_1000 = 1000,        // レスポンス内容異常（PLCエラー応答）

            ERR_NONE = 0,
        }


        /*******************************************************************************

	        構造体定義

        *******************************************************************************/
        public delegate void MELSEC_CBF_WRITE_LOG(
            UInt32 inst_no,                                            // APIインスタンス番号
            IntPtr header_text,                                        // ログヘッダーテキスト
            UInt32 blng,                                               // 通信データバイト数
            IntPtr data,                                               // 通信データ
            IntPtr vptr                                                // 汎用ポインタ値
        );

        // PLC CPU種類
        private const UInt16 MELSEC_CPU_A       = 0;                   // ACPU
        private const UInt16 MELSEC_CPU_AnA     = 1;                   // AnACPU
        private const UInt16 MELSEC_CPU_QnA     = 2;                   // QnACPU
        private const UInt16 MELSEC_CPU_Qn      = 3;                   // QnCPU
        private const UInt16 MELSEC_CPU_iQR     = 4;                   // iQRCPU
            
        // 伝送フレーム
	    private const UInt16 MELSEC_FRAME_1C    = 0;                   // A互換1Cフレーム
        private const UInt16 MELSEC_FRAME_2C    = 1;                   // QnA互換2Cフレーム
        private const UInt16 MELSEC_FRAME_3C    = 2;                   // QnA互換3Cフレーム
        private const UInt16 MELSEC_FRAME_4C    = 3;                   // QnA互換4Cフレーム
	    private const UInt16 MELSEC_FRAME_1E    = 4;                   // A互換1Eフレーム
	    private const UInt16 MELSEC_FRAME_3E    = 5;                   // QnA互換3Eフレーム
	    private const UInt16 MELSEC_FRAME_4E    = 6;                   // 4Eフレーム

        // 通信形式
        private const UInt16 MELSEC_PRTCL0      = 0;                   // 形式無し
        private const UInt16 MELSEC_PRTCL1      = 1;                   // 形式1
	    private const UInt16 MELSEC_PRTCL2      = 2;                   // 形式2
	    private const UInt16 MELSEC_PRTCL3      = 3;                   // 形式3
	    private const UInt16 MELSEC_PRTCL4      = 4;                   // 形式4
	    private const UInt16 MELSEC_PRTCL5      = 5;                   // 形式5 (QnA互換4Cフレーム時のみ有効)

        // 伝送コード形式
        private const UInt16 MELSEC_CODE_ASCII  = 0;                   // 伝文コード形式（ASCII）
        private const UInt16 MELSEC_CODE_BINARY = 1;                   // 伝文コード形式（BINARY）

        /***********************************************************************
            パラメータ構造体
        ***********************************************************************/

        [StructLayout(layoutKind, CharSet = charSet, Pack = packSize)]
        public struct MELSEC_PARM
        {
            public  UInt16	            CpuType;                 // PLC CPU種類
                                                                 //    =MELSEC_CPU_A:   ACPU
                                                                 //    =MELSEC_CPU_AnA: AnACPU
                                                                 //    =MELSEC_CPU_QnA: QnACPU
                                                                 //    =MELSEC_CPU_Qn:  QnCPU
                                                                 //    =MELSEC_CPU_iQR: iQRCPU
            public  UInt16              FrameType;               // 伝送フレーム
                                                                 //    =MELSEC_FRAME_1C: A互換1Cフレーム
                                                                 //    =MELSEC_FRAME_2C: QnA互換2Cフレーム
                                                                 //    =MELSEC_FRAME_3C: QnA互換3Cフレーム
                                                                 //    =MELSEC_FRAME_4C: QnA互換4Cフレーム
                                                                 //    =MELSEC_FRAME_1E: A互換1Eフレーム
                                                                 //    =MELSEC_FRAME_3E: QnA互換3Eフレーム
                                                                 //    =MELSEC_FRAME_4E: 4Eフレーム
            public  UInt16              Protocol;                // 通信形式
                                                                 //    =MELSEC_PRTCL0: 形式無し
                                                                 //    =MELSEC_PRTCL1: 形式1
                                                                 //    =MELSEC_PRTCL2: 形式2
                                                                 //    =MELSEC_PRTCL3: 形式3
                                                                 //    =MELSEC_PRTCL4: 形式4
                                                                 //    =MELSEC_PRTCL5: 形式5 (QnA互換4Cフレーム時のみ有効)
            public  UInt16              Code;                    // 伝送コード形式
                                                                 //    =MELSEC_CODE_ASCII:  ASCIIコード
                                                                 //    =MELSEC_CODE_BINARY: BINARYコード
            public  UInt16              Retry;                   // 通信リトライ回数（通信失敗時）
                                                                 //    =0:    初回通信のみ
                                                                 //    =1～MELSEC_RETRY_MAX: リトライ回数
            public  UInt32              RecvTimeOut;             // 受信タイムアウト時間[msec]
            public  UInt32              SendTimeOut;             // 送信タイムアウト時間[msec]
            public  UInt32              RetryWaitTime;           // 通信リトライ時の再送信待ち時間[msec]
            private UInt32              sumCheck;                // サムチェック有無
                                                                 //    =FALSE: 無し
                                                                 //    =TRUE:  有り

            // 通信ログ書込みコールバック関数
            public MELSEC_CBF_WRITE_LOG CBF_WriteLog;			 // 通信ログ書き込み

            // コンストラクタ
            public MELSEC_PARM(Nullable<MELSEC_PARM> buf)
            {
                if (buf.HasValue == true) {
                    // コピーする
                    CpuType         = buf.Value.CpuType;
                    FrameType       = buf.Value.FrameType;
                    Protocol        = buf.Value.Protocol;
                    Code            = buf.Value.Code;
                    Retry           = buf.Value.Retry;
                    RecvTimeOut     = buf.Value.RecvTimeOut;
                    SendTimeOut     = buf.Value.SendTimeOut;
                    RetryWaitTime   = buf.Value.RetryWaitTime;
                    sumCheck        = buf.Value.sumCheck;
                    CBF_WriteLog    = buf.Value.CBF_WriteLog;
                }
                else {
                    // クリアする
                    CpuType         = 0;
                    FrameType       = 0;
                    Protocol        = 0;
                    Code            = 0;
                    Retry           = 0;
                    RecvTimeOut     = 0;
                    SendTimeOut     = 0;
                    RetryWaitTime   = 0;
                    sumCheck        = 0;
                    CBF_WriteLog    = null;
                }
            }

            // 空のバッファを取得する
            public static MELSEC_PARM Empty
            {
                get
                {
                    return (new MELSEC_PARM(null));
                }
            }

            // サムチェック有無
            public bool SumCheck
            {
                get
                {
                    return (Convert.ToBoolean(sumCheck));
                }

                set
                {
                    sumCheck = (value == true) ? 1u : 0u;
                }
            }
        }


        //【Ethernet通信回線接続パラメータ】
        // プロトコル
        private const UInt16 MELSEC_TCP_IP = 0;                    // TCP/IP
        private const UInt16 MELSEC_UDP_IP = 1;                    // UDP/IP

        /***********************************************************************
            パラメータ構造体
        ***********************************************************************/

        [StructLayout(layoutKind, CharSet = charSet, Pack = packSize)]
        public struct MELSEC_CONNECT_PARM
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public  byte[]	 IpAddr;                     // PLC IPアドレス (TCHAR)
                                                         //    ="192.168.2.101" (設定例)
            public  UInt16    PortNo;                    // PLC ポート番号
            public  UInt16    LocalPortNo;               // 自局ポート番号（UDP/IPの場合、設定）
            public  UInt16    Protocol;                  // 通信プロトコル
                                                         //    =MELSEC_TCP_IP: TCP/IP
                                                         //    =MELSEC_UDP_IP: UDP/IP
            public  UInt32    ConTimeOut;                // 回線接続監視時間[msec]

            // コンストラクタ
            public MELSEC_CONNECT_PARM(Nullable<MELSEC_CONNECT_PARM> buf)
            {
                IpAddr = new byte[16];
                if (buf.HasValue == true) {
                    // コピーする
                    PortNo      = buf.Value.PortNo;
                    LocalPortNo = buf.Value.LocalPortNo;
                    Protocol    = buf.Value.Protocol;
                    ConTimeOut  = buf.Value.ConTimeOut;
                    for (int ii = 0; ii < IpAddr.Length; ii++) { IpAddr[ii] = buf.Value.IpAddr[ii];}
                }
                else {
                    // クリアする
                    PortNo      = 0;
                    LocalPortNo = 0;
                    Protocol    = 0;
                    ConTimeOut  = 0;
                    for (int ii = 0; ii < IpAddr.Length; ii++) { IpAddr[ii] = 0; }
                }
            }

            // 空のバッファを取得する
            public static MELSEC_CONNECT_PARM Empty
            {
                get
                {
                    return (new MELSEC_CONNECT_PARM(null));
                }
            }
        }


        /***********************************************************************
            パラメータ構造体
        ***********************************************************************/

        [StructLayout(layoutKind, CharSet = charSet, Pack = packSize)]
        public struct MELSEC_COM_PARM
        {
            public  byte     BlockNo;                    // ブロック番号
            public  byte     StNo;                       // 局番
            public  byte     PcNo;                       // PC番号
            public  byte     MyNo;                       // 自局番号（2C、3C、4Cフレーム時のみ有効）
            public  byte     NetNo;                      // ネットワーク番号（4C、3E、4Eフレーム時のみ有効）
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public  byte[]   yobi1;                      // 予備
            public  UInt16   UnitIoNo;                   // 要求先ユニットI/O番号（3E、4Eフレーム時のみ有効）
            public  byte     UnitStNo;                   // 要求先ユニット局番号（3E、4Eフレーム時のみ有効）
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public  byte[]   yobi2;                      // 予備
            public  UInt16   WaitTime;                   // 伝文ウェイト時間（CPU監視タイマ）[msec]
                                                         //    =0, 10～150（10msec刻み）

            // コンストラクタ
            public MELSEC_COM_PARM(Nullable<MELSEC_COM_PARM> buf)
            {
                yobi1 = new byte[3];
                yobi2 = new byte[3];
                if (buf.HasValue == true) {
                    // コピーする
                    BlockNo     = buf.Value.BlockNo;
                    StNo        = buf.Value.StNo;
                    PcNo        = buf.Value.PcNo;
                    MyNo        = buf.Value.MyNo;
                    NetNo       = buf.Value.NetNo;
                    UnitIoNo    = buf.Value.UnitIoNo;
                    UnitStNo    = buf.Value.UnitStNo;
                    WaitTime    = buf.Value.WaitTime;
                    for (int ii = 0; ii < yobi1.Length; ii++) { yobi1[ii] = buf.Value.yobi1[ii]; }
                    for (int ii = 0; ii < yobi2.Length; ii++) { yobi2[ii] = buf.Value.yobi2[ii]; }
                }
                else {
                    // クリアする
                    BlockNo     = 0;
                    StNo        = 0;
                    PcNo        = 0;
                    MyNo        = 0;
                    NetNo       = 0;
                    UnitIoNo    = 0;
                    UnitStNo    = 0;
                    WaitTime    = 0;
                    for (int ii = 0; ii < yobi1.Length; ii++) { yobi1[ii] = 0; }
                    for (int ii = 0; ii < yobi2.Length; ii++) { yobi2[ii] = 0; }
                }
            }

            // 空のバッファを取得する
            public static MELSEC_COM_PARM Empty
            {
                get
                {
                    return (new MELSEC_COM_PARM(null));
                }
            }
        }

        
        /***********************************************************************
            PLCデバイス番号
        ***********************************************************************/

        [StructLayout(layoutKind, CharSet = charSet, Pack = packSize)]
        public struct MELSEC_DEV
        {
            private UInt32    No;                        // デバイス番号
            private UInt16    Code;                      // デバイス種類

            // コンストラクタ
            public MELSEC_DEV(Nullable<MELSEC_DEV> buf)
            {
                if (buf.HasValue == true) {
                    // コピーする
                    No   = buf.Value.No;
                    Code = buf.Value.Code;
                }
                else {
                    // クリアする
                    No   = 0;
                    Code = 0;
                }
            }

            public MELSEC_DEV(UInt32 no, UInt16 code)
            {
                No   = no;
                Code = code;
            }

            // 空のバッファを取得する
            public static MELSEC_DEV Empty
            {
                get
                {
                    return (new MELSEC_DEV(null));
                }
            }
        }

        // デバイス種類
        public const UInt16 MELSEC_DEV_M       = 0x90;         // 内部リレー (A, QnA 特殊リレー含む）
        public const UInt16 MELSEC_DEV_SM      = 0x91;         // 特殊リレー
        public const UInt16 MELSEC_DEV_D       = 0xA8;         // データレジスタ (A, QnA 特殊レジスタ含む)
        public const UInt16 MELSEC_DEV_SD      = 0xA9;         // 特殊レジスタ
        public const UInt16 MELSEC_DEV_X       = 0x9C;         // 入力リレー
        public const UInt16 MELSEC_DEV_Y       = 0x9D;         // 出力リレー
        public const UInt16 MELSEC_DEV_L       = 0x92;         // ラッチリレー
        public const UInt16 MELSEC_DEV_F       = 0x93;         // アナンシェータ
        public const UInt16 MELSEC_DEV_V       = 0x94;         // エッジリレー
        public const UInt16 MELSEC_DEV_B       = 0xA0;         // リンクリレー
        public const UInt16 MELSEC_DEV_W       = 0xB4;         // リンクレジスタ
        public const UInt16 MELSEC_DEV_TS      = 0xC1;         // タイマ接点
        public const UInt16 MELSEC_DEV_TC      = 0xC0;         // タイマコイル
        public const UInt16 MELSEC_DEV_TN      = 0xC2;         // タイマ現在値
        public const UInt16 MELSEC_DEV_SS      = 0xC7;         // 積算タイマ接点
        public const UInt16 MELSEC_DEV_SC      = 0xC6;         // 積算タイマコイル
        public const UInt16 MELSEC_DEV_SN      = 0xC8;         // 積算タイマ現在値
        public const UInt16 MELSEC_DEV_CS      = 0xC4;         // カウンタ接点
        public const UInt16 MELSEC_DEV_CC      = 0xC3;         // カウンタコイル
        public const UInt16 MELSEC_DEV_CN      = 0xC5;         // カウンタ現在値
        public const UInt16 MELSEC_DEV_SB      = 0xA1;         // リンク特殊リレー
        public const UInt16 MELSEC_DEV_SW      = 0xB5;         // リンク特殊レジスタ
        public const UInt16 MELSEC_DEV_S       = 0x98;         // ステップリレー
        public const UInt16 MELSEC_DEV_DX      = 0xA2;         // ダイレクト入力
        public const UInt16 MELSEC_DEV_DY      = 0xA3;         // ダイレクト出力
        public const UInt16 MELSEC_DEV_Z       = 0xCC;         // インデックスレジスタ
        public const UInt16 MELSEC_DEV_R       = 0xAF;         // ファイルレジスタ
        public const UInt16 MELSEC_DEV_ZR      = 0xB0;		   // ファイルレジスタ

        private static string DevStr(UInt32 dev_no, UInt16 dev_code)
        {
            switch (dev_code)
            {
                case MELSEC_DEV_M : return $"M{dev_no}";
                case MELSEC_DEV_SM: return $"SM{dev_no}";
                case MELSEC_DEV_D : return $"D{dev_no}";
                case MELSEC_DEV_SD: return $"SD{dev_no}";
                case MELSEC_DEV_X : return $"X{dev_no}";
                case MELSEC_DEV_Y : return $"Y{dev_no}";
                case MELSEC_DEV_L : return $"L{dev_no}";
                case MELSEC_DEV_F : return $"F{dev_no}";
                case MELSEC_DEV_V : return $"V{dev_no}";
                case MELSEC_DEV_B : return $"B{dev_no}";
                case MELSEC_DEV_W : return $"W{dev_no}";
                case MELSEC_DEV_TS: return $"TS{dev_no}";
                case MELSEC_DEV_TC: return $"TC{dev_no}";
                case MELSEC_DEV_TN: return $"TN{dev_no}";
                case MELSEC_DEV_SS: return $"SS{dev_no}";
                case MELSEC_DEV_SC: return $"SC{dev_no}";
                case MELSEC_DEV_SN: return $"SN{dev_no}";
                case MELSEC_DEV_CS: return $"CS{dev_no}";
                case MELSEC_DEV_CC: return $"CC{dev_no}";
                case MELSEC_DEV_CN: return $"CN{dev_no}";
                case MELSEC_DEV_SB: return $"SB{dev_no}";
                case MELSEC_DEV_SW: return $"SW{dev_no}";
                case MELSEC_DEV_S : return $"S{dev_no}";
                case MELSEC_DEV_DX: return $"DX{dev_no}";
                case MELSEC_DEV_DY: return $"DY{dev_no}";
                case MELSEC_DEV_Z : return $"Z{dev_no}";
                case MELSEC_DEV_R : return $"R{dev_no}";
                case MELSEC_DEV_ZR: return $"ZR{dev_no}";
                default:            return string.Empty;
            }
        }

        /***********************************************************************
            Winsockのバージョンを指定するための構造体
        ***********************************************************************/

        [StructLayout(layoutKind)]
        public struct WSAData
        {
            public UInt16    wVersion;
            public UInt16    wHighVersion;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 257)]
            public string    szDescription;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
            public string    szSystemStatus;
            public ushort    iMaxSockets;
            public ushort    iMaxUdpDg;
            public IntPtr    lpVendorInfo;

            // コンストラクタ
            public WSAData(Nullable<WSAData> buf)
            {
                if (buf.HasValue == true) {
                    // コピーする
                    wVersion        = buf.Value.wVersion;
                    wHighVersion    = buf.Value.wHighVersion;
                    szDescription   = buf.Value.szDescription;
                    szSystemStatus  = buf.Value.szSystemStatus;
                    iMaxSockets     = buf.Value.iMaxSockets;
                    iMaxUdpDg       = buf.Value.iMaxUdpDg;
                    lpVendorInfo    = buf.Value.lpVendorInfo;
                }
                else {
                    // クリアする
                    wVersion        = 0;
                    wHighVersion    = 0;
                    szDescription   = string.Empty;
                    szSystemStatus  = string.Empty;
                    iMaxSockets     = 0;
                    iMaxUdpDg       = 0;
                    lpVendorInfo    = IntPtr.Zero;
                }
            }

            // 空のバッファを取得する
            public static WSAData Empty
            {
                get
                {
                    return (new WSAData(null));
                }
            }
        }

        private static UInt16 MAKEWORD(byte low, byte high)
        {
            return (UInt16)((high << 8) | low);
        }

        // WSAStartup関数のインポート
        [DllImport("ws2_32.dll")] private static extern int WSAStartup(ushort wVersionRequired, out WSAData lpWSAData);

        // WSACleanup関数のインポート
        [DllImport("ws2_32.dll")] private static extern int WSACleanup();
    }


    public static partial class Mx_sub
    {
        //============================================================
        //	DllImport
        //============================================================
        [DllImport(dllName, CallingConvention = callingConvention, CharSet = charSet, EntryPoint = "MELSEC_GetVer"         )] private static extern UInt32  DllGetVer(IntPtr ver);
		[DllImport(dllName, CallingConvention = callingConvention, CharSet = charSet, EntryPoint = "MELSEC_Initialize"     )] private static extern UInt32  DllInitialize(ref MELSEC_PARM Param, IntPtr vptr);
		[DllImport(dllName, CallingConvention = callingConvention, CharSet = charSet, EntryPoint = "MELSEC_Release"        )] private static extern UInt32  DllRelease(UInt32 inst_no);
		[DllImport(dllName, CallingConvention = callingConvention, CharSet = charSet, EntryPoint = "MELSEC_ReleaseAll"     )] private static extern UInt32  DllReleaseAll();


        [DllImport(dllName, CallingConvention = callingConvention, CharSet = charSet, EntryPoint = "MELSEC_Connect"        )] private static extern UInt32   DllConnect(UInt32 inst_no, ref MELSEC_CONNECT_PARM Param, bool log_enable = false);
        [DllImport(dllName, CallingConvention = callingConvention, CharSet = charSet, EntryPoint = "MELSEC_Close"          )] private static extern UInt32   DllClose(UInt32 inst_no, bool log_enable = false);
        [DllImport(dllName, CallingConvention = callingConvention, CharSet = charSet, EntryPoint = "MELSEC_IsOpen"         )] private static extern bool     DllIsOpen(UInt32 inst_no);

        [DllImport(dllName, CallingConvention = callingConvention, CharSet = charSet, EntryPoint = "MELSEC_DevCs"          )] private static extern void     DllDevCs(UInt16 dev_code, UInt32 dev_no, ref MELSEC_DEV dev);

        [DllImport(dllName, CallingConvention = callingConvention, CharSet = charSet, EntryPoint = "MELSEC_GetComParm"     )] private static extern UInt32   DllGetComParm(UInt32 inst_no, ref MELSEC_COM_PARM parm);
        [DllImport(dllName, CallingConvention = callingConvention, CharSet = charSet, EntryPoint = "MELSEC_ReadBits"       )] private static extern UInt32   DllReadBits(UInt32 inst_no, ref MELSEC_COM_PARM parm, MELSEC_DEV melsec_dev, UInt16 cnt, IntPtr data, bool log_enable = false);
        [DllImport(dllName, CallingConvention = callingConvention, CharSet = charSet, EntryPoint = "MELSEC_ReadWords"      )] private static extern UInt32   DllReadWords(UInt32 inst_no, ref MELSEC_COM_PARM parm, MELSEC_DEV melsec_dev, UInt16 cnt, IntPtr data, bool log_enable = false);
        [DllImport(dllName, CallingConvention = callingConvention, CharSet = charSet, EntryPoint = "MELSEC_WriteBits"      )] private static extern UInt32   DllWriteBits(UInt32 inst_no, ref MELSEC_COM_PARM parm, MELSEC_DEV melsec_dev, UInt16 cnt, IntPtr data, bool log_enable = false);
        [DllImport(dllName, CallingConvention = callingConvention, CharSet = charSet, EntryPoint = "MELSEC_WriteWords"     )] private static extern UInt32   DllWriteWords(UInt32 inst_no, ref MELSEC_COM_PARM parm, MELSEC_DEV melsec_dev, UInt16 cnt, IntPtr data, bool log_enable = false);
    }
}
