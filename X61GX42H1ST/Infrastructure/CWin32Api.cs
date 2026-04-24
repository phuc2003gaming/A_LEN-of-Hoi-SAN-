using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;

namespace Win32Api
{
    public static partial class CWin32Api
    {
        // ウィンドウメッセージ
        public  const int   WM_DESTROY                      = 0x00000002;
        public  const int   WM_ACTIVATE                     = 0x00000006;
        public  const int   WM_CLOSE                        = 0x00000010;
        public  const int   WM_QUERYENDSESSION              = 0x00000011;
        public  const int   WM_QUIT                         = 0x00000012;

        public  const int   WM_NOTIFY                       = 0x0000004E;

        public  const int   WM_KEYFIRST                     = 0x00000100;
        public  const int   WM_KEYDOWN                      = 0x00000100;
        public  const int   WM_KEYUP                        = 0x00000101;

        public  const int   WM_COMMAND                      = 0x00000111;
        public  const int   WM_SYSCOMMAND                   = 0x00000112;

        public  const int   WM_APP                          = 0x00008000;

        // Dialog Box Command IDs
        public  const int   IDOK                            = 1;
        public  const int   IDCANCEL                        = 2;
        public  const int   IDABORT                         = 3;
        public  const int   IDRETRY                         = 4;
        public  const int   IDIGNORE                        = 5;
        public  const int   IDYES                           = 6;
        public  const int   IDNO                            = 7;

        public  const int   IDCLOSE                         = 8;
        public  const int   IDHELP                          = 9;

        public  const int   IDTRYAGAIN                      = 10;
        public  const int   IDCONTINUE                      = 11;

        // MessageBox() Flags
        public  const uint  MB_OK                           = 0x00000000;
        public  const uint  MB_OKCANCEL                     = 0x00000001;
        public  const uint  MB_ABORTRETRYIGNORE             = 0x00000002;
        public  const uint  MB_YESNOCANCEL                  = 0x00000003;
        public  const uint  MB_YESNO                        = 0x00000004;
        public  const uint  MB_RETRYCANCEL                  = 0x00000005;

        public  const uint  MB_CANCELTRYCONTINUE            = 0x00000006;

        public  const uint  MB_ICONHAND                     = 0x00000010;
        public  const uint  MB_ICONQUESTION                 = 0x00000020;
        public  const uint  MB_ICONEXCLAMATION              = 0x00000030;
        public  const uint  MB_ICONASTERISK                 = 0x00000040;

        public  const uint  MB_USERICON                     = 0x00000080;
        public  const uint  MB_ICONWARNING                  = MB_ICONEXCLAMATION;
        public  const uint  MB_ICONERROR                    = MB_ICONHAND;

        public  const uint  MB_ICONINFORMATION              = MB_ICONASTERISK;
        public  const uint  MB_ICONSTOP                     = MB_ICONHAND;

        public  const uint  MB_DEFBUTTON1                   = 0x00000000;
        public  const uint  MB_DEFBUTTON2                   = 0x00000100;
        public  const uint  MB_DEFBUTTON3                   = 0x00000200;

        public  const uint  MB_DEFBUTTON4                   = 0x00000300;

        // WM_ACTIVATE state values
        public  const uint  WA_INACTIVE                     = 0;
        public  const uint  WA_ACTIVE                       = 1;
        public  const uint  WA_CLICKACTIVE                  = 2;

        // System Menu Command Values
        public  const uint  SC_SIZE                         = 0x0000F000;
        public  const uint  SC_MOVE                         = 0x0000F010;
        public  const uint  SC_MINIMIZE                     = 0x0000F020;
        public  const uint  SC_MAXIMIZE                     = 0x0000F030;
        public  const uint  SC_CLOSE                        = 0x0000F060;
        public  const uint  SC_MOUSEMENU                    = 0x0000F090;
        public  const uint  SC_KEYMENU                      = 0x0000F100;

        // Menu flags for Add/Check/EnableMenuItem()
        public  const uint  MF_BYCOMMAND                    = 0x00000000;
        public  const uint  MF_BYPOSITION                   = 0x00000400;

        public  const uint  MF_SEPARATOR                    = 0x00000800;

