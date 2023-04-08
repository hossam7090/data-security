using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class PlayFair : ICryptographic_Technique<string, string>
    {
        public string Decrypt(string cipherText, string key)
        {
            //throw new NotImplementedException();
            
            cipherText = cipherText.ToUpper();
            key = key.ToUpper();

            HashSet<char> set = new HashSet<char>();

            string Alpha = "ABCDEFGHIKLMNOPQRSTUVWXYZ";



            foreach (char c in key)
            {
                if (c == 'J')
                {
                    char c2 = 'I';
                    set.Add(c2);
                }
                else
                    set.Add(c);
            }


            char[,] matrix = new char[5, 5];

            string newMatrix = "";

            foreach (char c in set)
            {
                newMatrix = newMatrix + c;
            }

            foreach (char c in Alpha)
            {
                if (!newMatrix.Contains(c))
                {
                    newMatrix = newMatrix + c;
                }
            }


            int index1 = 0;
            int index2 = 0;

            Dictionary<char, string> map = new Dictionary<char, string>();

            for (int i = 0; i < newMatrix.Length; i++)
            {
                index2 = i % 5;
                if (index2 == 0 && i > 0)
                {
                    index1++;
                }
                matrix[index1, index2] = newMatrix[i];

                map.Add(newMatrix[i], (index1.ToString() + index2.ToString()));
            }


            string plainText = "";

            for (int i = 1; i < cipherText.Length; i += 2)
            {
                char c1 = cipherText[i - 1];
                char c2 = cipherText[i];
                if (c1 == 'J')
                {
                    c1 = 'I';
                }
                if (c2 == 'J')
                {
                    c2 = 'I';
                }

                string char1 = map[c1];
                string char2 = map[c2];

                if (char1[0] == char2[0])
                {
                    c1 = matrix[(char1[0] - 48), ((char1[1] - 48) + 4) % 5];
                    plainText = plainText + c1;
                    c2 = matrix[(char2[0] - 48), ((char2[1] - 48) + 4) % 5];
                    plainText = plainText + c2;
                }
                else if (char1[1] == char2[1])
                {
                    c1 = matrix[((char1[0] - 48) + 4) % 5, (char1[1] - 48)];
                    plainText = plainText + c1;
                    c2 = matrix[((char2[0] - 48) + 4) % 5, (char2[1] - 48)];
                    plainText = plainText + c2;
                }
                else
                {
                    c1 = matrix[(char1[0] - 48), (char2[1] - 48)];
                    plainText = plainText + c1;
                    c2 = matrix[(char2[0] - 48), (char1[1] - 48)];
                    plainText = plainText + c2;
                }
            }

            /*if (plainText.Length % 2 == 0 && plainText[plainText.Length - 1] == 'X')
            {
                plainText = plainText.Remove(plainText.Length - 1);
            }*/

           
            string finalString = "";

            finalString = finalString + plainText[0];

            for (int i = 1; i < plainText.Length - 1; i++)
            {
                if (plainText[i] == 'X')
                {
                    if (plainText[i - 1] == plainText[i + 1] && i % 2 != 0)
                        continue;
                    else
                        finalString = finalString + plainText[i];
                }
                else
                    finalString = finalString + plainText[i];
            }

            if (plainText[plainText.Length - 1] != 'X')
                finalString = finalString + plainText[plainText.Length - 1];

            return finalString;
        }

        public string Encrypt(string plainText, string key)
        {
            //throw new NotImplementedException();
            
            HashSet<char> set = new HashSet<char>();

            string Alpha = "ABCDEFGHIKLMNOPQRSTUVWXYZ";

            plainText = plainText.ToUpper();
            key = key.ToUpper();

            foreach (char c in key)
            {
                if (c == 'J')
                {
                    char c2 = 'I';
                    set.Add(c2);
                }
                else
                    set.Add(c);
            }


            char[,] matrix = new char[5, 5];

            string newMatrix = "";

            foreach (char c in set)
            {
                newMatrix = newMatrix + c;
            }

            foreach (char c in Alpha)
            {
                if (!newMatrix.Contains(c))
                {
                    newMatrix = newMatrix + c;
                }
            }


            int index1 = 0;
            int index2 = 0;

            Dictionary<char, string> map = new Dictionary<char, string>();
            
            for (int i = 0; i < newMatrix.Length; i++)
            {
                index2 = i % 5;
                if (index2 == 0 && i > 0)
                {
                    index1++;
                }
                matrix[index1, index2] = newMatrix[i];

                map.Add(newMatrix[i], (index1.ToString() + index2.ToString()));
            }

            for (int i = 0; i < plainText.Length - 1; i += 2)
            {
                if (plainText[i] == plainText[i + 1])
                {
                    plainText = plainText.Insert(i + 1, "X");
                }
            }

            if (plainText.Length % 2 != 0)
            {
                plainText = plainText + 'X';
            }


            string cipherText = "";

            for (int i = 1; i < plainText.Length; i += 2)
            {
                char c1 = plainText[i - 1];
                char c2 = plainText[i];
                if (c1 == 'J')
                {
                    c1 = 'I';
                }
                if (c2 == 'J')
                {
                    c2 = 'I';
                }

                string char1 = map[c1];
                string char2 = map[c2];

                if (char1[0] == char2[0])
                {
                    c1 = matrix[(char1[0] - 48), ((char1[1] - 48) + 1) % 5];
                    cipherText = cipherText + c1;
                    c2 = matrix[(char2[0] - 48), ((char2[1] - 48) + 1) % 5];
                    cipherText = cipherText + c2;
                }
                else if (char1[1] == char2[1])
                {
                    c1 = matrix[((char1[0] - 48) + 1) % 5, (char1[1] - 48)];
                    cipherText = cipherText + c1;
                    c2 = matrix[((char2[0] - 48) + 1) % 5, (char2[1] - 48)];
                    cipherText = cipherText + c2;
                }
                else
                {
                    c1 = matrix[(char1[0] - 48), (char2[1] - 48)];
                    cipherText = cipherText + c1;
                    c2 = matrix[(char2[0] - 48), (char1[1] - 48)];
                    cipherText = cipherText + c2;
                }
            }




            return cipherText;
        }
    }
}