#include "stdafx.h"
#include <stdio.h>
#include <windows.h>
#include "iprtrmib.h"
#include "Iphlpapi.h"
#include <stdlib.h>
#include "TokenRingDefines.h"

char** GetArpTable(int* num_of_IPs)
{
	printf("\n\n\n");

	PMIB_IPNETTABLE pMib = (PMIB_IPNETTABLE)malloc(sizeof(MIB_IPNETTABLE) + sizeof(MIB_IPNETROW)*(*num_of_IPs));

	DWORD dwRet = GetIpNetTable(pMib, (PULONG)&num_of_IPs, TRUE);

	if (dwRet == ERROR_NO_DATA || dwRet == ERROR_INVALID_PARAMETER || dwRet == ERROR_INSUFFICIENT_BUFFER || dwRet == ERROR_NOT_SUPPORTED)
		return{ NULL };

	if (*num_of_IPs>ARP_TABLE_SIZE)
	{
		printf("[Warning] Insufficient Memory(allocated %d needed %d)\n", ARP_TABLE_SIZE, num_of_IPs);
		*num_of_IPs = ARP_TABLE_SIZE;
	}
	else
	{
		*num_of_IPs = (unsigned long)pMib->dwNumEntries;
	}

	char **IPs = new char*[*num_of_IPs]; // Выделяем место под все записи
	printf("ARP Table ( %d Entries) \n", *num_of_IPs);
	printf("--------------------------------------------------------\n");
	printf("Internet Address      Physical Address         Type\n");

	for (int i = 0; i<*num_of_IPs; i++)
	{
		char ipaddr[20], macaddr[20];

		sprintf(ipaddr, "%d.%d.%d.%d",
			(pMib->table[i].dwAddr & 0x0000ff), ((pMib->table[i].dwAddr & 0xff00) >> 8),
			((pMib->table[i].dwAddr & 0xff0000) >> 16), (pMib->table[i].dwAddr >> 24)
			);

		sprintf(macaddr, "\t%02x-%02x-%02x-%02x-%02x-%02x",
			pMib->table[i].bPhysAddr[0], pMib->table[i].bPhysAddr[1],
			pMib->table[i].bPhysAddr[2], pMib->table[i].bPhysAddr[3],
			pMib->table[i].bPhysAddr[4], pMib->table[i].bPhysAddr[5]
			);

		printf("%-20s  %-25s", ipaddr, macaddr);
		IPs[i] = new char[20];
		strcpy(IPs[i], ipaddr);

		if (pMib->table[i].dwType == 3) printf("\tDynamic\n");
		else if (pMib->table[i].dwType == 4) printf("\tStatic\n");
	}
	return IPs;
}
