using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySqlConnector;
using UnityEngine.UI;
using System;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using Unity.PlasticSCM.Editor.WebApi;
using TMPro.EditorUtilities;

public class DatabaseManager : MonoBehaviour
{
    private string serverIP = "127.0.0.1";
    private string dbName = "game";
    private string tableName = "users";



    [SerializeField]
    private string rootPassword = "0000"; // �׽�Ʈ �ÿ� Ȱ���� �� ������, ���ȿ� ����ϹǷ� ����

    private MySqlConnection connection;

    public static DatabaseManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        DBconnect();
    }
    public void DBconnect()
    {
        string config =
            $"server={serverIP};port=3306;Database={dbName};" + $"uid = root; pwd = {rootPassword}; charset = utf8";


        connection = new MySqlConnection(config);
        connection.Open();
        //        print(connection.State);
    }

    //�α��� ����
    // �α����� �Ϸ��� �� ��, �α��� ������ ���� ��� �����Ͱ� ���� ���� �� �����Ƿ�, 
    // �α����� �Ϸ� �Ǿ��� ���� ȣ��� �Լ��� �Ķ���ͷ� �Բ� �޾��ֵ��� ��.
    // �ݹ�: �ݹ��� �Լ��� �Ķ���ͷ� �޾Ƽ�, �� �Լ��� �����ϴ� ���� ����.

    public void Login(string email, string password, Action<UserData> successCallback, Action failureCallback)
    {
        string pwhash = "";
        #region Dispose���
        /*
        
        SHA256 sha256 = SHA256.Create(); // ���� ����: ��ü ������ �޼ҵ� ȣ���� �� �ٷ� ó��
        byte[] hashArray = sha256.ComputeHash(Encoding.UTF8.GetBytes(password)); // ��й�ȣ�� �ؽ�
        foreach (byte b in hashArray)
        {
            //pwhash += $"{b:x2}";// 16������ ��ȯ
            pwhash += b.ToString("x2"); // 16������ ��ȯ
        }
        print(pwhash);
        // �ؽô� ����� ������ �ݵ�� �����ؾ� ��. �ֳ��ϸ� , GC�� �ؽø� �������� �ʱ� ����        
        sha256.Dispose();
        */
        #endregion
        #region ������ ������
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashArray = sha256.ComputeHash(Encoding.UTF8.GetBytes(password)); // ��й�ȣ�� �ؽ�
            foreach (byte b in hashArray)
            {
                //pwhash += $"{b:x2}";// 16������ ��ȯ
                pwhash += b.ToString("x2"); // 16������ ��ȯ
            }
            print(pwhash);
        }
        #endregion

        MySqlCommand cmd = new MySqlCommand(); // ������ ������ ���� ��ü

        cmd.Connection = connection; // ������ ���� Ŀ�ؼ��� ����

        cmd.CommandText =
            $"SELECT * FROM {tableName} WHERE email = '{email}' AND pw = '{password}'"; // ���� ����

        MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd); // ������ ������ ���� �����

        DataSet dataset = new DataSet(); // ���� ����� �޾ƿ� �����ͼ�
        dataAdapter.Fill(dataset); // �����ͼ¿� ���� ����� ä��

        bool isLoginSuccess = (dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0); // �α��� ���� ����

        if (isLoginSuccess)
        {
            DataRow row = dataset.Tables[0].Rows[0]; // �α��� ���� ��, �α����� ������ ������ �޾ƿ�

            print(row["level"].ToString()); // ��ҹ��� ������ �ʿ� ����.
            UserData data = new UserData(row); // �޾ƿ� ������ UserData ��ü�� ��ȯ

            print(data.email);

            successCallback?.Invoke(data); // �α��� ���� 
        }
        else
        {
            failureCallback?.Invoke(); // �α��� ����
        }
    }

    public void LevelUp(UserData data, Action successCallback)
    {
        int level = data.level;
        int nextLevel = level + 1;

        MySqlCommand cmd = new MySqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = $"UPDATE {tableName} SET level = {nextLevel} WHERE uid = {data.Uid}";

        int queryCount = cmd.ExecuteNonQuery();

        if (queryCount > 0)
        {
            // ���� ���� ����
            data.level = nextLevel;
            successCallback?.Invoke();
        }
        else
        {
            // ���� ���� ����
        }
    }
    public void SignUp(string email, string password, string name, string characterClass, string level, string profileText, Action successCallback)
    {

        int classValue = (int)Enum.Parse(typeof(CharacterClass), characterClass);
        int levelValue = int.Parse(level);

        MySqlCommand cmd = new MySqlCommand();
        cmd.Connection = connection;
        cmd.CommandText =
            $"INSERT INTO {tableName} (email, pw, name, class, level, profile_text) VALUES ('{email}', '{password}', '{name}', {classValue}, {levelValue}, '{profileText}')";

        int queryCount = cmd.ExecuteNonQuery();

        if (queryCount > 0)
        {

            successCallback?.Invoke();
        }
        else
        {
            // ���� ���� ����
        }
    }
    public void Edit(int uid ,string dropdown,string edit, Action successCallback)
    {
        MySqlCommand cmd = new MySqlCommand();
        cmd.Connection = connection;
        if(dropdown == "class")
        {
            int _edit = (int)Enum.Parse(typeof(CharacterClass), edit);
            

            cmd.CommandText =
                $"UPDATE {tableName} SET {dropdown} = '{_edit}' WHERE uid = '{uid}'";
        }
        else if(dropdown == "level")
        {
            int _edit = int.Parse(edit);
            

            cmd.CommandText =
            $"UPDATE {tableName} SET {dropdown} = '{_edit}' WHERE uid = ' {uid}'";
        }
        else
        {
            cmd.CommandText =
                $"UPDATE {tableName} SET {dropdown} = '{edit}' WHERE uid = ' {uid}'";
        }

        int queryCount = cmd.ExecuteNonQuery();
        if (queryCount > 0) {
            successCallback?.Invoke();
        }
        else
        {
            // ���� ���� ����
        }

    }
    public void Delete(int uid, Action successCallback)
    {
        MySqlCommand cmd = new MySqlCommand();
        cmd.Connection = connection;
        cmd.CommandText =
            $"DELETE FROM {tableName} WHERE uid = '{uid}'";

        int queryCount = cmd.ExecuteNonQuery();
        if (queryCount > 0)
        {
            successCallback?.Invoke();
        }
        else
        {
            // ���� ���� ����
        }
    }

}
