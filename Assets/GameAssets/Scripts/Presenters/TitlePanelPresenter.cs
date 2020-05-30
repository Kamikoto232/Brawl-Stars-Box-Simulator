using UnityEngine;
using System.Collections;
using Doozy.Engine.Progress;

public class TitlePanelPresenter : MonoBehaviour
{
    private static TitlePanelPresenter instance;
    public Progressor CoinsProg, GemsProg, TicketsProg, XpProg, LevelProg;

    private void Awake()
    {
        instance = this;
    }

    public static void SetCoinsValue(int Value)
    {
        instance.CoinsProg.SetMax(100000);
        instance.CoinsProg.SetValue(Value);
    }

    public static void SetGemsValue(int Value)
    {
        instance.GemsProg.SetMax(100000);
        instance.GemsProg.SetValue(Value);
    }

    public static void SetTicketsValue(int Value)
    {
        instance.TicketsProg.SetMax(100000);
        instance.TicketsProg.SetValue(Value);
    }

    public static void SetXpValue(int Value)
    {
        instance.XpProg.SetMin(PlayerDataModel.GetPreviousLevelXP());
        instance.XpProg.SetMax(PlayerDataModel.GetNextLevelXP());
        instance.XpProg.SetValue(Value);
    }

    public static void SetLevelValue(int Value)
    {
        instance.LevelProg.SetMax(Value);
        instance.LevelProg.SetValue(Value);
    }
}
