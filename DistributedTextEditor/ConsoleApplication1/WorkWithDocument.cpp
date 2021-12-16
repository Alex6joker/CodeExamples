#include <stdio.h>
#include <conio.h>
#include <winsock2.h>
#include <windows.h>
#include <stdlib.h>
#include "TokenRingGlobals.h"
#include "TokenRingDefines.h"
#include "TokenRingStructs.h"
#include "TokenRingFuncs.h"


void DocumentWork()
{
	OVERLAPPED OvlFile;
	// Дескриптор завершения ассинхронной операции ввода-вывода для структуры OVERLAPPED
	HANDLE OvlFileEnd = CreateEvent(NULL, FALSE, TRUE, NULL);
	OvlFile.Offset = 0;
	OvlFile.OffsetHigh = 0;
	OvlFile.hEvent = OvlFileEnd;

	// Анализируем, был ли изменен файл на других компьютерах
	// Создаем поток, который будем ожидать
	// В нем будем отсылать посылку следующему по кольцу, был ли изменен у него файл
	// Если был, то скачиваем файл, иначе Read_To_FileBuffer
	HANDLE hTestAndDownloadFileBuffer = CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)TestAndDownloadFileBuffer, NULL, NULL, NULL);
	printf("Загрузка общего файла, пожалуйста, подождите");
	WaitForSingleObject(hEventEndDataDownload, INFINITE);
	HANDLE hFile = NULL;
	if (FirstInNetwork)
	{
		// Открываем файл
		Read_To_FileBuffer(hFile, OvlFile);
	}
	system("cls");
	printf("%s", FileBuffer);
	CONSOLE_SCREEN_BUFFER_INFO bi;
	_Keyboard_Input_Codes Input_Codes; // Для получения скан и ascii-кодов нажатой клавиши

	BOOL There_are_some_changes = FALSE; // Для фиксирования действий в консоли и над текстом
	for (;;)
	{
		Get_The_Pressed_Key(&Input_Codes);
		{
			// Проверяем, нажата ли какая-либо функциональная клавиша по скан-коду
			if (Pressed_Func_Key(&Input_Codes))
			{
				Get_The_Pressed_Key(&Input_Codes);
				if (Input_Codes.Scan_Code == 0x01) // Если нажата ESC, то сохраняем файл и выходим из программы
					break;
				else if (Input_Codes.Scan_Code == 0x0e)
				{
					There_are_some_changes = TRUE;
					int Position;
					GetCursorPositionFromConsole(&bi, &Position);
					BACKSPACE_Remake_Text(Position);
					Make_And_Send_Data_Frame(&Input_Codes, Position);
				}
				else if (Input_Codes.Scan_Code == 0x53)
				{
					There_are_some_changes = TRUE;
					int Position;
					GetCursorPositionFromConsole(&bi, &Position);
					DELETE_Remake_Text(Position);
					Make_And_Send_Data_Frame(&Input_Codes, Position);
				}
			}
			else if ((Input_Codes.Key_Code >= 0xe0 && Input_Codes.Key_Code <= 0xff) || (Input_Codes.Key_Code >= 0xc0 && Input_Codes.Key_Code <= 0xdf)
				|| (Input_Codes.Key_Code >= 0x61 && Input_Codes.Key_Code <= 0x7a) || (Input_Codes.Key_Code >= 0x41 && Input_Codes.Key_Code <= 0x5a))
			{
				Get_The_Pressed_Key(&Input_Codes);
				There_are_some_changes = TRUE;
				int Position;
				GetCursorPositionFromConsole(&bi, &Position);
				FileBuffer = Remake_Text(Position, Input_Codes.Key_Code);
				Make_And_Send_Data_Frame(&Input_Codes, Position);
			}
			if (There_are_some_changes)
			{
				system("cls");
				printf("%s", FileBuffer);
				// Сохраняем позицию курсора консоли
				SetConsoleCursorPosition(GetStdHandle(STD_OUTPUT_HANDLE), bi.dwCursorPosition);
			}
			if (Input_Codes.Scan_Code == 0x4b)	ChangeConsolePozition(&bi, -1, 0);
			else if (Input_Codes.Scan_Code == 0x48)	ChangeConsolePozition(&bi, 0, -1);
			else if (Input_Codes.Scan_Code == 0x4d)	ChangeConsolePozition(&bi, 1, 0);
			else if (Input_Codes.Scan_Code == 0x50)	ChangeConsolePozition(&bi, 0, 1);
			There_are_some_changes = FALSE;
			memset((_Keyboard_Input_Codes*)&Input_Codes, 0, sizeof(_Keyboard_Input_Codes));
		}
		// Добавляем нажатую клавишу в список для отправки другим абонентам кольца
		Sleep(0);
	}

	// Сохранение файла
	SetEvent(OvlFileEnd);
	OvlFile.Offset = 0;
	OvlFile.OffsetHigh = 0;
	Make_New_File(hFile, OvlFile);
	SetEvent(hEscapeEvent);
}

