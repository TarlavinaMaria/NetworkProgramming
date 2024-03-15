#define _CRT_SECURE_NO_WARNINGS

#pragma once
#include<iostream>
#include<string>
#include<WS2tcpip.h>
#include<WinSock2.h>
#include<cstdio>

#pragma comment(lib, "WS2_32.lib")

using namespace std;

CONST INT MAXSTRLEN = 255;

class Socket
{
protected:
	WSADATA wsaData;
	SOCKET _socket;
	SOCKET acceptSocket;
	sockaddr_in addr;
	string username;
public:
	Socket();
	Socket(string username);
	~Socket();
	bool SendData(char* buffer);
	bool ReceiveData(char* buffer, int size);
	void CloseConnection();
	CONST CHAR* SendDataMessage();
};