#ifndef _TokenGlobals
#define _TokenGlobals
#include <windows.h>

extern sockaddr_in sin_Server; // ��������(�����) ������������ ������
extern char My_IP[20];
extern char Deleting_IP[20];
extern sockaddr new_client;
extern SOCKET TokenNetServerSocket; // ��������� �����
extern SOCKET TokenNetClientSocket; // ���������� �����
extern HANDLE hEventConnectNewUserToTokenRing; // ������� ����������� ������� �������� � ������
extern HANDLE hEscapeEvent; // ������� ������ �� ��������� (������� escape)
extern HANDLE hEventWorkWithMessageList[2]; // ������� ������������� ������ �� ������� ���������
extern HANDLE hEventIAmOwner; // ������� , ������������, ��� �� �������� ����������� �������
extern HANDLE hEventEndWorkWithMsgFormClient; // ������� ����� ������ � ����������
extern HANDLE hEventEndWorkDeleteClient; // ������� ����� ������ � ����������
extern HANDLE hEventEndDataDownload; // ������� ��������� �������� ���������� �����
extern BOOL DeleteInProcessing; // ����������, ����������, ��� ������ ���������� ��������
extern BOOL FileBufferIsChanged; // ����������, ����������, ��� ���� ��� ������� � ���� ������
extern BOOL FirstInNetwork; // ����������, ����������, ��� �� ������ � ����
extern HANDLE hstd;
extern char* FileBuffer;

#endif