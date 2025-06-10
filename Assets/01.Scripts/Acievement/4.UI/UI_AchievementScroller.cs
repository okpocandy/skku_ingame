using UnityEngine;
using UnlimitedScrollUI;
using System.Collections.Generic;

public class UI_AchievementScroller : MonoBehaviour
{
    [SerializeField] private GameObject achievementSlotPrefab;
    [SerializeField] private VerticalUnlimitedScroller scroller;
    
    private List<AchievementDTO> achievements;
    private bool isInitialized = false;

    private void OnEnable()
    {
        if (AchievementManager.Instance != null)
        {
            AchievementManager.Instance.OnDataChanged += RefreshAllSlots;
        }
    }

    private void OnDisable()
    {
        if (AchievementManager.Instance != null)
        {
            AchievementManager.Instance.OnDataChanged -= RefreshAllSlots;
        }
    }

    private void Start()
    {
        InitializeScroller();
    }

    private void InitializeScroller()
    {
        if (isInitialized) return;
        
        achievements = AchievementManager.Instance.AchievementList;
        
        scroller.Generate(achievementSlotPrefab, achievements.Count, (index, cell) =>
        {
            var regularCell = cell as RegularCell;
            if (regularCell != null)
            {
                regularCell.onGenerated?.Invoke(index);
                var achievementSlot = regularCell.GetComponent<UI_AchievementSlot>();
                if (achievementSlot != null)
                {
                    achievementSlot.Refresh(achievements[index]);
                }
            }
        });

        isInitialized = true;
    }

    public void RefreshAllSlots()
    {
        if (!isInitialized) return;
        
        achievements = AchievementManager.Instance.AchievementList;
        
        for (int i = 0; i < achievements.Count; i++)
        {
            var cell = scroller.transform.GetChild(i);
            if (cell != null)
            {
                var achievementSlot = cell.GetComponent<UI_AchievementSlot>();
                if (achievementSlot != null)
                {
                    achievementSlot.Refresh(achievements[i]);
                }
            }
        }
    }
} 