        public  const uint  MF_ENABLED                      = 0x00000000;
        public  const uint  MF_GRAYED                       = 0x00000001;
        public  const uint  MF_DISABLED                     = 0x00000002;

        // GetSystemMetrics() codes
        public  const int   SM_CXSCREEN                     = 0;
        public  const int   SM_CYSCREEN                     = 1;
        public  const int   SM_CXVSCROLL                    = 2;
        public  const int   SM_CYHSCROLL                    = 3;
        public  const int   SM_CYCAPTION                    = 4;
        public  const int   SM_CXBORDER                     = 5;
        public  const int   SM_CYBORDER                     = 6;
        public  const int   SM_CXDLGFRAME                   = 7;
        public  const int   SM_CYDLGFRAME                   = 8;
        public  const int   SM_CYVTHUMB                     = 9;
        public  const int   SM_CXHTHUMB                     = 10;
        public  const int   SM_CXICON                       = 11;
        public  const int   SM_CYICON                       = 12;
        public  const int   SM_CXCURSOR                     = 13;
        public  const int   SM_CYCURSOR                     = 14;
        public  const int   SM_CYMENU                       = 15;
        public  const int   SM_CXFULLSCREEN                 = 16;
        public  const int   SM_CYFULLSCREEN                 = 17;
        public  const int   SM_CYKANJIWINDOW                = 18;
        public  const int   SM_MOUSEPRESENT                 = 19;
        public  const int   SM_CYVSCROLL                    = 20;
        public  const int   SM_CXHSCROLL                    = 21;
        public  const int   SM_DEBUG                        = 22;
        public  const int   SM_SWAPBUTTON                   = 23;
        public  const int   SM_RESERVED1                    = 24;
        public  const int   SM_RESERVED2                    = 25;
        public  const int   SM_RESERVED3                    = 26;
        public  const int   SM_RESERVED4                    = 27;
        public  const int   SM_CXMIN                        = 28;
        public  const int   SM_CYMIN                        = 29;
        public  const int   SM_CXSIZE                       = 30;
        public  const int   SM_CYSIZE                       = 31;
        public  const int   SM_CXFRAME                      = 32;
        public  const int   SM_CYFRAME                      = 33;
        public  const int   SM_CXMINTRACK                   = 34;
        public  const int   SM_CYMINTRACK                   = 35;
        public  const int   SM_CXDOUBLECLK                  = 36;
        public  const int   SM_CYDOUBLECLK                  = 37;
        public  const int   SM_CXICONSPACING                = 38;
        public  const int   SM_CYICONSPACING                = 39;
        public  const int   SM_MENUDROPALIGNMENT            = 40;
        public  const int   SM_PENWINDOWS                   = 41;
        public  const int   SM_DBCSENABLED                  = 42;
        public  const int   SM_CMOUSEBUTTONS                = 43;

        public  const int   SM_CXFIXEDFRAME                 = SM_CXDLGFRAME;    // ;win40 name change
        public  const int   SM_CYFIXEDFRAME                 = SM_CYDLGFRAME;    // ;win40 name change
        public  const int   SM_CXSIZEFRAME                  = SM_CXFRAME;       // ;win40 name change
        public  const int   SM_CYSIZEFRAME                  = SM_CYFRAME;       // ;win40 name change

        public  const int   SM_SECURE                       = 44;
        public  const int   SM_CXEDGE                       = 45;
        public  const int   SM_CYEDGE                       = 46;
        public  const int   SM_CXMINSPACING                 = 47;
        public  const int   SM_CYMINSPACING                 = 48;
        public  const int   SM_CXSMICON                     = 49;
        public  const int   SM_CYSMICON                     = 50;
        public  const int   SM_CYSMCAPTION                  = 51;
        public  const int   SM_CXSMSIZE                     = 52;
        public  const int   SM_CYSMSIZE                     = 53;
        public  const int   SM_CXMENUSIZE                   = 54;
        public  const int   SM_CYMENUSIZE                   = 55;
        public  const int   SM_ARRANGE                      = 56;
        public  const int   SM_CXMINIMIZED                  = 57;
        public  const int   SM_CYMINIMIZED                  = 58;
        public  const int   SM_CXMAXTRACK                   = 59;
        public  const int   SM_CYMAXTRACK                   = 60;
        public  const int   SM_CXMAXIMIZED                  = 61;
        public  const int   SM_CYMAXIMIZED                  = 62;
        public  const int   SM_NETWORK                      = 63;
        public  const int   SM_CLEANBOOT                    = 67;
        public  const int   SM_CXDRAG                       = 68;
        public  const int   SM_CYDRAG                       = 69;

