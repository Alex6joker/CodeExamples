#ifndef _TokenFuncs
#define _TokenFuncs
#include <windows.h>
#include "TokenRingStructs.h"

char** GetArpTable(int*);
void Get_The_Pressed_Key(_Keyboard_Input_Codes*);
void SendTokenToTokenRing(LPVOID);
void WaitNewClients(LPVOID);
void WorkWithClientMessage(LPVOID);
void DocumentWork();
void Add_To_Msg_List(char*);
void Take_Package_With_The_Highest_Priority(char*);
BOOL Pressed_Func_Key(_Keyboard_Input_Codes*);
void ChangeConsolePozition(_CONSOLE_SCREEN_BUFFER_INFO*, int, int);
void Make_And_Send_Data_Frame(_Keyboard_Input_Codes*, int);
char* Remake_Text(int, char);
void GetCursorPositionFromConsole(_CONSOLE_SCREEN_BUFFER_INFO*, int*);
void BACKSPACE_Remake_Text(int);
void DELETE_Remake_Text(int);
void Make_New_File(HANDLE, OVERLAPPED);
void Read_To_FileBuffer(HANDLE, OVERLAPPED);
void reconnect();
void TestAndDownloadFileBuffer(LPVOID lParam);
void SendDataBuffer(int, char*);
char* UserQuestions(int, char**, sockaddr_in*, SOCKET, char*);
void WinSockConnect();
int ConnectOrDisconnectMeFromTokenRing(SOCKET, char*, int, char);
struct in_addr* GetHostNameAndAddresses(int*);
unsigned int ChooseMyIP(int, in_addr*);

#endif