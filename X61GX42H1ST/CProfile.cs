using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;
using System.IO;

namespace Nicosu
{
    namespace Profile
    {
        /// <summary>
        /// イニシャルファイル管理クラス(読込み専用)
        /// </summary>
        class CProfile
        {
            [DllImport("kernel32.dll")]
            private static extern uint  GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, uint nSize, string lpFileName);

            [DllImport("kernel32.dll")]
            private static extern uint  GetPrivateProfileInt(string lpAppName, string lpKeyName, int nDefault, string lpFileName);

            [DllImport("kernel32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            private static extern bool  WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);

            // プライベート変数
            private readonly string     m_fname;

            /// <summary>
            /// コンストラクタ(パス名＋ファイル名指定)
            /// </summary>
            public CProfile(string path, string name)
            {
                m_fname = Path.Combine(path, name);
            }

            /// <summary>
            /// コンストラクタ(フルパス名指定)
            /// </summary>
            public CProfile(string fname)
            {
                m_fname = fname;
            }

            /// <summary>
            /// イニシャルファイルからブール値を取得する
            /// </summary>
            /// <param name="section">セクション名</param>
            /// <param name="key">キー名(項目名)</param>
            /// <param name="initial">キー名が見つからない時の規定のブール値</param>
            /// <returns>
            /// 指定したキーに関連付けられているブール値
            /// </returns>
            public bool GetBoolean(string section, string key, bool initial)
            {
                int ini;

                if (initial != false) {
                    ini = 1;
                }
                else {
                    ini = 0;
                }

                if (GetValue(section, key, ini) != 0) {
                    return (true);
                }
                return (false);
            }

            /// <summary>
            /// イニシャルファイルから整数値を取得する
            /// </summary>
            /// <param name="section">セクション名</param>
            /// <param name="key">キー名(項目名)</param>
            /// <param name="initial">キー名が見つからない時の規定の整数値</param>
            /// <returns>
            /// 指定したキーに関連付けられている整数値
            /// </returns>
            public int GetValue(string section, string key, int initial)
            {
                return ((int)GetPrivateProfileInt(section, key, initial, m_fname));
            }

            /// <summary>
            /// イニシャルファイルから単精度浮動小数点値を取得する
            /// </summary>
            /// <param name="section">セクション名</param>
            /// <param name="key">キー名(項目名)</param>
            /// <param name="initial">キー名が見つからない時の規定の単精度浮動小数点値</param>
            /// <returns>
            /// 指定したキーに関連付けられている単精度浮動小数点値
            /// </returns>
            public float GetFloat(string section, string key, float initial)
            {
                string  str;
                string  defStr;
                float   retVal;

                defStr = initial.ToString();

                str = GetString(section, key, defStr);

                if (str.Length > 0) {
                    try
                    {
                        retVal = Convert.ToSingle(str);
                    }
                    catch
                    {
                        retVal = initial;
                    }
                    return (retVal);
                }

                return (initial);
            }

            /// <summary>
            /// イニシャルファイルから倍精度浮動小数点値を取得する
            /// </summary>
            /// <param name="section">セクション名</param>
            /// <param name="key">キー名(項目名)</param>
            /// <param name="initial">キー名が見つからない時の規定の倍精度浮動小数点値</param>
            /// <returns>
            /// 指定したキーに関連付けられている倍精度浮動小数点値
            /// </returns>
            public double GetDouble(string section, string key, double initial)
            {
                string  str;
                string  defStr;
                double  retVal;

                defStr = initial.ToString();

                str = GetString(section, key, defStr);

                if (str.Length > 0) {
                    try
                    {
                        retVal = Convert.ToDouble(str);
                    }
                    catch
                    {
                        retVal = initial;
                    }
                    return (retVal);
                }

                return (initial);
            }

            /// <summary>
            /// Ini ファイルから文字列を取得します。
            /// </summary>
            /// <param name="section">セクション名</param>
            /// <param name="key">キー名(項目名)</param>
            /// <param name="initial">キー名が見つからない時の規定の文字列</param>
            /// <returns>
            /// 指定したキーに関連付けられている文字列
            /// </returns>
            public string GetString(string section, string key, string initial)
            {
                string  str;

                var sb = new StringBuilder(2048);
                var rc = GetPrivateProfileString(section, key, initial, sb, (uint)sb.Capacity, m_fname);

                if (rc > 0) {
                    try
                    {
                        str = sb.ToString();
                    }
                    catch
                    {
                        str = initial;
                    }
                }
                else {
// MODIFY 2021/12/13 O.M ==============================================>
// 見直しによる変更。
//----------------------------------------------------------------------
// MODIFY
                    if (rc == 0) {
                        str = string.Empty;
                    }
                    else {
                        str = initial;
                    }
//<=====================================================================
                }

                return (str);
            }

            /// <summary>
            /// Ini ファイルにブール値を書込みます。
            /// </summary>
            /// <param name="section">セクション名</param>
            /// <param name="key">キー名(項目名)</param>
            /// <param name="value">書込む値</param>
            /// <returns></returns>
            public bool WriteBoolean(string section, string key, bool value)
            {
                int val;

                if (value != false) {
                    val = 1;
                }
                else {
                    val = 0;
                }

                return (WriteValue(section, key, val));
            }

            /// <summary>
            /// Ini ファイルに整数値を書込みます。
            /// </summary>
            /// <param name="section">セクション名</param>
            /// <param name="key">キー名(項目名)</param>
            /// <param name="value">書込む値</param>
            /// <returns></returns>
            public bool WriteValue(string section, string key, int value)
            {
                return (WriteString(section, key, value.ToString()));
            }

            /// <summary>
            /// Ini ファイルに単精度浮動小数点値を書込みます。
            /// </summary>
            /// <param name="section">セクション名</param>
            /// <param name="key">キー名(項目名)</param>
            /// <param name="value">書込む値</param>
            /// <returns></returns>
            public bool WriteFloat(string section, string key, float value)
            {
                return (WriteString(section, key, value.ToString()));
            }

            /// <summary>
            /// Ini ファイルに倍精度浮動小数点値を書込みます。
            /// </summary>
            /// <param name="section">セクション名</param>
            /// <param name="key">キー名(項目名)</param>
            /// <param name="value">書込む値</param>
            /// <returns></returns>
            public bool WriteDouble(string section, string key, double value)
            {
                return (WriteString(section, key, value.ToString()));
            }

            /// <summary>
            /// Ini ファイルに文字列を書込みます。
            /// </summary>
            /// <param name="section">セクション名</param>
            /// <param name="key">キー名(項目名)</param>
            /// <param name="value">書込む値</param>
            /// <returns></returns>
            public bool WriteString(string section, string key, string value)
            {
                string  str = string.Format("\"{0}\"", value);
                return (WritePrivateProfileString(section, key, str, m_fname));
            }
        }
    }
}
