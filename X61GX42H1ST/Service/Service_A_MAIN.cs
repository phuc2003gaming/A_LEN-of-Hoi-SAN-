using C1.Win.C1FlexGrid;
using JRO;
using Microsoft.VisualBasic;
using Nicosu.Encode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X61GX42H1ST.Service
{
    public class Service_A_MAIN
    {
        public static DataTable SV_READ1()
        {
            DataTable dt = new DataTable();

            string sql = "select * from T_車種情報Ｍ order by 識別コード";
            string conStr = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={Mdb_Module.DataBase1};";

            try
            {
                using (OleDbConnection db = new OleDbConnection(conStr))
                {
                    db.Open();

                    using (OleDbCommand cmd = new OleDbCommand(sql, db))
                    using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception)
            {
                // MessageBox.Show(ex.Message);
            }

            return dt;
        }
        // 車種情報Ｔの指定されたデータを削除します
        public void Mdb_Del1(string Key_code1)
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
        public void Mdb_Del2(string Key_code1)
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
        // 車種情報転送データ読込み
        public void DOWN_LOAD_DATA3(string Key_code1, string Key_code2, String[] TBL01, String AgNeme=null)
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
                                    RP = CEncode.WcovHex3(Strings.Right(RS["レシピ№"].ToString(), Strings.Len(RS["レシピ№"]) - 1));
                                    if (Conversion.Val(RS["ST0101"]) != 0)
                                    {
                                        TBL01[1] += CEncode.WcovHex1(Strings.Right(RS["ST0101"].ToString(), Strings.Len(RS["ST0101"]) - 1)) + RP;
                                    }
                                    if (Conversion.Val(RS["ST0102"]) != 0)
                                    {
                                        TBL01[2] += CEncode.WcovHex1(Strings.Right(RS["ST0102"].ToString(), Strings.Len(RS["ST0102"]) - 1)) + RP;
                                    }
                                    if (Conversion.Val(RS["ST0103"]) != 0)
                                    {
                                        TBL01[3] += CEncode.WcovHex1(Strings.Right(RS["ST0103"].ToString(), Strings.Len(RS["ST0103"]) - 1)) + RP;
                                    }
                                    if (Conversion.Val(RS["ST0104"]) != 0)
                                    {
                                        TBL01[4] += CEncode.WcovHex1(Strings.Right(RS["ST0104"].ToString(), Strings.Len(RS["ST0104"]) - 1)) + RP;
                                    }
                                    if (Conversion.Val(RS["ST0105"]) != 0)
                                    {
                                        TBL01[5] += CEncode.WcovHex1(Strings.Right(RS["ST0105"].ToString(), Strings.Len(RS["ST0105"]) - 1)) + RP;
                                    }
                                    if (Conversion.Val(RS["ST0106"]) != 0)
                                    {
                                        TBL01[6] += CEncode.WcovHex1(Strings.Right(RS["ST0106"].ToString(), Strings.Len(RS["ST0106"]) - 1)) + RP;
                                    }
                                    if (Conversion.Val(RS["ST0107"]) != 0)
                                    {
                                        TBL01[7] += CEncode.WcovHex1(Strings.Right(RS["ST0107"].ToString(), Strings.Len(RS["ST0107"]) - 1)) + RP;
                                    }
                                    if (Conversion.Val(RS["ST0108"]) != 0)
                                    {
                                        TBL01[8] += CEncode.WcovHex1(Strings.Right(RS["ST0108"].ToString(), Strings.Len(RS["ST0108"]) - 1)) + RP;
                                    }
                                    if (Conversion.Val(RS["ST0109"]) != 0)
                                    {
                                        TBL01[9] +=     CEncode.WcovHex1(Strings.Right(RS["ST0109"].ToString(), Strings.Len(RS["ST0109"]) - 1)) + RP;
                                    }
                                    if (Conversion.Val(RS["ST0110"]) != 0)
                                    {
                                        TBL01[10] += CEncode.WcovHex1(Strings.Right(RS["ST0110"].ToString(), Strings.Len(RS["ST0110"]) - 1)) + RP;
                                    }
                                    if (Conversion.Val(RS["ST0111"]) != 0)
                                    {
                                        TBL01[11] += CEncode.WcovHex1(Strings.Right(RS["ST0111"].ToString(), Strings.Len(RS["ST0111"]) - 1)) + RP;
                                    }
                                    if (Conversion.Val(RS["ST0112"]) != 0)
                                    {
                                        TBL01[12] +=CEncode.WcovHex1(Strings.Right(RS["ST0112"].ToString(), Strings.Len(RS["ST0112"]) - 1)) + RP;
                                    }
                                    if (Conversion.Val(RS["ST0113"]) != 0)
                                    {
                                        TBL01[13] += CEncode.WcovHex1(Strings.Right(RS["ST0113"].ToString(), Strings.Len(RS["ST0113"]) - 1)) + RP;
                                    }
                                    if (Conversion.Val(RS["ST0114"]) != 0)
                                    {
                                        TBL01[14] += CEncode.WcovHex1(Strings.Right(RS["ST0114"].ToString(), Strings.Len(RS["ST0114"]) - 1)) + RP;
                                    }
                                    if (Conversion.Val(RS["ST0115"]) != 0)
                                    {
                                        TBL01[15] += CEncode.WcovHex1(Strings.Right(RS["ST0115"].ToString(), Strings.Len(RS["ST0115"]) - 1)) + RP;
                                    }
                                    if (Conversion.Val(RS["ST0116"]) != 0)
                                    {
                                        TBL01[16] += CEncode.WcovHex1(Strings.Right(RS["ST0116"].ToString(), Strings.Len(RS["ST0116"]) - 1)) + RP;
                                    }

                                    if (R_Cnt == 1)
                                    {
                                        TBL01[0] += CEncode.WcovHex4(Strings.Left(RS["ｼｰﾄﾀｲﾌﾟ"].ToString(), Strings.InStr(RS["ｼｰﾄﾀｲﾌﾟ"].ToString(), ":") - 1));
                                        TBL01[0] += CEncode.WcovHex4(Strings.Left(RS["AGﾀｲﾌﾟ"].ToString(), Strings.InStr(RS["AGﾀｲﾌﾟ"].ToString(), ":") - 1));
                                        TBL01[0] += CEncode.WcovHex4(Strings.Left(RS["ﾋｰﾀｰ"].ToString(), Strings.InStr(RS["ﾋｰﾀｰ"].ToString(), ":") - 1));
                                        TBL01[0] += CEncode.WcovHex4(Strings.Left(RS["ﾊﾞｯｸﾙ"].ToString(), Strings.InStr(RS["ﾊﾞｯｸﾙ"].ToString(), ":") - 1));
                                        TBL01[0] += CEncode.WcovHex4(Strings.Left(RS["ﾍｯﾄﾞﾚｽﾄ"].ToString(), Strings.InStr(RS["ﾍｯﾄﾞﾚｽﾄ"].ToString(), ":") - 1));
                                        TBL01[0] += CEncode.WcovHex4(Strings.Left(RS["着座ｾﾝｻｰ"].ToString(), Strings.InStr(RS["着座ｾﾝｻｰ"].ToString(), ":") - 1));
                                        TBL01[0] += CEncode.WcovHex4(Strings.Left(RS["空調"].ToString(), Strings.InStr(RS["空調"].ToString(), ":") - 1));
                                        TBL01[0] += CEncode.WcovHex4(Strings.Left(RS["表皮材"].ToString(), Strings.InStr(RS["表皮材"].ToString(), ":") - 1));
                                        TBL01[0] += CEncode.WcovHex4(Strings.Left(RS["色"].ToString(), Strings.InStr(RS["色"].ToString(), ":") - 1));
                                        TBL01[0] += CEncode.WcovHex4(Strings.Left(RS["ﾗﾝﾊﾞｰ"].ToString(), Strings.InStr(RS["ﾗﾝﾊﾞｰ"].ToString(), ":") - 1));
                                        TBL01[0] += CEncode.WcovHex4(Strings.Left(RS["背面ﾎﾟｹｯﾄ"].ToString(), Strings.InStr(RS["背面ﾎﾟｹｯﾄ"].ToString(), ":") - 1));
                                        TBL01[0] += CEncode.WcovHex4(Strings.Left(RS["ﾌｯﾄｳｴﾙﾗﾝﾌﾟ"].ToString(), Strings.InStr(RS["ﾌｯﾄｳｴﾙﾗﾝﾌﾟ"].ToString(), ":") - 1));
                                        TBL01[0] += CEncode.WcovHex4(Strings.Left(RS["ｱｰﾑﾚｽﾄ"].ToString(), Strings.InStr(RS["ｱｰﾑﾚｽﾄ"].ToString(), ":") - 1));
                                        TBL01[0] += CEncode.WcovHex4(Strings.Left(RS["ﾀﾝﾌﾞﾙ"].ToString(), Strings.InStr(RS["ﾀﾝﾌﾞﾙ"].ToString(), ":") - 1));
                                        TBL01[0] += CEncode.WcovHex4(Strings.Left(RS["ISOFIX"].ToString(), Strings.InStr(RS["ISOFIX"].ToString(), ":") - 1));
                                        TBL01[0] += CEncode.WcovHex4(Strings.Left(RS["ﾃｻﾞｰ"].ToString(), Strings.InStr(RS["ﾃｻﾞｰ"].ToString(), ":") - 1));
                                        TBL01[0] += CEncode.WcovHex4(Strings.Left(RS["ｵｯﾄﾏﾝ"].ToString(), Strings.InStr(RS["ｵｯﾄﾏﾝ"].ToString(), ":") - 1));

                                        if (RS["ｺﾝﾋﾞﾆﾌｯｸ"].ToString() != "")
                                        {
                                            TBL01[0] += CEncode.WcovHex4(Strings.Left(RS["ｺﾝﾋﾞﾆﾌｯｸ"].ToString(), Strings.InStr(RS["ｺﾝﾋﾞﾆﾌｯｸ"].ToString(), ":") - 1));
                                            TBL01[0] += CEncode.WcovHex4(Strings.Left(RS["ｻｲﾄﾞﾃｰﾌﾞﾙ"].ToString(), Strings.InStr(RS["ｻｲﾄﾞﾃｰﾌﾞﾙ"].ToString(), ":") - 1));
                                        }
                                        else
                                        {
                                            TBL01[0] += "00000000";
                                        }
                                        if (RS["ﾛﾎﾞｯﾄ"].ToString() != "")
                                        {
                                            TBL01[0] += CEncode.WcovHex4(Strings.Left(RS["ﾛﾎﾞｯﾄ"].ToString(), Strings.InStr(RS["ﾛﾎﾞｯﾄ"].ToString(), ":") - 1));
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
        // タグ情報マスタ転送データの読込み
        public void DOWN_LOAD_DATA1(string Key_code1)
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
        // 車種情報Ｍマスタの読込み
        // Ａ＿ラインデータベース最適化・修復
        public bool Mdb_Compact()
        {
            string ConDB1 = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={Mdb_Module.DataBase1};";
            string ConDB2 = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={Mdb_Module.DataBase2};";
            try
            {
                // 最適化,修復
                JetEngine DBEngine = new JetEngine();
                DBEngine.CompactDatabase(ConDB1, ConDB2 + "Jet OLEDB:Engine Type=5");

                File.Delete(Mdb_Module.DataBase1);
                File.Move(Mdb_Module.DataBase2, Mdb_Module.DataBase1);
                return true;
                

                
            }
            catch (Exception)
            {
                return false;
                throw;
            }
          
        }
    }

}
