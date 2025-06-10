using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.FPS.Game;

public class AchievementManager : Singleton<AchievementManager>
{
    private List<Achievement> _achievementList;
    public List<Achievement> AchievementList => _achievementList;
    [SerializeField]
    private List<AchievementSO> _metaDataList;

    public event Action OnDataChanged;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _achievementList = new List<Achievement>();
        foreach (var metaData in _metaDataList)
        {
            _achievementList.Add(new Achievement(metaData));
        }
    }

    public void Increase(EAchievementCondition condition, int value)
    {
        foreach(var achievement in _achievementList)
        {
            if(achievement.Condition == condition)
            {
                achievement.Increase(value);
            }
        }

        OnDataChanged?.Invoke();
    }
        
}
