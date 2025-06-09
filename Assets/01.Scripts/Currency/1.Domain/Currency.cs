using System;

public enum ECurrencyType
{
    Gold,
    Diamond,


    Count,
}

public class Currency
{
    private ECurrencyType _type;
    public ECurrencyType Type => _type;
    private int _value = 0;
    public int Value => _value;

    public Currency(ECurrencyType type, int value)
    {
        // 에러를 발생시켜야 한다. 다른 곳으로 책임을 전가하면 안된다.
        if(value <0)
        {
            throw new Exception("value는 0보다 작을 수 없습니다.");
        }

        _type = type;
        _value = value;
    }

    // 모든 규칙을 도메인 내에서 작성해야 한다.
    // ex) 값이 음수면 오류를 발생시킨다.
    public void Add(int addedValue)
    {
        if(addedValue < 0)
        {
            throw new Exception("추가 값은 음수가 될 수 없다.");
        }

        _value += addedValue;
    }

    public void Subtract(int subtractedValue)
    {
        if(subtractedValue < 0)
        {
            throw new Exception("추가 값은 음수가 될 수 없다.");
        }

        if(_value < subtractedValue)
        {
            throw new Exception("보유량보다 큰 값을 차감할 수 없다.");
        }

        _value -= subtractedValue;
    }

    public bool TryBuy(int value)
    {
        if(value < 0)
        {
            throw new Exception("차감 값은 음수가 될 수 없다.");
        }

        if(_value < value)
        {
            return false;
        }

        _value -= value;    // 샀다다

        return true;        // 샀다 성공
    }
}
