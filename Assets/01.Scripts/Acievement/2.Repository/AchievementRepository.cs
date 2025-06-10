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

    public List<AchievementDTO> Load()
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY))
        {
            return null;
        }

        string json = PlayerPrefs.GetString(SAVE_KEY);
        AchievementSaveDatas datas = JsonUtility.FromJson<AchievementSaveDatas>(json);
        
        List<AchievementDTO> dtoList = new List<AchievementDTO>();

        foreach(var data in datas.DataList)
        {
            dtoList.Add(new AchievementDTO(data.ID, data.CurrentValue, data.RewardClaimed));
        }
        
        return dtoList;
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
