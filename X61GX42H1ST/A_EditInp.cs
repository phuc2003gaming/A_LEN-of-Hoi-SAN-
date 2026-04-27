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
    public partial class A_EditInp : Form
    {
        // プライベート変数
        private static   A_EditInp    m_Dlg = null;
        private static   C1FlexGrid   m_c1FlexGrid;

        private readonly TextBox[]    m_TextBox;

        /// <summary>
        /// クラスのコンストラクタ
        /// </summary>
        public A_EditInp()
        {
            InitializeComponent();

            m_TextBox = new TextBox[]
            {
                textBox1_0,  textBox1_1,  textBox1_2,  textBox1_3,  textBox1_4 ,  textBox1_5, 
                textBox1_6,  textBox1_7,  textBox1_8,  textBox1_9,  textBox1_10,  textBox1_11,
                textBox1_12, textBox1_13, textBox1_14, textBox1_15
            };
        }

        /// <summary>
        /// フォームの初期化
        /// </summary>
        private void A_EditInp_Load(object sender, EventArgs e)
        {
            // レシピマスタの読込み
            MDB_READ1();
            
            if(Convert.ToString(m_c1FlexGrid.GetData(m_c1FlexGrid.Row, 0)) != "")
            {
                comboBox1.SelectedItem = m_c1FlexGrid.GetData(m_c1FlexGrid.Row, 0) + ":" + m_c1FlexGrid.GetData(m_c1FlexGrid.Row, 1);  // レシピ№;レシピ名;
            }
            else
            {
                comboBox1.SelectedItem = "";
            }
           // SQl += "表皮材,色,ﾗﾝﾊﾞｰ,背面ﾎﾟｹｯﾄ,ﾌｯﾄｳｴﾙﾗﾝﾌﾟ,ｱｰﾑﾚｽﾄ,QRG,ISOFIX,ﾊﾞｯｸﾎﾞｰﾄﾞ,ｵｯﾄﾏﾝ,ﾛﾎﾞｯﾄ,ｺﾝﾋﾞﾆﾌｯｸ";
            for (int i = 2; i <= 17; i++)
            {
                m_TextBox[i-2].Text = Convert.ToString(m_c1FlexGrid.GetData(m_c1FlexGrid.Row, i));  // 回数設定値を移す
            }
        }

        /// <summary>
        /// 「レシピ名」コンボボックスの選択処理
        /// </summary>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i;

            for(i = 0; i < m_TextBox.Count(); i++)
            {
                m_TextBox[i].Text = "";
            }

            if (Strings.Len(comboBox1.Text) > 1) {

                int c = Convert.ToInt32(Strings.Left(comboBox1.Text, Strings.InStr(comboBox1.Text, ":") - 1));

                switch (c) {
                    case int k when ((k >= 1) && (k <= 110)):
                        for (i = 0; i <= 10; i++)
                        {
                            m_TextBox[i].Visible = true;
                        }

                        for (i = 11; i <= 15; i++)
                        {
                            m_TextBox[i].Visible = false;
                        }
                        break;

                    case int k when ((k >= 111) && (k <= 160)): // 12st
                        for (i = 0; i <= 15; i++)
                        {
                            m_TextBox[i].Visible = false;
                        }

                        m_TextBox[11].Visible = true;
                        break;

                    case int k when ((k >= 161) && (k <= 210)): // 13st
                        for (i = 0; i <= 15; i++)
                        {
                            m_TextBox[i].Visible = false;
                        }

                        m_TextBox[12].Visible = true;
                        break;

                    case int k when ((k >= 211) && (k <= 260)): // 14st
                        for (i = 0; i <= 15; i++)
                        {
                            m_TextBox[i].Visible = false;
                        }

                        m_TextBox[13].Visible = true;
                        break;

                    case int k when ((k >= 261) && (k <= 310)): /// 15st
                        for (i = 0; i <= 15; i++)
                        {
                            m_TextBox[i].Visible = false;
                        }

                        m_TextBox[14].Visible = true;
                        break;

                    case int k when ((k >= 311) && (k <= 360)): // 16st
                        for (i = 0; i <= 15; i++)
                        {
                            m_TextBox[i].Visible = false;
                        }

                        m_TextBox[15].Visible = true;
                        break;
                }
            }
        }

        /// <summary>
        /// テキストボックス変更
        /// </summary>
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int i;
            int j;
            int c;
            var textbox = sender as TextBox;

            if(textbox.Text.Length > 0)
            {
                if (Conversion.Val(textbox.Text) > 9)   // 入力値チェック
                {
                    textbox.Text = Strings.Left(textbox.Text, 1);
                }

                int Index = 0;

                for (i = 0; i < m_TextBox.Count(); i++) { if (m_TextBox[i].Equals(textbox) == true) { Index = i; break; } }
            
                for (i = 0; i < m_TextBox.Count(); i++) { // 複数入力チェック
                    if ((m_TextBox[i].Text != ""   ) &&
                        (i                 != Index))
                    {
                        m_TextBox[i].Text = "";
                    }
                }

                switch (Index) {
                        
                    case int k when ((k >=  0) && (k <= 10 )):  c = 10; break;
                    case int k when ((k >= 11) && (k <= 12 )):  c = 20; break;
                    case int k when  (k == 13):                 c = 25; break;
                    default:                                    c = 10; break;
                }

                // レシピ登録行チェック（１５項目以内）
                j = 0;
                for(i = 1; i <= (m_c1FlexGrid.Rows.Count - 1); i++)
                {
                    if (Convert.ToString(m_c1FlexGrid.GetData(i, Index + 2)) != "") {
                        j++;
                    }
                }

                if (j >= c) {
                    Program.MessageBox("登録範囲オーバー");
                    textbox.Text = "";
                }
            }
        }

        /// <summary>
        /// 「登録」ボタンをクリック
        /// </summary>
        private void button_Save_Click(object sender, EventArgs e)
        {
            // レシピ選択チェック
            if (Convert.ToString(comboBox1.SelectedItem) == "") return;

            // 入力チェック
            if(Input_Check() != true) {
                // エラーウィンドウを表示する
                MessageBox.Show("数値を入力してください", Properties.Resources.AppName, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                return;
            }

            m_c1FlexGrid.SetData(m_c1FlexGrid.Row, 0, Strings.Left(comboBox1.SelectedItem.ToString(),  Strings.InStr(comboBox1.SelectedItem.ToString(), ":") - 1));                                     // レシピ№
            m_c1FlexGrid.SetData(m_c1FlexGrid.Row, 1, Strings.Right(comboBox1.SelectedItem.ToString(), Strings.Len(comboBox1.SelectedItem) - Strings.InStr(comboBox1.SelectedItem.ToString(), ":")));   // レシピ名

            for(int i = 2; i <= (m_c1FlexGrid.Cols.Count - 1); i++)
            {
                m_c1FlexGrid.SetData(m_c1FlexGrid.Row, i, m_TextBox[i - 2].Text);  // レシピ名
            }

            // ウィンドウを非表示する
            VisibleWindow(false);
        }

        /// <summary>
        /// 「取消」ボタンをクリック
        /// </summary>
        private void button_Cancel_Click(object sender, EventArgs e)
        {
            // ウィンドウを非表示する
            VisibleWindow(false);
        }

        /// <summary>
        /// 起動中ウィンドウを表示／非表示する
        /// </summary>
        /// <param name="bShow"></param>表示／非表示する
        static public void VisibleWindow(bool bShow, C1FlexGrid c1FlexGrid = null)
        {
            if (bShow == true)
            {
                // フォームの初期化
                if ((m_Dlg is null) == true)
                {
                    m_Dlg = new A_EditInp();
                }

                m_c1FlexGrid = c1FlexGrid;

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
        
        //**********************************************************************
        // 
        // サブルーチン
        // 
        //**********************************************************************

        
        // レシピマスタの読込み
        private void MDB_READ1()
        {
            string SQl;
            string conStr = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={Mdb_Module.DataBase1};";

            try
            {
                using (OleDbConnection DB = new OleDbConnection(conStr))
                {
                    DB.Open(); // データベース接続

                    SQl  = "select * from T_ﾚｼﾋﾟ ";
                    SQl += "order by val(レシピ№)";

                    using (OleDbCommand cmd = new OleDbCommand(SQl, DB))
                    {
                        using (OleDbDataReader RS = cmd.ExecuteReader())
                        {
                            if(RS.HasRows == true)
                            {
                                // 「レシピ名」コンボボックスのアイテムをクリア
                                comboBox1.Items.Clear();

                                while (RS.Read())
                                {
                                    if (Strings.Len(RS["レシピ名"]) > 1) {
                                        comboBox1.Items.Add(Strings.Right(RS["レシピ№"].ToString(), Strings.Len(RS["レシピ№"]) - 1) + ":" + Strings.Right(RS["レシピ名"].ToString(), Strings.Len(RS["レシピ名"]) - 1));
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
        
// [2026/04/01][p.hoi][ADD]============================================================>>
// データが不正、または未入力の場合、エラーを表示するようにする。
//---------------------------------------------------------------------------------------
        // 設定データチェックサブ
        private bool Input_Check() {
            for (int i = 2; i <= (m_c1FlexGrid.Cols.Count - 1); i++) {
                if(m_TextBox[i - 2].Text.Length > 0) {
                    // 全角→半角変換
                    m_TextBox[i - 2].Text = m_TextBox[i - 2].Text.Normalize(NormalizationForm.FormKC);
                    // 数字チェック
                    if (int.TryParse(m_TextBox[i - 2].Text, out _) != true) {
                        return false;
                    }

                    return true;
                }
            }

            return false;
        }
//<<=====================================================================================
    }
}
