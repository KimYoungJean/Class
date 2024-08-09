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
    private string serverIP = "3.36.92.206";
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


        SHA256 sha256 = SHA256.Create(); // ���� ����: ��ü ������ �޼ҵ� ȣ���� �� �ٷ� ó��
        byte[] hashArray = sha256.ComputeHash(Encoding.UTF8.GetBytes(password)); // ��й�ȣ�� �ؽ�
        foreach (byte b in hashArray)
        {
            //pwhash += $"{b:x2}";// 16������ ��ȯ
            pwhash += b.ToString("x2"); // 16������ ��ȯ
        }
        print(pwhash);
        // �ؽô� ����� ������ �ݵ�� �����ؾ� ��. �ֳ��ϸ� , GC�� �ؽø� �������� �ʱ� ����        


        #endregion
        #region ������ ������
        /*   using (SHA256 sha256 = SHA256.Create())
           {
               byte[] hashArray = sha256.ComputeHash(Encoding.UTF8.GetBytes(password)); // ��й�ȣ�� �ؽ�
               foreach (byte b in hashArray)
               {
                   //pwhash += $"{b:x2}";// 16������ ��ȯ
                   pwhash += b.ToString("x2"); // 16������ ��ȯ
               }
               print(pwhash);
           }*/
        #endregion

        MySqlCommand cmd = new MySqlCommand(); // ������ ������ ���� ��ü

        cmd.Connection = connection; // ������ ���� Ŀ�ؼ��� ����

        cmd.CommandText =
            $"SELECT * FROM {tableName} WHERE email = '{email}' AND pwhash = '{pwhash}'"; // ���� ����

        MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd); // ������ ������ ���� �����

        DataSet dataset = new DataSet(); // ���� ����� �޾ƿ� �����ͼ�
        dataAdapter.Fill(dataset); // �����ͼ¿� ���� ����� ä��

        bool isLoginSuccess = (dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0); // �α��� ���� ����

        if (isLoginSuccess)
        {
            DataRow row = dataset.Tables[0].Rows[0]; // �α��� ���� ��, �α����� ������ ������ �޾ƿ�

            print(row["level"].ToString()); // ��ҹ��� ������ �ʿ� ����.
            UserData data = new UserData(row); // �޾ƿ� ������ UserData ��ü�� ��ȯ

            //print(data.email);

            successCallback?.Invoke(data); // �α��� ���� 
            sha256.Dispose();
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

        string pwhash = "";



        SHA256 sha256 = SHA256.Create(); // ���� ����: ��ü ������ �޼ҵ� ȣ���� �� �ٷ� ó��
        byte[] hashArray = sha256.ComputeHash(Encoding.UTF8.GetBytes(password)); // ��й�ȣ�� �ؽ�
        foreach (byte b in hashArray)
        {
            //pwhash += $"{b:x2}";// 16������ ��ȯ
            pwhash += b.ToString("x2"); // 16������ ��ȯ
        }
        print(pwhash);
        // �ؽô� ����� ������ �ݵ�� �����ؾ� ��. �ֳ��ϸ� , GC�� �ؽø� �������� �ʱ� ����        

        MySqlCommand cmd = new MySqlCommand();
        cmd.Connection = connection;
        cmd.CommandText =
            $"INSERT INTO {tableName} (email, pw, name, class, level, profile_text,pwHash) VALUES ('{email}', '{password}', '{name}', {classValue}, {levelValue}, '{profileText}','{pwhash}')";

        int queryCount = cmd.ExecuteNonQuery();

        if (queryCount > 0)
        {

            successCallback?.Invoke();
            sha256.Dispose();
        }
        else
        {
            // ���� ���� ����
        }
    }
    public void Edit(int uid, string cuurentInfo, string category, string edit, Action successCallback)
    {
        
        MySqlCommand cmd = new MySqlCommand();
        cmd.Connection = connection;
        if (category == "Class")
        {
            int _edit = (int)Enum.Parse(typeof(CharacterClass), edit);//���ڿ��� enum���� ��ȯ      
            cmd.CommandText =
                $"UPDATE {tableName} SET {category} = '{_edit}' WHERE uid = '{uid}'";
        }
        else if (category == "Level")
        {
            int _edit = int.Parse(edit);


            cmd.CommandText =
            $"UPDATE {tableName} SET {category} = '{_edit}' WHERE uid = ' {uid}'";
        }
        else if (category == "Password")
        {
            print("��й�ȣ ����");
            
            string pwhash = "";
            SHA256 sha256 = SHA256.Create(); // ���� ����: ��ü ������ �޼ҵ� ȣ���� �� �ٷ� ó��
            byte[] hashArray = sha256.ComputeHash(Encoding.UTF8.GetBytes(edit)); // ��й�ȣ�� �ؽ�
            foreach (byte b in hashArray)
            {
                //pwhash += $"{b:x2}";// 16������ ��ȯ
                pwhash += b.ToString("x2"); // 16������ ��ȯ
            }
            cmd.CommandText =
                $"UPDATE {tableName} " +
                $"SET pwHash = '{pwhash}',pw ='{edit}' " +
                $"WHERE uid = '{uid}'";
               
            

            
            print("��й�ȣ ����2");
            sha256.Dispose();
        }
        else
        {
            cmd.CommandText =
                $"UPDATE {tableName} SET {category} = '{edit}' WHERE uid = ' {uid}'";
        }

        
        
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

    public void Find(string email, Action<UserData> successCallback)
    {
        MySqlCommand cmd = new MySqlCommand();
        cmd.Connection = connection;
        cmd.CommandText =
            $"SELECT * FROM {tableName} WHERE email = '{email}'";

        MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);
        DataSet dataset = new DataSet();
        dataAdapter.Fill(dataset);

        bool isFindSuccess = (dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0);

        if (isFindSuccess)
        {
            
            DataRow row = dataset.Tables[0].Rows[0];
            UserData data = new UserData(row);
            successCallback?.Invoke(data);
        }
        else
        {
            // ���� ���� ����
        }

    }

}
