using System;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyRepository
{
    private const string SAVE_KEY = nameof(CurrencyRepository);

    public void Save(List<CurrencyDTO> dataList)
    {
        CurrencySaveDatas data = new CurrencySaveDatas();
        data.DataList = dataList.ConvertAll(x => new CurrencySaveData{Type = x.Type, Value = x.Value});

        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(SAVE_KEY, json);
    }

    public List<CurrencyDTO> Load()
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY))
        {
            return null;
        }

        string json = PlayerPrefs.GetString(SAVE_KEY);
        CurrencySaveDatas data = JsonUtility.FromJson<CurrencySaveDatas>(json);
        return data.DataList.ConvertAll<CurrencyDTO>(data => new CurrencyDTO(data.Type, data.Value));
    }
}

[Serializable]
public struct CurrencySaveData
{
    public ECurrencyType Type;
    public int Value;
}
[Serializable]
public class CurrencySaveDatas
{
    public List<CurrencySaveData> DataList;
}