void Get_The_Pressed_Key(_Keyboard_Input_Codes* Codes)
{
	const int count = 1;//с этим числом можно эксперементировать
	INPUT_RECORD IR[count];
	DWORD read;
	while (ReadConsoleInput(hstd, IR, count, &read))
	{
		//symbol = getch();
		for (DWORD i = 0; i < read; i++)
		{
			//if (IR[i].EventType == KEY_EVENT && IR[i].Event.KeyEvent.wVirtualKeyCode == VK_ESCAPE)
			if (IR[i].EventType == KEY_EVENT)
			{
				Codes->Scan_Code = IR[i].Event.KeyEvent.wVirtualScanCode;
				Codes->Key_Code = IR[i].Event.KeyEvent.wVirtualKeyCode;
				SHORT CAPS_condition = GetKeyState(VK_CAPITAL);
				SHORT SHIFT_condition = GetKeyState(VK_SHIFT);
				if (!((LOBYTE(CAPS_condition) == 1) || (HIBYTE(SHIFT_condition) == 0xff))) // Если включен CAPS или нажата SHIFT, то ничего не меняем
					strlwr(&(Codes->Key_Code));
			}
		}
		FlushConsoleInputBuffer(hstd);
		return;
	}
}

BOOL Pressed_Func_Key(_Keyboard_Input_Codes* Codes) // Проверка нажатия функциональной клавиши
{
	if (Codes->Scan_Code == 0x01 || Codes->Scan_Code == 0x1c || Codes->Scan_Code == 0x0e || Codes->Scan_Code == 0x0f || Codes->Scan_Code == 0x53
		|| Codes->Scan_Code == 0x4b || Codes->Scan_Code == 0x50 || Codes->Scan_Code == 0x4d || Codes->Scan_Code == 0x48 || Codes->Scan_Code == 0x3a)
		return TRUE;
	else return FALSE;
}

void ChangeConsolePozition(_CONSOLE_SCREEN_BUFFER_INFO* bi, int x, int y)
{
	GetConsoleScreenBufferInfo(GetStdHandle(STD_OUTPUT_HANDLE), bi); // Получаем положение клавиши
	if (x == -1)
		if (bi->dwCursorPosition.X != 0) bi->dwCursorPosition.X += x;
	if (y == -1)
		if (bi->dwCursorPosition.Y != 0) bi->dwCursorPosition.Y += y;
	if (x == 1)
		bi->dwCursorPosition.X += x;
	if (y == 1)
		bi->dwCursorPosition.Y += y;
	SetConsoleCursorPosition(GetStdHandle(STD_OUTPUT_HANDLE), bi->dwCursorPosition);
}

