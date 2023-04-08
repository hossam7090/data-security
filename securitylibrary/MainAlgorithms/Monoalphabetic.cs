using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Monoalphabetic : ICryptographicTechnique<string, string>
    {
        public string Analyse(string plainText, string cipherText)
        {
            //throw new NotImplementedException();
            cipherText = cipherText.ToUpper();
            plainText = plainText.ToUpper();
            
            string ABC = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string efg = "";
            string Key = "";
            

            var m = new Dictionary<char, char>();

            for (int i = 0; i < cipherText.Length; i++)
            {
                if (!m.ContainsKey(plainText[i]))
                {
                    m.Add(plainText[i], cipherText[i]);
                }

            }

            for (int i = 0; i < ABC.Count(); i++)
            {
                if (!cipherText.Contains(ABC[i]))
                {
                    efg = efg + ABC[i];

                }
                else
                {
                    continue;
                }

            }

            efg = efg.ToUpper();
            
            int j = 0;
            for (int i = 0; i < 26; i++)
            {
                if (plainText.Contains(ABC[i]))
                {
                    Key = Key + m[ABC[i]];
                }
                else
                {
                    Key = Key + efg[j];
                    j++;
                }
            }
            Key = Key.ToLower();
            return Key;
        }

        public string Decrypt(string cipherText, string key)
        {
            // throw new NotImplementedException();
            var m = new Dictionary<char, char>();

            cipherText = cipherText.ToUpper();
            key = key.ToUpper();

            char x = 'a';

            for (int i = 0; i < 26; i++)
            {
                m.Add(x, key[i]);
                x++;
            }
            string w = "";
            char k;
            for (int i = 0; i < cipherText.Count(); i++)
            {
                k = 'a';
                for (int j = 0; j < 26; j++)
                {
                    if (cipherText[i] == m[k])
                    {
                        w = w + k;
                        break;
                    }
                    k++;
                }
            }

            return w;
        }

        public string Encrypt(string plainText, string key)
        {
            //throw new NotImplementedException();

            var m = new Dictionary<char, char>();

            char x = 'a';
            for (int i = 0; i < 26; i++)
            {
                m.Add(x, key[i]);
                x++;
            }
            char c;
            string w = "";
            for (int i = 0; i < plainText.Count(); i++)
            {
                c = m[plainText[i]];
                w = w + c;

            }

            return w;

        }

        /// <summary>
        /// Frequency Information:
        /// E   12.51%
        /// T	9.25
        /// A	8.04
        /// O	7.60
        /// I	7.26
        /// N	7.09
        /// S	6.54
        /// R	6.12
        /// H	5.49
        /// L	4.14
        /// D	3.99
        /// C	3.06
        /// U	2.71
        /// M	2.53
        /// F	2.30
        /// P	2.00
        /// G	1.96
        /// W	1.92
        /// Y	1.73
        /// B	1.54
        /// V	0.99
        /// K	0.67
        /// X	0.19
        /// J	0.16
        /// Q	0.11
        /// Z	0.09
        /// </summary>
        /// <param name="cipher"></param>
        /// <returns>Plain text</returns>
        public string AnalyseUsingCharFrequency(string cipher)
        {
            //throw new NotImplementedException();


            string alphabet = "ETAOINSRHLDCUMFPGWYBVKXJQZ";
            alphabet = alphabet.ToLower();
            string ABC = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            

            Dictionary<char, int> dic = new Dictionary<char, int>();

            foreach (char c in cipher)
            {
                if (dic.ContainsKey(c))
                {
                    dic[c]++;
                }
                else
                {
                    dic.Add(c, 1);
                }
            }

            foreach (char c in ABC)
            {
                if (!dic.ContainsKey(c))
                {
                    dic.Add(c, 0);
                }
            }

            dic = dic.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

            string newABC = "";

            foreach (var item in dic)
            {
                newABC = newABC + item.Key;
            }


            string word = "";

            for (int i = 0; i < cipher.Length; i++)
            {
                int c = 0;
                for (int j = 0; j < newABC.Length; j++)
                {
                    if (cipher[i] == newABC[j])
                    {
                        c = j;
                        break;
                    }
                }
                word = word + alphabet[c];
            }

            return word;


        }

    }
}











/*string alphabet = "ETAOINSRHLDCUMFPGWYBVKXJQZ";
string ABC = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
cipher = cipher.ToUpper();

int[] frequancy = new int[26] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };


for (int i = 0; i < cipher.Length; i++)
{
    frequancy[cipher[i] - 65] = frequancy[cipher[i] - 65] + 1;

}

Dictionary<char, int> dic = new Dictionary<char, int>();

int index = 0;

foreach (char c in ABC)
{
    dic.Add(c, frequancy[index]);
    index++;
}
dic = dic.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

string newABC = "";

foreach (var item in dic)
{
    newABC = newABC + item.Key;
}

string word = "";

foreach (char c in cipher)
{
    int cv = 0;
    for (int i = 0; i < 26; i++)
    {
        if (c == newABC[i])
        {
            cv = i;
            break;
        }
    }
    word = word + alphabet[cv];
}
return word;*/