        public  const int   SM_SHOWSOUNDS                   = 70;

        public  const int   SM_CXMENUCHECK                  = 71;               // Use instead of GetMenuCheckMarkDimensions()!
        public  const int   SM_CYMENUCHECK                  = 72;
        public  const int   SM_SLOWMACHINE                  = 73;
        public  const int   SM_MIDEASTENABLED               = 74;

        public  const int   SM_MOUSEWHEELPRESENT            = 75;

        public  const int   SM_XVIRTUALSCREEN               = 76;
        public  const int   SM_YVIRTUALSCREEN               = 77;
        public  const int   SM_CXVIRTUALSCREEN              = 78;
        public  const int   SM_CYVIRTUALSCREEN              = 79;
        public  const int   SM_CMONITORS                    = 80;
        public  const int   SM_SAMEDISPLAYFORMAT            = 81;

        public  const int   SM_IMMENABLED                   = 82;

        public  const int   SM_CXFOCUSBORDER                = 83;
        public  const int   SM_CYFOCUSBORDER                = 84;

        public  const int   SM_TABLETPC                     = 86;
        public  const int   SM_MEDIACENTER                  = 87;
        public  const int   SM_STARTER                      = 88;
        public  const int   SM_SERVERR2                     = 89;

        public  const int   SM_MOUSEHORIZONTALWHEELPRESENT  = 91;
        public  const int   SM_CXPADDEDBORDER               = 92;

        public  const int   SM_DIGITIZER                    = 94;
        public  const int   SM_MAXIMUMTOUCHES               = 95;

// ※不要!! ===========================================================>
//   -> private const int   SM_CMETRICS_OTHER               = 76;               // その他の Windows
//   -> private const int   SM_CMETRICS_2000                = 83;               // Windows 2000
//   -> private const int   SM_CMETRICS_XP                  = 91;               // Windows XP
//   -> private const int   SM_CMETRICS_VISTA               = 93;               // Windows Vista
//<=====================================================================
        public  const int   SM_CMETRICS                     = 97;

        public  const int   SM_REMOTESESSION                = 0x1000;

        public  const int   SM_SHUTTINGDOWN                 = 0x2000;

        public  const int   SM_REMOTECONTROL                = 0x2001;

        public  const int   SM_CARETBLINKINGENABLED         = 0x2002;

        public  const int   SM_CONVERTIBLESLATEMODE         = 0x2003;
        public  const int   SM_SYSTEMDOCKED                 = 0x2004;

        // SetWindowPos Flags
        public  const uint  SWP_NOSIZE                      = 0x00000001;
        public  const uint  SWP_NOMOVE                      = 0x00000002;
        public  const uint  SWP_NOZORDER                    = 0x00000004;
        public  const uint  SWP_NOREDRAW                    = 0x00000008;
        public  const uint  SWP_NOACTIVATE                  = 0x00000010;
        public  const uint  SWP_FRAMECHANGED                = 0x00000020;       // The frame changed: send WM_NCCALCSIZE
        public  const uint  SWP_SHOWWINDOW                  = 0x00000040;
        public  const uint  SWP_HIDEWINDOW                  = 0x00000080;
        public  const uint  SWP_NOCOPYBITS                  = 0x00000100;
        public  const uint  SWP_NOOWNERZORDER               = 0x00000200;       // Don't do owner Z ordering
        public  const uint  SWP_NOSENDCHANGING              = 0x00000400;       // Don't send WM_WINDOWPOSCHANGING

        public  const int   HWND_TOP                        = 0;
        public  const int   HWND_BOTTOM                     = 1;
        public  const int   HWND_TOPMOST                    = -1;
        public  const int   HWND_NOTOPMOST                  = -2;

        // Ternary raster operations
        public  const uint  SRCCOPY                         = 0x00CC0020;

        // Ranges for control message IDs
        private const int   BCM_FIRST                       = 0x00001600;

        // ShowWindowAsync 関数のパラメータに渡す定義値
        public  const int   SW_RESTORE                      = 9;                // 画面を元の大きさに戻す

