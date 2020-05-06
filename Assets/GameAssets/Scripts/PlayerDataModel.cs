using UnityEngine;
using System.Collections;
using System;
//using EasyMobile;

public class PlayerDataModel : MonoBehaviour
{
    public static PlayerDataModel instance;
    public CurrenciesData Currencies = new CurrenciesData();
    public PlayerData Player = new PlayerData();
    public static event Action<int> OnNewLevel;
    public bool Dev;

    private void Awake()
    {
        instance = this;
        LoadData();
    }

    private void Start()
    {
        
    }

    public static int GetCoins()
    {
        if (instance.Dev) return int.MaxValue;
        return instance.Currencies.Coins;
    }

    public static int GetGems()
    {
        if (instance.Dev) return int.MaxValue;

        return instance.Currencies.Gems;
    }

    public static int GetTickets()
    {
        return instance.Currencies.Tickets;
    }

    public static int GetXp()
    {
        return instance.Player.XP;
    }

    public static int GetLevel()
    {
        return instance.Player.Level;
    }

    public static void AddCoins(int Value)
    {
        instance.Currencies.Coins += Value;
        instance.SaveData();
    }

    public static void AddGems(int Value)
    {
        instance.Currencies.Gems += Value;
        instance.SaveData();
    }

    public static void AddTickets(int Value)
    {
        instance.Currencies.Tickets += Value;
        instance.SaveData();
    }

    public static void AddXp(int Value)
    {
        instance.Player.XP += Value;
        instance.NextLevelCheck();
        instance.SaveData();
    }

    private void NextLevelCheck()
    {
        int lvl = (GetXp() / 50) + 1;
        if (lvl != GetLevel())
            SetLevel(lvl);
    }

    public static float GetNextLevelXP()
    {
        return GetLevel() > 0? GetLevel () * 50 : 50;
    }

    public static float GetPreviousLevelXP()
    {
        return GetLevel() != 0 ? (GetLevel() - 1) * 50 : 0;
    }

    public static void SetXp(int Value)
    {
        instance.Player.XP = Value;
        instance.SaveData();
    }

    public void SetLevel(int value)
    {
        instance.Player.Level = value;
        OnNewLevel?.Invoke(value);
    }

    public static bool TakeCoins(int Value)
    {
        if(GetCoins() - Value >= 0)
        {
            instance.Currencies.Coins -= Value;
            instance.SaveData();
            return true;
        }
        else return false;
    }

    public static bool TakeGems(int Value)
    {
        if(GetGems() - Value >= 0)
        {
            instance.Currencies.Gems -= Value;
            instance.SaveData();
            return true;
        }
        else return false;
    }

    public static bool TakeTickets(int Value)
    {
        if(GetTickets() - Value >= 0)
        {
            instance.Currencies.Tickets -= Value;
            instance.SaveData();
            return true;
        }
        else return false;
    }

    public void SaveData()
    {
        PlayerPrefs.SetString("Currensies", JsonUtility.ToJson(Currencies));
        PlayerPrefs.SetString("Player", JsonUtility.ToJson(Player));
        UpdateView();
    }

    private void LoadData()
    {
        if (HasSave())
        {
            Currencies = JsonUtility.FromJson<CurrenciesData>(PlayerPrefs.GetString("Currensies"));
            Player = JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString("Player"));
        }

        UpdateView();
    }

    private void UpdateView()
    {
        TitlePanelPresenter.SetCoinsValue(GetCoins());
        TitlePanelPresenter.SetGemsValue(GetGems());
        TitlePanelPresenter.SetTicketsValue(GetTickets());
        TitlePanelPresenter.SetXpValue(GetXp());
        TitlePanelPresenter.SetLevelValue(GetLevel());
    }

    private bool HasSave()
    {
        return PlayerPrefs.HasKey("Currensies");
    }
}