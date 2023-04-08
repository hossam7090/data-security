using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class RepeatingkeyVigenere : ICryptographicTechnique<string, string>
    {
        char start = 'A';
        char[,] charMatrix = new char[26, 26];

        public RepeatingkeyVigenere()
        {
            for (int i = 0; i < 26; i++)

            {
                for (int j = 0; j < 26; j++)
                {
                    charMatrix[i, j] = ((start + j) > 'Z') ? charMatrix[i, j] = (char)((start + j) - 'Z' + 'A' - 1) : charMatrix[i, j] = (char)(start + j);

                }
                start++;
            }


        }

        public string Analyse(string plainText, string cipherText)
        {
            StringBuilder keycontainer = new StringBuilder();
            char key = ' ';
            for (int i = 0; i < plainText.Length; i++)
            {
                for (int j = 0; j < 26; j++)
                {

                    if (cipherText[i] == charMatrix[j, plainText[i] - 'a'])
                    {
                        key = ((char)('a' + j));
                    }
                }
                keycontainer.Append(key);
            }

            int max = -1;
            for (int i = 0; i < keycontainer.Length; i++)
            {
                if (2 * i > keycontainer.Length)
                    break;

                string right = keycontainer.ToString().Substring(i, i);

                string left = keycontainer.ToString().Substring(0, i);


                if (left == right && i != 0)
                {
                    if (left.Length > max)
                        max = left.Length;
                }
            }

            return keycontainer.ToString().Substring(0, max);
        }

        public string Encrypt(string plainText, string key)
        {
            StringBuilder key_stream = new StringBuilder();
            StringBuilder cipherText = new StringBuilder();
            while (key_stream.Length < plainText.Length)
            {
                key_stream.Append(key);
            }

            for (int i = 0; i < plainText.Length; i++)
            {
                cipherText.Append(charMatrix[plainText[i] - 'a', key_stream[i] - 'a']);
            }

            return cipherText.ToString();
            //throw new NotImplementedException();
        }

        public string Decrypt(string cipherText, string key)
        {
            StringBuilder keyStream = new StringBuilder();
            StringBuilder plainText = new StringBuilder();
            while (keyStream.Length < cipherText.Length)
            {
                keyStream.Append(key);
            }

            for (int i = 0; i < cipherText.Length; i++)
            {
                char plainChar = ' ';
                for (int j = 0; j < 26; j++)
                {
                    if (cipherText[i] == this.charMatrix[keyStream[i] - 'a', j])
                    {
                        plainChar = (char)('a' + j);
                    }
                }
                plainText.Append(plainChar);
            }

            return plainText.ToString();
            //throw new NotImplementedException();
        }
    }


}

public interface ICryptographicTechnique<T1, T2>
{
}
