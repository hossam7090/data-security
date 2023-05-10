using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;


namespace SecurityLibrary.ElGamal
{
    public class ElGamal
    {
        /// <summary>
        /// Encryption
        /// </summary>
        /// <param name="alpha"></param>
        /// <param name="q"></param>
        /// <param name="y"></param>
        /// <param name="k"></param>
        /// <returns>list[0] = C1, List[1] = C2</returns>
        public List<long> Encrypt(int q, int alpha, int y, int k, int m)
        {
           
            BigInteger K = BigInteger.ModPow(y, k, q);
            BigInteger c1 = BigInteger.ModPow(alpha, k, q);
            BigInteger c2 = (int)(K * m) % q;
            List<long> list = new List<long>();
            list.Add((long)c1);
            list.Add((long)c2);
            return list;

        }
        public int Decrypt(int c1, int c2, int x, int q)
        {
            BigInteger K = BigInteger.ModPow(c1, x, q);
            BigInteger K_1 = BigInteger.ModPow(K, q - 2, q);
            BigInteger M = BigInteger.ModPow((c2 * K_1), 1, q);


            return (int)M;
        }
    }
}
