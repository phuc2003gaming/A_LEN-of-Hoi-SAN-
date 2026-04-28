using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using C1.Win.C1FlexGrid;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.IO;

namespace X61GX42H1ST
{
    public static class Danpre_Xls_sub
    {
        public static dynamic excelApp;
        public static dynamic wbDenmast;
        public static dynamic wbDenprt;
        public static Worksheet Shtcontinent;

        // マスターリスト
        public static string[,] m_Master;

        /// <summary>
        /// 選択項目ﾜｰｸｼｰﾄ参照設定
        /// </summary>
        public static bool Xls_MF_Set1() {
            try
            {
                // エクセルアプリケーション起動
                excelApp = Activator.CreateInstance(Type.GetTypeFromProgID("Excel.Application"));
                excelApp.Visible = false;
                excelApp.DisplayAlerts = false;      // アラート（例: 上書き保存の確認ダイアログ）を無効化 

                // ブックへの参照を取得します
                if (File.Exists(Program.Get_ShtDenmast_Path()) == true) {
                    wbDenmast = excelApp.Workbooks.Open(Program.Get_ShtDenmast_Path());
                    // ｼｰﾄの参照設定
                    Shtcontinent = wbDenmast.Sheets["Sheet1"];

                    return true;
                }
                else
                {
                    // エラーウィンドウを表示する
                    MessageBox.Show("「項目マスター」ファイルが存在しません。" + Path.GetDirectoryName(Program.Get_ShtDenmast_Path()) +
                                    "フォルダにコピーして貼り付けてください。", Properties.Resources.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                }
            }
            catch
            {
                ;
            }

            return false;
        }

        /// <summary>
        /// 選択項目ﾜｰｸｼｰﾄ参照設定
        /// </summary>
        public static bool Xls_MF_Set2() {
            try
            {
                // エクセルアプリケーション起動
                excelApp = Activator.CreateInstance(Type.GetTypeFromProgID("Excel.Application"));
                excelApp.Visible = false;
                excelApp.DisplayAlerts = false;      // アラート（例: 上書き保存の確認ダイアログ）を無効化 

                // ブックへの参照を取得します
                if (File.Exists(Program.Get_ShtDenprt_Path()) == true)
                {
                    wbDenprt = excelApp.Workbooks.Open(Program.Get_ShtDenprt_Path());

                    // ｼｰﾄの参照設定
                    Shtcontinent = wbDenprt.Sheets["Sheet1"];

                    return true;
                }
                else
                {
                    // エラーウィンドウを表示する
                    MessageBox.Show("「車種登録情報フォーム」ファイルが存在しません。" + Path.GetDirectoryName(Program.Get_ShtDenmast_Path()) +
                                    "フォルダにコピーして貼り付けてください。", Properties.Resources.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                }
            }
            catch
            {
                ;
            }

            return false;
        }

        /// <summary>
        /// オブジェクト変数に Nothing を設定します
        /// </summary>
        public static void Xls_MF_Rset1() {
            // これにより、他のｱﾌﾟﾘｹｰｼｮﾝ又はﾕｻﾞｰによってﾛｰﾄﾞされてない限り EXCEL が終了します
            wbDenmast.Close(false);

            excelApp.Quit();
            Marshal.ReleaseComObject(excelApp);

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        // オブジェクト変数に Nothing を設定します
        public static void Xls_MF_Rset2() {
            // これにより、他のｱﾌﾟﾘｹｰｼｮﾝ又はﾕｻﾞｰによってﾛｰﾄﾞされてない限り EXCEL が終了します
            wbDenprt.Close(false);

            excelApp.Quit();
            Marshal.ReleaseComObject(excelApp);

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        /// <summary>
        /// マスターアイテムの初期化
        /// </summary>
        public static bool MasterInit()
        {
            // マスターアイテム取込み
            Range startCell;
            Range endCell;
            Range writeRange;
            int cols = 0;
            int rows = 0;

            // 選択項目ﾜｰｸｼｰﾄ参照設定
            if (Xls_MF_Set1() == true)
            {
                // バッファ初期化
                cols = Shtcontinent.Rows[1].Find(null).Column;
                for (int col = 0; col < cols; col++) {
                    if (Shtcontinent.Columns[col + 1].Find("").Row > rows) {
                        rows = Shtcontinent.Columns[col + 1].Find(null).Row;
                    }
                }

                m_Master = new string[(cols - 1), (rows - 2)];
                startCell = Shtcontinent.Cells[2, 1];
                endCell = Shtcontinent.Cells[(rows - 1), (cols - 1)];
                writeRange = Shtcontinent.Range[startCell, endCell];

                // マスターアイテム読み込み
                var dat = writeRange.Value2;

                for (int col = 0; col < m_Master.GetLength(0); col++)
                {
                    for (int row = 0; row < m_Master.GetLength(1); row++)
                    {
                        if (dat[(row + 1), (col + 1)] != null)
                        {
                            m_Master[col, row] = dat[(row + 1), (col + 1)];
                        }
                        else break;
                    }
                }

                // オブジェクト変数に Nothing を設定します
                Xls_MF_Rset1();

                return true;
            }

            return false;
        }

        // 
        // 項目マスターシート１より選択項目値をコンボックスへセットします
        //
        public static void Xls_MF_List_Read3(ComboBox[] combo) {
            int Loop1;

            for (int col = 0; col < m_Master.GetLength(0); col++)
            {
                // 所定のｺﾝﾎﾞｯｸｽにﾃﾞｰﾀセット
                switch (col + 1) {
                    case 1:  // 治具切出し順
                        combo[1].Items.Clear();
                        for (Loop1 = 0; Loop1 < m_Master.GetLength(1); Loop1++) {
                            if (m_Master[col, Loop1] != null)
                            {
                                combo[1].Items.Add(m_Master[col, Loop1]);
                            }
                            else break;
                        }
                        combo[1].SelectedItem = combo[1].Items[0];
                        break;

                    case 3:  // 治具切出し順
                        combo[2].Items.Clear();
                        for (Loop1 = 0; Loop1 < m_Master.GetLength(1); Loop1++) {
                            if (m_Master[col, Loop1] != null)
                            {
                                combo[2].Items.Add(m_Master[col, Loop1]);
                            }
                            else break;
                        }
                        combo[2].SelectedItem = combo[2].Items[0];
                        break;

                    case 4:  // 治具ｽﾄｯｸ段
                        combo[3].Items.Clear();
                        combo[4].Items.Clear();
                        for (Loop1 = 0; Loop1 < m_Master.GetLength(1); Loop1++) {
                            if (m_Master[col, Loop1] != null)
                            {
                                combo[3].Items.Add(m_Master[col, Loop1]);
                                combo[4].Items.Add(m_Master[col, Loop1]);
                            }
                            else break;
                        }
                        combo[3].SelectedItem = combo[3].Items[0];
                        combo[4].SelectedItem = combo[4].Items[0];
                        break;

                    case 5:  // 向け先
                        combo[5].Items.Clear();
                        for (Loop1 = 0; Loop1 < m_Master.GetLength(1); Loop1++) {
                            if (m_Master[col, Loop1] != null)
                            {
                                combo[5].Items.Add(m_Master[col, Loop1]);
                            }
                            else break;
                        }
                        combo[5].SelectedItem = combo[5].Items[0];
                        break;

                    case 6:  // ｼｰﾄﾀｲﾌﾟ
                        combo[6].Items.Clear();
                        combo[23].Items.Clear();
                        for (Loop1 = 0; Loop1 < m_Master.GetLength(1); Loop1++) {
                            if (m_Master[col, Loop1] != null)
                            {
                                combo[6].Items.Add(m_Master[col, Loop1]);
                                combo[23].Items.Add(m_Master[col, Loop1]);
                            }
                            else break;
                        }
                        combo[6].SelectedItem = combo[6].Items[0];
                        combo[23].SelectedItem = combo[23].Items[0];
                        break;

                    case 7:  // AGﾀｲﾌﾟ
                        combo[7].Items.Clear();
                        combo[24].Items.Clear();
                        for (Loop1 = 0; Loop1 < m_Master.GetLength(1); Loop1++) {
                            if (m_Master[col, Loop1] != null)
                            {
                                combo[7].Items.Add(m_Master[col, Loop1]);
                                combo[24].Items.Add(m_Master[col, Loop1]);
                            }
                            else break;
                        }
                        combo[7].SelectedItem = combo[7].Items[0];
                        combo[24].SelectedItem = combo[24].Items[0];
                        break;

                    case 8:  // ヒーター
                        combo[8].Items.Clear();
                        combo[25].Items.Clear();
                        for (Loop1 = 0; Loop1 < m_Master.GetLength(1); Loop1++) {
                            if (m_Master[col, Loop1] != null)
                            {
                                combo[8].Items.Add(m_Master[col, Loop1]);
                                combo[25].Items.Add(m_Master[col, Loop1]);
                            }
                            else break;
                        }
                        combo[8].SelectedItem = combo[8].Items[0];
                        combo[25].SelectedItem = combo[25].Items[0];
                        break;

                    case 9:  // ﾊﾞｯｸﾙ
                        combo[9].Items.Clear();
                        combo[26].Items.Clear();
                        for (Loop1 = 0; Loop1 < m_Master.GetLength(1); Loop1++) {
                            if (m_Master[col, Loop1] != null)
                            {
                                combo[9].Items.Add(m_Master[col, Loop1]);
                                combo[26].Items.Add(m_Master[col, Loop1]);
                            }
                            else break;
                        }
                        combo[9].SelectedItem = combo[9].Items[0];
                        combo[26].SelectedItem = combo[26].Items[0];
                        break;

                    case 10:  // ﾍｯﾄﾞﾚｽﾄ
                        combo[10].Items.Clear();
                        combo[27].Items.Clear();
                        for (Loop1 = 0; Loop1 < m_Master.GetLength(1); Loop1++) {
                            if (m_Master[col, Loop1] != null)
                            {
                                combo[10].Items.Add(m_Master[col, Loop1]);
                                combo[27].Items.Add(m_Master[col, Loop1]);
                            }
                            else break;
                        }
                        combo[10].SelectedItem = combo[10].Items[0];
                        combo[27].SelectedItem = combo[27].Items[0];
                        break;

                    case 11:  // 着座ｾﾝｻｰ
                        combo[11].Items.Clear();
                        combo[28].Items.Clear();
                        for (Loop1 = 0; Loop1 < m_Master.GetLength(1); Loop1++) {
                            if (m_Master[col, Loop1] != null)
                            {
                                combo[11].Items.Add(m_Master[col, Loop1]);
                                combo[28].Items.Add(m_Master[col, Loop1]);
                            }
                            else break;
                        }
                        combo[11].SelectedItem = combo[11].Items[0];
                        combo[28].SelectedItem = combo[28].Items[0];
                        break;

                    case 12:  // 空調
                        combo[12].Items.Clear();
                        combo[29].Items.Clear();
                        for (Loop1 = 0; Loop1 < m_Master.GetLength(1); Loop1++) {
                            if (m_Master[col, Loop1] != null)
                            {
                                combo[12].Items.Add(m_Master[col, Loop1]);
                                combo[29].Items.Add(m_Master[col, Loop1]);
                            }
                            else break;
                        }
                        combo[12].SelectedItem = combo[12].Items[0];
                        combo[29].SelectedItem = combo[29].Items[0];
                        break;

                    case 13:  // 表皮材
                        combo[13].Items.Clear();
                        combo[30].Items.Clear();
                        for (Loop1 = 0; Loop1 < m_Master.GetLength(1); Loop1++) {
                            if (m_Master[col, Loop1] != null)
                            {
                                combo[13].Items.Add(m_Master[col, Loop1]);
                                combo[30].Items.Add(m_Master[col, Loop1]);
                            }
                            else break;
                        }
                        combo[13].SelectedItem = combo[13].Items[0];
                        combo[30].SelectedItem = combo[30].Items[0];
                        break;

                    case 14:  // 色
                        combo[14].Items.Clear();
                        combo[31].Items.Clear();
                        for (Loop1 = 0; Loop1 < m_Master.GetLength(1); Loop1++) {
                            if (m_Master[col, Loop1] != null)
                            {
                                combo[14].Items.Add(m_Master[col, Loop1]);
                                combo[31].Items.Add(m_Master[col, Loop1]);
                            }
                            else break;
                        }
                        combo[14].SelectedItem = combo[14].Items[0];
                        combo[31].SelectedItem = combo[31].Items[0];
                        break;

                    case 15:  // ﾗﾝﾊﾞｰ
                        combo[15].Items.Clear();
                        combo[32].Items.Clear();
                        for (Loop1 = 0; Loop1 < m_Master.GetLength(1); Loop1++) {
                            if (m_Master[col, Loop1] != null)
                            {
                                combo[15].Items.Add(m_Master[col, Loop1]);
                                combo[32].Items.Add(m_Master[col, Loop1]);
                            }
                            else break;
                        }
                        combo[15].SelectedItem = combo[15].Items[0];
                        combo[32].SelectedItem = combo[32].Items[0];
                        break;

                    case 16:  // 背面ﾎﾟｹｯﾄ
                        combo[16].Items.Clear();
                        combo[33].Items.Clear();
                        for (Loop1 = 0; Loop1 < m_Master.GetLength(1); Loop1++)
                        {
                            if (m_Master[col, Loop1] != null)
                            {
                                combo[16].Items.Add(m_Master[col, Loop1]);
                                combo[33].Items.Add(m_Master[col, Loop1]);
                            }
                            else break;
                        }
                        combo[16].SelectedItem = combo[16].Items[0];
                        combo[33].SelectedItem = combo[33].Items[0];
                        break;

                    case 17:  // ﾌｯﾄｳｴﾙﾗﾝﾌﾟ
                        combo[17].Items.Clear();
                        combo[34].Items.Clear();
                        for (Loop1 = 0; Loop1 < m_Master.GetLength(1); Loop1++)
                        {
                            if (m_Master[col, Loop1] != null)
                            {
                                combo[17].Items.Add(m_Master[col, Loop1]);
                                combo[34].Items.Add(m_Master[col, Loop1]);
                            }
                            else break;
                        }
                        combo[17].SelectedItem = combo[17].Items[0];
                        combo[34].SelectedItem = combo[34].Items[0];
                        break;

                    case 18:  // ｱｰﾑﾚｽﾄ
                        combo[18].Items.Clear();
                        combo[35].Items.Clear();
                        for (Loop1 = 0; Loop1 < m_Master.GetLength(1); Loop1++)
                        {
                            if (m_Master[col, Loop1] != null)
                            {
                                combo[18].Items.Add(m_Master[col, Loop1]);
                                combo[35].Items.Add(m_Master[col, Loop1]);
                            }
                            else break;
                        }
                        combo[18].SelectedItem = combo[18].Items[0];
                        combo[35].SelectedItem = combo[35].Items[0];
                        break;

                    case 19:  // ﾀﾝﾌﾞﾙ
                        combo[19].Items.Clear();
                        combo[36].Items.Clear();
                        for (Loop1 = 0; Loop1 < m_Master.GetLength(1); Loop1++)
                        {
                            if (m_Master[col, Loop1] != null)
                            {
                                combo[19].Items.Add(m_Master[col, Loop1]);
                                combo[36].Items.Add(m_Master[col, Loop1]);
                            }
                            else break;
                        }
                        combo[19].SelectedItem = combo[19].Items[0];
                        combo[36].SelectedItem = combo[36].Items[0];
                        break;

                    case 20:  // ISO-FIX
                        combo[20].Items.Clear();
                        combo[37].Items.Clear();
                        for (Loop1 = 0; Loop1 < m_Master.GetLength(1); Loop1++)
                        {
                            if (m_Master[col, Loop1] != null)
                            {
                                combo[20].Items.Add(m_Master[col, Loop1]);
                                combo[37].Items.Add(m_Master[col, Loop1]);
                            }
                            else break;
                        }
                        combo[20].SelectedItem = combo[20].Items[0];
                        combo[37].SelectedItem = combo[37].Items[0];
                        break;

                    case 21:  // ﾃｻﾞｰ
                        combo[21].Items.Clear();
                        combo[38].Items.Clear();
                        for (Loop1 = 0; Loop1 < m_Master.GetLength(1); Loop1++)
                        {
                            if (m_Master[col, Loop1] != null)
                            {
                                combo[21].Items.Add(m_Master[col, Loop1]);
                                combo[38].Items.Add(m_Master[col, Loop1]);
                            }
                            else break;
                        }
                        combo[21].SelectedItem = combo[21].Items[0];
                        combo[38].SelectedItem = combo[38].Items[0];
                        break;

                    case 22:  // ｵｯﾄﾏﾝ
                        combo[22].Items.Clear();
                        combo[39].Items.Clear();
                        for (Loop1 = 0; Loop1 < m_Master.GetLength(1); Loop1++)
                        {
                            if (m_Master[col, Loop1] != null)
                            {
                                combo[22].Items.Add(m_Master[col, Loop1]);
                                combo[39].Items.Add(m_Master[col, Loop1]);
                            }
                            else break;
                        }
                        combo[22].SelectedItem = combo[22].Items[0];
                        combo[39].SelectedItem = combo[39].Items[0];
                        break;

                    case 23:  // ｺﾝﾋﾞﾆﾌｯｸ
                        combo[40].Items.Clear();
                        combo[42].Items.Clear();
                        for (Loop1 = 0; Loop1 < m_Master.GetLength(1); Loop1++)
                        {
                            if (m_Master[col, Loop1] != null)
                            {
                                combo[40].Items.Add(m_Master[col, Loop1]);
                                combo[42].Items.Add(m_Master[col, Loop1]);
                            }
                            else break;
                        }
                        combo[40].SelectedItem = combo[40].Items[0];
                        combo[42].SelectedItem = combo[42].Items[0];
                        break;

                    case 24:  // ｻｲﾄﾞﾃｰﾌﾞﾙ
                        combo[41].Items.Clear();
                        combo[43].Items.Clear();
                        for (Loop1 = 0; Loop1 < m_Master.GetLength(1); Loop1++)
                        {
                            if (m_Master[col, Loop1] != null)
                            {
                                combo[41].Items.Add(m_Master[col, Loop1]);
                                combo[43].Items.Add(m_Master[col, Loop1]);
                            }
                            else break;
                        }
                        combo[41].SelectedItem = combo[41].Items[0];
                        combo[43].SelectedItem = combo[43].Items[0];
                        break;

                    case 25:  // ﾛﾎﾞｯﾄ№
                        combo[44].Items.Clear();
                        combo[45].Items.Clear();
                        for (Loop1 = 0; Loop1 < m_Master.GetLength(1); Loop1++)
                        {
                            if (m_Master[col, Loop1] != null)
                            {
                                combo[44].Items.Add(m_Master[col, Loop1]);
                                combo[45].Items.Add(m_Master[col, Loop1]);
                            }
                            else break;
                        }
                        combo[44].SelectedItem = combo[44].Items[0];
                        combo[45].SelectedItem = combo[45].Items[0];
                        break;
                }
            }
        }


        // 
        // 試験規定ＭＦより規定値データを読み込みグリッドへセットします
        // 
        public static void Xls_MF_List_Read4(C1FlexGrid flexgrid) {
            int Loop1;

            for (int col = 0; col <= 4; col++) {

                // 所定のｺﾝﾎﾞｯｸｽにﾃﾞｰﾀセット
                switch (col + 1) {
                    case 1:  // 治具タイプ
                        flexgrid.Cols[1].ComboList = string.Empty;
                        for (Loop1 = 0; Loop1 < m_Master.GetLength(1); Loop1++) {
                            if (m_Master[col, Loop1] != null)
                            {
                                flexgrid.Cols[1].ComboList += $"|{m_Master[col, Loop1]}";
                            }
                            else break;
                        }
                        flexgrid.Cols[1].ComboList += $"| ";
                        break;

                    case 2:  // 治具1st/2nd
                        flexgrid.Cols[2].ComboList = string.Empty;
                        for (Loop1 = 0; Loop1 < m_Master.GetLength(1); Loop1++) {
                            if (m_Master[col, Loop1] != null)
                            {
                                flexgrid.Cols[2].ComboList += $"|{m_Master[col, Loop1]}";
                            }
                            else break;
                        }
                        flexgrid.Cols[2].ComboList += $"| ";
                        break;

                    case 4:  // ｽﾄｯｸ段
                        flexgrid.Cols[3].ComboList = string.Empty;
                        for (Loop1 = 0; Loop1 < m_Master.GetLength(1); Loop1++) {
                            if (m_Master[col, Loop1] != null)
                            {
                                flexgrid.Cols[3].ComboList += $"|{m_Master[col, Loop1]}";
                            }
                            else break;
                        }
                        flexgrid.Cols[3].ComboList += $"| ";
                        break;
                }
            }
        }

        /// <summary>
        /// 印刷
        /// </summary>
        public static void Xls_Data_Prt_A(System.ComponentModel.BackgroundWorker ctl, C1FlexGrid flexGrid) {
            Range startCell;
            Range endCell;
            Range writeRange;
            object[,] writeData;
            int i;
            int j;
            int rcnt;
            int pcnt;
            int cnt;

            // グリッド初期設定
            j = 2; // セルの先頭デフォルト
            rcnt = 0;
            pcnt = 0;
            writeData = new object[64, 9];                       // 行3～66、列1～9
            startCell = Shtcontinent.Cells[3, 1];                // A3
            endCell = Shtcontinent.Cells[66, 9];               // H66
            writeRange = Shtcontinent.Range[startCell, endCell];

            // 印刷範囲
            for (int row = flexGrid.Rows.Fixed; row < flexGrid.Rows.Count; row++)
            {
                if (flexGrid.GetData(row, 1) == null)
                {
                    rcnt = row - 1;                                     // データ数
                    pcnt = (rcnt / 66) + ((rcnt % 66) != 0 ? 1 : 0);    // ページ数
                    break;
                }
            }

            if (rcnt > 0)
            {
                // データクリア
                for (int r = 0; r < writeData.GetLength(0); r++) {
                    for (int c = 0; c < writeData.GetLength(1); c++) {
                        writeData[r, c] = "";
                    }
                }

                // バックグラウンドの進捗処理更新
                cnt = 0;
                ctl.ReportProgress(100, new A_MEIN.CReadState(cnt, pcnt, "車種情報の印刷中"));

                for (i = 1; i <= rcnt; i++) {   // 行№を設定
                    j = j + 1;

                    for (int c = 1; c <= 9; c++)
                    {
                        writeData[j - 3, c - 1] = flexGrid[i, (c - 1)].ToString();
                    }

                    if (j == 66)
                    {
                        //　データ書込み
                        writeRange.Value2 = writeData;

                        Shtcontinent.PrintOut(From: 1, To: 1, Copies: 1, Preview: false);

                        j = 2; // セルの先頭デフォルト

                        // データクリア
                        for (int r = 0; r < writeData.GetLength(0); r++) {
                            for (int c = 0; c < writeData.GetLength(1); c++) {
                                writeData[r, c] = "";
                            }
                        }

                        // バックグラウンドの進捗処理更新
                        cnt++;
                        ctl.ReportProgress(100, new A_MEIN.CReadState(cnt, pcnt, "車種情報の印刷中"));
                    }
                }

                if (j != 2)
                {
                    //　データ書込み
                    writeRange.Value2 = writeData;

                    Shtcontinent.PrintOut(From: 1, To: 1, Copies: 1, Preview: false);

                    // バックグラウンドの進捗処理更新
                    cnt++;
                    ctl.ReportProgress(100, new A_MEIN.CReadState(cnt, pcnt, "車種情報の印刷中"));
                }
            }
        }
        
//[2026/04/06][p.hoi][Add]=============================================================>>
//  PLCデータ読込、照合追加
//---------------------------------------------------------------------------------------
        // マスター項目読込み
        public static string GetMasterStr(M_ID id, int no)
        {
            int    idx;
            string ret = string.Empty;

            switch (id)
            {
                case M_ID.JigLR:
                case M_ID.JigOrder:
                case M_ID.JigStock:
                case M_ID.Dest:
                case M_ID.SeatType:
                case M_ID.TorqueWrench:
                    if (no >= 1) { idx = no - 1; }
                    else         { idx = no; }
                    break;

                default: 
                    idx = no;
                    break;
            }

            // マスター項目以外
            ret = string.Format("{0}: ", no);
            if (idx < m_Master.GetLength(1))
            {
                if (m_Master[(int)id, idx] != null) { ret = m_Master[(int)id, idx]; }
            }

            return (ret);
        }

        // マスター項目確認
        public static bool IsMasterItem(M_ID id, int no)
        {
            int idx = -1;
            switch (id)
            {
                case M_ID.JigLR:
                case M_ID.JigOrder:
                case M_ID.JigStock:
                case M_ID.Dest:
                case M_ID.SeatType:
                case M_ID.TorqueWrench:
                    if (no >= 1) { idx = no - 1; }
                    else         { idx = no; }
                    break;

                default: 
                    idx = no;
                    break;
            }

            if (idx < m_Master.GetLength(1))
            {
                if (m_Master[(int)id, idx] != null) { return true; }
            }

            return false;
        }

        // マスター項目ＩＤ
        public enum M_ID{
            JigType = 0,    // 治具ﾀｲﾌﾟ
            JigLR,          // 治具RH/LH
            JigOrder,       // 治具切出し順
            JigStock,       // 治具ｽﾄｯｸ段
            Dest,           // 向け先
            SeatType,       // ｼｰﾄﾀｲﾌﾟ
            AgType,         // AGﾀｲﾌﾟ
            Heater,         // ﾋｰﾀｰ
            Buckle,         // ﾊﾞｯｸﾙ
            Headrest,       // ﾍｯﾄﾞﾚｽﾄ
            SeatSen,        // 着座ｾﾝｻｰ
            TorqueWrench,   // ﾄﾙｸﾚﾝﾁ (thiếu)
            AirCond,        // 空調
            Upholstery,     // 表皮材
            Lumbar,         // ﾗﾝﾊﾞｰ
            BackPock,       // 背面ﾎﾟｹｯﾄ
            FootwellLamp,   // ﾌｯﾄｳｴﾙﾗﾝﾌﾟ
            ArmRest,        // ｱｰﾑﾚｽﾄ   (thiếu)
            QRG,            // QRG--new
            ISOFIX,         // ISOFIX--new
            Backboard,      // ﾊﾞｯｸﾎﾞｰﾄﾞ--new
            Ottoman,        // ｵｯﾄﾏﾝ-- new
            ConvHook,       // コンビニフック--new
            Site_Table,     // サイトデーブル--new
            Robot,          // ロボット--new
           
            
            count           // 数
        }
//<<=====================================================================================
    }
}
