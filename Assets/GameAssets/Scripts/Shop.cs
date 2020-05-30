using Doozy.Engine.Soundy;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public static void ConvertToMoney(int amount, int rev)
    {
        if (PlayerDataModel.TakeGems(amount))
        {
            SoundyManager.Play("Game", "collect_coins_01");
            PlayerDataModel.AddCoins(rev);
        }
        else SoundyManager.Play("Game", "tap_normal_02");
    }

    public static void ConvertToCrystals(int amount, int rev)
    {
        if (PlayerDataModel.TakeCoins(amount))
        {
            SoundyManager.Play("Game", "buy_gems_01");
            PlayerDataModel.AddGems(rev);
        }
        else SoundyManager.Play("Game", "tap_normal_02");
    }
}