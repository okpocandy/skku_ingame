using UnityEngine;
using System;


public class DailyAttendanceReward
{
    public readonly int Day;                        //보상 일자
    public readonly ECurrencyType CurrencyType;     //보상 재화 타입
    public readonly int Amount;

    public DailyAttendanceReward(int day, ECurrencyType currencyType, int amount)
    {
        if (day <= 0 || day > 28)
        {
            throw new Exception("보상 일자는 1~28일 사이여야 합니다.");
        }
        if (amount <= 0)
        {
            throw new Exception("보상 양은 0보다 커야 합니다.");
        }

        Day = day;
        CurrencyType = currencyType;
        Amount = amount;
    }

}
