using Microsoft.Extensions.DependencyInjection;
using Microsoft.Office.Interop.Excel;
using Nicosu.Profile;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Win32Api;
using X61GX42H1ST.Service;
using Application = System.Windows.Forms.Application;
using Icon = System.Drawing.Icon;

namespace X61GX42H1ST
{
    static class Program
    {
        // プライベート変数
        private static readonly string   m_szAppPath;
        private static A_MAIN            m_pMainWnd; 
        private static readonly CProfile m_Profile;
        public static IServiceProvider Services
        {
            get; private set;
        }
        // システムプロファイル情報
        public const string FN_PROFILE = "X61GX42H1ST.ini";

        /// <summary>
        /// クラスの静的コンストラクタ
        /// </summary>
        static Program()
        {
            // プライベート変数の初期化
            m_szAppPath = null;
            m_pMainWnd  = null;

            // カレントディレクトリを記憶する
            m_szAppPath = Environment.CurrentDirectory;

            // システムプロファイルの初期化
            m_Profile = new CProfile(m_szAppPath, FN_PROFILE);
        }

        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            // DIコンテナの構築 27/4/2026
            var services = new ServiceCollection();
            services.AddSingleton<Service_A_MAIN>();
            services.AddSingleton<Service_A_RESIPI_MF>();
            services.AddSingleton<Service_A_RESIPICALL>();
            services.AddSingleton<Service_A_SIKIBETSU_MF>();
            services.AddSingleton<Service_A_TAGJYOHO_MF>();
            services.AddSingleton<Service_Edit>();
            Services = services.BuildServiceProvider();


            bool ret;

            // 二重起動のチェック
            using (Mutex mutex = new Mutex(true, "錦陵工業㈱／NS-K 2ND搬送コンベア", out bool createdNew))
            {
                if (createdNew == true)
                {
                    try
                    {
                        // はじめから Main() メソッドにあったコードを実行する
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);

                        // 起動中ウィンドウを表示する
                        GBoot.VisibleWindow(true);

                        // メインウィンドウを表示する
                        m_pMainWnd = new A_MAIN(Services.GetRequiredService<Service_A_MAIN>());

                        // ＭＣプロトコル通信ＡＰＩ初期化
                        ret = Mx_sub.Initialize();

                        if(ret == true) {
                            // マスターアイテム初期化
                            ret = Danpre_Xls_sub.MasterInit();

                            // アプリケーションの実行
                            if(ret == true) {
                                Application.Run(m_pMainWnd);
                            }
                        }
                    }
                    catch /*(Exception e)*/
                    {
                        ;
                    }
                    finally
                    {
                        GBoot.VisibleWindow(false);
                        // 起動中ウィンドウを閉じる

                        Mx_sub.Release();
                    }
                }
                else
                {
                    // 多重起動禁止時に実行中のアプリケーションを最前面に表示する
                    Process process = GetPreviousProcess();
                    if (process != null)
                    {
                        WakeupWindow(process.MainWindowHandle);
                    }
                }
            }
        }
        
        /// <summary>
        /// アプリケーション名称を取得する。
        /// </summary>
        public static string GetApplication()
        {
            return (Properties.Resources.AppName);
        }

        /// <summary>
        /// バージョン番号を取得する。
        /// </summary>
        public static string GetVersion()
        {
            // バージョン情報
            string value = string.Empty;
            Assembly assembly = Assembly.GetExecutingAssembly();
            AssemblyName asmName = assembly.GetName();
            string version = asmName.Version.ToString();
            int len = 0;
            int cnt = 0;

            while (len < version.Length)
            {
                if ((version[len] == '.') ||
                    (version[len] == ','))
                {
                    cnt++;
                    if (cnt >= 3)
                    {
                        break;
                    }
                    value += version[len];
                }
                else
                {
                    value += version[len];
                }
                len++;
            }

            return (value);
        }

        /// <summary>
        /// メイン・フレームを取得する。
        /// </summary>
        public static A_MAIN GetWindow()
        {
            return (m_pMainWnd);
        }

        /// <summary>
        /// メイン・フレームのアイコンを取得する。
        /// </summary>
        public static Icon GetIcon()
        {
            return (Properties.Resources.ICON);
        }

        /// <summary>
        /// アプリケーションフォルダを取得する。
        /// </summary>
        public static string GetDirectory()
        {
            return (m_szAppPath);
        }

        /// <summary>
        /// メッセージボックスを表示する
        /// </summary>
        public static DialogResult MessageBox(string message, MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.None, MessageBoxDefaultButton defaultbutton = MessageBoxDefaultButton.Button1)
        {
            return (System.Windows.Forms.MessageBox.Show(message, GetApplication(), buttons, icon, defaultbutton));
        }

        /// <summary>
        /// デバッグ文字列を出力する
        /// </summary>
        public static void OutputDebugStringEx(string format, params object[] args)
        {
            string str;

            str = string.Format(format, args);
            Debug.WriteLine(str);
        }

        /***********************************************************************
            多重起動禁止時に実行中のアプリケーションを最前面に表示する
        ***********************************************************************/
        // 外部プロセスのウィンドウを起動する
        private static void WakeupWindow(IntPtr hWnd)
        {
            // メイン・ウィンドウが最小化されていれば元に戻す
            if (CWin32Api.IsIconic(hWnd))
            {
                CWin32Api.ShowWindowAsync(hWnd, CWin32Api.SW_RESTORE);
            }

            // メイン・ウィンドウを最前面に表示する
            CWin32Api.SetForegroundWindow(hWnd);
        }

        // 実行中の同じアプリケーションのプロセスを取得する
        private static Process GetPreviousProcess()
        {
            Process MyProcess;
            Process[] Processes;

            MyProcess = Process.GetCurrentProcess();
            Processes = Process.GetProcessesByName(MyProcess.ProcessName);

            foreach (Process process in Processes)
            {
                // 自分自身のプロセスＩＤは無視する
                if (process.Id != MyProcess.Id)
                {
                    // プロセスのフルパス名を比較して同じアプリケーションか検証
                    try
                    {
                        if (String.Compare(process.MainModule.FileName, MyProcess.MainModule.FileName, true) == 0)
                        {
                            // 同じフルパス名のプロセスを取得
                            return (process);
                        }
                    }
                    catch
                    {
                        ;
                    }
                }
            }

            // 同じアプリケーションのプロセスが見つからない！
            return (null);
        }

        /***********************************************************************
            システムプロファイル情報／メインウィンドウ表示オフセット
        ***********************************************************************/
        // 表示位置Ｘ
        public static int GetScreenX()
        {
            return (m_Profile.GetValue("SCREEN", "X", -7));
        }

        // 表示位置Ｙ
        public static int GetScreenY()
        {
            return (m_Profile.GetValue("SCREEN", "Y", 0));
        }

        // 表示幅
        public static int GetScreenWidth()
        {
            return (m_Profile.GetValue("SCREEN", "WIDTH", 0));
        }

        // 表示高さ
        public static int GetScreenHeight()
        {
            return (m_Profile.GetValue("SCREEN", "HEIGHT", 0));
        }

        // パス取込み
        public static string Get_ShtDenmast_Path()
        {
            return (m_Profile.GetString("PATH", "SHT_DENMAST", @"D:\X61GX42H1ST\MtFiles\項目マスター.xls"));
        }

        public static string Get_ShtDenprt_Path()
        {
            return (m_Profile.GetString("PATH", "SHT_DENPRT", @"D:\X61GX42H1ST\MtFiles\車種登録情報フォーム.xls"));
        }
    }
}
