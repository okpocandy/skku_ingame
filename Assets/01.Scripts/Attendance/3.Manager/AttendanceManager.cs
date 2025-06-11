using System;
using System.Collections.Generic;
using UnityEngine;

public class AttendanceManager : MonoBehaviour
{
    public static AttendanceManager Instance;
    
    private List<DailyAttendanceReward> _dailyAttendanceRewards;
    private List<StreakRewardRule> _streakRewardRules;
    
    [Header("정적 보상 데이터(SO)")]
    [SerializeField] private List<DailyAttendanceSO> _dailyAttendanceList;
    public List<DailyAttendanceSO> DailyAttendanceList => _dailyAttendanceList;
    
    [SerializeField] private List<StreakAttendanceSO> _streakAttendanceList;
    public List<StreakAttendanceSO> StreakAttendanceList  => _streakAttendanceList;

    private AttendanceState _attendanceState;
    [SerializeField] AttendanceStateRepository _attendanceStateRepository;
    
    public event Action<AttendanceStateDTO>  OnAttendanceStateChanged;
    
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

        Init();
    }

    private void Init()
    {
        _attendanceStateRepository = new AttendanceStateRepository();
        
    }

    private void Start()
    {
        //_attendanceState = _attendanceStateRepository.Load();
    }

    public bool CanCheckToday()
    {
        return _attendanceState.IsOneDayPassed();
    }

    public void TryCheckIn(string playerID)
    {
        if (!CanCheckToday()) return;
        
        // 출석 처리
        _attendanceState.Attendance();
        
        // 보상 처리
        GiveDailyReward();
        GiveStreakReward();
        
        // 저장
        //_attendanceStateRepository.Save(_attendanceState);
        
        // UI 업데이트 이벤트 호출
        //OnAttendanceStateChanged?.Invoke(_attendanceState.);
    }

    private void GiveDailyReward()
    {
        var today = _attendanceState.CurrentAttendanceCount;
        var reward = _dailyAttendanceList.Find(x => x.Day == today);
        if (reward != null && !_attendanceState.isClaimedRewardDay(today))
        {
            CurrencyManager.Instance.Add(reward.CurrencyType, reward.Amount);
            _attendanceState.AddClaimRewardDay(today);
        }
    }

    private void GiveStreakReward()
    {
        foreach (var rule in _streakAttendanceList)
        {
            int streak = _attendanceState.ContinousAttendanceCount;

            if (streak == rule.StreakDate && !_attendanceState.isClaimedRewardDay(streak))
            {
                CurrencyManager.Instance.Add(rule.CurrencyType, rule.Amount);
                _attendanceState.AddClaimRewardDay(streak);
            }
        }
    }
    
    // public AttendanceState GetAttendanceState() => _attendanceState.;
    
    
    
    
}