void Make_And_Send_Data_Frame(_Keyboard_Input_Codes* Codes,int Pos)
{
	char* Data_Frame = (char*)malloc(25);
	char* begin = Data_Frame; // Указываем на начало сообщения

	// Заполняем приоритет и тип сообщения
	Data_Frame[0] = 2;
	Data_Frame[1] = '0';
	Data_Frame += 2;

	// Указываем владельца кадра данных
	strcpy(Data_Frame, My_IP);

	// Устанавливаем, что посылаем - скан-код или key-код
	Data_Frame += strlen(Data_Frame);
	Data_Frame[0] = ' ';
	Data_Frame++;
	if (Pressed_Func_Key(Codes))
	{
		Data_Frame[0] = '0';
		Data_Frame++;
		Data_Frame[0] = ' ';
		Data_Frame[1] = Codes->Scan_Code;
	}
	else
	{
		Data_Frame[0] = '1';
		Data_Frame++;
		Data_Frame[0] = ' ';
		Data_Frame[1] = Codes->Key_Code;
	}
	Data_Frame += 2;
	Data_Frame[0] = ' ';
	Data_Frame++;
	itoa(Pos, Data_Frame, 10);
	Add_To_Msg_List(begin);
	free(begin);
}

char* Remake_Text(int Position, char symbol) // Перестройка текста согласно введенному символу и позиции курсора
{
	if (Position == strlen(FileBuffer) + 1)
	{	// Выделяем новую память на 1 больше и вставляем в нее введенный символ на позицию курсора консоли
		FileBuffer = (char*)realloc(FileBuffer, Position);
		FileBuffer[Position - 1] = symbol;
		FileBuffer[Position] = '\0';
	}
	else
	{
		int i = strlen(FileBuffer);
		char* temp_after = (char*)malloc(strlen(FileBuffer) + 1 - (Position - 1));
		FileBuffer += Position - 1;
		strcpy(temp_after, FileBuffer);
		FileBuffer -= Position - 1;
		FileBuffer = (char*)realloc(FileBuffer, strlen(FileBuffer) + 1);
		strncpy(FileBuffer, FileBuffer, Position);
		FileBuffer[Position - 1] = symbol;
		FileBuffer[Position] = '\0';
		strcat(FileBuffer, temp_after);
		free(temp_after);
	}
	return FileBuffer;
}

void GetCursorPositionFromConsole(CONSOLE_SCREEN_BUFFER_INFO* bi, int* Pos)
{
	// Получаем положение курсора
	GetConsoleScreenBufferInfo(GetStdHandle(STD_OUTPUT_HANDLE), bi);
	// Вычисление позиции
	*Pos = (bi->dwCursorPosition.X + 1) + (bi->dwCursorPosition.Y * 80);
}

void BACKSPACE_Remake_Text(int Position)
{
	if (Position != 1)
	{
		if (Position == (strlen(FileBuffer) + 1)) // Удаление происходит с конца
		{
			if ((int)strlen(FileBuffer) - 1 > 0)
				FileBuffer = (char*)realloc(FileBuffer, strlen(FileBuffer) - 1); // Отрезаем конец
			if (Position > 1)FileBuffer[Position - 2] = '\0';
		}
		else
		{
			char* temp_before = (char*)malloc(Position);
			char* temp_after = (char*)malloc((strlen(FileBuffer) + 1) - (Position - 1));
			strncpy(temp_before, FileBuffer, Position - 2);
			temp_before[Position - 2] = '\0';
			FileBuffer += Position - 1;
			strcpy(temp_after, FileBuffer);
			FileBuffer -= Position - 1;
			FileBuffer = (char*)realloc(FileBuffer, strlen(FileBuffer) - 1); // Выделяем память меньше
			strcpy(FileBuffer, temp_before);
			strcat(FileBuffer, temp_after);
			free(temp_before);
			free(temp_after);
		}
	}
}

void DELETE_Remake_Text(int Position)
{
	if (Position != strlen(FileBuffer) + 1 && strlen(FileBuffer) != 1)
	{
		char* temp_before = (char*)malloc(Position);
		char* temp_after = (char*)malloc((strlen(FileBuffer) + 1) - (Position - 1));
		strncpy(temp_before, FileBuffer, Position - 1);
		temp_before[Position - 1] = '\0';
		FileBuffer += Position;
		strcpy(temp_after, FileBuffer);
		FileBuffer -= Position;
		FileBuffer = (char*)realloc(FileBuffer, strlen(FileBuffer) - 1); // Выделяем память меньше
		strcpy(FileBuffer, temp_before);
		strcat(FileBuffer, temp_after);
		free(temp_before);
		free(temp_after);
	}
}

