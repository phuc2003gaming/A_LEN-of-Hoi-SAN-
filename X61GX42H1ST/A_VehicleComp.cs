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
    public partial class A_VehicleComp : Form
    {
        // プライベート変数
        private readonly A_PlcComp m_Parent;
        private Control m_Active;

        // 照合結果変数
        private A_MEIN.VEHICLE_COMP m_VehicleComp;
        private int m_Index;

        //コントローラ
        private readonly Label[] m_SrcLhHead;
        private readonly Label[] m_DstLhHead;
        private readonly Label[] m_SrcLhdat;
        private readonly Label[] m_DstLhdat;
        private readonly Label[] m_SrcRhHead;
        private readonly Label[] m_DstRhHead;
        private readonly Label[] m_SrcRhdat;
        private readonly Label[] m_DstRhdat;

        /// <summary>
        /// クラスのコンストラクタ
        /// </summary>
        public A_VehicleComp(A_PlcComp parent, A_MEIN.VEHICLE_COMP vehiclecomp, int idx)
        {
            InitializeComponent();

            // プライベート変数の初期化
            m_Parent = parent;
            m_Active = null;
            // 照合結果コピー
            m_VehicleComp = new A_MEIN.VEHICLE_COMP(vehiclecomp);
            m_Index = idx;

            // コントローラ初期化
            m_SrcLhHead = new Label[] {
                LH_SRC_label1, LH_SRC_label2,LH_SRC_label3,LH_SRC_label4,LH_SRC_label5,LH_SRC_label6
            };
            m_DstLhHead = new Label[] {
                LH_DST_label1, LH_DST_label2,LH_DST_label3,LH_DST_label4,LH_DST_label5,LH_DST_label6
            };

            m_SrcLhdat = new Label[] {
                LH_RSC_label7 , LH_RSC_label8 , LH_RSC_label9 , LH_RSC_label10, LH_RSC_label11,
                LH_RSC_label12, LH_RSC_label13, LH_RSC_label14, LH_RSC_label15, LH_RSC_label16,
                LH_RSC_label17, LH_RSC_label18, LH_RSC_label19, LH_RSC_label20, LH_RSC_label21,
                LH_RSC_label22, LH_RSC_label23, LH_RSC_label24,LH_RSC_label25,LH_RSC_label26
            };

            m_DstLhdat = new Label[] {
                LH_DST_label7 , LH_DST_label8 , LH_DST_label9 , LH_DST_label10, LH_DST_label11,
                LH_DST_label12, LH_DST_label13, LH_DST_label14, LH_DST_label15, LH_DST_label16,
                LH_DST_label17, LH_DST_label18, LH_DST_label19, LH_DST_label20, LH_DST_label21,
                LH_DST_label22, LH_DST_label23, LH_DST_label24,LH_DST_label25,LH_DST_label26
            };

            m_SrcRhHead = new Label[] {
                RH_SRC_label1, RH_SRC_label2,RH_SRC_label3,RH_SRC_label4,RH_SRC_label5,RH_SRC_label6
            };
            m_DstRhHead = new Label[] {
                RH_DST_label1, RH_DST_label2,RH_DST_label3,RH_DST_label4,RH_DST_label5,RH_DST_label6
            };

            m_SrcRhdat = new Label[] {
                RH_RSC_label7 , RH_RSC_label8 , RH_RSC_label9 , RH_RSC_label10, RH_RSC_label11,
                RH_RSC_label12, RH_RSC_label13, RH_RSC_label14, RH_RSC_label15, RH_RSC_label16,
                RH_RSC_label17, RH_RSC_label18, RH_RSC_label19, RH_RSC_label20, RH_RSC_label21,
                RH_RSC_label22,RH_RSC_label23, RH_RSC_label24,RH_RSC_label25,RH_RSC_label26
            };

            m_DstRhdat = new Label[] {
                RH_DST_label7 , RH_DST_label8 , RH_DST_label9 , RH_DST_label10, RH_DST_label11,
                RH_DST_label12, RH_DST_label13, RH_DST_label14, RH_DST_label15, RH_DST_label16,
                RH_DST_label17, RH_DST_label18, RH_DST_label19, RH_DST_label20, RH_DST_label21, 
                RH_DST_label22, RH_DST_label23, RH_DST_label24,RH_DST_label25,RH_DST_label26
            };

        }

        /// <summary>
        /// フォームの初期化
        /// </summary>
        private void A_VehicleComp_Load(object sender, EventArgs e)
        {
            // プライベート変数の初期化
            m_Active = ActiveControl;               // フォーカスを記憶する!!

            // 照合結果表示
            CompDisp();

            // フォーカスを移行する
            ActiveControl = m_Active;               // フォーカスを元に戻す!!
        }

        // 終了メニュー処理
        private void File_Command_Click_1(object sender, EventArgs e)
        {
            // ウィンドウ閉じる
            Close();
        }

        // 照合結果表示
        private void CompDisp()
        {
            SORT_RECIPE[] srcrp;
            SORT_RECIPE[] dstrp;
            A_MEIN.COMP_ID ret;
            int cnt;
            bool srchas = true;
            bool dsthas = true;

            srcrp = new SORT_RECIPE[99];
            dstrp = new SORT_RECIPE[99];

            // 再描画の無効化
            LH_SRC_c1FlexGrid.Redraw = false;
            LH_DST_c1FlexGrid.Redraw = false;
            RH_SRC_c1FlexGrid.Redraw = false;
            RH_DST_c1FlexGrid.Redraw = false;
            for (int ii = 0; ii < m_SrcLhHead.Length; ii++)
            {
                m_SrcLhHead[ii].SuspendLayout();
                m_DstLhHead[ii].SuspendLayout();
                m_SrcRhHead[ii].SuspendLayout();
                m_DstRhHead[ii].SuspendLayout();
            }
            for (int ii = 0; ii < m_SrcLhdat.Length; ii++)
            {
                m_SrcLhdat[ii].SuspendLayout();
                m_DstLhdat[ii].SuspendLayout();
                m_SrcRhdat[ii].SuspendLayout();
                m_DstRhdat[ii].SuspendLayout();
            }

            // 空のバッファ確認
            ret = m_VehicleComp.HeadCompare(m_Index, 0);
            if (ret == A_MEIN.COMP_ID.ONLYSIDE)
            {
                if (m_VehicleComp.SrcDat[m_Index].IsEmpty() == true) srchas = false;
                else dsthas = false;
            }

            // ＬＨ側
            {
                // 初期化
                for (int ii = 0; ii < srcrp.Length; ii++)
                {
                    srcrp[ii] = SORT_RECIPE.Empty;
                    dstrp[ii] = SORT_RECIPE.Empty;
                }

                if (srchas == true)
                {
                    // ヘッダ部
                    m_SrcLhHead[0].Text = m_VehicleComp.SrcDat[m_Index].Head.IdenCode;
                    m_SrcLhHead[1].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.JigType, m_VehicleComp.SrcDat[m_Index].Head.JigType);
                    m_SrcLhHead[2].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.JigOrder, m_VehicleComp.SrcDat[m_Index].Head.JigOrder);
                    m_SrcLhHead[3].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.JigStock, m_VehicleComp.SrcDat[m_Index].Head.Jig1st);
                    m_SrcLhHead[4].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.JigStock, m_VehicleComp.SrcDat[m_Index].Head.Jig2nd);
                    m_SrcLhHead[5].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Dest, m_VehicleComp.SrcDat[m_Index].Head.Dest);
                    // 詳細部
                    m_SrcLhdat[0].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.SeatType, m_VehicleComp.SrcDat[m_Index].LhDat.SeatType);
                    m_SrcLhdat[1].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.AgType, m_VehicleComp.SrcDat[m_Index].LhDat.AgType);
                    m_SrcLhdat[2].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Heater, m_VehicleComp.SrcDat[m_Index].LhDat.Heater);
                    m_SrcLhdat[3].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Buckle, m_VehicleComp.SrcDat[m_Index].LhDat.Buckle);
                    m_SrcLhdat[4].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Headrest, m_VehicleComp.SrcDat[m_Index].LhDat.Headrest);
                    m_SrcLhdat[5].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Upholstery, m_VehicleComp.SrcDat[m_Index].LhDat.Upholstery);
                    m_SrcLhdat[6].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.TorqueWrench, m_VehicleComp.SrcDat[m_Index].LhDat.TorqueWrench);
                    m_SrcLhdat[7].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Lumbar, m_VehicleComp.SrcDat[m_Index].LhDat.Lumbar);
                    m_SrcLhdat[8].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.BackPock, m_VehicleComp.SrcDat[m_Index].LhDat.BackPock);
                    m_SrcLhdat[9].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.FootwellLamp, m_VehicleComp.SrcDat[m_Index].LhDat.FootwellLamp);
                    m_SrcLhdat[10].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.ArmRest, m_VehicleComp.SrcDat[m_Index].LhDat.ArmRest);
                    m_SrcLhdat[11].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.QRG, m_VehicleComp.SrcDat[m_Index].LhDat.QRG);
                    m_SrcLhdat[12].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.ISOFIX, m_VehicleComp.SrcDat[m_Index].LhDat.ISOFIX);
                    m_SrcLhdat[13].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.ConvHook, m_VehicleComp.SrcDat[m_Index].LhDat.ConvHook);
                    m_SrcLhdat[14].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Site_Table, m_VehicleComp.SrcDat[m_Index].LhDat.Site_Table);
                    m_SrcLhdat[15].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Robot, m_VehicleComp.SrcDat[m_Index].LhDat.Robot);
                    m_SrcLhdat[16].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Backboard, m_VehicleComp.SrcDat[m_Index].LhDat.Backboard);
                    m_SrcLhdat[17].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Ottoman, m_VehicleComp.SrcDat[m_Index].LhDat.Ottoman);
                    m_SrcLhdat[18].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.SeatSen, m_VehicleComp.SrcDat[m_Index].LhDat.SeatSen);
                    m_SrcLhdat[19].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.AirCond, m_VehicleComp.SrcDat[m_Index].LhDat.AirCond);
                    
                    // レシピ部
                    cnt = 0;
                    for (int stno = 1; stno <= 16; stno++)
                    {
                        for (int kk = 0; kk < 25; kk++)
                        {
                            int recipeno = m_VehicleComp.SrcDat[m_Index].LhDat.GetRecipeNo(stno, kk);
                            if (recipeno > 0)
                            {
                                srcrp[cnt].Recipe = recipeno;                                             // レシピ
                                srcrp[cnt].St[stno - 1] = m_VehicleComp.SrcDat[m_Index].LhDat.GetSt(stno, kk);  // ST
                                cnt++;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }

                if (dsthas == true)
                {
                    // ヘッダ部
                    m_DstLhHead[0].Text = m_VehicleComp.DstDat[m_Index].Head.IdenCode;
                    m_DstLhHead[1].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.JigType, m_VehicleComp.DstDat[m_Index].Head.JigType);
                    m_DstLhHead[2].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.JigOrder, m_VehicleComp.DstDat[m_Index].Head.JigOrder);
                    m_DstLhHead[3].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.JigStock, m_VehicleComp.DstDat[m_Index].Head.Jig1st);
                    m_DstLhHead[4].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.JigStock, m_VehicleComp.DstDat[m_Index].Head.Jig2nd);
                    m_DstLhHead[5].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Dest, m_VehicleComp.DstDat[m_Index].Head.Dest);
                    // 詳細部
                    m_DstLhdat[0].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.SeatType, m_VehicleComp.DstDat[m_Index].LhDat.SeatType);
                    m_DstLhdat[1].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.AgType, m_VehicleComp.DstDat[m_Index].LhDat.AgType);
                    m_DstLhdat[2].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Heater, m_VehicleComp.DstDat[m_Index].LhDat.Heater);
                    m_DstLhdat[3].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Buckle, m_VehicleComp.DstDat[m_Index].LhDat.Buckle);
                    m_DstLhdat[4].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Headrest, m_VehicleComp.DstDat[m_Index].LhDat.Headrest);
                    m_DstLhdat[5].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Upholstery, m_VehicleComp.DstDat[m_Index].LhDat.Upholstery);
                    m_DstLhdat[6].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.TorqueWrench, m_VehicleComp.DstDat[m_Index].LhDat.TorqueWrench);
                    m_SrcLhdat[7].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Lumbar, m_VehicleComp.DstDat[m_Index].LhDat.Lumbar);
                    m_SrcLhdat[8].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.BackPock, m_VehicleComp.DstDat[m_Index].LhDat.BackPock);
                    m_SrcLhdat[9].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.FootwellLamp, m_VehicleComp.DstDat[m_Index].LhDat.FootwellLamp);
                    m_SrcLhdat[10].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.ArmRest, m_VehicleComp.DstDat[m_Index].LhDat.ArmRest);
                    m_SrcLhdat[11].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.QRG, m_VehicleComp.DstDat[m_Index].LhDat.QRG);
                    m_SrcLhdat[12].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.ISOFIX, m_VehicleComp.DstDat[m_Index].LhDat.ISOFIX);
                    m_SrcLhdat[13].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.ConvHook, m_VehicleComp.DstDat[m_Index].LhDat.ConvHook);
                    m_SrcLhdat[14].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Site_Table, m_VehicleComp.DstDat[m_Index].LhDat.Site_Table);
                    m_SrcLhdat[15].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Robot, m_VehicleComp.DstDat[m_Index].LhDat.Robot);
                    m_SrcLhdat[16].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Backboard, m_VehicleComp.DstDat[m_Index].LhDat.Backboard);
                    m_SrcLhdat[17].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Ottoman, m_VehicleComp.DstDat[m_Index].LhDat.Ottoman);
                    m_SrcLhdat[18].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.SeatSen, m_VehicleComp.DstDat[m_Index].LhDat.SeatSen);
                    m_SrcLhdat[19].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.AirCond, m_VehicleComp.DstDat[m_Index].LhDat.AirCond);

                    // レシピ部
                    cnt = 0;
                    for (int stno = 1; stno <= 16; stno++)
                    {
                        for (int kk = 0; kk < 25; kk++)
                        {
                            int recipeno = m_VehicleComp.DstDat[m_Index].LhDat.GetRecipeNo(stno, kk);
                            if (recipeno > 0)
                            {
                                dstrp[cnt].Recipe = recipeno;                                             // レシピ
                                dstrp[cnt].St[stno - 1] = m_VehicleComp.DstDat[m_Index].LhDat.GetSt(stno, kk);  // ST
                                cnt++;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }

                //色表示
                {
                    // ヘッダ部
                    for (int ii = 0; ii < m_SrcLhHead.Length; ii++)
                    {
                        if (ret != A_MEIN.COMP_ID.ONLYSIDE)
                        {
                            ret = m_VehicleComp.HeadCompare(m_Index, ii);
                        }

                        m_SrcLhHead[ii].BackColor = A_PlcComp.GetColor(ret);
                        m_DstLhHead[ii].BackColor = A_PlcComp.GetColor(ret);
                    }
                    // 詳細部部
                    for (int ii = 0; ii < m_SrcLhdat.Length; ii++)
                    {
                        if (ret != A_MEIN.COMP_ID.ONLYSIDE)
                        {
                            ret = m_VehicleComp.LHCompare(m_Index, ii);
                        }

                        m_SrcLhdat[ii].BackColor = A_PlcComp.GetColor(ret);
                        m_DstLhdat[ii].BackColor = A_PlcComp.GetColor(ret);
                    }
                }

                // レシピを並び替えて整列する
                SortByRecipe(ref srcrp, ref dstrp);
                // レシピ部表示
                for (int ii = 0; ii < srcrp.Length; ii++)
                {
                    // 照合結果
                    A_MEIN.COMP_ID comp = A_MEIN.COMP_ID.NONE;
                    if ((srcrp[ii].Recipe == 0) != (dstrp[ii].Recipe == 0)) { comp = A_MEIN.COMP_ID.ONLYSIDE; }
                    else if ((srcrp[ii].Recipe == 0) && (dstrp[ii].Recipe == 0)) { break; }
                    else
                    {
                        for (int stno = 1; stno <= 16; stno++)
                        {
                            if ((srcrp[ii].St[stno - 1] > 0) ||
                               (dstrp[ii].St[stno - 1] > 0))
                            {
                                if (srcrp[ii].St[stno - 1] != dstrp[ii].St[stno - 1])
                                {
                                    comp = A_MEIN.COMP_ID.MISMATCH;
                                }
                                else
                                {
                                    comp = A_MEIN.COMP_ID.MATCH;
                                }
                            }
                        }
                    }

                    // レシピ値表示
                    // 照合元
                    if (srcrp[ii].Recipe > 0)
                    {
                        LH_SRC_c1FlexGrid[(ii + 1), 1] = srcrp[ii].Recipe;           // レシピ
                        for (int stno = 1; stno <= 16; stno++)
                        {
                            if (srcrp[ii].St[stno - 1] > 0)
                            {
                                LH_SRC_c1FlexGrid[(ii + 1), stno + 1] = srcrp[ii].St[stno - 1];     // ST
                            }
                        }
                    }
                    // 照合先
                    if (dstrp[ii].Recipe > 0)
                    {
                        LH_DST_c1FlexGrid[(ii + 1), 1] = dstrp[ii].Recipe;           // レシピ
                        for (int stno = 1; stno <= 16; stno++)
                        {
                            if (dstrp[ii].St[stno - 1] > 0)
                            {
                                LH_DST_c1FlexGrid[(ii + 1), stno + 1] = dstrp[ii].St[stno - 1];     // ST
                            }
                        }
                    }
                    // 表示色
                    Color color = A_PlcComp.GetColor(comp);
                    for (int stno = 1; stno <= 16; stno++)
                    {
                        LH_SRC_c1FlexGrid.GetCellRange((ii + 1), stno + 1).StyleNew.BackColor = color;
                        LH_DST_c1FlexGrid.GetCellRange((ii + 1), stno + 1).StyleNew.BackColor = color;
                    }
                }
            }


            // ＲＨ側
            {
                // 初期化
                for (int ii = 0; ii < srcrp.Length; ii++)
                {
                    srcrp[ii] = SORT_RECIPE.Empty;
                    dstrp[ii] = SORT_RECIPE.Empty;
                }

                if (srchas == true)
                {
                    // ヘッダ部
                    m_SrcRhHead[0].Text = m_VehicleComp.SrcDat[m_Index].Head.IdenCode;
                    m_SrcRhHead[1].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.JigType, m_VehicleComp.SrcDat[m_Index].Head.JigType);
                    m_SrcRhHead[2].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.JigOrder, m_VehicleComp.SrcDat[m_Index].Head.JigOrder);
                    m_SrcRhHead[3].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.JigStock, m_VehicleComp.SrcDat[m_Index].Head.Jig1st);
                    m_SrcRhHead[4].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.JigStock, m_VehicleComp.SrcDat[m_Index].Head.Jig2nd);
                    m_SrcRhHead[5].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Dest, m_VehicleComp.SrcDat[m_Index].Head.Dest);
                    // 詳細部
                    m_SrcRhdat[0].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.SeatType, m_VehicleComp.SrcDat[m_Index].RhDat.SeatType);
                    m_SrcRhdat[1].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.AgType, m_VehicleComp.SrcDat[m_Index].RhDat.AgType);
                    m_SrcRhdat[2].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Heater, m_VehicleComp.SrcDat[m_Index].RhDat.Heater);
                    m_SrcRhdat[3].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Buckle, m_VehicleComp.SrcDat[m_Index].RhDat.Buckle);
                    m_SrcRhdat[4].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Headrest, m_VehicleComp.SrcDat[m_Index].RhDat.Headrest);
                    m_SrcRhdat[5].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Upholstery, m_VehicleComp.SrcDat[m_Index].RhDat.Upholstery);
                    m_SrcRhdat[6].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.TorqueWrench, m_VehicleComp.SrcDat[m_Index].RhDat.TorqueWrench);
                    m_SrcLhdat[7].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Lumbar, m_VehicleComp.SrcDat[m_Index].LhDat.Lumbar);
                    m_SrcLhdat[8].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.BackPock, m_VehicleComp.SrcDat[m_Index].LhDat.BackPock);
                    m_SrcLhdat[9].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.FootwellLamp, m_VehicleComp.SrcDat[m_Index].LhDat.FootwellLamp);
                    m_SrcLhdat[10].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.ArmRest, m_VehicleComp.SrcDat[m_Index].LhDat.ArmRest);
                    m_SrcLhdat[11].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.QRG, m_VehicleComp.SrcDat[m_Index].LhDat.QRG);
                    m_SrcLhdat[12].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.ISOFIX, m_VehicleComp.SrcDat[m_Index].LhDat.ISOFIX);
                    m_SrcLhdat[13].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.ConvHook, m_VehicleComp.SrcDat[m_Index].LhDat.ConvHook);
                    m_SrcLhdat[14].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Site_Table, m_VehicleComp.SrcDat[m_Index].LhDat.Site_Table);
                    m_SrcLhdat[15].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Robot, m_VehicleComp.SrcDat[m_Index].LhDat.Robot);
                    m_SrcLhdat[16].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Backboard, m_VehicleComp.SrcDat[m_Index].LhDat.Backboard);
                    m_SrcLhdat[17].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Ottoman, m_VehicleComp.SrcDat[m_Index].LhDat.Ottoman);
                    m_SrcLhdat[18].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.SeatSen, m_VehicleComp.SrcDat[m_Index].LhDat.SeatSen);
                    m_SrcLhdat[19].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.AirCond, m_VehicleComp.SrcDat[m_Index].LhDat.AirCond);
                    // レシピ部
                    cnt = 0;
                    for (int stno = 1; stno <= 16; stno++)
                    {
                        for (int kk = 0; kk < 25; kk++)
                        {
                            int recipeno = m_VehicleComp.SrcDat[m_Index].RhDat.GetRecipeNo(stno, kk);
                            if (recipeno > 0)
                            {
                                srcrp[cnt].Recipe = recipeno;                                             // レシピ
                                srcrp[cnt].St[stno - 1] = m_VehicleComp.SrcDat[m_Index].RhDat.GetSt(stno, kk);  // ST
                                cnt++;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }

                if (dsthas == true)
                {
                    // ヘッダ部
                    m_DstRhHead[0].Text = m_VehicleComp.DstDat[m_Index].Head.IdenCode;
                    m_DstRhHead[1].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.JigType, m_VehicleComp.DstDat[m_Index].Head.JigType);
                    m_DstRhHead[2].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.JigOrder, m_VehicleComp.DstDat[m_Index].Head.JigOrder);
                    m_DstRhHead[3].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.JigStock, m_VehicleComp.DstDat[m_Index].Head.Jig1st);
                    m_DstRhHead[4].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.JigStock, m_VehicleComp.DstDat[m_Index].Head.Jig2nd);
                    m_DstRhHead[5].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Dest, m_VehicleComp.DstDat[m_Index].Head.Dest);
                    // 詳細部
                    m_DstRhdat[0].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.SeatType, m_VehicleComp.DstDat[m_Index].RhDat.SeatType);
                    m_DstRhdat[1].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.AgType, m_VehicleComp.DstDat[m_Index].RhDat.AgType);
                    m_DstRhdat[2].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Heater, m_VehicleComp.DstDat[m_Index].RhDat.Heater);
                    m_DstRhdat[3].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Buckle, m_VehicleComp.DstDat[m_Index].RhDat.Buckle);
                    m_DstRhdat[4].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Headrest, m_VehicleComp.DstDat[m_Index].RhDat.Headrest);
                    m_DstRhdat[5].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Upholstery, m_VehicleComp.DstDat[m_Index].RhDat.Upholstery);
                    m_DstRhdat[6].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.TorqueWrench, m_VehicleComp.DstDat[m_Index].RhDat.TorqueWrench);
                    m_SrcLhdat[7].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Lumbar, m_VehicleComp.DstDat[m_Index].LhDat.Lumbar);
                    m_SrcLhdat[8].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.BackPock, m_VehicleComp.DstDat[m_Index].LhDat.BackPock);
                    m_SrcLhdat[9].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.FootwellLamp, m_VehicleComp.DstDat[m_Index].LhDat.FootwellLamp);
                    m_SrcLhdat[10].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.ArmRest, m_VehicleComp.DstDat[m_Index].LhDat.ArmRest);
                    m_SrcLhdat[11].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.QRG, m_VehicleComp.DstDat[m_Index].LhDat.QRG);
                    m_SrcLhdat[12].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.ISOFIX, m_VehicleComp.DstDat[m_Index].LhDat.ISOFIX);
                    m_SrcLhdat[13].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.ConvHook, m_VehicleComp.DstDat[m_Index].LhDat.ConvHook);
                    m_SrcLhdat[14].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Site_Table, m_VehicleComp.DstDat[m_Index].LhDat.Site_Table);
                    m_SrcLhdat[15].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Robot, m_VehicleComp.DstDat[m_Index].LhDat.Robot);
                    m_SrcLhdat[16].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Backboard, m_VehicleComp.DstDat[m_Index].LhDat.Backboard);
                    m_SrcLhdat[17].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.Ottoman, m_VehicleComp.DstDat[m_Index].LhDat.Ottoman);
                    m_SrcLhdat[18].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.SeatSen, m_VehicleComp.DstDat[m_Index].LhDat.SeatSen);
                    m_SrcLhdat[19].Text = Danpre_Xls_sub.GetMasterStr(Danpre_Xls_sub.M_ID.AirCond, m_VehicleComp.DstDat[m_Index].LhDat.AirCond);
                    // レシピ部
                    cnt = 0;
                    for (int stno = 1; stno <= 16; stno++)
                    {
                        for (int kk = 0; kk < 25; kk++)
                        {
                            int recipeno = m_VehicleComp.DstDat[m_Index].RhDat.GetRecipeNo(stno, kk);
                            if (recipeno > 0)
                            {
                                dstrp[cnt].Recipe = recipeno;                                             // レシピ
                                dstrp[cnt].St[stno - 1] = m_VehicleComp.DstDat[m_Index].RhDat.GetSt(stno, kk);  // ST
                                cnt++;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }

                //色表示
                {
                    // ヘッダ部
                    for (int ii = 0; ii < m_SrcRhHead.Length; ii++)
                    {
                        if (ret != A_MEIN.COMP_ID.ONLYSIDE)
                        {
                            ret = m_VehicleComp.HeadCompare(m_Index, ii);
                        }

                        m_SrcRhHead[ii].BackColor = A_PlcComp.GetColor(ret);
                        m_DstRhHead[ii].BackColor = A_PlcComp.GetColor(ret);
                    }
                    // 詳細部部
                    for (int ii = 0; ii < m_SrcRhdat.Length; ii++)
                    {
                        if (ret != A_MEIN.COMP_ID.ONLYSIDE)
                        {
                            ret = m_VehicleComp.RHCompare(m_Index, ii);
                        }

                        m_SrcRhdat[ii].BackColor = A_PlcComp.GetColor(ret);
                        m_DstRhdat[ii].BackColor = A_PlcComp.GetColor(ret);
                    }
                }

                // レシピを並び替えて整列する
                SortByRecipe(ref srcrp, ref dstrp);
                // レシピ部表示
                for (int ii = 0; ii < srcrp.Length; ii++)
                {
                    // 照合結果
                    A_MEIN.COMP_ID comp = A_MEIN.COMP_ID.NONE;
                    if ((srcrp[ii].Recipe == 0) != (dstrp[ii].Recipe == 0)) { comp = A_MEIN.COMP_ID.ONLYSIDE; }
                    else if ((srcrp[ii].Recipe == 0) && (dstrp[ii].Recipe == 0)) { break; }
                    else
                    {
                        for (int stno = 1; stno <= 16; stno++)
                        {
                            if ((srcrp[ii].St[stno - 1] > 0) ||
                               (dstrp[ii].St[stno - 1] > 0))
                            {
                                if (srcrp[ii].St[stno - 1] != dstrp[ii].St[stno - 1])
                                {
                                    comp = A_MEIN.COMP_ID.MISMATCH;
                                }
                                else
                                {
                                    comp = A_MEIN.COMP_ID.MATCH;
                                }
                            }
                        }
                    }

                    // レシピ値表示
                    // 照合元
                    if (srcrp[ii].Recipe > 0)
                    {
                        RH_SRC_c1FlexGrid[(ii + 1), 1] = srcrp[ii].Recipe;           // レシピ
                        for (int stno = 1; stno <= 16; stno++)
                        {
                            if (srcrp[ii].St[stno - 1] > 0)
                            {
                                RH_SRC_c1FlexGrid[(ii + 1), stno + 1] = srcrp[ii].St[stno - 1];     // ST
                            }
                        }
                    }
                    // 照合先
                    if (dstrp[ii].Recipe > 0)
                    {
                        RH_DST_c1FlexGrid[(ii + 1), 1] = dstrp[ii].Recipe;           // レシピ
                        for (int stno = 1; stno <= 16; stno++)
                        {
                            if (dstrp[ii].St[stno - 1] > 0)
                            {
                                RH_DST_c1FlexGrid[(ii + 1), stno + 1] = dstrp[ii].St[stno - 1];     // ST
                            }
                        }
                    }
                    // 表示色
                    Color color = A_PlcComp.GetColor(comp);
                    for (int stno = 1; stno <= 16; stno++)
                    {
                        RH_SRC_c1FlexGrid.GetCellRange((ii + 1), stno + 1).StyleNew.BackColor = color;
                        RH_DST_c1FlexGrid.GetCellRange((ii + 1), stno + 1).StyleNew.BackColor = color;
                    }
                }
            }

            // 再描画の再設定
            LH_SRC_c1FlexGrid.Redraw = true;
            LH_DST_c1FlexGrid.Redraw = true;
            RH_SRC_c1FlexGrid.Redraw = true;
            RH_DST_c1FlexGrid.Redraw = true;
            for (int ii = 0; ii < m_SrcLhHead.Length; ii++)
            {
                m_SrcLhHead[ii].ResumeLayout();
                m_DstLhHead[ii].ResumeLayout();
                m_SrcRhHead[ii].ResumeLayout();
                m_DstRhHead[ii].ResumeLayout();
            }
            for (int ii = 0; ii < m_SrcLhdat.Length; ii++)
            {
                m_SrcLhdat[ii].ResumeLayout();
                m_DstLhdat[ii].ResumeLayout();
                m_SrcRhdat[ii].ResumeLayout();
                m_DstRhdat[ii].ResumeLayout();
            }
        }


        // レシピを並び替えて整列する
        static void SortByRecipe(ref SORT_RECIPE[] src, ref SORT_RECIPE[] dst)
        {
            try
            {
                int i;

                // 有効なデータを取得する
                var list1 = src.Where(x => (x.Recipe != 0));
                var list2 = dst.Where(x => (x.Recipe != 0));

                //キー作成
                var keys = list1.Select(x => x.Recipe)
                                .Union(list2.Select(x => x.Recipe))
                                .OrderBy(x => x)
                                .ToList();

                var dict1 = list1.ToDictionary(x => x.Recipe, x => x);
                var dict2 = list2.ToDictionary(x => x.Recipe, x => x);

                // 新しいバッファ作成
                SORT_RECIPE[] newSrc = new SORT_RECIPE[src.Length];
                SORT_RECIPE[] newDst = new SORT_RECIPE[dst.Length];
                // 初期化
                for (i = 0; i < newSrc.Length; i++)
                {
                    newSrc[i] = SORT_RECIPE.Empty;
                    newDst[i] = SORT_RECIPE.Empty;
                }

                // 並び結果セット
                i = 0;
                foreach (var key in keys)
                {
                    if (i >= newSrc.Length) break;

                    newSrc[i] = dict1.ContainsKey(key) ? dict1[key] : SORT_RECIPE.Empty;
                    newDst[i] = dict2.ContainsKey(key) ? dict2[key] : SORT_RECIPE.Empty;

                    i++;
                }

                // データコーピ
                Array.Copy(newSrc, src, src.Length);
                Array.Copy(newDst, dst, dst.Length);
            }
            catch
            {
                ;
            }
        }

        /***********************************************************************
            レシピを並び替えて整列する使用構造体
        ***********************************************************************/
        public struct SORT_RECIPE
        {
            public int Recipe;        // レシピNo
            public A_MEIN.COMP_ID Comp;          // 照合結果
            public UInt16[] St;            // St1～ST16

            // コンストラクタ
            public SORT_RECIPE(Nullable<SORT_RECIPE> buf)
            {
                St = new UInt16[16];

                if (buf.HasValue == true)
                {
                    // コピーする
                    for (int ii = 0; ii < St.Length; ii++) { St[ii] = buf.Value.St[ii]; }
                    Recipe = buf.Value.Recipe;
                    Comp = buf.Value.Comp;
                }
                else
                {
                    // クリアする
                    for (int ii = 0; ii < St.Length; ii++) { St[ii] = 0; }
                    Recipe = 0;
                    Comp = A_MEIN.COMP_ID.NONE;
                }
            }

            // 空のバッファを取得する
            public static SORT_RECIPE Empty
            {
                get
                {
                    return (new SORT_RECIPE(null));
                }
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
