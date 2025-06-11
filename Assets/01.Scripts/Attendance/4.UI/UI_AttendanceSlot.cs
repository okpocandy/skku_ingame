using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_AttendanceSlot : MonoBehaviour
{
    public TextMeshProUGUI DayText;
    public TextMeshProUGUI RewardText;

    public Image RewardIconImage;
    public Image BackgroundImage;
    public GameObject RewardCheck;

    public void Refresh(DailyAttendanceSO dailyAttendance, Sprite rewardIcon, bool isClaimed)
    {
        DayText.text = dailyAttendance.Day.ToString();
        RewardText.text = dailyAttendance.Amount.ToString();
        RewardIconImage.sprite = rewardIcon;
        RewardCheck.SetActive(isClaimed);

        // TODO :
        // 누적 일수가 SO에 날짜보다 크거나 같은데 보상을 받지 않았다면 배경색 흰색, 아니면 기본
        /*
        if(AttendanceManager.Instance.AttendanceState.TotalAttendanceCount >= dailyAttendance.Day && !isClaimed)
        {
            BackgroundImage.color = Color.white;
        }
        */
    }

    public void ClaimReward()
    {
        // 트라이 클래임 리워드 실행해서
        // 성공하면 커렌시매니저로 보상 추가
        // 실패하면 ...
    }
}
