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

    public void Refresh(Achievement achievement)
    {
        NameTextUI.text = achievement.Name;
        DescriptionTextUI.text = achievement.Description;
        RewardCountTextUI.text = achievement.RewardAmount.ToString();
        CurrentValueTextUI.text = $"{achievement.CurrentValue}/{achievement.GoalValue}";
        ProgressSlider.value = (float)achievement.CurrentValue / achievement.GoalValue;

        // 등등...

    }
}
