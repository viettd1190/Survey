using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Security;

namespace Survey.Common
{
    public static class StringHelpers
    {
        public static string ToUnsignString(this string input)
        {
            input = input.Trim();
            for (int i = 0x20; i < 0x30; i++)
            {
                input = input.Replace(((char) i).ToString(),
                                      " ");
            }

            input = input.Replace(".",
                                  "-");
            input = input.Replace(" ",
                                  "-");
            input = input.Replace(",",
                                  "-");
            input = input.Replace(";",
                                  "-");
            input = input.Replace(":",
                                  "-");
            input = input.Replace("  ",
                                  "-");
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string str = input.Normalize(NormalizationForm.FormD);
            string str2 = regex.Replace(str,
                                        string.Empty)
                               .Replace('đ',
                                        'd')
                               .Replace('Đ',
                                        'D');
            while (str2.IndexOf("?") >= 0)
            {
                str2 = str2.Remove(str2.IndexOf("?"),
                                   1);
            }

            while (str2.Contains("--"))
            {
                str2 = str2.Replace("--",
                                    "-")
                           .ToLower();
            }

            return str2;
        }

        public static string ToAlias(this string source)
        {
            if(string.IsNullOrEmpty(source))
            {
                return string.Empty;
            }

            source = source.ToUnsignString();
            string[] temp = source.Trim()
                                  .Split(' ');
            return string.Join("-",
                               temp.Select(c => c.ToLower()));
        }

        public static string CreateSalt()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[32];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        public static string CreatePasswordHash(string password,
                                                string passwordSalt)
        {
            string saltAndPwd = string.Concat(password,
                                              passwordSalt);
            return FormsAuthentication.HashPasswordForStoringInConfigFile(saltAndPwd,
                                                                          "sha1");
        }

        public static string ConvertStringToBase64(this string source)
        {
            byte[] byt = Encoding.UTF8.GetBytes(source);

            return Convert.ToBase64String(byt);
        }

        public static string ConvertBase64ToString(this string source)
        {
            byte[] b = Convert.FromBase64String(source);

            return Encoding.UTF8.GetString(b);
        }
    }
}
