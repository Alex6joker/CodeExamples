#include <stdlib.h>
#include "TokenRingGlobals.h"
#include "TokenRingStructs.h"
#include "TokenRingFuncs.h"

void Add_To_Msg_List(char* InputMessage)
{
	ResetEvent(hEventWorkWithMessageList[0]); // ��������� ����������� ������� ����� ������, ���� ������ � ���� �� ����� ���������
	struct _Message_List* temp; //��������� �� ����� ���������
	temp = (struct _Message_List*)malloc(sizeof(struct _Message_List)); // �������� ������ ��� ����� ���������
	if (Message_List == NULL)
	{
		temp->next = NULL;
		temp->Msg_Priority = InputMessage[0];
		temp->Msg_Type = InputMessage[1];
		strcpy(temp->Message, InputMessage);
		Message_List = temp;
	}
	else
		if (Message_List->next == NULL) // ������� �������� � ������ ��� �� ����
		{
		Message_List->next = temp; // ���������� ���� ��������� �� �����������
		temp->Msg_Priority = InputMessage[0];
		temp->Msg_Type = InputMessage[1];
		strcpy(temp->Message, InputMessage);
		temp->next = NULL; // ��������� ���� ��������� �� ��������� �������
		if (temp->Msg_Priority > Message_List->Msg_Priority) // ���� ��������� ����������� �������� ���������� ����, �� �������� � � ������
		{
			struct _Message_List* TempForSwop; //��������� �� ����� ���������	
			TempForSwop = Message_List;
			Message_List = temp;
			temp = TempForSwop;
			Message_List->next = temp;
			temp->next = NULL;
		}
		}
		else // ���������� �������� � �������, ��� ��� ��������� ��� ������� 2 ��������
		{
			struct _Message_List *TempForSwop, *p;
			TempForSwop = Message_List;
			BOOL InBeginOfList = TRUE;
			for (; (TempForSwop->next->Msg_Priority >= InputMessage[0]) && (TempForSwop->next->next != NULL); TempForSwop = TempForSwop->next)
				InBeginOfList = FALSE;
			p = TempForSwop->next; // ���������� ��������� �� ��������� ����
			TempForSwop->next = temp; // ���������� ���� ��������� �� �����������
			temp->Msg_Priority = InputMessage[0];
			temp->Msg_Type = InputMessage[1];
			strcpy(temp->Message, InputMessage);
			temp->next = p; // ��������� ���� ��������� �� ��������� �������
			if (temp->Msg_Priority > TempForSwop->Msg_Priority) // ���� ��������� ����������� �������� ���������� ����, �� �������� � � ������
			{
				struct _Message_List* Temp; //��������� �� ����� ���������	
				Temp = TempForSwop;
				TempForSwop = temp;
				temp = Temp;
				Temp = TempForSwop->next;
				TempForSwop->next = temp;
				temp->next = Temp;
			}
			if (InBeginOfList) Message_List = TempForSwop;
		}
	SetEvent(hEventWorkWithMessageList[0]); // �������� �������, ��� ������ ������������ � �� ����� ������������
}

void Take_Package_With_The_Highest_Priority(char* ReturningMsg)
{
	// ��������� ����������� ������� ����� ������, ���� ������ � ���� �� ����� ���������
	ResetEvent(hEventWorkWithMessageList[1]);

	struct _Message_List* temp = NULL; // ��������� ���������
	if (Message_List->next != NULL) temp = Message_List->next; // ������ ������ ��������� �������, ���� ����� ����������
	strcpy(ReturningMsg, Message_List->Message);
	free(Message_List); // ������� ����
	if (temp != NULL) Message_List = temp;
	else Message_List = NULL;

	// �������� �������, ��� ������ ������������ � �� ����� ������������
	SetEvent(hEventWorkWithMessageList[1]);
}