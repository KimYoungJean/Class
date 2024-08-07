using MySqlConnector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;



public class DatabaseUIManager : MonoBehaviour
{
    public GameObject loginPanel;
    public GameObject infoPanel;
    public GameObject signUpPanel;


    public InputField emailInput;
    public InputField passwordInput;

    public Button loginButton;
    public Button signUpPageButton;
    public Button cancelButton;
    public Button levelUpButton;
    public Button signUpButton;

    public Text infoText;
    public Text leveltext;

    private UserData userData;

    public InputField emailText;
    public InputField passwordText;
    public InputField nameText;
    public Text classText;
    public InputField levelText;
    public InputField profileText;


    private void Awake()
    {
        loginButton.onClick.AddListener(LoginButtonClick);
        levelUpButton.onClick.AddListener(onLevelupButtonClick);
        signUpPageButton.onClick.AddListener(SignUpPage);
        cancelButton.onClick.AddListener(CancelButton);
        signUpButton.onClick.AddListener(SignUpButtonClick);


    }
    public void onLevelupButtonClick()
    {
        DatabaseManager.instance.LevelUp(userData, onLevelUpsuccess);

    }

    //�ݹ�: �ݹ��� �Լ��� �Ķ���ͷ� �޾Ƽ�, �� �Լ��� �����ϴ� ���� ����.
    //�α����� �Ϸ� �Ǿ��� ���� ȣ��� �Լ��� �Ķ���ͷ� �Բ� �޾��ֵ��� ��.

    public void Start()
    {
        loginPanel.SetActive(true);
        infoPanel.SetActive(false);
        signUpPanel.SetActive(false);
    }
    public void onLevelUpsuccess()
    {
        leveltext.text = $"����: {userData.level}";
    }

    public void LoginButtonClick()
    {
        DatabaseManager.instance.Login(emailInput.text, passwordInput.text, onLoginSuccess, onLoginFailure);
    }

    private void onLoginSuccess(UserData data)
    {
        print("�α��� ����!");
        userData = data;

        loginPanel.SetActive(false);
        infoPanel.SetActive(true);

        //infoText.text = $"�̸���: {userData.email}\n�̸�: {userData.name}\nŬ����: {userData.characterClass}\n����: {userData.level}\n������: {userData.profileText}";
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"�ȳ��ϼ��� {userData.name} ��");

        sb.AppendLine($"�̸���: {userData.email}");
        sb.AppendLine($"Ŭ����: {userData.characterClass}");
        sb.AppendLine($"������: {userData.profileText}");

        infoText.text = sb.ToString();

        leveltext.text = $"����: {userData.level}";
    }
    private void onLoginFailure()
    {
        print("�α��� ���� �̤�");
    }

    // ��ȣȭ �ؽ��Լ� : SHA256 (Secure Hash Algorithm 256)
    // �ؽ��Լ���? : ������ ������ �����͸� ������ ������ �����ͷ� �����ϴ� �Լ�

    /* private void SignUpButtonClick()
      {
          DatabaseManager.instance.SignUp(SignUpPage);
      }*/
    public void SignUpPage()
    {
        loginPanel.SetActive(false);
        infoPanel.SetActive(false);
        signUpPanel.SetActive(true);
    }
    public void CancelButton()
    {
        infoPanel.SetActive(false);
        signUpPanel.SetActive(false);
        loginPanel.SetActive(true);
    }
    
    public void SignUpButtonClick()
    {
       DatabaseManager.instance.SignUp
            (emailText.text, 
            passwordText.text, 
            nameText.text, 
            classText.text, 
            levelText.text, 
            profileText.text, 
            onSignUpSuccess);
    }
    private void onSignUpSuccess()
    {
        print("ȸ������ ����!");
        infoPanel.SetActive(false);
        signUpPanel.SetActive(false);
        loginPanel.SetActive(true);
       // infoText.text = $"�̸���: {userData.email}\n�̸�: {userData.name}\nŬ����: {userData.characterClass}\n����: {userData.level}\n������: {userData.profileText}";
    }

}



