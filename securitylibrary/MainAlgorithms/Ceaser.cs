using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SecurityLibrary
{
    public class Ceaser : ICryptographicTechnique<string, int>
    {

        public string Encrypt(string plainText, int key)
        {

            int[] cipher_Text = new int[plainText.Length];
            string text = "";

            for (int i = 0; i < plainText.Length; i++)
            {
                if (char.IsLower(plainText[i]))
                {

                    cipher_Text[i] = (((int)plainText[i] + key - 97) % 26 + 97);

                }

                else
                {
                    cipher_Text[i] = (((int)plainText[i] + key - 65) % 26 + 65);
                }
                text += (char)cipher_Text[i];
            }

            return text;
            //  throw new NotImplementedException();
        }

        public string Decrypt(string cipherText, int key)
        {

            int[] decrypt = new int[cipherText.Length];
            string decrypt_mesg = "";
            for (int i = 0; i < cipherText.Length; i++)
            {
                if (char.IsLower(cipherText[i]))
                {
                    if ((int)cipherText[i] - (key % 26) < 97)
                    {
                        decrypt[i] = ((int)cipherText[i] + 26) - (key % 26);
                    }
                    else
                    {
                        decrypt[i] = ((int)cipherText[i]) - (key % 26);
                    }
                }
                else
                {
                    if ((int)cipherText[i] - (key % 26) < 65)
                    {
                        decrypt[i] = ((int)cipherText[i] + 26) - (key % 26);
                    }
                    else
                    {
                        decrypt[i] = ((int)cipherText[i]) - (key % 26);
                    }
                }
                decrypt_mesg += (char)(decrypt[i]);
            }
            return decrypt_mesg;

            //  throw new NotImplementedException();
        }

        public int Analyse(string plainText, string cipherText)
        {
            cipherText = cipherText.ToLower();
            int KEY = ((int)plainText[0] - (int)cipherText[0]);
            if (KEY >= 0)
            {
                KEY = (26 - KEY) % 26;
                return KEY;
            }
            else
            {
                KEY = (-KEY % 26);
                return KEY;
            }
            //throw new NotImplementedException();
        }
    }
}