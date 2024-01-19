/*##################################
스레드를 이용한 WinAPI데모(수업용)
파일명: WinAppDemo.cpp
작성자: 김홍규 (downkhg@gmail.com)
마지막수정날짜: 2020.12.14
버전: 1.2 (메세지 주소값 출력 및 문제 단계별화)
###################################*/

#include <iostream>
#include <Windows.h>
#include <process.h>
#include <string>
#include <queue>

using namespace std;

enum WM_MSG { CREATE, COMMAND, PAINT, DESTROY, MAX };
string strMSG[MAX] = { "CREATE","COMMOND","PAINT","DESTROY" };

bool g_bLoop = true;


void TestQueueMain(queue<int>& g_queMsg)
{
	g_queMsg.push(WM_MSG::CREATE);
	g_queMsg.push(WM_MSG::COMMAND);
	g_queMsg.push(WM_MSG::PAINT);
	g_queMsg.push(WM_MSG::DESTROY);
	printf("Queue Count: %d\n", g_queMsg.size());
	while (g_queMsg.empty() == false)
	{
		int nMsg = g_queMsg.front();
		printf("MsgCount[%d]:%s\n", g_queMsg.size(), strMSG[nMsg].c_str());
		g_queMsg.pop();
	}
	printf("Queue Count: %d\n", g_queMsg.size());
}

//arg를 통해 외부의 데이터값을 받을수있다.
unsigned int WINAPI WndProc(void* arg)
{
	cout << "arg:" << arg << endl;
	int* pData = (int*)arg;
	queue<int>* pQueue = (queue<int>*)arg;
	while (g_bLoop)
	{
		int nMsg = 0;
		if (!pQueue->empty())
		{
			nMsg = pQueue->front();
			pQueue->pop();
			//cout << "현재 메시지 수: " << g_queMsg.size() << endl;
		}
		switch (nMsg)
		{
		case CREATE:
			cout << "초기화" << endl;
			pQueue->push(WM_MSG::COMMAND);
			break;
		case COMMAND:
			cout << "명령을 입력하세요." << endl;
			for (int i = 0; i < MAX; i++)
				cout << i << ":" << strMSG[i] << " ";
			cout << endl;
			break;
		case PAINT:
			cout << "화면에 그리기" << endl;
			break;
		case DESTROY:
			cout << "프로그램 종료" << endl;
			g_bLoop = false;
			break;
		default:
			break;
		}
		Sleep(2000);
	}
	return 0;
}

int WinAPIDemoMain()
{
	HANDLE hThread = NULL;
	DWORD dwThreadID = NULL;
	queue<int> g_queMsg;
	int nMSG = CREATE;
	cout << "Msg:" << &nMSG << endl;
	
	hThread = (HANDLE)_beginthreadex(NULL, 0, 
		WndProc, 
		(void*)&g_queMsg, 0, 
		(unsigned int*)&dwThreadID);

	while (g_bLoop)
	{
		scanf_s("%d", &nMSG);

		g_queMsg.push(nMSG);
	}


	CloseHandle(hThread);

	return 0;
}

//입력을 받으면서 화면에 메세지에 필요한 출력을 처리하는 프로그램.
//큐를 활용하여 메세지를 큐에 쌓고, 쓰레드에서 큐에서 데이터를 1개씩꺼내서 처리하는 프로그램으로 만들기.
//1. 큐를 전역변수로 만들어서 처리하기.
//2. 큐를 main의 지역변수로 만들어서 동일한 과정이 가능하도록 처리하기.
int main()
{
	//TestQueueMain();
	WinAPIDemoMain();
}