using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using Microsoft.VisualBasic;
using System.Data.OleDb;        // データベース
using JRO;

using C1.Win.C1FlexGrid;
using X61GX42H1ST.Properties;
using Win32Api;

namespace X61GX42H1ST
{
    public partial class A_MEIN : Form
    {
        private String[] TBL01;   //転送データテーブル
        private String AgNeme;

        /// <summary>
        /// クラスのコンストラクタ
        /// </summary>
        public A_MEIN()
        {
            InitializeComponent();

            TBL01 = new String[100];
        }

        /// <summary>
        /// フォームの初期化
        /// </summary>
        private void A_MEIN_Load(object sender, EventArgs e)
        {
            // 元のカーソルを保持して待機カーソルに変更
            Cursor preCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            // フォーム・アイコンの設定
            {
                Icon = Program.GetIcon();
            }

            // ウィンドウの表示位置を調整する
            // ※↓それでも両サイドに"1pixel"ずつの余白ができる!!
            {
                uint flags = 0;
                Rectangle rect;
                int x;
                int y;
                int width;
                int height;

                rect = CWin32Api.GetWindowRect(Handle);

                x = Program.GetScreenX();
                y = Program.GetScreenY();
                width = Program.GetScreenWidth();
                height = Program.GetScreenHeight();

#if DEBUG
                flags |= CWin32Api.SWP_NOMOVE;
                flags |= CWin32Api.SWP_NOSIZE;
#else
                if ((x == 0) &&
                    (y == 0))
                {
                    flags |= CWin32Api.SWP_NOMOVE;
                }
                if ((width == 0) &&
                    (height == 0))
                {
                    flags |= CWin32Api.SWP_NOSIZE;
                }
#endif//DEBUG

                CWin32Api.SetWindowPos(Handle, 0, rect.X + x, rect.Y + y, rect.Width + width, rect.Height + height, flags);
            }

            // システムメニューを無効化する
            //
            {
                IntPtr hMenu;

                hMenu = CWin32Api.GetSystemMenu(Handle, false);     // システムメニューのハンドルを取得する

                if (hMenu != IntPtr.Zero)
                {
                    // システムメニュー定義あり
                    CWin32Api.RemoveMenu(hMenu, CWin32Api.SC_CLOSE, CWin32Api.MF_BYCOMMAND);  // ｢閉じる｣ボタン
                }
            }

            // 再描画の無効化
            c1FlexGrid1.Redraw = false;

            {
                int i;   // カウンター変数

                // グリッド初期設定
                c1FlexGrid1.FocusRect = FocusRectEnum.Heavy;  // セルを強調表示
                c1FlexGrid1.Rows.Count = 981;                  // 行の総数
                c1FlexGrid1.Cols.Count = 9;                    // 8 //7       //列の総数
                c1FlexGrid1.Rows.Fixed = 1;                    // 固定行の総数
                c1FlexGrid1.Cols.Fixed = 1;                    // 固定列の総数

                //c1FlexGrid1.Row = 0;        //行の指定
                c1FlexGrid1.Cols[0].Width = Pixel(12 * 50);       // 列幅の設定
                c1FlexGrid1.Cols[1].Width = Pixel(12 * 100);
                c1FlexGrid1.Cols[2].Width = Pixel(12 * 100);
                c1FlexGrid1.Cols[3].Width = Pixel(12 * 130);
                c1FlexGrid1.Cols[4].Width = Pixel(12 * 150);
                c1FlexGrid1.Cols[5].Width = Pixel(12 * 150);             // 12 * 150
                c1FlexGrid1.Cols[6].Width = Pixel(12 * 120);
                c1FlexGrid1.Cols[7].Width = Pixel(12 * 300);
                c1FlexGrid1.Cols[8].Width = Pixel(12 * 300);

                // 全行幅の設定
                c1FlexGrid1.Rows.DefaultSize = Pixel(350);

                // セル内のテキスト表示位置
                c1FlexGrid1.Cols[0].TextAlign = TextAlignEnum.CenterCenter;
                c1FlexGrid1.Cols[1].TextAlign = TextAlignEnum.LeftCenter;
                c1FlexGrid1.Cols[2].TextAlign = TextAlignEnum.LeftCenter;
                c1FlexGrid1.Cols[3].TextAlign = TextAlignEnum.LeftCenter;
                c1FlexGrid1.Cols[4].TextAlign = TextAlignEnum.LeftCenter;
                c1FlexGrid1.Cols[5].TextAlign = TextAlignEnum.LeftCenter;
                c1FlexGrid1.Cols[6].TextAlign = TextAlignEnum.LeftCenter;
                c1FlexGrid1.Cols[7].TextAlign = TextAlignEnum.LeftCenter;
                c1FlexGrid1.Cols[8].TextAlign = TextAlignEnum.LeftCenter;
                //c1FlexGrid1.Col = 0;        //列の指定

                // 行№を設定
                for (i = 1; i <= (c1FlexGrid1.Rows.Count - 1); i++) { c1FlexGrid1[i, 0] = i; }

                // ヘッダ部のセル内のテキスト表示位置
                c1FlexGrid1.Rows[0].TextAlignFixed = TextAlignEnum.CenterCenter;
                c1FlexGrid1.Rows[0].TextAlign = TextAlignEnum.CenterCenter;

                //列のタイトル設定
                c1FlexGrid1[0, 0] = "№";
                c1FlexGrid1[0, 1] = "識別コード";
                c1FlexGrid1[0, 2] = "治具タイプ";
                c1FlexGrid1[0, 3] = "治具切出し順";
                c1FlexGrid1[0, 4] = "1st治具ｽﾄｯｸ段"; //"1st治具ｽﾄｯｸ段"
                c1FlexGrid1[0, 5] = "2nd治具ｽﾄｯｸ段"; //"1st治具ｽﾄｯｸ段"
                //.Row = 0: .Col = 5: .Text = "2nd治具ｽﾄｯｸ段"
                c1FlexGrid1[0, 6] = "向　先";
                c1FlexGrid1[0, 7] = "1st側ｼｰﾄﾀｲﾌﾟ";
                c1FlexGrid1[0, 8] = "2nd側ｼｰﾄﾀｲﾌﾟ";

                // セルの初期位置
                c1FlexGrid1.Col = 1;
                c1FlexGrid1.Row = 1;
            }

            // 再描画の再設定
            c1FlexGrid1.Redraw = true;

            // 起動中ウィンドウを閉じる
            GBoot.VisibleWindow(false);

            // 表示タイマーの起動
            Timer1.Enabled = true;
        }


        /// <summary>
        /// ウィンドウ・プロシージャ
        /// </summary>
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {

                //////////////////////////////////
                // WM_SYSCOMMAND イベント       //
                //////////////////////////////////
                case CWin32Api.WM_SYSCOMMAND:
                    {
                        uint uCmdType = (uint)(m.WParam.ToInt32() & 0xFFF0);

                        switch (uCmdType)
                        {
                            case CWin32Api.SC_CLOSE:        // ウィンドウをクローズする
                                {
                                    return;                 // 禁止
                                }
                            // ※不要!! ===========================================================>
                            //   ->                 break;
                            //<=====================================================================
                            // ※不要!! ===========================================================>
                            //                            case CWin32Api.SC_MOVE:         // ウィンドウを移動する
                            //                                {
                            //#if DEBUG
                            //                                    break;                  // 許可
                            //#else
                            //                            return;                 // 禁止
                            //#endif //DEBUG
                            //                                }
                            //<=====================================================================
                            // ※不要!! ===========================================================>
                            //   ->                 break;
                            //<=====================================================================
                            case CWin32Api.SC_MOUSEMENU:    // マウス操作によるメニュー表示
                            case CWin32Api.SC_KEYMENU:      // キー操作によるメニュー表示
                                {
                                    return;                 // 禁止
                                }
                            // ※不要!! ===========================================================>
                            //   ->                 break;
                            //<=====================================================================
                            default:
                                break;
                        }
                    }
                    break;

                //////////////////////////////////
                // WM_QUERYENDSESSION イベント  //
                //////////////////////////////////
                case CWin32Api.WM_QUERYENDSESSION:
                    {
                        // フォームを閉じる
                        Exit();

                        return;
                    }
                // ※不要!! ===========================================================>
                //   ->         break;
                //<=====================================================================

                default:
                    break;
            }

            base.WndProc(ref m);
        }


        /// <summary>
        /// タイマー処理
        /// </summary>
        private void Timer1_Tick(object sender, EventArgs e)
        {
            ;
        }

