using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class UI_Notification : MonoBehaviour
{
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI DescriptionText;
    public TextMeshProUGUI RewardTypeText;
    public TextMeshProUGUI RewardAmountText;
    public TextMeshProUGUI ClaimDateText;

    public void Start()
    {
        AchievementManager.Instance.OnNewAchievementClaimed += Show;
        gameObject.SetActive(false);
    }

    public void Show(AchievementDTO achievementDTO)
    {
        gameObject.SetActive(true);

        NameText.text = achievementDTO.Name;
        DescriptionText.text = achievementDTO.Description;
        RewardTypeText.text = achievementDTO.RewardCurrencyType.ToString();
        RewardAmountText.text = achievementDTO.RewardAmount.ToString();
        //ClaimDateText.text = achievementDTO.ClaimDate.ToString();

        StartCoroutine(HideAfterDelay());
    }

    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
