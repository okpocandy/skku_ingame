using System;
using System.Text.RegularExpressions;
using UnityEngine;

public class Account
{
    public readonly string Email;
    public readonly string Nickname;
    public readonly string Password;
   
   
    public Account(string email, string nickname, string password)
    {
        var emailSpecification = new AccountEmailSpecification();
        if (!emailSpecification.IsSatisfiedBy(email))
        {
            throw new Exception(emailSpecification.ErrorMessage);
        }

        var nicknameSpeicification = new AccountNicknameSpecification();
        if (!nicknameSpeicification.IsSatisfiedBy(nickname))
        {
            throw new Exception(nicknameSpeicification.ErrorMessage);
        }

        Email = email;
        Nickname = nickname;
        Password = password;
    }

    public AccountDTO ToDTO()
    {
        return new AccountDTO(Email, Nickname, Password);
    }
}