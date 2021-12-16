#ifndef _TokenGlobals
#define _TokenGlobals
#include <windows.h>

extern sockaddr_in sin_Server; // Свойства(адрес) создаваемого сокета
extern char My_IP[20];
extern char Deleting_IP[20];
extern sockaddr new_client;
extern SOCKET TokenNetServerSocket; // Серверный сокет
extern SOCKET TokenNetClientSocket; // Клиентский сокет
extern HANDLE hEventConnectNewUserToTokenRing; // Событие подключения второго абонента к кольцу
extern HANDLE hEscapeEvent; // Событие выхода из программы (нажатие escape)
extern HANDLE hEventWorkWithMessageList[2]; // Событие незавершенной работы со списком сообщений
extern HANDLE hEventIAmOwner; // Событие , обозначающее, что мы являемся владельцами маркера
extern HANDLE hEventEndWorkWithMsgFormClient; // Событие конца работы с сообщением
extern HANDLE hEventEndWorkDeleteClient; // Событие конца работы с сообщением
extern HANDLE hEventEndDataDownload; // Событие окончания загрузки содержания файла
extern BOOL DeleteInProcessing; // Переменная, означающая, что сейчас происходит удаление
extern BOOL FileBufferIsChanged; // Переменная, означающая, что файл был изменен в ходе работы
extern BOOL FirstInNetwork; // Переменная, означающая, что мы первые в сети
extern HANDLE hstd;
extern char* FileBuffer;

#endif