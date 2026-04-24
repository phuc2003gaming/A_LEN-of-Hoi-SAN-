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
    public partial class PlcRecv : Form
    {
        // プライベート変数
        private readonly A_MEIN  m_Parent;
        private Control          m_Active;

        /// <summary>
        /// クラスのコンストラクタ
        /// </summary>
        public PlcRecv(A_MEIN parent)
        {
            InitializeComponent();

            // プライベート変数の初期化
            m_Parent = parent;
            m_Active = null;
        }

        /// <summary>
        /// フォームの初期化
        /// </summary>
        private void PlcRecv_Load(object sender, EventArgs e)
        {
            // プライベート変数の初期化
            m_Active = ActiveControl;               // フォーカスを記憶する!!

            // フォーカスを移行する
            ActiveControl = m_Active;               // フォーカスを元に戻す!!
        }

        /// <summary>
        /// 「参照」ボタンをクリック
        /// </summary>
        private void button_Ref_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "Text Files (*.mdb)|*.mdb";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBox_Path.Text = saveFileDialog.FileName;
            }
        }

        /// <summary>
        /// 「実行」ボタンをクリック
        /// </summary>
        private void button_Read_Click(object sender, EventArgs e)
        {
            string path = textBox_Path.Text;

            if (Path.GetExtension(path).Equals(".mdb", StringComparison.OrdinalIgnoreCase))
            {
                if (File.Exists(path) != true)
                {
                    // ファイル読込チェック
                    if (FileBox.FileOpenCheck(Program.GetWindow()) != true) return;

                    // ファイルコピー作成
                    File.Copy(Mdb_Module.DataBase1, path, true);
                }

                // ファイルパスセット
                {
                    Mdb_Module.DataBase3        = Path.GetDirectoryName(path);

                    Mdb_Module.DataBase1        = path;
                    Mdb_Module.Select_DataBase  = path;
                    // バクアップファイル
                    string c1                   = path.Substring(0, path.IndexOf("."));
                    Mdb_Module.DataBase2        = c1 + "Bakup.mdb";
                }

                DialogResult = DialogResult.OK;

                // ウィンドウを非表示する
                Close();
            }
            else
            {
                Program.MessageBox("データベースファイルではありません。もう一度選択してください。", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// ダイアログを表示する
        /// </summary>
        /// <returns></returns>
        public DialogResult DoModal()
        {
            DialogResult dr;

            // ダイアログの表示
            dr = ShowDialog(m_Parent);

            if (dr == DialogResult.OK)
            {
                ;
            }
            Dispose();

            return (dr);
        }
    }
}
