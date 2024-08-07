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
    private string rootPassword = "0000"; // 테스트 시에 활용할 수 있지만, 보안에 취약하므로 주의

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

    //로그인 구현
    // 로그인을 하려고 할 때, 로그인 쿼리를 날린 즉시 데이터가 오지 않을 수 있으므로, 
    // 로그인이 완료 되었을 때의 호출될 함수를 파라미터로 함께 받아주도록 함.
    // 콜백: 콜백은 함수를 파라미터로 받아서, 그 함수를 실행하는 것을 말함.

    public void Login(string email, string password, Action<UserData> successCallback, Action failureCallback)
    {
        string pwhash = "";
        #region Dispose사용
        /*
        
        SHA256 sha256 = SHA256.Create(); // 빌더 패턴: 객체 생성과 메소드 호출을 한 줄로 처리
        byte[] hashArray = sha256.ComputeHash(Encoding.UTF8.GetBytes(password)); // 비밀번호를 해싱
        foreach (byte b in hashArray)
        {
            //pwhash += $"{b:x2}";// 16진수로 변환
            pwhash += b.ToString("x2"); // 16진수로 변환
        }
        print(pwhash);
        // 해시는 사용이 끝나면 반드시 해제해야 함. 왜냐하면 , GC가 해시를 해제하지 않기 때문        
        sha256.Dispose();
        */
        #endregion
        #region 디스포즈 사용안함
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashArray = sha256.ComputeHash(Encoding.UTF8.GetBytes(password)); // 비밀번호를 해싱
            foreach (byte b in hashArray)
            {
                //pwhash += $"{b:x2}";// 16진수로 변환
                pwhash += b.ToString("x2"); // 16진수로 변환
            }
            print(pwhash);
        }
        #endregion

        MySqlCommand cmd = new MySqlCommand(); // 쿼리를 날리기 위한 객체

        cmd.Connection = connection; // 쿼리를 날릴 커넥션을 설정

        cmd.CommandText =
            $"SELECT * FROM {tableName} WHERE email = '{email}' AND pw = '{password}'"; // 쿼리 내용

        MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd); // 쿼리를 날리기 위한 어댑터

        DataSet dataset = new DataSet(); // 쿼리 결과를 받아올 데이터셋
        dataAdapter.Fill(dataset); // 데이터셋에 쿼리 결과를 채움

        bool isLoginSuccess = (dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0); // 로그인 성공 여부

        if (isLoginSuccess)
        {
            DataRow row = dataset.Tables[0].Rows[0]; // 로그인 성공 시, 로그인한 유저의 정보를 받아옴

            print(row["level"].ToString()); // 대소문자 구분할 필요 없음.
            UserData data = new UserData(row); // 받아온 정보를 UserData 객체로 변환

            print(data.email);

            successCallback?.Invoke(data); // 로그인 성공 
        }
        else
        {
            failureCallback?.Invoke(); // 로그인 실패
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
            // 쿼리 수행 성공
            data.level = nextLevel;
            successCallback?.Invoke();
        }
        else
        {
            // 쿼리 수행 실패
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
            // 쿼리 수행 실패
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
            // 쿼리 수행 실패
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
            // 쿼리 수행 실패
        }
    }

}
