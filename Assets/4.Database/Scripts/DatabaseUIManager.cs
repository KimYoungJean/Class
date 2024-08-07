using MySqlConnector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;



public class DatabaseUIManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject loginPanel;
    public GameObject infoPanel;
    public GameObject signUpPanel;
    public GameObject editPanel;

    [Header("UI Elements")]
    [Header("Input Fields")]
    public InputField emailInput;
    public InputField passwordInput;
    public InputField emailText;
    public InputField passwordText;
    public InputField nameText;
    public InputField levelText;
    public InputField profileText;
    public InputField EditedInput;

    [Header("Buttons")]
    public Button loginButton;
    public Button signUpPageButton;
    public Button editPageButton;

    public Button cancelButton;
    public Button levelUpButton;
    public Button signUpButton;
    public Button editButton;
    public Button deleteButton;

    [Header("Texts")]
    public Text infoText;
    public Text leveltext;
    public Text currentInfoLabel;
    public Text dropdownCategory;
    public Text classText;
    public Text currentInfo;

    private UserData userData;




    private void Awake()
    {
        loginButton.onClick.AddListener(LoginButtonClick);
        levelUpButton.onClick.AddListener(onLevelupButtonClick);
        signUpPageButton.onClick.AddListener(SignUpPage);
        cancelButton.onClick.AddListener(CancelButton);
        signUpButton.onClick.AddListener(SignUpButtonClick);
        editPageButton.onClick.AddListener(EditPage);
        editButton.onClick.AddListener(EditButton);
        deleteButton.onClick.AddListener(DeleteButton);



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
        signUpPanel.SetActive(false);
        editPanel.SetActive(false);
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
        editPanel.SetActive(false);

        signUpPanel.SetActive(true);
    }
    public void EditPage()
    {
        infoPanel.SetActive(false);
        signUpPanel.SetActive(false);
        loginPanel.SetActive(false);

        editPanel.SetActive(true);

    }
    public void CancelButton()
    {
        infoPanel.SetActive(false);
        signUpPanel.SetActive(false);
        editPanel.SetActive(false);

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

    public void onValueChanged()
    {
        currentInfoLabel.text = dropdownCategory.text;

        switch (dropdownCategory.text)
        {
            case "Email":
                currentInfo.text = userData.email;
                break;
            case "Name":
                currentInfo.text = userData.name;
                break;
            case "Class":
                currentInfo.text = userData.characterClass.ToString();
                break;
            case "Level":
                currentInfo.text = userData.level.ToString();
                break;
            case "Profile_Text":
                currentInfo.text = userData.profileText;
                break;
        }
    }

    public void EditButton()
    {
        DatabaseManager.instance.Edit(userData.Uid,dropdownCategory.text,EditedInput.text,onEditSuccess);
    }
    public void onEditSuccess()
    {
        print("���� ����!");
        infoPanel.SetActive(false);
        signUpPanel.SetActive(false);
        loginPanel.SetActive(true);
        editPanel.SetActive(false);
    }

    public void DeleteButton()
    {
       DatabaseManager.instance.Delete(userData.Uid, onDeleteSuccess);
    }
    public void onDeleteSuccess()
    {
        print("���� ����!");
        infoPanel.SetActive(false);
        signUpPanel.SetActive(false);
        loginPanel.SetActive(true);
        editPanel.SetActive(false);
    }
}