        // Window field offsets for GetWindowLong()
        public  const int   GWL_WNDPROC                     = -4;
        public  const int   GWL_HINSTANCE                   = -6;
        public  const int   GWL_HWNDPARENT                  = -8;
        public  const int   GWL_STYLE                       = -16;
        public  const int   GWL_EXSTYLE                     = -20;
        public  const int   GWL_USERDATA                    = -21;
        public  const int   GWL_ID                          = -12;

        // SetWindowsHook() codes
        public  const int   WH_MIN                          = -1;
        public  const int   WH_MSGFILTER                    = -1;
        public  const int   WH_JOURNALRECORD                = 0;
        public  const int   WH_JOURNALPLAYBACK              = 1;
        public  const int   WH_KEYBOARD                     = 2;
        public  const int   WH_GETMESSAGE                   = 3;
        public  const int   WH_CALLWNDPROC                  = 4;
        public  const int   WH_CBT                          = 5;
        public  const int   WH_SYSMSGFILTER                 = 6;
        public  const int   WH_MOUSE                        = 7;

        // CBT Hook Codes
        public const int    HCBT_MOVESIZE                   = 0;
        public const int    HCBT_MINMAX                     = 1;
        public const int    HCBT_QS                         = 2;
        public const int    HCBT_CREATEWND                  = 3;
        public const int    HCBT_DESTROYWND                 = 4;
        public const int    HCBT_ACTIVATE                   = 5;
        public const int    HCBT_CLICKSKIPPED               = 6;
        public const int    HCBT_KEYSKIPPED                 = 7;
        public const int    HCBT_SYSCOMMAND                 = 8;
        public const int    HCBT_SETFOCUS                   = 9;

        // Device Parameters for GetDeviceCaps()
        public const int    DRIVERVERSION                   = 0;                // Device driver version
        public const int    TECHNOLOGY                      = 2;                // Device classification
        public const int    HORZSIZE                        = 4;                // Horizontal size in millimeters
        public const int    VERTSIZE                        = 6;                // Vertical size in millimeters
        public const int    HORZRES                         = 8;                // Horizontal width in pixels
        public const int    VERTRES                         = 10;               // Vertical height in pixels
        public const int    BITSPIXEL                       = 12;               // Number of bits per pixel
        public const int    PLANES                          = 14;               // Number of planes
        public const int    NUMBRUSHES                      = 16;               // Number of brushes the device has
        public const int    NUMPENS                         = 18;               // Number of pens the device has
        public const int    NUMMARKERS                      = 20;               // Number of markers the device has
        public const int    NUMFONTS                        = 22;               // Number of fonts the device has
        public const int    NUMCOLORS                       = 24;               // Number of colors the device supports
        public const int    PDEVICESIZE                     = 26;               // Size required for device descriptor
        public const int    CURVECAPS                       = 28;               // Curve capabilities
        public const int    LINECAPS                        = 30;               // Line capabilities
        public const int    POLYGONALCAPS                   = 32;               // Polygonal capabilities
        public const int    TEXTCAPS                        = 34;               // Text capabilities
        public const int    CLIPCAPS                        = 36;               // Clipping capabilities
        public const int    RASTERCAPS                      = 38;               // Bitblt capabilities
        public const int    ASPECTX                         = 40;               // Length of the X leg
        public const int    ASPECTY                         = 42;               // Length of the Y leg
        public const int    ASPECTXY                        = 44;               // Length of the hypotenuse
        public const int    LOGPIXELSX                      = 88;               // Logical pixels/inch in X
        public const int    LOGPIXELSY                      = 90;               // Logical pixels/inch in Y

        public const int    SIZEPALETTE                     = 104;              // Number of entries in physical palette
        public const int    NUMRESERVED                     = 106;              // Number of reserved entries in palette
        public const int    COLORRES                        = 108;              // Actual color resolution

        // Printing related DeviceCaps. These replace the appropriate Escapes
        public const int    PHYSICALWIDTH                   = 110;              // Physical Width in device units
        public const int    PHYSICALHEIGHT                  = 111;              // Physical Height in device units
        public const int    PHYSICALOFFSETX                 = 112;              // Physical Printable Area x margin
        public const int    PHYSICALOFFSETY                 = 113;              // Physical Printable Area y margin
        public const int    SCALINGFACTORX                  = 114;              // Scaling factor x
        public const int    SCALINGFACTORY                  = 115;              // Scaling factor y

