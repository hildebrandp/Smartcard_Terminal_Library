using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Security.Cryptography;

namespace Smartcard_Terminal
{
    class diffieHellman
    {
        BigInteger[] primeArray = new BigInteger[] {BigInteger.Parse("48112959837082048697"), BigInteger.Parse("54673257461630679457"),
                                                    BigInteger.Parse("29497513910652490397"), BigInteger.Parse("40206835204840513073"),
                                                    BigInteger.Parse("12764787846358441471"), BigInteger.Parse("71755440315342536873"),
                                                    BigInteger.Parse("45095080578985454453"), BigInteger.Parse("27542476619900900873"),
                                                    BigInteger.Parse("66405897020462343733"), BigInteger.Parse("36413321723440003717")};

        BigInteger[] generatorArray = new BigInteger[] {BigInteger.Parse("5915587277"), BigInteger.Parse("1500450271"),
                                                    BigInteger.Parse("3267000013"), BigInteger.Parse("5754853343"),
                                                    BigInteger.Parse("4093082899"), BigInteger.Parse("9576890767"),
                                                    BigInteger.Parse("3628273133"), BigInteger.Parse("2860486313"),
                                                    BigInteger.Parse("5463458053"), BigInteger.Parse("3367900313")};

        private BigInteger public_p = new BigInteger();
        private BigInteger public_g = new BigInteger();

        private BigInteger private_a = new BigInteger();
        private BigInteger public_A = new BigInteger();
        private BigInteger public_B = new BigInteger();

        private BigInteger private_Shared = new BigInteger();

        public diffieHellman()
        {
            Random rnd = new Random();
            int num = rnd.Next(primeArray.Length);
            public_p = primeArray[num];

            num = rnd.Next(generatorArray.Length);
            public_g = generatorArray[num];

            RNGCryptoServiceProvider rand = new RNGCryptoServiceProvider();
            byte[] bytes = new byte[8];

            do
            {
                rand.GetBytes(bytes);
                private_a = new BigInteger(bytes);
                private_a = BigInteger.Abs(private_a);
            } while (BigInteger.Compare(private_a, public_p) >= 0);

            Console.WriteLine("Private a: " + private_a.ToString());

            public_A = BigInteger.ModPow(public_g, private_a, public_p);
            Console.WriteLine("Key´s generated.");
        }

        public String get_public_p()
        {
            return public_p.ToString();
        }

        public String get_public_g()
        {
            return public_g.ToString();
        }

        public String get_public_A()
        {
            return public_A.ToString();
        }

        public void set_public_B(String pub_B)
        {
            public_B = BigInteger.Parse(pub_B);
            private_Shared = BigInteger.ModPow(public_B, private_a, public_p);
        }

        public String get_Shared_Secret()
        {
            return private_Shared.ToString();
        }
    }
}
