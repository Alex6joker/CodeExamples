#include <stdlib.h>
#include "TokenRingGlobals.h"
#include "TokenRingStructs.h"
#include "TokenRingFuncs.h"

void Add_To_Msg_List(char* InputMessage)
{
	ResetEvent(hEventWorkWithMessageList[0]); // Блокируем возможность маркеру взять данные, пока работа с ними не будет проделана
	struct _Message_List* temp; //Указатель на новую структуру
	temp = (struct _Message_List*)malloc(sizeof(struct _Message_List)); // Выделяем память под новую структуру
	if (Message_List == NULL)
	{
		temp->next = NULL;
		temp->Msg_Priority = InputMessage[0];
		temp->Msg_Type = InputMessage[1];
		strcpy(temp->Message, InputMessage);
		Message_List = temp;
	}
	else
		if (Message_List->next == NULL) // Второго элемента в списке еще не было
		{
		Message_List->next = temp; // предыдущий узел указывает на создаваемый
		temp->Msg_Priority = InputMessage[0];
		temp->Msg_Type = InputMessage[1];
		strcpy(temp->Message, InputMessage);
		temp->next = NULL; // созданный узел указывает на следующий элемент
		if (temp->Msg_Priority > Message_List->Msg_Priority) // Если приоритет добавленной операции однозначно выше, то помещаем её в начало
		{
			struct _Message_List* TempForSwop; //Указатель на новую структуру	
			TempForSwop = Message_List;
			Message_List = temp;
			temp = TempForSwop;
			Message_List->next = temp;
			temp->next = NULL;
		}
		}
		else // Добавление операции в очередь, где уже находится как минимум 2 операции
		{
			struct _Message_List *TempForSwop, *p;
			TempForSwop = Message_List;
			BOOL InBeginOfList = TRUE;
			for (; (TempForSwop->next->Msg_Priority >= InputMessage[0]) && (TempForSwop->next->next != NULL); TempForSwop = TempForSwop->next)
				InBeginOfList = FALSE;
			p = TempForSwop->next; // сохранение указателя на следующий узел
			TempForSwop->next = temp; // предыдущий узел указывает на создаваемый
			temp->Msg_Priority = InputMessage[0];
			temp->Msg_Type = InputMessage[1];
			strcpy(temp->Message, InputMessage);
			temp->next = p; // созданный узел указывает на следующий элемент
			if (temp->Msg_Priority > TempForSwop->Msg_Priority) // Если приоритет добавленной операции однозначно выше, то помещаем её в начало
			{
				struct _Message_List* Temp; //Указатель на новую структуру	
				Temp = TempForSwop;
				TempForSwop = temp;
				temp = Temp;
				Temp = TempForSwop->next;
				TempForSwop->next = temp;
				temp->next = Temp;
			}
			if (InBeginOfList) Message_List = TempForSwop;
		}
	SetEvent(hEventWorkWithMessageList[0]); // Сообщаем маркеру, что данные подготовлены и их можно использовать
}

void Take_Package_With_The_Highest_Priority(char* ReturningMsg)
{
	// Блокируем возможность маркеру взять данные, пока работа с ними не будет проделана
	ResetEvent(hEventWorkWithMessageList[1]);

	struct _Message_List* temp = NULL; // Указатель структуру
	if (Message_List->next != NULL) temp = Message_List->next; // Корнем станет следующий элемент, если такой существует
	strcpy(ReturningMsg, Message_List->Message);
	free(Message_List); // Удаляем узел
	if (temp != NULL) Message_List = temp;
	else Message_List = NULL;

	// Сообщаем маркеру, что данные подготовлены и их можно использовать
	SetEvent(hEventWorkWithMessageList[1]);
}