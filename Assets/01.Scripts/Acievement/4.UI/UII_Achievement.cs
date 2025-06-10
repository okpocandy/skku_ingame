using System.Collections.Generic;
using UnityEngine;

public class UII_Achievement : MonoBehaviour
{
    [SerializeField]
    private List<UI_AchievementSlot> _slotList;

    private void Start()
    {
        Refresh();
        AchievementManager.Instance.OnDataChanged += Refresh;
    }

    private void Refresh()
    {
        List<AchievementDTO> achievements = AchievementManager.Instance.AchievementList;

        for(int i=0; i < achievements.Count; i++)
        {
            _slotList[i].Refresh(achievements[i]);
        }
    }
}
