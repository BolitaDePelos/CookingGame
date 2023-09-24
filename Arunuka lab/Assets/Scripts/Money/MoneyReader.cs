using UnityEngine;

public class MoneyReader
{
    public int GetMoney()
    {
        return PlayerPrefs.GetInt(SaveProperties.ScoreProperty, 0);
    }

    public bool CheckHasMoney(int amountCompare)
    {
        return GetMoney() >= amountCompare;
    }
}