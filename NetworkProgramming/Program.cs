using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace NetworkProgramming
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите IP адрес: ");
            string ipAddress = Console.ReadLine();

            Console.Write("Введите маску подсети: ");
            string subnetMask = Console.ReadLine();

            try
            {
                IPCalculator calculator = new IPCalculator(ipAddress, subnetMask);

                // Получение адреса сети.
                IPAddress networkAddress = calculator.GetNetworkAddress();
                // Получение широковещательного адреса.
                IPAddress broadcastAddress = calculator.GetBroadcastAddress();
                // Вычисление макс. кол-ва хостов.
                uint maxHosts = calculator.GetMaxNumberOfHosts();

                Console.WriteLine($"Адрес сети: {networkAddress}");
                Console.WriteLine($"Широковещательный адрес: {broadcastAddress}");
                Console.WriteLine($"Максимальное количество хостов: {maxHosts}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            /*
             * IP-адрес: 192.168.0.1
               Маска: 255.255.255.0
               Адрес сети: 192.168.0.0
               Широковещательный адрес: 192.168.0.255
               Максимальное количество IP-адресов: 254
             */
        }
    }
}
