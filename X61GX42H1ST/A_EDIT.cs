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
using System.Data.OleDb;        // データベース
using C1.Win.C1FlexGrid;

namespace X61GX42H1ST
{
    public partial class A_EDIT : Form
    {
        private          string          Key_code1;
        private          bool            m_init;

        // プライベート変数
        private static   A_EDIT          m_Dlg = null;
        private readonly ComboBox[]      m_Combo;
        private readonly C1FlexGrid[]    m_c1FlexGrid1;

        private static   Row             m_row;

        /// <summary>
        /// クラスのコンストラクタ
        /// </summary>
        public A_EDIT()
        {
            InitializeComponent();

            m_Combo = new ComboBox[]
            {
                comboBox1_0 , comboBox1_1 , comboBox1_2 , comboBox1_3 , comboBox1_4, comboBox1_5 , comboBox1_6 , comboBox1_7 , comboBox1_8,
                comboBox1_9 , comboBox1_10, comboBox1_11, comboBox1_12, comboBox1_13, comboBox1_14, comboBox1_15, comboBox1_16,
                comboBox1_17, comboBox1_18 , comboBox1_19 , comboBox1_20, comboBox1_21 , comboBox1_22, comboBox1_23, comboBox1_24,
                comboBox1_25, comboBox1_26, comboBox1_27, comboBox1_28, comboBox1_29, comboBox1_30, comboBox1_31, comboBox1_32,
                comboBox1_33, comboBox1_34, comboBox1_35, comboBox1_36, comboBox1_37, comboBox1_38, comboBox1_39, comboBox1_40,
                comboBox1_41, comboBox1_42, comboBox1_43, comboBox1_44, comboBox1_45,
            };
// [2025/06/24] p.hoi==================================================================>>
            /*m_c1FlexGrid1 = new C1FlexGrid[] {
                c1FlexGrid1_0, c1FlexGrid1_1, c1FlexGrid1_2, c1FlexGrid1_3, c1FlexGrid1_4, c1FlexGrid1_5
            };*/
// MODIFY
            m_c1FlexGrid1 = new C1FlexGrid[] {
                c1FlexGrid1_0, c1FlexGrid1_2, c1FlexGrid1_4, c1FlexGrid1_1, c1FlexGrid1_3, c1FlexGrid1_5
            };
//<<=====================================================================================

            m_init = false;
            label45.Visible = false;
            label46.Visible = false;
            label47.Visible = false;
            label48.Visible = false;
            comboBox1_18.Visible = false;
            comboBox1_20.Visible = false;
            comboBox1_35.Visible = false;
            comboBox1_37.Visible = false;
        }

        /// <summary>
        /// フォームの初期化
        /// </summary>
        private void A_EDIT_Load(object sender, EventArgs e)
        {

            int i; // カウンター変数
            int k; // ウンター変数

            for (k = 0; k < m_c1FlexGrid1.Count(); k++){ 
                // グリッド初期設定
                m_c1FlexGrid1[k].FocusRect               = FocusRectEnum.Heavy;  // セルを強調表示
                m_c1FlexGrid1[k].Rows.Count              = 100;                  // 行の総数
                m_c1FlexGrid1[k].Cols.Count              = 18;                   // 列の総数
                m_c1FlexGrid1[k].Rows.Fixed              = 1;                    // 固定行の総数
                m_c1FlexGrid1[k].Cols.Fixed              = 2;                    // 固定列の総数


                //m_c1FlexGrid1[k].Row = 0;        //行の指定
                m_c1FlexGrid1[k].Cols[0].Width           = Pixel(12 * 60);       // 列幅の設定
                m_c1FlexGrid1[k].Cols[1].Width           = Pixel(12 * 350);


                for (i = 2; i < m_c1FlexGrid1[k].Cols.Count; i++){ 
                    if(i <= 17) m_c1FlexGrid1[k].Cols[i].Width = Pixel(12 * 50);
                    else        m_c1FlexGrid1[k].Cols[i].Width = 0;
                }

                // 全行幅の設定
                m_c1FlexGrid1[k].Rows.DefaultSize = Pixel(350);

                // セル内のテキスト表示位置
                //m_c1FlexGrid1[k].Cols[0].TextAlignFixed  = TextAlignEnum.CenterCenter;
                for (i = 0; i < m_c1FlexGrid1[k].Cols.Count; i++){
                    if(i == 1) m_c1FlexGrid1[k].Cols[i].TextAlign = TextAlignEnum.LeftCenter;
                    else       m_c1FlexGrid1[k].Cols[i].TextAlign = TextAlignEnum.CenterCenter;
                }
                //c1FlexGrid1.Col = 0;        //列の指定

                // ヘッダ部のセル内のテキスト表示位置
                m_c1FlexGrid1[k].Rows[0].TextAlignFixed = TextAlignEnum.CenterCenter;
                m_c1FlexGrid1[k].Rows[0].TextAlign      = TextAlignEnum.CenterCenter;

                //列のタイトル設定
                m_c1FlexGrid1[k][0,  0] = "ﾚｼﾋﾟ№";
                m_c1FlexGrid1[k][0,  1] = "レシピ名";
        //             .Row = 0: .Col = 2: .Text = "仕　様"
        //             .CellAlignment = flexAlignCenterCenter
                m_c1FlexGrid1[k][0,  2] = "ST1";
                m_c1FlexGrid1[k][0,  3] = "ST2";
                m_c1FlexGrid1[k][0,  4] = "ST3";
                m_c1FlexGrid1[k][0,  5] = "ST4";
                m_c1FlexGrid1[k][0,  6] = "ST5";
                m_c1FlexGrid1[k][0,  7] = "ST6";
                m_c1FlexGrid1[k][0,  8] = "ST7";
                m_c1FlexGrid1[k][0,  9] = "ST8";
                m_c1FlexGrid1[k][0, 10] = "ST9";
                m_c1FlexGrid1[k][0, 11] = "ST10";
                m_c1FlexGrid1[k][0, 12] = "ST11";
                m_c1FlexGrid1[k][0, 13] = "ST12";
                m_c1FlexGrid1[k][0, 14] = "ST13";
                m_c1FlexGrid1[k][0, 15] = "ST14";
                m_c1FlexGrid1[k][0, 16] = "ST15";
                m_c1FlexGrid1[k][0, 17] = "ST16";
            }


            MDB_READ1();                                // 識別コードマスタの読込み
            Danpre_Xls_sub.Xls_MF_List_Read3(m_Combo);  // 選択項目にデータをセット

            for (i = 1; i < m_Combo.Count(); i++)
            {
                if(m_Combo[i] != null)
                {
                    m_Combo[i].Items.Add("");
                }
            }

            Key_code1               = Convert.ToString(m_row[1]);
            m_Combo[0].Text = Key_code1;        // 識別コード
            if (Key_code1 != "")
            {
                m_Combo[1].Text = Convert.ToString(m_row[2]);  // 治具タイプ
                m_Combo[2].Text = Convert.ToString(m_row[3]);  // 治具ＬＲ
                m_Combo[3].Text = Convert.ToString(m_row[4]);  // 1st治具ストック段
                m_Combo[4].Text = Convert.ToString(m_row[5]);  // 2nd治具ストック段
                m_Combo[5].Text = Convert.ToString(m_row[6]);  // 向け先
            }

            MDB_READ3();    // 車種情報マスタの読込み
            Application.DoEvents();
            MDB_READ4();    // レシピマスタの読込み

            for (i = 0; i <= 5; i++) {  // セル位置を先頭に移動
                m_c1FlexGrid1[i].Row = 1;
                m_c1FlexGrid1[i].Col = 1;
            }

            Application.DoEvents();
        }


