using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;


public class ServerManagerForMulti : MonoBehaviour
{

    public Button connectButton;
    public Text messagePrefab;
    public RectTransform textArea;

    public string ipAddress = "127.0.0.2";
    public int port = 8872;

    private bool isConnected = false;

    private List<Thread> serverThreads = new List<Thread>(); // List to manage server threads
    private Queue<string> log = new Queue<string>();

    private void Awake()
    {


        connectButton.onClick.AddListener(ServerConnectButtonClick);
    }

    public void ServerConnectButtonClick()
    {
        if (false == isConnected)
        {
            Thread serverThread = new Thread(ServerThread);
            serverThread.IsBackground = true;
            serverThread.Start();
            serverThreads.Add(serverThread); // Add the server thread to the list
            isConnected = true;
        }
        else
        {
            foreach (Thread serverThread in serverThreads)
            {
                serverThread.Abort(); // Abort all server threads
            }
            serverThreads.Clear(); // Clear the server threads list
            isConnected = false;
        }
    }

    private void ServerThread()
    {

        TcpListener tcpListener = new TcpListener(IPAddress.Parse(ipAddress), port);
        tcpListener.Start();
        log.Enqueue("서버 시작");

        while (true) // Run the server thread indefinitely
        {
            TcpClient tcpClient = tcpListener.AcceptTcpClient();
            log.Enqueue($"클라이언트 연결됨 총 클라이언트 수: {serverThreads.Count}");
          

            Thread clientThread = new Thread(() => HandleClient(tcpClient));
            clientThread.IsBackground = true;
            clientThread.Start();
        }
    }

    private void HandleClient(TcpClient tcpClient)
    {
        using (StreamReader reader = new StreamReader(tcpClient.GetStream()))
        using (StreamWriter writer = new StreamWriter(tcpClient.GetStream()))
        {
            writer.AutoFlush = true;

            while (tcpClient.Connected)
            {
                string readString = reader.ReadLine();
                if (string.IsNullOrEmpty(readString))
                {
                    continue;
                }
                writer.WriteLine($"당신의 메세지: {readString}");
                log.Enqueue($"Client message: {readString}");
            }
        }

        log.Enqueue("클라이언트 연결 종료");
    }

    private void Update()
    {
        while (log.Count > 0)
        {
            Text messageText = Instantiate(messagePrefab, textArea);
            messageText.text = log.Dequeue();
        }
    }
}