        // Display driver specific
        public const int    VREFRESH                        = 116;              // Current vertical refresh rate of the display device (for displays only) in Hz
        public const int    DESKTOPVERTRES                  = 117;              // Horizontal width of entire desktop in pixels
        public const int    DESKTOPHORZRES                  = 118;              // Vertical height of entire desktop in pixels
        public const int    BLTALIGNMENT                    = 119;              // Preferred blt alignment

        public const int    SHADEBLENDCAPS                  = 120;              // Shading and blending caps
        public const int    COLORMGMTCAPS                   = 121;              // Color Management caps
    }

    //////////////////////////////
    // SYSTEMTIME 構造体        //
    //////////////////////////////
    public static partial class CWin32Api
    {
        // System time is represented with the following structure:
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct SYSTEMTIME
        {
            public ushort   wYear;
            public ushort   wMonth;
            public ushort   wDayOfWeek;
            public ushort   wDay;
            public ushort   wHour;
            public ushort   wMinute;
            public ushort   wSecond;
            public ushort   wMiliseconds;

            public SYSTEMTIME(Nullable<SYSTEMTIME> st)
            {
                if (st.HasValue == true) {
                    wYear        = st.Value.wYear;
                    wMonth       = st.Value.wMonth;
                    wDayOfWeek   = st.Value.wDayOfWeek;
                    wDay         = st.Value.wDay;
                    wHour        = st.Value.wHour;
                    wMinute      = st.Value.wMinute;
                    wSecond      = st.Value.wSecond;
                    wMiliseconds = st.Value.wMiliseconds;
                }
                else {
                    wYear        = 0;
                    wMonth       = 0;
                    wDayOfWeek   = 0;
                    wDay         = 0;
                    wHour        = 0;
                    wMinute      = 0;
                    wSecond      = 0;
                    wMiliseconds = 0;
                }
            }

            private SYSTEMTIME(int Year, int Month, int Day, int Hour, int Minute, int Second, int Millisecond, DayOfWeek DayOfWeek)
            {
                wYear        = (ushort)Year;
                wMonth       = (ushort)Month;
                wDayOfWeek   = (ushort)DayOfWeek;
                wDay         = (ushort)Day;
                wHour        = (ushort)Hour;
                wMinute      = (ushort)Minute;
                wSecond      = (ushort)Second;
                wMiliseconds = (ushort)Millisecond;
            }

            public bool IsValid
            {
                get
                {
                    if ((wYear  != 0) &&
                        (wMonth != 0) &&
                        (wDay   != 0)) {
                        return (true);
                    }
                    return (false);
                }
            }

            public static SYSTEMTIME Empty
            {
                get
                {
                    return (new SYSTEMTIME(null));
                }
            }

            public bool IsEmpty
            {
                get
                {
                    if ((wYear        == 0) &&
                        (wMonth       == 0) &&
                        (wDay         == 0) &&
                        (wDayOfWeek   == 0) &&
                        (wHour        == 0) &&
                        (wMinute      == 0) &&
                        (wSecond      == 0) &&
                        (wMiliseconds == 0)) {
                        return (true);
                    }
                    return (false);
                }
            }

            // DateTimeへの代入
            public static explicit operator DateTime (SYSTEMTIME st)    // ※明示的型変換(キャスト要)
            {
                if (st.IsValid == true) {
                    return (new DateTime((int)st.wYear, (int)st.wMonth, (int)st.wDay, (int)st.wHour, (int)st.wMinute, (int)st.wSecond, (int)st.wMiliseconds));
                }
                return (new DateTime());
            }

            // DateTimeからの代入
            public static implicit operator SYSTEMTIME (DateTime dt)    // ※暗黙的型変換(キャスト不要)
            {
                return (new SYSTEMTIME(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, dt.Millisecond, dt.DayOfWeek));
            }

            // SYSTEMTIMEとの比較
            public static bool operator == (SYSTEMTIME st1, SYSTEMTIME st2)
            {
                if ((st1.wYear        == st2.wYear       ) &&
                    (st1.wMonth       == st2.wMonth      ) &&
                    (st1.wDay         == st2.wDay        ) &&
                    (st1.wDayOfWeek   == st2.wDayOfWeek  ) &&
                    (st1.wHour        == st2.wHour       ) &&
                    (st1.wMinute      == st2.wMinute     ) &&
                    (st1.wSecond      == st2.wSecond     ) &&
                    (st1.wMiliseconds == st2.wMiliseconds)) {
                    return (true);
                }
                return (false);
            }
            public static bool operator != (SYSTEMTIME st1, SYSTEMTIME st2)
            {
                return (!(st1 == st2));
            }

            // DateTimeとの比較
            public static bool operator == (SYSTEMTIME st, DateTime dt)
            {
                if ((st.wYear        == ((ushort)dt.Year       )) &&
                    (st.wMonth       == ((ushort)dt.Month      )) &&
                    (st.wDay         == ((ushort)dt.Day        )) &&
                    (st.wDayOfWeek   == ((ushort)dt.DayOfWeek  )) &&
                    (st.wHour        == ((ushort)dt.Hour       )) &&
                    (st.wMinute      == ((ushort)dt.Minute     )) &&
                    (st.wSecond      == ((ushort)dt.Second     )) &&
                    (st.wMiliseconds == ((ushort)dt.Millisecond))) {
                    return (true);
                }
                return (false);
            }
            public static bool operator != (SYSTEMTIME st, DateTime dt)
            {
                return (!(st == dt));
            }
            public static bool operator == (DateTime dt, SYSTEMTIME st)
            {
                return ( (st == dt));
            }
            public static bool operator != (DateTime dt, SYSTEMTIME st)
            {
                return (!(st == dt));
            }

            //////////////////////
            // Warning対策!!    //
            //////////////////////
            public override bool Equals(object value)
            {
                if ((value is null) != true) {
                    if ((value is DateTime) == true) {
                        return (this == (value as DateTime?));
                    }
                    else {
                        if ((value is SYSTEMTIME) == true) {
                            return (this == (value as SYSTEMTIME?));
                        }
                    }
                }
                return (false);
            }
            public override int GetHashCode()
            {
                // uncomment the GetHashCode function to resolve
                return (0);
            }
        }
    }

