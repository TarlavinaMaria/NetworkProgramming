#pragma once
#include"Socket.h"

class ServerSocket : public Socket
{
public:
	void Listen();
	void Bind(int port);
	void StartHosting(int port);
};