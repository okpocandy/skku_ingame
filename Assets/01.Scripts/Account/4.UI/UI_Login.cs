using System;
using System.Security.Cryptography;
using System.Text;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class UI_InputFields
{
    public TextMeshProUGUI ResultText;  // 결과 텍스트
    public TMP_InputField EmailInputField;
    public TMP_InputField NicknameInputField;
    public TMP_InputField PasswordInputField;
    public TMP_InputField PasswordComfirmInputField;
    public Button ConfirmButton;   // 로그인 or 회원가입 버튼
}

public class UI_Login : MonoBehaviour
{
    [Header("패널")]
    public GameObject LoginPanel;
    public GameObject ResisterPanel;

    [Header("로그인")] 
    public UI_InputFields LoginInputFields;
    
    [Header("회원가입")] 
    public UI_InputFields RegisterInputFields;

    private const string PREFIX = "ID_";
    private const string SALT = "10043420";

    private AccountRepository _accountRepository;
    
    

    // 게임 시작하면 로그인 켜주고 회원가입은 꺼주고..
    private void Start()
    {
        LoginPanel.SetActive(true);
        ResisterPanel.SetActive(false);
        
        LoginInputFields.ResultText.text    = string.Empty;
        RegisterInputFields.ResultText.text = string.Empty;

        LoginCheck();

        _accountRepository = new AccountRepository();
    }

    // 회원가입하기 버튼 클릭
    public void OnClickGoToResisterButton()
    {
        LoginPanel.SetActive(false);
        ResisterPanel.SetActive(true);
    }
    
    public void OnClickGoToLoginButton()
    {
        LoginPanel.SetActive(true);
        ResisterPanel.SetActive(false);
    }


    // 회원가입
    public void Resister()
    {
        // 1. 이메일 도메인 규칙을 확인한다.
        string email = RegisterInputFields.EmailInputField.text;
        var emailSpecification = new AccountEmailSpecification();
        if (!emailSpecification.IsSatisfiedBy(email))
        {
            throw new Exception(emailSpecification.ErrorMessage);
        }

        if (_accountRepository.Find(email) != null)
        {
            RegisterInputFields.ResultText.text = "이미 존재하는 이메일입니다.";
            return;
        }
        
        // 2. 닉네임 도메인 규칙을 확인한다.
        string nickname = RegisterInputFields.NicknameInputField.text;
        var nicknameSpecification = new AccountNicknameSpecification();
        if (!nicknameSpecification.IsSatisfiedBy(nickname))
        {
            throw new Exception(nicknameSpecification.ErrorMessage);
        }

        // 2. 1차 비밀번호 입력을 확인한다.
        string password = RegisterInputFields.PasswordInputField.text;
        var passwordSpecification = new AccountPasswordSpecification();
        if (!passwordSpecification.IsSatisfiedBy(password))
        {
            throw new Exception(passwordSpecification.ErrorMessage);
        }

        // 3. 2차 비밀번호 입력을 확인하고, 1차 비밀번호 입력과 같은지 확인한다.
        string password2 = RegisterInputFields.PasswordComfirmInputField.text;
        if (string.IsNullOrEmpty(password2))
        {
            RegisterInputFields.ResultText.text = "비밀번호를 입력해주세요.";
            return;
        }

        if (password != password2)
        {
            RegisterInputFields.ResultText.text = "비밀번혹가 다릅니다.";
            return;
        }

        Result result = AccountManager.Instance.TryRegister(email, nickname, password);
        if (result.IsSuccess)
        {
            // 5. 로그인 창으로 돌아간다.
            // (이때 아이디는 자동 입력되어 있다.)
            OnClickGoToLoginButton();
        }
        else
        {
            // 회원가입 실패
            RegisterInputFields.ResultText.text = result.Message;
        }

    }


    public void Login()
    {
        // 1. 이메일 입력을 확인한다.
        string email = LoginInputFields.EmailInputField.text;
        if (string.IsNullOrEmpty(email))
        {
            LoginInputFields.ResultText.text = "이메일을 입력해주세요.";
            return;
        }
        
        // 2. 비밀번호 입력을 확인한다.
        string password = LoginInputFields.PasswordInputField.text;
        if (string.IsNullOrEmpty(password))
        {
            LoginInputFields.ResultText.text = "비밀번호를 입력해주세요.";
            return;
        }
        
        if (AccountManager.Instance.TryLogin(email, password))
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            LoginInputFields.ResultText.text = "이메일 또는 비밀번호가 일치하지 않습니다.";
        }
        
        
    }
    
    
    // 아이디와 비밀번호 InputField 값이 바뀌었을 경우에만
    public void LoginCheck()
    {
        string email = LoginInputFields.EmailInputField.text;
        string password = LoginInputFields.PasswordInputField.text;
        
        //LoginInputFields.ConfirmButton.enabled = !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password);
    }
    
}