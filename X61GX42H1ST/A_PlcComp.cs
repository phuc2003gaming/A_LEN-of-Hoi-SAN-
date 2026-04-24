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
    public partial class A_PlcComp : Form
    {
        // プライベート変数
        private readonly A_MEIN  m_Parent;
        private Control          m_Active;

        // 照合結果変数
        private A_MEIN.TAG_COMP      m_TagComp;
        private A_MEIN.VEHICLE_COMP  m_VehicleComp;
        private DISP_MODE            m_DispMode;

        /// <summary>
        /// クラスのコンストラクタ
        /// </summary>
        public A_PlcComp(A_MEIN parent, A_MEIN.TAG_COMP tagcomp, A_MEIN.VEHICLE_COMP vehiclecomp)
        {
            InitializeComponent();

            // プライベート変数の初期化
            m_Parent = parent;
            m_Active = null;
            // 照合結果コピー
            m_TagComp     = new A_MEIN.TAG_COMP(tagcomp);
            m_VehicleComp = new A_MEIN.VEHICLE_COMP(vehiclecomp);
        }

        /// <summary>
        /// フォームの初期化
        /// </summary>
        private void A_PlcComp_Load(object sender, EventArgs e)
        {
            // プライベート変数の初期化
            m_Active = ActiveControl;               // フォーカスを記憶する!!

            // 初期化
            m_DispMode = DISP_MODE.MismatchOnly;

            // 固定行のコンテンツ設定
            TAG_c1FlexGrid[0, 0] = "タグNo.";
            TAG_c1FlexGrid[1, 0] = "タグNo.";
            TAG_c1FlexGrid[0, 1] = "照合元(ファイル)";
            TAG_c1FlexGrid[0, 2] = "照合元(ファイル)";
            TAG_c1FlexGrid[0, 3] = "照合元(ファイル)";
            TAG_c1FlexGrid[0, 4] = "照合先(ＰＬＣ)";
            TAG_c1FlexGrid[0, 5] = "照合先(ＰＬＣ)";
            TAG_c1FlexGrid[0, 6] = "照合先(ＰＬＣ)";
            TAG_c1FlexGrid[1, 1] = "治具タイプ";
            TAG_c1FlexGrid[1, 2] = "治具1st / 2nd";
            TAG_c1FlexGrid[1, 3] = "治具ｽﾄｯｸ段";
            TAG_c1FlexGrid[1, 4] = "治具タイプ";
            TAG_c1FlexGrid[1, 5] = "治具1st / 2nd";
            TAG_c1FlexGrid[1, 6] = "治具ｽﾄｯｸ段";
            //ヘッダー形式のマージ
            TAG_c1FlexGrid.AllowMerging         = AllowMergingEnum.FixedOnly;
            TAG_c1FlexGrid.Rows[0].AllowMerging = true;
            TAG_c1FlexGrid.Cols[0].AllowMerging = true;

            // 固定行のコンテンツ設定
            VEHICLE_c1FlexGrid[0, 0] = "No.";
            VEHICLE_c1FlexGrid[1, 0] = "No.";
            VEHICLE_c1FlexGrid[0, 1] = "照合元(ファイル)";
            VEHICLE_c1FlexGrid[0, 2] = "照合元(ファイル)";
            VEHICLE_c1FlexGrid[0, 3] = "照合元(ファイル)";
            VEHICLE_c1FlexGrid[0, 4] = "照合元(ファイル)";
            VEHICLE_c1FlexGrid[0, 5] = "照合先(ＰＬＣ)";
            VEHICLE_c1FlexGrid[0, 6] = "照合先(ＰＬＣ)";
            VEHICLE_c1FlexGrid[0, 7] = "照合先(ＰＬＣ)";
            VEHICLE_c1FlexGrid[0, 8] = "照合先(ＰＬＣ)";
            VEHICLE_c1FlexGrid[1, 1] = "識別コード";
            VEHICLE_c1FlexGrid[1, 2] = "治具タイプ";
            VEHICLE_c1FlexGrid[1, 3] = "治具切出し順";
            VEHICLE_c1FlexGrid[1, 4] = "向　先";
            VEHICLE_c1FlexGrid[1, 5] = "識別コード";
            VEHICLE_c1FlexGrid[1, 6] = "治具タイプ";
            VEHICLE_c1FlexGrid[1, 7] = "治具切出し順";
            VEHICLE_c1FlexGrid[1, 8] = "向　先";
            //ヘッダー形式のマージ
            VEHICLE_c1FlexGrid.AllowMerging         = AllowMergingEnum.FixedOnly;
            VEHICLE_c1FlexGrid.Rows[0].AllowMerging = true;
            VEHICLE_c1FlexGrid.Cols[0].AllowMerging = true;

            // タグ情報照合結果表示
            TagCompDisp();

            // 車種情報照合結果表示
            VehicleCompDisp();

            // 表示モード切替え処理
            DispModeChange();

            // フォーカスを移行する
            ActiveControl = m_Active;               // フォーカスを元に戻す!!
        }


        // ダブルクリック処理
        private void VEHICLE_c1FlexGrid_DoubleClick(object sender, EventArgs e)
        {
            string str = VEHICLE_c1FlexGrid.GetData(VEHICLE_c1FlexGrid.Row, 0).ToString();
            if ((str.Length > 0) &&
               (VEHICLE_c1FlexGrid.Row > 1))
            {
                int idx = Convert.ToInt32(str) - 1;
                //詳細結果ウィンドウ表示
                A_VehicleComp dlg = new A_VehicleComp(this, m_VehicleComp, idx);
                // ウィンドウ表示
                dlg.DoModal();
            }
        }
        
        // 表示モードボタンをクリック処理
        private void DsipMode_Click(object sender, EventArgs e)
        {
            if(m_DispMode == DISP_MODE.All) {
                m_DispMode = DISP_MODE.MismatchOnly;    // 不一致のみ
                buttonTag.Text = "全データ";
                buttonVeh.Text = "全データ";
            }
            else {
                m_DispMode = DISP_MODE.All;             // 全データ
                buttonTag.Text = "不一致のみ";
                buttonVeh.Text = "不一致のみ";
            }

            // 表示モード切替え処理
            DispModeChange();
        }

        // 終了メニュー処理
        private void File_Command_Click_1(object sender, EventArgs e)
        {
            // ウィンドウ閉じる
            Close();
        }

        // タグ情報照合結果表示
        private void TagCompDisp()
        {
            int  rcnt     = TAG_c1FlexGrid.Rows.Fixed;
            bool mismatch = false;

            // 再描画の無効化
            TAG_c1FlexGrid.Redraw = false;

            for (int ii = 0; ii < m_TagComp.ScrDat.Length; ii++)
            {
                Color wrk = GetColor(m_TagComp.Compare(ii));
                // 色セット
                if (wrk != Color.White)
                {
                    for (int kk = 1; kk <= 6; kk++) { TAG_c1FlexGrid.GetCellRange(rcnt, kk).StyleNew.BackColor = wrk; }
                    mismatch = true;
                }

                TAG_c1FlexGrid[rcnt, 0] = ii + 1;   // タグNo
                // 照合元(ファイル)
                if (m_TagComp.ScrDat[ii].IsEmpty() != true)
                {
                    TAG_c1FlexGrid[rcnt, 1] = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.JigType , m_TagComp.ScrDat[ii].JigType );   // 治具タイプ
                    TAG_c1FlexGrid[rcnt, 2] = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.JigLR   , m_TagComp.ScrDat[ii].JigLR   );   // 治具1st / 2nd
                    TAG_c1FlexGrid[rcnt, 3] = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.JigStock, m_TagComp.ScrDat[ii].JigStock);   // 治具ｽﾄｯｸ段
                }
                // 照合先(ＰＬＣ)
                if (m_TagComp.DstDat[ii].IsEmpty() != true)
                {
                    TAG_c1FlexGrid[rcnt, 4] = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.JigType , m_TagComp.DstDat[ii].JigType );   // 治具タイプ
                    TAG_c1FlexGrid[rcnt, 5] = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.JigLR   , m_TagComp.DstDat[ii].JigLR   );   // 治具1st / 2nd
                    TAG_c1FlexGrid[rcnt, 6] = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.JigStock, m_TagComp.DstDat[ii].JigStock);   // 治具ｽﾄｯｸ段
                }

                rcnt++;
            }

            if(mismatch != false) {
                labelTag.Text      = "不一致があります";
                labelTag.ForeColor = Color.Red;
            }
            else {
                labelTag.Text      = "不一致がありません";
                labelTag.ForeColor = Color.Black;
            }

            // 行数セット
            TAG_c1FlexGrid.Rows.Count = rcnt;

            // 再描画の再設定
            TAG_c1FlexGrid.Redraw = true;
        }

        // 車種情報照合結果表示
        private void VehicleCompDisp()
        {
            int  rcnt     = TAG_c1FlexGrid.Rows.Fixed;
            bool mismatch = false;

            // 再描画の無効化
            VEHICLE_c1FlexGrid.Redraw = false;

            for (int ii = 0; ii < m_VehicleComp.SrcDat.Length; ii++)
            {
                if ((m_VehicleComp.SrcDat[ii].IsEmpty() == true) &&
                    (m_VehicleComp.DstDat[ii].IsEmpty() == true)) {
                    break;
                }

                // 色表示
                A_MEIN.COMP_ID ret = A_MEIN.COMP_ID.NONE;
                do
                {
                    // ヘッダ部
                    {
                        for (int kk = 0; kk < 6; kk++)
                        {
                            ret = m_VehicleComp.HeadCompare(ii, kk);

                            if ((ret == A_MEIN.COMP_ID.ONLYSIDE) && (kk == 0)) break;
                            else if (ret > A_MEIN.COMP_ID.MATCH) { ret = A_MEIN.COMP_ID.MISMATCH; break; }
                        }
                        // 処理中断
                        if (ret > A_MEIN.COMP_ID.MATCH) break;
                    }

                    // ＬＨ側詳細部
                    {
                        for (int kk = 0; kk < 14; kk++)
                        {
                            ret = m_VehicleComp.LHCompare(ii, kk);
                            if (ret > A_MEIN.COMP_ID.MATCH) { ret = A_MEIN.COMP_ID.MISMATCH; break; }
                        }
                        // 処理中断
                        if (ret > A_MEIN.COMP_ID.MATCH) break;

                        // レシピ
                        for (int stno = 1; stno <= 16; stno++)    // st1-> st16
                        {
                            for(int hh = 0; hh < 25; hh++)
                            {
                                ret = m_VehicleComp.LHStCompare(ii, stno, hh);
                                if (ret > A_MEIN.COMP_ID.MATCH) { ret = A_MEIN.COMP_ID.MISMATCH; break; }
                            }
                            // 処理中断
                            if (ret > A_MEIN.COMP_ID.MATCH) break;
                        }
                        // 処理中断
                        if (ret > A_MEIN.COMP_ID.MATCH) break;
                    }

                    // ＲＨ側詳細部
                    {
                        for (int kk = 0; kk < 14; kk++)
                        {
                            ret = m_VehicleComp.RHCompare(ii, kk);
                            if (ret > A_MEIN.COMP_ID.MATCH) { ret = A_MEIN.COMP_ID.MISMATCH; break; }
                        }
                        // 処理中断
                        if (ret > A_MEIN.COMP_ID.MATCH) break;
                            
                        // レシピ
                        for (int stno = 1; stno <= 16; stno++)    // st1-> st16
                        {
                            for(int hh = 0; hh < 25; hh++)
                            {
                                ret = m_VehicleComp.RHStCompare(ii, stno, hh);
                                if (ret > A_MEIN.COMP_ID.MATCH) { ret = A_MEIN.COMP_ID.MISMATCH; break; }
                                else if(ret == A_MEIN.COMP_ID.NONE) break;
                            }
                            // 処理中断
                            if (ret > A_MEIN.COMP_ID.MATCH) break;
                        }
                        // 処理中断
                        if (ret > A_MEIN.COMP_ID.MATCH) break;
                    }
                }
                while (false);

                Color wrk = GetColor(ret);
                // 色セット
                if (wrk != Color.White)
                {
                    for (int kk = 1; kk <= 8; kk++) { VEHICLE_c1FlexGrid.GetCellRange(rcnt, kk).StyleNew.BackColor = wrk; }
                    mismatch = true;
                }

                VEHICLE_c1FlexGrid[rcnt, 0]                                     = rcnt - TAG_c1FlexGrid.Rows.Fixed + 1;                                // No

                // 照合元(ファイル)
                if (m_VehicleComp.SrcDat[ii].IsEmpty() != true)
                {
                    VEHICLE_c1FlexGrid[rcnt, 1] = m_VehicleComp.SrcDat[ii].Head.IdenCode;                                                              // 識別コード
                    VEHICLE_c1FlexGrid[rcnt, 2] = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.JigType,  m_VehicleComp.SrcDat[ii].Head.JigType );   // 治具タイプ
                    VEHICLE_c1FlexGrid[rcnt, 3] = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.JigOrder, m_VehicleComp.SrcDat[ii].Head.JigOrder);   // 治具切出し順
                    VEHICLE_c1FlexGrid[rcnt, 4] = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Dest,     m_VehicleComp.SrcDat[ii].Head.Dest    );   // 向　先
                }
                // 照合先(ＰＬＣ)
                if (m_VehicleComp.DstDat[ii].IsEmpty() != true)
                {
                    VEHICLE_c1FlexGrid[rcnt, 5] = m_VehicleComp.DstDat[ii].Head.IdenCode;                                                              // 識別コード
                    VEHICLE_c1FlexGrid[rcnt, 6] = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.JigType,  m_VehicleComp.DstDat[ii].Head.JigType );   // 治具タイプ
                    VEHICLE_c1FlexGrid[rcnt, 7] = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.JigOrder, m_VehicleComp.DstDat[ii].Head.JigOrder);   // 治具切出し順
                    VEHICLE_c1FlexGrid[rcnt, 8] = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Dest,     m_VehicleComp.DstDat[ii].Head.Dest    );   // 向　先
                }

                rcnt++;
            }

            // 行数セット
            VEHICLE_c1FlexGrid.Rows.Count = rcnt;
            
            if(mismatch != false) {
                labelVeh.Text      = "不一致があります";
                labelVeh.ForeColor = Color.Red;
            }
            else {
                labelVeh.Text      = "不一致がありません";
                labelVeh.ForeColor = Color.Black;
            }

            // 再描画の再設定
            VEHICLE_c1FlexGrid.Redraw = true;
        }

        // 表示モード切替え処理
        private void DispModeChange()
        {
            bool visible = false;

            // 再描画の無効化
            TAG_c1FlexGrid.Redraw     = false;
            VEHICLE_c1FlexGrid.Redraw = false;

            // 全データ表示
            if (m_DispMode == DISP_MODE.All) visible = true;

            for (int row = TAG_c1FlexGrid.Rows.Fixed; row < TAG_c1FlexGrid.Rows.Count; row++)
            {
                CellRange cell = TAG_c1FlexGrid.GetCellRange(row, 1);
                if (cell != null)
                {
                    if ((cell.StyleNew.BackColor != GetColor(A_MEIN.COMP_ID.MISMATCH)) &&
                        (cell.StyleNew.BackColor != GetColor(A_MEIN.COMP_ID.ONLYSIDE)))
                    {
                        TAG_c1FlexGrid.Rows[row].Visible = visible;
                    }
                }
                else
                {
                    TAG_c1FlexGrid.Rows[row].Visible = visible;
                }
            }


            for (int row = VEHICLE_c1FlexGrid.Rows.Fixed; row < VEHICLE_c1FlexGrid.Rows.Count; row++)
            {
                CellRange cell = VEHICLE_c1FlexGrid.GetCellRange(row, 1);

                if (cell != null)
                {
                    if ((cell.StyleNew.BackColor != GetColor(A_MEIN.COMP_ID.MISMATCH)) &&
                        (cell.StyleNew.BackColor != GetColor(A_MEIN.COMP_ID.ONLYSIDE)))
                    {
                        VEHICLE_c1FlexGrid.Rows[row].Visible = visible;
                    }
                }
                else
                {
                    VEHICLE_c1FlexGrid.Rows[row].Visible = visible;
                }
            }

            // 再描画の再設定
            TAG_c1FlexGrid.Redraw     = true;
            VEHICLE_c1FlexGrid.Redraw = true;
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

        // 照合結果色
        public static Color GetColor(A_MEIN.COMP_ID flg)
        {
            switch (flg)
            {
                case A_MEIN.COMP_ID.MATCH:    return Color.White;
                case A_MEIN.COMP_ID.MISMATCH: return Color.Yellow;
                case A_MEIN.COMP_ID.ONLYSIDE: return Color.Yellow;
                default:                      return Color.White;
            }
        }

        // 表示データ
        enum DISP_MODE
        {
            All,                // 全データ
            MismatchOnly        // 不一致のみ
        }
    }
}
