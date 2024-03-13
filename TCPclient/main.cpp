//TCPclient
#include<iostream>
#include<WS2tcpip.h>
#include<WinSock2.h>

#pragma comment(lib, "WS2_32.lib")

using namespace std;

void main()
{
	setlocale(LC_ALL, "");

	SOCKET _socket;
	sockaddr_in addr;

	WSADATA wsaData;
	WSAStartup(MAKEWORD(2, 2), &wsaData);
	_socket = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
	addr.sin_family = AF_INET;
	inet_pton(AF_INET, "127.0.0.1", &addr.sin_addr);
	addr.sin_port = htons(20000);
	connect(_socket, (SOCKADDR*)&addr, sizeof(addr));

	CONST INT MAXSTRLEN = 255;
	CHAR buffer[MAXSTRLEN]{};
	CONST CHAR text[] = "Hello Word!";

	cout << "Press Enter to send message" << endl;
	cin.get();

	send(_socket, text, strlen(text), 0);
	INT i = recv(_socket, buffer, MAXSTRLEN, 0);
	cout << buffer << endl;

	closesocket(_socket);
	WSACleanup();
	system("PAUSE");
}