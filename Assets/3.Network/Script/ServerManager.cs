using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;



public class ServerManager : MonoBehaviour
{
    public static ServerManager instance;

    public Button connectButton;
    public Text messagePrefab;
    public RectTransform textArea;


    public string ipAddress = "127.0.0.1"; // IPv6���� ȣȯ���� ���� String�� �ַ� ����Ѵ�.
    //public byte[] ipAdressArray = { 127, 0, 0, 1 }; // IPv4 �ּҸ� �����ϴ� �迭

    public int port = 8871; //0~65535 => uShort �������� ���ڸ� ����� �� ������, (port�ּҴ� 2����Ʈ�� ��ȣ���� ������ ���)
                            //C#������ int�� ����Ѵ�.
                            // 80������ ��Ʈ�� �ý��ۿ��� ����ϴ� ��Ʈ�̹Ƿ� ������� �ʴ� ���� ����.


    private bool isConnected = false;

    private Thread serverMainThread;

    private List<ClientHandler> clients = new List<ClientHandler>();
    private List<Thread> threads = new List<Thread>();

    public static Queue<string> log = new Queue<string>(); // ��� �����尡 ���� �Ҽ� �ִ�,
    // Data������ Queue�� ����, �����尣�� ����� �� �� �ְ� �Ѵ�.
    // ������ �����尣�� ����� �Ұ����ϴ�. �׷���, Queue�� ����ϸ� �����ϴ�.


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        connectButton.onClick.AddListener(ServerConnectButtonClick);
    }
    public void ServerConnectButtonClick()
    {
        if (false == isConnected)
        {
            serverMainThread = new Thread(ServerThread);
            serverMainThread.IsBackground = true; // ��׶��忡���� ����
            serverMainThread.Start();
            isConnected = true;

        }
        else
        {
            serverMainThread.Abort(); //Abort() : �����带 ��� ����
            isConnected = false;

        }
    }

    //������ ����� �� �������� ������ å������ Input,Output ��Ʈ���� �ʿ���.
    private StreamReader reader;
    private StreamWriter writer;
    private int ClientId = 0;


    private void ServerThread()//��Ƽ������� ������ �Ǿ����
    {
        //���������带 List�� �����Ͽ� 
        // ���� ������ ������ ������ ����� ������./

        //try catch���� ����Ͽ� ����ó���� �غ�����.
        // try,catch : ���ܰ� �߻��� �� �ִ� �ڵ带 try ��Ͽ� �ְ�, ���ܰ� �߻����� �� ó���� �ڵ带 catch ��Ͽ� �ִ´�.
        //try catch���� �뵵: exciption�߻��� �޼����� �������� Ȱ���� �� �ֵ��� �Ѵ�.
        //�� ����� if-else���� ����ϴ�.

        try //��Ͼ��� ������ ���� ��� ����.
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Parse(ipAddress), port);
            // TcpListener : TCP ���������� ����ϴ� ������ ���� �� ����ϴ� Ŭ����
            // IPAddress.Parse(ipAddress) : ���ڿ��� �� IP�ּҸ� IPAddress�� ��ȯ
            // port : ������ ����� ��Ʈ ��ȣ

            tcpListener.Start(); // TCP���� ����
           
            while (true)
            {
                TcpClient client = tcpListener.AcceptTcpClient(); // Ŭ���̾�Ʈ�� ���� ��û�� �޾Ƶ��δ�.
                ClientHandler handler = new ClientHandler();
                handler.Connect(ClientId++, this, client);

                clients.Add(handler);

                Thread clientThread = new Thread(handler.Run);
                clientThread.IsBackground = true;
                clientThread.Start();

                threads.Add(clientThread);

                log.Enqueue($"{handler.id}�� Ŭ���̾�Ʈ �����");

            }

            /*Text logText = Instantiate(messagePrefab, textArea);
            logText.text = "���� ����";*/
            //log.Enqueue("���� ����");

            /*TcpClient tcpClient = tcpListener.AcceptTcpClient(); // Ŭ���̾�Ʈ�� ���� ��û�� �޾Ƶ��δ�.
                                                                 //return�� �ö����� ���. >> ��Ƽ�������� �ʿ��� ����.

            *//*   Text logText2 = Instantiate(messagePrefab, textArea);
               logText2.text = "Ŭ���̾�Ʈ �����";*//*
            log.Enqueue("Ŭ���̾�Ʈ �����");

            reader = new StreamReader(tcpClient.GetStream()); // Ŭ���̾�Ʈ�� ����� ��Ʈ��ũ ��Ʈ���� �����´�.
            writer = new StreamWriter(tcpClient.GetStream()); // Ŭ���̾�Ʈ�� ����� ��Ʈ��ũ ��Ʈ���� �����´�.

            *//*writer.WriteLine("�޼���");
            writer.WriteLine("�޼���");
            writer.WriteLine("�޼���");
            writer.Flush(); // ���ۿ� �ִ� �����͸� ��� ����
            //>> ������ ������ �ѹ��� ���۵�*//*

            writer.AutoFlush = true; // ���۰� �������� �ʾƵ� �ڵ����� ����
                                     // >> ���۰� �������� �ʾƵ� �ڵ����� ���۵Ǳ� ������, writer.Flush();�� ������� �ʾƵ� �ȴ�.

            while (tcpClient.Connected)
            {
                //Ŭ���̾�Ʈ�� ����Ǿ� �ִ� ����
                string readString = reader.ReadLine();
                // Ŭ���̾�Ʈ�κ��� �� ���� ���ڿ��� �д´�.
                if (string.IsNullOrEmpty(readString))
                {
                    // ���� ���ڿ��� ����ִ��� Ȯ��
                    continue;
                }
                writer.WriteLine($"����� �޼���:{readString}"); // Ŭ���̾�Ʈ���� �޼��� ����
                *//*            Text messageText = Instantiate(messagePrefab, textArea);
                            messageText.text = readString*//*
                ;
                log.Enqueue($"Clinet message : {readString}");
            }
            log.Enqueue("Ŭ���̾�Ʈ ���� ����");*/
        }
        catch (ArgumentException e)
        {
            log.Enqueue($"{e} ���� �߻�!");
        }
        catch (NullReferenceException)
        {
            log.Enqueue(" �� ������ ���� �߻�!");
        }
        catch (Exception e) //���ܰ� �߻����� �� ����.
        {
            log.Enqueue($"{e} ���� �߻�!");
        }
        finally
        {
            // try�� ������ ������ �߻��ص� ���� �ǰ�, �ȵǵ� ����Ǵ� ���
            // �߰��� �帧�� ������ �ʰ� ������ ��ü�� ���� �ϴ� ���� �۾��� �� �� �ִ�.
            // �������, ������ ������ ��, ������ �ݴ� �۾��� finally ��Ͽ� �־ ������ ���� �� �ִ�.

            foreach (var thread in threads)
            {
                thread?.Abort();
            }

        }

    }

    public void Disconnect(ClientHandler client)
    {
        clients.Remove(client);
    }
    public void BreadcastToClients(string message)
    {
        log.Enqueue(message);
        foreach (var client in clients)
        {
            client.MessageToClient(message);
        }
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
//Ŭ���̾�Ʈ�� TCP���� ��û�� �� ������ �ش� Ŭ���̾�Ʈ�� �ٵ�� �ִ� ��ü�� �����Ѵ�.
public class ClientHandler
{
    public int id;
    public ServerManager server;
    public TcpClient tcpClient;
    public StreamReader reader;
    public StreamWriter writer;

    public void Connect(int id, ServerManager server, TcpClient tcpClient)
    {
        this.id = id;
        this.server = server;
        this.tcpClient = tcpClient;

        reader = new StreamReader(tcpClient.GetStream());
        writer = new StreamWriter(tcpClient.GetStream());

        writer.AutoFlush = true;
    }
    public void Disconnect()
    {
        writer.Close();
        reader.Close();
        tcpClient.Close();
        server.Disconnect(this);
    }
    public void MessageToClient(string message)
    {
        writer.WriteLine(message);

    }
    public void Run()
    {
        try
        {
            while (tcpClient.Connected)
            {
                string readString = reader.ReadLine();
                if (string.IsNullOrEmpty(readString))
                {
                    continue;
                }

                //�о�� �޼����� ������ �������� ����
                server.BreadcastToClients($"{id}�� Ŭ���̾�Ʈ: {readString}");
            }


        }
        catch (Exception e)
        {
            ServerManager.log.Enqueue($"{id}�� Ŭ�󸮾�Ʈ ���� �߻�:{e.Message}");
        }
        finally
        {
            Disconnect();
        }
    }

}