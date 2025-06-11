using UnityEngine;
using System;

public class StreakRewardRule
{
    public readonly int StreakDate;
    public readonly ECurrencyType CurrencyType;
    public readonly int Amount;

    public StreakRewardRule(int streakDate, ECurrencyType currencyType, int amount)
    {
        if(streakDate <= 0 || streakDate > 28)
        {
            throw new Exception("연속 출석 일수는 1~28일 사이여야 합니다.");
        }
        if(amount <= 0)
        {
            throw new Exception("보상 양은 0보다 커야 합니다.");
        }

        StreakDate = streakDate;
        CurrencyType = currencyType;
        Amount = amount;
    }
}
