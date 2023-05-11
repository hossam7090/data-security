using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.RSA
{
    public class RSA
    {
        public int Encrypt(int p, int q, int M, int e)
        {
            int n = p * q;

            int b = 1;
            int i = 0;
            while (i < e)
            {
                b = b % n;
                b *= (M % n);
                i++;
            }
            int res = b % n;
            return res;

        }

        public int Decrypt(int p, int q, int C, int e)
        {
            int d;
            int N = p * q;
            int Qn = (p - 1) * (q - 1);
            int a = e;
            int b = Qn;
            int X0 = 1;
            int XN = 1;
            int Y0 = 0;
            int YN = 0;
            int X1 = 0;
            int Y1 = 1;
            int qn;
            int r = a % b;
            for (; r > 0;)
            {
                q = a / b;
                XN = X0 - q * X1;
                YN = Y0 - q * Y1;

                X0 = X1;
                Y0 = Y1;
                X1 = XN;
                Y1 = YN;
                a = b;
                b = r;
                r = a % b;
            }
            d = XN;
            if (d < 0)
            {
                d = (XN % Qn);
                d += Qn;
            }

            int y = 1;
            int i = 0;
            while (i < d)
            {
                y = y % N;
                y *= (C % N);
                i++;
            }
            int res = y % N;
            return res;
        }
    }
}