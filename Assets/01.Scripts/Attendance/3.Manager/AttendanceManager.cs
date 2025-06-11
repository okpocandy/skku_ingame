using System;
using System.Collections.Generic;
using UnityEngine;

public class AttendanceManager : MonoBehaviour
{
    public static AttendanceManager Instance;
    
    private List<DailyAttendanceReward> _dailyAttendanceRewards;
    public List<DailyAttendanceRewardDTO> DailyAttendanceRewards => _dailyAttendanceRewards.ConvertAll(a => new DailyAttendanceRewardDTO(a));
    
    private List<StreakRewardRule> _streakRewardRules;
    public List<StreakRewardRuleDTO> StreakRewardRules => _streakRewardRules.ConvertAll(a => new StreakRewardRuleDTO(a));
    
    [Header("정적 보상 데이터(SO)")]
    [SerializeField] private List<DailyAttendanceSO> _dailyAttendanceSOList;
    public List<DailyAttendanceSO> DailyAttendanceList => _dailyAttendanceSOList;
    
    [SerializeField] private List<StreakAttendanceSO> _streakAttendanceSOList;
    public List<StreakAttendanceSO> StreakAttendanceList  => _streakAttendanceSOList;

    [SerializeField] AttendanceStateRepository _attendanceStateRepository;
    
    private AttendanceState _attendanceState;
    public AttendanceState AttendanceState => _attendanceState;
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
        _dailyAttendanceRewards = new List<DailyAttendanceReward>();
        foreach (var so in _dailyAttendanceSOList)
        {
            _dailyAttendanceRewards.Add(new DailyAttendanceReward(so.Day, so.CurrencyType, so.Amount));
        }
        _streakRewardRules = new List<StreakRewardRule>();
        foreach (var so in _streakAttendanceSOList)
        {
            _streakRewardRules.Add(new StreakRewardRule(so.StreakDate, so.CurrencyType, so.Amount));
        }

        var dto = _attendanceStateRepository.Load();
        _attendanceState = dto != null ? dto.ToDomain() : new AttendanceState(DateTime.MinValue, 1, 1, 1, new List<int>(), new List<int>(), "player01");
    }

    public bool CanCheckToday()
    {
        return _attendanceState.IsOneDayPassed();
    }
    
    // 오늘 이미 출석했는지 확인
    public void TryCheckIn(string playerID)
    {
        if (!CanCheckToday()) return;
        
        // 출석 처리
        _attendanceState.Attendance();
        
        // 보상 처리
        GiveDailyReward();
        GiveStreakReward();
        
        // 저장
        _attendanceStateRepository.Save(new AttendanceStateDTO(_attendanceState));
        
        // UI 업데이트 이벤트 호출
        OnAttendanceStateChanged?.Invoke(new AttendanceStateDTO(_attendanceState));
    }

    // 일일 보상 지급 세부 처리
    private void GiveDailyReward()
    {
        int today = _attendanceState.CurrentAttendanceCount;
        var reward = _dailyAttendanceSOList.Find(x => x.Day == today);
        if (reward != null && !_attendanceState.isClaimedRewardDay(today))
        {
            CurrencyManager.Instance.Add(reward.CurrencyType, reward.Amount);
            _attendanceState.AddClaimRewardDay(today);
        }
    }

    // 연속 보상 지급 세부 처리
    private void GiveStreakReward()
    {
        int streak = _attendanceState.ContinousAttendanceCount;
        
        foreach (var rule in _streakAttendanceSOList)
        {

            if (streak == rule.StreakDate && !_attendanceState.isClaimedRewardDay(streak))
            {
                CurrencyManager.Instance.Add(rule.CurrencyType, rule.Amount);
                _attendanceState.AddClaimRewardDay(streak);
            }
        }
    }

    public AttendanceStateDTO GetCurrentAttendanceState()
    {
        return new AttendanceStateDTO(_attendanceState);
    }
}
