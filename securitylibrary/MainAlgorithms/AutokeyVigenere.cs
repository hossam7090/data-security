using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class AutokeyVigenere : ICryptographicTechnique<string, string>
    {
        char[,] charMatrix = new char[26, 26];

        public AutokeyVigenere()
        {
            char ch = 'A';
            for (int i = 0; i < 26; i++)
            {
                for (int j = 0; j < 26; j++)
                {
                    charMatrix[i, j] = ((ch + j) > 'Z') ? charMatrix[i, j] = (char)((ch + j) - 'Z' + 'A' - 1) : charMatrix[i, j] = (char)(ch + j);

                }
                ch++;
            }
        }

        public string Analyse(string plainText, string cipherText)
        {
            StringBuilder kStream = new StringBuilder();

            for (int i = 0; i < plainText.Length; i++)
            {
                char key = ' ';
                for (int j = 0; j < 26; j++)
                {
                    if (cipherText[i] == charMatrix[j, plainText[i] - 'a'])
                    {
                        key = ((char)('a' + j));
                    }
                }
                kStream.Append(key);
            }

            int max = -1;
            for (int i = 0; i < plainText.Length; i++)
            {
                int id = kStream.ToString().IndexOf(plainText.Substring(0, i));
                if (id > max)
                {
                    max = id;
                }
            }

            return kStream.ToString().Substring(0, max);
        }

        public string Decrypt(string cipherText, string key)
        {
            StringBuilder kStream = new StringBuilder();
            StringBuilder plainText = new StringBuilder();
            kStream.Append(key);
            int k = 0;
            while (kStream.Length < cipherText.Length)
            {

                char kstreamChar = '.';
                for (int j = 0; j < 26; j++)
                {
                    if (cipherText[k] == charMatrix[kStream[k] - 'a', j])
                    {
                        kstreamChar = (char)('a' + j);
                    }
                }
                kStream.Append(kstreamChar);
                k++;
            }


            for (int i = 0; i < cipherText.Length; i++)
            {
                char plainChar = '.';
                for (int j = 0; j < 26; j++)
                {
                    if (cipherText[i] == charMatrix[kStream[i] - 'a', j])
                    {
                        plainChar = (char)('a' + j);
                    }
                }
                plainText.Append(plainChar);
            }

            return plainText.ToString();
        }

        public string Encrypt(string plainText, string key)
        {
            StringBuilder kStream = new StringBuilder();
            StringBuilder cipherText = new StringBuilder();
            kStream.Append(key);
            while (kStream.Length < plainText.Length)
            {
                kStream.Append(plainText);
            }

            for (int i = 0; i < plainText.Length; i++)
            {
                cipherText.Append(charMatrix[kStream[i] - 'a', plainText[i] - 'a']);
            }

            return cipherText.ToString();
        }
    }
}
