using System.Collections.Generic;
using UnityEngine;
using System;

public class AttendanceState
{
    private DateTime _lastCheckDate;              // 마지막 출석 체크 날짜
    public DateTime LastCheckDate => _lastCheckDate;
    private int _currentAttendanceCount;     // 달마다 초기화되는 누적 일수
    public int CurrentAttendanceCount => _currentAttendanceCount;
    private int _continousAttendanceCount;   // 연속 출석 일수
    public int ContinousAttendanceCount => _continousAttendanceCount;
    private int _totalAttendanceCount;       // 전체 누적 일수
    public int TotalAttendanceCount => _totalAttendanceCount;
    private List<int> _claimedRewardDays;     // 보상 받은 날짜
    public List<int> ClaimedRewardDays => _claimedRewardDays;
    private List<int> _claimedStreakDays;    // 연속 출석 보상 받은 날짜
    public List<int> ClaimedStreakDays => _claimedStreakDays;
    private string _playerID;
    public string PlayerID => _playerID;

    public AttendanceState(DateTime lastCheckDate, int currentAttendanceCount, int continousAttendanceCount,
                            int totalAttendanceCount, List<int> claimedRewardDays, List<int> claimedStreakDays, string playerID)
    {
        if(lastCheckDate <= DateTime.Now)
        {
            //throw new Exception("마지막 출석 체크 날짜는 현재 날짜보다 이전이어야 합니다.");
        }
        if(currentAttendanceCount <= 0 || currentAttendanceCount > 28)
        {
            throw new Exception("보상 받은 날짜는 1~28일 사이여야 합니다.");
        }
        if(continousAttendanceCount <= 0 || continousAttendanceCount > 28)
        {
            throw new Exception("연속 출석 일수는 1~28일 사이여야 합니다.");
        }
        if(totalAttendanceCount < 0)
        {
            throw new Exception("전체 누적 일수는 0보다 커야 합니다.");
        }

        _lastCheckDate = lastCheckDate;
        _currentAttendanceCount = currentAttendanceCount;
        _continousAttendanceCount = continousAttendanceCount;
        _totalAttendanceCount = totalAttendanceCount;
        _claimedRewardDays = claimedRewardDays;
        _claimedStreakDays = claimedStreakDays;
        _playerID = playerID;
    }

    public void AddClaimRewardDay(int day)
    {
        if(day <= 0 || day > 28)
        {
            throw new Exception("보상 받은 날짜는 1~28일 사이여야 합니다.");
        }
        ClaimedRewardDays.Add(day);
    }

    public void AddClaimStreakDay(int day)
    {
        if(day <= 0 || day > 28)
        {
            throw new Exception("연속 출석 보상 받은 날짜는 1~28일 사이여야 합니다.");
        }
        ClaimedStreakDays.Add(day);
    }

    /// <summary>
    /// 해당 날짜에 보상을 받았는지 체크. 리스트에 들어있으면 보상 받은거임.
    /// </summary>
    /// <param name="day"></param>
    /// <returns></returns>
    public bool isClaimedRewardDay(int day)
    {
        return ClaimedRewardDays.Contains(day);
    }

    /// <summary>
    /// 해당 연속 출석 일수에 보상을 받았는지 체크
    /// </summary>
    /// <param name="day"></param>
    /// <returns></returns>
    public bool isClaimedStreakDay(int day)
    {
        return ClaimedStreakDays.Contains(day);
    }

    /// <summary>
    /// 마지막 출석 체크 날짜로부터 하루가 지났는지 확인
    /// </summary>
    /// <returns></returns>
    public bool IsOneDayPassed()
    {
        var now = DateTime.Now;
        var timeSpan = now - _lastCheckDate;
        // 시간이 1초라도 지났다면 하루 지난거임
        return timeSpan.TotalSeconds >= 1;
    }

    /// <summary>
    /// 연속 출석이 끊겼는지 확인 (하루 이상 지났으면 끊긴 것)
    /// </summary>
    /// <returns></returns>
    public bool IsStreakBroken()
    {
        // 날짜차이가 하루보다 많다면 연속 출석이 끊김
        var now = DateTime.Now;
        var timeSpan = now - _lastCheckDate;
        return timeSpan.TotalDays > 1;
    }

    /// <summary>
    /// 월이 바뀌었는지 확인
    /// </summary>
    /// <returns></returns>
    public bool IsMonthChanged()
    {
        var now = DateTime.Now;
        return _lastCheckDate.Month != now.Month || _lastCheckDate.Year != now.Year;
    }
    
    /// <summary>
    /// 출석 처리. 월이 바뀌었으면 누적 일수 초기화, 연속 출석 일수 초기화, 마지막 출석 체크 날짜 업데이트
    /// </summary>
    /// <returns></returns>
    public bool Attendance()
    {
        if(IsMonthChanged())
        {
            _currentAttendanceCount = 1;
            _continousAttendanceCount = 1;
        }
        else
        {
            _currentAttendanceCount++;
            _totalAttendanceCount++;
            if(!IsStreakBroken())
            {
                _continousAttendanceCount++;
            }
            else
            {
                _continousAttendanceCount = 1;
            }
        }

        _lastCheckDate = DateTime.Now;
        return true;
    }
}
