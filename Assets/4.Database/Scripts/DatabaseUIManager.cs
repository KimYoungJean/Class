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

    //콜백: 콜백은 함수를 파라미터로 받아서, 그 함수를 실행하는 것을 말함.
    //로그인이 완료 되었을 때의 호출될 함수를 파라미터로 함께 받아주도록 함.

    public void Start()
    {
        loginPanel.SetActive(true);
        infoPanel.SetActive(false);
        signUpPanel.SetActive(false);
    }
    public void onLevelUpsuccess()
    {
        leveltext.text = $"레벨: {userData.level}";
    }

    public void LoginButtonClick()
    {
        DatabaseManager.instance.Login(emailInput.text, passwordInput.text, onLoginSuccess, onLoginFailure);
    }

    private void onLoginSuccess(UserData data)
    {
        print("로그인 성공!");
        userData = data;

        loginPanel.SetActive(false);
        infoPanel.SetActive(true);

        //infoText.text = $"이메일: {userData.email}\n이름: {userData.name}\n클래스: {userData.characterClass}\n레벨: {userData.level}\n프로필: {userData.profileText}";
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"안녕하세요 {userData.name} 님");

        sb.AppendLine($"이메일: {userData.email}");
        sb.AppendLine($"클래스: {userData.characterClass}");
        sb.AppendLine($"프로필: {userData.profileText}");

        infoText.text = sb.ToString();

        leveltext.text = $"레벨: {userData.level}";
    }
    private void onLoginFailure()
    {
        print("로그인 실패 ㅜㅜ");
    }

    // 암호화 해시함수 : SHA256 (Secure Hash Algorithm 256)
    // 해시함수란? : 임의의 길이의 데이터를 고정된 길이의 데이터로 매핑하는 함수

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
        print("회원가입 성공!");
        infoPanel.SetActive(false);
        signUpPanel.SetActive(false);
        loginPanel.SetActive(true);
       // infoText.text = $"이메일: {userData.email}\n이름: {userData.name}\n클래스: {userData.characterClass}\n레벨: {userData.level}\n프로필: {userData.profileText}";
    }

}