void Read_To_FileBuffer(HANDLE hFile, OVERLAPPED OvlFile)
{
	hFile = CreateFile(L"CommonFile.txt", GENERIC_READ, FILE_SHARE_READ, NULL, OPEN_EXISTING, FILE_FLAG_OVERLAPPED | FILE_FLAG_NO_BUFFERING, NULL);
	if (hFile == INVALID_HANDLE_VALUE)
	{
		wprintf(L"%s%c", L"Handle create file error", '\n');
	}

	int FileSize = GetFileSize(hFile, NULL); // Получаем размер файла

	DWORD ReadedBytesTotal = 0;
	DWORD ReadedBytes = 0;
	char* ReadFileBuffer = (char*)malloc(DEFAULT_BUFFER); // Буфер для чтения из файла
	while (ReadedBytesTotal < FileSize) // Пока не был полностью перекопирован весь файл
	{
		FileBuffer = (char*)realloc(FileBuffer, DEFAULT_BUFFER + ReadedBytesTotal); // Выделяем буферу память, равную размеру сектора
		FileBuffer[ReadedBytesTotal] = '\0';
		ReadFile(hFile, ReadFileBuffer, DEFAULT_BUFFER, NULL, &OvlFile); // Читаем асинхронно данные из файла

		WaitForSingleObject(OvlFile.hEvent, INFINITE); // Ждем завершения асинхронной операции чтения

		GetOverlappedResult(hFile, &OvlFile, &ReadedBytes, FALSE); // Узнаем число считанных байт

		OvlFile.Offset += ReadedBytes; // Увеличиваем смещение в файле
		ReadedBytesTotal += ReadedBytes;
		strcat(FileBuffer, ReadFileBuffer);
	}
	FileBuffer[FileSize] = '\0';
	CloseHandle(hFile);
	free(ReadFileBuffer);
	// Изначальный размер буфера указан в размере файла FileSize
}

void Make_New_File(HANDLE hFile,OVERLAPPED OvlFile)
{
	hFile = CreateFile(L"NewCommonFile.txt", GENERIC_WRITE, FILE_SHARE_READ, NULL, CREATE_ALWAYS, FILE_FLAG_OVERLAPPED | FILE_FLAG_NO_BUFFERING, NULL);
	if (hFile == INVALID_HANDLE_VALUE)
	{
		wprintf(L"%s%c", L"Handle create file error", '\n');
	}

	DWORD WrittenBytesTotal = 0;
	DWORD WrittenBytes = 0;
	while (WrittenBytesTotal < strlen(FileBuffer)) // Пока не был полностью перекопирован весь файл
	{
		WriteFile(hFile, FileBuffer, DEFAULT_BUFFER, NULL, &OvlFile); // Читаем асинхронно данные из файла

		WaitForSingleObject(OvlFile.hEvent, INFINITE); // Ждем завершения асинхронной операции чтения

		GetOverlappedResult(hFile, &OvlFile, &WrittenBytes, FALSE); // Узнаем число считанных байт

		OvlFile.Offset += WrittenBytes; // Увеличиваем смещение в файле
		WrittenBytesTotal += WrittenBytes;
	}
	SetFilePointer(hFile, strlen(FileBuffer), NULL, FILE_BEGIN);
	SetEndOfFile(hFile);
	CloseHandle(hFile);
}

void TestAndDownloadFileBuffer(LPVOID lParam)
{
	if (!FirstInNetwork)
	{
		while (Token_Ring == NULL) // Ожидаем формирования кольца
			Sleep(0);
		while (Token_Ring->next == NULL)
			Sleep(0);
		while (!strcmp(My_IP, Token_Ring->next->IP))
			Sleep(0);		// Ожидаем, пока не будем добавлены в кольцо
		char Msg[30];
		Msg[0] = WANT_TO_GET_FILEBUFFER;
		Msg[1] = '0';
		Msg[2] = '\0';
		strcat(Msg, My_IP);
		Add_To_Msg_List(Msg);
	}
	else
		SetEvent(hEventEndDataDownload);
}