    //////////////////////////////
    // RECT 構造体              //
    //////////////////////////////
    public static partial class CWin32Api
    {
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct RECT
        {
            public int      left;
            public int      top;
            public int      right;
            public int      bottom;

            public int Width    => ((right > left) ? right - left : left - right);
            public int Height   => ((bottom > top) ? bottom - top : top - bottom);
        }
    }

    //////////////////////////////
    // Win32 API のインポート   //
    //////////////////////////////
    public static partial class CWin32Api
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("user32.dll")]
        public static extern bool   EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);
        [DllImport("user32.dll")]
        public static extern UInt32 RemoveMenu(IntPtr hMenu, UInt32 nPosition, UInt32 wFlags);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr PostMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern bool   SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

        [DllImport("user32.dll")]
        public static extern bool   DestroyWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern void   PostQuitMessage(int nExitCode);

        [DllImport("user32.dll", EntryPoint = "GetWindowRect")]
        private static extern bool  DllGetWindowRect(IntPtr hWnd, out RECT lpRect);

        public static Rectangle GetWindowRect(IntPtr hWnd)
        {
            Rectangle   result = new Rectangle();
            bool        rc;

            rc = DllGetWindowRect(hWnd, out RECT rect);

            if (rc == true) {
                result.X      = rect.left;
                result.Y      = rect.top;
                result.Width  = rect.Width;
                result.Height = rect.Height;
            }

            return (result);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int    MessageBox(IntPtr hWnd, string lpText, string lpCaption, uint uType);

        [DllImport("gdi32.dll")]
        public static extern bool   BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, uint dwRop);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int    GetDeviceCaps(IntPtr hDC, int nIndex);

        [DllImport("user32.dll")]
        public static extern IntPtr SetFocus(IntPtr hWnd);

        // 外部プロセスのメイン・ウィンドウを起動するための Win32 API
        [DllImport("user32.dll")]
        public static extern bool   SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        public static extern bool   ShowWindowAsync(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        public static extern bool   IsIconic(IntPtr hWnd);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetCurrentThreadId();

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern IntPtr ReleaseDC(IntPtr hwnd, IntPtr hdc);

        [DllImport("user32.dll")]
        public static extern IntPtr GetLastActivePopup(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern int    GetSystemMetrics(int nIndex);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        public static extern IntPtr SetWindowsHookEx(int idHook, HOOKPROC lpfn, IntPtr hInstance, IntPtr threadId);

        public delegate IntPtr HOOKPROC(int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern bool   UnhookWindowsHookEx(IntPtr hHook);
        [DllImport("user32.dll")]
        public static extern IntPtr CallNextHookEx(IntPtr hHook, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint   GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool GetComputerName(StringBuilder lpBuffer, ref int nSize);

        // ウィンドウのクラス名を取得する
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int   GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        public static string        GetClassName(IntPtr hWnd)
        {
            string  result  = string.Empty;
            int     rc;

            StringBuilder csb = new StringBuilder(1024);

            rc = GetClassName(hWnd, csb, csb.Capacity);

            if (rc > 0) {
                result = csb.ToString();
            }

            return (result);
        }

        // トップレベルウィンドウを列挙する
        [DllImport("user32.dll")]
        public static extern bool   EnumWindows(EnumWindowsDelegate lpEnumFunc, IntPtr lParam);

        // EnumWindowsから呼び出されるコールバック関数のデリゲート
        public delegate bool EnumWindowsDelegate(IntPtr hWnd, IntPtr lParam);
    }

    /// <summary>
    /// UAC の盾アイコンをボタンコントロールに表示(あるいは、非表示)する
    /// </summary>
    /// <param name="targetButton">盾アイコンを表示するボタンコントロール</param>
    /// <param name="showShield">盾アイコンを表示する時は"true"。
    /// 非表示にする時は"false1"。</param>
    public static partial class CWin32Api
    {
        private const int   BCM_SETSHIELD       = BCM_FIRST + 0x000C;

        // Macro to use on a button or command link to display an elevated icon
        public static bool Button_SetElevationRequiredState(Button targetButton, bool showShield)
        {
            bool    result;
            IntPtr  rc;
            int     fRequired;

            result = false;

            // ｢Windows Vista｣以上か確認する
            if ((Environment.OSVersion.Platform      == PlatformID.Win32NT) &&
                (Environment.OSVersion.Version.Major >= 6                 )) {
                // FlatStyleをSystemにする
                targetButton.FlatStyle = FlatStyle.System;

                // 盾アイコンを表示／非表示にする
                if (showShield == true) {
                    fRequired = 1;
                }
                else {
                    fRequired = 0;
                }
                rc = SendMessage(targetButton.Handle, BCM_SETSHIELD, IntPtr.Zero, new IntPtr(fRequired));
                if (rc == IntPtr.Zero) {    // ※"1"が成功とあるが、実際には"0"が返ってきている!!
                    result = true;
// MODIFY =============================================================>
// 見直しによる変更。
//----------------------------------------------------------------------
// MODIFY
                    // ボタン文字を調整(スペースを1文字追加)する
                    targetButton.Text = targetButton.Text.Insert(0, " ");
//<=====================================================================
                }
            }

            return (result);
        }
    }

    //
    // Macros that are no longer used in this header but which clients may
    // depend on being defined here.
    //
    public static partial class CWin32Api
    {
        public static int MAKELONG(ushort low, ushort high)
        {
            return (((int)((high << 16) & 0xFFFF0000)) | ((int)((low << 0) & 0x0000FFFF)));
        }

        public static ushort HIWORD(int n)
        {
            return ((ushort)((n >> 16) & 0xFFFF));
        }

        public static ushort LOWORD(int n)
        {
            return ((ushort)((n >>  0) & 0xFFFF));
        }

        public static ushort MAKEWORD(byte low, byte high)
        {
            uint    l;
            uint    h;

            l = ((uint)low  << 0) & 0x00FF;
            h = ((uint)high << 8) & 0xFF00;

            return ((ushort)(l | h));
        }

        public static byte HIBYTE(ushort n)
        {
            return ((byte)((n >> 8) & 0x00FF));
        }

        public static byte LOBYTE(ushort n)
        {
            return ((byte)((n >> 0) & 0x00FF));
        }
    }
}
