using UnityEngine;

public class DailyAttendanceRewardDTO
{
    public readonly int Day;
    public readonly ECurrencyType CurrencyType;
    public readonly int Amount;

    public DailyAttendanceRewardDTO(DailyAttendanceReward reward)
    {
        Day = reward.Day;
        CurrencyType = reward.CurrencyType;
        Amount = reward.Amount;
    }

    public DailyAttendanceReward ToDomain()
    {
        return new DailyAttendanceReward(Day, CurrencyType, Amount);
    }
}
