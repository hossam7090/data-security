using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class RailFence : ICryptographicTechnique<string, int>
    {
        public int Analyse(string plainText, string cipherText)
        {
            cipherText = cipherText.ToUpper();
            int x = 2;
            for (int i = 2; i < plainText.Length; i++)
            {

                string y = Encrypt(plainText, i);
                if (string.Compare(y, cipherText) == 0)
                {
                    x = i;
                    break;

                }
            }
            return x;
        }

        public string Decrypt(string cipherText, int key)
        {
            // throw new NotImplementedException();
            float plain = cipherText.Length;
            double y = plain / key;
            int dep = (int)Math.Ceiling(y);

            char[,] board = new char[key, dep];

            String plainT = "";

            int m = 0;
            for (int i = 0; i < key; i++)
            {
                for (int j = 0; j < dep; j++)
                {
                    if (m < plain)
                    {
                        board[i, j] = cipherText[m];
                        m++;
                    }
                }


            }
            m = 0;
            for (int i = 0; i < dep; i++)
            {
                for (int g = 0; g < key; g++)
                {
                    if (m < plain)
                    {
                        plainT = plainT + board[g, i];
                    }
                }
            }

            plainT = plainT.ToLower();
            return plainT;
        }

        public string Encrypt(string plainText, int key)
        {
            string plaintext = plainText.ToLower();
            //throw new NotImplementedException();
            float plain = plainText.Length;
            double y = plain / key;
            int dep = (int)Math.Ceiling(y);

            char[,] board = new char[key, dep];

            String cipher = "";

            int m = 0;
            for (int i = 0; i < dep; i++)
            {
                for (int j = 0; j < key; j++)
                {
                    if (m < plain)
                    {
                        board[j, i] = plainText[m];
                        m++;
                    }
                }


            }
            m = 0;
            for (int i = 0; i < key; i++)
            {
                for (int g = 0; g < dep; g++)
                {
                    if (m < plain)
                    {
                        cipher = cipher + board[i, g];
                    }
                }
            }
            cipher = cipher.ToUpper();

            return cipher;
        }
    }
}
