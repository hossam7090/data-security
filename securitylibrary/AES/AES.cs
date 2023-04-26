using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.AES
{
    /// <summary>
    /// If the string starts with 0x.... then it's Hexadecimal not string
    /// </summary>
    public class AES : CryptographicTechnique
    {
        string BinToHex(string bin)
        {
            int x = bin.Length;
            string s;
            string hexadecimal = "";
            for (int i = 0; i < x; i += 4)
            {
                s = bin.Substring(0, 4);
                bin = bin.Remove(0, 4);

                switch (s)
                {
                    case "0000": hexadecimal += '0'; break;
                    case "0001": hexadecimal += '1'; break;
                    case "0010": hexadecimal += '2'; break;
                    case "0011": hexadecimal += '3'; break;
                    case "0100": hexadecimal += '4'; break;
                    case "0101": hexadecimal += '5'; break;
                    case "0110": hexadecimal += '6'; break;
                    case "0111": hexadecimal += '7'; break;
                    case "1000": hexadecimal += '8'; break;
                    case "1001": hexadecimal += '9'; break;
                    case "1010": hexadecimal += 'A'; break;
                    case "1011": hexadecimal += 'B'; break;
                    case "1100": hexadecimal += 'C'; break;
                    case "1101": hexadecimal += 'D'; break;
                    case "1110": hexadecimal += 'E'; break;
                    case "1111": hexadecimal += 'F'; break;
                    default:
                        return "Invalid number";
                }
            }


            return hexadecimal.ToString();
        }
        string convertToBinary(string hexstring)
        {

            return String.Join(String.Empty,
              hexstring.Substring(2).Select(
                c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')
              )
            );
        }
        string HexToBin(string hexdec)
        {

            string bin = "";
            for (int i = 0; i < hexdec.Length; i++)
            {

                switch (hexdec[i])
                {
                    case '0':
                        bin += ("0000");
                        break;
                    case '1':
                        bin += ("0001");
                        break;
                    case '2':
                        bin += ("0010");
                        break;
                    case '3':
                        bin += ("0011");
                        break;
                    case '4':
                        bin += ("0100");
                        break;
                    case '5':
                        bin += ("0101");
                        break;
                    case '6':
                        bin += ("0110");
                        break;
                    case '7':
                        bin += ("0111");
                        break;
                    case '8':
                        bin += ("1000");
                        break;
                    case '9':
                        bin += ("1001");
                        break;
                    case 'A':
                    case 'a':
                        bin += ("1010");
                        break;
                    case 'B':
                    case 'b':
                        bin += ("1011");
                        break;
                    case 'C':
                    case 'c':
                        bin += ("1100");
                        break;
                    case 'D':
                    case 'd':
                        bin += ("1101");
                        break;
                    case 'E':
                    case 'e':
                        bin += ("1110");
                        break;
                    case 'F':
                    case 'f':
                        bin += ("1111");
                        break;
                    default:
                        System.Console.Write("\nInvalid hexadecimal digit " +
                                                                  hexdec[i]);
                        break;
                }
            }
            return bin;
        }
        string s_box(string s)
        {
            string[,] Sbox = {
            { "63",  "7C",  "77", "7B", "F2",  "6b",  "6f",  "c5",  "30",  "01",  "67",  "2b",  "fe",  "d7",  "ab",  "76" },
            { "cA",  "82",  "C9", "7D", "FA",  "59",  "47",  "f0",  "ad",  "d4",  "a2",  "af",  "9c",  "a4",  "72",  "c0" },
            { "B7",  "FD",  "93", "26", "36",  "3f",  "f7",  "cc",  "34",  "a5",  "e5",  "f1",  "71",  "d8",  "31",  "15" },
            { "04",  "C7",  "23", "C3", "18",  "96",  "05",  "9a",  "07",  "12",  "80",  "e2",  "eb",  "27",  "b2",  "75" },
            { "09",  "83",  "2C", "1A", "1B",  "6e",  "5a",  "a0",  "52",  "3b",  "d6",  "b3",  "29",  "e3",  "2f",  "84" },
            { "53",  "D1",  "00", "ED", "20",  "fc",  "b1",  "5b",  "6a",  "cb",  "be",  "39",  "4a",  "4c",  "58",  "cf" },
            { "D0",  "EF",  "AA", "FB", "43",  "4d",  "33",  "85",  "45",  "f9",  "02",  "7f",  "50",  "3c",  "9f",  "a8" },
            { "51",  "A3",  "40", "8F", "92",  "9d",  "38",  "f5",  "bc",  "b6",  "da",  "21",  "10",  "ff",  "f3",  "d2" },
            { "cD",  "0C",  "13", "EC", "5F",  "97",  "44",  "17",  "c4",  "a7",  "7e",  "3d",  "64",  "5d",  "19",  "73" },
            { "60",  "81",  "4F", "DC", "22",  "2a",  "90",  "88",  "46",  "ee",  "b8",  "14",  "de",  "5e",  "0b",  "db" },
            { "E0",  "32",  "3A", "0A", "49",  "06",  "24",  "5c",  "c2",  "d3",  "ac",  "62",  "91",  "95",  "e4",  "79" },
            { "E7",  "C8",  "37", "6D", "8D",  "d5",  "4e",  "a9",  "6c",  "56",  "f4",  "ea",  "65",  "7a",  "ae",  "08" },
            { "BA",  "78",  "25", "2E", "1C",  "a6",  "b4",  "c6",  "e8",  "dd",  "74",  "1f",  "4b",  "bd",  "8b",  "8a" },
            { "70",  "3E",  "B5", "66", "48",  "03",  "f6",  "0e",  "61",  "35",  "57",  "b9",  "86",  "c1",  "1d",  "9e" },
            { "E1",  "F8",  "98", "11", "69",  "d9",  "8e",  "94",  "9b",  "1e",  "87",  "e9",  "ce",  "55",  "28",  "df" },
            { "8C",  "A1",  "89", "0D", "BF",  "e6",  "42",  "68",  "41",  "99",  "2d",  "0f",  "b0",  "54",  "bb",  "16" },
        };
            return Sbox[Convert.ToInt32(s.Substring(0, 4), 2), Convert.ToInt32(s.Substring(4), 2)];
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
        string[] g_fun(string[,] key, int x)
        {
            string[] result = new string[4];
            string[] w3 = new string[4];
            string[] w0 = new string[4];
            for (int i = 0; i < 4; i++)
            {
                w3[i] = key[i, 3];
            }
            string temp = w3[0];
            for (int i = 0; i < 3; i++)
            {
                w3[i] = w3[i + 1];
            }
            w3[3] = temp;

            for (int i = 0; i < 4; i++)
            {


                w3[i] = HexToBin(s_box(w3[i]));

            }

            for (int i = 0; i < 4; i++)
            {
                w0[i] = key[i, 0];
            }
            string[,] Rcon = {
            {"00000001","00000000","00000000","00000000"}, // 0
            {"00000010","00000000","00000000","00000000"}, // 1
            {"00000100","00000000","00000000","00000000"}, // 2
            {"00001000","00000000","00000000","00000000"}, // 3
            {"00010000","00000000","00000000","00000000"}, // 4
            {"00100000","00000000","00000000","00000000"}, // 5
            {"01000000","00000000","00000000","00000000"}, // 6
            {"10000000","00000000","00000000","00000000"}, // 7
            {"00011011","00000000","00000000","00000000"}, // 8
            {"00110110","00000000","00000000","00000000"}, // 9
    };
            for (int j = 0; j < 4; j++)
            {

                result[j] = xoring(xoring(Rcon[x, j], w3[j], 8), w0[j], 8);
            }
            return result;
        }
        string[,] key_gen(string[,] key, int x)
        {
            string[,] result = new string[4, 4];
            string[] w1 = g_fun(key, x);
            for (int j = 0; j < 4; j++)
            {
                result[j, 0] = w1[j];
            }
            for (int j = 1; j < 4; j++)
            {
                for (int k = 0; k < 4; k++)
                {
                    result[k, j] = xoring(result[k, j - 1], key[k, j], 8);
                }
            }

            return result;
        }
        string GF_m(string w, int m)
        {
            string c = w;
            if (m == 2)
            {
                char ch = w[0];
                w = w.Remove(0, 1);
                w += "0";
                if (ch == '1')
                    return xoring(w, "00011011", 8);
                else
                    return w;

            }
            else if (m == 3)
            {
                char ch = w[0];
                w = w.Remove(0, 1);
                w += "0";
                if (ch == '1')
                    return xoring(xoring(w, "00011011", 8), c, 8);
                else
                    return xoring(w, c, 8);
            }
            return w;
        }
        string[,] mix_columns(string[,] key)
        {
            int[,] C = {
            {2,3,1,1},
            {1,2,3,1},
            {1,1,2,3},
            {3,1,1,2}
        };
            string[,] result = new string[4, 4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    string s1 = GF_m(key[0, i], C[j, 0]);
                    string s2 = GF_m(key[1, i], C[j, 1]);
                    string s3 = GF_m(key[2, i], C[j, 2]);
                    string s4 = GF_m(key[3, i], C[j, 3]);

                    result[j, i] = xoring(xoring(s1, s2, 8), xoring(s4, s3, 8), 8);
                }
            }
            return result;
        }
        void shiftrow(string[,] state)
        {
            // Shift second row by 1 byte
            string temp = state[1, 0];
            state[1, 0] = state[1, 1];
            state[1, 1] = state[1, 2];
            state[1, 2] = state[1, 3];
            state[1, 3] = temp;

            // Shift third row by 2 bytes
            temp = state[2, 0];
            string temp2 = state[2, 1];
            state[2, 0] = state[2, 2];
            state[2, 1] = state[2, 3];
            state[2, 2] = temp;
            state[2, 3] = temp2;

            // Shift fourth row by 3 bytes
            temp = state[3, 0];
            temp2 = state[3, 1];
            string temp3 = state[3, 2];
            state[3, 0] = state[3, 3];
            state[3, 1] = temp;
            state[3, 2] = temp2;
            state[3, 3] = temp3;
        }
        string[,] addRoundKey(string[,] key, string[,] plain)
        {
            string[,] result = new string[4, 4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    result[i, j] = xoring(plain[i, j], key[i, j], 8);

                }
            }
            return result;
        }
        string[,] sub_byte(string[,] plain)
        {
            string[,] result = new string[4, 4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    result[i, j] = s_box(plain[i, j]);

                }
            }
            return result;
        }


        string Invs_box(string s)
        {
            string[,] inverse_sbox = new string[,] {
            {"52","09","6a","d5","30","36","a5","38","bf","40","a3","9e","81","f3","d7","fb"},
            {"7c","e3","39","82","9b","2f","ff","87","34","8e","43","44","c4","de","e9","cb"},
            {"54","7b","94","32","a6","c2","23","3d","ee","4c","95","0b","42","fa","c3","4e"},
            {"08","2e","a1","66","28","d9","24","b2","76","5b","a2","49","6d","8b","d1","25"},
            {"72","f8","f6","64","86","68","98","16","d4","a4","5c","cc","5d","65","b6","92"},
            {"6c","70","48","50","fd","ed","b9","da","5e","15","46","57","a7","8d","9d","84"},
            {"90","d8","ab","00","8c","bc","d3","0a","f7","e4","58","05","b8","b3","45","06"},
            {"d0","2c","1e","8f","ca","3f","0f","02","c1","af","bd","03","01","13","8a","6b"},
            {"3a","91","11","41","4f","67","dc","ea","97","f2","cf","ce","f0","b4","e6","73"},
            {"96","ac","74","22","e7","ad","35","85","e2","f9","37","e8","1c","75","df","6e"},
            {"47","f1","1a","71","1d","29","c5","89","6f","b7","62","0e","aa","18","be","1b"},
            {"fc","56","3e","4b","c6","d2","79","20","9a","db","c0","fe","78","cd","5a","f4"},
            {"1f","dd","a8","33","88","07","c7","31","b1","12","10","59","27","80","ec","5f"},
            {"60","51","7f","a9","19","b5","4a","0d","2d","e5","7a","9f","93","c9","9c","ef"},
            {"a0","e0","3b","4d","ae","2a","f5","b0","c8","eb","bb","3c","83","53","99","61"},
            {"17","2b","04","7e","ba","77","d6","26","e1","69","14","63","55","21","0c","7d"}};
            return inverse_sbox[Convert.ToInt32(s.Substring(0, 4), 2), Convert.ToInt32(s.Substring(4), 2)];
        }
        string[,] Invmix_columns(string[,] key)
        {
            int[,] C = {
            {14,11,13,9},
            {9,14,11,13},
            {13,9,14,11},
            {11,13,9,14}
        };
            string[,] result = new string[4, 4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    string s1 = GF_m(key[0, i], C[j, 0]);
                    string s2 = GF_m(key[1, i], C[j, 1]);
                    string s3 = GF_m(key[2, i], C[j, 2]);
                    string s4 = GF_m(key[3, i], C[j, 3]);

                    result[j, i] = xoring(xoring(s1, s2, 8), xoring(s4, s3, 8), 8);
                }
            }
            return result;
        }
        void invshiftrow(string[,] state)
        {
            // Shift second row by 1 byte
            string temp = state[1, 3];
            state[1, 3] = state[1, 2];
            state[1, 2] = state[1, 1];
            state[1, 1] = state[1, 0];
            state[1, 0] = temp;

            // Shift third row by 2 bytes
            temp = state[2, 3];
            string temp2 = state[2, 2];
            state[2, 2] = state[2, 0];
            state[2, 3] = state[2, 1];
            state[2, 1] = temp;
            state[2, 0] = temp2;

            // Shift fourth row by 3 bytes
            temp = state[3, 3];
            temp2 = state[3, 2];
            string temp3 = state[3, 1];
            state[3, 3] = state[3, 0];
            state[3, 2] = temp;
            state[3, 1] = temp2;
            state[3, 0] = temp3;
        }
        string[,] Invsub_byte(string[,] plain)
        {
            string[,] result = new string[4, 4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    result[i, j] = Invs_box(plain[i, j]);

                }
            }
            return result;
        }
        String Shift_1B(string bin)//shift left and xor with 1B
        {
            if (bin[0] == '0')
            {
                return bin.Remove(0, 1) + "0";
            }
            else
            {
                return xoring((bin.Remove(0, 1) + "0"), "00011011", 8);
            }
        }
        string _09(string bin)//bin×09=(((bin×2)×2)×2)+bin
        {
            string res = xoring(Shift_1B(Shift_1B(Shift_1B(bin))), bin, 8);
            return res;
        }
        string _0B(string bin)//bin×0B=((((bin×2)×2)+bin)×2)+bin
        {
            string res = xoring(Shift_1B(xoring(Shift_1B(Shift_1B(bin)), bin, 8)), bin, 8);
            return res;
        }
        string _0D(string bin)//bin×0D=((((bin×2)+bin)×2)×2)+bin
        {
            string res = xoring(Shift_1B(Shift_1B(xoring(Shift_1B(bin), bin, 8))), bin, 8);
            return res;

        }
        string _0E(string bin)//bin×0E=((((bin×2)+bin)×2)+bin)×2
        {
            string res = Shift_1B(xoring(Shift_1B(xoring(Shift_1B(bin), bin, 8)), bin, 8));
            return res;

        }
        string[,] InvMixColumns(string[,] matrix)
        {
            string[,] Inverse_MixColumnFactor = {
    { "0E" ,  "0B" ,  "0D" ,  "09" },
    { "09" ,  "0E" ,  "0B" ,  "0D" },
    { "0D" ,  "09" ,  "0E" ,  "0B" },
    { "0B" ,  "0D" ,  "09" ,  "0E" }
    };

            string[,] mixed = { { "", "", "", "" }, { "", "", "", "" }, { "", "", "", "" }, { "", "", "", "" } };
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int k = 0; k < 4; k++)
                    {



                        StringBuilder binary1 = new StringBuilder((matrix[k, j]));
                        string res = "";
                        if (Inverse_MixColumnFactor[i, k].Equals("09", StringComparison.OrdinalIgnoreCase))
                        {
                            res = _09(binary1.ToString());
                        }
                        else if (Inverse_MixColumnFactor[i, k].Equals("0B", StringComparison.OrdinalIgnoreCase))
                        {
                            res = _0B(binary1.ToString());
                        }
                        else if (Inverse_MixColumnFactor[i, k].Equals("0D", StringComparison.OrdinalIgnoreCase))
                        {
                            res = _0D(binary1.ToString());
                        }
                        else if (Inverse_MixColumnFactor[i, k].Equals("0E", StringComparison.OrdinalIgnoreCase))
                        {
                            res = _0E(binary1.ToString());
                        }
                        mixed[i, j] = xoring(mixed[i, j].PadLeft(8, '0'), res, 8);

                    }
                }
            }
            return mixed;
        }








        public override string Decrypt(string cipherText, string key)
        {
            
            string plain = cipherText.Remove(0, 2);
            key = key.Remove(0, 2);
            key = HexToBin(key);
            plain = HexToBin(plain);

            string[,] key_arr = new string[4, 4];
            string[,] plain_arr = new string[4, 4];

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    key_arr[j, i] = key.Substring(0, 8);
                    key = key.Remove(0, 8);
                    plain_arr[j, i] = plain.Substring(0, 8);
                    plain = plain.Remove(0, 8);

                }
            }
            List<string[,]> arr = new List<string[,]>();
            string[,] vvv = key_arr;
            for (int i = 0; i < 10; i++)
            {
                vvv = key_gen(vvv, i);
                arr.Add(vvv);
            }

            //                1
            plain_arr = addRoundKey(plain_arr, arr[9]);

            for (int i = 8; i >= 0; i--)
            {
                invshiftrow(plain_arr);
                plain_arr = Invsub_byte(plain_arr);
                for (int k = 0; k < 4; k++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        plain_arr[k, j] = HexToBin(plain_arr[k, j]);

                    }
                }

                plain_arr = addRoundKey(plain_arr, arr[i]);
                plain_arr = InvMixColumns(plain_arr);

            }
            invshiftrow(plain_arr);
            plain_arr = Invsub_byte(plain_arr);
            for (int k = 0; k < 4; k++)
            {
                for (int j = 0; j < 4; j++)
                {
                    plain_arr[k, j] = HexToBin(plain_arr[k, j]);

                }
            }

            plain_arr = addRoundKey(plain_arr, key_arr);
            string cipher = "";

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    cipher += (BinToHex(plain_arr[j, i]));

                }
            }
            cipher = "0x" + cipher;
            return cipher;
        }

        public override string Encrypt(string plainText, string key)
        {
            string plain = plainText.Remove(0,2);
            key = key.Remove(0, 2);
            key = HexToBin(key);
            plain = HexToBin(plain);

            string[,] key_arr = new string[4, 4];
            string[,] plain_arr = new string[4, 4];

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    key_arr[j, i] = key.Substring(0, 8);
                    key = key.Remove(0, 8);
                    plain_arr[j, i] = plain.Substring(0, 8);
                    plain = plain.Remove(0, 8);

                }
            }

            plain_arr = addRoundKey(plain_arr, key_arr);
            for (int i = 0; i < 9; i++)
            {
                plain_arr = sub_byte(plain_arr);
                for (int k = 0; k < 4; k++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        plain_arr[k, j] = HexToBin(plain_arr[k, j]);

                    }
                }
                shiftrow(plain_arr);
                plain_arr = mix_columns(plain_arr);
                key_arr = key_gen(key_arr, i);
                plain_arr = addRoundKey(plain_arr, key_arr);

            }
            plain_arr = sub_byte(plain_arr);
            for (int k = 0; k < 4; k++)
            {
                for (int j = 0; j < 4; j++)
                {
                    plain_arr[k, j] = HexToBin(plain_arr[k, j]);

                }
            }
            shiftrow(plain_arr);
            key_arr = key_gen(key_arr, 9);
            plain_arr = addRoundKey(plain_arr, key_arr);
            string cipher = "";
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    cipher+=(BinToHex(plain_arr[j, i]));

                }
            }
            cipher = "0x" + cipher;
            return cipher;

        }
    }
}