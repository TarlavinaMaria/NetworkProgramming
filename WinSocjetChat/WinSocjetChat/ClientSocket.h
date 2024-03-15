#pragma once
#include"Socket.h"

class ClientSocket :public Socket
{
public:
	ClientSocket(string username):Socket(username){}
	void ConnectToServer(const char ip[], int port);
};