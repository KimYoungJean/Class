using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class ClientManager : MonoBehaviour
{
    public Button connectButton;
    public Text messagePrefab;
    public RectTransform textArea;

    public InputField ip;
    public InputField port;

    public InputField messageInput;
    public Button sendButton;

    private bool isConnected = false;
    private Thread clientThread;

    public static Queue<string> log = new Queue<string>();

    private StreamReader reader;
    private StreamWriter writer;

    public void Awake()
    {
        /*connectButton.onClick.AddListener(ConnectButtonClick);
        sendButton.onClick.AddListener(() => MessageToServer(messageInput.text));*/
    }
    public void ConnectButtonClick()
    {
        if (false == isConnected)
        {
            clientThread = new Thread(ClientThread);
            clientThread.IsBackground = true;
            clientThread.Start();
            isConnected = true;

        }
        else
        {
            clientThread.Abort();
            isConnected = false;

        }
    }

    private void ClientThread()
    {
        TcpClient tcpClient = new TcpClient();

        IPAddress serverAddress = IPAddress.Parse(ip.text);
        int portNum = int.Parse(port.text);

        IPEndPoint endPoint = new IPEndPoint(serverAddress, portNum);

        tcpClient.Connect(endPoint); // ������ ���� 

        log.Enqueue($"������ ���� ���� : IP:{endPoint.Address},{endPoint}");

        reader = new StreamReader(tcpClient.GetStream());
        writer = new StreamWriter(tcpClient.GetStream());

        writer.AutoFlush = true;

        while (tcpClient.Connected)
        {
            string readString = reader.ReadLine();
            log.Enqueue($"[����]{readString}");


        }

        log.Enqueue("������ ������ ���������ϴ�.");
    }

    public void MessageToServer(string message)
    {
        //inputField�� OnSubmit�� ����
        writer.WriteLine(message);
        messageInput.text = ""; // �޼��� ���� �� �Է�â �ʱ�ȭ
    }

    private void Update()
    {
        if (log.Count > 0)
        {
            Text newText = Instantiate(messagePrefab, textArea);
            newText.text = log.Dequeue();
        }
    }
}

