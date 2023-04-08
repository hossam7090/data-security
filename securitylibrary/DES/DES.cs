using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.DES
{
    /// <summary>
    /// If the string starts with 0x.... then it's Hexadecimal not string
    /// </summary>
    public class DES : CryptographicTechnique
    {

        // **** Functions For Keys ****

        string convertToBinary(string hexstring)
        {

            return String.Join(String.Empty,
              hexstring.Substring(2).Select(
                c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')
              )
            );
        }

        string permutedChioce1(string k)
        {
            char[] p1 = new char[56];
            int[] perm1 = {57,49,41,33,25,17,9
                  ,1,58,50,42,34,26,18,10
                  ,2,59,51,43,35,27,19
                  ,11,3,60,52,44,36,63
                  ,55,47,39,31,23,15,7
                  ,62,56,46,38,30,22,14
                  ,6,61,53,45,37,29,21
                  ,13,5,28,20,12,4};

            for (int i = 0; i < perm1.Length; i++)
            {
                p1[i] = k[perm1[i] - 1];
            }
            return new string(p1);
        }

        string leftCircularshift(string k1, int bitsToShift)
        {

            string c = k1.Substring(0, 28);
            string d = k1.Substring(28);
            string tempC = c;
            string tempD = d;
            for (int j = 0; j < bitsToShift; j++)
            {
                tempC = tempC.Substring(1, tempC.Length - 1) + tempC[0];
                tempD = tempD.Substring(1, tempD.Length - 1) + tempD[0];

            }
            return (tempC + tempD);
        }

        string permutedChioce2(string k)
        {

            char[] p2 = new char[48];
            int[] perm2 = {14,17,11,24,1,5,
                   3,28,15,6,21,10,
                   23,19,12,4,26,8,
                   16,7,27,20,13,2,
                   41,52,31,37,47,55,
                   30,40,51,45,33,48,
                   44,49,39,56,34,53,
                   46,42,50,36,29,32};
            for (int i = 0; i < perm2.Length; i++)
            {
                p2[i] = k[perm2[i] - 1];
            }
            return new string(p2);

        }

        // **** Functions for des ****

        string initialPermutation(string m)
        {
            char[] p1 = new char[64];
            int[] perm =
            {
        58, 50, 42, 34, 26, 18, 10, 2,
        60, 52, 44, 36, 28, 20, 12, 4,
        62, 54, 46, 38, 30, 22, 14, 6,
        64, 56, 48, 40, 32, 24, 16, 8,
        57, 49, 41, 33, 25, 17, 9, 1,
        59, 51, 43, 35, 27, 19, 11, 3,
        61, 53, 45, 37, 29, 21, 13, 5,
        63, 55, 47, 39, 31, 23, 15, 7,
    };
            for (int i = 0; i < perm.Length; i++)
            {
                p1[i] = m[perm[i] - 1];
            }
            return new string(p1);
        }

        string expansionPermutation(string ip)
        {
            char[] p1 = new char[48];
            int[] perm =
            {
        32, 1, 2, 3, 4, 5,
        4, 5, 6, 7, 8, 9,
        8, 9, 10, 11, 12, 13,
        12, 13, 14, 15, 16, 17,
        16, 17, 18, 19, 20, 21,
        20, 21, 22, 23, 24, 25,
        24, 25, 26, 27, 28, 29,
        28, 29, 30, 31, 32, 1,
    };
            for (int i = 0; i < perm.Length; i++)
            {
                p1[i] = ip[perm[i] - 1];
            }
            return new string(p1);
        }

        string xoring(string a, string b, int n)
        {
            string ans = "";


            for (int i = 0; i < n; i++)
            {
                if (a[i] == b[i])
                    ans += "0";
                else
                    ans += "1";
            }
            return ans;
        }

        string sbox(string xor)
        {
            string new_s = "";
            int[,] s1 =
            {
        { 14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9, 0, 7 },
        { 0, 15, 7, 4, 14, 2, 13, 1, 10, 6, 12, 11, 9, 5, 3, 8 },
        { 4, 1, 14, 8, 13, 6, 2, 11, 15, 12, 9, 7, 3, 10, 5, 0, },
        { 15, 12, 8, 2, 4, 9, 1, 7, 5, 11, 3, 14, 10, 0, 6, 13, }
    };
            int[,] s2 =
            {
        { 15, 1, 8, 14, 6, 11, 3, 4, 9, 7, 2, 13, 12, 0, 5, 10, },
        { 3, 13, 4, 7, 15, 2, 8, 14, 12, 0, 1, 10, 6, 9, 11, 5 },
        { 0, 14, 7, 11, 10, 4, 13, 1, 5, 8, 12, 6, 9, 3, 2, 15 },
        { 13, 8, 10, 1, 3, 15, 4, 2, 11, 6, 7, 12, 0, 5, 14, 9 }
    };
            int[,] s3 =
            {
         { 10, 0, 9, 14, 6, 3, 15, 5, 1, 13, 12, 7, 11, 4, 2, 8 },
         { 13, 7, 0, 9, 3, 4, 6, 10, 2, 8, 5, 14, 12, 11, 15, 1 },
         { 13, 6, 4, 9, 8, 15, 3, 0, 11, 1, 2, 12, 5, 10, 14, 7 },
         { 1, 10, 13, 0, 6, 9, 8, 7, 4, 15, 14, 3, 11, 5, 2, 12 }
    };
            int[,] s4 =
            {
        { 7, 13, 14, 3, 0, 6, 9, 10, 1, 2, 8, 5, 11, 12, 4, 15 },
        { 13, 8, 11, 5, 6, 15, 0, 3, 4, 7, 2, 12, 1, 10, 14, 9 },
        { 10, 6, 9, 0, 12, 11, 7, 13, 15, 1, 3, 14, 5, 2, 8, 4 },
        { 3, 15, 0, 6, 10, 1, 13, 8, 9, 4, 5, 11, 12, 7, 2, 14 }
    };
            int[,] s5 =
            {
        { 2, 12, 4, 1, 7, 10, 11, 6, 8, 5, 3, 15, 13, 0, 14, 9 },
        { 14, 11, 2, 12, 4, 7, 13, 1, 5, 0, 15, 10, 3, 9, 8, 6 },
        { 4, 2, 1, 11, 10, 13, 7, 8, 15, 9, 12, 5, 6, 3, 0, 14 },
        { 11, 8, 12, 7, 1, 14, 2, 13, 6, 15, 0, 9, 10, 4, 5, 3 },
    };
            int[,] s6 =
            {
        { 12, 1, 10, 15, 9, 2, 6, 8, 0, 13, 3, 4, 14, 7, 5, 11 },
        { 10, 15, 4, 2, 7, 12, 9, 5, 6, 1, 13, 14, 0, 11, 3, 8 },
        { 9, 14, 15, 5, 2, 8, 12, 3, 7, 0, 4, 10, 1, 13, 11, 6 },
        { 4, 3, 2, 12, 9, 5, 15, 10, 11, 14, 1, 7, 6, 0, 8, 13 }
    };
            int[,] s7 =
            {
        { 4, 11, 2, 14, 15, 0, 8, 13, 3, 12, 9, 7, 5, 10, 6, 1 },
        { 13, 0, 11, 7, 4, 9, 1, 10, 14, 3, 5, 12, 2, 15, 8, 6 },
        { 1, 4, 11, 13, 12, 3, 7, 14, 10, 15, 6, 8, 0, 5, 9, 2 },
        { 6, 11, 13, 8, 1, 4, 10, 7, 9, 5, 0, 15, 14, 2, 3, 12 }
    };
            int[,] s8 =
            {
        { 13, 2, 8, 4, 6, 15, 11, 1, 10, 9, 3, 14, 5, 0, 12, 7 },
        { 1, 15, 13, 8, 10, 3, 7, 4, 12, 5, 6, 11, 0, 14, 9, 2 },
        { 7, 11, 4, 1, 9, 12, 14, 2, 0, 6, 10, 13, 15, 3, 5, 8 },
        { 2, 1, 14, 7, 4, 10, 8, 13, 15, 12, 9, 0, 3, 5, 6, 11 },
    };
            string chunk1 = xor.Substring(0, 6);
            string chunk2 = xor.Substring(6, 6);
            string chunk3 = xor.Substring(12, 6);
            string chunk4 = xor.Substring(18, 6);
            string chunk5 = xor.Substring(24, 6);
            string chunk6 = xor.Substring(30, 6);
            string chunk7 = xor.Substring(36, 6);
            string chunk8 = xor.Substring(42, 6);


            string r1 = "";
            string c1 = "";
            int raw1;
            int col1;

            string r2 = "";
            string c2 = "";
            int raw2;
            int col2;

            string r3 = "";
            string c3 = "";
            int raw3;
            int col3;

            string r4 = "";
            string c4 = "";
            int raw4;
            int col4;

            string r5 = "";
            string c5 = "";
            int raw5;
            int col5;

            string r6 = "";
            string c6 = "";
            int raw6;
            int col6;

            string r7 = "";
            string c7 = "";
            int raw7;
            int col7;

            string r8 = "";
            string c8 = "";
            int raw8;
            int col8;

            c1 = chunk1.Substring(1, 4);
            r1 += chunk1[0];
            r1 += chunk1[5];
            raw1 = Convert.ToInt32(r1, 2);
            col1 = Convert.ToInt32(c1, 2);
            string str1 = Convert.ToString(s1[raw1, col1], 2);
            new_s += new string('0', 4 - str1.Length) + str1;








            c2 = chunk2.Substring(1, 4);
            r2 += chunk2[0];
            r2 += chunk2[5];
            raw2 = Convert.ToInt32(r2, 2);
            col2 = Convert.ToInt32(c2, 2);
            string str2 = Convert.ToString(s2[raw2, col2], 2);
            new_s += new string('0', 4 - str2.Length) + str2;




            c3 = chunk3.Substring(1, 4);
            r3 += chunk3[0];
            r3 += chunk3[5];
            raw3 = Convert.ToInt32(r3, 2);
            col3 = Convert.ToInt32(c3, 2);
            string str3 = Convert.ToString(s3[raw3, col3], 2);
            new_s += new string('0', 4 - str3.Length) + str3;

            c4 = chunk4.Substring(1, 4);
            r4 += chunk4[0];
            r4 += chunk4[5];
            raw4 = Convert.ToInt32(r4, 2);
            col4 = Convert.ToInt32(c4, 2);
            string str4 = Convert.ToString(s4[raw4, col4], 2);
            new_s += new string('0', 4 - str4.Length) + str4;


            c5 = chunk5.Substring(1, 4);
            r5 += chunk5[0];
            r5 += chunk5[5];
            raw5 = Convert.ToInt32(r5, 2);
            col5 = Convert.ToInt32(c5, 2);
            string str5 = Convert.ToString(s5[raw5, col5], 2);
            new_s += new string('0', 4 - str5.Length) + str5;


            c6 = chunk6.Substring(1, 4);
            r6 += chunk6[0];
            r6 += chunk6[5];
            raw6 = Convert.ToInt32(r6, 2);
            col6 = Convert.ToInt32(c6, 2);
            string str6 = Convert.ToString(s6[raw6, col6], 2);
            new_s += new string('0', 4 - str6.Length) + str6;


            c7 = chunk7.Substring(1, 4);
            r7 += chunk7[0];
            r7 += chunk7[5];
            raw7 = Convert.ToInt32(r7, 2);
            col7 = Convert.ToInt32(c7, 2);
            string str7 = Convert.ToString(s7[raw7, col7], 2);
            new_s += new string('0', 4 - str7.Length) + str7;


            c8 = chunk8.Substring(1, 4);
            r8 += chunk8[0];
            r8 += chunk8[5];
            raw8 = Convert.ToInt32(r8, 2);
            col8 = Convert.ToInt32(c8, 2);
            string str8 = Convert.ToString(s8[raw8, col8], 2);
            new_s += new string('0', 4 - str8.Length) + str8;



            return new_s;

        }

        string permutation(string str)
        {
            char[] p1 = new char[32];
            int[] perm =
            {
        16, 7, 20, 21,
        29, 12, 28, 17,
        1, 15, 23, 26,
        5, 18, 31, 10,
        2, 8, 24, 14,
        32, 27, 3, 9,
        19, 13, 30, 6,
        22, 11, 4, 25
    };
            for (int i = 0; i < perm.Length; i++)
            {
                p1[i] = str[perm[i] - 1];
            }
            return new string(p1);
        }

        string finalPermutation(string str)
        {
            char[] p1 = new char[64];
            int[] perm =
            {
        40, 8, 48, 16, 56, 24, 64, 32,
        39, 7, 47, 15, 55, 23, 63, 31,
        38, 6, 46, 14, 54, 22, 62, 30,
        37, 5, 45, 13, 53, 21, 61, 29,
        36, 4, 44, 12, 52, 20, 60, 28,
        35, 3, 43, 11, 51, 19, 59, 27,
        34, 2, 42, 10, 50, 18, 58, 26,
        33, 1, 41, 9, 49, 17, 57, 25,
    };
            for (int i = 0; i < perm.Length; i++)
            {
                p1[i] = str[perm[i] - 1];
            }
            return new string(p1);

        }

        string BinaryStringToHexString(string binary)
        {
            if (string.IsNullOrEmpty(binary))
                return binary;

            StringBuilder result = new StringBuilder(binary.Length / 8 + 1);



            int mod4Len = binary.Length % 8;
            if (mod4Len != 0)
            {

                binary = binary.PadLeft(((binary.Length / 8) + 1) * 8, '0');
            }

            for (int i = 0; i < binary.Length; i += 8)
            {
                string eightBits = binary.Substring(i, 8);
                result.AppendFormat("{0:X2}", Convert.ToByte(eightBits, 2));
            }

            return result.ToString();
        }

        public override string Decrypt(string cipherText, string key)
        {
            //  **** Key Preparation ****

            string hex_key = key;

            string k1 = permutedChioce1(convertToBinary(hex_key));

            string[] keys = new string[16];
            keys[0] = leftCircularshift(k1, 1);
            int[] sh = { 1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1 };

            for (int i = 1; i < keys.Length; i++)
            {

                keys[i] = leftCircularshift(keys[i - 1], sh[i]);

            }

            string[] new_keys = new string[16];

            for (int p = 0; p < new_keys.Length; p++)
            {
                new_keys[p] = permutedChioce2(keys[p]);
            }

            //**** des algorithm ****

            string hex_text = cipherText;
            string m = (convertToBinary(hex_text));
            string ip = initialPermutation(m);

            string left = ip.Substring(0, 32);
            string right = ip.Substring(32);


            for (int j = 15; j >= 0; j--)
            {
                string Er = expansionPermutation(right);
                string xor1 = xoring(new_keys[j], Er, 48);
                string sbox1 = sbox(xor1);
                string perm1 = permutation(sbox1);
                string temp_r = right;
                right = xoring(perm1, left, 32);
                left = temp_r;
            }

            string final_string = right + left;
            string fin_bin = finalPermutation(final_string);
            string plain_text = BinaryStringToHexString(fin_bin);
            return ("0x" + plain_text);




        }

        public override string Encrypt(string plainText, string key)
        {
            //  **** Key Preparation ****

            string hex_key = key;

            string k1 = permutedChioce1(convertToBinary(hex_key));

            string[] keys = new string[16];
            keys[0] = leftCircularshift(k1, 1);
            int[] sh = { 1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1 };

            for (int i = 1; i < keys.Length; i++)
            {

                keys[i] = leftCircularshift(keys[i - 1], sh[i]);

            }

            string[] new_keys = new string[16];

            for (int p = 0; p < new_keys.Length; p++)
            {
                new_keys[p] = permutedChioce2(keys[p]);
            }

            //**** des algorithm ****

            string hex_text = plainText;
            string m = (convertToBinary(hex_text));
            string ip = initialPermutation(m);

            string left = ip.Substring(0, 32);
            string right = ip.Substring(32);


            for (int j = 0; j < 16; j++)
            {
                string Er = expansionPermutation(right);
                string xor1 = xoring(new_keys[j], Er, 48);
                string sbox1 = sbox(xor1);
                string perm1 = permutation(sbox1);
                string temp_r = right;
                right = xoring(perm1, left, 32);
                left = temp_r;
            }

            string final_string = right + left;
            string fin_bin = finalPermutation(final_string);
            string cipher_text = BinaryStringToHexString(fin_bin);
            return ("0x" + cipher_text);
        }


    }
}