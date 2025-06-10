using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class AchievementRepository
{
    private const string SAVE_KEY = nameof(AchievementRepository);

    public void Save(List<AchievementDTO> dataList)
    {
        AchievementSaveDatas data = new AchievementSaveDatas();
        data.DataList = dataList.ConvertAll(x => new AchievementSaveData{
            ID = x.ID,
            CurrentValue = x.CurrentValue,
            RewardClaimed = x.RewardClaimed
        });

        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(SAVE_KEY, json);
    }

    public List<AchievementSaveData> Load()
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY))
        {
            return null;
        }

        string json = PlayerPrefs.GetString(SAVE_KEY);
        AchievementSaveDatas datas = JsonUtility.FromJson<AchievementSaveDatas>(json);
        return datas.DataList;
    }
    
}

[Serializable]
public struct AchievementSaveData
{
    public string ID;
    public int CurrentValue;
    public bool RewardClaimed;
}

[Serializable]
public class AchievementSaveDatas
{
    public List<AchievementSaveData> DataList;
}
