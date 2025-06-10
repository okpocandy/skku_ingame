using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.FPS.Game;

public class AchievementManager : Singleton<AchievementManager>
{
    private List<Achievement> _achievementList;
    public List<AchievementDTO> AchievementList => _achievementList.ConvertAll(a => new AchievementDTO(a));
    [SerializeField]
    private List<AchievementSO> _metaDataList;
    private AchievementRepository _repository;
    public event Action OnDataChanged;
    public event Action<AchievementDTO> OnNewAchievementClaimed;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _achievementList = new List<Achievement>();

        foreach (var metaData in _metaDataList)
        {
            Achievement duplicatedAchievement = FindByID(metaData.ID);
            if(duplicatedAchievement != null)
            {
                throw new Exception($"업적 ID({metaData.ID})가 중복됩니다.");
            }
            _achievementList.Add(new Achievement(metaData));
        }

        _repository = new AchievementRepository();

        List<AchievementSaveData> loadedDataList = _repository.Load();
        if(loadedDataList != null)
        {
            foreach(var data in loadedDataList)
            {
                Achievement achievement = _achievementList.Find(a => a.ID == data.ID);
                if(achievement != null)
                {
                    achievement.SetCurrentValue(data.CurrentValue);
                    achievement.SetRewardClaimed(data.RewardClaimed);
                }
            }
        }
    }

    private Achievement FindByID(string id)
    {
        return _achievementList.Find(a => a.ID == id);
    }

    public void Increase(EAchievementCondition condition, int value)
    {
        foreach(var achievement in _achievementList)
        {
            if(achievement.Condition == condition)
            {
                bool prevCanClaimReward = achievement.CanClaimReward();
                achievement.Increase(value);

                bool canClaimReward = achievement.CanClaimReward();

                if(prevCanClaimReward == false && canClaimReward == true)
                {
                    // 새로운 리워드 보상이 가능할때
                    // 노티피케이션 띄어주기
                    // 이벤트를 실행행
                    OnNewAchievementClaimed?.Invoke(new AchievementDTO(achievement));
                }
            }
        }

        _repository.Save(AchievementList);

        OnDataChanged?.Invoke();
    }

    public bool TryClaimReward(AchievementDTO achievementDTO)
    {
        Achievement achievement = _achievementList.Find(a => a.ID == achievementDTO.ID);
        if(achievement == null)
        {
            return false;
        }

        if(achievement.TryClaimReward())
        {
            CurrencyManager.Instance.Add(achievement.RewardCurrencyType, achievement.RewardAmount);
            
            OnDataChanged?.Invoke();
            
            return true;
        }

        return false;
    }
        
}
