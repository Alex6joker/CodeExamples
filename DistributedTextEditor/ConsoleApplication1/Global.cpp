#include "TokenringGlobals.h"
#include "TokenRingStructs.h"

sockaddr_in sin_Server; // ��������(�����) ������������ ������
char My_IP[20];
sockaddr new_client;
SOCKET TokenNetServerSocket; // ��������� �����
SOCKET TokenNetClientSocket; // ���������� �����
HANDLE hEventConnectNewUserToTokenRing; // ������� ����������� ������� �������� � ������
HANDLE hEscapeEvent; // ������� ������ �� ��������� (������� escape)
HANDLE hEventWorkWithMessageList[2]; // ������� ������������� ������ �� ������� ���������
HANDLE hstd = GetStdHandle(STD_INPUT_HANDLE);
HANDLE hEventIAmOwner;
HANDLE hEventEndWorkWithMsgFormClient; // ������� ����� ������ � ����������
HANDLE hEventEndWorkDeleteClient;
HANDLE hEventEndDataDownload; // ������� ��������� �������� ���������� �����
struct _Token_Ring *Token_Ring;
struct _Token Token;
struct _Message_List *Message_List;
char* FileBuffer = (char*)malloc(DEFAULT_BUFFER); // �������� ������ ������, ������ ������� �������
BOOL DeleteInProcessing = FALSE;
BOOL FileBufferIsChanged = FALSE;
BOOL FirstInNetwork = FALSE;
char Deleting_IP[20];