/*##################################
���Ͽ��Ḯ��Ʈ(������)
���ϸ�: LinkedList_empty.cpp
�ۼ���: ��ȫ�� (downkhg@gmail.com)
������������¥: 2019.04.12
����: 1.10
###################################*/

#include <stdio.h>
#include <stdlib.h> //�޸� �����Ҵ� ���
#include <crtdbg.h> //�޸� ���� Ž�� ���
//#include  "linkedlistClass.h"

struct CNode {
	int nData;
	CNode* pNext;
};

CNode* CreateNode(CNode* pNode, int data); //��带 �����Ͽ� �����Ѵ�.
CNode* FindNodeData(CNode* pStart, int data); //�ش� �����͸� ���� ��带 ã�´�.
CNode* InsertNodeData(CNode* pStart, int data, int insert); //�ش� �����͸� ���� ��� �ڿ� ��带 �߰��Ѵ�.
void DeleteNodeData(CNode* pStart, int del); //�ش絥���͸� ���� ��带 �����Ѵ�.
void PrintLinkedList(CNode* pStart); //��带 ��ȸ�ϸ� ���������� ����Ѵ�.
void DeleteLinkedList(CNode* pStart); //��带 ��ȸ�ϸ� ��絥���͸� �����Ѵ�.
void ReverseLinkedList(CNode* pStart); //
void CircularLinkedList(CNode* pStart);
//���Ḯ��Ʈ �������� �Է¹ޱ�.(�����Ҵ� �����)
void InputAdd();

//�����۵� �׽�Ʈ�� ���ؼ�, ������ ���� �⺻���� ������ ������ Ȯ���Ѵ�.
//�� �ҽ��� ��� ���װ� �����Ѵ�.
//�� �ڵ尡 �����۵� �� �� �߰��غ���!
//main()�Լ� �� �ڵ�� �߰��� ���������� ������ ��������!
void main()
{
	//_CrtSetBreakAlloc(71); //�޸� ������ ��ȣ�� ������ �Ҵ��ϴ� ��ġ�� �극��ũ ����Ʈ�� �Ǵ�.
	//_CrtSetDbgFlag(_CRTDBG_ALLOC_MEM_DF | _CRTDBG_LEAK_CHECK_DF); //�޸� ���� �˻� 

	CNode* pBegin = NULL;
	CNode* pEnd = NULL;

	//��� �߰� �׽�Ʈ
	pEnd = CreateNode(pEnd, 10);
	pBegin = pEnd; //������ ��带 �˾ƾ� �˻��� �����ϹǷ� �����صд�.
	//pBegin->pNext = pEnd;
	pEnd = CreateNode(pEnd, 20);
	pEnd = CreateNode(pEnd, 30);
	pEnd = CreateNode(pEnd, 40);
	pEnd = CreateNode(pEnd, 50);

	//CircularLinkedList(pBegin);

	PrintLinkedList(pBegin);

	CNode* pFind = FindNodeData(pBegin, 40);
	if (pFind != NULL)
		printf("Find:%d\n", pFind->nData);

	CNode* pInsert = InsertNodeData(pBegin, 30, 60);//��� ����
	if (pInsert != NULL)
		printf("Insert:%d\n", pInsert->nData);

	PrintLinkedList(pBegin);

	DeleteNodeData(pBegin, 60);//��� ����

	PrintLinkedList(pBegin);

	DeleteLinkedList(pBegin); //�������� - �� �Լ��� ȣ������������ �޸𸮰� ������.
}

//���⼭ ���� ����� �����Ѵ�.
//�����ڵ�� �մ�������, �߰��� �Ͽ� �� ���α׷� ���� �۵��ϵ����Ұ�.
CNode* CreateNode(CNode* pNode, int data)
{
	CNode* pTemp = new CNode();
	pTemp->nData = data;

	if (pNode == NULL)
	{
		pTemp->pNext = pTemp;
	}
	else
	{
		// ������ ��尡 ���ο� ��带 ����Ű��, ���ο� ���� ù ��° ��带 ����Ŵ
		pTemp->pNext = pNode->pNext;
		pNode->pNext = pTemp;
	}
	return  pTemp;
}

