using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TcpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            // 서버 소켓 생성
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // IP 주소 및 포트 설정
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 5000);

            // 서버 소켓 바인딩
            serverSocket.Bind(endPoint);

            // 서버 소켓 수신 대기
            serverSocket.Listen(10);

            Console.WriteLine("서버 실행 중...");

            // 클라이언트 연결 수락
            while (true)
            {
                Socket clientSocket = serverSocket.Accept();

                // 클라이언트로부터 데이터 수신
                byte[] data = new byte[1024];
                int receivedBytes = clientSocket.Receive(data);

                // 수신 데이터 문자열 변환 및 출력
                string message = Encoding.UTF8.GetString(data, 0, receivedBytes);
                Console.WriteLine("[클라이언트] " + message);

                // 클라이언트에게 데이터 전송
                string response = "서버로부터 받은 메시지: " + message;
                byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                clientSocket.Send(responseBytes);

                // 클라이언트 연결 종료
                clientSocket.Close();
            }
        }
    }
}
