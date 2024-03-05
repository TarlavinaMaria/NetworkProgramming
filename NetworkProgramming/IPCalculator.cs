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
        // Объявление переменных для хранения IP и маски подсети.
        private IPAddress ip;
        private IPAddress subnetMask;

        public IPCalculator(string ip, string subnetMask)
        {
            this.ip = IPAddress.Parse(ip); // Парсинг и сохранение IP.
            this.subnetMask = IPAddress.Parse(subnetMask); // Парсинг и сохранение маски подсети.
        }

        public IPAddress GetNetworkAddress() // Метод для получения адреса сети.
        {
            // Получение байтового представления IP и маски.
            byte[] ipBytes = ip.GetAddressBytes();
            byte[] maskBytes = subnetMask.GetAddressBytes();

            // Создание массива байтов такого же размера, как и ipBytes, массив будет содержать байты итогового сетевого адреса
            byte[] networkBytes = new byte[ipBytes.Length];

            for (int i = 0; i < networkBytes.Length; i++) // Цикл для вычисления адреса сети.
            {
                #region INFO
                //    В теле цикла происходит побитовое логическое умножение каждого байта IP-адреса с соответствующим байтом маски подсети через выражение
                //    Эта операция определяет, какие биты в IP-адресе являются частью сетевого адреса;
                //    единицы в маске подсети указывают, что соответствующие биты IP-адреса должны оставаться неизменными, в то время как нули обозначают, 
                //    что биты IP-адреса не относятся к сетевому адресу и должны быть обнулены.
                #endregion
                networkBytes[i] = (byte)(ipBytes[i] & maskBytes[i]); // Логическое 'И'
            }

            return new IPAddress(networkBytes); // Создание и возврат адреса сети.
        }

        public IPAddress GetBroadcastAddress() // Метод для получения широковещательного адреса.
        {
            // Получение байтовых представлений.
            byte[] ipBytes = ip.GetAddressBytes();
            byte[] maskBytes = subnetMask.GetAddressBytes();

            // Создание массива байтов такого же размера, как и ipBytes, для хранения широковещательного адреса.
            byte[] broadcastBytes = new byte[ipBytes.Length];

            for (int i = 0; i < broadcastBytes.Length; i++) // Цикл для адреса широковещательной рассылки.
            {
                #region INFO
                // Побитовое ИЛИ (OR) между битами IP-адреса и инвертированными битами маски подсети.
                // Инвертирование маски подсети (~maskBytes[i]) превращает 0 в 1 и наоборот.
                // Побитовое ИЛИ с инвертированной маской устанавливает все биты "хоста" в IP-адресе в 1,
                // создавая широковещательный адрес.
                #endregion
                broadcastBytes[i] = (byte)(ipBytes[i] | ~maskBytes[i]); // OR между IP и инвертированной маской.
            }

            return new IPAddress(broadcastBytes); // Возврат широковещательного адреса.
        }

        public uint GetMaxNumberOfHosts() // Метод для вычисления максимального числа хостов.
        {
            byte[] maskBytes = subnetMask.GetAddressBytes(); // Байты маски.
            #region INFO
            // maskBytes.Reverse().ToArray() преобразует массив байтов в обратный порядок. Это необходимо, потому что IP-адреса обычно представлены в сетевом порядке байтов (big-endian), 
            // в то время как BitConverter.ToUInt32 ожидает данные в порядке little-endian на машинах с архитектурой Intel и AMD.
            // BitConverter.ToUInt32 преобразует четыре байта из массива в беззнаковое 32-битное целое число (uint).
            // ~ является побитовым оператором NOT, он инвертирует биты маски; то есть из 1 делает 0 и наоборот.
            #endregion
            uint invertedMask = ~BitConverter.ToUInt32(maskBytes.Reverse().ToArray(), 0); // Инвертирование маски.
            #region INFO
            // После инвертирования маски подсети получается значение, в котором все биты, изначально выставленные в 0 (означающие биты хостов), теперь становятся 1.
            // Это число на самом деле на единицу больше, чем максимальное количество хостов (поскольку включает в себя адрес для широковещательной передачи),
            // так что нам необходимо вычесть 1, чтобы получить максимально возможное число хостов.
            #endregion
            return invertedMask - 1; // Вычитание 2 (сеть и широковещательный адрес) и возврат числа хостов.
        }
    }
}
