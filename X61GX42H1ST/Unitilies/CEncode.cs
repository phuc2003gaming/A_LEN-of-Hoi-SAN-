using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X61GX42H1ST;
using static X61GX42H1ST.A_MAIN;

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
            #region １０進数を１６進数へ変換処理
            //**********************************************************************
            // 
            // サブルーチン
            // 
            //**********************************************************************
            // １０進数を１６進数へ変換
            public static string WcovHex1(string WDAT)
            {
                if (Conversion.Val(WDAT) == 0)
                {
                    return ("0");
                }
                else
                {
                    return (Strings.Right("0" + Conversion.Hex(Conversion.Val(WDAT)), 1));
                }
            }

            // １０進数を１６進数へ変換
            public static string WcovHex2(string WDAT)
            {
                if (Conversion.Val(WDAT) == 0)
                {
                    return ("00");
                }
                else
                {
                    return (Strings.Right("00" + Conversion.Hex(Conversion.Val(WDAT)), 2));
                }

            }

            // １０進数を１６進数へ変換
            public static string WcovHex3(string WDAT)
            {
                if (Conversion.Val(WDAT) == 0)
                {
                    return ("000");
                }
                else
                {
                    return (Strings.Right("000" + Conversion.Hex(Conversion.Val(WDAT)), 3));
                }

            }

            // １０進数を１６進数へ変換
            public static string WcovHex4(string WDAT)
            {
                if (Conversion.Val(WDAT) == 0)
                {
                    return ("0000");
                }
                else
                {
                    return (Strings.Right("0000" + Conversion.Hex(Conversion.Val(WDAT)), 4));
                }
            }


            // １０進数を２進数へ変換し文字列で結合させ１０進数へ変換する
            public static string WcovBit2(string WDAT01, string WDAT02)
            {
                Int32 C_item01;
                String C_item02;
                Int32 R_item01;
                String R_item02;
                String X_item;
                Int16 i;
                Int16 c;

                c = 0;
                C_item02 = String.Empty;
                R_item02 = String.Empty;
                C_item01 = (Int32)Conversion.Val(WDAT01);

                do
                {
                    C_item02 = Convert.ToString(C_item01 % 2L) + C_item02;
                    C_item01 = (Int32)Math.Round(Conversion.Fix((double)C_item01 / 2d));
                }
                while (C_item01 > 0);

                R_item01 = (Int32)Conversion.Val(WDAT02);
                do
                {
                    R_item02 = Convert.ToString(R_item01 % 2L) + R_item02;
                    R_item01 = (Int32)Math.Round(Conversion.Fix((double)R_item01 / 2d));
                }
                while (R_item01 > 0);

                X_item = Strings.Right("00000000" + C_item02, 8) + Strings.Right("00000000" + R_item02, 8);
                for (i = 1; i <= 15; i++)
                {
                    if (Strings.Mid(X_item, i, 1) == "1")
                    {
                        c = (Int16)Math.Round(c + Math.Pow(2d, i - 1));
                    }
                }

                if (c == 0)
                {
                    return ("0000");
                }
                else
                {
                    return (Strings.Right("0000" + Conversion.Hex(c), 4));
                }
            }
            // 識別コードを並び替えて整列する
            public static void SortByCode(ref VEHICLE_COMP buf)
            {
                try
                {
                    int i;

                    // 有効なデータを取得する
                    var list1 = buf.SrcDat.Where(x => !(x.IsEmpty()));
                    var list2 = buf.DstDat.Where(x => !(x.IsEmpty()));

                    //キー作成
                    var keys = list1.Select(x => x.Head.IdenCode)
                                    .Union(list2.Select(x => x.Head.IdenCode))
                                    .OrderBy(x => x)
                                    .ToList();

                    var dict1 = list1.ToDictionary(x => x.Head.IdenCode, x => x);
                    var dict2 = list2.ToDictionary(x => x.Head.IdenCode, x => x);

                    // 新しいバッファ作成
                    Mx_sub.VEHICLE_INFO[] newSrc = new Mx_sub.VEHICLE_INFO[buf.SrcDat.Length];
                    Mx_sub.VEHICLE_INFO[] newDst = new Mx_sub.VEHICLE_INFO[buf.DstDat.Length];
                    // 初期化
                    for (i = 0; i < newSrc.Length; i++)
                    {
                        newSrc[i] = Mx_sub.VEHICLE_INFO.Empty;
                        newDst[i] = Mx_sub.VEHICLE_INFO.Empty;
                    }

                    // 並び結果セット
                    i = 0;
                    foreach (var key in keys)
                    {
                        if (i >= newSrc.Length) break;

                        newSrc[i] = dict1.ContainsKey(key) ? dict1[key] : Mx_sub.VEHICLE_INFO.Empty;
                        newDst[i] = dict2.ContainsKey(key) ? dict2[key] : Mx_sub.VEHICLE_INFO.Empty;

                        i++;
                    }

                    // データコーピ
                    Array.Copy(newSrc, buf.SrcDat, buf.SrcDat.Length);
                    Array.Copy(newDst, buf.DstDat, buf.DstDat.Length);
                }
                catch
                {

                }
            }
            #endregion
        }
    }
}
