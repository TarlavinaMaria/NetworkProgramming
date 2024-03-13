#include "ServerSocket.h"

void ServerSocket::Listen()
{
	cout << "Waiting for client ..." << endl;
	if (listen(_socket, 1) == SOCKET_ERROR)
	{
		cerr << "Listen error" << endl;
		system("PAUSE");
		WSACleanup();
		exit(15);
	}
	acceptSocket = accept(_socket, NULL, NULL);
	while (acceptSocket == SOCKET_ERROR)
	{
		acceptSocket = accept(_socket, NULL, NULL);
	}
	_socket = acceptSocket;
}

void ServerSocket::Bind(int port)
{
	addr.sin_family = AF_INET;
	inet_pton(AF_INET, "0.0.0.0", &addr.sin_addr);
	addr.sin_port = htons(port);
	cout << "Binding to port" << port << endl;
	if (bind(_socket, (SOCKADDR*)&addr, sizeof(addr)) == SOCKET_ERROR)
	{
		cerr << "Failed to bind port" << endl;
		system("PAUSE");
		WSACleanup();
		exit(14);
	}
}

void ServerSocket::StartHosting(int port)
{
	Bind(port);
	Listen();
}
