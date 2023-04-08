using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Columnar : ICryptographicTechnique<string, List<int>>
    {

        List<int> findPermutation(List<int> array, int size, int n , string cipherText , string plainText)
        {
            // if size becomes 1, it prints the obtained permutation  
            List<int> list = new List<int>();

            if (size == 1)
            {
                string test = Decrypt(cipherText, array);
                if (test.CompareTo(plainText) == 0)
                {
                    for (int b = 0; b < array.Count; b++)
                    {
                        list.Add(array[b]);
                    }
                    //return list;
                }
                //printPermutations(array, n);

            }
            for (int j = 0; j < size; j++)
            {
                findPermutation(array, size - 1, n, cipherText, plainText);
                
                //if the length of the array is odd, it swaps the 0th element with the last element   
                if (size % 2 == 1)
                {
                    //performing swapping     
                    int temp = array[0];
                    array[0] = array[size - 1];
                    array[size - 1] = temp;
                }
                
                //if the size of the array is even, it swaps the ith element with the last element  
                else
                {
                    //taking a temp variable for swapping     
                    int temp;
                    //performing swapping   
                    temp = array[j];
                    array[j] = array[size - 1];
                    array[size - 1] = temp;
                }
            }
            
            return list;
        }









        public List<int> Analyse(string plainText, string cipherText)
        {

            List<int> key;

            for (int i = 2; i < plainText.Length; i++)
            {
                key = new List<int>();

                double number_of_columns = i;

                double number_of_row = (plainText.Length) / number_of_columns;
                double number_of_rows = Math.Ceiling(number_of_row);

                char[,] matrixP = new char[(int)number_of_rows, (int)number_of_columns];
                char[,] matrixC = new char[(int)number_of_rows, (int)number_of_columns];

                int ip = 0;
                for (int d = 0; d < number_of_rows; d++)
                {
                    for (int q = 0; q < number_of_columns; q++)
                    {
                        if (ip < plainText.Length)
                        {
                            matrixP[d, q] = plainText[ip];
                            ip++;
                        }
                        else

                            matrixP[d, q] = 'x';

                    }
                }





                int ic = 0;
                for (int r = 0; r < number_of_columns; r++)
                {
                    for (int g = 0; g < number_of_rows; g++)
                    {
                        if (ic < cipherText.Length)
                        {
                            matrixC[g, r] = cipherText[ic];
                            ic++;
                        }
                        /*else
                            matrixC[g, r] = 'x';*/

                    }


                }






                int count2 = 0;
                for (int x = 0; x < number_of_columns; x++)
                {

                    for (int z = 0; z < number_of_columns; z++)
                    {
                        int count = 0;
                        if (matrixC[0, x] == matrixP[0, z])
                        {

                            for (int a = 0; a < number_of_rows; a++)
                            {

                                if (matrixC[a, x] == matrixP[a, z])
                                {
                                    count++;
                                }

                            }

                        }

                        if (count == number_of_rows)
                        {

                            count2++;
                            key.Add(z + 1);

                        }

                    }


                }

                if (count2 == number_of_columns)
                {
                    return key;

                }
            }
            List<int> manga = new List<int>();
            return manga;
        }

        public string Decrypt(string cipherText, List<int> key)
        {
            double number_of_columns = key.Count();
            double number_of_row = (cipherText.Length) / number_of_columns;
            double number_of_rows = Math.Ceiling(number_of_row);
            char[,] matrix = new char[(int)number_of_rows, (int)number_of_columns];
            string plain = "";
            int index = 0;
            for (int i = 1; i <= key.Count(); i++)
            {
                for (int g = 0; g < number_of_rows; g++)
                {
                    if (index < cipherText.Length)
                    {
                        matrix[g, key.IndexOf(i)] = cipherText[index];
                        index++;

                    }

                }
            }
            index = 0;
            for (int i = 0; i < number_of_rows; i++)
            {
                for (int g = 0; g < number_of_columns; g++)
                {
                    if (index < cipherText.Length)
                    {
                        plain = plain + matrix[i, g];

                    }

                }

            }




            return plain;
        }

        public string Encrypt(string plainText, List<int> key)
        {
            // throw new NotImplementedException();
            plainText = plainText.ToUpper();
            double number_of_columns = key.Count();
            double number_of_row = (plainText.Length) / number_of_columns;
            double number_of_rows = Math.Ceiling(number_of_row);

            char[,] matrix = new char[(int)number_of_rows, (int)number_of_columns];
            string cipher = "";
            int index = 0;


            for (int i = 0; i < number_of_rows; i++)
            {
                for (int j = 0; j < number_of_columns; j++)
                {
                    if (index < plainText.Length)
                    {
                        matrix[i, j] = plainText[index];
                        index++;
                    }
                    else
                    {
                        matrix[i, j] = 'x';
                    }
                }
            }

            for (int i = 1; i <= key.Count(); i++)
            {

                for (int j = 0; j < number_of_rows; j++)

                {
                    cipher += matrix[j, key.IndexOf(i)];
                }
            }
            return cipher.ToLower();
        }


    }
}
