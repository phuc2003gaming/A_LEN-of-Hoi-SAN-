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
    public partial class FileBox : Form
    {
        // プライベート変数
        private static A_MEIN  m_paren = null;
        private static FileBox m_Dlg   = null;

        /// <summary>
        /// クラスのコンストラクタ
        /// </summary>
        public FileBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// フォームの初期化
        /// </summary>
        private void FileBox_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 「参照」ボタンをクリック
        /// </summary>
        private void button_Ref_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Text Files (*.mdb)|*.mdb";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBox_Path.Text = openFileDialog.FileName;
            }
        }

        /// <summary>
        /// 「ファイル読込」ボタンをクリック
        /// </summary>
        private void button_Read_Click(object sender, EventArgs e)
        {
            string c1;
            string path = textBox_Path.Text;

            if (File.Exists(path))
            {
                if (Path.GetExtension(path).Equals(".mdb", StringComparison.OrdinalIgnoreCase))
                {
                    //フォルダパス
                    Mdb_Module.DataBase3 = Path.GetDirectoryName(path);

                    Mdb_Module.DataBase1 = path;
                    Mdb_Module.Select_DataBase = path;
                    // バクアップファイル
                    c1 = path.Substring(0, path.IndexOf("."));
                    Mdb_Module.DataBase2 = c1 + "Bakup.mdb";

                    // 車種情報Ｍマスタの読込み
                    A_MEIN.MDB_READ1(m_paren);

                    // ウィンドウを非表示する
                    VisibleWindow(null, false);
                }
                else
                {
                    Program.MessageBox("データベースファイルではありません。もう一度選択してください。", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                Program.MessageBox("ファイルが存在しません。ファイルを選択してください。", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        
        /// <summary>
        /// 起動中ウィンドウを表示／非表示する
        /// </summary>
        /// <param name="bShow"></param>表示／非表示する
        static public void VisibleWindow(A_MEIN frm, bool bShow)
        {
            if (bShow == true)
            {
                // フォームの初期化
                if ((m_Dlg is null) == true)
                {
                    m_Dlg = new FileBox();
                }

                // フォームの表示
                if ((m_Dlg is null) != true)
                {
                    if (m_Dlg.Visible != true)
                    {
                        ;
                    }
                    // メイン画面
                    m_paren = frm;

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
        
        /// <summary>
        /// ファイルを読み込んでいない、ファイル読込処理
        /// </summary>
        static public bool FileOpenCheck(A_MEIN frm)
        {
            DialogResult dr;

            if((Mdb_Module.DataBase1.Length <= 0) &&
               (Mdb_Module.DataBase2.Length <= 0))
            {
                // 確認ウィンドウを表示する
                dr = Program.MessageBox("データファイルが読み込まれていません。ファイルを選択しますか。", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.Yes)
                {
                    VisibleWindow(frm, true);
                }
            }

            if ((Mdb_Module.DataBase1.Length > 0) &&
                (Mdb_Module.DataBase2.Length > 0)) return true;
            else                                   return false;
        }
    }
}
