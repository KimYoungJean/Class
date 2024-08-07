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


    public string ipAddress = "127.0.0.1"; // IPv6와의 호환성을 위해 String을 주로 사용한다.
    //public byte[] ipAdressArray = { 127, 0, 0, 1 }; // IPv4 주소를 저장하는 배열

    public int port = 8871; //0~65535 => uShort 사이즈의 숫자만 취급할 수 있으나, (port주소는 2바이트의 부호없는 정수를 사용)
                            //C#에서는 int로 사용한다.
                            // 80이전의 포트는 시스템에서 사용하는 포트이므로 사용하지 않는 것이 좋다.


    private bool isConnected = false;

    private Thread serverMainThread;

    private List<ClientHandler> clients = new List<ClientHandler>();
    private List<Thread> threads = new List<Thread>();

    public static Queue<string> log = new Queue<string>(); // 모든 스레드가 접근 할수 있는,
    // Data영역의 Queue를 만들어서, 스레드간의 통신을 할 수 있게 한다.
    // 원래는 쓰레드간의 통신은 불가능하다. 그러나, Queue를 사용하면 가능하다.


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
            serverMainThread.IsBackground = true; // 백그라운드에서도 실행
            serverMainThread.Start();
            isConnected = true;

        }
        else
        {
            serverMainThread.Abort(); //Abort() : 스레드를 즉시 종료
            isConnected = false;

        }
    }

    //데이터 입출력 등 데이터의 전송을 책임지는 Input,Output 스트림이 필요함.
    private StreamReader reader;
    private StreamWriter writer;
    private int ClientId = 0;


    private void ServerThread()//멀티스레드로 생성이 되어야함
    {
        //서버스레드를 List로 관리하여 
        // 다중 연결이 가능한 서버로 만들어 보세요./

        //try catch문을 사용하여 예외처리를 해보세요.
        // try,catch : 예외가 발생할 수 있는 코드를 try 블록에 넣고, 예외가 발생했을 때 처리할 코드를 catch 블록에 넣는다.
        //try catch문의 용도: exciption발생시 메세지를 수동으로 활용할 수 있도록 한다.
        //잘 제어된 if-else문과 비슷하다.

        try //블록안의 에러가 없을 경우 실행.
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Parse(ipAddress), port);
            // TcpListener : TCP 프로토콜을 사용하는 서버를 만들 때 사용하는 클래스
            // IPAddress.Parse(ipAddress) : 문자열로 된 IP주소를 IPAddress로 변환
            // port : 서버가 사용할 포트 번호

            tcpListener.Start(); // TCP서버 시작
           
            while (true)
            {
                TcpClient client = tcpListener.AcceptTcpClient(); // 클라이언트의 연결 요청을 받아들인다.
                ClientHandler handler = new ClientHandler();
                handler.Connect(ClientId++, this, client);

                clients.Add(handler);

                Thread clientThread = new Thread(handler.Run);
                clientThread.IsBackground = true;
                clientThread.Start();

                threads.Add(clientThread);

                log.Enqueue($"{handler.id}번 클라이언트 연결됨");

            }

            /*Text logText = Instantiate(messagePrefab, textArea);
            logText.text = "서버 시작";*/
            //log.Enqueue("서버 시작");

            /*TcpClient tcpClient = tcpListener.AcceptTcpClient(); // 클라이언트의 연결 요청을 받아들인다.
                                                                 //return이 올때까지 대기. >> 멀티쓰레딩이 필요한 이유.

            *//*   Text logText2 = Instantiate(messagePrefab, textArea);
               logText2.text = "클라이언트 연결됨";*//*
            log.Enqueue("클라이언트 연결됨");

            reader = new StreamReader(tcpClient.GetStream()); // 클라이언트와 연결된 네트워크 스트림을 가져온다.
            writer = new StreamWriter(tcpClient.GetStream()); // 클라이언트와 연결된 네트워크 스트림을 가져온다.

            *//*writer.WriteLine("메세지");
            writer.WriteLine("메세지");
            writer.WriteLine("메세지");
            writer.Flush(); // 버퍼에 있는 데이터를 즉시 전송
            //>> 세개의 라인이 한번에 전송됨*//*

            writer.AutoFlush = true; // 버퍼가 가득차지 않아도 자동으로 전송
                                     // >> 버퍼가 가득차지 않아도 자동으로 전송되기 때문에, writer.Flush();를 사용하지 않아도 된다.

            while (tcpClient.Connected)
            {
                //클라이언트가 연결되어 있는 동안
                string readString = reader.ReadLine();
                // 클라이언트로부터 한 줄의 문자열을 읽는다.
                if (string.IsNullOrEmpty(readString))
                {
                    // 읽은 문자열이 비어있는지 확인
                    continue;
                }
                writer.WriteLine($"당신의 메세지:{readString}"); // 클라이언트에게 메세지 전송
                *//*            Text messageText = Instantiate(messagePrefab, textArea);
                            messageText.text = readString*//*
                ;
                log.Enqueue($"Clinet message : {readString}");
            }
            log.Enqueue("클라이언트 연결 종료");*/
        }
        catch (ArgumentException e)
        {
            log.Enqueue($"{e} 에러 발생!");
        }
        catch (NullReferenceException)
        {
            log.Enqueue(" 널 포인터 에러 발생!");
        }
        catch (Exception e) //예외가 발생했을 때 실행.
        {
            log.Enqueue($"{e} 에러 발생!");
        }
        finally
        {
            // try문 내에서 에러가 발생해도 실행 되고, 안되도 실행되는 블록
            // 중간에 흐름이 끊기지 않고 생성된 객체를 해제 하는 등의 작업을 할 수 있다.
            // 예를들어, 파일을 열었을 때, 파일을 닫는 작업을 finally 블록에 넣어서 파일을 닫을 수 있다.

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
//클라이언트가 TCP접속 요청을 할 때마다 해당 클라이언트를 붙들고 있는 객체를 생성한다.
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

                //읽어온 메세지가 있으면 서버에게 전달
                server.BreadcastToClients($"{id}번 클라이언트: {readString}");
            }


        }
        catch (Exception e)
        {
            ServerManager.log.Enqueue($"{id}번 클라리언트 오류 발생:{e.Message}");
        }
        finally
        {
            Disconnect();
        }
    }

}