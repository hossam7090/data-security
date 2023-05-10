using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.RC4
{
    /// <summary>
    /// If the string starts with 0x.... then it's Hexadecimal not string
    /// </summary>
    public class RC4 : CryptographicTechnique
    {
        int xorr(int a, int b)
        {
            return a ^ b;
        }
        String hexToASCII(String hex)
        {

            String ascii = "";
            for (int i = 0; i < hex.Length; i += 2)
            {
                String part = hex.Substring(i, 2);
                char ch = (char)Convert.ToInt32(part, 16);
                ascii = ascii + ch;
            }
            return ascii;
        }
        string decToHexa(int n)
        {
            char[] hexaDeciNum = new char[100];
            int i = 0;

            while (n != 0)
            {
                int temp = 0;
                temp = n % 16;
                if (temp < 10)
                {
                    hexaDeciNum[i] =
                        (char)(temp + 48);
                    i++;
                }
                else
                {
                    hexaDeciNum[i] =
                        (char)(temp + 87);
                    i++;
                }

                n = n / 16;
            }
            string ans = "";
            for (int j = i - 1;
                    j >= 0; j--)
            {
                ans += hexaDeciNum[j];
            }

            return ans;
        }
        string ASCIItoHEX(string ascii)
        {
            string hex = "";
            for (int i = 0;
                     i < ascii.Length; i++)
            {
                char ch = ascii[i];
                int tmp = (int)ch;
                string part = decToHexa(tmp);
                hex += part;
            }
            return hex;
        }
        public override string Decrypt(string cipherText, string key)
        {
            string plain = cipherText;
            bool hex = false;

            if (plain.Substring(0, 2) == "0x")
            {
                hex = true;
                plain = plain.Remove(0, 2);
                plain = hexToASCII(plain);
                key = key.Remove(0, 2);
                key = hexToASCII(key);
            }


            byte[] S = new byte[256];
            for (int k = 0; k < S.Length; k++)
            {
                S[k] = (byte)k;
            }
            byte[] asciiKey = Encoding.GetEncoding("iso-8859-1").GetBytes(key);
            byte[] asciiPlain = Encoding.GetEncoding("iso-8859-1").GetBytes(plain);

            byte[] T = new byte[256];

            if (asciiKey.Length < 256)
            {
                int n = 0;
                int c = asciiKey.Length;
                while (n < 256)
                {
                    int x = (n % c);
                    T[n] = asciiKey[x];
                    n++;
                }

            }
            else
            {
                T = asciiKey;
            }
            int j = 0;
            int i = 0;
            for (i = 0; i < 256; i++)
            {
                j = (j + S[i] + T[i]) % 256;
                int temp = S[i];
                S[i] = S[j];
                S[j] = (byte)temp;
            }
            i = 0;
            j = 0;
            int it = 0;
            string result = "";
            while (it < asciiPlain.Length)
            {
                i = (i + 1) % 256;
                j = (j + S[i]) % 256;
                int temp = S[i];
                S[i] = S[j];
                S[j] = (byte)temp;
                int t = (S[i] + S[j]) % 256;
                int k = S[t];
                result += (char)xorr(k, asciiPlain[it]);
                it++;
            }
            if (hex == true)
                return ("0x" + ASCIItoHEX(result));
            else
                return ((result));

        }

        public override  string Encrypt(string plainText, string key)
        {

            
            string plain = plainText;
            bool hex = false;

            if (plain.Substring(0, 2) == "0x")
            {
                hex = true;
                plain = plain.Remove(0, 2);
                plain = hexToASCII(plain);
                key = key.Remove(0, 2);
                key = hexToASCII(key);
            }


            byte[] S = new byte[256];
            for (int k = 0; k < S.Length; k++)
            {
                S[k] = (byte)k;
            }
            byte[] asciiKey = Encoding.GetEncoding("iso-8859-1").GetBytes(key);
            byte[] asciiPlain = Encoding.GetEncoding("iso-8859-1").GetBytes(plain);

            byte[] T = new byte[256];

            if (asciiKey.Length < 256)
            {
                int n = 0;
                int c = asciiKey.Length;
                while (n < 256)
                {
                    int x = (n % c);
                    T[n] = asciiKey[x];
                    n++;
                }

            }
            else
            {
                T = asciiKey;
            }
            int j = 0;
            int i = 0;
            for (i = 0; i < 256; i++)
            {
                j = (j + S[i] + T[i]) % 256;
                int temp = S[i];
                S[i] = S[j];
                S[j] = (byte)temp;
            }
            i = 0;
            j = 0;
            int it = 0;
            string result = "";
            while (it < asciiPlain.Length)
            {
                i = (i + 1) % 256;
                j = (j + S[i]) % 256;
                int temp = S[i];
                S[i] = S[j];
                S[j] = (byte)temp;
                int t = (S[i] + S[j]) % 256;
                int k = S[t];
                result += (char)xorr(k, asciiPlain[it]);
                it++;
            }
            if (hex == true)
                return("0x" + ASCIItoHEX(result));
            else
                return((result));



        }
    }
}
