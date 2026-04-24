using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using C1.Win.C1FlexGrid;
using System.Data.OleDb;    // データベース

namespace X61GX42H1ST
{
    public partial class A_RESIPI_MF : Form
    {
        // プライベート変数
        private static A_RESIPI_MF m_Dlg = null;

        /// <summary>
        /// クラスのコンストラクタ
        /// </summary>
        public A_RESIPI_MF()
        {
            InitializeComponent();
        }

        /// <summary>
        /// フォームの初期化
        /// </summary>
        private void A_RESIPI_MF_Load(object sender, EventArgs e)
        {
            int i;   // カウンター変数

            // 再描画の無効化
            c1FlexGrid1.Redraw = false;

            // グリッド初期設定
            c1FlexGrid1.FocusRect               = FocusRectEnum.Heavy;  // セルを強調表示
            c1FlexGrid1.Rows.Count              = 1025;                 // 100 // 行の総数
            c1FlexGrid1.Cols.Count              = 2;                    //列の総数
            c1FlexGrid1.Rows.Fixed              = 1;                    // 固定行の総数
            c1FlexGrid1.Cols.Fixed              = 1;                    // 固定列の総数
            
            //c1FlexGrid1.Row = 0;        //行の指定
            c1FlexGrid1.Cols[0].Width           = Pixel(12 * 110);       // 列幅の設定
            c1FlexGrid1.Cols[1].Width           = 339;

            // 全行幅の設定
            c1FlexGrid1.Rows.DefaultSize = Pixel(350);

            c1FlexGrid1.Cols[0].TextAlign       = TextAlignEnum.CenterCenter;
            c1FlexGrid1.Cols[1].TextAlign       = TextAlignEnum.LeftCenter;
            //c1FlexGrid1.Col = 0;        //列の指定
            
            // 行№を設定
            for (i = 1; i <= (c1FlexGrid1.Rows.Count - 1); i++) { c1FlexGrid1[i, 0] = i; }

            // ヘッダ部のセル内のテキスト表示位置
            c1FlexGrid1.Rows[0].TextAlignFixed = TextAlignEnum.CenterCenter;
            c1FlexGrid1.Rows[0].TextAlign      = TextAlignEnum.CenterCenter;

            //列のタイトル設定
            c1FlexGrid1[0, 0] = "レシピ№";
            c1FlexGrid1[0, 1] = "レシピ名";

            // セルの初期位置
            c1FlexGrid1.Col = 1;
            c1FlexGrid1.Row = 1;

            // データ読込
            MDB_READ1();

            // 再描画の再設定
            c1FlexGrid1.Redraw = true;
        }

        /// <summary>
        /// 「編集」メニューをクリック
        /// </summary>
        private void Edit_Command_Click(object sender, EventArgs e)
        {
            DialogResult dr;

            // 確認ウィンドウを表示する
            dr = MessageBox.Show("選択行を削除しますか?", "行削除", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.Yes)
            {
                for(int i = 1; i <= (c1FlexGrid1.Cols.Count - 1); i++)
                {
                    c1FlexGrid1.SetData(c1FlexGrid1.Row, i, "");
                }
            }
        }
        