        /// <summary>
        /// 「ファイル」メニューをクリック
        /// </summary>
        private void File_Command_Click(object sender, EventArgs e)
        {
            var clkdItem = sender as ToolStripMenuItem;
            string Msg;
            string Title;
            DialogResult dr;


            if ((clkdItem != null) &&
                (clkdItem.OwnerItem is ToolStripMenuItem item))
            {
                // 選択インデックス
                int index = item.DropDownItems.IndexOf(clkdItem);

                if ((index != 0) && (index != 5))
                {
                    // ファイル読込チェック
                    if (FileBox.FileOpenCheck(Program.GetWindow()) != true) return;
                }

                switch (index)
                {
                    case 0: // ファイル読込
                        Msg = "ファイルを選択し読込ます。　よろしいですか。";         // メッセージを定義します。
                        Title = "ファイル読込";                                       // タイトルを定義します。

                        // 確認ウィンドウを表示する
                        dr = MessageBox.Show(Msg, Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        if (dr == DialogResult.Yes)
                        {
                            FileBox.VisibleWindow(this, true);
                        }
                        break;

                    case 1: // データコピー
                        A_Dcopy.VisibleWindow(true);
                        break;

                    case 2: // 識別データ削除
                        Msg = "選択行を削除しますか?";         // メッセージを定義します。
                        Title = "削除";                          // タイトルを定義します。

                        // 確認ウィンドウを表示する
                        dr = MessageBox.Show(Msg, Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        if (dr == DialogResult.Yes)
                        {
                            Mdb_Del1(c1FlexGrid1.GetData(c1FlexGrid1.Row, 1).ToString());
                            Mdb_Del2(c1FlexGrid1.GetData(c1FlexGrid1.Row, 1).ToString());

                            for (int i = 1; i <= (c1FlexGrid1.Cols.Count - 1); i++)
                            {
                                c1FlexGrid1.SetData(c1FlexGrid1.Row, i, "");
                            }
                        }
                        break;

                    case 3: // データベース最適化
                        Msg = "データベースを最適化／修復しますか。";           // メッセージを定義します。
                        Title = "最適化／修復";                                   // タイトルを定義します。

                        // 確認ウィンドウを表示する
                        dr = MessageBox.Show(Msg, Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        if (dr == DialogResult.Yes)
                        {
                            Mdb_Compact();
                        }
                        break;

                    case 4: // 検索
                        // クリア
                        textBoxSearch.Text = string.Empty;

                        // 検索パネルを表示する
                        panelSearch.Visible = true;
                        break;

                    case 5: // アプリケーション終了
                        Exit(true);
                        break;

                    case 6: // ファイル名を付けてをアプリケーション終了
                        Msg = "ファイル名を付けてをアプリケーションを終了します。　よろしいですか。";           // メッセージを定義します。
                        Title = "アプリケーション終了";                                                           // タイトルを定義します。

                        // 確認ウィンドウを表示する
                        dr = MessageBox.Show(Msg, Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        if (dr == DialogResult.Yes)
                        {
                            FileEndBox.VisibleWindow(true);
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// 検索の「OK」ボタンをクリック
        /// </summary>
        private void button_OK_Click(object sender, EventArgs e)
        {
            if (textBoxSearch.Text != "")
            {
                for (int row = c1FlexGrid1.Rows.Fixed; row < c1FlexGrid1.Rows.Count; row++)
                {
                    var value = c1FlexGrid1.GetData(row, 1);

                    if (value != null)
                    {
                        if (string.Equals(value.ToString(), textBoxSearch.Text, StringComparison.OrdinalIgnoreCase) == true)
                        {
                            c1FlexGrid1.Row = row;
                            c1FlexGrid1.TopRow = row;
                            c1FlexGrid1.Focus();
                            return;
                        }
                    }
                }

                Program.MessageBox("検索対象が見つかりません。", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Program.MessageBox("検索キーワードを入力してください。", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 検索の「ｷｬﾝｾﾙ」ボタンをクリック
        /// </summary>
        private void button_Cancel_Click(object sender, EventArgs e)
        {
            // 検索パネルを非表示する
            panelSearch.Visible = false;
        }

        /// <summary>
        /// 「編集」メニューをクリック
        /// </summary>
        private void Edit_Command_Click(object sender, EventArgs e)
        {
            var clkdItem = sender as ToolStripMenuItem;

            // ファイル読込チェック
            if (FileBox.FileOpenCheck(Program.GetWindow()) != true) return;

            if ((clkdItem != null) &&
                (clkdItem.OwnerItem is ToolStripMenuItem item))
            {
                // 選択インデックス
                int index = item.DropDownItems.IndexOf(clkdItem);

                switch (index)
                {
                    case 0: // 識別コードマスター
                        A_SIKIBETU_MF.VisibleWindow(true);
                        break;

                    case 1: // レシピマスター
                        A_RESIPI_MF.VisibleWindow(true);
                        break;

                    case 2: // タグ情報マスター
                        A_TAGJYOHO_MF.VisibleWindow(true);
                        break;
                }
            }

            // 車種情報Ｍマスタの読込み
            MDB_READ1(this);
        }

        /// <summary>
        /// 「表示」メニューをクリック
        /// </summary>
        private void Disp_Command_Click(object sender, EventArgs e)
        {
            // ファイル読込チェック
            if (FileBox.FileOpenCheck(Program.GetWindow()) != true) return;

            // 「レシピ照会」画面の表示
            A_RESIPICALL.VisibleWindow(true);
        }


        //[2026/04/06][p.hoi][Add]=============================================================>>
        //  PLCデータ読込、照合追加
        //---------------------------------------------------------------------------------------

        /*/// <summary>
        /// 「通信」メニューをクリック
        /// </summary>
        private void Comm_Command_Click(object sender, EventArgs e)
        {
            var          clkdItem = sender as ToolStripMenuItem;
            string       MSG1;       // メッセージボックス用
            string       MSG2;       // メッセージボックス用
            string       Title;      // メッセージボックス用
            DialogResult dr;

            // ファイル読込チェック
            if (FileBox.FileOpenCheck(Program.GetWindow()) != true) return;

            if ((clkdItem != null) &&
                (clkdItem.OwnerItem is ToolStripMenuItem item))
            {
                // 選択インデックス
                WRK_ID id = (WRK_ID)item.DropDownItems.IndexOf(clkdItem);

                switch (id)
                {
                    case WRK_ID.PLC_SEND_T: // タグ情報アップロード
                        MSG1 = "タグ情報をＰＬＣへ転送します、よろしいですか。";                 // メッセージを定義します。
                        MSG2 = "タグ情報をアップロード中。";                                     // メッセージを定義します。
                        break;

                    case WRK_ID.PLC_SEND_1: // レシピ情報アップロード（パターン１）
                        MSG1 = "車種情報パターン１をＰＬＣへ転送します、よろしいですか。";       // メッセージを定義します。
                        MSG2 = "車種情報パターン１をアップロード中。";                           // メッセージを定義します。
                        break;

                    case WRK_ID.PLC_SEND_2: // レシピ情報アップロード（パターン２）
                        MSG1 = "車種情報パターン２をＰＬＣへ転送します、よろしいですか。";       // メッセージを定義します。
                        MSG2 = "車種情報パターン２をアップロード中。";                           // メッセージを定義します。
                        break;

                    case WRK_ID.PLC_SEND_3: // レシピ情報アップロード（パターン３）
                        MSG1 = "車種情報パターン３をＰＬＣへ転送します、よろしいですか。";       // メッセージを定義します。
                        MSG2 = "車種情報パターン３をアップロード中。";                           // メッセージを定義します。
                        break;

                    default:
                        MSG1 = "";
                        MSG2 = "";
                        break;
                }

                Title = "アップロード";                                                          // タイトルを定義します。
                // 確認ウィンドウを表示する
                dr = MessageBox.Show(MSG1, Title, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dr != DialogResult.OK)  return;

                labelTile.Text = MSG2;
                

                // アップロード処理の開始
                StartBackground(id);
            }
        }*/
        //---------------------------------------------------------------------------------------
        // タグ情報アップロード
        private void Comm_TagUpLoad_Click(object sender, EventArgs e)
        {
            string Title = "アップロード";
            string MSG1 = "タグ情報をＰＬＣへ転送します、よろしいですか。";
            string MSG2 = "タグ情報をアップロード中。";

            // ファイル読込チェック
            if (FileBox.FileOpenCheck(Program.GetWindow()) != true) return;

            // 確認ウィンドウを表示する
            DialogResult dr = MessageBox.Show(MSG1, Title,
                                              MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dr != DialogResult.OK) return;

            labelTile.Text = MSG2;

            // アップロード処理の開始
            StartBackground(WRK_ID.PLC_SEND_T);
        }

        // レシピ情報アップロード
        private void Comm_RepUpLoad_Click(object sender, EventArgs e)
        {
            var clkdItem = sender as ToolStripMenuItem;
            string MSG1;       // メッセージボックス用
            string MSG2;       // メッセージボックス用
            string Title;      // メッセージボックス用
            DialogResult dr;

            // ファイル読込チェック
            if (FileBox.FileOpenCheck(Program.GetWindow()) != true) return;

            if ((clkdItem != null) &&
                (clkdItem.OwnerItem is ToolStripMenuItem item))
            {
                // 選択インデックス
                WRK_ID id = (WRK_ID)(item.DropDownItems.IndexOf(clkdItem) + WRK_ID.PLC_SEND_1);

                switch (id)
                {
                    case WRK_ID.PLC_SEND_1: // レシピ情報アップロード（パターン１）
                        MSG1 = "車種情報パターン１をＰＬＣへ転送します、よろしいですか。";       // メッセージを定義します。
                        MSG2 = "車種情報パターン１をアップロード中。";                           // メッセージを定義します。
                        break;

                    case WRK_ID.PLC_SEND_2: // レシピ情報アップロード（パターン２）
                        MSG1 = "車種情報パターン２をＰＬＣへ転送します、よろしいですか。";       // メッセージを定義します。
                        MSG2 = "車種情報パターン２をアップロード中。";                           // メッセージを定義します。
                        break;

                    case WRK_ID.PLC_SEND_3: // レシピ情報アップロード（パターン３）
                        MSG1 = "車種情報パターン３をＰＬＣへ転送します、よろしいですか。";       // メッセージを定義します。
                        MSG2 = "車種情報パターン３をアップロード中。";                           // メッセージを定義します。
                        break;
                    default:
                        MSG1 = "";
                        MSG2 = "";
                        break;
                }

                Title = "アップロード";                                                          // タイトルを定義します。
                // 確認ウィンドウを表示する
                dr = MessageBox.Show(MSG1, Title, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dr != DialogResult.OK) return;

                labelTile.Text = MSG2;

                // アップロード処理の開始
                StartBackground(id);
            }
        }

        // ＰＬＣから読込み
        private void Comm_Read_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem clkdItem = sender as ToolStripMenuItem;
            string MSG1;       // メッセージボックス用
            string MSG2;       // メッセージボックス用
            string Title;      // メッセージボックス用
            DialogResult dr;

            if ((clkdItem != null) &&
                (clkdItem.OwnerItem is ToolStripMenuItem item))
            {
                // 選択インデックス
                WRK_ID id = (WRK_ID)(item.DropDownItems.IndexOf(clkdItem) + WRK_ID.PLC_RECV_1);

                switch (id)
                {
                    case WRK_ID.PLC_RECV_1: // ＰＬＣから読込み
                        MSG1 = "車種情報パターン１をＰＬＣから読込みます、よろしいですか。";     // メッセージを定義します。
                        MSG2 = "車種情報パターン１をＰＬＣから読込み中。";                       // メッセージを定義します。
                        break;

                    case WRK_ID.PLC_RECV_2: // ＰＬＣから読込み
                        MSG1 = "車種情報パターン２をＰＬＣから読込みます、よろしいですか。";     // メッセージを定義します。
                        MSG2 = "車種情報パターン２をＰＬＣから読込み中。";                       // メッセージを定義します。
                        break;

                    case WRK_ID.PLC_RECV_3: // ＰＬＣから読込み
                        MSG1 = "車種情報パターン３をＰＬＣから読込みます、よろしいですか。";     // メッセージを定義します。
                        MSG2 = "車種情報パターン３をＰＬＣから読込み中。";                       // メッセージを定義します。
                        break;

                    default:
                        MSG1 = "";
                        MSG2 = "";
                        break;
                }

                Title = "ＰＬＣから読込み";                                                     　// タイトルを定義します。
                // 確認ウィンドウを表示する
                dr = MessageBox.Show(MSG1, Title, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dr != DialogResult.OK) return;

                // ファイル作成
                PlcRecv dlg = new PlcRecv(Program.GetWindow());
                if (dlg.DoModal() != DialogResult.OK) return;

                labelTile.Text = MSG2;
                // アップロード処理の開始
                StartBackground(id);
            }
        }

        // ＰＬＣとの照合
        private void Comm_Compare_Click(object sender, EventArgs e)
        {
            var clkdItem = sender as ToolStripMenuItem;
            string MSG1;       // メッセージボックス用
            string MSG2;       // メッセージボックス用
            string Title;      // メッセージボックス用
            DialogResult dr;

            // ファイル読込チェック
            if (FileBox.FileOpenCheck(Program.GetWindow()) != true) return;

            if ((clkdItem != null) &&
                (clkdItem.OwnerItem is ToolStripMenuItem item))
            {
                // 選択インデックス
                WRK_ID id = (WRK_ID)(item.DropDownItems.IndexOf(clkdItem) + WRK_ID.PLC_COMP_1);

                switch (id)
                {
                    case WRK_ID.PLC_COMP_1: // ＰＬＣとの照合
                        MSG1 = "車種情報パターン１はＰＬＣとの照合します、よろしいですか。";     // メッセージを定義します。
                        MSG2 = "車種情報パターン１を照合中。";                                   // メッセージを定義します。
                        break;

                    case WRK_ID.PLC_COMP_2: // ＰＬＣとの照合
                        MSG1 = "車種情報パターン２はＰＬＣとの照合します、よろしいですか。";     // メッセージを定義します。
                        MSG2 = "車種情報パターン２を照合中。";                                   // メッセージを定義します。
                        break;

                    case WRK_ID.PLC_COMP_3: // ＰＬＣとの照合
                        MSG1 = "車種情報パターン３はＰＬＣとの照合します、よろしいですか。";     // メッセージを定義します。
                        MSG2 = "車種情報パターン３を照合中。";                                   // メッセージを定義します。
                        break;

                    default:
                        MSG1 = "";
                        MSG2 = "";
                        break;
                }

                Title = "ＰＬＣとの照合";                                                     　// タイトルを定義します。
                // 確認ウィンドウを表示する
                dr = MessageBox.Show(MSG1, Title, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dr != DialogResult.OK) return;

                labelTile.Text = MSG2;

                // アップロード処理の開始
                StartBackground(id);
            }
        }
        //<<=====================================================================================

        /// <summary>
        /// 「印刷」メニューをクリック
        /// </summary>
        private void Print_Command_Click(object sender, EventArgs e)
        {
            labelTile.Text = "車種情報の印刷中";

            // ファイル読込チェック
            if (FileBox.FileOpenCheck(Program.GetWindow()) != true) return;

            // 印刷処理の開始
            StartBackground(WRK_ID.PRINT);
        }

        /// <summary>
        /// ダブルクリック処理
        /// </summary>
        private void c1FlexGrid1_DoubleClick(object sender, EventArgs e)
        {
            // ファイル読込チェック
            if (FileBox.FileOpenCheck(Program.GetWindow()) != true) return;

            if ((c1FlexGrid1.Row > 0) &&
               (c1FlexGrid1.Col == 1) &&
               (Mdb_Module.DataBase1 != null))
            {
                // 「車種データ編集」画面を表示する
                A_EDIT.VisibleWindow(true, c1FlexGrid1.Rows[c1FlexGrid1.Row]);

                // 車種情報Ｍマスタの読込み
                MDB_READ1(this);
            }
        }

        /***********************************************************************
            フォームを閉じる
        ***********************************************************************/
        public void Exit(bool msg = false)
        {
            DialogResult dr;
            string message;

            if (msg == true)
            {
                // 確認ウィンドウを表示する
                message = Resources.ExitMessage;
                dr = Program.MessageBox(message, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.Yes)
                {
                    // フォームを閉じる
                    Close();
                }
            }
            else
            {
                // フォームを閉じる
                Close();
            }
        }

        /***********************************************************************
            TWIP <-> Pixel 変換
        ***********************************************************************/
        private const int TWIPS = 15;           // ピクセルあたりのツイップ値

        // TWIP -> Pixel 変換
        private static int Pixel(int twip)
        {
            return (twip / TWIPS);
        }


        //**********************************************************************
        // 
        // サブルーチン
        // 
        //**********************************************************************

        // １０進数を１６進数へ変換
        public string WcovHex1(string WDAT)
        {
            if (Conversion.Val(WDAT) == 0)
            {
                return ("0");
            }
            else
            {
                return (Strings.Right("0" + Conversion.Hex(Conversion.Val(WDAT)), 1));
            }
        }

        // １０進数を１６進数へ変換
        public string WcovHex2(string WDAT)
        {
            if (Conversion.Val(WDAT) == 0)
            {
                return ("00");
            }
            else
            {
                return (Strings.Right("00" + Conversion.Hex(Conversion.Val(WDAT)), 2));
            }

        }

        // １０進数を１６進数へ変換
        public string WcovHex3(string WDAT)
        {
            if (Conversion.Val(WDAT) == 0)
            {
                return ("000");
            }
            else
            {
                return (Strings.Right("000" + Conversion.Hex(Conversion.Val(WDAT)), 3));
            }

        }

        // １０進数を１６進数へ変換
        public string WcovHex4(string WDAT)
        {
            if (Conversion.Val(WDAT) == 0)
            {
                return ("0000");
            }
            else
            {
                return (Strings.Right("0000" + Conversion.Hex(Conversion.Val(WDAT)), 4));
            }
        }


        // １０進数を２進数へ変換し文字列で結合させ１０進数へ変換する
        public string WcovBit2(string WDAT01, string WDAT02)
        {
            Int32 C_item01;
            String C_item02;
            Int32 R_item01;
            String R_item02;
            String X_item;
            Int16 i;
            Int16 c;

            c = 0;
            C_item02 = String.Empty;
            R_item02 = String.Empty;
            C_item01 = (Int32)Conversion.Val(WDAT01);

            do
            {
                C_item02 = Convert.ToString(C_item01 % 2L) + C_item02;
                C_item01 = (Int32)Math.Round(Conversion.Fix((double)C_item01 / 2d));
            }
            while (C_item01 > 0);

            R_item01 = (Int32)Conversion.Val(WDAT02);
            do
            {
                R_item02 = Convert.ToString(R_item01 % 2L) + R_item02;
                R_item01 = (Int32)Math.Round(Conversion.Fix((double)R_item01 / 2d));
            }
            while (R_item01 > 0);

            X_item = Strings.Right("00000000" + C_item02, 8) + Strings.Right("00000000" + R_item02, 8);
            for (i = 1; i <= 15; i++)
            {
                if (Strings.Mid(X_item, i, 1) == "1")
                {
                    c = (Int16)Math.Round(c + Math.Pow(2d, i - 1));
                }
            }

            if (c == 0)
            {
                return ("0000");
            }
            else
            {
                return (Strings.Right("0000" + Conversion.Hex(c), 4));
            }
        }

        // Ａ＿ラインデータベース最適化・修復
        private void Mdb_Compact()
        {
            string ConDB1 = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={Mdb_Module.DataBase1};";
            string ConDB2 = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={Mdb_Module.DataBase2};";

            label1.Text = "Ａ＿ラインデータベース最適化・修復中";

            try
            {
                // 最適化,修復
                JetEngine DBEngine = new JetEngine();
                DBEngine.CompactDatabase(ConDB1, ConDB2 + "Jet OLEDB:Engine Type=5");

                File.Delete(Mdb_Module.DataBase1);
                File.Move(Mdb_Module.DataBase2, Mdb_Module.DataBase1);

                Program.MessageBox("最適化／修復完了");

                label1.Text = "";
            }
            catch
            {
                ;
            }
        }

        // 車種情報Ｔの指定されたデータを削除します
        private void Mdb_Del1(string Key_code1)
        {
            string SQl;
            string conStr = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={Mdb_Module.DataBase1};";

            try
            {
                using (OleDbConnection DB = new OleDbConnection(conStr))
                {
                    DB.Open(); // データベース接続

                    SQl = "DELETE * From T_車種情報Ｔ ";
                    SQl += "WHERE 識別コード = '" + Key_code1 + "'";

                    // 実行
                    using (OleDbCommand cmd1 = new OleDbCommand(SQl, DB))
                    {
                        cmd1.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
                ;
            }
        }

        // 車種情報Ｍの指定されたデータを削除します
        private void Mdb_Del2(string Key_code1)
        {
            string SQl;
            string conStr = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={Mdb_Module.DataBase1};";

            try
            {
                using (OleDbConnection DB = new OleDbConnection(conStr))
                {
                    DB.Open(); // データベース接続

                    SQl = "DELETE * From T_車種情報Ｍ ";
                    SQl += "WHERE 識別コード = '" + Key_code1 + "'";

                    // 実行
                    using (OleDbCommand cmd1 = new OleDbCommand(SQl, DB))
                    {
                        cmd1.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
                ;
            }
        }

        // 車種情報Ｍマスタの読込み
        public static void MDB_READ1(A_MEIN frm)
        {
            string SQl;
            string conStr = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={Mdb_Module.DataBase1};";

            try
            {
                using (OleDbConnection DB = new OleDbConnection(conStr))
                {
                    DB.Open(); // データベース接続

                    SQl = "select * from T_車種情報Ｍ ";
                    SQl += "order by 識別コード";

                    using (OleDbCommand cmd = new OleDbCommand(SQl, DB))
                    {
                        using (OleDbDataReader RS = cmd.ExecuteReader())
                        {
                            int row = 1;

                            // 再描画の無効化
                            frm.c1FlexGrid1.Redraw = false;

                            while (RS.Read())
                            {
                                if (RS["識別コード"].ToString().Length > 1)
                                {
                                    frm.c1FlexGrid1.SetData(row, 1, (RS["識別コード"]));
                                    frm.c1FlexGrid1.SetData(row, 2, (RS["治具タイプ"]));
                                    frm.c1FlexGrid1.SetData(row, 3, (RS["治具切出し順"]));
                                    frm.c1FlexGrid1.SetData(row, 4, (RS["1st治具ストック段"]));
                                    frm.c1FlexGrid1.SetData(row, 5, (RS["2nd治具ストック段"]));
                                    frm.c1FlexGrid1.SetData(row, 6, (RS["向先"]));
                                    frm.c1FlexGrid1.SetData(row, 7, (RS["1st側ｼｰﾄﾀｲﾌﾟ"]));
                                    frm.c1FlexGrid1.SetData(row, 8, (RS["2nd側ｼｰﾄﾀｲﾌﾟ"]));
                                }

                                row++;
                            }

                            // 再描画の再設定
                            frm.c1FlexGrid1.Redraw = true;
                        }
                    }

                    frm.label2.Text = Mdb_Module.DataBase1;
                }
            }
            catch
            {
                ;
            }
        }

        // 治具タイプマスタの読込み
        private string MDB_READ2(string Key_code)
        {
            string SQl;
            string conStr = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={Mdb_Module.DataBase1};";

            try
            {
                using (OleDbConnection DB = new OleDbConnection(conStr))
                {
                    DB.Open(); // データベース接続

                    SQl = "select * from T_治具タイプ ";
                    SQl += "WHERE № = '" + " " + Key_code + "'";

                    using (OleDbCommand cmd = new OleDbCommand(SQl, DB))
                    {
                        using (OleDbDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows != true) return ("");
                            else return (Strings.Right(RS["№"].ToString(), Strings.Len(RS["№"]) - 1) + ":" + Strings.Right(RS["治具タイプ"].ToString(), Strings.Len(RS["治具タイプ"]) - 1));
                        }
                    }
                }
            }
            catch
            {
                ;
            }

            return ("");
        }

        // ロボットプログラムマスタの読込み
        private string MDB_READ3(string Key_code)
        {
            string SQl;
            string conStr = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={Mdb_Module.DataBase1};";

            try
            {
                using (OleDbConnection DB = new OleDbConnection(conStr))
                {
                    DB.Open(); // データベース接続

                    SQl = "select * from T_ＲＢプログラム ";
                    SQl += "WHERE ＰＧ№ = '" + " " + Key_code + "'";

                    using (OleDbCommand cmd = new OleDbCommand(SQl, DB))
                    {
                        using (OleDbDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows != true) return ("");
                            else return (Strings.Right(RS["ＰＧ№"].ToString(), Strings.Len(RS["ＰＧ№"]) - 1) + ":" + Strings.Right(RS["ＰＧ名"].ToString(), Strings.Len(RS["ＰＧ名"]) - 1));
                        }
                    }
                }
            }
            catch
            {
                ;
            }

            return ("");
        }

        // タグ情報マスタ転送データの読込み
        private void DOWN_LOAD_DATA1(string Key_code1)
        {
            int i;
            string WAK;
            string SQl;
            string conStr = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={Mdb_Module.DataBase1};";

            try
            {
                using (OleDbConnection DB = new OleDbConnection(conStr))
                {
                    DB.Open(); // データベース接続

                    SQl = "select * from T_タグ ";
                    SQl += "WHERE № = '" + Key_code1 + "'";

                    using (OleDbCommand cmd = new OleDbCommand(SQl, DB))
                    {
                        using (OleDbDataReader RS = cmd.ExecuteReader())
                        {
                            for (i = 0; i <= 5; i++)
                            {
                                Mx_sub.Buf[i] = 0;
                            }

                            if (RS.Read() == true)
                            {
                                if (Convert.ToString(RS["治具タイプ"]).Length > 1)
                                {
                                    WAK = Strings.Right(RS["治具タイプ"].ToString(), Strings.Len(RS["治具タイプ"]) - 1);
                                    Mx_sub.Buf[0] = (UInt16)Conversion.Val(Strings.Left(WAK, Strings.InStr(WAK, ":") - 1));
                                }
                                else
                                {
                                    Mx_sub.Buf[0] = 0;
                                }

                                if (Convert.ToString(RS["治具ＬＲ"]).Length > 1)
                                {
                                    WAK = Strings.Right(RS["治具ＬＲ"].ToString(), Strings.Len(RS["治具ＬＲ"]) - 1);
                                    Mx_sub.Buf[1] = (UInt16)Conversion.Val(Strings.Left(WAK, Strings.InStr(WAK, ":") - 1));
                                }
                                else
                                {
                                    Mx_sub.Buf[1] = 0;
                                }

                                if (Convert.ToString(RS["治具ストック段"]).Length > 1)
                                {
                                    WAK = Strings.Right(RS["治具ストック段"].ToString(), Strings.Len(RS["治具ストック段"]) - 1);
                                    Mx_sub.Buf[2] = (UInt16)Conversion.Val(Strings.Left(WAK, Strings.InStr(WAK, ":") - 1));
                                }
                                else
                                {
                                    Mx_sub.Buf[2] = 0;
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                ;
            }
        }

        // 車種情報転送データ読込み
        private void DOWN_LOAD_DATA3(string Key_code1, string Key_code2)
        {
            string RP;
            int i;
            int R_Cnt;
            string SQl;
            string conStr = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={Mdb_Module.DataBase1};";

            for (i = 0; i <= 16; i++)
            {
                TBL01[i] = string.Empty;
            }

            AgNeme = Strings.Space(10);

            try
            {
                using (OleDbConnection DB = new OleDbConnection(conStr))
                {
                    DB.Open(); // データベース接続

                    SQl = "select * from T_車種情報Ｔ ";
                    SQl += "WHERE Trim(識別コード) = '" + Key_code1.Trim() + "'" + " AND " + "Trim(タブＧＰ) = '" + Key_code2.Trim() + "'";
                    SQl += " order by val(レシピ№)";

                    using (OleDbCommand cmd = new OleDbCommand(SQl, DB))
                    {
                        using (OleDbDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows == true)
                            {
                                R_Cnt = 1;

                                while (RS.Read())
                                {
                                    RP = WcovHex3(Strings.Right(RS["レシピ№"].ToString(), Strings.Len(RS["レシピ№"]) - 1));
                                    if (Conversion.Val(RS["ST0101"]) != 0)
                                    {
                                        TBL01[1] += WcovHex1(Strings.Right(RS["ST0101"].ToString(), Strings.Len(RS["ST0101"]) - 1)) + RP;
                                    }
                                    if (Conversion.Val(RS["ST0102"]) != 0)
                                    {
                                        TBL01[2] += WcovHex1(Strings.Right(RS["ST0102"].ToString(), Strings.Len(RS["ST0102"]) - 1)) + RP;
                                    }
                                    if (Conversion.Val(RS["ST0103"]) != 0)
                                    {
                                        TBL01[3] += WcovHex1(Strings.Right(RS["ST0103"].ToString(), Strings.Len(RS["ST0103"]) - 1)) + RP;
                                    }
                                    if (Conversion.Val(RS["ST0104"]) != 0)
                                    {
                                        TBL01[4] += WcovHex1(Strings.Right(RS["ST0104"].ToString(), Strings.Len(RS["ST0104"]) - 1)) + RP;
                                    }
                                    if (Conversion.Val(RS["ST0105"]) != 0)
                                    {
                                        TBL01[5] += WcovHex1(Strings.Right(RS["ST0105"].ToString(), Strings.Len(RS["ST0105"]) - 1)) + RP;
                                    }
                                    if (Conversion.Val(RS["ST0106"]) != 0)
                                    {
                                        TBL01[6] += WcovHex1(Strings.Right(RS["ST0106"].ToString(), Strings.Len(RS["ST0106"]) - 1)) + RP;
                                    }
                                    if (Conversion.Val(RS["ST0107"]) != 0)
                                    {
                                        TBL01[7] += WcovHex1(Strings.Right(RS["ST0107"].ToString(), Strings.Len(RS["ST0107"]) - 1)) + RP;
                                    }
                                    if (Conversion.Val(RS["ST0108"]) != 0)
                                    {
                                        TBL01[8] += WcovHex1(Strings.Right(RS["ST0108"].ToString(), Strings.Len(RS["ST0108"]) - 1)) + RP;
                                    }
                                    if (Conversion.Val(RS["ST0109"]) != 0)
                                    {
                                        TBL01[9] += WcovHex1(Strings.Right(RS["ST0109"].ToString(), Strings.Len(RS["ST0109"]) - 1)) + RP;
                                    }
                                    if (Conversion.Val(RS["ST0110"]) != 0)
                                    {
                                        TBL01[10] += WcovHex1(Strings.Right(RS["ST0110"].ToString(), Strings.Len(RS["ST0110"]) - 1)) + RP;
                                    }
                                    if (Conversion.Val(RS["ST0111"]) != 0)
                                    {
                                        TBL01[11] += WcovHex1(Strings.Right(RS["ST0111"].ToString(), Strings.Len(RS["ST0111"]) - 1)) + RP;
                                    }
                                    if (Conversion.Val(RS["ST0112"]) != 0)
                                    {
                                        TBL01[12] += WcovHex1(Strings.Right(RS["ST0112"].ToString(), Strings.Len(RS["ST0112"]) - 1)) + RP;
                                    }
                                    if (Conversion.Val(RS["ST0113"]) != 0)
                                    {
                                        TBL01[13] += WcovHex1(Strings.Right(RS["ST0113"].ToString(), Strings.Len(RS["ST0113"]) - 1)) + RP;
                                    }
                                    if (Conversion.Val(RS["ST0114"]) != 0)
                                    {
                                        TBL01[14] += WcovHex1(Strings.Right(RS["ST0114"].ToString(), Strings.Len(RS["ST0114"]) - 1)) + RP;
                                    }
                                    if (Conversion.Val(RS["ST0115"]) != 0)
                                    {
                                        TBL01[15] += WcovHex1(Strings.Right(RS["ST0115"].ToString(), Strings.Len(RS["ST0115"]) - 1)) + RP;
                                    }
                                    if (Conversion.Val(RS["ST0116"]) != 0)
                                    {
                                        TBL01[16] += WcovHex1(Strings.Right(RS["ST0116"].ToString(), Strings.Len(RS["ST0116"]) - 1)) + RP;
                                    }

                                    if (R_Cnt == 1)
                                    {
                                        TBL01[0] += WcovHex4(Strings.Left(RS["ｼｰﾄﾀｲﾌﾟ"].ToString(), Strings.InStr(RS["ｼｰﾄﾀｲﾌﾟ"].ToString(), ":") - 1));
                                        TBL01[0] += WcovHex4(Strings.Left(RS["AGﾀｲﾌﾟ"].ToString(), Strings.InStr(RS["AGﾀｲﾌﾟ"].ToString(), ":") - 1));
                                        TBL01[0] += WcovHex4(Strings.Left(RS["ﾋｰﾀｰ"].ToString(), Strings.InStr(RS["ﾋｰﾀｰ"].ToString(), ":") - 1));
                                        TBL01[0] += WcovHex4(Strings.Left(RS["ﾊﾞｯｸﾙ"].ToString(), Strings.InStr(RS["ﾊﾞｯｸﾙ"].ToString(), ":") - 1));
                                        TBL01[0] += WcovHex4(Strings.Left(RS["ﾍｯﾄﾞﾚｽﾄ"].ToString(), Strings.InStr(RS["ﾍｯﾄﾞﾚｽﾄ"].ToString(), ":") - 1));
                                        TBL01[0] += WcovHex4(Strings.Left(RS["着座ｾﾝｻｰ"].ToString(), Strings.InStr(RS["着座ｾﾝｻｰ"].ToString(), ":") - 1));
                                        TBL01[0] += WcovHex4(Strings.Left(RS["空調"].ToString(), Strings.InStr(RS["空調"].ToString(), ":") - 1));
                                        TBL01[0] += WcovHex4(Strings.Left(RS["表皮材"].ToString(), Strings.InStr(RS["表皮材"].ToString(), ":") - 1));
                                        TBL01[0] += WcovHex4(Strings.Left(RS["色"].ToString(), Strings.InStr(RS["色"].ToString(), ":") - 1));
                                        TBL01[0] += WcovHex4(Strings.Left(RS["ﾗﾝﾊﾞｰ"].ToString(), Strings.InStr(RS["ﾗﾝﾊﾞｰ"].ToString(), ":") - 1));
                                        TBL01[0] += WcovHex4(Strings.Left(RS["背面ﾎﾟｹｯﾄ"].ToString(), Strings.InStr(RS["背面ﾎﾟｹｯﾄ"].ToString(), ":") - 1));
                                        TBL01[0] += WcovHex4(Strings.Left(RS["ﾌｯﾄｳｴﾙﾗﾝﾌﾟ"].ToString(), Strings.InStr(RS["ﾌｯﾄｳｴﾙﾗﾝﾌﾟ"].ToString(), ":") - 1));
                                        TBL01[0] += WcovHex4(Strings.Left(RS["ｱｰﾑﾚｽﾄ"].ToString(), Strings.InStr(RS["ｱｰﾑﾚｽﾄ"].ToString(), ":") - 1));
                                        TBL01[0] += WcovHex4(Strings.Left(RS["ﾀﾝﾌﾞﾙ"].ToString(), Strings.InStr(RS["ﾀﾝﾌﾞﾙ"].ToString(), ":") - 1));
                                        TBL01[0] += WcovHex4(Strings.Left(RS["ISOFIX"].ToString(), Strings.InStr(RS["ISOFIX"].ToString(), ":") - 1));
                                        TBL01[0] += WcovHex4(Strings.Left(RS["ﾃｻﾞｰ"].ToString(), Strings.InStr(RS["ﾃｻﾞｰ"].ToString(), ":") - 1));
                                        TBL01[0] += WcovHex4(Strings.Left(RS["ｵｯﾄﾏﾝ"].ToString(), Strings.InStr(RS["ｵｯﾄﾏﾝ"].ToString(), ":") - 1));

                                        if (RS["ｺﾝﾋﾞﾆﾌｯｸ"].ToString() != "")
                                        {
                                            TBL01[0] += WcovHex4(Strings.Left(RS["ｺﾝﾋﾞﾆﾌｯｸ"].ToString(), Strings.InStr(RS["ｺﾝﾋﾞﾆﾌｯｸ"].ToString(), ":") - 1));
                                            TBL01[0] += WcovHex4(Strings.Left(RS["ｻｲﾄﾞﾃｰﾌﾞﾙ"].ToString(), Strings.InStr(RS["ｻｲﾄﾞﾃｰﾌﾞﾙ"].ToString(), ":") - 1));
                                        }
                                        else
                                        {
                                            TBL01[0] += "00000000";
                                        }
                                        if (RS["ﾛﾎﾞｯﾄ"].ToString() != "")
                                        {
                                            TBL01[0] += WcovHex4(Strings.Left(RS["ﾛﾎﾞｯﾄ"].ToString(), Strings.InStr(RS["ﾛﾎﾞｯﾄ"].ToString(), ":") - 1));
                                        }
                                        else
                                        {
                                            TBL01[0] += "0000";
                                        }
                                        // ****TBL01(0) = TBL01(0) + "0000"
                                        AgNeme = Strings.Right(RS["AGﾀｲﾌﾟ"].ToString(), Strings.Len(RS["AGﾀｲﾌﾟ"].ToString()) - Strings.InStr(RS["AGﾀｲﾌﾟ"].ToString(), ":")) + Strings.Space(10);
                                        // ＡＧ名の取込
                                        AgNeme = Strings.Left(AgNeme, 10);
                                    }

                                    R_Cnt++;
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                ;
            }
        }

        /// <summary>
        /// アップロード処理
        /// </summary>
        private Mx_sub.PLC_ERR PlcDown(BackgroundWorker ctl, WRK_ID id, C1FlexGrid flexGrid)
        {
            string DAT;        // 送信データ
            int i;          // ループ添字
            int j;          // 
            int j1;
            int j2;
            string Lpatan;
            string Rpatan;
            string Wasc;
            UInt32 ADS01;
            UInt32 Devno;
            Int32 size;
            Mx_sub.PLC_ERR Ret;
            string c;

            Lpatan = string.Empty;
            Rpatan = string.Empty;

            Ret = Mx_sub.Com1_OPEN(); // イーサーネットポートを開く

            if (Mx_sub.IsConnect() == true)
            {
                Ret = Mx_sub.DatRecv(Mx_sub.MELSEC_DEV_ZR, 0, 5);    // ＰＬＣ読込み処理
                if (Ret == 0)
                {
                    if (Mx_sub.Com01_Tbl01[0] == 1)  // アップロード許可
                    {
                        Mx_sub.Buf[0] = (UInt16)Conversion.Val("0001");
                        Ret = Mx_sub.DatSend(Mx_sub.MELSEC_DEV_ZR, 5, 1, Mx_sub.Buf);   // ＰＬＣ書込み中フラグ
                        if (Ret == 0)
                        {
                            switch (id)
                            {
                                case WRK_ID.PLC_SEND_T:   // タグ情報アップロード

                                    // バックグラウンドの進捗処理更新
                                    ctl.ReportProgress(100, new CReadState(0, 500, "タグ情報アップロード中"));

                                    ADS01 = 7000;                         // 先頭アドレス
                                    for (i = 1; i <= 500; i++)              // グリッドのループ
                                    {
                                        Devno = ADS01;                      //先頭デバイス№の算出
                                        c = Conversion.Str(Devno);
                                        size = 5;                        // 書込みワード数
                                        DOWN_LOAD_DATA1(Conversion.Str(i)); // 転送データの読込み
                                                                            // タグ情報アップロード（Ｃｏｍ１）
                                        Ret = Mx_sub.DatSend(Mx_sub.MELSEC_DEV_ZR, Devno, (UInt16)size, Mx_sub.Buf);
                                        if (Ret != 0)
                                        {
                                            // イーサーネットポートを閉じる
                                            Mx_sub.Com1_CLOSE();
                                            return Ret;
                                        }

                                        ADS01 = ADS01 + 5;   // 先頭アドレス

                                        // バックグラウンドの進捗処理更新
                                        ctl.ReportProgress(100, new CReadState(i, 500, "タグ情報アップロード中"));
                                    }

                                    break;

                                case WRK_ID.PLC_SEND_1:   // レシピ情報アップロード
                                case WRK_ID.PLC_SEND_2:
                                case WRK_ID.PLC_SEND_3:

                                    int rcnt = 0;

                                    for (int row = flexGrid.Rows.Fixed; row < flexGrid.Rows.Count; row++)
                                    {
                                        if (flexGrid.GetData(row, 1) == null)
                                        {
                                            rcnt = row - 1;     // データ数
                                            break;
                                        }
                                    }

                                    if (rcnt > 0)
                                    {
                                        // パターン設定
                                        if (id == WRK_ID.PLC_SEND_1) { Lpatan = "0"; Rpatan = "3"; }
                                        if (id == WRK_ID.PLC_SEND_2) { Lpatan = "1"; Rpatan = "4"; }
                                        if (id == WRK_ID.PLC_SEND_3) { Lpatan = "2"; Rpatan = "5"; }

                                        // バックグラウンドの進捗処理更新
                                        ctl.ReportProgress(100, new CReadState(0, flexGrid.Rows.Count, "レシピヘッダー部アップロード中"));

                                        // ヘッダー部アップロード処理
                                        ADS01 = 10000;                                            // 書込み先頭アドレス初期値
                                        for (i = 1; i <= (flexGrid.Rows.Count - 1); i++)
                                        {     // 行のループ
                                            DAT = "";                                             // 書込みデータ変数リセット
                                            for (j = 1; j <= (flexGrid.Cols.Count - 2); j++)
                                            { // 列のループ
                                                switch (j)
                                                {
                                                    case 1:     // 識別コードの場合
                                                        Wasc = flexGrid.GetData(i, j) + Strings.Space(20);
                                                        Wasc = Strings.Left(Wasc, 20);
                                                        c = Mx_sub.WcovAsc(Wasc, 20);            // 識別コードをアスキー変換

                                                        Mx_sub.Buf[0] = (UInt16)Conversion.Val("&h" + Strings.Left(c, 4));
                                                        Mx_sub.Buf[1] = (UInt16)Conversion.Val("&h" + Strings.Mid(c, 5, 4));
                                                        Mx_sub.Buf[2] = (UInt16)Conversion.Val("&h" + Strings.Mid(c, 9, 4));
                                                        Mx_sub.Buf[3] = (UInt16)Conversion.Val("&h" + Strings.Mid(c, 13, 4));
                                                        Mx_sub.Buf[4] = (UInt16)Conversion.Val("&h" + Strings.Mid(c, 17, 4));
                                                        Mx_sub.Buf[5] = (UInt16)Conversion.Val("&h" + Strings.Mid(c, 21, 4));
                                                        Mx_sub.Buf[6] = (UInt16)Conversion.Val("&h" + Strings.Mid(c, 25, 4));
                                                        Mx_sub.Buf[7] = (UInt16)Conversion.Val("&h" + Strings.Mid(c, 29, 4));
                                                        Mx_sub.Buf[8] = (UInt16)Conversion.Val("&h" + Strings.Mid(c, 33, 4));
                                                        Mx_sub.Buf[9] = (UInt16)Conversion.Val("&h" + Strings.Right(c, 4));
                                                        break;

                                                    case 2:
                                                    case 3:
                                                    case 4:
                                                    case 5:
                                                    case 6:
                                                        if (flexGrid.GetData(i, j) != null)
                                                        {
                                                            // [2025/07/20] [ver1.0.1]=============================================================>>
                                                            //Mx_sub.Buf[j + 8] = (UInt16)Conversion.Val(Strings.Left(flexGrid.GetData(i, j).ToString(), Strings.InStr(flexGrid.GetData(i, j).ToString(), ":") - 1));
                                                            //---------------------------------------------------------------------------------------
                                                            string wrk = string.Empty;
                                                            string str = flexGrid.GetData(i, j).ToString().TrimStart();

                                                            for (int ii = 0; ii < str.Length; ii++)
                                                            {
                                                                if ((str[ii] >= '0') && (str[ii] <= '9')) wrk += str[ii];
                                                                else
                                                                {
                                                                    break;
                                                                }
                                                            }

                                                            if (wrk.Length > 0) Mx_sub.Buf[j + 8] = (UInt16)Conversion.Val(wrk);
                                                            else Mx_sub.Buf[j + 8] = 0;
                                                            //<<=====================================================================================
                                                        }
                                                        else
                                                        {
                                                            Mx_sub.Buf[j + 8] = 0;
                                                        }

                                                        break;

                                                }
                                            }

                                            Devno = ADS01;  // 先頭デバイス№の算出
                                            c = Conversion.Str(Devno);
                                            size = 15;      // 書込みワード数

                                            // ヘッダー部アップロード（Ｃｏｍ１）
                                            Ret = Mx_sub.DatSend(Mx_sub.MELSEC_DEV_ZR, Devno, (UInt16)size, Mx_sub.Buf);
                                            if (Ret != 0)
                                            {
                                                // イーサーネットポートを閉じる
                                                Mx_sub.Com1_CLOSE();
                                                return Ret;
                                            }
                                            ADS01 = ADS01 + 500; // 先頭デバイス№の算出

                                            // バックグラウンドの進捗処理更新
                                            ctl.ReportProgress(100, new CReadState(i, flexGrid.Rows.Count, "レシピヘッダー部アップロード中"));
                                        }


                                        // パターン設定
                                        if (id == WRK_ID.PLC_SEND_1) { Lpatan = "0"; Rpatan = "3"; }
                                        if (id == WRK_ID.PLC_SEND_2) { Lpatan = "1"; Rpatan = "4"; }
                                        if (id == WRK_ID.PLC_SEND_3) { Lpatan = "2"; Rpatan = "5"; }

                                        // バックグラウンドの進捗処理更新
                                        ctl.ReportProgress(100, new CReadState(0, rcnt, "レシピ詳細部(ＬＨ側）アップロード中"));

                                        DAT = "";
                                        // 詳細部アップロード処理(１ｓｔ側）
                                        //Com_Msg1 = "レシピ詳細部(１ｓｔ側）アップロード中"
                                        //'+++++++                              // MSFlexGrid1.Col = 1  列の指定
                                        ADS01 = 10040;          // 書込み先頭アドレス初期値
                                        for (i = 1; i <= rcnt; i++)
                                        {  // 行のループ
                                            if (flexGrid.GetData(i, 1) != null)
                                            {
                                                // 識別コードが有る時
                                                DOWN_LOAD_DATA3(flexGrid.GetData(i, 1).ToString(), Lpatan);     // 転送データの読込み
                                                for (j = 1; j <= 16; j++)
                                                { // ステーションのループ
                                                    switch (j)
                                                    {
                                                        case 1:
                                                        case 2:
                                                        case 3:
                                                        case 4:
                                                        case 5:
                                                        case 6:
                                                        case 7:
                                                        case 8:
                                                        case 9:
                                                        case 10:
                                                        case 11:
                                                            DAT = Strings.Left(TBL01[j] + "0000000000000000000000000000000000000000", 40);
                                                            break;

                                                        case 12:
                                                        case 13:
                                                            DAT = TBL01[j] + "0000000000000000000000000000000000000000";
                                                            DAT = Strings.Left(DAT + "0000000000000000000000000000000000000000", 80);
                                                            break;
                                                        case 14:
                                                            DAT = TBL01[j] + "0000000000000000000000000000000000000000";
                                                            DAT += "0000000000000000000000000000000000000000";
                                                            DAT = Strings.Left(DAT + "00000000000000000000", 100);
                                                            break;

                                                        case 15:
                                                        case 16:
                                                            DAT = Strings.Left(TBL01[j] + "0000000000000000000000000000000000000000", 40);
                                                            break;
                                                    }

                                                    j2 = 0;
                                                    for (j1 = 1; j1 <= Strings.Len(DAT); j1 += 4)
                                                    {
                                                        Mx_sub.Buf[j2] = (UInt16)Conversion.Val("&h" + Strings.Mid(DAT, j1, 4));
                                                        j2 = j2 + 1;
                                                    }
                                                    Devno = ADS01;  // 先頭デバイス№の算出
                                                    c = Conversion.Str(Devno);
                                                    size = Strings.Len(DAT) / 4; // 書込みワード数
                                                                                 // １ｓｔ側詳細アップロード（Ｃｏｍ１）
                                                    Ret = Mx_sub.DatSend(Mx_sub.MELSEC_DEV_ZR, Devno, (UInt16)size, Mx_sub.Buf);
                                                    //Debug.Print "詳細", Deve, Size, DAT
                                                    if (Ret != 0)
                                                    {
                                                        // イーサーネットポートを閉じる
                                                        Mx_sub.Com1_CLOSE();
                                                        return Ret;
                                                    }
                                                    switch (j)
                                                    {
                                                        case 1:
                                                        case 2:
                                                        case 3:
                                                        case 4:
                                                        case 5:
                                                        case 6:
                                                        case 7:
                                                        case 8:
                                                        case 9:
                                                        case 10:
                                                        case 11:
                                                            ADS01 += 10;
                                                            break;

                                                        case 12:
                                                        case 13:
                                                            ADS01 += 20;
                                                            break;

                                                        case 14:
                                                            ADS01 += 25;
                                                            break;

                                                        case 15:
                                                        case 16:
                                                            ADS01 += 10;
                                                            break;
                                                    }
                                                }


                                                // 1st仕様設定部
                                                DAT = TBL01[0] + "0000000000000000000000000000000000000000";
                                                DAT = Strings.Left(DAT + "0000000000000000000000000000000000000000", 80);
                                                j2 = 0;
                                                for (j1 = 1; j1 <= Strings.Len(DAT); j1 += 4)
                                                {
                                                    Mx_sub.Buf[j2] = (UInt16)Conversion.Val("&h" + Strings.Mid(DAT, j1, 4));
                                                    j2 += 1;
                                                }
                                                Devno = ADS01;  // 先頭デバイス№の算出
                                                c = Conversion.Str(Devno);
                                                size = Strings.Len(DAT) / 4; // 書込みバイト数
                                                // 1st仕様アップロード（Ｃｏｍ１）
                                                // Debug.Print "仕様部", Deve, Size, DAT
                                                Ret = Mx_sub.DatSend(Mx_sub.MELSEC_DEV_ZR, Devno, (UInt16)size, Mx_sub.Buf);
                                                if (Ret != 0)
                                                {
                                                    // イーサーネットポートを閉じる
                                                    Mx_sub.Com1_CLOSE();
                                                    return Ret;
                                                }

                                                ADS01 += 30;
                                                c = Mx_sub.WcovAsc(AgNeme, 10);    //'1stＡＧタイプ名をアスキー変換
                                                Mx_sub.Buf[0] = (UInt16)Conversion.Val("&h" + Strings.Left(c, 4));
                                                Mx_sub.Buf[1] = (UInt16)Conversion.Val("&h" + Strings.Mid(c, 5, 4));
                                                Mx_sub.Buf[2] = (UInt16)Conversion.Val("&h" + Strings.Mid(c, 9, 4));
                                                Mx_sub.Buf[3] = (UInt16)Conversion.Val("&h" + Strings.Mid(c, 13, 4));
                                                Mx_sub.Buf[4] = (UInt16)Conversion.Val("&h" + Strings.Right(c, 4));
                                                Devno = ADS01; //先頭デバイス№の算出
                                                c = Conversion.Str(Devno);
                                                size = 5; // 書込みバイト数
                                                //1stＡＧタイプ名アップロード（Ｃｏｍ１）
                                                //Debug.Print "ＡＧタイプ名", Deve, Size, Buf(0); Buf(1); Buf(2); Buf(3); Buf(4)
                                                Ret = Mx_sub.DatSend(Mx_sub.MELSEC_DEV_ZR, Devno, (UInt16)size, Mx_sub.Buf);
                                                if (Ret != 0)
                                                {
                                                    Mx_sub.Com1_CLOSE();
                                                    return Ret;
                                                }
                                                ADS01 += 275;
                                            }

                                            // バックグラウンドの進捗処理更新
                                            ctl.ReportProgress(100, new CReadState(i, rcnt, "レシピ詳細部(ＬＨ側）アップロード中"));
                                        }

                                        // バックグラウンドの進捗処理更新
                                        ctl.ReportProgress(100, new CReadState(0, rcnt, "レシピ詳細部(ＲＨ側）アップロード中"));

                                        // 詳細部アップロード処理(２ｎｄ側）
                                        //Com_Msg1 = "レシピ詳細部(２ｎｄ側）アップロード中"
                                        //MSFlexGrid1.Col = 1    列の指定
                                        ADS01 = 10270;          // 書込み先頭アドレス初期値
                                        for (i = 1; i <= rcnt; i++)
                                        {  // 行のループ
                                            if (flexGrid.GetData(i, 1) != null)
                                            {   // 識別コードが有る時
                                                DOWN_LOAD_DATA3(flexGrid.GetData(i, 1).ToString(), Rpatan);  // 転送データの読込み
                                                for (j = 1; j <= 16; j++)
                                                { // ステーションのループ
                                                    switch (j)
                                                    {
                                                        case 1:
                                                        case 2:
                                                        case 3:
                                                        case 4:
                                                        case 5:
                                                        case 6:
                                                        case 7:
                                                        case 8:
                                                        case 9:
                                                        case 10:
                                                        case 11:
                                                            DAT = Strings.Left(TBL01[j] + "0000000000000000000000000000000000000000", 40);
                                                            break;

                                                        case 12:
                                                        case 13:
                                                            DAT = TBL01[j] + "0000000000000000000000000000000000000000";
                                                            DAT = Strings.Left(DAT + "0000000000000000000000000000000000000000", 80);
                                                            break;
                                                        case 14:
                                                            DAT = TBL01[j] + "0000000000000000000000000000000000000000";
                                                            DAT += "0000000000000000000000000000000000000000";
                                                            DAT = Strings.Left(DAT + "00000000000000000000", 100);
                                                            break;
                                                        case 15:
                                                        case 16:
                                                            DAT = Strings.Left(TBL01[j] + "0000000000000000000000000000000000000000", 40);
                                                            break;
                                                    }
                                                    j2 = 0;
                                                    for (j1 = 1; j1 <= Strings.Len(DAT); j1 += 4)
                                                    {
                                                        Mx_sub.Buf[j2] = (UInt16)Conversion.Val("&h" + Strings.Mid(DAT, j1, 4));
                                                        j2 += 1;
                                                    }

                                                    Devno = ADS01;  // 先頭デバイス№の算出
                                                    c = Conversion.Str(Devno);
                                                    size = Strings.Len(DAT) / 4;    // 書込みワード数
                                                    // '２ｎｄ側詳細アップロード（Ｃｏｍ１）
                                                    //Debug.Print "詳細", Deve, Size, DAT
                                                    Ret = Mx_sub.DatSend(Mx_sub.MELSEC_DEV_ZR, Devno, (UInt16)size, Mx_sub.Buf);
                                                    if (Ret != 0)
                                                    {
                                                        // イーサーネットポートを閉じる
                                                        Mx_sub.Com1_CLOSE();
                                                        return Ret;
                                                    }
                                                    switch (j)
                                                    {
                                                        case 1:
                                                        case 2:
                                                        case 3:
                                                        case 4:
                                                        case 5:
                                                        case 6:
                                                        case 7:
                                                        case 8:
                                                        case 9:
                                                        case 10:
                                                        case 11:
                                                            ADS01 += 10;
                                                            break;

                                                        case 12:
                                                        case 13:
                                                            ADS01 += 20;
                                                            break;

                                                        case 14:
                                                            ADS01 += 25;
                                                            break;
                                                        case 15:
                                                        case 16:
                                                            ADS01 += 10;
                                                            break;

                                                    }
                                                }


                                                // 2nd仕様設定部
                                                DAT = TBL01[0] + "0000000000000000000000000000000000000000";
                                                DAT = Strings.Left(DAT + "0000000000000000000000000000000000000000", 80);
                                                j2 = 0;
                                                for (j1 = 1; j1 <= Strings.Len(DAT); j1 += 4)
                                                {
                                                    Mx_sub.Buf[j2] = (UInt16)Conversion.Val("&h" + Strings.Mid(DAT, j1, 4));
                                                    j2 += 1;
                                                }
                                                Devno = ADS01;  // 先頭デバイス№の算出
                                                c = Conversion.Str(Devno);
                                                size = Strings.Len(DAT) / 4; // 書込みバイト数
                                                // 2nd仕様アップロード（Ｃｏｍ１）
                                                //Debug.Print "仕様部", Deve, Size, DAT
                                                Ret = Mx_sub.DatSend(Mx_sub.MELSEC_DEV_ZR, Devno, (UInt16)size, Mx_sub.Buf);
                                                if (Ret != 0)
                                                {
                                                    // イーサーネットポートを閉じる
                                                    Mx_sub.Com1_CLOSE();
                                                    return Ret;
                                                }

                                                ADS01 += 30;
                                                c = Mx_sub.WcovAsc(AgNeme, 10);    //ＡＧタイプ名をアスキー変換
                                                Mx_sub.Buf[0] = (UInt16)Conversion.Val("&h" + Strings.Left(c, 4));
                                                Mx_sub.Buf[1] = (UInt16)Conversion.Val("&h" + Strings.Mid(c, 5, 4));
                                                Mx_sub.Buf[2] = (UInt16)Conversion.Val("&h" + Strings.Mid(c, 9, 4));
                                                Mx_sub.Buf[3] = (UInt16)Conversion.Val("&h" + Strings.Mid(c, 13, 4));
                                                Mx_sub.Buf[4] = (UInt16)Conversion.Val("&h" + Strings.Right(c, 4));
                                                Devno = ADS01; //先頭デバイス№の算出
                                                c = Conversion.Str(Devno);
                                                size = 5; // 書込みバイト数
                                                //2ndＡＧタイプ名アップロード（Ｃｏｍ１）
                                                //Debug.Print "ＡＧタイプ名", Deve, Size, Buf(0); Buf(1); Buf(2); Buf(3); Buf(4)
                                                Ret = Mx_sub.DatSend(Mx_sub.MELSEC_DEV_ZR, Devno, (UInt16)size, Mx_sub.Buf);
                                                if (Ret != 0)
                                                {
                                                    Mx_sub.Com1_CLOSE();
                                                    return Ret;
                                                }
                                                ADS01 += 275;
                                            }

                                            // バックグラウンドの進捗処理更新
                                            ctl.ReportProgress(100, new CReadState(i, rcnt, "レシピ詳細部(ＲＨ側）アップロード中"));
                                        }
                                    }

                                    break;
                            }

                            Mx_sub.Buf[0] = (UInt16)Conversion.Val("0002");
                            Ret = Mx_sub.DatSend(Mx_sub.MELSEC_DEV_ZR, 5, 1, Mx_sub.Buf);   // ＰＬＣ書込み中フラグ
                            if (Ret != 0)
                            {
                                // イーサーネットポートを閉じる
                                Mx_sub.Com1_CLOSE();
                                return Ret;
                            }
                        }
                        else
                        {
                            // イーサーネットポートを閉じる
                            Mx_sub.Com1_CLOSE();
                            return Ret;
                        }
                    }
                    else
                    {
                        // 自動運転中の為転送できません。
                        return Mx_sub.PLC_ERR.ERR_0400;
                    }
                }
                else
                {
                    // イーサーネットポートを閉じる
                    Mx_sub.Com1_CLOSE();
                    return Ret;
                }

                Ret = Mx_sub.Com1_CLOSE(); // イーサーネットポートを閉じる
            }

            return Ret;
        }

        //[2026/04/06][p.hoi][Add]=============================================================>>
        //  PLCデータ読込、照合追加
        //---------------------------------------------------------------------------------------
        /// <summary>
        /// PLCから読込み処理
        /// </summary>
        private Mx_sub.PLC_ERR PlcRecv(BackgroundWorker ctl, int patn, Mx_sub.TAG_INFO[] tagdat, Mx_sub.VEHICLE_INFO[] vehdat)
        {
            UInt32 Devno;
            Mx_sub.PLC_ERR Ret;
            IntPtr ptr;

            ptr = IntPtr.Zero;
            Ret = Mx_sub.Com1_OPEN(); // イーサーネットポートを開く

            if (Mx_sub.IsConnect() == true)
            {
                // タグ情報を読込み
                {
                    // バックグラウンドの進捗処理更新
                    ctl.ReportProgress(100, new CReadState(0, 500, "タグ情報読込み中"));

                    // データクリア
                    Mdb_Module.TagInfoClear();

                    Devno = 7000;                       // 先頭アドレス
                    for (int i = 0; i < 500; i++)       // グリッドのループ
                    {
                        // データクリア
                        tagdat[i] = Mx_sub.TAG_INFO.Empty;

                        Ret = Mx_sub.TagInfoRecv(Mx_sub.MELSEC_DEV_ZR, Devno, ref tagdat[i]);
                        if (Ret != 0)
                        {
                            // イーサーネットポートを閉じる
                            Mx_sub.Com1_CLOSE();
                            return Ret;
                        }

                        if (tagdat[i].IsEmpty() != true)
                        {
                            //データベース書込み
                            Mdb_Module.TagInfoWrite((i + 1), tagdat[i]);
                        }

                        Devno += 5;   // 先頭アドレス

                        // バックグラウンドの進捗処理更新
                        ctl.ReportProgress(100, new CReadState((i + 1), 500, "タグ情報読込み中"));
                    }
                }

                // 車種情報を読込み
                {
                    // バックグラウンドの進捗処理更新
                    ctl.ReportProgress(100, new CReadState(0, 980, "車種情報読込み中"));

                    // データクリア
                    Mdb_Module.Vehicle_T_Clear();
                    Mdb_Module.Vehicle_M_Clear();

                    Devno = 10000;                      // 先頭アドレス
                    for (int i = 0; i < 980; i++)      // グリッドのループ
                    {
                        // データクリア
                        vehdat[i] = Mx_sub.VEHICLE_INFO.Empty;

                        Ret = Mx_sub.VehicleInfoRecv(Mx_sub.MELSEC_DEV_ZR, Devno, ref vehdat[i]);
                        if (Ret != 0)
                        {
                            // イーサーネットポートを閉じる
                            Mx_sub.Com1_CLOSE();
                            return Ret;
                        }

                        if (vehdat[i].IsEmpty() != true)
                        {
                            // データベース書込み
                            Mdb_Module.Vehicle_T_Write(patn, vehdat[i]);      // 車種情報Ｔ
                            Mdb_Module.Vehicle_M_Write(vehdat[i]);            // 車種情報Ｍ
                        }

                        Devno += 500;   // 先頭アドレス

                        // バックグラウンドの進捗処理更新
                        ctl.ReportProgress(100, new CReadState((i + 1), 980, "車種情報読込み中"));
                    }

                    // バックグラウンドの進捗処理更新
                    ctl.ReportProgress(100, new CReadState(980, 980, "車種情報読込み中"));
                }


                Ret = Mx_sub.Com1_CLOSE(); // イーサーネットポートを閉じる
            }

            return Ret;
        }

        /// <summary>
        /// PLCから読込み処理
        /// </summary>
        private Mx_sub.PLC_ERR PlcComp(BackgroundWorker ctl, int patn, ref TAG_COMP tagcomp, ref VEHICLE_COMP vehicelcomp)
        {
            UInt32 Devno;
            Mx_sub.PLC_ERR Ret;

            Ret = Mx_sub.Com1_OPEN(); // イーサーネットポートを開く

            if (Mx_sub.IsConnect() == true)
            {
                // タグ情報を読込み
                {
                    // 初期化
                    tagcomp = TAG_COMP.Empty;

                    // データベースから読込み
                    Mdb_Module.TagInfoRead(ref tagcomp.ScrDat);

                    // バックグラウンドの進捗処理更新
                    ctl.ReportProgress(100, new CReadState(0, 500, "タグ情報照合中"));

                    // PLCから読込み
                    {
                        // タグ情報バッファ
                        Mx_sub.TAG_INFO tagdat = new Mx_sub.TAG_INFO(null);

                        Devno = 7000;                       // 先頭アドレス
                        for (int i = 0; i < 500; i++)       // グリッドのループ
                        {
                            // データクリア
                            tagdat = Mx_sub.TAG_INFO.Empty;

                            Ret = Mx_sub.TagInfoRecv(Mx_sub.MELSEC_DEV_ZR, Devno, ref tagdat);
                            if (Ret != 0)
                            {
                                // イーサーネットポートを閉じる
                                Mx_sub.Com1_CLOSE();
                                return Ret;
                            }

                            if (tagdat.IsEmpty() != true)
                            {
                                // データコーピ
                                tagcomp.DstDat[i] = new Mx_sub.TAG_INFO(tagdat);
                            }

                            // 先頭アドレス
                            Devno += 5;


                            // バックグラウンドの進捗処理更新
                            ctl.ReportProgress(100, new CReadState((i + 1), 500, "タグ情報照合中"));
                        }
                    }
                }

                // 車種情報を読込み
                {
                    // データベースから読込み
                    Mdb_Module.Vehicle_T_Read(patn, ref vehicelcomp.SrcDat);

                    // バックグラウンドの進捗処理更新
                    ctl.ReportProgress(100, new CReadState(0, 980, "車種情報照合中"));

                    // PLCから読込み
                    {
                        // 車種情報バッファ
                        Mx_sub.VEHICLE_INFO vehdat = new Mx_sub.VEHICLE_INFO(null);

                        Devno = 10000;                      // 先頭アドレス
                        for (int i = 0; i < 980; i++)      // グリッドのループ
                        {
                            // データクリア
                            vehdat = Mx_sub.VEHICLE_INFO.Empty;

                            Ret = Mx_sub.VehicleInfoRecv(Mx_sub.MELSEC_DEV_ZR, Devno, ref vehdat);
                            if (Ret != 0)
                            {
                                // イーサーネットポートを閉じる
                                Mx_sub.Com1_CLOSE();
                                return Ret;
                            }

                            if (vehdat.IsEmpty() != true)
                            {
                                // データコーピ
                                vehicelcomp.DstDat[i] = new Mx_sub.VEHICLE_INFO(vehdat);
                            }

                            Devno += 500;   // 先頭アドレス

                            // バックグラウンドの進捗処理更新
                            ctl.ReportProgress(100, new CReadState((i + 1), 980, "車種情報照合中"));
                        }

                        // バックグラウンドの進捗処理更新
                        ctl.ReportProgress(100, new CReadState(980, 980, "車種情報照合中"));
                    }
                }

                Ret = Mx_sub.Com1_CLOSE(); // イーサーネットポートを閉じる

                // 識別コードを並び替えて整列する
                SortByCode(ref vehicelcomp);
            }

            return Ret;
        }

        // ＰＬＣから読込みデータチェック
        private bool PlcRecvChk(Mx_sub.TAG_INFO[] tagdat, Mx_sub.VEHICLE_INFO[] vehdat, ref List<(string, string)> list)
        {
            // タグ情報
            for (int ii = 0; ii < tagdat.Length; ii++)
            {
                if (tagdat[ii].IsEmpty() != true)
                {
                    // 治具タイプ
                    if (Danpre_Xls_sub.IsMasterItem(Danpre_Xls_sub.M_ID.JigType, tagdat[ii].JigType) != true)
                    {
                        list.Add((string.Format("タグNo.：“{0}”", (ii + 1)), string.Format("治具タイプ：“{0}”", Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.JigType, tagdat[ii].JigType))));
                    }
                    // 治具 LH/RH
                    if (Danpre_Xls_sub.IsMasterItem(Danpre_Xls_sub.M_ID.JigLR, tagdat[ii].JigLR) != true)
                    {
                        list.Add((string.Format("タグNo.：“{0}”", (ii + 1)), string.Format("治具　LH/RH：“{0}”", Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.JigLR, tagdat[ii].JigLR))));
                    }
                    // 治具ｽﾄｯｸ段
                    if (Danpre_Xls_sub.IsMasterItem(Danpre_Xls_sub.M_ID.JigStock, tagdat[ii].JigStock) != true)
                    {
                        list.Add((string.Format("タグNo.：“{0}”", (ii + 1)), string.Format("治具ストック段：“{0}”", Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.JigStock, tagdat[ii].JigStock))));
                    }
                }
            }

            // 車種情報
            for (int ii = 0; ii < vehdat.Length; ii++)
            {
                if (vehdat[ii].IsEmpty() != true)
                {
                    // 治具タイプ
                    if (Danpre_Xls_sub.IsMasterItem(Danpre_Xls_sub.M_ID.JigType, vehdat[ii].Head.JigType) != true)
                    {
                        list.Add((string.Format("識別コード：“{0}”", vehdat[ii].Head.IdenCode), string.Format("治具タイプ：“{0}”", Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.JigType, vehdat[ii].Head.JigType))));
                    }
                    // 治具切出し順
                    if (Danpre_Xls_sub.IsMasterItem(Danpre_Xls_sub.M_ID.JigOrder, vehdat[ii].Head.JigOrder) != true)
                    {
                        list.Add((string.Format("識別コード：“{0}”", vehdat[ii].Head.IdenCode), string.Format("治具切出し順：“{0}”", Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.JigOrder, vehdat[ii].Head.JigOrder))));
                    }
                    // １ｓｔ治具ｽﾄｯｸ段
                    if (Danpre_Xls_sub.IsMasterItem(Danpre_Xls_sub.M_ID.JigStock, vehdat[ii].Head.Jig1st) != true)
                    {
                        list.Add((string.Format("識別コード：“{0}”", vehdat[ii].Head.IdenCode), string.Format("１ｓｔ治具ストック段：“{0}”", Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.JigStock, vehdat[ii].Head.Jig1st))));
                    }
                    // ２ｎｄ治具ｽﾄｯｸ段
                    if (Danpre_Xls_sub.IsMasterItem(Danpre_Xls_sub.M_ID.JigStock, vehdat[ii].Head.Jig2nd) != true)
                    {
                        list.Add((string.Format("識別コード：“{0}”", vehdat[ii].Head.IdenCode), string.Format("２ｎｄ治具ストック段：“{0}”", Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.JigStock, vehdat[ii].Head.Jig2nd))));
                    }
                    // 向先
                    if (Danpre_Xls_sub.IsMasterItem(Danpre_Xls_sub.M_ID.Dest, vehdat[ii].Head.Dest) != true)
                    {
                        list.Add((string.Format("識別コード：“{0}”", vehdat[ii].Head.IdenCode), string.Format("向先：“{0}”", Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Dest, vehdat[ii].Head.Dest))));
                    }
                    // ＬＨ側シートタイプ
                    if (Danpre_Xls_sub.IsMasterItem(Danpre_Xls_sub.M_ID.SeatType, vehdat[ii].LhDat.SeatType) != true)
                    {
                        list.Add((string.Format("識別コード：“{0}”", vehdat[ii].Head.IdenCode), string.Format("ＬＨ側のシートタイプ：“{0}”", Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.SeatType, vehdat[ii].LhDat.SeatType))));
                    }
                    // ＬＨ側AGﾀｲﾌﾟ
                    if (Danpre_Xls_sub.IsMasterItem(Danpre_Xls_sub.M_ID.AgType, vehdat[ii].LhDat.AgType) != true)
                    {
                        list.Add((string.Format("識別コード：“{0}”", vehdat[ii].Head.IdenCode), string.Format("ＬＨ側のＡＧタイプ：“{0}”", Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.AgType, vehdat[ii].LhDat.AgType))));
                    }
                    // ＬＨ側ﾋｰﾀｰ
                    if (Danpre_Xls_sub.IsMasterItem(Danpre_Xls_sub.M_ID.Heater, vehdat[ii].LhDat.Heater) != true)
                    {
                        list.Add((string.Format("識別コード：“{0}”", vehdat[ii].Head.IdenCode), string.Format("ＬＨ側のヒーター：“{0}”", Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Heater, vehdat[ii].LhDat.Heater))));
                    }

                    // ＬＨ側ﾊﾞｯｸﾙ
                    if (Danpre_Xls_sub.IsMasterItem(Danpre_Xls_sub.M_ID.Buckle, vehdat[ii].LhDat.Buckle) != true)
                    {
                        list.Add((string.Format("識別コード：“{0}”", vehdat[ii].Head.IdenCode), string.Format("ＬＨ側のバックル：“{0}”", Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Buckle, vehdat[ii].LhDat.Buckle))));
                    }
                    // ＬＨ側ﾍｯﾄﾞﾚｽﾄ
                    if (Danpre_Xls_sub.IsMasterItem(Danpre_Xls_sub.M_ID.Headrest, vehdat[ii].LhDat.Headrest) != true)
                    {
                        list.Add((string.Format("識別コード：“{0}”", vehdat[ii].Head.IdenCode), string.Format("ＬＨ側のヘッドレスト：“{0}”", Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Headrest, vehdat[ii].LhDat.Headrest))));
                    }
                    // ＬＨ着座ｾﾝｻｰ
                    if (Danpre_Xls_sub.IsMasterItem(Danpre_Xls_sub.M_ID.SeatSen, vehdat[ii].LhDat.SeatSen) != true)
                    {
                        list.Add((string.Format("識別コード：“{0}”", vehdat[ii].Head.IdenCode), string.Format("ＬＨ側の着座センサー：“{0}”", Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.SeatSen, vehdat[ii].LhDat.SeatSen))));
                    }
                    // ＬＨ側表皮材
                    if (Danpre_Xls_sub.IsMasterItem(Danpre_Xls_sub.M_ID.Upholstery, vehdat[ii].LhDat.Upholstery) != true)
                    {
                        list.Add((string.Format("識別コード：“{0}”", vehdat[ii].Head.IdenCode), string.Format("ＬＨ側の表皮材：“{0}”", Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Upholstery, vehdat[ii].LhDat.Upholstery))));
                    }
                    // ＬＨ側ﾄﾙｸﾚﾝﾁ(色)
                    if (Danpre_Xls_sub.IsMasterItem(Danpre_Xls_sub.M_ID.TorqueWrench, vehdat[ii].LhDat.TorqueWrench) != true)
                    {
                        list.Add((string.Format("識別コード：“{0}”", vehdat[ii].Head.IdenCode), string.Format("ＬＨ側のトルクレンチ：“{0}”", Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.TorqueWrench, vehdat[ii].LhDat.TorqueWrench))));
                    }
                    // ＬＨ側空調
                    if (Danpre_Xls_sub.IsMasterItem(Danpre_Xls_sub.M_ID.AirCond, vehdat[ii].LhDat.AirCond) != true)
                    {
                        list.Add((string.Format("識別コード：“{0}”", vehdat[ii].Head.IdenCode), string.Format("ＬＨ側の空調：“{0}”", Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.AirCond, vehdat[ii].LhDat.AirCond))));
                    }
                    // ＬＨ側　ﾗﾝﾊﾞｰ      
                    if (Danpre_Xls_sub.IsMasterItem(Danpre_Xls_sub.M_ID.Lumbar, vehdat[ii].LhDat.Lumbar) != true)
                    {
                        list.Add((string.Format("識別コード：“{0}”", vehdat[ii].Head.IdenCode), string.Format("ＬＨ側のランバー：“{0}”", Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Lumbar, vehdat[ii].LhDat.Lumbar))));
                    }
                    // ＬＨ側　背面ﾎﾟｹｯﾄ
                    if (Danpre_Xls_sub.IsMasterItem(Danpre_Xls_sub.M_ID.BackPock, vehdat[ii].LhDat.BackPock) != true)
                    {
                        list.Add((string.Format("識別コード：“{0}”", vehdat[ii].Head.IdenCode), string.Format("ＬＨ側の背面ﾎﾟｹｯﾄ：“{0}”", Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.BackPock, vehdat[ii].LhDat.BackPock))));
                    }
                    // ＬＨ側ISO-FIX
                    if (Danpre_Xls_sub.IsMasterItem(Danpre_Xls_sub.M_ID.ISOFIX, vehdat[ii].LhDat.ISOFIX) != true)
                    {
                        list.Add((string.Format("識別コード：“{0}”", vehdat[ii].Head.IdenCode), string.Format("ＬＨ側のＩＳＯ－FIX：“{0}”", Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.ISOFIX, vehdat[ii].LhDat.ISOFIX))));
                    }
                    // ＬＨ側ﾌｯﾄｳｴﾙﾗﾝﾌﾟ
                    if (Danpre_Xls_sub.IsMasterItem(Danpre_Xls_sub.M_ID.FootwellLamp, vehdat[ii].LhDat.FootwellLamp) != true)
                    {
                        list.Add((string.Format("識別コード：“{0}”", vehdat[ii].Head.IdenCode), string.Format("ＬＨ側のフットウェルランプ：“{0}”", Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.FootwellLamp, vehdat[ii].LhDat.FootwellLamp))));
                    }
                    // ＬＨ側 ｱｰﾑﾚｽﾄ
                    if (Danpre_Xls_sub.IsMasterItem(Danpre_Xls_sub.M_ID.ArmRest, vehdat[ii].LhDat.ArmRest) != true)
                    {
                        list.Add((string.Format("識別コード：“{0}”", vehdat[ii].Head.IdenCode), string.Format("ＬＨ側のアームレスト：“{0}”", Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.ArmRest, vehdat[ii].LhDat.ArmRest))));
                    }
                    // ＬＨ側ｺﾝﾋﾞﾆﾌｯｸ
                    if (Danpre_Xls_sub.IsMasterItem(Danpre_Xls_sub.M_ID.ConvHook, vehdat[ii].LhDat.ConvHook) != true)
                    {
                        list.Add((string.Format("識別コード：“{0}”", vehdat[ii].Head.IdenCode), string.Format("ＬＨ側のコンビニフック：“{0}”", Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.ConvHook, vehdat[ii].LhDat.ConvHook))));
                    }
                    // ＬＨ側QRG
                    if (Danpre_Xls_sub.IsMasterItem(Danpre_Xls_sub.M_ID.QRG, vehdat[ii].LhDat.QRG) != true)
                    {
                        list.Add((string.Format("識別コード：“{0}”", vehdat[ii].Head.IdenCode), string.Format("ＬＨ側のQRG：“{0}”", Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.QRG, vehdat[ii].LhDat.QRG))));
                    }
                    // ＬＨ側サイトデーブル
                    if (Danpre_Xls_sub.IsMasterItem(Danpre_Xls_sub.M_ID.Site_Table, vehdat[ii].LhDat.Site_Table) != true)
                    {
                        list.Add((string.Format("識別コード：“{0}”", vehdat[ii].Head.IdenCode), string.Format("ＬＨ側のサイトテーブル：“{0}”", Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Site_Table, vehdat[ii].LhDat.Site_Table))));
                    }
                    // ＬＨ側ロボット
                    if (Danpre_Xls_sub.IsMasterItem(Danpre_Xls_sub.M_ID.Robot, vehdat[ii].LhDat.Robot) != true)
                    {
                        list.Add((string.Format("識別コード：“{0}”", vehdat[ii].Head.IdenCode), string.Format("ＬＨ側のロボット：“{0}”", Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Robot, vehdat[ii].LhDat.Robot))));
                    }
                    // ＬＨ側バックポード
                    if (Danpre_Xls_sub.IsMasterItem(Danpre_Xls_sub.M_ID.Backboard, vehdat[ii].LhDat.Backboard) != true)
                    {
                        list.Add((string.Format("識別コード：“{0}”", vehdat[ii].Head.IdenCode), string.Format("ＬＨ側のバックボード：“{0}”", Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Backboard, vehdat[ii].LhDat.Backboard))));
                    }
                    // ＬＨ側オットマン
                    if (Danpre_Xls_sub.IsMasterItem(Danpre_Xls_sub.M_ID.Ottoman, vehdat[ii].LhDat.Ottoman) != true)
                    {
                        list.Add((string.Format("識別コード：“{0}”", vehdat[ii].Head.IdenCode), string.Format("ＬＨ側のオットマン：“{0}”", Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Ottoman, vehdat[ii].LhDat.Ottoman))));
                    }


                    // ＲＨ側シートタイプ
                    if (Danpre_Xls_sub.IsMasterItem(Danpre_Xls_sub.M_ID.SeatType, vehdat[ii].RhDat.SeatType) != true)
                    {
                        list.Add((string.Format("識別コード：“{0}”", vehdat[ii].Head.IdenCode), string.Format("ＲＨ側のシートタイプ：“{0}”", Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.SeatType, vehdat[ii].RhDat.SeatType))));
                    }
                    // ＲＨ側AGﾀｲﾌﾟ
                    if (Danpre_Xls_sub.IsMasterItem(Danpre_Xls_sub.M_ID.AgType, vehdat[ii].RhDat.AgType) != true)
                    {
                        list.Add((string.Format("識別コード：“{0}”", vehdat[ii].Head.IdenCode), string.Format("ＲＨ側のＡＧタイプ：“{0}”", Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.AgType, vehdat[ii].RhDat.AgType))));
                    }
                    // ＲＨ側ﾋｰﾀｰ
                    if (Danpre_Xls_sub.IsMasterItem(Danpre_Xls_sub.M_ID.Heater, vehdat[ii].RhDat.Heater) != true)
                    {
                        list.Add((string.Format("識別コード：“{0}”", vehdat[ii].Head.IdenCode), string.Format("ＲＨ側のヒーター：“{0}”", Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Heater, vehdat[ii].RhDat.Heater))));
                    }

                    // ＲＨ側ﾊﾞｯｸﾙ
                    if (Danpre_Xls_sub.IsMasterItem(Danpre_Xls_sub.M_ID.Buckle, vehdat[ii].RhDat.Buckle) != true)
                    {
                        list.Add((string.Format("識別コード：“{0}”", vehdat[ii].Head.IdenCode), string.Format("ＲＨ側のバックル：“{0}”", Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Buckle, vehdat[ii].RhDat.Buckle))));
                    }
                    // ＲＨ側ﾍｯﾄﾞﾚｽﾄ
                    if (Danpre_Xls_sub.IsMasterItem(Danpre_Xls_sub.M_ID.Headrest, vehdat[ii].RhDat.Headrest) != true)
                    {
                        list.Add((string.Format("識別コード：“{0}”", vehdat[ii].Head.IdenCode), string.Format("ＲＨ側のヘッドレスト：“{0}”", Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Headrest, vehdat[ii].RhDat.Headrest))));
                    }
                    // ＲＨ側表皮材
                    if (Danpre_Xls_sub.IsMasterItem(Danpre_Xls_sub.M_ID.Upholstery, vehdat[ii].RhDat.Upholstery) != true)
                    {
                        list.Add((string.Format("識別コード：“{0}”", vehdat[ii].Head.IdenCode), string.Format("ＲＨ側の表皮材：“{0}”", Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Upholstery, vehdat[ii].RhDat.Upholstery))));
                    }
                    // ＲＨ側ﾄﾙｸﾚﾝﾁ(色)
                    if (Danpre_Xls_sub.IsMasterItem(Danpre_Xls_sub.M_ID.TorqueWrench, vehdat[ii].RhDat.TorqueWrench) != true)
                    {
                        list.Add((string.Format("識別コード：“{0}”", vehdat[ii].Head.IdenCode), string.Format("ＲＨ側のトルクレンチ：“{0}”", Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.TorqueWrench, vehdat[ii].RhDat.TorqueWrench))));
                    }
                    // ＲＨ側空調
                    if (Danpre_Xls_sub.IsMasterItem(Danpre_Xls_sub.M_ID.AirCond, vehdat[ii].LhDat.AirCond) != true)
                    {
                        list.Add((string.Format("識別コード：“{0}”", vehdat[ii].Head.IdenCode), string.Format("ＲＨ 側の空調：“{0}”", Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.AirCond, vehdat[ii].LhDat.AirCond))));
                    }
                    // ＲＨ側　ﾗﾝﾊﾞｰ      
                    if (Danpre_Xls_sub.IsMasterItem(Danpre_Xls_sub.M_ID.Lumbar, vehdat[ii].LhDat.Lumbar) != true)
                    {
                        list.Add((string.Format("識別コード：“{0}”", vehdat[ii].Head.IdenCode), string.Format("ＲＨ側のランバー：“{0}”", Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Lumbar, vehdat[ii].LhDat.Lumbar))));
                    }
                    // ＲＨ側　背面ﾎﾟｹｯﾄ
                    if (Danpre_Xls_sub.IsMasterItem(Danpre_Xls_sub.M_ID.BackPock, vehdat[ii].LhDat.BackPock) != true)
                    {
                        list.Add((string.Format("識別コード：“{0}”", vehdat[ii].Head.IdenCode), string.Format("ＲＨ側の背面ﾎﾟｹｯﾄ：“{0}”", Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.BackPock, vehdat[ii].LhDat.BackPock))));
                    }
                    // ＲＨ側ISO-FIX
                    if (Danpre_Xls_sub.IsMasterItem(Danpre_Xls_sub.M_ID.ISOFIX, vehdat[ii].LhDat.ISOFIX) != true)
                    {
                        list.Add((string.Format("識別コード：“{0}”", vehdat[ii].Head.IdenCode), string.Format("ＲＨ側のＩＳＯ－FIX：“{0}”", Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.ISOFIX, vehdat[ii].LhDat.ISOFIX))));
                    }
                    // ＲＨ側ﾌｯﾄｳｴﾙﾗﾝﾌﾟ
                    if (Danpre_Xls_sub.IsMasterItem(Danpre_Xls_sub.M_ID.FootwellLamp, vehdat[ii].LhDat.FootwellLamp) != true)
                    {
                        list.Add((string.Format("識別コード：“{0}”", vehdat[ii].Head.IdenCode), string.Format("ＲＨ側のフットウェルランプ：“{0}”", Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.FootwellLamp, vehdat[ii].LhDat.FootwellLamp))));
                    }
                    // ＲＨ側 ｱｰﾑﾚｽﾄ
                    if (Danpre_Xls_sub.IsMasterItem(Danpre_Xls_sub.M_ID.ArmRest, vehdat[ii].LhDat.ArmRest) != true)
                    {
                        list.Add((string.Format("識別コード：“{0}”", vehdat[ii].Head.IdenCode), string.Format("ＲＨ側のアームレスト：“{0}”", Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.ArmRest, vehdat[ii].LhDat.ArmRest))));
                    }
                    // ＲＨ側ｺﾝﾋﾞﾆﾌｯｸ
                    if (Danpre_Xls_sub.IsMasterItem(Danpre_Xls_sub.M_ID.ConvHook, vehdat[ii].LhDat.ConvHook) != true)
                    {
                        list.Add((string.Format("識別コード：“{0}”", vehdat[ii].Head.IdenCode), string.Format("ＲＨ側のコンビニフック：“{0}”", Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.ConvHook, vehdat[ii].LhDat.ConvHook))));
                    }
                    // ＲＨ側QRG
                    if (Danpre_Xls_sub.IsMasterItem(Danpre_Xls_sub.M_ID.QRG, vehdat[ii].LhDat.QRG) != true)
                    {
                        list.Add((string.Format("識別コード：“{0}”", vehdat[ii].Head.IdenCode), string.Format("ＲＨ側のQRG：“{0}”", Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.QRG, vehdat[ii].LhDat.QRG))));
                    }
                    // ＲＨ側サイトデーブル
                    if (Danpre_Xls_sub.IsMasterItem(Danpre_Xls_sub.M_ID.Site_Table, vehdat[ii].LhDat.Site_Table) != true)
                    {
                        list.Add((string.Format("識別コード：“{0}”", vehdat[ii].Head.IdenCode), string.Format("ＲＨ側のサイトテーブル：“{0}”", Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Site_Table, vehdat[ii].LhDat.Site_Table))));
                    }
                    // ＲＨ側ロボット
                    if (Danpre_Xls_sub.IsMasterItem(Danpre_Xls_sub.M_ID.Robot, vehdat[ii].LhDat.Robot) != true)
                    {
                        list.Add((string.Format("識別コード：“{0}”", vehdat[ii].Head.IdenCode), string.Format("ＲＨ側のロボット：“{0}”", Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Robot, vehdat[ii].LhDat.Robot))));
                    }
                    // ＲＨ側バックポード
                    if (Danpre_Xls_sub.IsMasterItem(Danpre_Xls_sub.M_ID.Backboard, vehdat[ii].LhDat.Backboard) != true)
                    {
                        list.Add((string.Format("識別コード：“{0}”", vehdat[ii].Head.IdenCode), string.Format("ＲＨ側のバックボード：“{0}”", Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Backboard, vehdat[ii].LhDat.Backboard))));
                    }
                    // ＲＨ側オットマン
                    if (Danpre_Xls_sub.IsMasterItem(Danpre_Xls_sub.M_ID.Ottoman, vehdat[ii].LhDat.Ottoman) != true)
                    {
                        list.Add((string.Format("識別コード：“{0}”", vehdat[ii].Head.IdenCode), string.Format("ＲＨ側のオットマン：“{0}”", Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Ottoman, vehdat[ii].LhDat.Ottoman))));
                    }


                }
            }

            if (list.Count > 0) return false;
            return true;
        }

        // 識別コードを並び替えて整列する
        private static void SortByCode(ref VEHICLE_COMP buf)
        {
            try
            {
                int i;

                // 有効なデータを取得する
                var list1 = buf.SrcDat.Where(x => !(x.IsEmpty()));
                var list2 = buf.DstDat.Where(x => !(x.IsEmpty()));

                //キー作成
                var keys = list1.Select(x => x.Head.IdenCode)
                                .Union(list2.Select(x => x.Head.IdenCode))
                                .OrderBy(x => x)
                                .ToList();

                var dict1 = list1.ToDictionary(x => x.Head.IdenCode, x => x);
                var dict2 = list2.ToDictionary(x => x.Head.IdenCode, x => x);

                // 新しいバッファ作成
                Mx_sub.VEHICLE_INFO[] newSrc = new Mx_sub.VEHICLE_INFO[buf.SrcDat.Length];
                Mx_sub.VEHICLE_INFO[] newDst = new Mx_sub.VEHICLE_INFO[buf.DstDat.Length];
                // 初期化
                for (i = 0; i < newSrc.Length; i++)
                {
                    newSrc[i] = Mx_sub.VEHICLE_INFO.Empty;
                    newDst[i] = Mx_sub.VEHICLE_INFO.Empty;
                }

                // 並び結果セット
                i = 0;
                foreach (var key in keys)
                {
                    if (i >= newSrc.Length) break;

                    newSrc[i] = dict1.ContainsKey(key) ? dict1[key] : Mx_sub.VEHICLE_INFO.Empty;
                    newDst[i] = dict2.ContainsKey(key) ? dict2[key] : Mx_sub.VEHICLE_INFO.Empty;

                    i++;
                }

                // データコーピ
                Array.Copy(newSrc, buf.SrcDat, buf.SrcDat.Length);
                Array.Copy(newDst, buf.DstDat, buf.DstDat.Length);
            }
            catch
            {

            }
        }

        /***********************************************************************
            タグ情報照合結果構造体
        ***********************************************************************/
        public struct TAG_COMP
        {
            public Mx_sub.TAG_INFO[] ScrDat;        // 照合元(ファイル)
            public Mx_sub.TAG_INFO[] DstDat;        // 照合先(ＰＬＣ)

            // コンストラクタ
            public TAG_COMP(Nullable<TAG_COMP> buf)
            {
                ScrDat = new Mx_sub.TAG_INFO[500];
                DstDat = new Mx_sub.TAG_INFO[500];

                if (buf.HasValue == true)
                {
                    // コピーする
                    for (int ii = 0; ii < ScrDat.Length; ii++) { ScrDat[ii] = new Mx_sub.TAG_INFO(buf.Value.ScrDat[ii]); }
                    for (int ii = 0; ii < DstDat.Length; ii++) { DstDat[ii] = new Mx_sub.TAG_INFO(buf.Value.DstDat[ii]); }
                }
                else
                {
                    // クリアする
                    for (int ii = 0; ii < ScrDat.Length; ii++) { ScrDat[ii] = Mx_sub.TAG_INFO.Empty; }
                    for (int ii = 0; ii < DstDat.Length; ii++) { DstDat[ii] = Mx_sub.TAG_INFO.Empty; }
                }
            }

            // 空のバッファを取得する
            public static TAG_COMP Empty
            {
                get
                {
                    return (new TAG_COMP(null));
                }
            }

            // 照合結果
            public COMP_ID Compare(int idx)
            {
                // 片側のみ存在
                {
                    if (ScrDat[idx].IsEmpty() != DstDat[idx].IsEmpty()) return COMP_ID.ONLYSIDE;
                }

                if ((ScrDat[idx].JigType == DstDat[idx].JigType) &&
                   (ScrDat[idx].JigLR == DstDat[idx].JigLR) &&
                   (ScrDat[idx].JigStock == DstDat[idx].JigStock))
                {
                    return COMP_ID.MATCH;
                }

                return COMP_ID.MISMATCH;
            }
        }

        /***********************************************************************
            タグ情報照合結果構造体
        ***********************************************************************/
        public struct VEHICLE_COMP
        {
            public Mx_sub.VEHICLE_INFO[] SrcDat;        // 照合元(ファイル)
            public Mx_sub.VEHICLE_INFO[] DstDat;        // 照合先(ＰＬＣ)

            // コンストラクタ
            public VEHICLE_COMP(Nullable<VEHICLE_COMP> buf)
            {
                SrcDat = new Mx_sub.VEHICLE_INFO[980 * 2];
                DstDat = new Mx_sub.VEHICLE_INFO[980 * 2];

                if (buf.HasValue == true)
                {
                    // コピーする
                    for (int ii = 0; ii < SrcDat.Length; ii++) { SrcDat[ii] = new Mx_sub.VEHICLE_INFO(buf.Value.SrcDat[ii]); }
                    for (int ii = 0; ii < DstDat.Length; ii++) { DstDat[ii] = new Mx_sub.VEHICLE_INFO(buf.Value.DstDat[ii]); }
                }
                else
                {
                    // クリアする
                    for (int ii = 0; ii < SrcDat.Length; ii++) { SrcDat[ii] = Mx_sub.VEHICLE_INFO.Empty; }
                    for (int ii = 0; ii < DstDat.Length; ii++) { DstDat[ii] = Mx_sub.VEHICLE_INFO.Empty; }
                }
            }

            // 空のバッファを取得する
            public static VEHICLE_COMP Empty
            {
                get
                {
                    return (new VEHICLE_COMP(null));
                }
            }

            // ヘッダ部照合結果
            public COMP_ID HeadCompare(int idx, int datno)
            {
                COMP_ID ret = COMP_ID.NONE;
                if ((SrcDat[idx].Head.IdenCode.Length > 0) != (DstDat[idx].Head.IdenCode.Length > 0))
                {
                    ret = COMP_ID.ONLYSIDE;
                }
                else
                {
                    switch (datno)
                    {
                        case 0:
                            if (string.Compare(SrcDat[idx].Head.IdenCode, DstDat[idx].Head.IdenCode) == 0) { ret = COMP_ID.MATCH; }
                            else { ret = COMP_ID.MISMATCH; }
                            break;
                        case 1:
                            if (SrcDat[idx].Head.JigType == DstDat[idx].Head.JigType) { ret = COMP_ID.MATCH; }
                            else { ret = COMP_ID.MISMATCH; }
                            break;
                        case 2:
                            if (SrcDat[idx].Head.JigOrder == DstDat[idx].Head.JigOrder) { ret = COMP_ID.MATCH; }
                            else { ret = COMP_ID.MISMATCH; }
                            break;
                        case 3:
                            if (SrcDat[idx].Head.Jig1st == DstDat[idx].Head.Jig1st) { ret = COMP_ID.MATCH; }
                            else { ret = COMP_ID.MISMATCH; }
                            break;
                        case 4:
                            if (SrcDat[idx].Head.Jig2nd == DstDat[idx].Head.Jig2nd) { ret = COMP_ID.MATCH; }
                            else { ret = COMP_ID.MISMATCH; }
                            break;
                        case 5:
                            if (SrcDat[idx].Head.Dest == DstDat[idx].Head.Dest) { ret = COMP_ID.MATCH; }
                            else { ret = COMP_ID.MISMATCH; }
                            break;
                    }
                }

                return ret;
            }

            // ＬＨ側詳細部照合結果
            public COMP_ID LHCompare(int idx, int datno)
            {
                COMP_ID ret = COMP_ID.NONE;
                switch (datno)
                {
                    case 0:
                        if (SrcDat[idx].LhDat.SeatType == DstDat[idx].LhDat.SeatType) { ret = COMP_ID.MATCH; }
                        else { ret = COMP_ID.MISMATCH; }
                        break;
                    case 1:
                        if (SrcDat[idx].LhDat.AgType == DstDat[idx].LhDat.AgType) { ret = COMP_ID.MATCH; }
                        else { ret = COMP_ID.MISMATCH; }
                        break;
                    case 2:
                        if (SrcDat[idx].LhDat.Heater == DstDat[idx].LhDat.Heater) { ret = COMP_ID.MATCH; }
                        else { ret = COMP_ID.MISMATCH; }
                        break;
                    case 3:
                        if (SrcDat[idx].LhDat.Buckle == DstDat[idx].LhDat.Buckle) { ret = COMP_ID.MATCH; }
                        else { ret = COMP_ID.MISMATCH; }
                        break;
                    case 4:
                        if (SrcDat[idx].LhDat.Headrest == DstDat[idx].LhDat.Headrest) { ret = COMP_ID.MATCH; }
                        else { ret = COMP_ID.MISMATCH; }
                        break;
                    case 5:
                        if (SrcDat[idx].LhDat.Upholstery == DstDat[idx].LhDat.Upholstery) { ret = COMP_ID.MATCH; }
                        else { ret = COMP_ID.MISMATCH; }
                        break;
                    case 6:
                        if (SrcDat[idx].LhDat.TorqueWrench == DstDat[idx].LhDat.TorqueWrench) { ret = COMP_ID.MATCH; }
                        else { ret = COMP_ID.MISMATCH; }
                        break;
                    case 7:
                        if (SrcDat[idx].LhDat.Lumbar == DstDat[idx].LhDat.Lumbar) { ret = COMP_ID.MATCH; }
                        else { ret = COMP_ID.MISMATCH; }
                        break;
                    case 8:
                        if (SrcDat[idx].LhDat.BackPock == DstDat[idx].LhDat.BackPock) { ret = COMP_ID.MATCH; }
                        else { ret = COMP_ID.MISMATCH; }
                        break;
                    case 9:
                        if (SrcDat[idx].LhDat.FootwellLamp == DstDat[idx].LhDat.FootwellLamp) { ret = COMP_ID.MATCH; }
                        else { ret = COMP_ID.MISMATCH; }
                        break;
                    case 10:
                        if (SrcDat[idx].LhDat.ArmRest == DstDat[idx].LhDat.ArmRest) { ret = COMP_ID.MATCH; }
                        else { ret = COMP_ID.MISMATCH; }
                        break;
                    case 11:
                        if (SrcDat[idx].LhDat.QRG == DstDat[idx].LhDat.QRG) { ret = COMP_ID.MATCH; }
                        else { ret = COMP_ID.MISMATCH; }
                        break;
                    case 12:
                        if (SrcDat[idx].LhDat.ISOFIX == DstDat[idx].LhDat.ISOFIX) { ret = COMP_ID.MATCH; }
                        else { ret = COMP_ID.MISMATCH; }
                        break;
                    case 13:
                        if (SrcDat[idx].LhDat.ConvHook == DstDat[idx].LhDat.ConvHook) { ret = COMP_ID.MATCH; }
                        else { ret = COMP_ID.MISMATCH; }
                        break;
                    case 14:
                        if (SrcDat[idx].LhDat.Site_Table == DstDat[idx].LhDat.Site_Table) { ret = COMP_ID.MATCH; }
                        else { ret = COMP_ID.MISMATCH; }
                        break;
                    case 15:
                        if (SrcDat[idx].LhDat.Robot == DstDat[idx].LhDat.Robot) { ret = COMP_ID.MATCH; }
                        else { ret = COMP_ID.MISMATCH; }
                        break;
                    case 16:
                        if (SrcDat[idx].LhDat.Backboard == DstDat[idx].LhDat.Backboard) { ret = COMP_ID.MATCH; }
                        else { ret = COMP_ID.MISMATCH; }
                        break;
                    case 17:
                        if (SrcDat[idx].LhDat.Ottoman == DstDat[idx].LhDat.Ottoman) { ret = COMP_ID.MATCH; }
                        else { ret = COMP_ID.MISMATCH; }
                        break;
                    case 18:
                        if (SrcDat[idx].LhDat.SeatSen == DstDat[idx].LhDat.SeatSen) { ret = COMP_ID.MATCH; }
                        else { ret = COMP_ID.MISMATCH; }
                        break;
                    case 19:
                        if (SrcDat[idx].LhDat.AirCond == DstDat[idx].LhDat.AirCond) { ret = COMP_ID.MATCH; }
                        else { ret = COMP_ID.MISMATCH; }
                        break;

                }

                return ret;
            }

            // ＬＨ側詳細部照合結果
            public COMP_ID LHStCompare(int idx, int stno, int stidx)
            {
                COMP_ID ret = COMP_ID.NONE;

                switch (stno)
                {
                    case 1:
                        if (stidx < SrcDat[idx].LhDat.St01.Length)
                        {
                            if (SrcDat[idx].LhDat.St01[stidx] == DstDat[idx].LhDat.St01[stidx]) { ret = COMP_ID.MATCH; }
                            else { ret = COMP_ID.MISMATCH; }
                        }
                        break;
                    case 2:
                        if (stidx < SrcDat[idx].LhDat.St02.Length)
                        {
                            if (SrcDat[idx].LhDat.St02[stidx] == DstDat[idx].LhDat.St02[stidx]) { ret = COMP_ID.MATCH; }
                            else { ret = COMP_ID.MISMATCH; }
                        }
                        break;
                    case 3:
                        if (stidx < SrcDat[idx].LhDat.St03.Length)
                        {
                            if (SrcDat[idx].LhDat.St03[stidx] == DstDat[idx].LhDat.St03[stidx]) { ret = COMP_ID.MATCH; }
                            else { ret = COMP_ID.MISMATCH; }
                        }
                        break;
                    case 4:
                        if (stidx < SrcDat[idx].LhDat.St04.Length)
                        {
                            if (SrcDat[idx].LhDat.St04[stidx] == DstDat[idx].LhDat.St04[stidx]) { ret = COMP_ID.MATCH; }
                            else { ret = COMP_ID.MISMATCH; }
                        }
                        break;
                    case 5:
                        if (stidx < SrcDat[idx].LhDat.St05.Length)
                        {
                            if (SrcDat[idx].LhDat.St05[stidx] == DstDat[idx].LhDat.St05[stidx]) { ret = COMP_ID.MATCH; }
                            else { ret = COMP_ID.MISMATCH; }
                        }
                        break;
                    case 6:
                        if (stidx < SrcDat[idx].LhDat.St06.Length)
                        {
                            if (SrcDat[idx].LhDat.St06[stidx] == DstDat[idx].LhDat.St06[stidx]) { ret = COMP_ID.MATCH; }
                            else { ret = COMP_ID.MISMATCH; }
                        }
                        break;
                    case 7:
                        if (stidx < SrcDat[idx].LhDat.St07.Length)
                        {
                            if (SrcDat[idx].LhDat.St07[stidx] == DstDat[idx].LhDat.St07[stidx]) { ret = COMP_ID.MATCH; }
                            else { ret = COMP_ID.MISMATCH; }
                        }
                        break;
                    case 8:
                        if (stidx < SrcDat[idx].LhDat.St08.Length)
                        {
                            if (SrcDat[idx].LhDat.St08[stidx] == DstDat[idx].LhDat.St08[stidx]) { ret = COMP_ID.MATCH; }
                            else { ret = COMP_ID.MISMATCH; }
                        }
                        break;
                    case 9:
                        if (stidx < SrcDat[idx].LhDat.St09.Length)
                        {
                            if (SrcDat[idx].LhDat.St09[stidx] == DstDat[idx].LhDat.St09[stidx]) { ret = COMP_ID.MATCH; }
                            else { ret = COMP_ID.MISMATCH; }
                        }
                        break;
                    case 10:
                        if (stidx < SrcDat[idx].LhDat.St10.Length)
                        {
                            if (SrcDat[idx].LhDat.St10[stidx] == DstDat[idx].LhDat.St10[stidx]) { ret = COMP_ID.MATCH; }
                            else { ret = COMP_ID.MISMATCH; }
                        }
                        break;
                    case 11:
                        if (stidx < SrcDat[idx].LhDat.St11.Length)
                        {
                            if (SrcDat[idx].LhDat.St11[stidx] == DstDat[idx].LhDat.St11[stidx]) { ret = COMP_ID.MATCH; }
                            else { ret = COMP_ID.MISMATCH; }
                        }
                        break;
                    case 12:
                        if (stidx < SrcDat[idx].LhDat.St12.Length)
                        {
                            if (SrcDat[idx].LhDat.St12[stidx] == DstDat[idx].LhDat.St12[stidx]) { ret = COMP_ID.MATCH; }
                            else { ret = COMP_ID.MISMATCH; }
                        }
                        break;
                    case 13:
                        if (stidx < SrcDat[idx].LhDat.St13.Length)
                        {
                            if (SrcDat[idx].LhDat.St13[stidx] == DstDat[idx].LhDat.St13[stidx]) { ret = COMP_ID.MATCH; }
                            else { ret = COMP_ID.MISMATCH; }
                        }
                        break;
                    case 14:
                        if (stidx < SrcDat[idx].LhDat.St14.Length)
                        {
                            if (SrcDat[idx].LhDat.St14[stidx] == DstDat[idx].LhDat.St14[stidx]) { ret = COMP_ID.MATCH; }
                            else { ret = COMP_ID.MISMATCH; }
                        }
                        break;
                    case 15:
                        if (stidx < SrcDat[idx].LhDat.St15.Length)
                        {
                            if (SrcDat[idx].LhDat.St15[stidx] == DstDat[idx].LhDat.St15[stidx]) { ret = COMP_ID.MATCH; }
                            else { ret = COMP_ID.MISMATCH; }
                        }
                        break;
                    case 16:
                        if (stidx < SrcDat[idx].LhDat.St16.Length)
                        {
                            if (SrcDat[idx].LhDat.St16[stidx] == DstDat[idx].LhDat.St16[stidx]) { ret = COMP_ID.MATCH; }
                            else { ret = COMP_ID.MISMATCH; }
                        }
                        break;
                }

                return ret;
            }

            // ＲＨ側詳細部照合結果
            public COMP_ID RHCompare(int idx, int datno)
            {
                COMP_ID ret = COMP_ID.NONE;
                switch (datno)
                {
                    case 0:
                        if (SrcDat[idx].RhDat.SeatType == DstDat[idx].RhDat.SeatType) { ret = COMP_ID.MATCH; }
                        else { ret = COMP_ID.MISMATCH; }
                        break;
                    case 1:
                        if (SrcDat[idx].RhDat.AgType == DstDat[idx].RhDat.AgType) { ret = COMP_ID.MATCH; }
                        else { ret = COMP_ID.MISMATCH; }
                        break;
                    case 2:
                        if (SrcDat[idx].RhDat.Heater == DstDat[idx].RhDat.Heater) { ret = COMP_ID.MATCH; }
                        else { ret = COMP_ID.MISMATCH; }
                        break;
                    case 3:
                        if (SrcDat[idx].RhDat.Buckle == DstDat[idx].RhDat.Buckle) { ret = COMP_ID.MATCH; }
                        else { ret = COMP_ID.MISMATCH; }
                        break;
                    case 4:
                        if (SrcDat[idx].RhDat.Headrest == DstDat[idx].RhDat.Headrest) { ret = COMP_ID.MATCH; }
                        else { ret = COMP_ID.MISMATCH; }
                        break;
                    case 5:
                        if (SrcDat[idx].RhDat.Upholstery == DstDat[idx].RhDat.Upholstery) { ret = COMP_ID.MATCH; }
                        else { ret = COMP_ID.MISMATCH; }
                        break;
                    case 6:
                        if (SrcDat[idx].RhDat.TorqueWrench == DstDat[idx].RhDat.TorqueWrench) { ret = COMP_ID.MATCH; }
                        else { ret = COMP_ID.MISMATCH; }
                        break;
                    case 7:
                        if (SrcDat[idx].RhDat.Lumbar == DstDat[idx].RhDat.Lumbar) { ret = COMP_ID.MATCH; }
                        else { ret = COMP_ID.MISMATCH; }
                        break;
                    case 8:
                        if (SrcDat[idx].RhDat.BackPock == DstDat[idx].RhDat.BackPock) { ret = COMP_ID.MATCH; }
                        else { ret = COMP_ID.MISMATCH; }
                        break;
                    case 9:
                        if (SrcDat[idx].RhDat.FootwellLamp == DstDat[idx].RhDat.FootwellLamp) { ret = COMP_ID.MATCH; }
                        else { ret = COMP_ID.MISMATCH; }
                        break;
                    case 10:
                        if (SrcDat[idx].RhDat.ArmRest == DstDat[idx].RhDat.ArmRest) { ret = COMP_ID.MATCH; }
                        else { ret = COMP_ID.MISMATCH; }
                        break;
                    case 11:
                        if (SrcDat[idx].RhDat.QRG == DstDat[idx].RhDat.QRG) { ret = COMP_ID.MATCH; }
                        else { ret = COMP_ID.MISMATCH; }
                        break;
                    case 12:
                        if (SrcDat[idx].RhDat.ISOFIX == DstDat[idx].RhDat.ISOFIX) { ret = COMP_ID.MATCH; }
                        else { ret = COMP_ID.MISMATCH; }
                        break;
                    case 13:
                        if (SrcDat[idx].RhDat.ConvHook == DstDat[idx].RhDat.ConvHook) { ret = COMP_ID.MATCH; }
                        else { ret = COMP_ID.MISMATCH; }
                        break;
                    case 14:
                        if (SrcDat[idx].RhDat.Site_Table == DstDat[idx].RhDat.Site_Table) { ret = COMP_ID.MATCH; }
                        else { ret = COMP_ID.MISMATCH; }
                        break;
                    case 15:
                        if (SrcDat[idx].RhDat.Robot == DstDat[idx].RhDat.Robot) { ret = COMP_ID.MATCH; }
                        else { ret = COMP_ID.MISMATCH; }
                        break;
                    case 16:
                        if (SrcDat[idx].RhDat.Backboard == DstDat[idx].RhDat.Backboard) { ret = COMP_ID.MATCH; }
                        else { ret = COMP_ID.MISMATCH; }
                        break;
                    case 17:
                        if (SrcDat[idx].RhDat.Ottoman == DstDat[idx].RhDat.Ottoman) { ret = COMP_ID.MATCH; }
                        else { ret = COMP_ID.MISMATCH; }
                        break;
                    case 18:
                        if (SrcDat[idx].RhDat.SeatSen == DstDat[idx].RhDat.SeatSen) { ret = COMP_ID.MATCH; }
                        else { ret = COMP_ID.MISMATCH; }
                        break;
                    case 19:
                        if (SrcDat[idx].RhDat.AirCond == DstDat[idx].RhDat.AirCond) { ret = COMP_ID.MATCH; }
                        else { ret = COMP_ID.MISMATCH; }
                        break;
                }

                return ret;
            }
            // ＲＨ側詳細部照合結果
            public COMP_ID RHStCompare(int idx, int stno, int stidx)
            {
                COMP_ID ret = COMP_ID.NONE;

                switch (stno)
                {
                    case 1:
                        if (stidx < SrcDat[idx].RhDat.St01.Length)
                        {
                            if (SrcDat[idx].RhDat.St01[stidx] == DstDat[idx].RhDat.St01[stidx]) { ret = COMP_ID.MATCH; }
                            else { ret = COMP_ID.MISMATCH; }
                        }
                        break;
                    case 2:
                        if (stidx < SrcDat[idx].RhDat.St02.Length)
                        {
                            if (SrcDat[idx].RhDat.St02[stidx] == DstDat[idx].RhDat.St02[stidx]) { ret = COMP_ID.MATCH; }
                            else { ret = COMP_ID.MISMATCH; }
                        }
                        break;
                    case 3:
                        if (stidx < SrcDat[idx].RhDat.St03.Length)
                        {
                            if (SrcDat[idx].RhDat.St03[stidx] == DstDat[idx].RhDat.St03[stidx]) { ret = COMP_ID.MATCH; }
                            else { ret = COMP_ID.MISMATCH; }
                        }
                        break;
                    case 4:
                        if (stidx < SrcDat[idx].RhDat.St04.Length)
                        {
                            if (SrcDat[idx].RhDat.St04[stidx] == DstDat[idx].RhDat.St04[stidx]) { ret = COMP_ID.MATCH; }
                            else { ret = COMP_ID.MISMATCH; }
                        }
                        break;
                    case 5:
                        if (stidx < SrcDat[idx].RhDat.St05.Length)
                        {
                            if (SrcDat[idx].RhDat.St05[stidx] == DstDat[idx].RhDat.St05[stidx]) { ret = COMP_ID.MATCH; }
                            else { ret = COMP_ID.MISMATCH; }
                        }
                        break;
                    case 6:
                        if (stidx < SrcDat[idx].RhDat.St06.Length)
                        {
                            if (SrcDat[idx].RhDat.St06[stidx] == DstDat[idx].RhDat.St06[stidx]) { ret = COMP_ID.MATCH; }
                            else { ret = COMP_ID.MISMATCH; }
                        }
                        break;
                    case 7:
                        if (stidx < SrcDat[idx].RhDat.St07.Length)
                        {
                            if (SrcDat[idx].RhDat.St07[stidx] == DstDat[idx].RhDat.St07[stidx]) { ret = COMP_ID.MATCH; }
                            else { ret = COMP_ID.MISMATCH; }
                        }
                        break;
                    case 8:
                        if (stidx < SrcDat[idx].RhDat.St08.Length)
                        {
                            if (SrcDat[idx].RhDat.St08[stidx] == DstDat[idx].RhDat.St08[stidx]) { ret = COMP_ID.MATCH; }
                            else { ret = COMP_ID.MISMATCH; }
                        }
                        break;
                    case 9:
                        if (stidx < SrcDat[idx].RhDat.St09.Length)
                        {
                            if (SrcDat[idx].RhDat.St09[stidx] == DstDat[idx].RhDat.St09[stidx]) { ret = COMP_ID.MATCH; }
                            else { ret = COMP_ID.MISMATCH; }
                        }
                        break;
                    case 10:
                        if (stidx < SrcDat[idx].RhDat.St10.Length)
                        {
                            if (SrcDat[idx].RhDat.St10[stidx] == DstDat[idx].RhDat.St10[stidx]) { ret = COMP_ID.MATCH; }
                            else { ret = COMP_ID.MISMATCH; }
                        }
                        break;
                    case 11:
                        if (stidx < SrcDat[idx].RhDat.St11.Length)
                        {
                            if (SrcDat[idx].RhDat.St11[stidx] == DstDat[idx].RhDat.St11[stidx]) { ret = COMP_ID.MATCH; }
                            else { ret = COMP_ID.MISMATCH; }
                        }
                        break;
                    case 12:
                        if (stidx < SrcDat[idx].RhDat.St12.Length)
                        {
                            if (SrcDat[idx].RhDat.St12[stidx] == DstDat[idx].RhDat.St12[stidx]) { ret = COMP_ID.MATCH; }
                            else { ret = COMP_ID.MISMATCH; }
                        }
                        break;
                    case 13:
                        if (stidx < SrcDat[idx].RhDat.St13.Length)
                        {
                            if (SrcDat[idx].RhDat.St13[stidx] == DstDat[idx].RhDat.St13[stidx]) { ret = COMP_ID.MATCH; }
                            else { ret = COMP_ID.MISMATCH; }
                        }
                        break;
                    case 14:
                        if (stidx < SrcDat[idx].RhDat.St14.Length)
                        {
                            if (SrcDat[idx].RhDat.St14[stidx] == DstDat[idx].RhDat.St14[stidx]) { ret = COMP_ID.MATCH; }
                            else { ret = COMP_ID.MISMATCH; }
                        }
                        break;
                    case 15:
                        if (stidx < SrcDat[idx].RhDat.St15.Length)
                        {
                            if (SrcDat[idx].RhDat.St15[stidx] == DstDat[idx].RhDat.St15[stidx]) { ret = COMP_ID.MATCH; }
                            else { ret = COMP_ID.MISMATCH; }
                        }
                        break;
                    case 16:
                        if (stidx < SrcDat[idx].RhDat.St16.Length)
                        {
                            if (SrcDat[idx].RhDat.St16[stidx] == DstDat[idx].RhDat.St16[stidx]) { ret = COMP_ID.MATCH; }
                            else { ret = COMP_ID.MISMATCH; }
                        }
                        break;
                }

                return ret;
            }
        }
        // 照合結果定義
        public enum COMP_ID
        {
            NONE = 0,
            MATCH,      // 一致
            MISMATCH,   // 不一致
            ONLYSIDE    // 片側のみ存在
        }
        //<<=====================================================================================

        /***********************************************************************
            すべてのコントロールの有効／無効を設定する
        ***********************************************************************/
        private void SetEnableAllControls(Control parent, bool enable)
        {
            foreach (Control control in parent.Controls)
            {
                if (((control is Panel) != true) &&
                    ((control is Label) != true))
                {
                    control.Enabled = enable;
                }

                if ((control is GroupBox) != true)
                {
                    // ｢処理中｣表示以外のすべてのコントロールを再帰的に有効／無効を設定する
                    if (control.Equals(Panel_Progress) != true)
                    {
                        SetEnableAllControls(control, enable);
                    }
                }
            }
        }

        /***********************************************************************
            操作可能か確認する
        ***********************************************************************/
        private bool IsEnabled()
        {
            if (backgroundWork.IsBusy == true)
            {
                // バックグラウンド処理中も｢操作不可｣とする!!
                return (false);
            }

            return (true);
        }

        /***********************************************************************
            バックグラウンドによる読込み処理
        ***********************************************************************/
        // バックグラウンド処理を開始する
        private bool StartBackground(WRK_ID id)
        {
            if (IsEnabled() == true)
            {
                // バックグラウンド処理を実行する
                try
                {
                    // 処理
                    switch (id)
                    {
                        case WRK_ID.PLC_SEND_T:
                        case WRK_ID.PLC_SEND_1:
                        case WRK_ID.PLC_SEND_2:
                        case WRK_ID.PLC_SEND_3:
                        case WRK_ID.PRINT:
                            // バックグラウンド処理を実行する
                            backgroundWork.RunWorkerAsync(new CReadArgs(id, c1FlexGrid1));
                            break;
                        case WRK_ID.PLC_RECV_1:
                        case WRK_ID.PLC_RECV_2:
                        case WRK_ID.PLC_RECV_3:
                        case WRK_ID.PLC_COMP_1:
                        case WRK_ID.PLC_COMP_2:
                        case WRK_ID.PLC_COMP_3:
                            // バックグラウンド処理を実行する
                            backgroundWork.RunWorkerAsync(new CReadArgs(id));
                            break;
                    }


                    ProgressBar1.Value = 0; //プロセス初期化

                    // ｢処理中｣の表示
                    Panel_Progress.Show();
                    Panel_Progress.Update();

                    // すべてのコントロールの無効にする!!
                    SetEnableAllControls(this, false);

                    // 成功!!
                    return (true);
                }
                catch
                {
                    throw;
                }
            }

            return (false);
        }

        /// <summary>
        /// バックグラウンドのメイン処理
        /// </summary>
        private void backgroundWork1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bgrd = sender as BackgroundWorker;
            CReadArgs args = e.Argument as CReadArgs;
            int result = 0;
            Mx_sub.TAG_INFO[] tagdat;
            Mx_sub.VEHICLE_INFO[] vehdat;
            TAG_COMP tagcomp;
            VEHICLE_COMP vehiclecomp;

            // 元のカーソルを保持して待機カーソルに変更
            Cursor preCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            // 初期化
            tagdat = new Mx_sub.TAG_INFO[500];
            for (int ii = 0; ii < tagdat.Length; ii++) { tagdat[ii] = Mx_sub.TAG_INFO.Empty; }
            vehdat = new Mx_sub.VEHICLE_INFO[980];
            for (int ii = 0; ii < vehdat.Length; ii++) { vehdat[ii] = Mx_sub.VEHICLE_INFO.Empty; }
            tagcomp = TAG_COMP.Empty;
            vehiclecomp = VEHICLE_COMP.Empty;

            // 処理
            switch (args.Id)
            {
                case WRK_ID.PLC_SEND_T:
                case WRK_ID.PLC_SEND_1:
                case WRK_ID.PLC_SEND_2:
                case WRK_ID.PLC_SEND_3:
                    // アップロード処理
                    result = (int)PlcDown(bgrd, args.Id, args.FlexGrid);
                    break;
                case WRK_ID.PLC_RECV_1:
                case WRK_ID.PLC_RECV_2:
                case WRK_ID.PLC_RECV_3:
                    // PLCから読込み処理
                    result = (int)PlcRecv(bgrd, (args.Id - WRK_ID.PLC_RECV_1 + 1), tagdat, vehdat);
                    break;
                case WRK_ID.PLC_COMP_1:
                case WRK_ID.PLC_COMP_2:
                case WRK_ID.PLC_COMP_3:
                    // PLCとの照合処理
                    result = (int)PlcComp(bgrd, (args.Id - WRK_ID.PLC_COMP_1 + 1), ref tagcomp, ref vehiclecomp);
                    break;
                case WRK_ID.PRINT:
                    bool rc;

                    // 印刷処理
                    rc = Danpre_Xls_sub.Xls_MF_Set2();                      // 印刷フォームの取得

                    if (rc == true)
                    {
                        Danpre_Xls_sub.Xls_Data_Prt_A(bgrd, c1FlexGrid1);   // 対象データを読込印刷フォームへセットし印刷する
                        Danpre_Xls_sub.Xls_MF_Rset2();                      // 印刷フォームの開放
                    }

                    break;
            }

            // カーソルを元に戻す
            Cursor.Current = preCursor;

            // 処理完了
            switch (args.Id)
            {
                case WRK_ID.PLC_SEND_T:
                case WRK_ID.PLC_SEND_1:
                case WRK_ID.PLC_SEND_2:
                case WRK_ID.PLC_SEND_3:
                case WRK_ID.PRINT:
                    // バックグラウンド処理完了!!
                    e.Result = new CReadResult(args.Id, result);    // 処理結果をセットする
                    break;

                case WRK_ID.PLC_RECV_1:
                case WRK_ID.PLC_RECV_2:
                case WRK_ID.PLC_RECV_3:
                    // バックグラウンド処理完了!!
                    e.Result = new CReadResult(args.Id, result, tagdat, vehdat);    // 処理結果をセットする
                    break;
                case WRK_ID.PLC_COMP_1:
                case WRK_ID.PLC_COMP_2:
                case WRK_ID.PLC_COMP_3:
                    // バックグラウンド処理完了!!
                    e.Result = new CReadResult(args.Id, result, tagcomp, vehiclecomp);    // 処理結果をセットする
                    break;
            }
        }

        /// <summary>
        /// バックグラウンドの進捗処理
        /// </summary>
        private void backgroundWork1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int parcent;
            CReadState state = e.UserState as CReadState;

            // プログレスバーの表示更新
            if ((state.Current != ProgressBar1.Value) ||
                (state.MaxCnt != ProgressBar1.Maximum))
            {
                if (state.MaxCnt == 0)
                {
                    parcent = 100;
                }
                else
                {
                    parcent = 100 * state.Current / state.MaxCnt;
                }

                ProgressBar1.Value = parcent; //全体を１００とした値を返す
            }

            if (state.Msg != labelTile.Text) labelTile.Text = state.Msg;

            Application.DoEvents();       //表示更新のために 画面の制御を返す
        }

        /// <summary>
        /// バックグラウンドの完了処理
        /// </summary>
        private void backgroundWork1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BackgroundWorker bgrd = sender as BackgroundWorker;
            CReadResult result = e.Result as CReadResult;

            // すべてのコントロールの有効にする!!
            SetEnableAllControls(this, true);

            // ｢処理中｣の非表示
            Panel_Progress.Hide();

            // エラー判定
            if (result.Result == 0)
            {
                switch (result.Id)
                {
                    case WRK_ID.PLC_SEND_T:
                    case WRK_ID.PLC_SEND_1:
                    case WRK_ID.PLC_SEND_2:
                    case WRK_ID.PLC_SEND_3:
                        Program.MessageBox("アップロード正常完了");

                        break;
                    //[2026/04/06][p.hoi][Add]=============================================================>>
                    //  PLCデータ読込、照合追加
                    //---------------------------------------------------------------------------------------
                    case WRK_ID.PLC_RECV_1:
                    case WRK_ID.PLC_RECV_2:
                    case WRK_ID.PLC_RECV_3:
                        // 車種情報Ｍマスタの読込み
                        MDB_READ1(this);

                        // マスター項目存在いないリスト表示
                        {
                            List<(string, string)> list = new List<(string, string)>();

                            PlcRecvChk(result.TagDat, result.VehDat, ref list);

                            if (list.Count > 0)
                            {
                                A_PlcRecvWarring dlglist = new A_PlcRecvWarring(this, list);

                                dlglist.DoModal();
                            }
                            else
                            {
                                // 完了メッセージ
                                Program.MessageBox("ＰＬＣから読込み正常完了");
                            }
                        }
                        break;
                    case WRK_ID.PLC_COMP_1:
                    case WRK_ID.PLC_COMP_2:
                    case WRK_ID.PLC_COMP_3:
                        //照合画面切替え
                        A_PlcComp dlg = new A_PlcComp(this, result.TagComp, result.VehicleComp);
                        // ウィンドウ表示
                        dlg.DoModal();
                        break;
                    //<<=====================================================================================
                    case WRK_ID.PRINT:
                        Program.MessageBox("印刷正常完了");
                        break;
                }
            }
            else
            {
                switch (result.Id)
                {
                    case WRK_ID.PLC_SEND_T:
                    case WRK_ID.PLC_SEND_1:
                    case WRK_ID.PLC_SEND_2:
                    case WRK_ID.PLC_SEND_3:
                    case WRK_ID.PLC_RECV_1:
                    case WRK_ID.PLC_RECV_2:
                    case WRK_ID.PLC_RECV_3:
                    case WRK_ID.PLC_COMP_1:
                    case WRK_ID.PLC_COMP_2:
                    case WRK_ID.PLC_COMP_3:
                        Program.MessageBox(Mx_sub.GetErrMsg((Mx_sub.PLC_ERR)result.Result), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case WRK_ID.PRINT:
                        Program.MessageBox("印刷失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
        }

        // バックグラウンド処理への引数
        private class CReadArgs
        {
            // コンストラクタ
            public CReadArgs(WRK_ID id, C1FlexGrid flexgrid)
            {
                Id = id;
                FlexGrid = flexgrid;
            }

            public CReadArgs(WRK_ID id)
            {
                Id = id;
            }

            public CReadArgs(WRK_ID id, int ptn, Mx_sub.TAG_INFO[] tagdat, Mx_sub.VEHICLE_INFO[] vehdat)
            {
                Id = id;
                Ptn = ptn;
                TagDat = new Mx_sub.TAG_INFO[tagdat.Length];
                VehDat = new Mx_sub.VEHICLE_INFO[vehdat.Length];

                // データコーピ
                for (int ii = 0; ii < tagdat.Length; ii++)
                {
                    TagDat[ii] = new Mx_sub.TAG_INFO(tagdat[ii]);
                }

                for (int ii = 0; ii < vehdat.Length; ii++)
                {
                    VehDat[ii] = new Mx_sub.VEHICLE_INFO(vehdat[ii]);
                }
            }

            // ワークバッファのプロパティ
            public WRK_ID Id { get; }        // 処理ＩＤ
            public C1FlexGrid FlexGrid { get; }        // データ
            public int Ptn { get; }        // パターン
            public Mx_sub.TAG_INFO[] TagDat { get; }        // タグ情報読込み結果
            public Mx_sub.VEHICLE_INFO[] VehDat { get; }        // 車種情報読込み結果
        }

        // バックグラウンド処理の進捗状況
        public class CReadState
        {
            // コンストラクタ
            public CReadState(int current, int maxcnt, string msg = "")
            {
                Current = current;
                MaxCnt = maxcnt;
                Msg = msg;
            }

            // ワークバッファのプロパティ
            public int Current { get; }        // 
            public int MaxCnt { get; }        // 
            public string Msg { get; }        // 
        }

        // バックグラウンド処理の結果
        private class CReadResult
        {
            // コンストラクタ
            public CReadResult(WRK_ID id, int result)
            {
                Id = id;
                Result = result;
            }

            public CReadResult(WRK_ID id, int result, TAG_COMP tagcomp, VEHICLE_COMP vehiclecomp)
            {
                Id = id;
                Result = result;
                TagComp = new TAG_COMP(tagcomp);
                VehicleComp = new VEHICLE_COMP(vehiclecomp);
            }

            public CReadResult(WRK_ID id, int result, Mx_sub.TAG_INFO[] tagdat, Mx_sub.VEHICLE_INFO[] vehdat)
            {
                Id = id;
                Result = result;
                TagDat = new Mx_sub.TAG_INFO[tagdat.Length];
                VehDat = new Mx_sub.VEHICLE_INFO[vehdat.Length];

                // データコーピ
                for (int ii = 0; ii < tagdat.Length; ii++)
                {
                    TagDat[ii] = new Mx_sub.TAG_INFO(tagdat[ii]);
                }

                for (int ii = 0; ii < vehdat.Length; ii++)
                {
                    VehDat[ii] = new Mx_sub.VEHICLE_INFO(vehdat[ii]);
                }
            }

            // ワークバッファのプロパティ
            public WRK_ID Id { get; }        // 処理ＩＤ
            public int Result { get; }        // 処理結果のプロパティ
            public TAG_COMP TagComp { get; }        // タグ情報照合結果
            public VEHICLE_COMP VehicleComp { get; }        // 車種情報照合結果
            public Mx_sub.TAG_INFO[] TagDat { get; }        // タグ情報読込み結果
            public Mx_sub.VEHICLE_INFO[] VehDat { get; }        // 車種情報読込み結果
        }

        // 処理ＩＤ
        private enum WRK_ID
        {
            PLC_SEND_T = 0,     // タグ情報をアップロード
            PLC_SEND_1,         // 車種情報パターン１をアップロード
            PLC_SEND_2,         // 車種情報パターン２をアップロード
            PLC_SEND_3,         // 車種情報パターン３をアップロード
                                //[2026/04/06][p.hoi][Add]=============================================================>>
                                //  PLCデータ読込、照合追加
                                //---------------------------------------------------------------------------------------
            PLC_RECV_1,         // PLCから読込みパターン１
            PLC_RECV_2,         // PLCから読込みパターン２
            PLC_RECV_3,         // PLCから読込みパターン３
            PLC_COMP_1,         // PLCとの照合パターン１
            PLC_COMP_2,         // PLCとの照合パターン２
            PLC_COMP_3,         // PLCとの照合パターン３
                                //<<=====================================================================================
            PRINT,              // 印刷
            count
        }
    }
}
