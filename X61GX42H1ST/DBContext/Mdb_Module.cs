using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.OleDb;    // データベース
using Microsoft.VisualBasic;
using JRO;
using System.IO;

namespace X61GX42H1ST
{
    public static class Mdb_Module
    {
        public static string DataBase1 = string.Empty;
        public static string DataBase2 = string.Empty;
        public static string DataBase3 = string.Empty;

        //Public Const DataBase1 = "D:\X61GX42H1ST\X61GX42H1ST.mdb"
        //Public Const Database2 = "D:\X61GX42H1ST\X61GX42H1STBAKUP.mdb"

        //2026/01/08
        //Public Const DataBase1 = "D:\X61GX42H1ST\X61GX42HL.mdb"
        //Public Const Database2 = "D:\X61GX42H1ST\X61GX42HLBAKUP.mdb"


        public static string Select_DataBase;


//[2026/04/06][p.hoi][Add]=============================================================>>
//  PLCデータ読込、照合追加
//---------------------------------------------------------------------------------------
        //**********************************************************************
        // 
        // サブルーチン
        // 
        //**********************************************************************
        /// <summary>
        /// タグ情報マスタのクリア
        /// </summary>
        public static void TagInfoClear()
        {
            string SQl;
            string conStr = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={Mdb_Module.DataBase1};";

            try
            {
                using (OleDbConnection DB = new OleDbConnection(conStr))
                {
                    DB.Open(); // データベース接続

                    SQl  = "DELETE FROM T_タグ ";

                    using (OleDbCommand cmd = new OleDbCommand(SQl, DB))    // 検索条件をセット
                    {
                        // 実行
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
                ;
            }
        }


        /// <summary>
        /// タグ情報マスタの新規書込み/更新
        /// </summary>
        public static void TagInfoWrite(int no, Mx_sub.TAG_INFO dat)
        {
            string SQl;
            string conStr = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={Mdb_Module.DataBase1};";

            try
            {
                using (OleDbConnection DB = new OleDbConnection(conStr))
                {
                    DB.Open(); // データベース接続

                    SQl  = "select * from T_タグ ";
                    SQl += "WHERE Trim(№) = '" + no + "'";

                    using (OleDbCommand cmd = new OleDbCommand(SQl, DB))    // 検索条件をセット
                    {
                        using (OleDbDataReader RS = cmd.ExecuteReader())
                        {
                            string jigtype  = string.Empty;
                            string jiglr    = string.Empty;
                            string jigstock = string.Empty;

                            if((dat.JigLR    > 0) &&
                               (dat.JigStock > 0))
                            {
                                jigtype  = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.JigType ,  dat.JigType  );
                                jiglr    = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.JigLR   , (dat.JigLR   ));
                                jigstock = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.JigStock, (dat.JigStock));
                            }



                            if (RS.HasRows == false) {
                                // 新規書込み
                                SQl  = "INSERT INTO T_タグ ";
                                SQl += "(№,治具タイプ,治具ＬＲ,治具ストック段) VALUES ";
                                SQl += "(";
                                SQl += "'" + " " + no       + "',";
                                SQl += "'" + " " + jigtype  + "',";
                                SQl += "'" + " " + jiglr    + "',";
                                SQl += "'" + " " + jigstock + "'";
                                SQl += ")";
                            }
                            else
                            {
                                // 更新
                                SQl  = "UPDATE T_タグ SET ";
                                SQl += "治具タイプ = '"     + " " + jigtype  + "',";
                                SQl += "治具ＬＲ = '"       + " " + jiglr    + "',";
                                SQl += "治具ストック段 = '" + " " + jigstock + "' ";
                                SQl += "WHERE Trim(№) = '" + no + "';";

                            }

                            // 実行
                            using (OleDbCommand cmd2 = new OleDbCommand(SQl, DB))
                            {
                                cmd2.ExecuteNonQuery();
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
        public static void TagInfoRead(ref Mx_sub.TAG_INFO[] dat)
        {
            Mx_sub.TAG_INFO wrk;
            string              str;
            string              SQl;
            string              conStr  = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={Mdb_Module.DataBase1};";

            try
            {
                using (OleDbConnection DB = new OleDbConnection(conStr))
                {
                    DB.Open(); // データベース接続

                    SQl = "select * from T_タグ ";
                    SQl += "order by val(№)";

                    using (OleDbCommand cmd = new OleDbCommand(SQl, DB))
                    {
                        using (OleDbDataReader RS = cmd.ExecuteReader())
                        {
                            while (RS.Read())
                            {
                                wrk = Mx_sub.TAG_INFO.Empty;

                                if (Convert.ToString(RS["治具タイプ"]).Trim().Length > 1) {
                                    str         = Strings.Right(RS["治具タイプ"].ToString(), Strings.Len(RS["治具タイプ"]) - 1);
                                    wrk.JigType = (UInt16)Conversion.Val(Strings.Left(str, Strings.InStr(str, ":") - 1));
                                }

                                if (Convert.ToString(RS["治具ＬＲ"]).Trim().Length > 1)
                                {
                                    str         = Strings.Right(RS["治具ＬＲ"].ToString(), Strings.Len(RS["治具ＬＲ"]) - 1);
                                    wrk.JigLR   = (UInt16)Conversion.Val(Strings.Left(str, Strings.InStr(str, ":") - 1));
                                }

                                if (Convert.ToString(RS["治具ストック段"]).Trim().Length > 1)
                                {
                                    str          = Strings.Right(RS["治具ストック段"].ToString(), Strings.Len(RS["治具ストック段"]) - 1);
                                    wrk.JigStock = (UInt16)Conversion.Val(Strings.Left(str, Strings.InStr(str, ":") - 1));
                                }

                                // データコーピ
                                dat[Convert.ToInt32(RS["№"]) - 1] = new Mx_sub.TAG_INFO(wrk);
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
        /// 車種情報Ｔのクリア
        /// </summary>
        public static void Vehicle_T_Clear()
        {
            string SQl;
            string conStr = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={Mdb_Module.DataBase1};";

            try
            {
                using (OleDbConnection DB = new OleDbConnection(conStr))
                {
                    DB.Open(); // データベース接続

                    SQl = "DELETE FROM T_車種情報Ｔ ";

                    using (OleDbCommand cmd = new OleDbCommand(SQl, DB))    // 検索条件をセット
                    {
                        // 実行
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
                ;
            }
        }

        // 車種情報Ｔの新規書込み/更新
        public static void Vehicle_T_Write(int patn, Mx_sub.VEHICLE_INFO dat)
        {
            int      recp_no;
            string[] st_no = new string[16];
            int      taggp;
            int      idx;
            string   SP;
            string   SQl = string.Empty;
            string   conStr = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={Mdb_Module.DataBase1};";


            try
            {
                using (OleDbConnection DB = new OleDbConnection(conStr))
                {
                    DB.Open(); // データベース接続

                    for (int lr = 0; lr < 2; lr++) {
                        for (int i = 1; i <= 16; i++) {  // 最大数25
                            // 初期化
                            taggp   = 0;
                            recp_no = 0;
                            for(int wrk = 0; wrk < 16; wrk++) {
                                st_no[wrk] = string.Empty;
                            }
                            
                            for (idx = 0; idx < 25; idx++) {
                                // レシピ読込み
                                if(lr == 0) {
                                    recp_no = dat.LhDat.GetRecipeNo(i, idx);                      // レシピＮｏ
                                    if(recp_no > 0) {
                                        st_no[i - 1] = dat.LhDat.GetSt(i, idx).ToString();        // 回数
                                    }
                                    else break;

                                    // タグＧＰ
                                    switch (patn) {
                                        case 1: taggp = 0; break;
                                        case 2: taggp = 1; break;
                                        case 3: taggp = 2; break;
                                    }
                                }
                                else {
                                    recp_no = dat.RhDat.GetRecipeNo(i, idx);                      // レシピＮｏ
                                    if(recp_no > 0) {
                                        st_no[i - 1] = dat.RhDat.GetSt(i, idx).ToString();        // 回数
                                    }
                                    else break;

                                    // タグＧＰ
                                    switch (patn) {
                                        case 1: taggp = 3; break;
                                        case 2: taggp = 4; break;
                                        case 3: taggp = 5; break;
                                    }
                                }

                                if(recp_no > 0) {
                                    SQl  = "select * from T_車種情報Ｔ ";
                                    SQl += "WHERE Trim(識別コード) = '" + dat.Head.IdenCode + "'" + " AND " + "Trim(タブＧＰ) = '" + taggp + "'" + " AND " + "Trim(レシピ№) = '" + recp_no + "'";

                                    using (OleDbCommand cmd = new OleDbCommand(SQl, DB))    // 検索条件をセット
                                    {
                                        using (OleDbDataReader RS = cmd.ExecuteReader())
                                        {
                                            if (RS.HasRows != true)
                                            {
                                                // 新規書込み
                                                SQl  = "INSERT INTO T_車種情報Ｔ ";
                                                SQl += "(識別コード,タブＧＰ,治具タイプ,治具切出し順,1st治具ストック段,2nd治具ストック段,向先,ｼｰﾄﾀｲﾌﾟ,AGﾀｲﾌﾟ,ﾋｰﾀｰ,ﾊﾞｯｸﾙ,ﾍｯﾄﾞﾚｽﾄ,着座ｾﾝｻｰ,空調,";
                                                SQl += "表皮材,色,ﾗﾝﾊﾞｰ,背面ﾎﾟｹｯﾄ,ﾌｯﾄｳｴﾙﾗﾝﾌﾟ,ｱｰﾑﾚｽﾄ,ﾀﾝﾌﾞﾙ,ISOFIX,ﾃｻﾞｰ,ｵｯﾄﾏﾝ,ｺﾝﾋﾞﾆﾌｯｸ,ｻｲﾄﾞﾃｰﾌﾞﾙ,ﾛﾎﾞｯﾄ,";
                                                SQl += "作業パターン,レシピ№,ST0101,ST0102,ST0103,ST0104,ST0105,ST0106,ST0107,ST0108,ST0109,ST0110,ST0111,ST0112,ST0113,ST0114,ST0115,ST0116) VALUES ";
                                                SQl += "(";
                                                SQl += "'" + dat.Head.IdenCode      + "',";
                                                SQl += "'" + Conversion.Str(taggp)  + "' ,";
                                                SQl += "'" + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.JigType,   dat.Head.JigType  ) + "',";
                                                SQl += "'" + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.JigOrder, (dat.Head.JigOrder)) + "',";
                                                SQl += "'" + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.JigStock, (dat.Head.Jig1st  )) + "',";
                                                SQl += "'" + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.JigStock, (dat.Head.Jig2nd  )) + "',";
                                                SQl += "'" + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Dest,     (dat.Head.Dest    )) + "',";

                                                switch (taggp) {
                                                    case 0: // １ｓｔ側ｼｰﾄﾀｲﾌﾟ
                                                    case 1:
                                                    case 2:
                                                        SQl += "'" + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.SeatType     , dat.LhDat.SeatType    ) + "',";
                                                        SQl += "'" + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.AgType       , dat.LhDat.AgType      ) + "',";
                                                        SQl += "'" + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Heater       , dat.LhDat.Heater      ) + "',";
                                                        SQl += "'" + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Buckle       , dat.LhDat.Buckle      ) + "',";
                                                        SQl += "'" + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Headrest     , dat.LhDat.Headrest    ) + "',";
                                                        SQl += "'" + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.SeatSen      , dat.LhDat.SeatSen     ) + "',";
                                                        SQl += "'" + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.AirCond      , dat.LhDat.AirCond     ) + "',";
                                                        SQl += "'" + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Upholstery   , dat.LhDat.Upholstery  ) + "',";
                                                        SQl += "'" + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.TorqueWrench , dat.LhDat.TorqueWrench) + "',";
                                                        SQl += "'" + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Lumbar       , dat.LhDat.Lumbar      ) + "',";
                                                        SQl += "'" + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.BackPock     , dat.LhDat.BackPock    ) + "',";
                                                        SQl += "'" + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.FootwellLamp , dat.LhDat.FootwellLamp) + "',";
                                                        SQl += "'" + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Armrest      , dat.LhDat.Armrest     ) + "',";
                                                        SQl += "'" + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Tumble       , dat.LhDat.Tumble      ) + "',";
                                                        SQl += "'" + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.IsoFix       , dat.LhDat.IsoFix      ) + "',";
                                                        SQl += "'" + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Tether       , dat.LhDat.Tether      ) + "',";
                                                        SQl += "'" + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Ottoman      , dat.LhDat.Ottoman     ) + "',";
                                                        SQl += "'" + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.ConvHook     , dat.LhDat.ConvHook    ) + "',";
                                                        SQl += "'" + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.SideTable    , dat.LhDat.SideTable   ) + "',";
                                                        SQl += "'" + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Robot        , dat.LhDat.Robot       ) + "',";
                                                        break;
                                                    case 3: // ２ｎｄ側ｼｰﾄﾀｲﾌﾟ
                                                    case 4:
                                                    case 5:
                                                        SQl += "'" + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.SeatType     , dat.RhDat.SeatType    ) + "',";
                                                        SQl += "'" + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.AgType       , dat.RhDat.AgType      ) + "',";
                                                        SQl += "'" + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Heater       , dat.RhDat.Heater      ) + "',";
                                                        SQl += "'" + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Buckle       , dat.RhDat.Buckle      ) + "',";
                                                        SQl += "'" + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Headrest     , dat.RhDat.Headrest    ) + "',";
                                                        SQl += "'" + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.SeatSen      , dat.RhDat.SeatSen     ) + "',";
                                                        SQl += "'" + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.AirCond      , dat.RhDat.AirCond     ) + "',";
                                                        SQl += "'" + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Upholstery   , dat.RhDat.Upholstery  ) + "',";
                                                        SQl += "'" + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.TorqueWrench , dat.RhDat.TorqueWrench) + "',";
                                                        SQl += "'" + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Lumbar       , dat.RhDat.Lumbar      ) + "',";
                                                        SQl += "'" + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.BackPock     , dat.RhDat.BackPock    ) + "',";
                                                        SQl += "'" + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.FootwellLamp , dat.RhDat.FootwellLamp) + "',";
                                                        SQl += "'" + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Armrest      , dat.RhDat.Armrest     ) + "',";
                                                        SQl += "'" + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Tumble       , dat.RhDat.Tumble      ) + "',";
                                                        SQl += "'" + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.IsoFix       , dat.RhDat.IsoFix      ) + "',";
                                                        SQl += "'" + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Tether       , dat.RhDat.Tether      ) + "',";
                                                        SQl += "'" + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Ottoman      , dat.RhDat.Ottoman     ) + "',";
                                                        SQl += "'" + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.ConvHook     , dat.RhDat.ConvHook    ) + "',";
                                                        SQl += "'" + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.SideTable    , dat.RhDat.SideTable   ) + "',";
                                                        SQl += "'" + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Robot        , dat.RhDat.Robot       ) + "',";
                                                        break;
                                                }

                                                switch (taggp) { // 作業パターン
                                                    case 0:
                                                    case 3:
                                                        SP = " 1";
                                                        break;
                                                    case 1:
                                                    case 4:
                                                        SP = " 2";
                                                        break;
                                                    case 2:
                                                    case 5:
                                                        SP = " 3";
                                                        break;
                                                    default:
                                                        SP = string.Empty;
                                                        break;
                                                }
                                                SQl += "' " + SP + "' ,";

                                                SQl += "'" + " " + recp_no   + "',"; // レシピ№
                                                SQl += "'" + " " + st_no[ 0] + "',"; // 1,作業回数
                                                SQl += "'" + " " + st_no[ 1] + "',"; // 2
                                                SQl += "'" + " " + st_no[ 2] + "',"; // 3
                                                SQl += "'" + " " + st_no[ 3] + "',"; // 4
                                                SQl += "'" + " " + st_no[ 4] + "',"; // 5
                                                SQl += "'" + " " + st_no[ 5] + "',"; // 6
                                                SQl += "'" + " " + st_no[ 6] + "',"; // 7
                                                SQl += "'" + " " + st_no[ 7] + "',"; // 8
                                                SQl += "'" + " " + st_no[ 8] + "',"; // 9
                                                SQl += "'" + " " + st_no[ 9] + "',"; // 10
                                                SQl += "'" + " " + st_no[10] + "',"; // 11
                                                SQl += "'" + " " + st_no[11] + "',"; // 12
                                                SQl += "'" + " " + st_no[12] + "',"; // 13
                                                SQl += "'" + " " + st_no[13] + "',"; // 14
                                                SQl += "'" + " " + st_no[14] + "',"; // 15
                                                SQl += "'" + " " + st_no[15] + "'";  // 16
                                                SQl += ")";
                                            }
                                            else
                                            {
                                                // 更新
                                                SQl  = "UPDATE T_車種情報Ｔ SET ";
                                                SQl += "識別コード = '"        + dat.Head.IdenCode     + "',";
                                                SQl += "タブＧＰ = '"          + Conversion.Str(taggp) + "',";
                                                SQl += "治具タイプ = '"        + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.JigType,   dat.Head.JigType  ) + "',";
                                                SQl += "治具切出し順 = '"      + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.JigOrder, (dat.Head.JigOrder)) + "',";
                                                SQl += "1st治具ストック段 = '" + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.JigStock, (dat.Head.Jig1st  )) + "',";
                                                SQl += "2nd治具ストック段 = '" + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.JigStock, (dat.Head.Jig2nd  )) + "',";
                                                SQl += "向先 = '"              + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Dest,     (dat.Head.Dest    )) + "',";
                                                switch (taggp)
                                                {
                                                    case 0: // １ｓｔ側ｼｰﾄﾀｲﾌﾟ
                                                    case 1:
                                                    case 2:
                                                        SQl += "ｼｰﾄﾀｲﾌﾟ = '"       + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.SeatType     , dat.LhDat.SeatType    ) + "',";
                                                        SQl += "AGﾀｲﾌﾟ = '"        + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.AgType       , dat.LhDat.AgType      ) + "',";
                                                        SQl += "ﾋｰﾀｰ = '"          + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Heater       , dat.LhDat.Heater      ) + "',";
                                                        SQl += "ﾊﾞｯｸﾙ = '"         + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Buckle       , dat.LhDat.Buckle      ) + "',";
                                                        SQl += "ﾍｯﾄﾞﾚｽﾄ = '"       + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Headrest     , dat.LhDat.Headrest    ) + "',";
                                                        SQl += "着座ｾﾝｻｰ = '"      + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.SeatSen      , dat.LhDat.SeatSen     ) + "',";
                                                        SQl += "空調 = '"          + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.AirCond      , dat.LhDat.AirCond     ) + "',";
                                                        SQl += "表皮材 = '"        + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Upholstery   , dat.LhDat.Upholstery  ) + "',";
                                                        SQl += "色 = '"            + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.TorqueWrench , dat.LhDat.TorqueWrench) + "',";
                                                        SQl += "ﾗﾝﾊﾞｰ = '"         + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Lumbar       , dat.LhDat.Lumbar      ) + "',";
                                                        SQl += "背面ﾎﾟｹｯﾄ = '"     + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.BackPock     , dat.LhDat.BackPock    ) + "',";
                                                        SQl += "ﾌｯﾄｳｴﾙﾗﾝﾌﾟ = '"    + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.FootwellLamp , dat.LhDat.FootwellLamp) + "',";
                                                        SQl += "ｱｰﾑﾚｽﾄ = '"        + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Armrest      , dat.LhDat.Armrest     ) + "',";
                                                        SQl += "ﾀﾝﾌﾞﾙ = '"         + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Tumble       , dat.LhDat.Tumble      ) + "',";
                                                        SQl += "ISOFIX = '"        + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.IsoFix       , dat.LhDat.IsoFix      ) + "',";
                                                        SQl += "ﾃｻﾞｰ = '"          + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Tether       , dat.LhDat.Tether      ) + "',";
                                                        SQl += "ｵｯﾄﾏﾝ = '"         + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Ottoman      , dat.LhDat.Ottoman     ) + "',";
                                                        SQl += "ｺﾝﾋﾞﾆﾌｯｸ = '"      + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.ConvHook     , dat.LhDat.ConvHook    ) + "',";
                                                        SQl += "ｻｲﾄﾞﾃｰﾌﾞﾙ = '"     + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.SideTable    , dat.LhDat.SideTable   ) + "',";
                                                        SQl += "ﾛﾎﾞｯﾄ = '"         + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Robot        , dat.LhDat.Robot       ) + "',";
                                                        break;
                                                    case 3: // ２ｎｄ側ｼｰﾄﾀｲﾌﾟ
                                                    case 4:
                                                    case 5:
                                                        SQl += "ｼｰﾄﾀｲﾌﾟ = '"      + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.SeatType     , dat.LhDat.SeatType    ) + "',";
                                                        SQl += "AGﾀｲﾌﾟ = '"       + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.AgType       , dat.LhDat.AgType      ) + "',";
                                                        SQl += "ﾋｰﾀｰ = '"         + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Heater       , dat.LhDat.Heater      ) + "',";
                                                        SQl += "ﾊﾞｯｸﾙ = '"        + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Buckle       , dat.LhDat.Buckle      ) + "',";
                                                        SQl += "ﾍｯﾄﾞﾚｽﾄ = ' "     + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Headrest     , dat.LhDat.Headrest    ) + "',";
                                                        SQl += "着座ｾﾝｻｰ = '"     + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.SeatSen      , dat.LhDat.SeatSen     ) + "',";
                                                        SQl += "空調 = '"         + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.AirCond      , dat.LhDat.AirCond     ) + "',";
                                                        SQl += "表皮材 = '"       + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Upholstery   , dat.LhDat.Upholstery  ) + "',";
                                                        SQl += "色 = '"           + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.TorqueWrench , dat.LhDat.TorqueWrench) + "',";
                                                        SQl += "ﾗﾝﾊﾞｰ = '"        + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Lumbar       , dat.LhDat.Lumbar      ) + "',";
                                                        SQl += "背面ﾎﾟｹｯﾄ = '"    + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.BackPock     , dat.LhDat.BackPock    ) + "',";
                                                        SQl += "ﾌｯﾄｳｴﾙﾗﾝﾌﾟ = '"   + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.FootwellLamp , dat.LhDat.FootwellLamp) + "',";
                                                        SQl += "ｱｰﾑﾚｽﾄ = '"       + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Armrest      , dat.LhDat.Armrest     ) + "',";
                                                        SQl += "ﾀﾝﾌﾞﾙ = '"        + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Tumble       , dat.LhDat.Tumble      ) + "',";
                                                        SQl += "ISOFIX = '"       + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.IsoFix       , dat.LhDat.IsoFix      ) + "',";
                                                        SQl += "ﾃｻﾞｰ = '"         + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Tether       , dat.LhDat.Tether      ) + "',";
                                                        SQl += "ｵｯﾄﾏﾝ = '"        + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Ottoman      , dat.LhDat.Ottoman     ) + "',";
                                                        SQl += "ｺﾝﾋﾞﾆﾌｯｸ = '"     + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.ConvHook     , dat.LhDat.ConvHook    ) + "',";
                                                        SQl += "ｻｲﾄﾞﾃｰﾌﾞﾙ = '"    + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.SideTable    , dat.LhDat.SideTable   ) + "',";
                                                        SQl += "ﾛﾎﾞｯﾄ = '"        + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Robot        , dat.LhDat.Robot       ) + "',";
                                                        break;
                                                }

                                                switch (taggp) {
                                                    case 0:
                                                    case 3:
                                                        SP = " 1";
                                                        break;
                                                    case 1:
                                                    case 4:
                                                        SP = " 2";
                                                        break;
                                                    case 2:
                                                    case 5:
                                                        SP = " 3";
                                                        break;
                                                    default:
                                                        SP = string.Empty;
                                                        break;
                                                }
                                                SQl += "作業パターン = ' " + SP + "' ,";

                                                SQl += "レシピ№ = '" + " "   + recp_no   + "',"; // レシピ№
                                                SQl += "ST0101 = '"   + " "   + st_no[ 0] + "',"; // 1,作業回数
                                                SQl += "ST0102 = '"   + " "   + st_no[ 1] + "',"; // 2
                                                SQl += "ST0103 = '"   + " "   + st_no[ 2] + "',"; // 3
                                                SQl += "ST0104 = '"   + " "   + st_no[ 3] + "',"; // 4
                                                SQl += "ST0105 = '"   + " "   + st_no[ 4] + "',"; // 5
                                                SQl += "ST0106 = '"   + " "   + st_no[ 5] + "',"; // 6
                                                SQl += "ST0107 = '"   + " "   + st_no[ 6] + "',"; // 7
                                                SQl += "ST0108 = '"   + " "   + st_no[ 7] + "',"; // 8
                                                SQl += "ST0109 = '"   + " "   + st_no[ 8] + "',"; // 9
                                                SQl += "ST0110 = '"   + " "   + st_no[ 9] + "',"; // 10
                                                SQl += "ST0111 = '"   + " "   + st_no[10] + "',"; // 11
                                                SQl += "ST0112 = '"   + " "   + st_no[11] + "',"; // 12
                                                SQl += "ST0113 = '"   + " "   + st_no[12] + "',"; // 13
                                                SQl += "ST0114 = '"   + " "   + st_no[13] + "',"; // 14
                                                SQl += "ST0115 = '"   + " "   + st_no[14] + "',"; // 15
                                                SQl += "ST0116 = '"   + " "   + st_no[15] + "' "; // 16
                                                SQl += "WHERE Trim(識別コード) = '" + dat.Head.IdenCode + "'" + " AND " + "Trim(タブＧＰ) = '" + taggp + "'" + " AND " + "Trim(レシピ№) = '" + recp_no + "';";
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
        public static int Vehicle_T_Read(int patn, ref Mx_sub.VEHICLE_INFO[] dat)
        {
            int                 ret;
            int                 taggp;
            int                 lhtaggp;
            int                 rhtaggp;
            int[]               rowl;
            int[]               rowr;
            int                 rp;
            bool                hasdat;
            Mx_sub.VEHICLE_INFO wrk;
            string              code;
            string              SQl;
            string              conStr = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={Mdb_Module.DataBase1};";

            rowl   = new int[16];
            rowr   = new int[16];
            ret    = 0;
            code   = string.Empty;
            wrk    = new Mx_sub.VEHICLE_INFO(null);
            hasdat = false;

            for (int ii = 0; ii < rowl.Length; ii++)
            {
                rowl[ii] = 0;
                rowr[ii] = 0;
            }

            // タグＧＰ
            switch (patn)
            {
                case 1:  { lhtaggp = 0; rhtaggp = 3; } break;
                case 2:  { lhtaggp = 1; rhtaggp = 4; } break;
                case 3:  { lhtaggp = 2; rhtaggp = 5; } break;
                default: { lhtaggp = 0; rhtaggp = 0; } break;
            }

            try
            {
                using (OleDbConnection DB = new OleDbConnection(conStr))
                {
                    DB.Open(); // データベース接続

                    SQl  = "select * from T_車種情報Ｔ ";
                    SQl += "where (trim(タブＧＰ) = '" + lhtaggp + "') OR (trim(タブＧＰ) = '" + rhtaggp + "') ";
                    SQl += "order by 識別コード, val(タブＧＰ), val(レシピ№)";

                    using (OleDbCommand cmd = new OleDbCommand(SQl, DB))
                    {
                        using (OleDbDataReader RS = cmd.ExecuteReader())
                        {
                            while (RS.Read()) {
                                if(RS["識別コード"].ToString().Length > 1) {
                                    taggp  = (int)Conversion.Val(RS["タブＧＰ"]);

                                    if (string.Compare(RS["識別コード"].ToString().Trim(), code) != 0) {

                                        if(hasdat == true) {
                                            // データコーピ
                                            dat[ret] = new Mx_sub.VEHICLE_INFO(wrk);

                                            // データクリア
                                            wrk    = Mx_sub.VEHICLE_INFO.Empty;
                                            hasdat = false;
                                            for (int ii = 0; ii < rowl.Length; ii++) {
                                                rowl[ii] = 0;
                                                rowr[ii] = 0;
                                            }
                                            // カウンタアップ
                                            ret++;
                                        }

                                        // ヘッダ部
                                        wrk.Head.IdenCode = RS["識別コード"].ToString().Trim();
                                        wrk.Head.JigType  = (UInt16)Conversion.Val(Strings.Left(RS["治具タイプ"       ].ToString(), Strings.InStr(RS["治具タイプ"       ].ToString(), ":") - 1));
                                        wrk.Head.JigOrder = (UInt16)Conversion.Val(Strings.Left(RS["治具切出し順"     ].ToString(), Strings.InStr(RS["治具切出し順"     ].ToString(), ":") - 1));
                                        wrk.Head.Jig1st   = (UInt16)Conversion.Val(Strings.Left(RS["1st治具ストック段"].ToString(), Strings.InStr(RS["1st治具ストック段"].ToString(), ":") - 1));
                                        wrk.Head.Jig2nd   = (UInt16)Conversion.Val(Strings.Left(RS["2nd治具ストック段"].ToString(), Strings.InStr(RS["2nd治具ストック段"].ToString(), ":") - 1));
                                        wrk.Head.Dest     = (UInt16)Conversion.Val(Strings.Left(RS["向先"             ].ToString(), Strings.InStr(RS["向先"             ].ToString(), ":") - 1));

                                        // 識別コード記憶
                                        code = wrk.Head.IdenCode;
                                    }

                                    // 詳細部
                                    switch (taggp) {
                                        case 0: // 1st側
                                        case 1:
                                        case 2:
                                            wrk.LhDat.SeatType     = (UInt16)Conversion.Val(Strings.Left(RS["ｼｰﾄﾀｲﾌﾟ"   ].ToString(), Strings.InStr(RS["ｼｰﾄﾀｲﾌﾟ"   ].ToString(), ":") - 1));
                                            wrk.LhDat.AgType       = (UInt16)Conversion.Val(Strings.Left(RS["AGﾀｲﾌﾟ"    ].ToString(), Strings.InStr(RS["AGﾀｲﾌﾟ"    ].ToString(), ":") - 1));
                                            wrk.LhDat.Heater       = (UInt16)Conversion.Val(Strings.Left(RS["ﾋｰﾀｰ"      ].ToString(), Strings.InStr(RS["ﾋｰﾀｰ"      ].ToString(), ":") - 1));
                                            wrk.LhDat.Buckle       = (UInt16)Conversion.Val(Strings.Left(RS["ﾊﾞｯｸﾙ"     ].ToString(), Strings.InStr(RS["ﾊﾞｯｸﾙ"     ].ToString(), ":") - 1));
                                            wrk.LhDat.Headrest     = (UInt16)Conversion.Val(Strings.Left(RS["ﾍｯﾄﾞﾚｽﾄ"   ].ToString(), Strings.InStr(RS["ﾍｯﾄﾞﾚｽﾄ"   ].ToString(), ":") - 1));
                                            wrk.LhDat.SeatSen      = (UInt16)Conversion.Val(Strings.Left(RS["着座ｾﾝｻｰ"  ].ToString(), Strings.InStr(RS["着座ｾﾝｻｰ"  ].ToString(), ":") - 1));
                                            wrk.LhDat.AirCond      = (UInt16)Conversion.Val(Strings.Left(RS["空調"      ].ToString(), Strings.InStr(RS["空調"      ].ToString(), ":") - 1));
                                            wrk.LhDat.Upholstery   = (UInt16)Conversion.Val(Strings.Left(RS["表皮材"    ].ToString(), Strings.InStr(RS["表皮材"    ].ToString(), ":") - 1));
                                            wrk.LhDat.TorqueWrench = (UInt16)Conversion.Val(Strings.Left(RS["色"        ].ToString(), Strings.InStr(RS["色"        ].ToString(), ":") - 1));
                                            wrk.LhDat.Lumbar       = (UInt16)Conversion.Val(Strings.Left(RS["ﾗﾝﾊﾞｰ"     ].ToString(), Strings.InStr(RS["ﾗﾝﾊﾞｰ"     ].ToString(), ":") - 1));
                                            wrk.LhDat.BackPock     = (UInt16)Conversion.Val(Strings.Left(RS["背面ﾎﾟｹｯﾄ" ].ToString(), Strings.InStr(RS["背面ﾎﾟｹｯﾄ" ].ToString(), ":") - 1));
                                            wrk.LhDat.FootwellLamp = (UInt16)Conversion.Val(Strings.Left(RS["ﾌｯﾄｳｴﾙﾗﾝﾌﾟ"].ToString(), Strings.InStr(RS["ﾌｯﾄｳｴﾙﾗﾝﾌﾟ"].ToString(), ":") - 1));
                                            wrk.LhDat.Armrest      = (UInt16)Conversion.Val(Strings.Left(RS["ｱｰﾑﾚｽﾄ"    ].ToString(), Strings.InStr(RS["ｱｰﾑﾚｽﾄ"    ].ToString(), ":") - 1));
                                            wrk.LhDat.Tumble       = (UInt16)Conversion.Val(Strings.Left(RS["ﾀﾝﾌﾞﾙ"     ].ToString(), Strings.InStr(RS["ﾀﾝﾌﾞﾙ"     ].ToString(), ":") - 1));
                                            wrk.LhDat.IsoFix       = (UInt16)Conversion.Val(Strings.Left(RS["ISOFIX"    ].ToString(), Strings.InStr(RS["ISOFIX"    ].ToString(), ":") - 1));
                                            wrk.LhDat.Tether       = (UInt16)Conversion.Val(Strings.Left(RS["ﾃｻﾞｰ"      ].ToString(), Strings.InStr(RS["ﾃｻﾞｰ"      ].ToString(), ":") - 1));
                                            wrk.LhDat.Ottoman      = (UInt16)Conversion.Val(Strings.Left(RS["ｵｯﾄﾏﾝ"     ].ToString(), Strings.InStr(RS["ｵｯﾄﾏﾝ"     ].ToString(), ":") - 1));

                                            if (RS["ｺﾝﾋﾞﾆﾌｯｸ"].ToString() != "") {
                                                wrk.LhDat.ConvHook  = (UInt16)Conversion.Val(Strings.Left(RS["ｺﾝﾋﾞﾆﾌｯｸ" ].ToString(), Strings.InStr(RS["ｺﾝﾋﾞﾆﾌｯｸ" ].ToString(), ":") - 1));
                                                wrk.LhDat.SideTable = (UInt16)Conversion.Val(Strings.Left(RS["ｻｲﾄﾞﾃｰﾌﾞﾙ"].ToString(), Strings.InStr(RS["ｻｲﾄﾞﾃｰﾌﾞﾙ"].ToString(), ":") - 1));
                                            }
                                            else {
                                                wrk.LhDat.ConvHook  = 0;
                                                wrk.LhDat.SideTable = 0;
                                            }

                                            if (RS["ﾛﾎﾞｯﾄ"].ToString() != "") {
                                                wrk.LhDat.Robot     = (UInt16)Conversion.Val(Strings.Left(RS["ﾛﾎﾞｯﾄ"].ToString(), Strings.InStr(RS["ﾛﾎﾞｯﾄ"].ToString(), ":") - 1));
                                            }
                                            else {
                                                wrk.LhDat.Robot     = 0;
                                            }

                                            //レシピ
                                            rp = (int)Conversion.Val(RS["レシピ№"]);
                                            for (int ii = 1; ii <= 16; ii++)
                                            {
                                                string str = string.Format("ST01{0:00}", ii);
                                                if(Conversion.Val(RS[str]) != 0) {
                                                    wrk.LhDat.SetSt(ii, rowl[ii - 1], (UInt16)(((UInt16)Conversion.Val(RS[str]) << 12) + rp));
                                                    rowl[ii - 1]++;
                                                    break;
                                                }
                                            }

                                            hasdat = true;
                                            break;

                                        default:
                                            wrk.RhDat.SeatType      = (UInt16)Conversion.Val(Strings.Left(RS["ｼｰﾄﾀｲﾌﾟ"   ].ToString(), Strings.InStr(RS["ｼｰﾄﾀｲﾌﾟ"   ].ToString(), ":") - 1));
                                            wrk.RhDat.AgType        = (UInt16)Conversion.Val(Strings.Left(RS["AGﾀｲﾌﾟ"    ].ToString(), Strings.InStr(RS["AGﾀｲﾌﾟ"    ].ToString(), ":") - 1));
                                            wrk.RhDat.Heater        = (UInt16)Conversion.Val(Strings.Left(RS["ﾋｰﾀｰ"      ].ToString(), Strings.InStr(RS["ﾋｰﾀｰ"      ].ToString(), ":") - 1));
                                            wrk.RhDat.Buckle        = (UInt16)Conversion.Val(Strings.Left(RS["ﾊﾞｯｸﾙ"     ].ToString(), Strings.InStr(RS["ﾊﾞｯｸﾙ"     ].ToString(), ":") - 1));
                                            wrk.RhDat.Headrest      = (UInt16)Conversion.Val(Strings.Left(RS["ﾍｯﾄﾞﾚｽﾄ"   ].ToString(), Strings.InStr(RS["ﾍｯﾄﾞﾚｽﾄ"   ].ToString(), ":") - 1));
                                            wrk.RhDat.SeatSen       = (UInt16)Conversion.Val(Strings.Left(RS["着座ｾﾝｻｰ"  ].ToString(), Strings.InStr(RS["着座ｾﾝｻｰ"  ].ToString(), ":") - 1));
                                            wrk.RhDat.AirCond       = (UInt16)Conversion.Val(Strings.Left(RS["空調"      ].ToString(), Strings.InStr(RS["空調"      ].ToString(), ":") - 1));
                                            wrk.RhDat.Upholstery    = (UInt16)Conversion.Val(Strings.Left(RS["表皮材"    ].ToString(), Strings.InStr(RS["表皮材"    ].ToString(), ":") - 1));
                                            wrk.RhDat.TorqueWrench  = (UInt16)Conversion.Val(Strings.Left(RS["色"        ].ToString(), Strings.InStr(RS["色"        ].ToString(), ":") - 1));
                                            wrk.RhDat.Lumbar        = (UInt16)Conversion.Val(Strings.Left(RS["ﾗﾝﾊﾞｰ"     ].ToString(), Strings.InStr(RS["ﾗﾝﾊﾞｰ"     ].ToString(), ":") - 1));
                                            wrk.RhDat.BackPock      = (UInt16)Conversion.Val(Strings.Left(RS["背面ﾎﾟｹｯﾄ" ].ToString(), Strings.InStr(RS["背面ﾎﾟｹｯﾄ" ].ToString(), ":") - 1));
                                            wrk.RhDat.FootwellLamp  = (UInt16)Conversion.Val(Strings.Left(RS["ﾌｯﾄｳｴﾙﾗﾝﾌﾟ"].ToString(), Strings.InStr(RS["ﾌｯﾄｳｴﾙﾗﾝﾌﾟ"].ToString(), ":") - 1));
                                            wrk.RhDat.Armrest       = (UInt16)Conversion.Val(Strings.Left(RS["ｱｰﾑﾚｽﾄ"    ].ToString(), Strings.InStr(RS["ｱｰﾑﾚｽﾄ"    ].ToString(), ":") - 1));
                                            wrk.RhDat.Tumble        = (UInt16)Conversion.Val(Strings.Left(RS["ﾀﾝﾌﾞﾙ"     ].ToString(), Strings.InStr(RS["ﾀﾝﾌﾞﾙ"     ].ToString(), ":") - 1));
                                            wrk.RhDat.IsoFix        = (UInt16)Conversion.Val(Strings.Left(RS["ISOFIX"    ].ToString(), Strings.InStr(RS["ISOFIX"    ].ToString(), ":") - 1));
                                            wrk.RhDat.Tether        = (UInt16)Conversion.Val(Strings.Left(RS["ﾃｻﾞｰ"      ].ToString(), Strings.InStr(RS["ﾃｻﾞｰ"      ].ToString(), ":") - 1));
                                            wrk.RhDat.Ottoman       = (UInt16)Conversion.Val(Strings.Left(RS["ｵｯﾄﾏﾝ"     ].ToString(), Strings.InStr(RS["ｵｯﾄﾏﾝ"     ].ToString(), ":") - 1));

                                            if (RS["ｺﾝﾋﾞﾆﾌｯｸ"].ToString() != "") {
                                                wrk.RhDat.ConvHook  = (UInt16)Conversion.Val(Strings.Left(RS["ｺﾝﾋﾞﾆﾌｯｸ" ].ToString(), Strings.InStr(RS["ｺﾝﾋﾞﾆﾌｯｸ" ].ToString(), ":") - 1));
                                                wrk.RhDat.SideTable = (UInt16)Conversion.Val(Strings.Left(RS["ｻｲﾄﾞﾃｰﾌﾞﾙ"].ToString(), Strings.InStr(RS["ｻｲﾄﾞﾃｰﾌﾞﾙ"].ToString(), ":") - 1));
                                            }
                                            else {
                                                wrk.RhDat.ConvHook  = 0;
                                                wrk.RhDat.SideTable = 0;
                                            }

                                            if (RS["ﾛﾎﾞｯﾄ"].ToString() != "") {
                                                wrk.RhDat.Robot     = (UInt16)Conversion.Val(Strings.Left(RS["ﾛﾎﾞｯﾄ"].ToString(), Strings.InStr(RS["ﾛﾎﾞｯﾄ"].ToString(), ":") - 1));
                                            }
                                            else {
                                                wrk.RhDat.Robot     = 0;
                                            }
                                            
                                            //レシピ
                                            rp = (int)Conversion.Val(RS["レシピ№"]);
                                            for (int ii = 1; ii <= 16; ii++)
                                            {
                                                string str = string.Format("ST01{0:00}", ii);
                                                if(Conversion.Val(RS[str]) != 0) {
                                                    wrk.RhDat.SetSt(ii, rowr[ii - 1], (UInt16)(((UInt16)Conversion.Val(RS[str]) << 12) + rp));
                                                    rowr[ii - 1]++;
                                                    break;
                                                }
                                            }

                                            hasdat = true;
                                            break;
                                    }
                                }
                            }

                            // 最終データコピー
                            if (hasdat == true) {
                                // データコーピ
                                dat[ret] = new Mx_sub.VEHICLE_INFO(wrk);

                                // カウンタアップ
                                ret++;
                            }

                        }
                    }
                }
            }
            catch
            {
                ;
            }

            return (ret);
        }
        
        /// <summary>
        /// 車種情報Mのクリア
        /// </summary>
        public static void Vehicle_M_Clear()
        {
            string SQl;
            string conStr = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={Mdb_Module.DataBase1};";

            try
            {
                using (OleDbConnection DB = new OleDbConnection(conStr))
                {
                    DB.Open(); // データベース接続

                    SQl  = "DELETE FROM T_車種情報Ｍ ";

                    using (OleDbCommand cmd = new OleDbCommand(SQl, DB))    // 検索条件をセット
                    {
                        // 実行
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
                ;
            }
        }

        // 車種情報Ｍ新規書込み/更新
        // (作業パターン、データバッファ)
        public static void Vehicle_M_Write(Mx_sub.VEHICLE_INFO dat)
        {
            string SQl = string.Empty;
            string conStr = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={Mdb_Module.DataBase1};";

            try
            {
                using (OleDbConnection DB = new OleDbConnection(conStr))
                {
                    DB.Open(); // データベース接続

                    SQl  = "select * from T_車種情報Ｍ ";
                    SQl += "WHERE Trim(識別コード) = '" + dat.Head.IdenCode + "' ";

                    using (OleDbCommand cmd = new OleDbCommand(SQl, DB))    // 検索条件をセット
                    {
                        using (OleDbDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows != true) {
                                // 新規書込み
                                SQl  = "INSERT INTO T_車種情報Ｍ ";
                                SQl += "(識別コード,治具タイプ,治具切出し順,1st治具ストック段,2nd治具ストック段,向先,1st側ｼｰﾄﾀｲﾌﾟ,2nd側ｼｰﾄﾀｲﾌﾟ) VALUES ";
                                SQl += "(";
                                SQl += "'" + dat.Head.IdenCode + "',";
                                SQl += "'" + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.JigType,  dat.Head.JigType  ) + "',";
                                SQl += "'" + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.JigOrder, dat.Head.JigOrder ) + "',";
                                SQl += "'" + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.JigStock, dat.Head.Jig1st   ) + "',";
                                SQl += "'" + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.JigStock, dat.Head.Jig2nd   ) + "',";
                                SQl += "'" + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Dest,     dat.Head.Dest     ) + "',";
                                SQl += "'" + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.SeatType, dat.LhDat.SeatType) + "',";
                                SQl += "'" + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.SeatType, dat.RhDat.SeatType) + "'";
                                SQl += ")";
                            }
                            else
                            {
                                // 更新
                                SQl  = "UPDATE T_車種情報Ｍ SET ";
                                SQl += "識別コード = '"        + dat.Head.IdenCode + "',";
                                SQl += "治具タイプ = '"        + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.JigType,  dat.Head.JigType  ) + "',";
                                SQl += "治具切出し順 = '"      + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.JigOrder, dat.Head.JigOrder ) + "',";
                                SQl += "1st治具ストック段 = '" + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.JigStock, dat.Head.Jig1st   ) + "',";
                                SQl += "2nd治具ストック段 = '" + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.JigStock, dat.Head.Jig2nd   ) + "',";
                                SQl += "向先 = '"              + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Dest,     dat.Head.Dest     ) + "',";
                                SQl += "1st側ｼｰﾄﾀｲﾌﾟ = '"      + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.SeatType, dat.LhDat.SeatType) + "',";
                                SQl += "2nd側ｼｰﾄﾀｲﾌﾟ = '"      + Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.SeatType, dat.RhDat.SeatType) + "' ";
                                SQl += "WHERE 識別コード = '"  + dat.Head.IdenCode + "';";
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
            catch
            {
                ;
            }
        }

        // ファイル
        public static void SetDatabasePath(string path)
        {
            try
            {
                Mdb_Module.DataBase3        = Path.GetDirectoryName(path);

                Mdb_Module.DataBase1        = path;
                Mdb_Module.Select_DataBase  = path;
                // バクアップファイル
                string c1                   = path.Substring(0, path.IndexOf("."));
                Mdb_Module.DataBase2        = c1 + "Bakup.mdb";
            }
            catch
            {

            }
        }
//<<=====================================================================================
    }
}
