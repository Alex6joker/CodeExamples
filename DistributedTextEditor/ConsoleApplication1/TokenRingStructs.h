#ifndef _TokenStructs
#define _TokenStructs
#include "TokenRingDefines.h"

struct _Token_Ring
{
	char IP[20]; // IP данного компьютера
	_Token_Ring* next;
};

struct _Token
{
	char Owner_IP[20];
	int Priority; // бит приоритета (0 - 7)
	char Token_bit; // Бит маркера (1 - маркер, 0 - кадр данных)
};

struct _Message_List
{
	char Message[DEFAULT_BUFFER];
	int Msg_Priority; // Приоритет сообщения
	int Msg_Type;    // Тип сообщения (0 - кадр данных, 1 - маркер)
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