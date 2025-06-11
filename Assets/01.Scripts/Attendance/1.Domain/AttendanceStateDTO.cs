using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class AttendanceStateDTO
{
    public readonly string LastCheckDate;              // 마지막 출석 체크 날짜 (DateTime을 string으로 저장)
    public readonly int CurrentAttendanceCount;        // 달마다 초기화되는 누적 일수
    public readonly int ContinousAttendanceCount;      // 연속 출석 일수
    public readonly int TotalAttendanceCount;          // 전체 누적 일수
    public readonly List<int> ClaimedRewardDays;       // 보상 받은 날짜
    public readonly List<int> ClaimedStreakDays;       // 연속 출석 보상 받은 날짜
    public readonly string PlayerID;                   // 플레이어 ID

    public AttendanceStateDTO()
    {
        LastCheckDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        CurrentAttendanceCount = 0;
        ContinousAttendanceCount = 0;
        TotalAttendanceCount = 0;
        ClaimedRewardDays = new List<int>();
        ClaimedStreakDays = new List<int>();
        PlayerID = string.Empty;
    }

    public AttendanceStateDTO(AttendanceState state)
    {
        LastCheckDate = state.LastCheckDate.ToString("yyyy-MM-dd HH:mm:ss");
        CurrentAttendanceCount = state.CurrentAttendanceCount;
        ContinousAttendanceCount = state.ContinousAttendanceCount;
        TotalAttendanceCount = state.TotalAttendanceCount;
        ClaimedRewardDays = new List<int>(state.ClaimedRewardDays);
        ClaimedStreakDays = new List<int>(state.ClaimedStreakDays);
        PlayerID = state.PlayerID;
    }

    public AttendanceState ToDomain()
    {
        return new AttendanceState(
            DateTime.Parse(LastCheckDate),
            CurrentAttendanceCount,
            ContinousAttendanceCount,
            TotalAttendanceCount,
            new List<int>(ClaimedRewardDays),
            new List<int>(ClaimedStreakDays),
            PlayerID
        );
    }
}
