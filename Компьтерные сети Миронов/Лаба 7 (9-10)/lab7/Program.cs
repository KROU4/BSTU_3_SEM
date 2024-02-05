using System;
using System.Net;
using System.Net.NetworkInformation;

class Program
{
    static void Main()
    {
        Console.WriteLine("Выберите действие:");
        Console.WriteLine("1. Определить MAC-адрес по IP-адресу");
        Console.WriteLine("2. Определить IP-адрес по MAC-адресу");

        int choice = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case 1:
                Console.Write("Введите IP-адрес: ");
                string ip = Console.ReadLine();
                string macAddress = GetMacAddressFromIp(ip);
                Console.WriteLine($"MAC-адрес для {ip}: {macAddress}");
                break;

            case 2:
                Console.Write("Введите MAC-адрес: ");
                string mac = Console.ReadLine();
                string ipAddress = GetIpFromMacAddress(mac);
                Console.WriteLine($"IP-адрес для {mac}: {ipAddress}");
                break;

            default:
                Console.WriteLine("Неверный выбор.");
                break;
        }
    }

    static string GetMacAddressFromIp(string ipAddress)
    {
        try
        {
            IPAddress ip = IPAddress.Parse(ipAddress);
            var arp = new ARPRequest(ip);
            return arp.MacAddress;
        }
        catch (Exception ex)
        {
            return $"Ошибка: {ex.Message}";
        }
    }

    static string GetIpFromMacAddress(string macAddress)
    {
        try
        {
            var arp = new ARPRequest(macAddress);
            return arp.IpAddress;
        }
        catch (Exception ex)
        {
            return $"Ошибка: {ex.Message}";
        }
    }
}

class ARPRequest
{
    public string MacAddress { get; }
    public string IpAddress { get; }

    public ARPRequest(IPAddress ipAddress)
    {
        var macAddr = GetMacAddress(ipAddress);
        MacAddress = macAddr.ToString();
        IpAddress = ipAddress.ToString();
    }

    public ARPRequest(string macAddress)
    {
        var ipAddr = GetIPAddress(macAddress);
        MacAddress = macAddress;
        IpAddress = ipAddr?.ToString() ?? "Не найден";
    }

    private PhysicalAddress GetMacAddress(IPAddress ipAddress)
    {
        var macAddr = PhysicalAddress.None;

        foreach (var nic in NetworkInterface.GetAllNetworkInterfaces())
        {
            var ipProps = nic.GetIPProperties();

            foreach (var ua in ipProps.UnicastAddresses)
            {
                if (ua.Address.Equals(ipAddress))
                {
                    macAddr = nic.GetPhysicalAddress();
                    break;
                }
            }

            if (!macAddr.Equals(PhysicalAddress.None))
                break;
        }

        return macAddr;
    }

    private IPAddress GetIPAddress(string macAddress)
    {
        var ipAddr = IPAddress.None;

        foreach (var nic in NetworkInterface.GetAllNetworkInterfaces())
        {
            if (nic.GetPhysicalAddress().ToString() == macAddress)
            {
                var ipProps = nic.GetIPProperties();
                foreach (var ua in ipProps.UnicastAddresses)
                {
                    if (ua.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        ipAddr = ua.Address;
                        break;
                    }
                }
            }

            if (!ipAddr.Equals(IPAddress.None))
                break;
        }

        return ipAddr;
    }
}
