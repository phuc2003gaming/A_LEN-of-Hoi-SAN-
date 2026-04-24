using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.VisualBasic;
using System.Data.OleDb;    // データベース

using C1.Win.C1FlexGrid;

namespace X61GX42H1ST
{
    public partial class A_TAGJYOHO_MF : Form
    {
        // プライベート変数
        private static A_TAGJYOHO_MF m_Dlg = null;

        /// <summary>
        /// クラスのコンストラクタ
        /// </summary>
        public A_TAGJYOHO_MF()
        {
            InitializeComponent();
        }

        /// <summary>
        /// フォームの初期化
        /// </summary>
        private void A_TAGJYOHO_MF_Load(object sender, EventArgs e)
        {
            int i;   // カウンター変数

            // 再描画の無効化
            c1FlexGrid1.Redraw = false;

            // グリッド初期設定
            c1FlexGrid1.FocusRect               = FocusRectEnum.Heavy;  // セルを強調表示
            c1FlexGrid1.Rows.Count              = 501;                  // 行の総数
            c1FlexGrid1.Cols.Count              = 4;                    // 列の総数
            c1FlexGrid1.Rows.Fixed              = 1;                    // 固定行の総数
            c1FlexGrid1.Cols.Fixed              = 1;                    // 固定列の総数
            
            //c1FlexGrid1.Row = 0;        //行の指定
            c1FlexGrid1.Cols[0].Width           = Pixel(12 * 110);       // 列幅の設定
            c1FlexGrid1.Cols[1].Width           = Pixel(12 * 224);
            c1FlexGrid1.Cols[2].Width           = Pixel(12 * 150);
            c1FlexGrid1.Cols[3].Width           = Pixel(12 * 150);


            // 全行幅の設定
            c1FlexGrid1.Rows.DefaultSize = Pixel(350);

            c1FlexGrid1.Cols[0].TextAlign       = TextAlignEnum.CenterCenter;
            c1FlexGrid1.Cols[1].TextAlign       = TextAlignEnum.LeftCenter;
            c1FlexGrid1.Cols[2].TextAlign       = TextAlignEnum.LeftCenter;
            c1FlexGrid1.Cols[3].TextAlign       = TextAlignEnum.LeftCenter;
            //c1FlexGrid1.Col = 0;        //列の指定
            
            c1FlexGrid1.Cols[1].TextAlign       = TextAlignEnum.LeftCenter;
            c1FlexGrid1.Cols[2].TextAlign       = TextAlignEnum.LeftCenter;
            c1FlexGrid1.Cols[3].TextAlign       = TextAlignEnum.LeftCenter;

            // 行№を設定
            for (i = 1; i <= (c1FlexGrid1.Rows.Count - 1); i++) { c1FlexGrid1[i, 0] = i; }

            // ヘッダ部のセル内のテキスト表示位置
            c1FlexGrid1.Rows[0].TextAlignFixed = TextAlignEnum.CenterCenter;
            c1FlexGrid1.Rows[0].TextAlign      = TextAlignEnum.CenterCenter;

            //列のタイトル設定
            c1FlexGrid1[0, 0] = "ＩＤ№";
            c1FlexGrid1[0, 1] = "治具タイプ";
            c1FlexGrid1[0, 2] = "治具1st / 2nd";
            c1FlexGrid1[0, 3] = "治具ｽﾄｯｸ段";

            // セルの初期位置
            c1FlexGrid1.Col = 1;
            c1FlexGrid1.Row = 1;

            Danpre_Xls_sub.Xls_MF_List_Read4(c1FlexGrid1); // 選択項目にデータをセット

            // データ読み込み
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
                    m_Dlg = new A_TAGJYOHO_MF();
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
        /// タグ情報マスタの新規書込み/更新
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

                    SQl  = "select * from T_タグ ";
                    SQl += "WHERE № = '" + " 1" + "'";

                    using (OleDbCommand cmd = new OleDbCommand(SQl, DB))    // 検索条件をセット
                    {
                        using (OleDbDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows == false) {
                                for (i = 1; i <= (c1FlexGrid1.Rows.Count - 1); i++)
                                {
                                    // 新規書込み
                                    SQl  = "INSERT INTO T_タグ ";
                                    SQl += "(№,治具タイプ,治具ＬＲ,治具ストック段) VALUES ";
                                    SQl += "(";
                                    SQl += "'" + " " + c1FlexGrid1.GetData(i, 0) + "',";
                                    SQl += "'" + " " + c1FlexGrid1.GetData(i, 1) + "',";
                                    SQl += "'" + " " + c1FlexGrid1.GetData(i, 2) + "',";
                                    SQl += "'" + " " + c1FlexGrid1.GetData(i, 3) + "'";
                                    SQl += ")";
                                    
                                    // 実行
                                    using (OleDbCommand cmd1 = new OleDbCommand(SQl, DB)) {
                                        cmd1.ExecuteNonQuery();
                                    }
                                }
                            }
                            else
                            {
                                for (i = 1; i <= (c1FlexGrid1.Rows.Count - 1); i++)
                                {
                                    // 更新
                                    SQl  = "UPDATE T_タグ SET ";
                                    SQl += "治具タイプ = '"     + " " + c1FlexGrid1.GetData(i, 1) + "',";
                                    SQl += "治具ＬＲ = '"       + " " + c1FlexGrid1.GetData(i, 2) + "',";
                                    SQl += "治具ストック段 = '" + " " + c1FlexGrid1.GetData(i, 3) + "' ";
                                    SQl += "WHERE Trim(№) = '" + i.ToString() + "';";

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
            }
            catch
            {
                ;
            }
        }

        /// <summary>
        /// タグ情報マスタの読込み
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

                    SQl  = "select * from T_タグ ";
                    SQl += "order by val(№)";

                    using (OleDbCommand cmd = new OleDbCommand(SQl, DB))
                    {
                        using (OleDbDataReader RS = cmd.ExecuteReader())
                        {
                            while (RS.Read()) {
                                c1FlexGrid1.SetData(Convert.ToInt32(RS["№"]), 1, Strings.Right(RS["治具タイプ"].ToString(),     Strings.Len(RS["治具タイプ"])     - 1));
                                c1FlexGrid1.SetData(Convert.ToInt32(RS["№"]), 2, Strings.Right(RS["治具ＬＲ"].ToString(),       Strings.Len(RS["治具ＬＲ"])       - 1));
                                c1FlexGrid1.SetData(Convert.ToInt32(RS["№"]), 3, Strings.Right(RS["治具ストック段"].ToString(), Strings.Len(RS["治具ストック段"]) - 1));
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
