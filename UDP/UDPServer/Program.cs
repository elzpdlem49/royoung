using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class UdpChatServer
{
    private static readonly int port = 5000;
    private static readonly Dictionary<EndPoint, string> clients = new Dictionary<EndPoint, string>();

    public static void Main(string[] args)
    {
        // 소켓 생성
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        // 포트 바인딩
        socket.Bind(new IPEndPoint(IPAddress.Any, port));

        Console.WriteLine("서버 시작");

        // 데이터 수신 대기
        while (true)
        {
            EndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
            byte[] data = new byte[1024];

            int recv = socket.ReceiveFrom(data, ref remoteEP);

            // 수신된 메시지 처리
            string message = Encoding.UTF8.GetString(data, 0, recv);
            string[] splitMessage = message.Split(' ');
            string command = splitMessage[0];
            string name = splitMessage[1];
            string content = message.Substring(command.Length + name.Length + 2);

            switch (command)
            {
                case "JOIN":
                    // 클라이언트 추가
                    clients.Add(remoteEP, name);
                    Console.WriteLine("{0}님이 접속했습니다.", name);
                    BroadcastMessage($"{name}님이 접속했습니다.");
                    break;
                case "LEAVE":
                    // 클라이언트 제거
                    clients.Remove(remoteEP);
                    Console.WriteLine("{0}님이 퇴장했습니다.", name);
                    BroadcastMessage($"{name}님이 퇴장했습니다.");
                    break;
                case "MESSAGE":
                    // 메시지 전송
                    Console.WriteLine("{0}: {1}", name, content);
                    BroadcastMessage($"{name}: {content}");
                    break;
            }
        }

        // 소켓 닫기
        socket.Close();
    }

    private static void BroadcastMessage(string message)
    {
        // 모든 클라이언트에게 메시지 전송
        byte[] data = Encoding.UTF8.GetBytes(message);
        foreach (EndPoint client in clients.Keys)
        {
            socket.SendTo(data, data.Length, SocketFlags.None, client);
        }
    }

}
