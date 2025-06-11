using UnityEngine;

[CreateAssetMenu(fileName = "DailyAttendanceSO", menuName = "Scriptable Objects/DailyAttendanceSO")]
public class DailyAttendanceSO : ScriptableObject
{
    [SerializeField]
    private int _day;
    public int Day => _day;
    
    [SerializeField]
    private ECurrencyType _currencyType;
    public ECurrencyType CurrencyType => _currencyType;
    
    [SerializeField]
    private int _amount;
    public int Amount => _amount;
}
