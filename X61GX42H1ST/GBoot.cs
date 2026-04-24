using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using X61GX42H1ST.Properties;

namespace X61GX42H1ST
{
    public partial class GBoot : Form
    {
        // プライベート変数
        private static GBoot    m_GBoot = null;

        /// <summary>
        /// クラスのコンストラクタ
        /// </summary>
        public GBoot()
        {
            InitializeComponent();
        }

        /// <summary>
        /// フォームの表示
        /// </summary>
        private void GBoot_Load(object sender, EventArgs e)
        {
            ;
        }

        /// <summary>
        /// 起動中ウィンドウを表示／非表示する
        /// </summary>
        static public void VisibleWindow(bool bShow)
        {
            if (bShow == true) {
                // フォームの初期化
                if ((m_GBoot is null) == true) {
                    m_GBoot = new GBoot();
                }

                // フォームの表示
                if ((m_GBoot is null) != true) {
                    if (m_GBoot.Visible != true) {
                        ;
                    }
                    m_GBoot.Show();
                    m_GBoot.Update();
                }
            }
            else {
                // フォームを閉じる
                if ((m_GBoot is null) != true) {
                    if (m_GBoot.Visible == true) {
                        m_GBoot.Close();
                    }
                    m_GBoot.Dispose();
                }

                // フォームの開放
                m_GBoot = null;
            }
        }
    }
}
