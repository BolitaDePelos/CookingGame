using UnityEngine;

public class MoneyReader
{
    public int GetMoney() => PlayerPrefs.GetInt(KeyStorage.Money, 0);

    public bool CheckHasMoney(int amountCompare) => GetMoney() >= amountCompare;
}
