using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nicosu
{
    namespace Encode
    {
        public static class CEncode
        {
            /***********************************************************************
                文字列エンコード定義
            ***********************************************************************/
            private const string    EncodeType  = "Shift_JIS";      // Shift-JIS

            /***********************************************************************
                バイト配列から文字列へ変換する
            ***********************************************************************/
            public static string GetString(byte[] val, int max = -1)
            {
                string  str;

                // 文字列へ変換する
                try
                {
// MODIFY =============================================================>
// 見直しによる変更。
//----------------------------------------------------------------------
//                  str = Encoding.GetEncoding(EncodeType).GetString(val);
// MODIFY
                    byte[]  buf;
                    int     len;

                    if (max >= 0) {
                        if (max < val.Count()) {
                            ;
                        }
                        else {
                            max = val.Count();
                        }
                    }
                    else {
                        max = val.Count();
                    }
                    buf = new byte[max];
                    len = 0;

                    for (int ii = 0; ii < buf.Count(); ii++) {
                        if (ii < val.Count()) {
                            if (val[ii] >= ((byte)(' '))) {
                                buf[len] = val[ii];
                                len++;
                            }
                            else {
                                break;
                            }
                        }
                        else {
                            buf[len] = ((byte)(' '));
                            len++;
                        }
                    }

                    if (len > 0) {
                        Array.Resize(ref buf, len);
                        str = Encoding.GetEncoding(EncodeType).GetString(buf);
                    }
                    else {
                        str = string.Empty;
                    }
//<=====================================================================
// ※不要!! ===========================================================>
//   ->             str = str.Trim();
//<=====================================================================
                }
                catch
                {
                    str = string.Empty;
                }

                return (str);
            }

            /***********************************************************************
                文字列からバイト配列へ変換する
            ***********************************************************************/
            public static byte[] GetBytes(object str)
            {
                byte[]  val;

                // 文字列へ変換する
                try
                {
                    val = Encoding.GetEncoding(EncodeType).GetBytes(str.ToString());
                }
                catch
                {
                    val = null;
                }

                return (val);
            }

            /***********************************************************************
                文字列のバイト数を取得する
            ***********************************************************************/
            public static int GetByteCount(object str)
            {
                int bytes;

                // 文字列へ変換する
                try
                {
                    bytes = Encoding.GetEncoding(EncodeType).GetByteCount(str.ToString());
                }
                catch
                {
                    bytes = 0;
                }

                return (bytes);
            }

            /***********************************************************************
                文字配列から文字列へ変換する
            ***********************************************************************/
            public static string GetString(char[] val)
            {
                string  str;

                // 文字列へ変換する
                try
                {
                    str = new string(val);
// ※不要!! ===========================================================>
//   ->             str = str.Trim();
//<=====================================================================
                }
                catch
                {
                    str = string.Empty;
                }

                return (str);
            }

            /***********************************************************************
                文字列から文字配列へ変換する
            ***********************************************************************/
            public static char[] GetChars(object str)
            {
                char[]  val;

                // 文字列へ変換する
                try
                {
                    val = str.ToString().ToCharArray();
                }
                catch
                {
                    val = null;
                }

                return (val);
            }

            /***********************************************************************
                １０進数値を文字列へ変換する
            ***********************************************************************/
            /// <summary>
            /// １０進数値を文字列へ変換する
            /// </summary>
            /// <param name="value">１０進数値</param>
            /// <param name="point">小数点位置（0：小数点なし）</param>
            /// <param name="length">変換後の文字列桁数（0：桁数指定なし）</param>
            /// <param name="zero">ゼロサプレス（true：ゼロ付数値）</param>
            /// <returns>
            /// <para>変換後の１０進数文字列</para>
            /// <para>※Trim()は呼出し元で行うこと！！</para>
            /// </returns>
            public static string DecString(object value, int point = 0, int length = 0, bool zero = false)
            {
                string  format  = string.Empty;
                int     offset;
                double  val;
                string  str;

                str = string.Empty;

                // 文字列へ変換する
                try
                {
                    // 整数部
                    format += '0';

                    // 小数点以下部
                    if (point > 0) {
                        format += '.';

                        for (int ii = 0; ii < point; ii++) {
                            format += '0';
                        }
                    }

                    // 一旦浮動小数点型に変換する!!
                    val = Convert.ToDouble(value);

                    if (point > 0) {
                        for (int ii = 0; ii < point; ii++) {
                            val /= 10;
                        }
                    }

                    // 文字列に変換する
                    str = val.ToString(format);

                    if (length > 0) {
                        if (zero  == true) {
                            offset = 0;
                            if (str.Length > 0) {
                                if ((str.ElementAt(0).Equals('+') == true) ||
                                    (str.ElementAt(0).Equals('-') == true)) {
                                    offset += 1;
                                }
                            }
                            while (str.Length < length) {
                                str = str.Insert(offset, "0");
                            }
                        }
                        else {
                            while (str.Length < length) {
                                str = str.Insert(0, " ");
                            }
                        }

                        if (str.Length > length) {
                            str = string.Empty;

                            while (str.Length < length) {
                                str = str.Insert(0, "*");
                            }
                        }
                    }
                }
                catch
                {
                    if (length > 0) {
                        str = string.Empty;

                        while (str.Length < length) {
                            str = str.Insert(0, "*");
                        }
                    }
                }

                return (str);
            }

            /***********************************************************************
                １６進数値を文字列へ変換する
            ***********************************************************************/
            /// <summary>
            /// １６進数値を文字列へ変換する
            /// </summary>
            /// <param name="value">１６進数値</param>
            /// <param name="length">変換後の文字列桁数（0：桁数指定なし）</param>
            /// <returns>
            /// <para>変換後の１６進数文字列</para>
            /// <para>※Trim()は呼出し元で行うこと！！</para>
            /// </returns>
            public static string HexString(object value, int length = 0)
            {
                string  format  = string.Empty;
                ulong   val;
                string  str;

                str = string.Empty;

                // 文字列へ変換する
                try
                {
                    // 整数部
                    if (length > 0) {
                        format += string.Format("X{0}", length);
                    }
                    else {
                        format += string.Format("X");
                    }

                    // 一旦６４ビット整数型に変換する!!
                    val = Convert.ToUInt64(value);

                    // 文字列に変換する
                    str = val.ToString(format);

                    if (length > 0) {
                        while (str.Length < length) {
                            str = str.Insert(0, "0");
                        }

                        if (str.Length > length) {
                            str = string.Empty;

                            while (str.Length < length) {
                                str = str.Insert(0, "*");
                            }
                        }
                    }
                }
                catch
                {
                    if (length > 0) {
                        str = string.Empty;

                        while (str.Length < length) {
                            str = str.Insert(0, "*");
                        }
                    }
                }

                return (str);
            }

            /***********************************************************************
                浮動小数点数値値を文字列へ変換する
            ***********************************************************************/
            /// <summary>
            /// 浮動小数点数値値を文字列へ変換する
            /// </summary>
            /// <param name="value">浮動小数点数値値</param>
            /// <param name="point">小数点位置（0：小数点なし）</param>
            /// <param name="length">変換後の文字列桁数（0：桁数指定なし）</param>
            /// <param name="zero">ゼロサプレス（true：ゼロ付数値）</param>
            /// <returns>
            /// <para>変換後の浮動小数点数値文字列</para>
            /// <para>※Trim()は呼出し元で行うこと！！</para>
            /// </returns>
            public static string FloatString(object value, int point = 0, int length = 0, bool zero = false)
            {
                string  format  = string.Empty;
                int     offset;
                double  val;
                string  str;

                str = string.Empty;

                // 文字列へ変換する
                try
                {
                    // 整数部
                    format += '0';

                    // 小数点以下部
                    if (point > 0) {
                        format += '.';

                        for (int ii = 0; ii < point; ii++) {
                            format += '0';
                        }
                    }

                    // 一旦浮動小数点型に変換する!!
                    val = Convert.ToDouble(value);

                    // 文字列に変換する
                    str = val.ToString(format);

                    if (length > 0) {
                        if (zero  == true) {
                            offset = 0;
                            if (str.Length > 0) {
                                if ((str.ElementAt(0).Equals('+') == true) ||
                                    (str.ElementAt(0).Equals('-') == true)) {
                                    offset += 1;
                                }
                            }
                            while (str.Length < length) {
                                str = str.Insert(offset, "0");
                            }
                        }
                        else {
                            while (str.Length < length) {
                                str = str.Insert(0, " ");
                            }
                        }

                        if (str.Length > length) {
                            str = string.Empty;

                            while (str.Length < length) {
                                str = str.Insert(0, "*");
                            }
                        }
                    }
                }
                catch
                {
                    if (length > 0) {
                        str = string.Empty;

                        while (str.Length < length) {
                            str = str.Insert(0, "*");
                        }
                    }
                }

                return (str);
            }
        }
    }
}
