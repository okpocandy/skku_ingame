using System.Collections.Generic;
using UnityEngine;

public class UI_Attendance : MonoBehaviour
{
    [SerializeField]
    private List<UI_AttendanceSlot> _slotList = new List<UI_AttendanceSlot>();
    [SerializeField]
    private List<Sprite> _rewardIconList;               // 골드 다이아 루비 순서

    private AttendanceState _attendanceState;

    private void Start()
    {
        Refresh();
        // TODO: 이벤트 구독?
    }
    private void Refresh()
    {
        List<DailyAttendanceSO> dailyRewardList = AttendanceManager.Instance.DailyAttendanceList;

        for (int i=0; i<_slotList.Count; i++)
        {
            UI_AttendanceSlot slot = _slotList[i];
            slot.Refresh(dailyRewardList[i], 
            _rewardIconList[(int)dailyRewardList[i].CurrencyType],
            _attendanceState.isClaimedRewardDay(i));
        }

    }

}
