/*##################################
단일연결리스트(수업용)
파일명: LinkedList_empty.cpp
작성자: 김홍규 (downkhg@gmail.com)
마지막수정날짜: 2019.04.12
버전: 1.10
###################################*/

#include <stdio.h>
#include <stdlib.h> //메모리 동적할당 헤더
#include <crtdbg.h> //메모리 누수 탐지 헤더
//#include  "linkedlistClass.h"

struct CNode {
	int nData;
	CNode* pNext;
};

CNode* CreateNode(CNode* pNode, int data); //노드를 생성하여 리턴한다.
CNode* FindNodeData(CNode* pStart, int data); //해당 데이터를 가진 노드를 찾는다.
CNode* InsertNodeData(CNode* pStart, int data, int insert); //해당 데이터를 가진 노드 뒤에 노드를 추가한다.
void DeleteNodeData(CNode* pStart, int del); //해당데이터를 가진 노드를 삭제한다.
void PrintLinkedList(CNode* pStart); //노드를 순회하며 끝날때까지 출력한다.
void DeleteLinkedList(CNode* pStart); //노드를 순회하며 모든데이터를 삭제한다.
void ReverseLinkedList(CNode* pStart); //
void CircularLinkedList(CNode* pStart);
//연결리스트 동적으로 입력받기.(동적할당 설명용)
void InputAdd();

//정상작동 테스트를 위해서, 다음과 같이 기본적인 절차로 오류를 확인한다.
//이 소스에 몇가지 버그가 존재한다.
//이 코드가 정상작동 된 후 발견해볼것!
//main()함수 내 코드는 추가는 가능하지만 삭제는 하지말것!
void main()
{
	//_CrtSetBreakAlloc(71); //메모리 누수시 번호를 넣으면 할당하는 위치에 브레이크 포인트를 건다.
	//_CrtSetDbgFlag(_CRTDBG_ALLOC_MEM_DF | _CRTDBG_LEAK_CHECK_DF); //메모리 누수 검사 

	CNode* pBegin = NULL;
	CNode* pEnd = NULL;

	//노드 추가 테스트
	pEnd = CreateNode(pEnd, 10);
	pBegin = pEnd; //마지막 노드를 알아야 검색이 가능하므로 저장해둔다.
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

	CNode* pInsert = InsertNodeData(pBegin, 30, 60);//노드 삽입
	if (pInsert != NULL)
		printf("Insert:%d\n", pInsert->nData);

	PrintLinkedList(pBegin);

	DeleteNodeData(pBegin, 60);//노드 삭제

	PrintLinkedList(pBegin);

	DeleteLinkedList(pBegin); //모든노드삭제 - 이 함수를 호출하지않을시 메모리가 누수됨.
}

//여기서 부터 기능을 구현한다.
//기존코드는 손대지말고, 추가만 하여 현 프로그램 정상 작동하도록할것.
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
		// 마지막 노드가 새로운 노드를 가리키고, 새로운 노드는 첫 번째 노드를 가리킴
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
	printf("입력 리스트에서 모든 노드가 삭제 되었습니다.\n");
}

void InputAdd()
{
	CNode* pStart = NULL;
	CNode* pNode = NULL;
	int nData = 0;

	//동적할당을 하면 프로그램이 사용자에 의해서 사용되는 메모리가 결정된다.
	//쉽게말해서, 컴파일단계에서 100개를 만들고 쓴다면, 
	//사용하지않더라도 100개의 메모리를 사용할수밖에없다.
	//그리고, 100개 이상의 메모리도 사용할수없다.
	//그러나, 동적할당을 하면 사용자가 추가한 메모리만큼만 메모리가 사용되고 
	//메모리용량이 허용하는 한 추가가 된다.
	while (nData != -1)
	{
		scanf_s("%d", &nData);
		pNode = CreateNode(pNode, nData);

		if (pNode == NULL)
		{
			printf("더 이상 사용할수 있는 메모리가 없습니다!");
		}

		if (pStart == NULL)
			pStart = pNode;

		PrintLinkedList(pStart);
	}

	DeleteLinkedList(pStart); //모든노드삭제 - 이 함수를 호출하지않을시 메모리가 누수됨.
}