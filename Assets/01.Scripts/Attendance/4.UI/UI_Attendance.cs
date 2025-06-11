using System.Collections.Generic;
using UnityEngine;

public class UI_DailyAttendance : MonoBehaviour
{
    [SerializeField]
    private List<UI_DailyAttendanceSlot> _dailySlotList = new List<UI_DailyAttendanceSlot>();
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
        List<DailyAttendanceRewardDTO> dailyRewardList = AttendanceManager.Instance.DailyAttendanceRewards;

        for (int i=0; i<_dailySlotList.Count; i++)
        {
            UI_DailyAttendanceSlot slot = _dailySlotList[i];
            slot.Refresh(dailyRewardList[i], 
            _rewardIconList[(int)dailyRewardList[i].CurrencyType],
            _attendanceState.isClaimedRewardDay(dailyRewardList[i].Day));
        }

    }

}
