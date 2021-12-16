#include "TokenringGlobals.h"
#include "TokenRingStructs.h"

sockaddr_in sin_Server; // Свойства(адрес) создаваемого сокета
char My_IP[20];
sockaddr new_client;
SOCKET TokenNetServerSocket; // Серверный сокет
SOCKET TokenNetClientSocket; // Клиентский сокет
HANDLE hEventConnectNewUserToTokenRing; // Событие подключения второго абонента к кольцу
HANDLE hEscapeEvent; // Событие выхода из программы (нажатие escape)
HANDLE hEventWorkWithMessageList[2]; // Событие незавершенной работы со списком сообщений
HANDLE hstd = GetStdHandle(STD_INPUT_HANDLE);
HANDLE hEventIAmOwner;
HANDLE hEventEndWorkWithMsgFormClient; // Событие конца работы с сообщением
HANDLE hEventEndWorkDeleteClient;
HANDLE hEventEndDataDownload; // Событие окончания загрузки содержания файла
struct _Token_Ring *Token_Ring;
struct _Token Token;
struct _Message_List *Message_List;
char* FileBuffer = (char*)malloc(DEFAULT_BUFFER); // Выделяем буферу память, равную размеру сектора
BOOL DeleteInProcessing = FALSE;
BOOL FileBufferIsChanged = FALSE;
BOOL FirstInNetwork = FALSE;
char Deleting_IP[20];