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
    public partial class A_PlcRecvWarring : Form
    {
        // プライベート変数
        private readonly A_MAIN  m_Parent;
        private Control          m_Active;

        // マスター項目存在いないリスト
        private List<(string, string)>     m_Warringlist = null;

        /// <summary>
        /// クラスのコンストラクタ
        /// </summary>
        public A_PlcRecvWarring(A_MAIN parent, List<(string, string)> list)
        {
            InitializeComponent();

            // プライベート変数の初期化
            m_Parent = parent;
            m_Active = null;
            // リストコピー
            m_Warringlist = new List<(string, string)>(list);
        }

        /// <summary>
        /// フォームの初期化
        /// </summary>
        private void A_PlcRecvWarring_Load(object sender, EventArgs e)
        {
            // プライベート変数の初期化
            m_Active = ActiveControl;               // フォーカスを記憶する!!

            // 固定行のコンテンツ設定
            c1FlexGrid[0, 0] = "No.";
            c1FlexGrid[0, 1] = "内容";
            c1FlexGrid[0, 2] = "内容";
            c1FlexGrid.AllowMerging         = AllowMergingEnum.FixedOnly;
            c1FlexGrid.Rows[0].AllowMerging = true;

            // 行数
            c1FlexGrid.Rows.Count = m_Warringlist.Count + 1;

            // マスター項目存在いないリスト表示
            ListDisp();

            // フォーカスを移行する
            ActiveControl = m_Active;               // フォーカスを元に戻す!!
        }

        // マスター項目存在いないリスト表示
        private void ListDisp()
        {
            // 再描画の無効化
            c1FlexGrid.Redraw = false;

            for (int ii = 0; ii < m_Warringlist.Count; ii++)
            {
                c1FlexGrid[ii + 1, 0] = ii + 1;                    // No
                c1FlexGrid[ii + 1, 1] = m_Warringlist[ii].Item1;   // 内容
                c1FlexGrid[ii + 1, 2] = m_Warringlist[ii].Item2;   // 内容
            }

            // 再描画の再設定
            c1FlexGrid.Redraw = true;
        }

        /// <summary>
        /// 「キャンセル」ボタンのクリック処理
        /// </summary
        private void button_Close_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;

            // ウィンドウを非表示する
            Close();
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
