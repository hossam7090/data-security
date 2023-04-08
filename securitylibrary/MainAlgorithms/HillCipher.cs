using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SecurityLibrary
{
   
    /// <summary>
    /// The List<int> is row based. Which means that the key is given in row based manner.
    /// </summary>
    public class HillCipher : ICryptographicTechnique<string, string>, ICryptographicTechnique<List<int>, List<int>>
    {

        public static int GCD(int num1, int num2)
        {
            int Remainder;

            while (num2 != 0)
            {
                Remainder = num1 % num2;
                num1 = num2;
                num2 = Remainder;
            }

            return num1;
        }




        public List<int> Analyse(List<int> plainText, List<int> cipherText)
        {
           // throw new NotImplementedException();
           
            
           if(plainText.Count == 4)
           {
                //----------------- fill Cipher Matrix ---------------//

                int num1 = (cipherText.Count() / 2) + (cipherText.Count() % 2);
                int[,] cipherMatrix = new int[num1, 2];
                int index1 = 0;
                for (int i = 0; i < num1; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        cipherMatrix[i, j] = cipherText[index1];
                        index1++;
                    }


                }

                //----------------- fill plain Matrix ---------------//

                int[,] plainMatrix = new int[2, 2];
                int index2 = 0;
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        plainMatrix[i, j] = plainText[index2];
                        if (plainMatrix[i, j] < 0 || plainMatrix[i, j] > 26)
                        {
                            throw new InvalidAnlysisException();
                        }
                        index2++;
                    }
                }


                //--------------- find matrix determinant -------------// 
                int det = 0;
                det += plainMatrix[0, 0] * plainMatrix[1, 1] - plainMatrix[0, 1] * plainMatrix[1, 0];
                det = det % 26;
                if (det < 0)
                {
                    det += 26;
                }
                if (det == 0)
                {
                    throw new InvalidAnlysisException();
                }
                int z = GCD(det, 26);
                if (z > 1)
                {
                    throw new InvalidAnlysisException();
                }

                //----------- find multiplicative inverse of matrix determinant --------//
                int b = 0;
                for (int i = 1; i <= 26; i++)
                {
                    if ((i * det) % 26 == 1)
                    {
                        b = i;
                        break;
                    }
                }

                if (b == 26)
                {
                    throw new InvalidAnlysisException();
                }


                //------------ find inverse of matrix in 2*2 ---------// 

                int x1 = plainMatrix[0, 0];
                int x2 = plainMatrix[0, 1];
                int x3 = plainMatrix[1, 0];
                int x4 = plainMatrix[1, 1];


                plainMatrix[0, 0] = x4;
                plainMatrix[1, 1] = x1;
                plainMatrix[0, 1] = -x2;
                plainMatrix[1, 0] = -x3;

                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        plainMatrix[i, j] = (b * plainMatrix[i, j]) % 26;
                        if (plainMatrix[i, j] < 0)
                        {
                            plainMatrix[i, j] += 26;
                        }
                    }
                }


                //------------- Hill Cipher - Analysis -------//
                List<int> final = new List<int>();

                for (int i = 0; i < num1; i++)
                {
                    int sum1 = 0;

                    sum1 = (plainMatrix[i, 0] * cipherMatrix[0, 0]) + (plainMatrix[i, 1] * cipherMatrix[0, 1]);
                    final.Add(sum1 % 26);


                }

                for (int i = 0; i < num1; i++)
                {

                    int sum2 = 0;
                    sum2 = (plainMatrix[i, 0] * cipherMatrix[1, 0]) + (plainMatrix[i, 1] * cipherMatrix[1, 1]);
                    final.Add(sum2 % 26);

                }


                return final;
            }
            else
            {
                int a = 0;
                int b = 0;
                int c = 0;
                int d = 0;


                int cIndex1 = cipherText[0];
                int cIndex2 = cipherText[1];
                int cIndex3 = cipherText[2];
                int cIndex4 = cipherText[3];

                int pIndex1 = plainText[0];
                int pIndex2 = plainText[1];
                int pIndex3 = plainText[2];
                int pIndex4 = plainText[3];

                while (true)
                {
                    if (cIndex1 % pIndex1 != 0)
                    {
                        cIndex1 += 26;
                    }
                    else
                    {
                        a = cIndex1 / pIndex1;
                        break;
                    }
                }

                while (true)
                {
                    if (cIndex2 % pIndex1 != 0)
                    {
                        cIndex2 += 26;
                        c++;
                    }
                    else
                    {
                        c = cIndex2 / pIndex1;
                        break;
                    }
                }

                cIndex3 = cIndex3 - ((a * pIndex3) % 26);

                if (cIndex3 < 0)
                {
                    cIndex3 += 26;
                }

                while (true)
                {
                    if (cIndex3 % pIndex4 != 0)
                    {
                        cIndex3 += 26;
                    }
                    else
                    {
                        b = cIndex3 / pIndex4;
                        break;
                    }
                }

                cIndex4 = cIndex4 - ((a * pIndex4) % 26);

                if (cIndex4 < 0)
                {
                    cIndex4 += 26;
                }


                while (true)
                {
                    if (cIndex4 % pIndex4 != 0)
                    {
                        cIndex4 += 26;
                    }
                    else
                    {
                        d = cIndex4 / pIndex4;
                        break;
                    }
                }

                List<int> final = new List<int>();
                final.Add(a);
                final.Add(b);
                final.Add(c);
                final.Add(d);

                return final;

            }



        }

        public string Analyse(string plainText, string cipherText)
        {
            throw new NotImplementedException();
        }



        public List<int> Decrypt(List<int> cipherText, List<int> key)
        {
            //throw new NotImplementedException();

            //---------- Check if key matrix are 2*2 or 3*3 --------//

            List<int> final = new List<int>();

            if (key.Count() == 9)
            {

                //----------------- fill Cipher Matrix ---------------//

                int num1 = (cipherText.Count() / 3) + (cipherText.Count() % 3);
                int[,] cipherMatrix = new int[num1, 3];
                int index1 = 0;
                for (int i = 0; i < num1; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        cipherMatrix[i, j] = cipherText[index1];
                        index1++;
                    }


                }

                //----------------- fill Key Matrix ---------------//

                int[,] keyMatrix = new int[3, 3];
                int index2 = 0;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        keyMatrix[i, j] = key[index2];
                        if (keyMatrix[i, j] < 0 || keyMatrix[i, j] > 26)
                        {
                            throw new InvalidAnlysisException();
                        }
                        index2++;
                    }
                }



                //--------------- find matrix determinant -------------// 
                int det = 0;
                det += keyMatrix[0, 0] * (keyMatrix[1, 1] * keyMatrix[2, 2] - keyMatrix[1, 2] * keyMatrix[2, 1]);
                det -= keyMatrix[1, 0] * (keyMatrix[0, 1] * keyMatrix[2, 2] - keyMatrix[0, 2] * keyMatrix[2, 1]);
                det += keyMatrix[2, 0] * (keyMatrix[0, 1] * keyMatrix[1, 2] - keyMatrix[0, 2] * keyMatrix[1, 1]);
                det = det % 26;
                
                if (det < 0)
                {
                    det += 26;
                }

                if (det == 0)
                {
                    throw new InvalidAnlysisException();
                }
                int z = GCD(det, 26);
                if (z > 1)
                {
                    throw new InvalidAnlysisException();
                }

                //----------- find multiplicative inverse of matrix determinant --------//
                int b = 0;
                for (int i = 1; i < 26; i++)
                {
                    if ((i * det) % 26 == 1)
                    {
                        b = i;
                        break;
                    }
                }

                if (b == 26)
                {
                    throw new InvalidAnlysisException();
                }

                //------------ find inverse of matrix in 3*3 ---------// 

                int[,] D = new int[3, 3];
                D[0, 0] = (keyMatrix[1, 1] * keyMatrix[2, 2] - keyMatrix[1, 2] * keyMatrix[2, 1]);
                D[0, 1] = (keyMatrix[1, 0] * keyMatrix[2, 2] - keyMatrix[1, 2] * keyMatrix[2, 0]);
                D[0, 2] = (keyMatrix[1, 0] * keyMatrix[2, 1] - keyMatrix[1, 1] * keyMatrix[2, 0]);
                D[1, 0] = (keyMatrix[0, 1] * keyMatrix[2, 2] - keyMatrix[0, 2] * keyMatrix[2, 1]);
                D[1, 1] = (keyMatrix[0, 0] * keyMatrix[2, 2] - keyMatrix[0, 2] * keyMatrix[2, 0]);
                D[1, 2] = (keyMatrix[0, 0] * keyMatrix[2, 1] - keyMatrix[0, 1] * keyMatrix[2, 0]);
                D[2, 0] = (keyMatrix[0, 1] * keyMatrix[1, 2] - keyMatrix[0, 2] * keyMatrix[1, 1]);
                D[2, 1] = (keyMatrix[0, 0] * keyMatrix[1, 2] - keyMatrix[0, 2] * keyMatrix[1, 0]);
                D[2, 2] = (keyMatrix[0, 0] * keyMatrix[1, 1] - keyMatrix[0, 1] * keyMatrix[1, 0]);


                int[,] k = new int[3, 3];
                for (int i = 0; i < 3; i++)
                {

                    for (int j = 0; j < 3; j++)
                    {
                        k[i, j] = (b * (((int)Math.Pow(-1, (i + j)) * D[i, j]))) % 26;
                        if (k[i, j] < 0)
                        {
                            k[i, j] += 26;
                        }
                    }
                }


                //----------------- transpose of inverse matrix 3*3 -----------//

                int[,] kt = new int[3, 3];
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        kt[j, i] = k[i, j];
                    }
                }

                //------------- Hill Cipher - Decryption -------//
                for (int i = 0; i < num1; i++)
                {
                    int sum1 = 0;
                    int sum2 = 0;
                    int sum3 = 0;

                    sum1 = (kt[0, 0] * cipherMatrix[i, 0]) + (kt[0, 1] * cipherMatrix[i, 1]) + (kt[0, 2] * cipherMatrix[i, 2]);
                    final.Add(sum1 % 26);

                    sum2 = (kt[1, 0] * cipherMatrix[i, 0]) + (kt[1, 1] * cipherMatrix[i, 1]) + (kt[1, 2] * cipherMatrix[i, 2]);
                    final.Add(sum2 % 26);

                    sum3 = (kt[2, 0] * cipherMatrix[i, 0]) + (kt[2, 1] * cipherMatrix[i, 1]) + (kt[2, 2] * cipherMatrix[i, 2]);
                    final.Add(sum3 % 26);
                }



               // return final;




            }
            else
            {
                //----------------- fill Cipher Matrix ---------------//

                int num1 = (cipherText.Count() / 2) + (cipherText.Count() % 2);
                int[,] cipherMatrix = new int[num1, 2];
                int index1 = 0;
                for (int i = 0; i < num1; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        cipherMatrix[i, j] = cipherText[index1];
                        index1++;
                    }


                }

                //----------------- fill Key Matrix ---------------//

                int[,] keyMatrix = new int[2, 2];
                int index2 = 0;
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        keyMatrix[i, j] = key[index2];
                        if (keyMatrix[i, j] < 0 || keyMatrix[i, j] > 26)
                        {
                            throw new InvalidAnlysisException();
                        }
                        index2++;
                    }
                }


                //--------------- find matrix determinant -------------// 
                int det = 0;
                det += keyMatrix[0, 0] * keyMatrix[1, 1] - keyMatrix[0, 1] * keyMatrix[1, 0];
                det = det % 26;
                if (det < 0)
                {
                    det += 26;
                }
                if (det == 0)
                {
                    throw new InvalidAnlysisException();
                }
                int z = GCD(det,26);
                if (z > 1)
                {
                    throw new InvalidAnlysisException();
                }

                //----------- find multiplicative inverse of matrix determinant --------//
                int b = 0;
                for (int i = 1; i <= 26; i++)
                {
                    if ((i * det) % 26 == 1)
                    {
                        b = i;
                        break;
                    }
                }

                if (b == 26)
                {
                    throw new InvalidAnlysisException();
                }


                //------------ find inverse of matrix in 2*2 ---------// 

                int x1 = keyMatrix[0, 0];
                int x2 = keyMatrix[0, 1];
                int x3 = keyMatrix[1, 0];
                int x4 = keyMatrix[1, 1];


                keyMatrix[0, 0] = x4;
                keyMatrix[1, 1] = x1;
                keyMatrix[0, 1] = -x2;
                keyMatrix[1, 0] = -x3;

                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        keyMatrix[i, j] = (b * keyMatrix[i, j]) % 26;
                        if (keyMatrix[i, j] < 0)
                        {
                            keyMatrix[i, j] += 26;
                        }
                    }
                }





                //------------- Hill Cipher - Decryption -------//

                for (int i = 0; i < num1; i++)
                {
                    int sum1 = 0;
                    int sum2 = 0;

                    sum1 = (keyMatrix[0, 0] * cipherMatrix[i, 0]) + (keyMatrix[0, 1] * cipherMatrix[i, 1]);
                    final.Add(sum1 % 26);

                    sum2 = (keyMatrix[1, 0] * cipherMatrix[i, 0]) + (keyMatrix[1, 1] * cipherMatrix[i, 1]);
                    final.Add(sum2 % 26);

                }

            }



                return final;

        }


        public string Decrypt(string cipherText, string key)
        {
            throw new NotImplementedException();
        }



        public List<int> Encrypt(List<int> plainText, List<int> key)
        {
            //throw new NotImplementedException();
            int number = 0;
            if (key.Count() == 9) 
            {
                number = 3;
            }
            else
            {
                number = 2;
            }

            int num1 = (plainText.Count() / number) + (plainText.Count() % number);
            int[,] plainMatrix = new int[num1, number];
            int index1 = 0;
            for (int i = 0; i < num1; i++)
            {
                for (int j = 0; j < number; j++)
                {
                    plainMatrix[i, j] = plainText[index1];
                    index1++;
                }


            }

            int[,] keyMatrix = new int[number, number];
            int index2 = 0;
            for (int i = 0; i < number; i++)
            {
                for (int j = 0; j < number; j++)
                {
                    keyMatrix[j, i] = key[index2];
                    index2++;
                }
            }

            List<int> final = new List<int>();
            if(number == 3) 
            {
                for (int i = 0; i < num1; i++)
                {
                    int sum1 = 0;
                    int sum2 = 0;
                    int sum3 = 0;
                    sum1 = (keyMatrix[0, 0] * plainMatrix[i, 0]) + (keyMatrix[1, 0] * plainMatrix[i, 1]) + (keyMatrix[2, 0] * plainMatrix[i, 2]);
                    final.Add(sum1 % 26);

                    sum2 = (keyMatrix[0, 1] * plainMatrix[i, 0]) + (keyMatrix[1, 1] * plainMatrix[i, 1]) + (keyMatrix[2, 1] * plainMatrix[i, 2]);
                    final.Add(sum2 % 26);

                    sum3 = (keyMatrix[0, 2] * plainMatrix[i, 0]) + (keyMatrix[1, 2] * plainMatrix[i, 1]) + (keyMatrix[2, 2] * plainMatrix[i, 2]);
                    final.Add(sum3 % 26);

                }
            }
            else
            {
                for (int i = 0; i < num1; i++)
                {
                    int sum1 = 0;
                    int sum2 = 0;
                    
                    sum1 = (keyMatrix[0, 0] * plainMatrix[i, 0]) + (keyMatrix[1, 0] * plainMatrix[i, 1]);
                    final.Add(sum1 % 26);

                    sum2 = (keyMatrix[0, 1] * plainMatrix[i, 0]) + (keyMatrix[1, 1] * plainMatrix[i, 1]);
                    final.Add(sum2 % 26);

                }
            }

            


            return final;
        }

        public string Encrypt(string plainText, string key)
        {
            throw new NotImplementedException();
        }



        public List<int> Analyse3By3Key(List<int> plain3, List<int> cipher3)
        {
            //throw new NotImplementedException();

            //----------------- fill Cipher Matrix ---------------//
            int num1 = (cipher3.Count() / 3) + (cipher3.Count() % 3);
            int[,] cipherMatrix = new int[num1, 3];
            int index1 = 0;
            for (int i = 0; i < num1; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    cipherMatrix[i, j] = cipher3[index1];
                    index1++;
                }
            }

            //----------------- fill plain Matrix ---------------//
            int num2 = (plain3.Count() / 3) + (plain3.Count() % 3);
            int[,] plainMatrix = new int[num2, 3];
            int index2 = 0;
            for (int i = 0; i < num2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    plainMatrix[i, j] = plain3[index2];
                    index2++;
                }
            }


            //--------------- find matrix determinant -------------// 
            int det = 0;
            det += plainMatrix[0, 0] * (plainMatrix[1, 1] * plainMatrix[2, 2] - plainMatrix[1, 2] * plainMatrix[2, 1]);
            det -= plainMatrix[1, 0] * (plainMatrix[0, 1] * plainMatrix[2, 2] - plainMatrix[0, 2] * plainMatrix[2, 1]);
            det += plainMatrix[2, 0] * (plainMatrix[0, 1] * plainMatrix[1, 2] - plainMatrix[0, 2] * plainMatrix[1, 1]);
            det = det % 26;

            if (det < 0)
            {
                det += 26;
            }


            //----------- find multiplicative inverse of matrix determinant --------//
            int b = 0;
            for (int i = 1; i < 26; i++)
            {
                if ((i * det) % 26 == 1)
                {
                    b = i;
                    break;
                }
            }


            //------------ find inverse of plain matrix ---------// 

            int[,] D = new int[3, 3];
            D[0, 0] = (plainMatrix[1, 1] * plainMatrix[2, 2] - plainMatrix[1, 2] * plainMatrix[2, 1]);
            D[0, 1] = (plainMatrix[1, 0] * plainMatrix[2, 2] - plainMatrix[1, 2] * plainMatrix[2, 0]);
            D[0, 2] = (plainMatrix[1, 0] * plainMatrix[2, 1] - plainMatrix[1, 1] * plainMatrix[2, 0]);
            D[1, 0] = (plainMatrix[0, 1] * plainMatrix[2, 2] - plainMatrix[0, 2] * plainMatrix[2, 1]);
            D[1, 1] = (plainMatrix[0, 0] * plainMatrix[2, 2] - plainMatrix[0, 2] * plainMatrix[2, 0]);
            D[1, 2] = (plainMatrix[0, 0] * plainMatrix[2, 1] - plainMatrix[0, 1] * plainMatrix[2, 0]);
            D[2, 0] = (plainMatrix[0, 1] * plainMatrix[1, 2] - plainMatrix[0, 2] * plainMatrix[1, 1]);
            D[2, 1] = (plainMatrix[0, 0] * plainMatrix[1, 2] - plainMatrix[0, 2] * plainMatrix[1, 0]);
            D[2, 2] = (plainMatrix[0, 0] * plainMatrix[1, 1] - plainMatrix[0, 1] * plainMatrix[1, 0]);

            int[,] k = new int[3, 3];
            for (int i = 0; i < 3; i++)
            {

                for (int j = 0; j < 3; j++)
                {
                    k[i, j] = (b * (((int)Math.Pow(-1, (i + j)) * D[i, j]))) % 26;
                    if (k[i, j] < 0)
                    {
                        k[i, j] += 26;
                    }
                }
            }


            //----------------- transpose of inverse matrix 3*3 -----------//
            int[,] kt = new int[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    kt[j, i] = k[i, j];
                }
            }




            //------------- Hill Cipher - Analysis -------//
            List<int> final = new List<int>();

            for (int i = 0; i < num1; i++)
            {
                int sum1 = 0;
                sum1 = (kt[i, 0] * cipherMatrix[0, 0]) + (kt[i, 1] * cipherMatrix[1, 0]) + (kt[i, 2] * cipherMatrix[2, 0]);
                final.Add(sum1 % 26);

            }
            for (int i = 0; i < num1; i++)
            {
                int sum2 = 0;
                sum2 = (kt[i, 0] * cipherMatrix[0, 1]) + (kt[i, 1] * cipherMatrix[1, 1]) + (kt[i, 2] * cipherMatrix[2, 1]);
                final.Add(sum2 % 26);

            }
            for (int i = 0; i < num1; i++)
            {
                int sum3 = 0;
                sum3 = (kt[i, 0] * cipherMatrix[0, 2]) + (kt[i, 1] * cipherMatrix[1, 2]) + (kt[i, 2] * cipherMatrix[2, 2]);
                final.Add(sum3 % 26);
            }



            return final;



        }

        public string Analyse3By3Key(string plain3, string cipher3)
        {
            throw new NotImplementedException();
        }
    }
}
