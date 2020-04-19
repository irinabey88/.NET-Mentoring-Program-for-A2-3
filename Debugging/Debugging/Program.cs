using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using CrackMe;
using KeyGenerator;

namespace Debugging
{
    class Program
    {
        static void Main(string[] args)
        {
            KeysGenerator keyGenerator = new KeysGenerator();

            Console.WriteLine(keyGenerator.GenerateKey());
            Console.ReadLine();
        }
    }
}
