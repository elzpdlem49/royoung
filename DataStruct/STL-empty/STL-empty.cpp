/*##################################
STL(자료구조 수업용)
파일명: STL_empty.cpp
작성자 : 김홍규(downkhg@gmail.com)
마지막수정날짜 : 2022.03.09
버전 : 1.05
###################################*/
#include <iostream>
#include <vector>
#include <list>
#include <deque>
#include <queue>
#include <stack>
#include <map>
#include <set>
#include <string>
#include <unordered_map>//hash_map -> unordered_map: vs2019부터 변경
using namespace std;
//벡터: 동적배열
//0.배열은 데이터가 저장될공간이 미리 확보되어있다.
//1.인덱스로 원소접근이 가능하다.
//2.각 자료는 포인터연산(인덱스)을 통한 순차/랜덤접근이 가능하다.
//3.배열의 크기를 런타임중에 변경가능하다.
void VectorMain()
{
	vector<int> container(1);//컨테이너생성시 크기를 지정가능하다.
	container[0] = 10;
	cout << "Print:";
	for (int i = 0; i < container.size(); i++)
		cout << "[" << i << "]" << container[i] << ",";
	cout << endl;
	container.resize(3); //배열의 크기를 지정한다.
	cout << "Print:";
	for (int i = 0; i < container.size(); i++)
		cout << "[" << i << "]" << container[i] << ",";
	cout << endl;
	//1.추가 2.삽입 3.삭제 4.모두삭제
	//1. 추가
	container.push_back(20);
	//2. 삽입: 10뒤에 30추가
	vector<int>::iterator insert = container.begin() +1;
	cout << "insert:" << *insert << endl;
	container.insert(insert, 30);

	vector<int>::iterator moveN = container.begin() +4;
	cout << "MoveN:" << *moveN << endl;
	vector<int>::iterator movePre = container.end() -1;
	cout << "movePre:" << *movePre << endl;
	vector<int>::iterator it;
	cout << "PrintPtr:";
	for (it = container.begin(); it != container.end(); it++)
		cout << "[" << &*it << "]" << *it << ",";
	cout << endl;
	container.clear(); //모두삭제
	cout << "Clear:";
	for (it = container.begin(); it != container.end(); it++)
		cout << "[" << &*it << "]" << *it << ",";
	cout << endl;
}
//연결리스트
//1.데이터는 순차접근만 가능하다.(랜덤x)
//2.연결리스트에 추가,삽입,삭제는 O(1)이다.
//3.연결리스트의 종류: 단일, 환형, 이중 stl의 리스트는 어디에 해당되는가?
void ListMain()
{
	list<int> container(1);//컨테이너생성시 크기를 지정가능하다.
	*container.begin() = 10;
	list<int>::iterator it;
	cout << "Print:";
	for (it = container.begin(); it != container.end(); it++)
		cout << *it << ",";
	cout << endl;
	container.resize(3); //배열의 크기를 지정한다.
	cout << "Print:";
	for (it = container.begin(); it != container.end(); it++)
		cout << *it << ",";
	cout << endl;
	//1.추가 2.삽입 3.삭제 4.모두삭제
	//1. 추가
	container.push_back(20);
	//2. 삽입: 10뒤에 30추가
	list<int>::iterator insert = container.begin()++;
	cout << "insert:" << *insert << endl;
	container.insert(insert, 30);

	list<int>::iterator moveN = ++ ++ ++container.begin();
	cout << "MoveN:" << *moveN << endl;
	list<int>::iterator movePre = --container.end();
	cout << "movePre:" << *movePre << endl;
	//list<int>::iterator it;
	cout << "PrintPtr:";
	for (it = container.begin(); it != container.end(); it++)
		cout << "[" << &*it << "]" << *it << ",";
	cout << endl;
	container.clear(); //모두삭제
	cout << "Clear:";
	for (it = container.begin(); it != container.end(); it++)
		cout << "[" << &*it << "]" << *it << ",";
	cout << endl;
}
//데크: 앞뒤로 자료를 추가/삭제가능, 랜덤접근가능.
void DequeMain()
{
	deque<int> container(1);//컨테이너생성시 크기를 지정가능하다.
	container[0] = 10;
	cout << "Print:";
	for (int i = 0; i < container.size(); i++)
		cout << "[" << i << "]" << container[i] << ",";
	cout << endl;
	container.resize(3); //배열의 크기를 지정한다.
	cout << "Print:";
	for (int i = 0; i < container.size(); i++)
		cout << "[" << i << "]" << container[i] << ",";
	cout << endl;
	//1.추가 2.삽입 3.삭제 4.모두삭제
	//1. 추가
	container.push_back(20);
	//2. 삽입: 10뒤에 30추가
	deque<int>::iterator insert = container.begin();
	cout << "insert:" << *insert << endl;
	container.insert(insert, 30);

	deque<int>::iterator moveN = container.begin() + 4;
	cout << "MoveN:" << *moveN << endl;
	deque<int>::iterator movePre = container.end() - 1;
	cout << "movePre:" << *movePre << endl;
	deque<int>::iterator it;
	cout << "PrintPtr:";
	for (it = container.begin(); it != container.end(); it++)
		cout << "[" << &*it << "]" << *it << ",";
	cout << endl;
	container.clear(); //모두삭제
	cout << "Clear:";
	for (it = container.begin(); it != container.end(); it++)
		cout << "[" << &*it << "]" << *it << ",";
	cout << endl;
}
//스택: 뒤에서 추가되고 뒤에서 꺼냄.
//재귀함수에서 이전 함수를 호출할때마다 스택에 쌓임.
//문자열뒤집기 -> 문자배열 -> apple -> elppa
void StackMain()
{
	stack<int> container; // stack 객체 생성

	container.push(10); // 스택에 데이터 추가
	cout << "Print:";
	while (!container.empty()) // 스택이 비어있지 않은 동안 반복
	{
		cout << "[" << container.top() << "]"; // 스택의 맨 위 원소 출력
		container.pop(); // 스택의 맨 위 원소 제거
	}
	cout << endl;

	// 위에서 스택이 비어져서 다시 데이터를 추가
	container.push(10);

	// 스택의 크기를 3으로 조절하려면 직접 구현이 필요
	// stack은 동적으로 크기를 조절하는 것이 불가능함

	// 데이터 추가
	container.push(20);

	// 데이터 삽입은 스택 맨 위에만 가능하므로 insert 사용 불가

	// 스택의 상태 출력
	cout << "Print:";
	while (!container.empty())
	{
		cout << "[" << container.top() << "]";
		container.pop();
	}
	cout << endl;

	// 스택 내의 모든 데이터 삭제 (clear 함수가 없으므로 pop을 이용)
	while (!container.empty())
	{
		container.pop();
	}
	cout << "Clear:";
	while (!container.empty())
	{
		cout << "[" << container.top() << "]";
		container.pop();
	}
	cout << endl;
}
//큐: 뒤에서 추가하고 앞에서 꺼냄.
//메세지큐: 이벤트가 발생한 순서대로 저장하는 공간.
//입력된 순서대로 명령어 처리하기
void QueueMain()
{
	queue<int> container;//컨테이너생성시 크기를 지정가능하다.

	//1.추가 2.삽입 3.삭제 4.모두삭제
	//1. 추가
	container.push(10);
	cout << "Size: " << container.size() << endl;
	cout << "Front: " << container.front() << endl;

	//2. 삽입
	container.push(20);
	cout << "Size: " << container.size() << endl;
	cout << "Front: " << container.front() << endl;

	//3. 삭제
	container.pop();
	cout << "Size: " << container.size() << endl;
	cout << "Front: " << container.front() << endl;

	//4. 모두삭제
	while (!container.empty())
	{
		container.pop();
	}
	cout << "Clear:";
	cout << endl;
}
//우선순위큐: 우선순위가 높은 원소가 먼저나감(힙)
//무작위로 데이터를 넣었을때 어떤 순서대로 데이터가 나오는가? 큰값부터 나온다.
void PriorytyQueueMain()
{
	priority_queue<int> container;//컨테이너생성시 크기를 지정가능하다.

	//1.추가 2.삽입 3.삭제 4.모두삭제
	//1. 추가
	container.push(10);
	container.push(30);
	cout << "Size: " << container.size() << endl;
	cout << "Top: " << container.top() << endl;

	//2. 삽입
	container.push(20);
	cout << "Size: " << container.size() << endl;
	cout << "Top: " << container.top() << endl;

	//3. 삭제
	container.pop();
	cout << "Size: " << container.size() << endl;
	cout << "Top: " << container.top() << endl;

	//4. 모두삭제
	while (!container.empty())
	{
		container.pop();
	}
	cout << "Clear:";
	cout << endl;
}
//맵: 사전식으로 데이터를 찾을수있다.
//해당영어단어를 넣으면 한국어 결과가 나온다.
void MapMain()
{
	map<string, string> mapDic;

	mapDic["test"] = "시험";
	mapDic["pratice"] = "연습";
	mapDic["try"] = "도전";
	mapDic["note"] = "기록";

	cout << mapDic["try"] << endl;
	cout << mapDic["note"] << endl;
}
//셋: 순서없이 데이터를 넣는다. 데이터는 순서와 상관없이 데이터를 찾는다.
void SetMain()
{
	set<int> setData;

	setData.insert(10);
	setData.insert(20);
	setData.insert(30);
	setData.insert(40);

	set<int>::iterator it = setData.find(10);

	if (it != setData.end()) it;
	for (it = setData.begin(); it != setData.end(); it++)
		cout << *it << ",";
	cout << endl;
}
//해시맵: 해시테이블
void HashMapMain()
{
	unordered_map<string, string> mapDic;

	mapDic["test"] = "시험";
	mapDic["pratice"] = "연습";
	mapDic["try"] = "도전";
	mapDic["note"] = "기록";

	cout << mapDic["try"] << endl;
	cout << mapDic["note"] << endl;
}
void main()
{
	//VectorMain();
	//ListMain();
	//DequeMain();
	//StackMain();
	//QueueMain();
	PriorytyQueueMain();
	MapMain();
	SetMain();
	HashMapMain();
}