CNode* FindNodeData(CNode* pStart, int data)
{
	CNode* pNode = pStart;

	while (pNode != NULL && pNode->nData != data)
	{
		pNode = pNode->pNext;
	}
	return pNode;
}

//SNode* InsertNodeData(SNode* pStart, int data, int insert)
//{
//	SNode* pNode = pStart;
//	SNode* pInsert = CreateNode(NULL, insert);
//
//	pNode = FindNodeData(pStart, data);
//	if (pNode != NULL)
//	{
//		pInsert->pNext = pNode->pNext;
//		pNode->pNext = pInsert;
//	}
//	return pStart;
//}
CNode* InsertNodeData(CNode* pStart, int data, int insert)
{
	CNode* pNode = pStart;
	CNode* pInsert = NULL;

	pNode = FindNodeData(pStart, data);

	pInsert = new CNode();
	pInsert->nData = insert;
	pInsert->pNext = pNode->pNext;
	pNode->pNext = pInsert;
	return pInsert;
}

void DeleteNodeData(CNode* pStart, int del)
{
	CNode* pPre = NULL;
	CNode* pNode = pStart;

	while (pNode != NULL && pNode->nData != del)
	{
		pPre = pNode;
		pNode = pNode->pNext;
	}

	if (pNode != NULL)
	{
		if (pPre != NULL)
			pPre->pNext = pNode->pNext;
		delete pNode;
	}

}

//void CircularLinkedList(CNode* pStart)
//{
//	CNode* pNode = pStart;
//	while (pNode->pNext != NULL && pNode->pNext != pStart)
//	{
//		pNode = pNode->pNext;
//	}
//	pNode->pNext = pStart;
//}

void PrintLinkedList(CNode* pStart)
{
	CNode* pNode = pStart;
	printf("data:");
	while (pNode)
	{
		printf("%d", pNode->nData);
		pNode = pNode->pNext;

		if (pNode != NULL)
			printf(",");
	}
	printf("\n");
}

void DeleteLinkedList(CNode* pStart)
{
	CNode* pNode = pStart;
	CNode* pDel = NULL;
	while (pNode != NULL)
	{
		pDel = pNode;
		pNode = pNode->pNext;
		delete pDel;
	}
	printf("�Է� ����Ʈ���� ��� ��尡 ���� �Ǿ����ϴ�.\n");
}

void InputAdd()
{
	CNode* pStart = NULL;
	CNode* pNode = NULL;
	int nData = 0;

	//�����Ҵ��� �ϸ� ���α׷��� ����ڿ� ���ؼ� ���Ǵ� �޸𸮰� �����ȴ�.
	//���Ը��ؼ�, �����ϴܰ迡�� 100���� ����� ���ٸ�, 
	//��������ʴ��� 100���� �޸𸮸� ����Ҽ��ۿ�����.
	//�׸���, 100�� �̻��� �޸𸮵� ����Ҽ�����.
	//�׷���, �����Ҵ��� �ϸ� ����ڰ� �߰��� �޸𸮸�ŭ�� �޸𸮰� ���ǰ� 
	//�޸𸮿뷮�� ����ϴ� �� �߰��� �ȴ�.
	while (nData != -1)
	{
		scanf_s("%d", &nData);
		pNode = CreateNode(pNode, nData);

		if (pNode == NULL)
		{
			printf("�� �̻� ����Ҽ� �ִ� �޸𸮰� �����ϴ�!");
		}

		if (pStart == NULL)
			pStart = pNode;

		PrintLinkedList(pStart);
	}

	DeleteLinkedList(pStart); //�������� - �� �Լ��� ȣ������������ �޸𸮰� ������.
}