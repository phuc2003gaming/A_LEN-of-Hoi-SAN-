/*******************************************************************************

	MELSEC COMUNICATION PROTOCOL (MELSEC) 通信ＡＰＩ

	Create Date : 2021/08/25 NCS
	Update Date :

*******************************************************************************/
#pragma once

#ifdef MELSEC_API_EXPORTS
#define DECLSPEC __declspec(dllexport)
#else
#define DECLSPEC __declspec(dllimport)
#endif


#ifdef __cplusplus
extern "C" {
#endif


// Update '21/09/30 NCS [Ver1.1.0( 1)] ----------------------------------------->
typedef LONG	MELSEC_INSTANCE;			// インスタンス番号型定義
//<------------------------------------------------------------------------------

/*******************************************************************************
	コールバック関数型定義
*******************************************************************************/
// 通信ログ書き込み
// Update '21/09/30 NCS [Ver1.1.0( 1)] ----------------------------------------->
//typedef VOID (*MELSEC_CBF_WRITE_LOG) (
//	LPCTSTR		header_text,	// ログヘッダーテキスト
//	LONG		blng,			// 通信データバイト数
//	BYTE*		data			// 通信データ
//);
// Update '21/12/23 NCS [Ver1.1.3( 1)] ----------------------------------------->
//typedef VOID (*MELSEC_CBF_WRITE_LOG) (
//	MELSEC_INSTANCE		inst_no,		// APIインスタンス番号
//	CHAR*				header_text,	// ログヘッダーテキスト
//	LONG				blng,			// 通信データバイト数
//	BYTE*				data,			// 通信データ
//	VOID*				vptr			// 汎用ポインタ値
//);
typedef VOID (CALLBACK *MELSEC_CBF_WRITE_LOG) (
	MELSEC_INSTANCE		inst_no,		// APIインスタンス番号
	CHAR*				header_text,	// ログヘッダーテキスト
	LONG				blng,			// 通信データバイト数
	BYTE*				data,			// 通信データ
	VOID*				vptr			// 汎用ポインタ値
);
//<------------------------------------------------------------------------------


/*******************************************************************************

	構造体定義

*******************************************************************************/
// インスタンス番号不正値
#define MELSEC_INSTANCE_ILLEGAL_1		(-1)	// インスタンス番号獲得失敗（パラメータ不正：CPU種類）
#define MELSEC_INSTANCE_ILLEGAL_2		(-2)	// インスタンス番号獲得失敗（パラメータ不正：伝送フレーム）
#define MELSEC_INSTANCE_ILLEGAL_3		(-3)	// インスタンス番号獲得失敗（パラメータ不正：通信形式）
#define MELSEC_INSTANCE_ILLEGAL_4		(-4)	// インスタンス番号獲得失敗（パラメータ不正：伝送コード）
#define MELSEC_INSTANCE_ILLEGAL_5		(-5)	// インスタンス番号獲得失敗（パラメータ不正：通信リトライ回数）
#define MELSEC_INSTANCE_ILLEGAL_6		(-6)	// インスタンス番号獲得失敗（パラメータ不正：サムチェック）
#define MELSEC_INSTANCE_ILLEGAL_7		(-7)	// インスタンス番号獲得失敗（インスタンス空き無し）


#pragma pack (push, 1)	// 構造体メンバを1バイトのアライメントで整列開始

//【初期化パラメータ】
// PLC CPU種類
enum {
	MELSEC_CPU_A = 0,					// ACPU
	MELSEC_CPU_AnA,						// AnACPU
	MELSEC_CPU_QnA,						// QnACPU
	MELSEC_CPU_Qn,						// QnCPU
	MELSEC_CPU_iQR,						// iQRCPU
	MELSEC_CPU_CNT
};
// 伝送フレーム
enum {
	MELSEC_FRAME_1C = 0,				// A互換1Cフレーム
	MELSEC_FRAME_2C,					// QnA互換2Cフレーム
	MELSEC_FRAME_3C,					// QnA互換3Cフレーム
	MELSEC_FRAME_4C,					// QnA互換4Cフレーム
	MELSEC_FRAME_1E,					// A互換1Eフレーム
	MELSEC_FRAME_3E,					// QnA互換3Eフレーム
	MELSEC_FRAME_4E,					// 4Eフレーム
	MELSEC_FRAME_CNT
};
// 通信形式
enum {
	MELSEC_PRTCL0 = 0,					// 形式無し
	MELSEC_PRTCL1,						// 形式1
	MELSEC_PRTCL2,						// 形式2
	MELSEC_PRTCL3,						// 形式3
	MELSEC_PRTCL4,						// 形式4
	MELSEC_PRTCL5,						// 形式5 (QnA互換4Cフレーム時のみ有効)
	MELSEC_PRTCL_CNT					// 
};
// 伝送コード形式
enum {
	MELSEC_CODE_ASCII = 0,				// 伝文コード形式（ASCII）
	MELSEC_CODE_BINARY,					// 伝文コード形式（BINARY）
	MELSEC_CODE_CNT
};

// 通信リトライ最大数
#define MELSEC_RETRY_MAX		3			// リトライ最大数
// 通信タイムアウト
#define MELSEC_TIMEOUUT_FOREVER	0xFFFFFFFF	// 永久待ち

// パラメータ構造体
typedef struct {
	WORD	CpuType;				// PLC CPU種類
									//    =MELSEC_CPU_A:   ACPU
									//    =MELSEC_CPU_AnA: AnACPU
									//    =MELSEC_CPU_QnA: QnACPU
									//    =MELSEC_CPU_Qn:  QnCPU
									//    =MELSEC_CPU_iQR: iQRCPU
	WORD	FrameType;				// 伝送フレーム
									//    =MELSEC_FRAME_1C: A互換1Cフレーム
									//    =MELSEC_FRAME_2C: QnA互換2Cフレーム
									//    =MELSEC_FRAME_3C: QnA互換3Cフレーム
									//    =MELSEC_FRAME_4C: QnA互換4Cフレーム
									//    =MELSEC_FRAME_1E: A互換1Eフレーム
									//    =MELSEC_FRAME_3E: QnA互換3Eフレーム
									//    =MELSEC_FRAME_4E: 4Eフレーム
	WORD	Protocol;				// 通信形式
									//    =MELSEC_PRTCL0: 形式無し
									//    =MELSEC_PRTCL1: 形式1
									//    =MELSEC_PRTCL2: 形式2
									//    =MELSEC_PRTCL3: 形式3
									//    =MELSEC_PRTCL4: 形式4
									//    =MELSEC_PRTCL5: 形式5 (QnA互換4Cフレーム時のみ有効)
	WORD	Code;					// 伝送コード形式
									//    =MELSEC_CODE_ASCII:  ASCIIコード
									//    =MELSEC_CODE_BINARY: BINARYコード
	WORD	Retry;					// 通信リトライ回数（通信失敗時）
									//    =0:    初回通信のみ
									//    =1～MELSEC_RETRY_MAX: リトライ回数
	DWORD	RecvTimeOut;			// 受信タイムアウト時間[msec]
	DWORD	SendTimeOut;			// 送信タイムアウト時間[msec]
	DWORD	RetryWaitTime;			// 通信リトライ時の再送信待ち時間[msec]
	BOOL	SumCheck;				// サムチェック有無
									//    =FALSE: 無し
									//    =TRUE:  有り

	// 通信ログ書込みコールバック関数
	MELSEC_CBF_WRITE_LOG	CBF_WriteLog;	// 通信ログ書き込み
// Update '21/09/30 NCS [Ver1.1.0( 2)] ----------------------------------------->
//	LONG	ErrBase;						// エラーベース値
//<------------------------------------------------------------------------------
} MELSEC_PARM;


//【シリアル回線オープンパラメータ】
// WinBase.hに定義されている

// 通信ボーレート
// #define CBR_300			300
// #define CBR_600			600
// #define CBR_1200			1200
// #define CBR_2400			2400
// #define CBR_4800			4800
// #define CBR_9600			9600
// #define CBR_19200		19200

// パリティ
// #define NOPARITY			0
// #define ODDPARITY		1
// #define EVENPARITY		2

// ストップビット
// #define ONESTOPBIT		0
// #define TWOSTOPBITS		2

// RS制御
// #define RTS_CONTROL_DISABLE		0x00
// #define RTS_CONTROL_ENABLE		0x01
// #define RTS_CONTROL_HANDSHAKE	0x02
// #define RTS_CONTROL_TOGGLE		0x03

// パラメータ構造体
typedef struct {
// Update '25/06/18 NCS [Ver1.5.0( 2)] ----------------------------------------->
//	TCHAR	ComPort[16];			// COMポート番号
	CHAR	ComPort[16];			// COMポート番号
//<------------------------------------------------------------------------------
									//    ="COM1"～"COMn"
	DWORD	BaudRate;				// 通信ボーレート
									//    =CBR_300:   300[bps]
									//    =CBR_600:   600[bps]
									//    =CBR_1200:  1200[bps]
									//    =CBR_4800:  4800[bps]
									//    =CBR_9600:  9600[bps]
									//    =CBR_19200: 19200[bps]
	BYTE	ByteSize;				// バイトサイズ
									//    =7: 7ビット
									//    =8: 8ビット
	BYTE	Parity;					// パリティ
									//    =NOPARITY:   パリティなし
									//    =ODDPARITY:  奇数パリティ
									//    =EVENPARITY: 偶数パリティ
	BYTE	StopBits;				// ストップビット長
									//    =ONESTOPBIT:  1ストップビット
									//    =TWOSTOPBITS: 2ストップビット
	BYTE	yobi;					// 
	DWORD	RtsControl;				// RTS制御
									//    =RTS_CONTROL_*
	DWORD	ByteTimeOut;			// 受信バイト間タイムアウト時間[msec]
} MELSEC_OPEN_PARM;


//【Ethernet通信回線接続パラメータ】
// プロトコル
enum {
	MELSEC_TCP_IP = 0,				// TCP/IP
	MELSEC_UDP_IP					// UDP/IP
};
// パラメータ構造体
typedef struct {
// Update '25/06/18 NCS [Ver1.5.0( 2)] ----------------------------------------->
//	TCHAR	IpAddr[16];				// PLC IPアドレス
	CHAR	IpAddr[16];				// PLC IPアドレス
//<------------------------------------------------------------------------------
									//    ="192.168.2.101" (設定例)
	WORD	PortNo;					// PLC ポート番号
	WORD	LocalPortNo;			// 自局ポート番号（UDP/IPの場合、設定）
	WORD	Protocol;				// 通信プロトコル
									//    =MELSEC_TCP_IP: TCP/IP
									//    =MELSEC_UDP_IP: UDP/IP
	DWORD	ConTimeOut;				// 回線接続監視時間[msec]
} MELSEC_CONNECT_PARM;


//【通信パラメータ】（デフォルト値）
// ブロック番号
#define MELSEC_BLOCK_NO	0x00		// 0x00～0xFF
// 局番号
#define MELSEC_ST_NO		0x00	// 通信ユニット設定局番
									// 0x00: 1対1で通信する場合
									// 0x01～0x1F: マルチドロップで通信する場合の相手PLC局番号
// PC番号
#define MELSEC_PC_NO		0xFF	// 通信ユニット搭載PLC自局番号
									// 0x00～0x40: MELSECNET上のPLC PC番号
// 自局番号
#define MELSEC_MY_NO		0x00	// 既定値
// ネットワーク番号（4Eフレーム時のみ有効）
#define MELSEC_NET_NO		0x00		// 通信ユニット搭載PLC自身のネットワーク番号
									// 0x01～0xEF: アクセス局のネットワーク番号
// 要求先ユニットI/O番号
#define MELSEC_UNIT_IO_NO	0x03FF	// 通信ユニット搭載PLC自身のユニットI/O番号
// 要求先ユニット局番号
#define MELSEC_UNIT_ST_NO	0x00	// 通信ユニット搭載PLC自身のユニット局番号
// パラメータ構造体
typedef struct {
	BYTE	BlockNo;				// ブロック番号
	BYTE	StNo;					// 局番
	BYTE	PcNo;					// PC番号
	BYTE	MyNo;					// 自局番号（2C、3C、4Cフレーム時のみ有効）
	BYTE	NetNo;					// ネットワーク番号（4C、3E、4Eフレーム時のみ有効）
	BYTE	yobi1[3];				// 
	WORD	UnitIoNo;				// 要求先ユニットI/O番号（3E、4Eフレーム時のみ有効）
	BYTE	UnitStNo;				// 要求先ユニット局番号（3E、4Eフレーム時のみ有効）
	BYTE	yobi2[3];				// 
	WORD	WaitTime;				// 伝文ウェイト時間（CPU監視タイマ）[msec]
									//    =0, 10～150（10msec刻み）
} MELSEC_COM_PARM;

// CPU形名読み込み用構造体
// CPU形名コード
#define MELSEC_CPU_TYPE_A0J2HCPU	0x98		// A0J2HCPU
#define MELSEC_CPU_TYPE_A1CPU		0xA1		// A1CPU
#define MELSEC_CPU_TYPE_A1SCPU		0x98		// A1SCPU
#define MELSEC_CPU_TYPE_A2CPU		0xA2		// A2CPU
#define MELSEC_CPU_TYPE_A2ACPU		0x92		// A2ACPU
#define MELSEC_CPU_TYPE_A2ACPU_S1	0x93		// A2ACPU-S1
#define MELSEC_CPU_TYPE_A2CCPU		0x9A		// A2CCPU
#define MELSEC_CPU_TYPE_A3CPU		0xA3		// A3CPU
#define MELSEC_CPU_TYPE_A3ACPU		0x94		// A3ACPU
#define MELSEC_CPU_TYPE_A3HCPU		0xA4		// A3HCPU
#define MELSEC_CPU_TYPE_A73CPU		0xA3		// A73CPU
#define MELSEC_CPU_TYPE_AJ72P25		0xAB		// AJ72P25
#define MELSEC_CPU_TYPE_A7LMS_F		0xA3		// A7LMS-F

typedef struct {
	WORD	Code;					// 形名コード
// Update '25/06/18 NCS [Ver1.5.0( 2)] ----------------------------------------->
//	TCHAR	Name[32];				// 形名
	CHAR	Name[32];				// 形名
//<------------------------------------------------------------------------------
} MELSEC_CPU_TYPE;


// PLCデバイス番号
typedef struct {
	DWORD	No;					// デバイス番号
	WORD	Code;				// デバイス種類
} MELSEC_DEV;

// デバイス種類
#define MELSEC_DEV_M		0x90		// 内部リレー (A, QnA 特殊リレー含む）
#define MELSEC_DEV_SM		0x91		// 特殊リレー
#define MELSEC_DEV_D		0xA8		// データレジスタ (A, QnA 特殊レジスタ含む)
#define MELSEC_DEV_SD		0xA9		// 特殊レジスタ
#define MELSEC_DEV_X		0x9C		// 入力リレー
#define MELSEC_DEV_Y		0x9D		// 出力リレー
#define MELSEC_DEV_L		0x92		// ラッチリレー
#define MELSEC_DEV_F		0x93		// アナンシェータ
#define MELSEC_DEV_V		0x94		// エッジリレー
#define MELSEC_DEV_B		0xA0		// リンクリレー
#define MELSEC_DEV_W		0xB4		// リンクレジスタ
#define MELSEC_DEV_TS		0xC1		// タイマ接点
#define MELSEC_DEV_TC		0xC0		// タイマコイル
#define MELSEC_DEV_TN		0xC2		// タイマ現在値
#define MELSEC_DEV_SS		0xC7		// 積算タイマ接点
#define MELSEC_DEV_SC		0xC6		// 積算タイマコイル
#define MELSEC_DEV_SN		0xC8		// 積算タイマ現在値
#define MELSEC_DEV_CS		0xC4		// カウンタ接点
#define MELSEC_DEV_CC		0xC3		// カウンタコイル
#define MELSEC_DEV_CN		0xC5		// カウンタ現在値
#define MELSEC_DEV_SB		0xA1		// リンク特殊リレー
#define MELSEC_DEV_SW		0xB5		// リンク特殊レジスタ
#define MELSEC_DEV_S		0x98		// ステップリレー
#define MELSEC_DEV_DX		0xA2		// ダイレクト入力
#define MELSEC_DEV_DY		0xA3		// ダイレクト出力
#define MELSEC_DEV_Z		0xCC		// インデックスレジスタ
#define MELSEC_DEV_R		0xAF		// ファイルレジスタ
#define MELSEC_DEV_ZR		0xB0		// ファイルレジスタ

// デバイス管理テーブル
// デバイス番号形態
enum {
	MELSEC_DEV_NUM_DEC = 0,			// デバイス番号10進数
	MELSEC_DEV_NUM_HEX				// デバイス番号16進数
};
// デバイス種類
enum {
	MELSEC_DEV_BIT = 0,				// ビットデバイス
	MELSEC_DEV_WORD					// ワードデバイス
};
// 構造体
typedef struct {
	WORD	Code;					// デバイスコード
									//    =MELSEC_DEV_xx
// Update '21/12/27 NCS [Ver1.2.0( 2)] ----------------------------------------->
//	TCHAR*	DevStr;					// デバイスコード（文字列）
	CHAR	DevStr[4];				// デバイスコード（文字列）
//<------------------------------------------------------------------------------
									//    例）"M"
	WORD	NumType;				// デバイス番号数値種類
									//    =MELSEC_DEV_NUM_DEC: 10進数
									//    =MELSEC_DEV_NUM_HEX: 16進数
	WORD	DevType;				// デバイス種類
									//    =MELSEC_DEV_BIT:  ビットデバイス
									//    =MELSEC_DEV_WORD: ワードデバイス
} MELSEC_DEV_INFO;


// リモートCPUモード
enum {
	MELSEC_REMOTE_STOP = 0,			// STOP
	MELSEC_REMOTE_RUN,				// RUN
	MELSEC_REMOTE_PAUSE,			// PAUSE
	MELSEC_REMOTE_CLEAR_RUN,		// RUN（データメモリクリア＋RUN）
	MELSEC_REMOTE_LATCH_CLEAR,		// ラッチクリア
	MELSEC_REMOTE_RESET,			// RESET
	MELSEC_REMOTE_CNT
};

// Update '22/01/20 NCS [Ver1.3.0( 1)] ----------------------------------------->
// ラベルデバイス単位指定
enum {
	MELSEC_LABEL_TYPE_BITS = 0,			// ビットラベル
	MELSEC_LABEL_TYPE_WORDS,			// ワードラベル
	MELSEC_LABEL_TYPE_CNT
};
//<------------------------------------------------------------------------------


#pragma pack(pop)		// 構造体メンバを1バイトのアライメントで整列終了


/*******************************************************************************

	ＡＰＩ関数定義

*******************************************************************************/
// 戻り値型定義
// Update '21/09/30 NCS [Ver1.1.0( 1)] ----------------------------------------->
//typedef LONG	MELSEC_INSTANCE;			// インスタンス番号型定義
//<------------------------------------------------------------------------------
typedef LONG	MELSEC_ERR;					// エラー番号

// Update '25/06/18 NCS [Ver1.5.0( 2)] ----------------------------------------->
//extern LONG DECLSPEC WINAPI MELSEC_GetVer(TCHAR* ver);
extern LONG DECLSPEC WINAPI MELSEC_GetVer(CHAR* ver);
//<------------------------------------------------------------------------------
// Update '21/09/30 NCS [Ver1.1.0( 1)] ----------------------------------------->
//extern MELSEC_INSTANCE DECLSPEC WINAPI MELSEC_Initialize(MELSEC_PARM* parm);
extern MELSEC_INSTANCE DECLSPEC WINAPI MELSEC_Initialize(MELSEC_PARM* parm, VOID* vptr);
//<------------------------------------------------------------------------------
extern MELSEC_ERR	DECLSPEC WINAPI MELSEC_Release(LONG inst_no);
extern MELSEC_ERR	DECLSPEC WINAPI MELSEC_ReleaseAll(void);

extern MELSEC_ERR	DECLSPEC WINAPI MELSEC_Open(MELSEC_INSTANCE inst_no, MELSEC_OPEN_PARM* parm, BOOL log_enable);
extern MELSEC_ERR	DECLSPEC WINAPI MELSEC_Connect(MELSEC_INSTANCE inst_no, MELSEC_CONNECT_PARM* parm, BOOL log_enable);
extern MELSEC_ERR	DECLSPEC WINAPI MELSEC_Close(MELSEC_INSTANCE inst_no, BOOL log_enable);
extern BOOL		DECLSPEC WINAPI MELSEC_IsOpen(MELSEC_INSTANCE inst_no);

extern MELSEC_DEV	DECLSPEC WINAPI MELSEC_DevStr(LPCTSTR dev_str);
// Update '21/12/27 NCS [Ver1.2.0( 2)] ----------------------------------------->
extern BOOL			DECLSPEC WINAPI MELSEC_DevStrCs(LPCTSTR dev_str, MELSEC_DEV* dev);
//<------------------------------------------------------------------------------
// Update '25/06/18 NCS [Ver1.5.0( 1)] ----------------------------------------->
//extern MELSEC_DEV	DECLSPEC WINAPI MELSEC_Dev(WORD dev_code, WORD dev_no);
extern MELSEC_DEV	DECLSPEC WINAPI MELSEC_Dev(WORD dev_code, DWORD dev_no);
//<------------------------------------------------------------------------------
// Update '21/12/27 NCS [Ver1.2.0( 3)] ----------------------------------------->
// Update '25/06/18 NCS [Ver1.5.0( 1)] ----------------------------------------->
//extern VOID			DECLSPEC WINAPI MELSEC_DevCs(WORD dev_code, WORD dev_no, MELSEC_DEV* dev);
extern VOID			DECLSPEC WINAPI MELSEC_DevCs(WORD dev_code, DWORD dev_no, MELSEC_DEV* dev);
//<------------------------------------------------------------------------------
extern MELSEC_ERR	DECLSPEC WINAPI MELSEC_GetDevInfo(MELSEC_DEV mcp_dev, MELSEC_DEV_INFO* dev_info);

extern MELSEC_ERR	DECLSPEC WINAPI MELSEC_GetComParm(MELSEC_INSTANCE inst_no, MELSEC_COM_PARM* parm);
extern MELSEC_ERR	DECLSPEC WINAPI MELSEC_ReadBits(MELSEC_INSTANCE inst_no, MELSEC_COM_PARM* parm, MELSEC_DEV mcp_dev, WORD cnt, WORD* data, BOOL log_enable);
extern MELSEC_ERR	DECLSPEC WINAPI MELSEC_ReadWords(MELSEC_INSTANCE inst_no, MELSEC_COM_PARM* parm, MELSEC_DEV mcp_dev, WORD cnt, WORD* data, BOOL log_enable);
extern MELSEC_ERR	DECLSPEC WINAPI MELSEC_WriteBits(MELSEC_INSTANCE inst_no, MELSEC_COM_PARM* parm, MELSEC_DEV mcp_dev, WORD cnt, WORD* data, BOOL log_enable);
extern MELSEC_ERR	DECLSPEC WINAPI MELSEC_WriteWords(MELSEC_INSTANCE inst_no, MELSEC_COM_PARM* parm, MELSEC_DEV mcp_dev, WORD cnt, WORD* data, BOOL log_enable);

extern MELSEC_ERR	DECLSPEC WINAPI MELSEC_RemoteCpu(MELSEC_INSTANCE inst_no, MELSEC_COM_PARM* parm, WORD mode, BOOL log_enable);
extern MELSEC_ERR	DECLSPEC WINAPI MELSEC_GetCpuType(MELSEC_INSTANCE inst_no, MELSEC_COM_PARM* parm, MELSEC_CPU_TYPE* cpu_type, BOOL log_enable);
extern MELSEC_ERR	DECLSPEC WINAPI MELSEC_LoopbackTest(MELSEC_INSTANCE inst_no, MELSEC_COM_PARM* parm, WORD cnt, LPCTSTR data, BOOL log_enable);

// Update '22/01/20 NCS [Ver1.3.0( 1)] ----------------------------------------->
extern MELSEC_ERR	DECLSPEC WINAPI MELSEC_ReadArrayLabel(MELSEC_INSTANCE inst_no, MELSEC_COM_PARM* parm, LPCTSTR label, WORD label_type, WORD cnt, WORD* data, BOOL log_enable);
extern MELSEC_ERR	DECLSPEC WINAPI MELSEC_WriteArrayLabel(MELSEC_INSTANCE inst_no, MELSEC_COM_PARM* parm, LPCTSTR label, WORD label_type, WORD cnt, WORD* data, BOOL log_enable);
//<------------------------------------------------------------------------------


/*******************************************************************************
	エラー番号（MELSEC_GetLastErrorで獲得するエラー番号）
*******************************************************************************/
// MELSEC_ERRの内容
#define ERR_MELSEC_0001	(    1)		// 初期化パラメータ内容不正
#define ERR_MELSEC_0002	(    2)		// 初期化失敗（空きのインスタンス不足）
#define ERR_MELSEC_0003	(    3)		// API処理失敗（インスタンス未初期化）
#define ERR_MELSEC_0004	(    4)		// API解放失敗（インスタンス番号不正）
#define ERR_MELSEC_0005	(    5)		// API解放失敗（インスタンス未初期化）

#define ERR_MELSEC_0010	(   10)		// シリアル回線オープン異常（初期化伝送フレーム設定異常）
#define ERR_MELSEC_0011	(   11)		// シリアル回線オープン異常（既に回線オープン中）
#define ERR_MELSEC_0012	(   12)		// シリアル回線オープン異常（回線オープンに失敗）
#define ERR_MELSEC_0015	(   15)		// シリアル回線パージ失敗

#define ERR_MELSEC_0021	(   21)		// シリアル回線データ送信異常（回線未オープン状態）
#define ERR_MELSEC_0022	(   22)		// シリアル回線データ送信異常（0バイト送信完了）
#define ERR_MELSEC_0023	(   23)		// シリアル回線データ送信異常（送信ペンディングに失敗）
#define ERR_MELSEC_0024	(   24)		// シリアル回線データ送信異常（送信タイムアウト）

#define ERR_MELSEC_0031	(   31)		// シリアル回線データ受信異常（回線未オープン状態）
#define ERR_MELSEC_0032	(   32)		// シリアル回線データ受信異常（イベント登録に失敗）
#define ERR_MELSEC_0033	(   33)		// シリアル回線データ受信異常
#define ERR_MELSEC_0034	(   34)		// シリアル回線データ受信異常（受信タイムアウト）
#define ERR_MELSEC_0035	(   35)		// シリアル回線データ受信異常（エラー情報の取得に失敗）
#define ERR_MELSEC_0036	(   36)		// シリアル回線データ受信異常（通信エラーが発生）
#define ERR_MELSEC_0037	(   37)		// シリアル回線データ受信異常（受信データ読込に失敗）
#define ERR_MELSEC_0038	(   38)		// シリアル回線データ受信異常（受信データ読込に失敗）
#define ERR_MELSEC_0039	(   39)		// シリアル回線データ受信異常（受信データ読込に失敗）

#define ERR_MELSEC_0040	(   40)		// Ethernet回線接続異常（初期化伝送フレーム設定異常）
#define ERR_MELSEC_0041	(   41)		// Ethernet回線接続異常（既に回線オープン中）
#define ERR_MELSEC_0042	(   42)		// Ethernet回線接続異常（ソケット作成に失敗）
#define ERR_MELSEC_0043	(   43)		// Ethernet回線接続異常（イベント登録に失敗）
#define ERR_MELSEC_0044	(   44)		// Ethernet回線接続異常（イベント選択に失敗）
#define ERR_MELSEC_0045	(   45)		// Ethernet回線接続異常（接続監視タイムアウト）
#define ERR_MELSEC_0046	(   46)		// Ethernet回線接続異常（イベント種類抽出に失敗）
#define ERR_MELSEC_0047	(   47)		// Ethernet回線接続異常（イベント結果不正）

#define ERR_MELSEC_0051	(   51)		// Ethernet回線データ送信異常（回線未接続状態）
#define ERR_MELSEC_0052	(   52)		// Ethernet回線データ送信異常（送信に失敗）

#define ERR_MELSEC_0061	(   61)		// Ethernet回線データ受信異常（回線未接続状態）
#define ERR_MELSEC_0062	(   62)		// Ethernet回線データ受信異常（回線切断検出）
#define ERR_MELSEC_0063	(   63)		// Ethernet回線データ受信異常（受信タイムアウト）

#define ERR_MELSEC_0090	(   90)		// 回線クロース異常（初期化伝送フレーム設定異常）
#define ERR_MELSEC_0091	(   91)		// シリアル回線クローズ異常（回線未オープン状態）
#define ERR_MELSEC_0092	(   92)		// Ethernet回線切断異常（回線未接続状態）
// Update '25/01/27 NCS [Ver1.4.0( 1)] ----------------------------------------->
#define ERR_MELSEC_0093	(   93)		// Ethernet回線切断異常（shutdown命令に失敗）
#define ERR_MELSEC_0094	(   94)		// Ethernet回線切断異常（closesocket命令に失敗）
//<------------------------------------------------------------------------------

#define ERR_MELSEC_0100	(  100)		// コマンド編集異常（該当フレーム未サポート）
#define ERR_MELSEC_0101	(  101)		// コマンド編集異常（指定点数オーバー）
#define ERR_MELSEC_0102	(  102)		// コマンド編集異常（デバイス指定不正）
#define ERR_MELSEC_0103	(  103)		// コマンド編集異常（CPU型式指定不正）
// Update '22/01/20 NCS [Ver1.3.0( 1)] ----------------------------------------->
#define ERR_MELSEC_0104	(  104)		// コマンド編集異常（ラベル名長不正）
#define ERR_MELSEC_0105	(  105)		// コマンド編集異常（ラベル名配列指定不正）
//<------------------------------------------------------------------------------

#define ERR_MELSEC_0200	(  200)		// レスポンス受信に失敗
#define ERR_MELSEC_0201	(  201)		// レスポンス内容異常（先頭コード不正）
#define ERR_MELSEC_0202	(  202)		// レスポンス内容異常（サブヘッダ不正）
#define ERR_MELSEC_0203	(  203)		// レスポンス内容異常（ブロック番号／シリアル番号不一致）
#define ERR_MELSEC_0204	(  204)		// レスポンス内容異常（局番号不一致）
#define ERR_MELSEC_0205	(  205)		// レスポンス内容異常（PC番号不一致）
#define ERR_MELSEC_0206	(  206)		// レスポンス内容異常（ETXコード不正）
#define ERR_MELSEC_0207	(  207)		// レスポンス内容異常（サムチェック不一致）
#define ERR_MELSEC_0208	(  208)		// レスポンス内容異常（ネットワーク番号不一致）
#define ERR_MELSEC_0209	(  209)		// レスポンス内容異常（要求先ユニットI/O番号不一致）
#define ERR_MELSEC_0210	(  210)		// レスポンス内容異常（要求先ユニット局番号不一致）
#define ERR_MELSEC_0211	(  211)		// レスポンス内容異常（応答データ長不一致）
#define ERR_MELSEC_0212	(  212)		// レスポンス内容異常（フレーム識別番号不正）
#define ERR_MELSEC_0213	(  213)		// レスポンス内容異常（自局番号不一致）
#define ERR_MELSEC_0214	(  214)		// レスポンス内容異常（応答識別コード不正）
#define ERR_MELSEC_0215	(  215)		// レスポンス内容異常（固定値不正）
// Update '22/01/20 NCS [Ver1.3.0( 1)] ----------------------------------------->
#define ERR_MELSEC_0216	(  216)		// レスポンス内容異常（配列点数不正）
#define ERR_MELSEC_0217	(  217)		// レスポンス内容異常（データ型ID不正）
#define ERR_MELSEC_0218	(  218)		// レスポンス内容異常（読出し単位指定不正）
#define ERR_MELSEC_0219	(  219)		// レスポンス内容異常（読出し配列データ長不正）
#define ERR_MELSEC_0220	(  220)		// レスポンス内容異常（シリアル番号不一致）
//<------------------------------------------------------------------------------
// Update '24/02/08 NCS [Ver1.3.1( 4)] ----------------------------------------->
#define ERR_MELSEC_0221	(  221)		// レスポンス内容異常（CR,LFコード不正）
//<------------------------------------------------------------------------------
// Update '25/02/03 NCS [Ver1.4.0( 4)] ----------------------------------------->
#define ERR_MELSEC_0222	(  222)		// レスポンス内容異常（読出しデータ部不良）
//<------------------------------------------------------------------------------

#define ERR_MELSEC_0300	(  300)		// 通信ログエクスポート異常（ファイルオープンに失敗）
#define ERR_MELSEC_0301	(  301)		// 通信ログエクスポート異常（ファイルアクセス障害発生）

// PLCからの応答エラーコード（ERR_MELSEC_1000 + PLC応答エラーコード）
#define ERR_MELSEC_1000	( 1000)		// レスポンス内容異常（PLCエラー応答）


#ifdef __cplusplus
}
#endif

