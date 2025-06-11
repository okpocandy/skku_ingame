using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_DailyAttendanceSlot : MonoBehaviour
{
    public TextMeshProUGUI DayText;
    public TextMeshProUGUI RewardText;

    public Image RewardIconImage;
    public Image BackgroundImage;
    public GameObject RewardCheck;

    public void Refresh(DailyAttendanceRewardDTO dailyAttendance, Sprite rewardIcon, bool isClaimed)
    {
        DayText.text = dailyAttendance.Day.ToString();
        RewardText.text = dailyAttendance.Amount.ToString();

        RewardIconImage.sprite = rewardIcon;
        RewardCheck.SetActive(isClaimed);

        // 누적 날짜가 보상 날짜보다 크거나 같고 보상을 받지 않았다면 검은색
        if(AttendanceManager.Instance.AttendanceState.TotalAttendanceCount >= dailyAttendance.Day && !isClaimed)
        {
            BackgroundImage.color = Color.black;
        }

    }

    public void ClaimReward()
    {
        // 트라이 클래임 리워드 실행해서
        // 성공하면 커렌시매니저로 보상 추가
        // 실패하면 ...
    }
}
