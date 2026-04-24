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
using System.Data.OleDb;    // データベース
using Microsoft.VisualBasic;
using JRO;

namespace X61GX42H1ST
{
    public partial class A_Dcopy : Form
    {
        // プライベート変数
        private static   A_Dcopy       m_Dlg = null;
        private readonly RadioButton[] Option1;
        private readonly RadioButton[] Option2;

        /// <summary>
        /// クラスのコンストラクタ
        /// </summary>
        public A_Dcopy()
        {
            InitializeComponent();

            Option1 = new RadioButton[]
            {
                Option1_0, Option1_1, Option1_2
            };

            Option2 = new RadioButton[]
            {
                Option2_0, Option2_1, Option2_2
            };
        }
        
        /// <summary>
        /// フォームの初期化
        /// </summary>
        private void A_Dcopy_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 「ＯＫ」ボタンをクリック
        /// </summary>
        private void button_OK_Click(object sender, EventArgs e)
        {
            int     i;
            int     SL_point;
            int     SR_point;
            int     EL_point;
            int     ER_point;
            int     P_Ban;
            string  ConDB1;
            string  SQl1;
            string  ConDB2;
            string  SQl2;

            SL_point = 0;
            SR_point = 0;
            EL_point = 0;
            ER_point = 0;
            P_Ban    = 0;

            for (i = 0; i <= 2; i++)
            {
                if (Option1[i].Checked == true) {   // コピー元のチェック
                    SL_point = i;                   // Ｌ側タブインデックス
                    SR_point = i + 3;               // Ｒ側タブインデックス

                    break;
                }
            }
            
            for (i = 0; i <= 2; i++)
            {
                if (Option2[i].Checked == true) {   // コピー先のチェック
                    EL_point = i;                   // Ｌ側タブインデックス
                    ER_point = i + 3;               // Ｒ側タブインデックス
                    P_Ban    = i + 1;               // 作業パターン

                    break;
                }
            }

            ConDB1 = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={Mdb_Module.DataBase1};";
            ConDB2 = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={Mdb_Module.DataBase2};";

            if (SL_point != EL_point) {

                try
                {
                    using (OleDbConnection DB1 = new OleDbConnection($"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={Mdb_Module.DataBase1};"))
                    {
                        DB1.Open();

                        SQl1  = "DELETE * From T_車種情報Ｔ ";
                        SQl1 += "WHERE Trim(タブＧＰ) = '" + EL_point.ToString().Trim() + "'";
                        // 実行
                        using (OleDbCommand cmd = new OleDbCommand(SQl1, DB1)) {
                            cmd.ExecuteNonQuery();
                        }

                        SQl1  = "DELETE * From T_車種情報Ｔ ";
                        SQl1 += "WHERE Trim(タブＧＰ) = '" + ER_point.ToString().Trim() + "'";
                        // 実行
                        using (OleDbCommand cmd = new OleDbCommand(SQl1, DB1)) {
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // 最適化,修復
                    JetEngine DBEngine = new JetEngine();
                    DBEngine.CompactDatabase(ConDB1, ConDB2 + "Jet OLEDB:Engine Type=5");

                    using (OleDbConnection DB1 = new OleDbConnection(ConDB1))   
                    using (OleDbConnection DB2 = new OleDbConnection(ConDB2))   
                    {
                        DB1.Open();           // コピー元を開く
                        DB2.Open();           // コピー先を開く

                        SQl1  = "select * from T_車種情報Ｔ ";
                        SQl1 += "WHERE Trim(タブＧＰ) = '" + SL_point.ToString().Trim() + "'";
                        SQl1 += " order by val(タブＧＰ),val(レシピ№)";

                        using (OleDbCommand cmd = new OleDbCommand(SQl1, DB1))  // 検索条件をセット
                        {
                            using (OleDbDataReader RS1 = cmd.ExecuteReader()) {
                                while (RS1.Read())
                                {
                                    // 新規書込み
                                    SQl2  = "INSERT INTO T_車種情報Ｔ ";
                                    SQl2 += "(識別コード,タブＧＰ,治具タイプ,治具切出し順,1st治具ストック段,2nd治具ストック段,向先,ｼｰﾄﾀｲﾌﾟ,AGﾀｲﾌﾟ,ﾋｰﾀｰ,ﾊﾞｯｸﾙ,ﾍｯﾄﾞﾚｽﾄ,着座ｾﾝｻｰ,空調,";
                                    SQl2 += "表皮材,色,ﾗﾝﾊﾞｰ,背面ﾎﾟｹｯﾄ,ﾌｯﾄｳｴﾙﾗﾝﾌﾟ,ｱｰﾑﾚｽﾄ,ﾀﾝﾌﾞﾙ,ISOFIX,ﾃｻﾞｰ,ｵｯﾄﾏﾝ,ﾛﾎﾞｯﾄ,";
                                    SQl2 += "作業パターン,レシピ№,ST0101,ST0102,ST0103,ST0104,ST0105,ST0106,ST0107,ST0108,ST0109,ST0110,ST0111,ST0112,ST0113,ST0114,ST0115,ST0116) VALUES ";
                                    SQl2 += "(";
                                    SQl2 += "'" +       RS1["識別コード"]            + "',";
                                    SQl2 += "'" +       Conversion.Str(EL_point)    + "',";
                                    SQl2 += "'" +       RS1["治具タイプ"]            + "',";
                                    SQl2 += "'" +       RS1["治具切出し順"]          + "',";
                                    SQl2 += "'" +       RS1["1st治具ストック段"]     + "',";
                                    SQl2 += "'" +       RS1["2nd治具ストック段"]     + "',";
                                    SQl2 += "'" +       RS1["向先"]                 + "',";
                                    SQl2 += "'" +       RS1["ｼｰﾄﾀｲﾌﾟ"]               + "',";
                                    SQl2 += "'" +       RS1["AGﾀｲﾌﾟ"]                + "',";
                                    SQl2 += "'" +       RS1["ﾋｰﾀｰ"]                  + "',";
                                    SQl2 += "'" +       RS1["ﾊﾞｯｸﾙ"]                 + "',";
                                    SQl2 += "'" +       RS1["ﾍｯﾄﾞﾚｽﾄ"]               + "',";
                                    SQl2 += "'" +       RS1["着座ｾﾝｻｰ"]              + "',";
                                    SQl2 += "'" +       RS1["空調"]                  + "',";
                                    SQl2 += "'" +       RS1["表皮材"]                + "',";
                                    SQl2 += "'" +       RS1["色"]                    + "',";
                                    SQl2 += "'" +       RS1["ﾗﾝﾊﾞｰ"]                 + "',";
                                    SQl2 += "'" +       RS1["背面ﾎﾟｹｯﾄ"]              + "',";
                                    SQl2 += "'" +       RS1["ﾌｯﾄｳｴﾙﾗﾝﾌﾟ"]             + "',";
                                    SQl2 += "'" +       RS1["ｱｰﾑﾚｽﾄ"]                + "',";
                                    SQl2 += "'" +       RS1["ﾀﾝﾌﾞﾙ"]                 + "',";
                                    SQl2 += "'" +       RS1["ISOFIX"]                + "',";
                                    SQl2 += "'" +       RS1["ﾃｻﾞｰ"]                  + "',";
                                    SQl2 += "'" +       RS1["ｵｯﾄﾏﾝ"]                 + "',";
                                    SQl2 += "'" +       RS1["ﾛﾎﾞｯﾄ"]                 + "',";
                                    SQl2 += "'" +       Conversion.Str(P_Ban)        + "',";
                                    SQl2 += "'" + " " + Strings.Right(RS1["レシピ№"].ToString(), Strings.Len(RS1["レシピ№"]) - 1) + "',";
                                    SQl2 += "'" + " " + Strings.Right(RS1["ST0101"].ToString()  , Strings.Len(RS1["ST0101"]) - 1)   + "',";
                                    SQl2 += "'" + " " + Strings.Right(RS1["ST0102"].ToString()  , Strings.Len(RS1["ST0102"]) - 1)   + "',";
                                    SQl2 += "'" + " " + Strings.Right(RS1["ST0103"].ToString()  , Strings.Len(RS1["ST0103"]) - 1)   + "',";
                                    SQl2 += "'" + " " + Strings.Right(RS1["ST0104"].ToString()  , Strings.Len(RS1["ST0104"]) - 1)   + "',";
                                    SQl2 += "'" + " " + Strings.Right(RS1["ST0105"].ToString()  , Strings.Len(RS1["ST0105"]) - 1)   + "',";
                                    SQl2 += "'" + " " + Strings.Right(RS1["ST0106"].ToString()  , Strings.Len(RS1["ST0106"]) - 1)   + "',";
                                    SQl2 += "'" + " " + Strings.Right(RS1["ST0107"].ToString()  , Strings.Len(RS1["ST0107"]) - 1)   + "',";
                                    SQl2 += "'" + " " + Strings.Right(RS1["ST0108"].ToString()  , Strings.Len(RS1["ST0108"]) - 1)   + "',";
                                    SQl2 += "'" + " " + Strings.Right(RS1["ST0109"].ToString()  , Strings.Len(RS1["ST0109"]) - 1)   + "',";
                                    SQl2 += "'" + " " + Strings.Right(RS1["ST0110"].ToString()  , Strings.Len(RS1["ST0110"]) - 1)   + "',";
                                    SQl2 += "'" + " " + Strings.Right(RS1["ST0111"].ToString()  , Strings.Len(RS1["ST0111"]) - 1)   + "',";
                                    SQl2 += "'" + " " + Strings.Right(RS1["ST0112"].ToString()  , Strings.Len(RS1["ST0112"]) - 1)   + "',";
                                    SQl2 += "'" + " " + Strings.Right(RS1["ST0113"].ToString()  , Strings.Len(RS1["ST0113"]) - 1)   + "',";
                                    SQl2 += "'" + " " + Strings.Right(RS1["ST0114"].ToString()  , Strings.Len(RS1["ST0114"]) - 1)   + "',";
                                    SQl2 += "'" + " " + Strings.Right(RS1["ST0115"].ToString()  , Strings.Len(RS1["ST0115"]) - 1)   + "',";
                                    SQl2 += "'" + " " + Strings.Right(RS1["ST0116"].ToString()  , Strings.Len(RS1["ST0116"]) - 1)   + "'";
                                    SQl2 += ")";
                                    
                                    
                                    // 実行
                                    using (OleDbCommand cmd2 = new OleDbCommand(SQl2, DB2)) {
                                        cmd2.ExecuteNonQuery();
                                    }
                                }
                            }
                        }


                        SQl1  = "select * from T_車種情報Ｔ ";
                        SQl1 += "WHERE Trim(タブＧＰ) = '" + SR_point.ToString().Trim() + "'";
                        SQl1 += " order by val(タブＧＰ),val(レシピ№)";

                        using (OleDbCommand cmd = new OleDbCommand(SQl1, DB1))  // 検索条件をセット
                        {
                            using (OleDbDataReader RS2 = cmd.ExecuteReader()) {
                                while (RS2.Read())
                                {
                                    // 新規書込み
                                    SQl2  = "INSERT INTO T_車種情報Ｔ ";
                                    SQl2 += "(識別コード,タブＧＰ,治具タイプ,治具切出し順,1st治具ストック段,2nd治具ストック段,向先,ｼｰﾄﾀｲﾌﾟ,AGﾀｲﾌﾟ,ﾋｰﾀｰ,ﾊﾞｯｸﾙ,ﾍｯﾄﾞﾚｽﾄ,着座ｾﾝｻｰ,空調,";
                                    SQl2 += "表皮材,色,ﾗﾝﾊﾞｰ,背面ﾎﾟｹｯﾄ,ﾌｯﾄｳｴﾙﾗﾝﾌﾟ,ｱｰﾑﾚｽﾄ,ﾀﾝﾌﾞﾙ,ISOFIX,ﾃｻﾞｰ,ｵｯﾄﾏﾝ,ﾛﾎﾞｯﾄ,";
                                    SQl2 += "作業パターン,レシピ№,ST0101,ST0102,ST0103,ST0104,ST0105,ST0106,ST0107,ST0108,ST0109,ST0110,ST0111,ST0112,ST0113,ST0114,ST0115,ST0116) VALUES ";
                                    SQl2 += "(";
                                    SQl2 += "'" +       RS2["識別コード"]            + "',";
                                    SQl2 += "'" +       Conversion.Str(ER_point)    + "',";
                                    SQl2 += "'" +       RS2["治具タイプ"]            + "',";
                                    SQl2 += "'" +       RS2["治具切出し順"]          + "',";
                                    SQl2 += "'" +       RS2["1st治具ストック段"]     + "',";
                                    SQl2 += "'" +       RS2["2nd治具ストック段"]     + "',";
                                    SQl2 += "'" +       RS2["向先"]                 + "',";
                                    SQl2 += "'" +       RS2["ｼｰﾄﾀｲﾌﾟ"]              + "',";
                                    SQl2 += "'" +       RS2["AGﾀｲﾌﾟ"]               + "',";
                                    SQl2 += "'" +       RS2["ﾋｰﾀｰ"]                 + "',";
                                    SQl2 += "'" +       RS2["ﾊﾞｯｸﾙ"]                + "',";
                                    SQl2 += "'" +       RS2["ﾍｯﾄﾞﾚｽﾄ"]              + "',";
                                    SQl2 += "'" +       RS2["着座ｾﾝｻｰ"]             + "',";
                                    SQl2 += "'" +       RS2["空調"]                 + "',";
                                    SQl2 += "'" +       RS2["表皮材"]               + "',";
                                    SQl2 += "'" +       RS2["色"]                   + "',";
                                    SQl2 += "'" +       RS2["ﾗﾝﾊﾞｰ"]                + "',";
                                    SQl2 += "'" +       RS2["背面ﾎﾟｹｯﾄ"]             + "',";
                                    SQl2 += "'" +       RS2["ﾌｯﾄｳｴﾙﾗﾝﾌﾟ"]            + "',";
                                    SQl2 += "'" +       RS2["ｱｰﾑﾚｽﾄ"]                + "',";
                                    SQl2 += "'" +       RS2["ﾀﾝﾌﾞﾙ"]                 + "',";
                                    SQl2 += "'" +       RS2["ISOFIX"]                + "',";
                                    SQl2 += "'" +       RS2["ﾃｻﾞｰ"]                  + "',";
                                    SQl2 += "'" +       RS2["ｵｯﾄﾏﾝ"]                 + "',";
                                    SQl2 += "'" +       RS2["ﾛﾎﾞｯﾄ"]                 + "',";
                                    SQl2 += "'" +       Conversion.Str(P_Ban)        + "',";
                                    SQl2 += "'" + " " + Strings.Right(RS2["レシピ№"].ToString(), Strings.Len(RS2["レシピ№"]) - 1) + "',";
                                    SQl2 += "'" + " " + Strings.Right(RS2["ST0101"].ToString()  , Strings.Len(RS2["ST0101"]) - 1)   + "',";
                                    SQl2 += "'" + " " + Strings.Right(RS2["ST0102"].ToString()  , Strings.Len(RS2["ST0102"]) - 1)   + "',";
                                    SQl2 += "'" + " " + Strings.Right(RS2["ST0103"].ToString()  , Strings.Len(RS2["ST0103"]) - 1)   + "',";
                                    SQl2 += "'" + " " + Strings.Right(RS2["ST0104"].ToString()  , Strings.Len(RS2["ST0104"]) - 1)   + "',";
                                    SQl2 += "'" + " " + Strings.Right(RS2["ST0105"].ToString()  , Strings.Len(RS2["ST0105"]) - 1)   + "',";
                                    SQl2 += "'" + " " + Strings.Right(RS2["ST0106"].ToString()  , Strings.Len(RS2["ST0106"]) - 1)   + "',";
                                    SQl2 += "'" + " " + Strings.Right(RS2["ST0107"].ToString()  , Strings.Len(RS2["ST0107"]) - 1)   + "',";
                                    SQl2 += "'" + " " + Strings.Right(RS2["ST0108"].ToString()  , Strings.Len(RS2["ST0108"]) - 1)   + "',";
                                    SQl2 += "'" + " " + Strings.Right(RS2["ST0109"].ToString()  , Strings.Len(RS2["ST0109"]) - 1)   + "',";
                                    SQl2 += "'" + " " + Strings.Right(RS2["ST0110"].ToString()  , Strings.Len(RS2["ST0110"]) - 1)   + "',";
                                    SQl2 += "'" + " " + Strings.Right(RS2["ST0111"].ToString()  , Strings.Len(RS2["ST0111"]) - 1)   + "',";
                                    SQl2 += "'" + " " + Strings.Right(RS2["ST0112"].ToString()  , Strings.Len(RS2["ST0112"]) - 1)   + "',";
                                    SQl2 += "'" + " " + Strings.Right(RS2["ST0113"].ToString()  , Strings.Len(RS2["ST0113"]) - 1)   + "',";
                                    SQl2 += "'" + " " + Strings.Right(RS2["ST0114"].ToString()  , Strings.Len(RS2["ST0114"]) - 1)   + "',";
                                    SQl2 += "'" + " " + Strings.Right(RS2["ST0115"].ToString()  , Strings.Len(RS2["ST0115"]) - 1)   + "',";
                                    SQl2 += "'" + " " + Strings.Right(RS2["ST0116"].ToString()  , Strings.Len(RS2["ST0116"]) - 1)   + "'";
                                    SQl2 += ")";
                                    
                                    // 実行
                                    using (OleDbCommand cmd2 = new OleDbCommand(SQl2, DB2)) {
                                        cmd2.ExecuteNonQuery();
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
               
                File.Delete(Mdb_Module.DataBase1);
                File.Move(Mdb_Module.DataBase2, Mdb_Module.DataBase1);
            }

            // ウィンドウを非表示する
            VisibleWindow(false);
        }

        /// <summary>
        /// 「キャンセル」ボタンをクリック
        /// </summary>
        private void button_Cancel_Click(object sender, EventArgs e)
        {
            // 起動中ウィンドウを非表示する
            VisibleWindow(false);
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
                    m_Dlg = new A_Dcopy();
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
    }
}
