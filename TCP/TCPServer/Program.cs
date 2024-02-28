using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TcpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // 클라이언트 소켓 생성
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // 서버 IP 주소 및 포트 설정
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);

            // 서버에 연결
            clientSocket.Connect(endPoint);

            // 서버에게 데이터 전송
            string message = "클라이언트로부터 보내는 메시지";
            byte[] data = Encoding.UTF8.GetBytes(message);
            clientSocket.Send(data);

            // 서버로부터 데이터 수신
            byte[] receivedData = new byte[1024];
            int receivedBytes = clientSocket.Receive(receivedData);

            // 수신 데이터 문자열 변환 및 출력
            string response = Encoding.UTF8.GetString(receivedData, 0, receivedBytes);
            Console.WriteLine("[서버] " + response);

            // 클라이언트 연결 종료
            clientSocket.Close();
        }
    }
}
