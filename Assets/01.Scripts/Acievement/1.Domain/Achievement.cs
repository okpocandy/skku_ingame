using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public enum EAchievementCondition
{
    GoldCollect,
    DronKillCount,
    BossKillCount,
    PlayTime,
    Trigger,
}

public class Achievement
{
    // 최종 목적: 자기서술형

    // 데이터
    public readonly string ID;
    public readonly string Name;
    public readonly string Description;
    public readonly EAchievementCondition Condition;
    public int GoalValue;
    public ECurrencyType RewardCurrencyType;
    public int RewardAmount;

    // 상태
    private int _currentValue;
    public int CurrentValue => _currentValue;

    private bool _rewardClaimed;
    public bool RewardClaimed => _rewardClaimed;


    // 생성자
    public Achievement(AchievementSO metaData, AchievementDTO saveData)
    {
        if(string.IsNullOrEmpty(metaData.ID))
        {
            throw new Exception("업적 ID는 비어있을 수 없습니다.");
        }
        if(string.IsNullOrEmpty(metaData.Name))
        {
            throw new Exception("업적 이름은 비어있을 수 없습니다.");
        }
        if(string.IsNullOrEmpty(metaData.Description))
        {
            throw new Exception("업적 설명은 비어있을 수 없습니다.");
        }

        if(metaData.GoalValue <= 0)
        {
            throw new Exception("목표 값은 0보다 커야 합니다.");
        }
        if(metaData.RewardAmount <= 0)
        {
            throw new Exception("보상 값은 0보다 커야 합니다.");
        }
        if(saveData != null && saveData.CurrentValue < 0)
        {
            throw new Exception("현재 값은 0보다 커야 합니다. - Achievement");
        }

        ID = metaData.ID;
        Name = metaData.Name;
        Description = metaData.Description;
        Condition = metaData.Condition;
        GoalValue = metaData.GoalValue;
        RewardCurrencyType = metaData.RewardCurrencyType;
        RewardAmount = metaData.RewardAmount;

        if(saveData != null)
        {
            _currentValue = saveData.CurrentValue;
            _rewardClaimed = saveData.RewardClaimed;
        }

        
    }

    public void Increase(int value)
    {
        if(value <= 0)
        {
            throw new Exception("증가할 값은 0보다 커야 합니다. - Increase");
        }
        _currentValue += value;
    }

    public bool CanClaimReward()
    {
        return _currentValue >= GoalValue && !RewardClaimed;
    }

    public bool TryClaimReward()
    {
        if(!CanClaimReward())
        {
            return false;
        }
        _rewardClaimed = true;
        return true;
    }
}
