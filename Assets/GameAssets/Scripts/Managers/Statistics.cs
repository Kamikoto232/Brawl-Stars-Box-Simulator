using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Statistics : MonoBehaviour
{
    public StatisticsData statisticsData;
    public static Statistics i;
    public Box Free, Big, Mega, Epic;
    public TMP_Text FreeBoxOpenT, BigBoxOpenT, MegaBoxOpenT, EpicBoxOpenT, TotalOpenBoxesT;

    private void Awake()
    {
        i = this;
    }

    void Start()
    {
        LoadData();
        UpdateView();
    }

    public void SaveData()
    {
        PlayerPrefs.SetString("Statistics", JsonUtility.ToJson(statisticsData));
        UpdateView();
    }

    private void LoadData()
    {
        if (HasSave())
        {
            statisticsData = JsonUtility.FromJson<StatisticsData>(PlayerPrefs.GetString("Statistics"));
        }

        UpdateView();
    }

    private bool HasSave()
    {
        return PlayerPrefs.HasKey("Statistics");
    }

    public static void OpenBox(Box box)
    {
        i.Openbox(box);
    }

    private void Openbox(Box box)
    {
        if (box == Free) statisticsData.FreeBoxOpen++;
        if (box == Big) statisticsData.BigBoxOpen++;
        if (box == Mega) statisticsData.MegaBoxOpen++;
        if (box == Epic) statisticsData.EpicBoxOpen++;
        statisticsData.TotalBoxOpen++;
        UpdateView();
        SaveData();
    }

    private void UpdateView()
    {
        FreeBoxOpenT.text = statisticsData.FreeBoxOpen.ToString();
        BigBoxOpenT.text = statisticsData.BigBoxOpen.ToString();
        EpicBoxOpenT.text = statisticsData.EpicBoxOpen.ToString();
        MegaBoxOpenT.text = statisticsData.MegaBoxOpen.ToString();
        TotalOpenBoxesT.text = statisticsData.TotalBoxOpen.ToString();
    }
}

[System.Serializable]
public struct StatisticsData
{
    public int TotalBoxOpen, FreeBoxOpen, BigBoxOpen, MegaBoxOpen, EpicBoxOpen;
}