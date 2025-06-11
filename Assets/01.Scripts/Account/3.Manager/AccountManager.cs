using System.Security.Cryptography;
using System.Text;
using UnityEditor.Overlays;
using UnityEngine;

public class AccountManager : MonoBehaviour
{
    public static AccountManager Instance;

    private Account _myAccount;
    public AccountDTO CurrentAccount => _myAccount.ToDTO();

    private AccountRepository _accountRepository;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        _accountRepository = new AccountRepository();
    }

    private const string SALT = "123456";
    public Result TryRegister(string email, string nickname, string password)
    {
        string encryptedPassword = CryptoUitl.Encryption(password, SALT);
        
        // 레포 저장
        if (_accountRepository.Find(email) != null)
        {
            return new Result(false, "이미 존재하는 이메일입니다.");
        }
        _accountRepository.Save(new AccountDTO(email, nickname, encryptedPassword));
        
        return new Result(true, "회원가입 성공");
    }
    
    public bool TryLogin(string email, string password)
    {
        AccountSaveData data = _accountRepository.Find(email);
        if (data == null)
        {
            return false;
        }
        if (!CryptoUitl.Verify(password, data.Password, SALT))
        {
            return false;
        }
        _myAccount = new Account(email, data.Nickname, data.Password);
        Debug.Log($"로그인 성공: {_myAccount.Email} {_myAccount.Nickname} {_myAccount.Password}");
        return true;
    }
   
}