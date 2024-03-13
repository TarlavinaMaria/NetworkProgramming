#pragma once
#include"Socket.h"

class ClientSocket :public Socket
{
public:
	void ConnectToServer(const char ip[], int port);
};