using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Doozy.Engine.Progress;

public class BrawlerCard : MonoBehaviour
{
    public Image Icon;
    public TMP_Text Name;
    public CanvasGroup CanvGroup;
    public Progressor Level, Power;
    private Brawler CurrBrawler;
    public Button Btn;
    public Image Locked;
    private BrawlerData brawlerDat;

    public void Init(Brawler brawler, BrawlerData brawlerData)
    {
        brawlerDat = brawlerData;
        Icon.sprite = brawler.Icon;
        Name.text = brawler.BrawlerName;

        if(brawlerData != null)
        {
            Locked.gameObject.SetActive(false);
            Power.SetValue(brawlerData.Power);
            Power.SetMax(brawlerData.Power);
            Level.SetValue(brawlerData.Level);
        }

        CurrBrawler = brawler;
        BrawlersInventory.OnAddBrawler += BrawlersInventory_OnAddBrawler;
        BrawlersInventory.OnAddPower += BrawlersInventory_OnAddPower;
    }

    private void BrawlersInventory_OnAddPower(Brawler brawler)
    {
        if (CurrBrawler.BrawlerName != brawler.BrawlerName) return;
        Power.SetMax(brawlerDat.Power);
        Power.SetValue(brawlerDat.Power);
    }

    private void BrawlersInventory_OnAddBrawler(Brawler brawler, BrawlerData brawlerData)
    {
        if (CurrBrawler.BrawlerName == brawler.BrawlerName)
        {
            brawlerDat = brawlerData;
            CanvGroup.enabled = false;
        }
    }
}