using UnityEngine;

public class StreakRewardRuleDTO
{
    public readonly int StreakDate;
    public readonly ECurrencyType CurrencyType;
    public readonly int Amount;

    public StreakRewardRuleDTO(StreakRewardRule rule)
    {
        StreakDate = rule.StreakDate;
        CurrencyType = rule.CurrencyType;
        Amount = rule.Amount;
    }

    public StreakRewardRule ToDomain()
    {
        return new StreakRewardRule(StreakDate, CurrencyType, Amount);
    }
}
