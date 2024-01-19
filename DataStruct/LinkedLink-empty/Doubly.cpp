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

struct DNode {
	int nData;
	DNode* pNext;
	DNode* pPrev;
};

DNode* CreateNode(DNode* pNode, int data); //��带 �����Ͽ� �����Ѵ�.
DNode* FindNodeData(DNode* pStart, int data); //�ش� �����͸� ���� ��带 ã�´�.
DNode* InsertNodeData(DNode* pStart, int data, int insert); //�ش� �����͸� ���� ��� �ڿ� ��带 �߰��Ѵ�.
void DeleteNodeData(DNode* pStart, int del); //�ش絥���͸� ���� ��带 �����Ѵ�.
void PrintLinkedList(DNode* pStart); //��带 ��ȸ�ϸ� ���������� ����Ѵ�.
void DeleteLinkedList(DNode* pStart); //��带 ��ȸ�ϸ� ��絥���͸� �����Ѵ�.
void ReverseLinkedList(DNode* pStart); //

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

	DNode* pBegin = NULL;
	DNode* pEnd = NULL;

	//��� �߰� �׽�Ʈ
	pEnd = CreateNode(pEnd, 10);
	pBegin = pEnd; //������ ��带 �˾ƾ� �˻��� �����ϹǷ� �����صд�.
	//pBegin->pNext = pEnd;
	pEnd = CreateNode(pEnd, 20);
	pEnd = CreateNode(pEnd, 30);
	pEnd = CreateNode(pEnd, 40);
	pEnd = CreateNode(pEnd, 50);

	PrintLinkedList(pBegin);

	DNode* pFind = FindNodeData(pBegin, 40);
	if (pFind != NULL)
		printf("Find:%d\n", pFind->nData);

	DNode* pInsert = InsertNodeData(pBegin, 30, 60);//��� ����
	if (pInsert != NULL)
		printf("Insert:%d\n", pInsert->nData);

	PrintLinkedList(pBegin);

	DeleteNodeData(pBegin, 60);//��� ����

	PrintLinkedList(pBegin);

	DeleteLinkedList(pBegin); //�������� - �� �Լ��� ȣ������������ �޸𸮰� ������.
}

//���⼭ ���� ����� �����Ѵ�.
//�����ڵ�� �մ�������, �߰��� �Ͽ� �� ���α׷� ���� �۵��ϵ����Ұ�.
DNode* CreateNode(DNode* pNode, int data)
{
	//SNode* pTemp = NULL;

	DNode* pTemp = new DNode();
	pTemp->nData = data;
	pTemp->pNext = nullptr;
	pTemp->pPrev = pNode;
	if (pNode != nullptr)
	{
		pNode->pNext = pTemp;
	}
	return  pTemp;
}

DNode* FindNodeData(DNode* pStart, int data)
{
	DNode* pNode = pStart;

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
DNode* InsertNodeData(DNode* pStart, int data, int insert)
{
	DNode* pNode = pStart;
	DNode* pInsert = nullptr;

	pNode = FindNodeData(pStart, data);
	if (pNode != nullptr) 
	{
		pInsert = new DNode();
		pInsert->nData = insert;
		pInsert->pNext = pNode->pNext;
		pInsert->pPrev = pNode;
		if (pNode->pNext != nullptr)
			pNode->pNext->pPrev = pInsert;
		pNode->pNext = pInsert;
	}
	
	return pInsert;
}

void DeleteNodeData(DNode* pStart, int del)
{
	//DNode* pPre = NULL;
	DNode* pNode = pStart;

	while (pNode != NULL && pNode->nData != del)
	{
		//pPre = pNode;
		pNode = pNode->pNext;
	}

	if (pNode != NULL)
	{
		if (pNode->pPrev != NULL)
			pNode->pPrev->pNext = pNode->pNext;
		if (pNode->pNext != NULL)
			pNode->pNext->pPrev = pNode->pPrev;
		delete pNode;
	}

}

void PrintLinkedList(DNode* pStart)
{
	DNode* pNode = pStart;
	printf("data:");
	while (pNode)
	{
		printf("%d", pNode->nData);
		pNode = pNode->pNext;

		if (pNode != NULL)
			printf(" <-> ");
	}
	printf("\n");
}

void DeleteLinkedList(DNode* pStart)
{
	DNode* pNode = pStart;
	DNode* pDel = NULL;
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
	DNode* pStart = NULL;
	DNode* pNode = NULL;
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