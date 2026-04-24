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
    public partial class A_RESIPICALL : Form
    {
        // プライベート変数
        private static A_RESIPICALL m_Dlg = null;

        /// <summary>
        /// クラスのコンストラクタ
        /// </summary>
        public A_RESIPICALL()
        {
            InitializeComponent();
        }

        /// <summary>
        /// フォームの初期化
        /// </summary>
        private void A_RESIPICALL_Load(object sender, EventArgs e)
        {
            int     i;          // カウンター変数

            // 再描画の無効化
            c1FlexGrid0.Redraw = false;
            c1FlexGrid1.Redraw = false;

            {
                // グリッド初期設定
                c1FlexGrid0.FocusRect               = FocusRectEnum.Heavy;  // セルを強調表示
                c1FlexGrid0.Rows.Count              = 1025;                 // 行の総数
                c1FlexGrid0.Cols.Count              = 4;                    // 列の総数
                c1FlexGrid0.Rows.Fixed              = 1;                    // 固定行の総数
                c1FlexGrid0.Cols.Fixed              = 1;                    // 固定列の総数
            
                //c1FlexGrid0.Row = 0;        //行の指定
                c1FlexGrid0.Cols[0].Width           = Pixel(12 * 110);       // 列幅の設定
                c1FlexGrid0.Cols[1].Width           = Pixel(42 * 110);
                c1FlexGrid0.Cols[2].Width           = Pixel(12 * 129);
                c1FlexGrid0.Cols[3].Width           = Pixel(0);

                // 全行幅の設定
                c1FlexGrid0.Rows.DefaultSize = Pixel(350);

                c1FlexGrid0.Cols[0].TextAlign       = TextAlignEnum.CenterCenter;
                c1FlexGrid0.Cols[1].TextAlign       = TextAlignEnum.RightCenter;
                c1FlexGrid0.Cols[2].TextAlign       = TextAlignEnum.RightCenter;
                c1FlexGrid0.Cols[3].TextAlign       = TextAlignEnum.RightCenter;
                //c1FlexGrid0.Col = 0;        //列の指定

                // 行№を設定
                for (i = 1; i <= (c1FlexGrid0.Rows.Count - 1); i++) { c1FlexGrid0[i, 0] = i; }

                // ヘッダ部のセル内のテキスト表示位置
                c1FlexGrid0.Rows[0].TextAlignFixed = TextAlignEnum.CenterCenter;
                c1FlexGrid0.Rows[0].TextAlign      = TextAlignEnum.CenterCenter;

                //列のタイトル設定
                c1FlexGrid0[0, 0] = "レシピ№";
                c1FlexGrid0[0, 1] = "レシピ名";
                c1FlexGrid0[0, 2] = "使用数";
                c1FlexGrid0[0, 3] = "セルの初期位置";

                // セルの初期位置
                c1FlexGrid0.Col = 1;
                c1FlexGrid0.Row = 1;   
            }

            
            {
                // グリッド初期設定
                c1FlexGrid1.FocusRect               = FocusRectEnum.Heavy;  // セルを強調表示
                c1FlexGrid1.Rows.Count              = 2;                    // 行の総数
                c1FlexGrid1.Cols.Count              = 2;                    // 列の総数
                c1FlexGrid1.Rows.Fixed              = 1;                    // 固定行の総数
                c1FlexGrid1.Cols.Fixed              = 1;                    // 固定列の総数
            
                //c1FlexGrid1.Row = 0;        //行の指定
                c1FlexGrid1.Cols[0].Width           = Pixel(12 * 110);       // 列幅の設定
                c1FlexGrid1.Cols[1].Width           = Pixel(12 * 174);

                // 全行幅の設定
                c1FlexGrid1.Rows.DefaultSize = Pixel(350);

                c1FlexGrid1.Cols[0].TextAlign       = TextAlignEnum.RightCenter;
                c1FlexGrid1.Cols[1].TextAlign       = TextAlignEnum.RightCenter;
                //c1FlexGrid1.Col = 0;        //列の指定

                // 行№を設定
                for (i = 1; i <= (c1FlexGrid1.Rows.Count - 1); i++) { c1FlexGrid1[i, 0] = i; }

                // ヘッダ部のセル内のテキスト表示位置
                c1FlexGrid1.Rows[0].TextAlignFixed = TextAlignEnum.CenterCenter;
                c1FlexGrid1.Rows[0].TextAlign      = TextAlignEnum.CenterCenter;

                //列のタイトル設定
                c1FlexGrid1[0, 0] = "識別コード";
                c1FlexGrid1[0, 1] = "作業パターン";

                // セルの初期位置
                c1FlexGrid1.Col = 1;
                c1FlexGrid1.Row = 1;   
            }

            MDB_READ1();    // レシピマスター読込み
            MDB_READ2();    // 車種情報ファイルよりレシピ使用状況生成

            // 再描画の再設定
            c1FlexGrid0.Redraw = true;
            c1FlexGrid1.Redraw = true;
        }

        /// <summary>
        /// 「ファイル」メニューをクリック
        /// </summary>
        private void File_Command_Click(object sender, EventArgs e)
        {
            DialogResult dr;

            // 確認ウィンドウを表示する
            dr = MessageBox.Show("レシピ参照を終了しますか?", "終了", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.Yes)
            {
                // 終了
                VisibleWindow(false);
            }
        }

        /// <summary>
        /// ダブルクリック処理
        /// </summary>
        private void c1FlexGrid0_DoubleClick(object sender, EventArgs e)
        {
            string Key_code;

            if ((c1FlexGrid0.Row > 0) && (c1FlexGrid0.Row < c1FlexGrid0.Rows.Count))
            {
                Key_code = " " + c1FlexGrid0.GetData(c1FlexGrid0.Row, 0);

                // 再描画の無効化
                c1FlexGrid1.Redraw = false;

                c1FlexGrid1.Rows.Count = 1;
                MDB_READ3(Key_code);

                // セルの初期位置
                if(c1FlexGrid1.Rows.Count > 1)
                {
                    c1FlexGrid1.Row    = 1;
                    c1FlexGrid1.TopRow = 1;
                }

                // 再描画の再設定
                c1FlexGrid1.Redraw = true;
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
                    m_Dlg = new A_RESIPICALL();
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
                                c1FlexGrid0.SetData(Convert.ToInt32(RS["レシピ№"]), 1, Strings.Right(RS["レシピ名"].ToString(), Strings.Len(RS["レシピ名"]) - 1));
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
        /// 車種情報マスタの読込み(使用数確認）
        /// </summary>
        private void MDB_READ2() 
        {
            string SQl;
            string conStr = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={Mdb_Module.DataBase1};";

            try
            {
                using (OleDbConnection DB = new OleDbConnection(conStr))
                {
                    DB.Open(); // データベース接続

                    for (int i = 1; i <= (c1FlexGrid0.Rows.Count - 1); i++)
                    {
                        SQl  = "select count(*) from T_車種情報Ｔ ";
                        SQl += "WHERE レシピ№ = '" + " " + c1FlexGrid0.GetData(i, 0) + "'";

                        using (OleDbCommand cmd = new OleDbCommand(SQl, DB))
                        {
                            using (OleDbDataReader RS = cmd.ExecuteReader())
                            {
                                if (RS.HasRows == true) {
                                    if (RS.Read()) {
                                        if (RS.GetInt32(0) > 0) {
                                            c1FlexGrid0.SetData(i, 2, RS.GetInt32(0));
                                        }
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
        /// 車種情報マスタの読込み(使用数確認）
        /// </summary>
        private void MDB_READ3(string Key_code) 
        {
            int    i;
            string SQl;
            string conStr = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={Mdb_Module.DataBase1};";

            try
            {
                using (OleDbConnection DB = new OleDbConnection(conStr))
                {
                    DB.Open(); // データベース接続

                    SQl  = "select * from T_車種情報Ｔ ";
                    SQl += "WHERE レシピ№ = '" + Key_code + "'";
                    SQl += " order by 識別コード,val(レシピ№),val(作業パターン)";

                    using (OleDbCommand cmd = new OleDbCommand(SQl, DB))
                    {
                        using (OleDbDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows == true)
                            {
                                i = 0;

                                if (RS.HasRows == true)
                                {
                                    while (RS.Read())
                                    {
                                        i++;

                                        c1FlexGrid1.Rows.Count = i + 1;
                                        c1FlexGrid1.SetData(i, 0, RS["識別コード"]);
                                        c1FlexGrid1.SetData(i, 1, RS["作業パターン"]);
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
    }
}
