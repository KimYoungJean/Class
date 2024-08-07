using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySqlConnector;
using UnityEngine.UI;

public class DatabaseManager : MonoBehaviour
{
    private string serverIP = "127.0.0.1";
    private string dbName = "game";
    private string tableName = "users";

    

    [SerializeField]
    private string rootPassword = "0000"; // �׽�Ʈ �ÿ� Ȱ���� �� ������, ���ȿ� ����ϹǷ� ����

    private MySqlConnection connection;

  

    public void DBconnect()
    {
        string config =
            $"server={serverIP};port=3306;Database={dbName};" + $"uid = root; pwd = {rootPassword}; charset = utf8";
          

        connection = new MySqlConnection(config);
        connection.Open();
        print(connection.State);




    }





}
