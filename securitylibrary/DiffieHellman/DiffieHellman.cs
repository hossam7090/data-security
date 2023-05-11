using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.DiffieHellman
{
    public class DiffieHellman
    {
        public List<int> GetKeys(int q, int alpha, int xa, int xb)
        {
            //throw new NotImplementedException();
            BigInteger ya = BigInteger.ModPow(alpha, xa, q);
            BigInteger yb = BigInteger.ModPow(alpha, xb, q);
            BigInteger Ka = BigInteger.ModPow(yb, xa, q);
            BigInteger Kb = BigInteger.ModPow(ya, xb, q);
            List<int> keys = new List<int>();
            keys.Add((int)Ka);
            keys.Add((int)Kb);
            return keys;
            
        }
    }
}