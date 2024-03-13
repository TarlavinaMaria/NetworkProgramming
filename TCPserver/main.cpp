#include<iostream>
#include<WS2tcpip.h> //содержит фунции дл€ работы с адресами
#include<WinSock2.h> //содержит обь€влени€ дл€ работы с протоколами TCP, UDP, ICMP ...

#pragma comment(lib, "WS2_32.lib")

using namespace std;

void main()
{
	setlocale(LC_ALL, "");

	SOCKET _socket; // этот сокет будет прослушивать порт
	sockaddr_in addr; // адрес, который будет прослушивать сервер

	// —оздаем и инициализируем сокет:
	WSADATA wsaData;
	WSAStartup(MAKEWORD(2, 2), &wsaData);
	_socket = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
	addr.sin_family = AF_INET;  //AF_INET - IPv4
	inet_pton(AF_INET, "0.0.0.0", &addr.sin_addr); //задаем прослущиваемый IP адерес
	addr.sin_port = htons(20000);
	//ѕосле того как адресс и порт указаны, их нужно прив€зать к сокету:
	bind(_socket, (SOCKADDR*)&addr, sizeof(addr));
	listen(_socket, 1);
	cout << "Server started" << endl;

	//ѕри подключении клиента создаетс€ еще один сокет:
	SOCKET accept_socket = accept(_socket, NULL, NULL);
	const int MAXSTRLEN = 255;
	char buffer[MAXSTRLEN] = {};
	int i = recv(accept_socket, buffer, MAXSTRLEN, 0);
	cout << buffer << endl;
	const char text[] = "Hello from Server!";
	send(accept_socket, text, strlen(text), 0);

	// «акрываем сокеты:
	closesocket(accept_socket);
	closesocket(_socket);

	// ќсвобождаем ресурсы:
	WSACleanup();
	system("PAUSE");
}