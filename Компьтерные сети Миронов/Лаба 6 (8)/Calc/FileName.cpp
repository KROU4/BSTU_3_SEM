#include <iostream>
#include <sstream>
#include <vector>

bool isValidIPAddress(const std::string& ipAddress) {
    std::vector<int> octets;
    std::stringstream ss(ipAddress);
    std::string octet;

    while (std::getline(ss, octet, '.')) {
        try {
            int value = std::stoi(octet);
            if (value < 0 || value > 255) {
                return false; // Ошибка: некорректный октет
            }
            octets.push_back(value);
        }
        catch (std::invalid_argument&) {
            return false; // Ошибка: не число
        }
        catch (std::out_of_range&) {
            return false; // Ошибка: переполнение
        }
    }

    return octets.size() == 4; // Должно быть четыре октета
}

bool isValidSubnetMask(const std::string& subnetMask) {
    std::vector<int> octets;
    std::stringstream ss(subnetMask);
    std::string octet;

    while (std::getline(ss, octet, '.')) {
        try {
            int value = std::stoi(octet);
            if (value != 0 && value != 128 && value != 192 && value != 224 && value != 240 && value != 248 && value != 252 && value != 254 && value != 255) {
                return false; // Ошибка: недопустимое значение октета маски
            }
            octets.push_back(value);
        }
        catch (std::invalid_argument&) {
            return false; // Ошибка: не число
        }
        catch (std::out_of_range&) {
            return false; // Ошибка: переполнение
        }
    }

    // Проверка непрерывности единиц в маске
    bool encounteredZero = false;
    for (const int& octet : octets) {
        if (encounteredZero && octet != 0) {
            return false; // Ошибка: непрерывность единиц нарушена
        }
        if (octet == 0) {
            encounteredZero = true;
        }
    }

    return true;
}

void calculateNetworkAndHostID(const std::string& ipAddress, const std::string& subnetMask) {
    std::vector<int> ipOctets, maskOctets, networkID, hostID, broadcastID;

    std::stringstream ipStream(ipAddress);
    std::stringstream maskStream(subnetMask);
    std::string octet;

    while (std::getline(ipStream, octet, '.')) {
        ipOctets.push_back(std::stoi(octet));
    }
    while (std::getline(maskStream, octet, '.')) {
        maskOctets.push_back(std::stoi(octet));
    }

    for (size_t i = 0; i < ipOctets.size(); ++i) {
        networkID.push_back(ipOctets[i] & maskOctets[i]);
        broadcastID.push_back((ipOctets[i] & maskOctets[i]) | (~maskOctets[i] & 0xFF));
    }

    for (size_t i = 0; i < ipOctets.size(); ++i) {
        hostID.push_back(ipOctets[i] & ~maskOctets[i]);
    }

    std::cout << "\nIP Address: " << ipAddress << std::endl;
    std::cout << "Subnet Mask: " << subnetMask << std::endl;
    std::cout << "Network ID: ";
    for (size_t i = 0; i < networkID.size(); ++i) {
        std::cout << networkID[i];
        if (i < networkID.size() - 1) {
            std::cout << ".";
        }
    }
    std::cout << std::endl;

    std::cout << "Host ID: ";
    for (size_t i = 0; i < hostID.size(); ++i) {
        std::cout << hostID[i];
        if (i < hostID.size() - 1) {
            std::cout << ".";
        }
    }
    std::cout << std::endl;

    std::cout << "Broadcast ID: ";
    for (size_t i = 0; i < broadcastID.size(); ++i) {
        std::cout << broadcastID[i];
        if (i < broadcastID.size() - 1) {
            std::cout << ".";
        }
    }
    std::cout << std::endl;
}

int main() {
    std::string ipAddress, subnetMask;

    std::cout << "Enter IP Address: ";
    std::cin >> ipAddress;

    if (!isValidIPAddress(ipAddress)) {
        std::cout << "Invalid IP Address." << std::endl;
        return 1;
    }

    std::cout << "Enter Subnet Mask: ";
    std::cin >> subnetMask;

    if (!isValidSubnetMask(subnetMask)) {
        std::cout << "Invalid Subnet Mask." << std::endl;
        return 1;
    }

    calculateNetworkAndHostID(ipAddress, subnetMask);

    return 0;
}
