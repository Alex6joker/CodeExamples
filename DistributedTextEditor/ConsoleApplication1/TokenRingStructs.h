#ifndef _TokenStructs
#define _TokenStructs
#include "TokenRingDefines.h"

struct _Token_Ring
{
	char IP[20]; // IP ������� ����������
	_Token_Ring* next;
};

struct _Token
{
	char Owner_IP[20];
	int Priority; // ��� ���������� (0 - 7)
	char Token_bit; // ��� ������� (1 - ������, 0 - ���� ������)
};

struct _Message_List
{
	char Message[DEFAULT_BUFFER];
	int Msg_Priority; // ��������� ���������
	int Msg_Type;    // ��� ��������� (0 - ���� ������, 1 - ������)
	struct _Message_List *next;
};

struct _Keyboard_Input_Codes
{
	char Scan_Code;
	char Key_Code;
};

extern struct _Token_Ring *Token_Ring;
extern struct _Token Token;
extern struct _Message_List *Message_List;

#endif