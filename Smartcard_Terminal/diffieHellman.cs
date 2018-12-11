using System;
using System.Numerics;
using System.Security.Cryptography;

/// <summary>
/// Class for creating Diffie-Hellman Keys
/// </summary>
namespace Smartcard_Terminal
{
    class diffieHellman
    {
        private BigInteger public_p = new BigInteger();
        private BigInteger public_g = new BigInteger();

        private BigInteger private_a = new BigInteger();
        private BigInteger public_A = new BigInteger();
        private BigInteger public_B = new BigInteger();

        private BigInteger private_Shared = new BigInteger();

        /// <summary>
        /// Constructor of the Diffie-Hellman Class
        /// Initialize the public values p and g
        /// Calculates the private value a and public value A
        /// </summary>
        public diffieHellman()
        {
            public_p = BigInteger.Parse("2074722246773485207821695222107608587480996474721117292752992589912196684750549658310084416732550077");
            public_g = BigInteger.Parse("282755483533707287054752184321121345766861480697448703443857012153264407439766013042402571");

            RNGCryptoServiceProvider rand = new RNGCryptoServiceProvider();
            byte[] bytes = new byte[40];

            //do
            //{
                rand.GetBytes(bytes);
                private_a = new BigInteger(bytes);
                private_a = BigInteger.Abs(private_a);
            //} while (BigInteger.Compare(private_a, public_p) >= 0);

            Console.WriteLine("Private a: " + private_a.ToString());

            public_A = BigInteger.ModPow(public_g, private_a, public_p);
            Console.WriteLine("Key´s generated.");
        }

        /// <summary>
        /// Getter Method that Returns public_p
        /// </summary>
        /// <returns>public_p</returns>
        public String get_public_p()
        {
            return public_p.ToString();
        }

        /// <summary>
        /// Getter Method that Returns public_g
        /// </summary>
        /// <returns>public_g</returns>
        public String get_public_g()
        {
            return public_g.ToString();
        }

        /// <summary>
        /// Getter Method that Returns public_A
        /// </summary>
        /// <returns>public_A</returns>
        public String get_public_A()
        {
            return public_A.ToString();
        }

        /// <summary>
        /// Getter Method that Returns public_B
        /// </summary>
        /// <returns>public_B</returns>
        public void set_public_B(String pub_B)
        {
            public_B = BigInteger.Parse(pub_B);
            private_Shared = BigInteger.ModPow(public_B, private_a, public_p);
        }

        /// <summary>
        /// Getter Method that Returns private_Shared
        /// </summary>
        /// <returns>private_Shared</returns>
        public String get_Shared_Secret()
        {
            return private_Shared.ToString();
        }
    }
}
