using System.Collections.Generic;
using UnityEngine;

public class AttendanceManager : MonoBehaviour
{
    public static AttendanceManager Instance;
    
    private List<DailyAttendanceSO> _dailyAttendanceList;
    public List<DailyAttendanceSO> DailyAttendanceList => _dailyAttendanceList;
    private List<StreakAttendanceSO> _streakAttendanceList;
    public List<StreakAttendanceSO> StreakAttendanceList => _streakAttendanceList;

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
    }
    
    
    
    
}
