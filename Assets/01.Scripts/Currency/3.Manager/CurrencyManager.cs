using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CurrencyManager : Singleton<CurrencyManager>
{
    private CurrencyRepository _currencyRepository;
    private Dictionary<ECurrencyType, Currency> _currencyDict;

    // 도메인에 변화가 있을 때 호출되는 액션
    // 세분화가 필요하면 세분화하는게 좋다.
    // 무조건 하나하나 다 세분화 하는 것은 좋지 않다.
    public event Action OnDataChanged;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _currencyDict = new Dictionary<ECurrencyType, Currency>();
        // 레포지토리
        _currencyRepository = new CurrencyRepository();

        List<CurrencyDTO> loadedCurrencyList = _currencyRepository.Load();

        // 이후 열거형이 추가되더라도 알아서 잘 삽입되게 변경
        for(int i=0; i<(int)ECurrencyType.Count; i++)
        {
            // Currency 타입에 따라서 생성
            ECurrencyType type = (ECurrencyType)i;

            CurrencyDTO loadedCurrency = loadedCurrencyList?.Find(data => data.Type == type);

            Currency currency = new Currency(type, loadedCurrency != null ? loadedCurrency.Value : 0);

            _currencyDict.Add(type, currency);
        }
    }

    public void Add(ECurrencyType type, int value)
    {
        _currencyDict[type].Add(value);

        Debug.Log($"{type} : {_currencyDict[type].Value}");

        _currencyRepository.Save(ToDtoList());

        OnDataChanged?.Invoke();
    }

    public CurrencyDTO Get(ECurrencyType type)
    {
        return new CurrencyDTO(_currencyDict[type]);
    }

    public bool TryBuy(ECurrencyType type, int value)
    {
        if(!_currencyDict[type].TryBuy(value))
        {
            return false;
        }

        _currencyRepository.Save(ToDtoList());

        OnDataChanged?.Invoke();
        
        return true;
    }

    private List<CurrencyDTO> ToDtoList()
    {
        return _currencyDict.ToList().Select(x => new CurrencyDTO(x.Value)).ToList();
    }
}
