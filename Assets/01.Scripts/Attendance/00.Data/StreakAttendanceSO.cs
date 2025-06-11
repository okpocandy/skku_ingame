using UnityEngine;

[CreateAssetMenu(fileName = "StreakAttendanceSO", menuName = "Scriptable Objects/StreakAttendanceSO")]
public class StreakAttendanceSO : ScriptableObject
{
    [SerializeField]
    private int _streakDate;
    public int StreakDate => _streakDate;
    
    [SerializeField]
    private ECurrencyType _currencyType;
    public ECurrencyType CurrencyType => _currencyType;
    
    [SerializeField]
    private int _amount;
    public int Amount => _amount;
}
