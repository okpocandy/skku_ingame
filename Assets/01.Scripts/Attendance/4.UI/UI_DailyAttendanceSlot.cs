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

        // 보상을 받지 않았다면 흰색, 받았다면 회색
        BackgroundImage.color = isClaimed ? Color.gray : Color.white;

    }

    public void ClaimReward()
    {
        // 트라이 클래임 리워드 실행해서
        // 성공하면 커렌시매니저로 보상 추가
        // 실패하면 ...
    }
}
