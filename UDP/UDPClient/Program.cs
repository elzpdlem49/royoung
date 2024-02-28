using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class UdpChatClient
{
    private static readonly int port = 5000;
    private static readonly string serverAddress = "127.0.0.1";
    private static readonly string name = "유저";

    public static void Main(string[] args)
    {
        // 소켓 생성
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        // 서버 주소 및 포트 지정
        EndPoint serverEP = new IPEndPoint(IPAddress.Parse(serverAddress), port);

        // 접속 메시지 전송
        string message = $"JOIN {name}";
        byte[] data = Encoding.UTF8.GetBytes(message);
        socket.SendTo(data, data.Length, SocketFlags.None, serverEP);

        // 메시지 수신 및 송신 대기
        while (true)
        {
            // 메시지 입력
            Console.Write($"{name}: ");
            string content = Console.ReadLine();

            // 메시지 전송
            message = $"MESSAGE {name} {content}";
            data = Encoding.UTF8.GetBytes(message);
            socket.SendTo(data, data.Length, SocketFlags.None, serverEP);

            // 서버로부터 응답 수신
            EndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
            data = new byte[1024];

            int recv = socket.ReceiveFrom(data, ref remoteEP);

            // 수신된 메시지 출력
            Console.WriteLine(Encoding.UTF8.GetString(data, 0, recv));
        }

        // 소켓 닫기
        socket.Close();
    }
}