        /// <summary>
        /// 「ファイル」メニューをクリック
        /// </summary>
        private void File_Command_Click(object sender, EventArgs e)
        {
            var          clkdItem = sender as ToolStripMenuItem;
            string       Msg;
            string       Title;
            DialogResult dr;

            if ((clkdItem != null) &&
                (clkdItem.OwnerItem is ToolStripMenuItem item))
            {
                // 選択インデックス
                int index = item.DropDownItems.IndexOf(clkdItem);

                switch (index)
                {
                    case 0: // 保存
                        Msg   = "ファイルに保存しますか?";         // メッセージを定義します。
                        Title = "ファイルに保存";                  // タイトルを定義します。

                        // 確認ウィンドウを表示する
                        dr = MessageBox.Show(Msg, Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        if (dr == DialogResult.Yes)
                        {
                            c1FlexGrid1.FinishEditing();    // 編集途中にもデータ取れる

                            // 書込み
                            MDB_WRITE1();
                            c1FlexGrid1.Row = 1;
                            c1FlexGrid1.Col = 1;

                            MessageBox.Show("書込み完了", Title);
                        }
                        break;

                    case 1: // 終了
                        Msg   = "ファイルに保存して終了しますか?";         // メッセージを定義します。
                        Title = "終了";                                    // タイトルを定義します。
                        
                        // 確認ウィンドウを表示する
                        dr = MessageBox.Show(Msg, Title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                        switch (dr)
                        {
                            case DialogResult.Yes:
                                c1FlexGrid1.FinishEditing();    // 編集途中にもデータ取れる
                                // 書込み
                                MDB_WRITE1();
                                // ウィンドウを非表示する
                                VisibleWindow(false);
                                break;

                            case DialogResult.No:
                                // ウィンドウを非表示する
                                VisibleWindow(false);
                                break;
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// 起動中ウィンドウを表示／非表示する
        /// </summary>
        /// <param name="bShow"></param>表示／非表示する
        static public void VisibleWindow(bool bShow)
        {
            if (bShow == true)
            {
                // フォームの初期化
                if ((m_Dlg is null) == true)
                {
                    m_Dlg = new A_RESIPI_MF();
                }

                // フォームの表示
                if ((m_Dlg is null) != true)
                {
                    if (m_Dlg.Visible != true)
                    {
                        ;
                    }
                    // ダイアログ表示
                    m_Dlg.ShowDialog(Program.GetWindow());
                }
            }
            else
            {
                // フォームを閉じる
                if ((m_Dlg is null) != true)
                {
                    if (m_Dlg.Visible == true)
                    {
                        m_Dlg.Close();
                    }
                    m_Dlg.Dispose();
                }

                // フォームの開放
                m_Dlg = null;
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

        /// <summary>
        /// レシピマスタの新規書込み/更新
        /// </summary>
        private void MDB_WRITE1()
        {
            string SQl;
            int    i;
            string conStr = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={Mdb_Module.DataBase1};";

            try
            {
                using (OleDbConnection DB = new OleDbConnection(conStr))
                {
                    DB.Open(); // データベース接続


                    for (i = 1; i <= (c1FlexGrid1.Rows.Count - 1); i++)
                    {
                        SQl  = @"select * from T_ﾚｼﾋﾟ ";
                        SQl += @"WHERE Trim(レシピ№) = '" + i.ToString() + "'";

                        using (OleDbCommand cmd = new OleDbCommand(SQl, DB))    // 検索条件をセット
                        {
                            using (OleDbDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows == false) {
                                    // 新規書込み
                                    SQl  = "INSERT INTO T_ﾚｼﾋﾟ ";
                                    SQl += "(レシピ№,レシピ名) VALUES ";
                                    SQl += "(";
                                    SQl += "'" + " " + c1FlexGrid1.GetData(i, 0) + "',";
                                    SQl += "'" + " " + c1FlexGrid1.GetData(i, 1) + "'";
                                    SQl += ")"; 
                                }
                                else
                                {
                                    // 更新
                                    SQl  = "UPDATE T_ﾚｼﾋﾟ SET ";
                                    SQl += "レシピ名 = '" + " " + c1FlexGrid1.GetData(i, 1) + "' ";
                                    SQl += "WHERE Trim(レシピ№) = '" + i.ToString() + "';";
                                }

                                // 実行
                                using (OleDbCommand cmd1 = new OleDbCommand(SQl, DB))
                                {
                                    cmd1.ExecuteNonQuery();
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
        /// レシピマスタの読込み
        /// </summary>
        private void MDB_READ1() 
        {
            string SQl;
            string conStr = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={Mdb_Module.DataBase1};";

            try
            {
                using (OleDbConnection DB = new OleDbConnection(conStr))
                {
                    DB.Open(); // データベース接続

                    SQl = "select * from T_ﾚｼﾋﾟ ";

                    using (OleDbCommand cmd = new OleDbCommand(SQl, DB))
                    {
                        using (OleDbDataReader RS = cmd.ExecuteReader())
                        {
                            while (RS.Read()) {

                                c1FlexGrid1.SetData(Convert.ToInt32(RS["レシピ№"]), 1, RS["レシピ名"].ToString().Substring(1));
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
    }
}
