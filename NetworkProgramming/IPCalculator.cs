using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;// Подключение пространства имен, работа с IP-адресами

namespace NetworkProgramming
{
    internal class IPCalculator
    {
        // Объявление приватных переменных для хранения IP и маски подсети.
        private IPAddress ip;
        private IPAddress subnetMask;

        public IPCalculator(string ip, string subnetMask) // Конструктор класса.
        {
            this.ip = IPAddress.Parse(ip); // Парсинг и сохранение IP.
            this.subnetMask = IPAddress.Parse(subnetMask); // Парсинг и сохранение маски подсети.
        }

        public IPAddress GetNetworkAddress() // Метод для получения адреса сети.
        {
            // Получение байтового представления IP и маски.
            byte[] ipBytes = ip.GetAddressBytes();
            byte[] maskBytes = subnetMask.GetAddressBytes();
            byte[] networkBytes = new byte[ipBytes.Length];

            for (int i = 0; i < networkBytes.Length; i++) // Цикл для вычисления адреса сети.
            {
                networkBytes[i] = (byte)(ipBytes[i] & maskBytes[i]); // Логическое 'И' между IP и маской.
            }

            return new IPAddress(networkBytes); // Создание и возврат адреса сети.
        }

        public IPAddress GetBroadcastAddress() // Метод для получения широковещательного адреса.
        {
            // Получение байтовых представлений.
            byte[] ipBytes = ip.GetAddressBytes();
            byte[] maskBytes = subnetMask.GetAddressBytes();
            byte[] broadcastBytes = new byte[ipBytes.Length];

            for (int i = 0; i < broadcastBytes.Length; i++) // Цикл для адреса широковещательной рассылки.
            {
                broadcastBytes[i] = (byte)(ipBytes[i] | ~maskBytes[i]); // OR между IP и инвертированной маской.
            }

            return new IPAddress(broadcastBytes); // Возврат широковещательного адреса.
        }

        public uint GetMaxNumberOfHosts() // Метод для вычисления максимального числа хостов.
        {
            byte[] maskBytes = subnetMask.GetAddressBytes(); // Байты маски.
            uint invertedMask = ~BitConverter.ToUInt32(maskBytes.Reverse().ToArray(), 0); // Инвертирование маски.

            return invertedMask - 1; // Вычитание 2 (сеть и широковещательный адрес) и возврат числа хостов.
        }
    }
}
