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

    /// <summary>
    /// 해당 보상을 받을 수 있는지 확인
    /// </summary>
    /// <param name="attendanceState">현재 출석 상태</param>
    /// <returns>보상을 받을 수 있으면 true, 아니면 false</returns>
    public bool CanClaim(AttendanceState attendanceState)
    {
        // 1. 현재 출석 카운트가 보상 일자보다 크거나 같아야 함
        if (attendanceState.CurrentAttendanceCount < Day)
        {
            return false;
        }

        // 2. 아직 보상을 받지 않았어야 함
        if (attendanceState.ClaimedRewardDays.Contains(Day))
        {
            return false;
        }

        return true;
    }
}