        /// <summary>
        /// 「識別コード」選択処理
        /// </summary>
        private void comboBox1_0_SelectedValueChanged(object sender, EventArgs e)
        {
            if (m_init == true)
            {
                int Check_Flg = MDB_CHECK1(m_Combo[0].Text.ToString());
                if (Check_Flg == 1)
                {
                    Program.MessageBox("すでに登録されています、確認してください。");
                    m_Combo[0].Text = "";
                }
            }
            else m_init = true;
        }

        /// <summary>
        /// 「ファイル」メニューをクリック
        /// </summary>
        private void File_Command_Click(object sender, EventArgs e)
        {
            var          clkdItem = sender as ToolStripMenuItem;
            int          In_flg;
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
                            In_flg = Input_Check(); // 入力データをチェック
                            
// [2026/04/01][p.hoi][MODIFY、ADD]====================================================>>
//  コンボボックス値のチェックを追加
//---------------------------------------------------------------------------------------
                            /*if(In_flg == 1)
                            {
                                Program.MessageBox("入力データに誤りっがあります　確認してください。");
                            }*/
//---------------------------------------------------------------------------------------
                            if(In_flg != -1)
                            {
                                Program.MessageBox(m_Combo[In_flg].Tag +
                                                   "の選択に誤りがあります。確認してください。", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
//<<=====================================================================================
                            else
                            {
                                MDB_WRITE1();     // 車種情報Ｔの新規書込み/更新
                                MDB_WRITE2();     // 車種情報Ｍの新規書込み/更新

                                MessageBox.Show("書込み完了", Title);

                                // 再描画
                                Key_code1 = m_Combo[0].Text.ToString();
                                MDB_READ3();    // 車種情報マスタの読込み
                                MDB_READ4();    // レシピマスタの読込み

                                for (int i = 0; i <= 5; i++) {  // セル位置を先頭に移動
                                    m_c1FlexGrid1[i].Row = 1;
                                    m_c1FlexGrid1[i].Col = 1;
                                }

                            }
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
                                In_flg = Input_Check(); // 入力データをチェック
                                
// [2026/04/01][p.hoi][MODIFY、ADD]====================================================>>
//  コンボボックス値のチェックを追加
//---------------------------------------------------------------------------------------
                                /*if(In_flg == 1)
                                {
                                    Program.MessageBox("入力データに誤りっがあります　確認してください。");
                                }*/
//---------------------------------------------------------------------------------------
                                if(In_flg != -1)
                                {
                                    Program.MessageBox(m_Combo[In_flg].Tag +
                                                       "の選択に誤りがあります。確認してください。", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
//<<=====================================================================================
                                else
                                {
                                    MDB_WRITE1();     // 車種情報Ｔの新規書込み/更新
                                    MDB_WRITE2();     // 車種情報Ｍの新規書込み/更新
                                    // ウィンドウを非表示する
                                    VisibleWindow(false);
                                }
                                break;

                            case DialogResult.No:
                                // ウィンドウを非表示する
                                VisibleWindow(false);
                                break;
                        }
                        break;

                    default:
                        break;
                }
            }
        }


        /// <summary>
        /// 「編集」メニューをクリック
        /// </summary>
        private void Edit_Command_Click(object sender, EventArgs e)
        {
            var          clkdItem = sender as ToolStripMenuItem;
            int          i;
            int          j;
            int          k;
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
                    case 0: // 削除
                        Msg   = "選択行を削除しますか?";      // メッセージを定義します。
                        Title = "削除";                       // タイトルを定義します。

                        // 確認ウィンドウを表示する
                        dr = MessageBox.Show(Msg, Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        if (dr == DialogResult.Yes)
                        {
// [2025/06/24] p.hoi==================================================================>>
                            //int idx = tabControl1.SelectedIndex;
// MODIFY
                            int idx = SelectedTabIndex;
//<<=====================================================================================

                            Mdb_Del1(m_Combo[0].Text.ToString(),
                                     Conversion.Str(idx), 
                                     m_c1FlexGrid1[idx].GetData(m_c1FlexGrid1[idx].Row, 0).ToString());
                            
                            for(i = 0; i <= (m_c1FlexGrid1[idx].Cols.Count - 1); i++)
                            {
                                m_c1FlexGrid1[idx].SetData(m_c1FlexGrid1[idx].Row, i, "");
                            }
                        }
                        break;

                    case 1: // パターン１を展開
                        Msg   = "パターン１を選択パターンに展開しますか？";      // メッセージを定義します。
                        Title = "パターン展開";                                  // タイトルを定義します。

                        // 確認ウィンドウを表示する
                        dr = MessageBox.Show(Msg, Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        if (dr == DialogResult.Yes)
                        {
// [2025/06/24] p.hoi==================================================================>>
                            //int idx = tabControl1.SelectedIndex;
// MODIFY
                            int idx = SelectedTabIndex;
//<<=====================================================================================

                            switch (idx)
                            {
                                case 1:
                                case 2:
                                    k = 0;
                                    break;
                                case 4:
                                case 5:
                                    k = 3;
                                    break;
                                default:
                                    return;
                            }

                            for (i = 1; i <= (m_c1FlexGrid1[k].Rows.Count - 1); i++)
                            {
                                for (j = 0; j <= (m_c1FlexGrid1[k].Cols.Count - 1); j++)
                                {
                                    m_c1FlexGrid1[idx].SetData(i, j, m_c1FlexGrid1[k].GetData(i, j));
                                }
                            }
                        }
                        break;

                    case 2: // パターン２を展開
                        Msg   = "パターン２を選択パターンに展開しますか？";      // メッセージを定義します。
                        Title = "パターン展開";                                  // タイトルを定義します。

                        // 確認ウィンドウを表示する
                        dr = MessageBox.Show(Msg, Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        if (dr == DialogResult.Yes)
                        {
// [2025/06/24] p.hoi==================================================================>>
                            //int idx = tabControl1.SelectedIndex;
// MODIFY
                            int idx = SelectedTabIndex;
//<<=====================================================================================

                            switch (idx)
                            {
                                case 0:
                                case 2:
                                    k = 1;
                                    break;
                                case 3:
                                case 5:
                                    k = 4;
                                    break;
                                default:
                                    return;
                            }

                            for (i = 1; i <= (m_c1FlexGrid1[k].Rows.Count - 1); i++)
                            {
                                for (j = 0; j <= (m_c1FlexGrid1[k].Cols.Count - 1); j++)
                                {
                                    m_c1FlexGrid1[idx].SetData(i, j, m_c1FlexGrid1[k].GetData(i, j));
                                }
                            }
                        }
                        break;

                    case 3: // Ｌ１をＲ１に展開
                        Msg   = "１ｓｔ側パターン１を２ｎｄ側パターン１に展開しますか？";     // メッセージを定義します。
                        Title = "パターン展開";                                               // タイトルを定義します。

                        // 確認ウィンドウを表示する
                        dr = MessageBox.Show(Msg, Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        if (dr == DialogResult.Yes)
                        {
                            for (i = 1; i <= (m_c1FlexGrid1[0].Rows.Count - 1); i++)
                            {
                                for (j = 0; j <= (m_c1FlexGrid1[0].Cols.Count - 1); j++)
                                {
                                    m_c1FlexGrid1[3].SetData(i, j, m_c1FlexGrid1[0].GetData(i, j));
                                }
                            }
                        }
                        break;

                    case 4: // Ｌ２をＲ２に展開
                        Msg   = "１ｓｔ側パターン２を２ｎｄ側パターン２に展開しますか？";      // メッセージを定義します。
                        Title = "パターン展開";                                                // タイトルを定義します。

                        // 確認ウィンドウを表示する
                        dr = MessageBox.Show(Msg, Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        if (dr == DialogResult.Yes)
                        {
                            for (i = 1; i <= (m_c1FlexGrid1[1].Rows.Count - 1); i++)
                            {
                                for (j = 0; j <= (m_c1FlexGrid1[1].Cols.Count - 1); j++)
                                {
                                    m_c1FlexGrid1[4].SetData(i, j, m_c1FlexGrid1[1].GetData(i, j));
                                }
                            }
                        }
                        break;

                    case 5: // Ｌ３をＲ３に展開
                        Msg   = "１ｓｔ側パターン３を２ｎｄ側パターン３に展開しますか？";      // メッセージを定義します。
                        Title = "パターン展開";                                                // タイトルを定義します。

                        // 確認ウィンドウを表示する
                        dr = MessageBox.Show(Msg, Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        if (dr == DialogResult.Yes)
                        {
                            for (i = 1; i <= (m_c1FlexGrid1[2].Rows.Count - 1); i++)
                            {
                                for (j = 0; j <= (m_c1FlexGrid1[2].Cols.Count - 1); j++)
                                {
                                    m_c1FlexGrid1[5].SetData(i, j, m_c1FlexGrid1[2].GetData(i, j));
                                }
                            }
                        }
                        break;

                    default:
                        break;
                }
            }
        }
        
        /// <summary>
        /// ダブルクリック処理
        /// </summary>
        private void c1FlexGrid1_DoubleClick(object sender, EventArgs e)
        {
            // 「データ入力」ウィンドウを表示する
// [2025/06/24] p.hoi==================================================================>>
            //A_EditInp.VisibleWindow(true, m_c1FlexGrid1[tabControl1.SelectedIndex]);
// MODIFY
            A_EditInp.VisibleWindow(true, m_c1FlexGrid1[SelectedTabIndex]);
//<<=====================================================================================
        }
        
        /// <summary>
        /// 起動中ウィンドウを表示／非表示する
        /// </summary>
        static public void VisibleWindow(bool bShow, Row row = null)
        {
            if (bShow == true)
            {
                // フォームの初期化
                if ((m_Dlg is null) == true)
                {
                    m_Dlg = new A_EDIT();
                }

                // 「車種データ編集」画面のデータ
                m_row = row;

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
        private const int   TWIPS   = 15;           // ピクセルあたりのツイップ値

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

        // 設定データチェックサブ
        private int Input_Check() 
        {
// [2026/04/01][p.hoi][MODIFY、ADD]====================================================>>
// 「データ入力」ウィンドウで入力値のチェックを行うため、再チェックは不要
//  また、コンボボックス値のチェックを追加
//---------------------------------------------------------------------------------------
            /*int i;
            int j;
            int k;
            int l;

            l = 0;

            for (i = 0; i <= 5; i++) { // パタンのループ
                for (j = 1; j <= (m_c1FlexGrid1[i].Rows.Count - 1); j++) {
                    if (Convert.ToString(m_c1FlexGrid1[i].GetData(j, 0)) != "")
                    {
                        l++;
                        break;
                    }
                }

                if (l != 0) break;
            }

            if (l == 0) return (1);

            
            for (i = 0; i <= 5; i++) { // パタンのループ
                for (j = 1; j <= (m_c1FlexGrid1[i].Rows.Count - 1); j++) {
                    if (Convert.ToString(m_c1FlexGrid1[i].GetData(j, 0)) != "")
                    {
                        l = 0;
                        for (k = 2; k <= (m_c1FlexGrid1[i].Cols.Count - 1); k++)
                        {
                            if (Convert.ToString(m_c1FlexGrid1[i].GetData(j, k)) != "")
                            {
                                l = 1;
                                break;
                            }
                        }

                        if (l == 0) return (1);
                    }
                }
            }
            
            return (0);*/
//---------------------------------------------------------------------------------------
            for(int i = 1; i < m_Combo.Count(); i++) {
                if(m_Combo[i] != null) {
                    if(m_Combo[i].Text.Length > 0) {

                        string[] str = m_Combo[i].Text.Trim().Split(':');   // 項目番号取込み
                        // 数字チェック
                        if (int.TryParse(str[0].Normalize(NormalizationForm.FormKC), out _) != true) {
                            return (i);
                        }

                        // 全角→半角変換
                        m_Combo[i].Text = string.Concat(str[0].Normalize(NormalizationForm.FormKC), ":", str[1]);
                    }
                    else {
                        return (i);
                    }
                }
            }

            return (-1);
//<<=====================================================================================
        }

        // 車種情報の指定されたﾃﾞｰﾀを削除します
        private void Mdb_Del1(string Key_code1, string Key_code2, string Key_code3)
        {
            string SQl;
            string conStr = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={Mdb_Module.DataBase1};";

            try
            {
                using (OleDbConnection DB = new OleDbConnection(conStr))
                {
                    DB.Open(); // データベース接続

                    SQl  = "DELETE * From T_車種情報Ｔ ";
                    SQl += "WHERE Trim(識別コード) = '" + Key_code1.Trim() + "'" + " AND " + "Trim(タブＧＰ) = '" + Key_code2.Trim() + "'" + " AND " + "Trim(レシピ№) = '" + Key_code3.Trim() + "'";
                    
                    // 実行
                    using (OleDbCommand cmd = new OleDbCommand(SQl, DB)) {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
                ;
            }
        }

        // 車種情報の識別コードの全てを削除します
        private void Mdb_ALL_Del1() 
        {
            string SQl;
            string conStr = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={Mdb_Module.DataBase1};";

            try
            {
                using (OleDbConnection DB = new OleDbConnection(conStr))
                {
                    DB.Open(); // データベース接続

                    SQl  = "DELETE * From T_車種情報Ｍ ";
                    SQl += "WHERE Trim(識別コード) = '" + m_Combo[0].Text.Trim() + "' ";
                    
                    // 実行
                    using (OleDbCommand cmd = new OleDbCommand(SQl, DB)) {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
                ;
            }
        }

        // 識別コードマスタの読込み
        private void MDB_READ1()
        {
            string SQl;
            string conStr = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={Mdb_Module.DataBase1};";

            try
            {
                using (OleDbConnection DB = new OleDbConnection(conStr))
                {
                    DB.Open(); // データベース接続

                    SQl  = "select * from T_識別コード ";
                    SQl += "order by 識別コード";

                    using (OleDbCommand cmd = new OleDbCommand(SQl, DB))
                    {
                        using (OleDbDataReader RS = cmd.ExecuteReader())
                        {
                            if(RS.HasRows == true)
                            {
                                // 「識別コード」コンボボックスのアイテムをクリア
                                m_Combo[0].Items.Clear();

                                while (RS.Read())
                                {
                                    if (Strings.Len(RS["識別コード"]) > 1) {
                                        m_Combo[0].Items.Add(Strings.Right(RS["識別コード"].ToString(), Strings.Len(RS["識別コード"]) - 1));
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

        // 治具タイプマスタの読込み
        private void MDB_READ2()
        {
            string SQl;
            string conStr = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={Mdb_Module.DataBase1};";

            try
            {
                using (OleDbConnection DB = new OleDbConnection(conStr))
                {
                    DB.Open(); // データベース接続

                    SQl  = "select * from T_治具タイプ ";
                    SQl += "order by val(№)";

                    using (OleDbCommand cmd = new OleDbCommand(SQl, DB))
                    {
                        using (OleDbDataReader RS = cmd.ExecuteReader())
                        {
                            if(RS.HasRows == true)
                            {
                                // 「治具タイプ」コンボボックスのアイテムをクリア
                                m_Combo[1].Items.Clear();

                                while (RS.Read())
                                {
                                    if (Strings.Len(RS["治具タイプ"]) > 1) {
                                        m_Combo[1].Items.Add(Strings.Right(RS["№"].ToString(), Strings.Len(RS["№"]) - 1) + ":" + Strings.Right(RS["治具タイプ"].ToString(), Strings.Len(RS["治具タイプ"]) - 1));
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

        // 車種情報マスタの読込み
        private void MDB_READ3()
        {
            int i, j;
            int cnt0 = 0, cnt1 = 0, cnt2 = 0, cnt3 = 0, cnt4 = 0, cnt5 = 0;
            string SQl;
            string conStr = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={Mdb_Module.DataBase1};";

            try
            {
                using (OleDbConnection DB = new OleDbConnection(conStr))
                {
                    DB.Open();

                    SQl = "select * from T_車種情報Ｔ ";
                    SQl += "WHERE Trim(識別コード) = '" + Key_code1.Trim() + "'";
                    SQl += " order by val(タブＧＰ),val(レシピ№)";

                    using (OleDbCommand cmd = new OleDbCommand(SQl, DB))
                    using (OleDbDataReader RS = cmd.ExecuteReader())
                    {
                        if (RS.HasRows != true)
                        {
                            for (i = 2; i <= 3; i++) m_Combo[i].SelectedItem = m_Combo[i].Items[0];
                            for (i = 5; i <= 11; i++) m_Combo[i].SelectedItem = m_Combo[i].Items[0];
                            m_Combo[23].SelectedItem = m_Combo[23].Items[0];
                            for (i = 24; i <= 28; i++) m_Combo[i].SelectedItem = m_Combo[i].Items[0];
                        }
                        else
                        {
                            while (RS.Read())
                            {
                                if (Conversion.Val(RS["レシピ№"]) != 0)
                                {
                                    // KEY POINT:
                                    // DB stores タブＧＰ as: 0=LH Pat1, 1=LH Pat2, 2=LH Pat3, 3=RH Pat1, 4=RH Pat2, 5=RH Pat3
                                    // m_c1FlexGrid1 array is:  [0]=LH Pat1, [1]=LH Pat2, [2]=LH Pat3, [3]=RH Pat1, [4]=RH Pat2, [5]=RH Pat3
                                    // Therefore: m_c1FlexGrid1[i] where i = DB タブＧＰ is ALWAYS correct. No conversion needed.
                                    i = (int)Conversion.Val(RS["タブＧＰ"]);

                                    // Row counter per grid
                                    switch (i)
                                    {
                                        case 0: cnt0++; j = cnt0; break;
                                        case 1: cnt1++; j = cnt1; break;
                                        case 2: cnt2++; j = cnt2; break;
                                        case 3: cnt3++; j = cnt3; break;
                                        case 4: cnt4++; j = cnt4; break;
                                        case 5: cnt5++; j = cnt5; break;
                                        default: j = 1; break;
                                    }

                                    // LH/RH combo fill: DB 0,1,2 = LH side / DB 3,4,5 = RH side
                                    switch (i)
                                    {
                                        case 0: // LH Pat1,2,3
                                        case 1:
                                        case 2:
                                            m_Combo[6].Text = Convert.ToString(RS["ｼｰﾄﾀｲﾌﾟ"]);
                                            m_Combo[7].Text = Convert.ToString(RS["AGﾀｲﾌﾟ"]);
                                            m_Combo[8].Text = Convert.ToString(RS["ﾋｰﾀｰ"]);
                                            m_Combo[9].Text = Convert.ToString(RS["ﾊﾞｯｸﾙ"]);
                                            m_Combo[10].Text = Convert.ToString(RS["ﾍｯﾄﾞﾚｽﾄ"]);
                                            m_Combo[11].Text = Convert.ToString(RS["着座ｾﾝｻｰ"]);
                                            m_Combo[12].Text = Convert.ToString(RS["空調"]);
                                            m_Combo[13].Text = Convert.ToString(RS["表皮材"]);
                                            m_Combo[14].Text = Convert.ToString(RS["色"]);
                                            m_Combo[15].Text = Convert.ToString(RS["ﾗﾝﾊﾞｰ"]);
                                            m_Combo[16].Text = Convert.ToString(RS["背面ﾎﾟｹｯﾄ"]);
                                            m_Combo[17].Text = Convert.ToString(RS["ﾌｯﾄｳｴﾙﾗﾝﾌﾟ"]);
                                            m_Combo[18].Text = Convert.ToString(RS["ｱｰﾑﾚｽﾄ"]);
                                            m_Combo[19].Text = Convert.ToString(RS["QRG"]);
                                            m_Combo[20].Text = Convert.ToString(RS["ISOFIX"]);
                                            m_Combo[21].Text = Convert.ToString(RS["ﾊﾞｯｸﾎﾞｰﾄﾞ"]);
                                            m_Combo[22].Text = Convert.ToString(RS["ｵｯﾄﾏﾝ"]);
                                            if (Convert.ToString(RS["ｺﾝﾋﾞﾆﾌｯｸ"]) != "")
                                            {
                                                m_Combo[40].Text = Convert.ToString(RS["ｺﾝﾋﾞﾆﾌｯｸ"]);
                                                m_Combo[41].Text = Convert.ToString(RS["ｻｲﾄﾞﾃｰﾌﾞﾙ"]);
                                            }
                                            if (Convert.ToString(RS["ﾛﾎﾞｯﾄ"]) != "")
                                                m_Combo[44].Text = Convert.ToString(RS["ﾛﾎﾞｯﾄ"]);
                                            break;

                                        default: // RH Pat1,2,3 (DB タブＧＰ = 3,4,5)
                                            m_Combo[23].Text = Convert.ToString(RS["ｼｰﾄﾀｲﾌﾟ"]);
                                            m_Combo[24].Text = Convert.ToString(RS["AGﾀｲﾌﾟ"]);
                                            m_Combo[25].Text = Convert.ToString(RS["ﾋｰﾀｰ"]);
                                            m_Combo[26].Text = Convert.ToString(RS["ﾊﾞｯｸﾙ"]);
                                            m_Combo[27].Text = Convert.ToString(RS["ﾍｯﾄﾞﾚｽﾄ"]);
                                            m_Combo[28].Text = Convert.ToString(RS["着座ｾﾝｻｰ"]);
                                            m_Combo[29].Text = Convert.ToString(RS["空調"]);
                                            m_Combo[30].Text = Convert.ToString(RS["表皮材"]);
                                            m_Combo[31].Text = Convert.ToString(RS["色"]);
                                            m_Combo[32].Text = Convert.ToString(RS["ﾗﾝﾊﾞｰ"]);
                                            m_Combo[33].Text = Convert.ToString(RS["背面ﾎﾟｹｯﾄ"]);
                                            m_Combo[34].Text = Convert.ToString(RS["ﾌｯﾄｳｴﾙﾗﾝﾌﾟ"]);
                                            m_Combo[35].Text = Convert.ToString(RS["ｱｰﾑﾚｽﾄ"]);
                                            m_Combo[36].Text = Convert.ToString(RS["QRG"]);
                                            m_Combo[37].Text = Convert.ToString(RS["ISOFIX"]);
                                            m_Combo[38].Text = Convert.ToString(RS["ﾊﾞｯｸﾎﾞｰﾄﾞ"]);
                                            m_Combo[39].Text = Convert.ToString(RS["ｵｯﾄﾏﾝ"]);
                                            if (Convert.ToString(RS["ｺﾝﾋﾞﾆﾌｯｸ"]) != "")
                                            {
                                                m_Combo[42].Text = Convert.ToString(RS["ｺﾝﾋﾞﾆﾌｯｸ"]);
                                                m_Combo[43].Text = Convert.ToString(RS["ｻｲﾄﾞﾃｰﾌﾞﾙ"]);
                                            }
                                            
                                            if (Convert.ToString(RS["ﾛﾎﾞｯﾄ"]) != "")
                                                m_Combo[45].Text = Convert.ToString(RS["ﾛﾎﾞｯﾄ"]);
                                            break;
                                    }

                                    
                                    m_c1FlexGrid1[i].SetData(j, 0, Strings.Right(RS["レシピ№"].ToString(), Strings.Len(RS["レシピ№"]) - 1));
                                    m_c1FlexGrid1[i].SetData(j, 2, Strings.Right(RS["ST0101"].ToString(), Strings.Len(RS["ST0101"]) - 1));
                                    m_c1FlexGrid1[i].SetData(j, 3, Strings.Right(RS["ST0102"].ToString(), Strings.Len(RS["ST0102"]) - 1));
                                    m_c1FlexGrid1[i].SetData(j, 4, Strings.Right(RS["ST0103"].ToString(), Strings.Len(RS["ST0103"]) - 1));
                                    m_c1FlexGrid1[i].SetData(j, 5, Strings.Right(RS["ST0104"].ToString(), Strings.Len(RS["ST0104"]) - 1));
                                    m_c1FlexGrid1[i].SetData(j, 6, Strings.Right(RS["ST0105"].ToString(), Strings.Len(RS["ST0105"]) - 1));
                                    m_c1FlexGrid1[i].SetData(j, 7, Strings.Right(RS["ST0106"].ToString(), Strings.Len(RS["ST0106"]) - 1));
                                    m_c1FlexGrid1[i].SetData(j, 8, Strings.Right(RS["ST0107"].ToString(), Strings.Len(RS["ST0107"]) - 1));
                                    m_c1FlexGrid1[i].SetData(j, 9, Strings.Right(RS["ST0108"].ToString(), Strings.Len(RS["ST0108"]) - 1));
                                    m_c1FlexGrid1[i].SetData(j, 10, Strings.Right(RS["ST0109"].ToString(), Strings.Len(RS["ST0109"]) - 1));
                                    m_c1FlexGrid1[i].SetData(j, 11, Strings.Right(RS["ST0110"].ToString(), Strings.Len(RS["ST0110"]) - 1));
                                    m_c1FlexGrid1[i].SetData(j, 12, Strings.Right(RS["ST0111"].ToString(), Strings.Len(RS["ST0111"]) - 1));
                                    m_c1FlexGrid1[i].SetData(j, 13, Strings.Right(RS["ST0112"].ToString(), Strings.Len(RS["ST0112"]) - 1));
                                    m_c1FlexGrid1[i].SetData(j, 14, Strings.Right(RS["ST0113"].ToString(), Strings.Len(RS["ST0113"]) - 1));
                                    m_c1FlexGrid1[i].SetData(j, 15, Strings.Right(RS["ST0114"].ToString(), Strings.Len(RS["ST0114"]) - 1));
                                    m_c1FlexGrid1[i].SetData(j, 16, Strings.Right(RS["ST0115"].ToString(), Strings.Len(RS["ST0115"]) - 1));
                                    m_c1FlexGrid1[i].SetData(j, 17, Strings.Right(RS["ST0116"].ToString(), Strings.Len(RS["ST0116"]) - 1));
                                }
                            }
                        }
                    }
                }
            }
            catch {; }
        }

        // レシピマスタの読込み
        private void MDB_READ4()
        {
            int    i;
            int    j;
            string SQl;
            string conStr = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={Mdb_Module.DataBase1};";

            try
            {
                using (OleDbConnection DB = new OleDbConnection(conStr))
                {
                    DB.Open(); // データベース接続

                    for(i = 0; i <= (tabControl1.TabCount - 1); i++)
                    {
                        for (j = 1; j <= (m_c1FlexGrid1[i].Rows.Count - 1); j++)
                        {
                            if (Conversion.Val(m_c1FlexGrid1[i].GetData(j, 0)) != 0) {
                                SQl  = "select * from T_ﾚｼﾋﾟ ";
                                SQl += "WHERE Trim(レシピ№) = '" + m_c1FlexGrid1[i].GetData(j, 0).ToString().Trim() + "' ";
                                using (OleDbCommand cmd = new OleDbCommand(SQl, DB))
                                {
                                    using (OleDbDataReader RS = cmd.ExecuteReader())
                                    {
                                        if (RS.HasRows == true)
                                        {
                                            if (RS.Read())
                                            {
                                                m_c1FlexGrid1[i].SetData(j, 1, Strings.Right(RS["レシピ名"].ToString(), Strings.Len(RS["レシピ名"]) - 1));
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

        // ロボットプログラムマスタの読込み
        private void MDB_READ5()
        {
            string SQl;
            string conStr = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={Mdb_Module.DataBase1};";

            try
            {
                using (OleDbConnection DB = new OleDbConnection(conStr))
                {
                    DB.Open(); // データベース接続

                    SQl  = "select * from T_ＲＢプログラム ";
                    SQl += "order by val(ＰＧ№)";

                    using (OleDbCommand cmd = new OleDbCommand(SQl, DB))
                    {
                        using (OleDbDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows == true)
                            {
                                while (RS.Read())
                                {
                                    if (Strings.Len(RS["ＰＧ名"]) > 1)
                                    {
                                        m_Combo[4].Items.Add(Strings.Right(RS["ＰＧ№"].ToString(), Strings.Len(RS["ＰＧ№"]) - 1) + ":" + Strings.Right(RS["ＰＧ名"].ToString(), Strings.Len(RS["ＰＧ名"]) - 1));
                                        m_Combo[5].Items.Add(Strings.Right(RS["ＰＧ№"].ToString(), Strings.Len(RS["ＰＧ№"]) - 1) + ":" + Strings.Right(RS["ＰＧ名"].ToString(), Strings.Len(RS["ＰＧ名"]) - 1));
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

        // 車種情報Ｔの新規書込み/更新
        private void MDB_WRITE1()
        {
            int    i;
            int    j;
            string SP;
            string SQl;
            string conStr = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={Mdb_Module.DataBase1};";

            try
            {
                using (OleDbConnection DB = new OleDbConnection(conStr))
                {
                    DB.Open(); // データベース接続

                    for (i = 0; i <= (tabControl1.TabCount - 1); i++)
                    {
                        for (j = 1; j <= (m_c1FlexGrid1[i].Rows.Count - 1); j++)
                        {
                            if (Conversion.Val(m_c1FlexGrid1[i].GetData(j, 0)) != 0) {
                                SQl  = "select * from T_車種情報Ｔ ";
                                SQl += "WHERE Trim(識別コード) = '" + m_Combo[0].Text + "'" + " AND " + "Trim(タブＧＰ) = '" + Conversion.Str(i).Trim() + "'" + " AND " + "Trim(レシピ№) = '" + m_c1FlexGrid1[i].GetData(j, 0) + "'";

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
                                            SQl += "'" + m_Combo[0].Text + "',";
                                            SQl += "'" + Conversion.Str(i) + "' ,";
                                            SQl += "'" + m_Combo[1].Text + "',";
                                            SQl += "'" + m_Combo[2].Text + "',";
                                            SQl += "'" + m_Combo[3].Text + "',";
                                            SQl += "'" + m_Combo[4].Text + "',";
                                            SQl += "'" + m_Combo[5].Text + "',";

                                            switch (i) {
                                                case 0: // １ｓｔ側ｼｰﾄﾀｲﾌﾟ
                                                case 1:
                                                case 2:
                                                    SQl += "'" + m_Combo[6 ].Text + "',";
                                                    SQl += "'" + m_Combo[7 ].Text + "',";
                                                    SQl += "'" + m_Combo[8 ].Text + "',";
                                                    SQl += "'" + m_Combo[9 ].Text + "',";
                                                    SQl += "'" + m_Combo[10].Text + "',";
                                                    SQl += "'" + m_Combo[11].Text + "',";
                                                    SQl += "'" + m_Combo[12].Text + "',";
                                                    SQl += "'" + m_Combo[13].Text + "',";
                                                    SQl += "'" + m_Combo[14].Text + "',";
                                                    SQl += "'" + m_Combo[15].Text + "',";
                                                    SQl += "'" + m_Combo[16].Text + "',";
                                                    SQl += "'" + m_Combo[17].Text + "',";
                                                    SQl += "'" + m_Combo[18].Text + "',";
                                                    SQl += "'" + m_Combo[19].Text + "',";
                                                    SQl += "'" + m_Combo[20].Text + "',";
                                                    SQl += "'" + m_Combo[21].Text + "',";
                                                    SQl += "'" + m_Combo[22].Text + "',";
                                                    SQl += "'" + m_Combo[40].Text + "',";
                                                    SQl += "'" + m_Combo[41].Text + "',";
                                                    SQl += "'" + m_Combo[44].Text + "',";
                                                    break;
                                                case 3: // ２ｎｄ側ｼｰﾄﾀｲﾌﾟ
                                                case 4:
                                                case 5:
                                                    SQl += "'" + m_Combo[23].Text + "',";
                                                    SQl += "'" + m_Combo[24].Text + "',";
                                                    SQl += "'" + m_Combo[25].Text + "',";
                                                    SQl += "'" + m_Combo[26].Text + "',";
                                                    SQl += "'" + m_Combo[27].Text + "',";
                                                    SQl += "'" + m_Combo[28].Text + "',";
                                                    SQl += "'" + m_Combo[29].Text + "',";
                                                    SQl += "'" + m_Combo[30].Text + "',";
                                                    SQl += "'" + m_Combo[31].Text + "',";
                                                    SQl += "'" + m_Combo[32].Text + "',";
                                                    SQl += "'" + m_Combo[34].Text + "',";
                                                    SQl += "'" + m_Combo[35].Text + "',";
                                                    SQl += "'" + m_Combo[36].Text + "',";
                                                    SQl += "'" + m_Combo[37].Text + "',";
                                                    SQl += "'" + m_Combo[38].Text + "',";
                                                    SQl += "'" + m_Combo[39].Text + "',";
                                                    SQl += "'" + m_Combo[40].Text + "',";
                                                    SQl += "'" + m_Combo[42].Text + "',";
                                                    SQl += "'" + m_Combo[43].Text + "',";
                                                    SQl += "'" + m_Combo[45].Text + "',";
                                                    break;
                                            }

                                            switch (i) { // 作業パターン
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

                                            SQl += "'" + " " + m_c1FlexGrid1[i].GetData(j, 0 ) + "',"; // レシピ№
                                            SQl += "'" + " " + m_c1FlexGrid1[i].GetData(j, 2 ) + "',"; // 1,作業回数
                                            SQl += "'" + " " + m_c1FlexGrid1[i].GetData(j, 3 ) + "',"; // 2
                                            SQl += "'" + " " + m_c1FlexGrid1[i].GetData(j, 4 ) + "',"; // 3
                                            SQl += "'" + " " + m_c1FlexGrid1[i].GetData(j, 5 ) + "',"; // 4
                                            SQl += "'" + " " + m_c1FlexGrid1[i].GetData(j, 6 ) + "',"; // 5
                                            SQl += "'" + " " + m_c1FlexGrid1[i].GetData(j, 7 ) + "',"; // 6
                                            SQl += "'" + " " + m_c1FlexGrid1[i].GetData(j, 8 ) + "',"; // 7
                                            SQl += "'" + " " + m_c1FlexGrid1[i].GetData(j, 9 ) + "',"; // 8
                                            SQl += "'" + " " + m_c1FlexGrid1[i].GetData(j, 10) + "',"; // 9
                                            SQl += "'" + " " + m_c1FlexGrid1[i].GetData(j, 11) + "',"; // 10
                                            SQl += "'" + " " + m_c1FlexGrid1[i].GetData(j, 12) + "',"; // 11
                                            SQl += "'" + " " + m_c1FlexGrid1[i].GetData(j, 13) + "',"; // 12
                                            SQl += "'" + " " + m_c1FlexGrid1[i].GetData(j, 14) + "',"; // 13
                                            SQl += "'" + " " + m_c1FlexGrid1[i].GetData(j, 15) + "',"; // 14
                                            SQl += "'" + " " + m_c1FlexGrid1[i].GetData(j, 16) + "',"; // 15
                                            SQl += "'" + " " + m_c1FlexGrid1[i].GetData(j, 17) + "'"; // 16
                                            SQl += ")";
                                        }
                                        else
                                        {
                                            // 更新
                                            SQl  = "UPDATE T_車種情報Ｔ SET ";
                                            SQl += "識別コード = '"        + m_Combo[0].Text + "',";
                                            SQl += "タブＧＰ = '"          + Conversion.Str(i)       + "',";
                                            SQl += "治具タイプ = '"        + m_Combo[1].Text + "',";
                                            SQl += "治具切出し順 = '"      + m_Combo[2].Text + "',";
                                            SQl += "1st治具ストック段 = '" + m_Combo[3].Text + "',";
                                            SQl += "2nd治具ストック段 = '" + m_Combo[4].Text + "',";
                                            SQl += "向先 = '"             + m_Combo[5].Text + "',";
                                            switch (i)
                                            {
                                                case 0: // １ｓｔ側ｼｰﾄﾀｲﾌﾟ
                                                case 1:
                                                case 2:
                                                    SQl += "ｼｰﾄﾀｲﾌﾟ = '"       + m_Combo[6 ].Text + "',";
                                                    SQl += "AGﾀｲﾌﾟ = '"        + m_Combo[7 ].Text + "',";
                                                    SQl += "ﾋｰﾀｰ = '"          + m_Combo[8 ].Text + "',";
                                                    SQl += "ﾊﾞｯｸﾙ = '"         + m_Combo[9 ].Text + "',";
                                                    SQl += "ﾍｯﾄﾞﾚｽﾄ = '"       + m_Combo[10].Text + "',";
                                                    SQl += "着座ｾﾝｻｰ = '"      + m_Combo[11].Text + "',";
                                                    SQl += "空調 = '"          + m_Combo[12].Text + "',";
                                                    SQl += "表皮材 = '"        + m_Combo[13].Text + "',";
                                                    SQl += "色 = '" + m_Combo[14].Text + "',";
                                                    SQl += "ﾗﾝﾊﾞｰ = '"         + m_Combo[15].Text + "',";
                                                    SQl += "背面ﾎﾟｹｯﾄ = '"     + m_Combo[16].Text + "',";
                                                    SQl += "ﾌｯﾄｳｴﾙﾗﾝﾌﾟ = '"    + m_Combo[17].Text + "',";
                                                    SQl += "ｱｰﾑﾚｽﾄ = '"        + m_Combo[18].Text + "',";
                                                    SQl += "ﾀﾝﾌﾞﾙ = '"         + m_Combo[19].Text + "',";
                                                    SQl += "ISOFIX = '"        + m_Combo[20].Text + "',";
                                                    SQl += "ﾃｻﾞｰ = '"          + m_Combo[21].Text + "',";
                                                    SQl += "ｵｯﾄﾏﾝ = '"         + m_Combo[22].Text + "',";
                                                    SQl += "ｺﾝﾋﾞﾆﾌｯｸ = '"      + m_Combo[40].Text + "',";
                                                    SQl += "ｻｲﾄﾞﾃｰﾌﾞﾙ = '"     + m_Combo[41].Text + "',";
                                                    SQl += "ﾛﾎﾞｯﾄ = '"         + m_Combo[44].Text + "',";
                                                    break;
                                                case 3: // ２ｎｄ側ｼｰﾄﾀｲﾌﾟ
                                                case 4:
                                                case 5:
                                                    SQl += "ｼｰﾄﾀｲﾌﾟ = '"     + m_Combo[23].Text + "',";
                                                    SQl += "AGﾀｲﾌﾟ = '"      + m_Combo[24].Text + "',";
                                                    SQl += "ﾋｰﾀｰ = '"        + m_Combo[25].Text + "',";
                                                    SQl += "ﾊﾞｯｸﾙ = '"       + m_Combo[26].Text + "',";
                                                    SQl += "ﾍｯﾄﾞﾚｽﾄ = ' "    + m_Combo[27].Text + "',";
                                                    SQl += "着座ｾﾝｻｰ = '"    + m_Combo[28].Text + "',";
                                                    SQl += "空調 = '"        + m_Combo[29].Text + "',";
                                                    SQl += "表皮材 = '"      + m_Combo[30].Text + "',";
                                                    SQl += "色 = '" + m_Combo[31].Text + "',";
                                                    SQl += "ﾗﾝﾊﾞｰ = '"       + m_Combo[32].Text + "',";
                                                    SQl += "背面ﾎﾟｹｯﾄ = '"    + m_Combo[33].Text + "',";
                                                    SQl += "ﾌｯﾄｳｴﾙﾗﾝﾌﾟ = '"   + m_Combo[34].Text + "',";
                                                    SQl += "ｱｰﾑﾚｽﾄ = '"       + m_Combo[35].Text + "',";
                                                    SQl += "ﾀﾝﾌﾞﾙ = '"        + m_Combo[36].Text + "',";
                                                    SQl += "ISOFIX = '"       + m_Combo[37].Text + "',";
                                                    SQl += "ﾃｻﾞｰ = '"         + m_Combo[38].Text + "',";
                                                    SQl += "ｵｯﾄﾏﾝ = '"        + m_Combo[39].Text + "',";
                                                    SQl += "ｺﾝﾋﾞﾆﾌｯｸ = '"     + m_Combo[42].Text + "',";
                                                    SQl += "ｻｲﾄﾞﾃｰﾌﾞﾙ = '"    + m_Combo[43].Text + "',";
                                                    SQl += "ﾛﾎﾞｯﾄ = '"        + m_Combo[45].Text + "',";
                                                    break;
                                            }

                                            switch (i) {
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

                                            SQl += "レシピ№ = '" + " " + m_c1FlexGrid1[i].GetData(j, 0 ) + "',"; // レシピ№
                                            SQl += "ST0101 = '"   + " " + m_c1FlexGrid1[i].GetData(j, 2 ) + "',"; // 1,作業回数
                                            SQl += "ST0102 = '"   + " " + m_c1FlexGrid1[i].GetData(j, 3 ) + "',"; // 2
                                            SQl += "ST0103 = '"   + " " + m_c1FlexGrid1[i].GetData(j, 4 ) + "',"; // 3
                                            SQl += "ST0104 = '"   + " " + m_c1FlexGrid1[i].GetData(j, 5 ) + "',"; // 4
                                            SQl += "ST0105 = '"   + " " + m_c1FlexGrid1[i].GetData(j, 6 ) + "',"; // 5
                                            SQl += "ST0106 = '"   + " " + m_c1FlexGrid1[i].GetData(j, 7 ) + "',"; // 6
                                            SQl += "ST0107 = '"   + " " + m_c1FlexGrid1[i].GetData(j, 8 ) + "',"; // 7
                                            SQl += "ST0108 = '"   + " " + m_c1FlexGrid1[i].GetData(j, 9 ) + "',"; // 8
                                            SQl += "ST0109 = '"   + " " + m_c1FlexGrid1[i].GetData(j, 10) + "',"; // 9
                                            SQl += "ST0110 = '"   + " " + m_c1FlexGrid1[i].GetData(j, 11) + "',"; // 10
                                            SQl += "ST0111 = '"   + " " + m_c1FlexGrid1[i].GetData(j, 12) + "',"; // 11
                                            SQl += "ST0112 = '"   + " " + m_c1FlexGrid1[i].GetData(j, 13) + "',"; // 12
                                            SQl += "ST0113 = '"   + " " + m_c1FlexGrid1[i].GetData(j, 14) + "',"; // 13
                                            SQl += "ST0114 = '"   + " " + m_c1FlexGrid1[i].GetData(j, 15) + "',"; // 14
                                            SQl += "ST0115 = '"   + " " + m_c1FlexGrid1[i].GetData(j, 16) + "',"; // 15
                                            SQl += "ST0116 = '"   + " " + m_c1FlexGrid1[i].GetData(j, 17) + "' "; // 16
                                            SQl += "WHERE Trim(識別コード) = '" + m_Combo[0].Text + "'" + " AND " + "Trim(タブＧＰ) = '" + Conversion.Str(i).Trim() + "'" + " AND " + "Trim(レシピ№) = '" + m_c1FlexGrid1[i].GetData(j, 0) + "';";
                                        }
                                        
                                        // 実行
                                        using (OleDbCommand cmd1 = new OleDbCommand(SQl, DB)) {
                                            cmd1.ExecuteNonQuery();
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

        // 車種情報Ｍ新規書込み/更新
        private void MDB_WRITE2()
        {
            string SQl;
            string conStr = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={Mdb_Module.DataBase1};";

            try
            {
                using (OleDbConnection DB = new OleDbConnection(conStr))
                {
                    DB.Open(); // データベース接続

                    SQl  = "select * from T_車種情報Ｍ ";
                    SQl += "WHERE Trim(識別コード) = '" + m_Combo[0].SelectedItem + "' ";

                    using (OleDbCommand cmd = new OleDbCommand(SQl, DB))    // 検索条件をセット
                    {
                        using (OleDbDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows != true) {
                                // 新規書込み
                                SQl  = "INSERT INTO T_車種情報Ｍ ";
                                SQl += "(識別コード,治具タイプ,治具切出し順,1st治具ストック段,2nd治具ストック段,向先,1st側ｼｰﾄﾀｲﾌﾟ,2nd側ｼｰﾄﾀｲﾌﾟ) VALUES ";
                                SQl += "(";
                                SQl += "'" + m_Combo[0 ].Text + "',";
                                SQl += "'" + m_Combo[1 ].Text + "',";
                                SQl += "'" + m_Combo[2 ].Text + "',";
                                SQl += "'" + m_Combo[3 ].Text + "',";
                                SQl += "'" + m_Combo[4 ].Text + "',";
                                SQl += "'" + m_Combo[5 ].Text + "',";
                                SQl += "'" + m_Combo[6 ].Text + "',";
                                SQl += "'" + m_Combo[23].Text + "'";
                                SQl += ")";
                            }
                            else
                            {
                                // 更新
                                SQl  = "UPDATE T_車種情報Ｍ SET ";
                                SQl += "識別コード = '"        + m_Combo[0 ].Text + "',";
                                SQl += "治具タイプ = '"        + m_Combo[1 ].Text + "',";
                                SQl += "治具切出し順 = '"      + m_Combo[2 ].Text + "',";
                                SQl += "1st治具ストック段 = '" + m_Combo[3 ].Text + "',";
                                SQl += "2nd治具ストック段 = '" + m_Combo[4 ].Text + "',";
                                SQl += "向先 = '"              + m_Combo[5 ].Text + "',";
                                SQl += "1st側ｼｰﾄﾀｲﾌﾟ = '"      + m_Combo[6 ].Text + "',";
                                SQl += "2nd側ｼｰﾄﾀｲﾌﾟ = '"      + m_Combo[23].Text + "' ";
                                SQl += "WHERE Trim(識別コード) = '"  + m_Combo[0 ].Text.Trim() + "';";
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

        // 車種情報Ｍマスタの読込み
        private int MDB_CHECK1(string Key_code1)
        {
            string SQl;
            string conStr = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={Mdb_Module.DataBase1};";

            try
            {
                using (OleDbConnection DB = new OleDbConnection(conStr))
                {
                    DB.Open(); // データベース接続

                    SQl = $"select * from T_車種情報Ｍ WHERE Trim(識別コード) = '{Key_code1}'";

                    using (OleDbCommand cmd = new OleDbCommand(SQl, DB))
                    {
                        using (OleDbDataReader RS = cmd.ExecuteReader())
                        {
                            if (RS.HasRows == true)
                            {
                                return 1;
                            }
                        }
                    }
                }
            }
            catch
            {
                ;
            }

            return 0;
        }
        
// [2025/06/24] p.hoi==================================================================>>
        private int SelectedTabIndex
        {
            get
            {
                int idx = 0;

                switch (tabControl1.SelectedIndex)
                {
                    case 0: idx = 0; break;
                    case 1: idx = 3; break;
                    case 2: idx = 1; break;
                    case 3: idx = 4; break;
                    case 4: idx = 2; break;
                    case 5: idx = 5; break;
                }

                return (idx);
            }
        }
//<<=====================================================================================
    }
}
