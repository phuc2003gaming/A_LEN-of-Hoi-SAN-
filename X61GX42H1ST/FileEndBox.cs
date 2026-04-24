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

namespace X61GX42H1ST
{
    public partial class FileEndBox : Form
    {
        // プライベート変数
        private static FileEndBox m_Dlg = null;

        /// <summary>
        /// クラスのコンストラクタ
        /// </summary>
        public FileEndBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// フォームの初期化
        /// </summary>
        private void FileEndBox_Load(object sender, EventArgs e)
        {
            label1.Text = Mdb_Module.DataBase1;
        }

        /// <summary>
        /// 「完了」ボタンをクリック
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            File.Copy(Mdb_Module.DataBase1, $@"{Mdb_Module.DataBase3}\{textBox1.Text}.mdb", true);

            // ウィンドウを非表示する
            VisibleWindow(false);

            // アプリケーション終了
            Application.Exit();
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
                    m_Dlg = new FileEndBox();
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
