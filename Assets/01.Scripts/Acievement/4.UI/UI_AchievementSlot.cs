using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_AchievementSlot : MonoBehaviour
{
    public TextMeshProUGUI NameTextUI;
    public TextMeshProUGUI DescriptionTextUI;
    public TextMeshProUGUI RewardCountTextUI;
    public TextMeshProUGUI CurrentValueTextUI;

    public Slider ProgressSlider;

    public TextMeshProUGUI RewardClaimDateTextUI;
    public Button RewardClaimButtion;

    private AchievementDTO _achievementDTO;

    public void Refresh(AchievementDTO achievement)
    {
        _achievementDTO = achievement;

        NameTextUI.text = achievement.Name;
        DescriptionTextUI.text = achievement.Description;
        RewardCountTextUI.text = achievement.RewardAmount.ToString();
        CurrentValueTextUI.text = $"{achievement.CurrentValue}/{achievement.GoalValue}";
        ProgressSlider.value = (float)achievement.CurrentValue / achievement.GoalValue;

        RewardClaimButtion.interactable = achievement.CanClaimReward();

        // 등등...

    }

    public void ClaimReward()
    {
        if(AchievementManager.Instance.TryClaimReward(_achievementDTO))
        {
            // 성공 효과
        }
        else
        {
            // 실패 효과
        }
    }
}
