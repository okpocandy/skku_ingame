using UnityEngine;

public class AttendanceStateRepository : MonoBehaviour
{
    private const string SAVE_KEY = nameof(AttendanceStateRepository);

    public void Save(AttendanceStateDTO data)
    {
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(SAVE_KEY, json);
    }

    public AttendanceStateDTO Load()
    {
        if(!PlayerPrefs.HasKey(SAVE_KEY))
        {
            return null;
        }

        string json = PlayerPrefs.GetString(SAVE_KEY);
        AttendanceStateDTO data = JsonUtility.FromJson<AttendanceStateDTO>(json);
        return data;
    }
}
