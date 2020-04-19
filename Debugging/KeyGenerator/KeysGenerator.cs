using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

namespace KeyGenerator
{
    public class KeysGenerator
    {
        public string GenerateKey()
        {
            byte[] address = NetworkInterface.GetAllNetworkInterfaces()
                                             .FirstOrDefault()
                                             .GetPhysicalAddress()
                                             .GetAddressBytes();
            byte[] date = BitConverter.GetBytes(DateTime.Now.Date.ToBinary());

            IEnumerable<int> result = address.Select((item, i) => item ^ date[i])
                                             .Select(item => item * 10);

            return string.Join("-", result);
        }
    }
}
