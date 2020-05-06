using UnityEngine;

public class Shop : MonoBehaviour
{
    public static void ConvertToMoney(int amount, int rev)
    {
        if (PlayerDataModel.TakeGems(amount))
            PlayerDataModel.AddCoins(rev);
    }

    public static void ConvertToCrystals(int amount, int rev)
    {
        if (PlayerDataModel.TakeCoins(amount))
            PlayerDataModel.AddGems(rev);
    }
}