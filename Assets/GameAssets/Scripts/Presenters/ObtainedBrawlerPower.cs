using Doozy.Engine.Progress;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObtainedBrawlerPower : MonoBehaviour
{
    public Image BrawlerImg;
    public TMP_Text Name;
    public Progressor PowerCount;
    public bool destr = true;

    public void Show(Brawler brawler, int power)
    {
        Name.text = brawler.BrawlerName;
        BrawlerImg.sprite = brawler.Icon;
        PowerCount.InstantSetValue(power);
    }

    private void OnEnable()
    {
        BoxItemPresenter.OnHideObtained += ItemDistributor_OnGetItem;
    }

    private void OnDisable()
    {
        BoxItemPresenter.OnHideObtained -= ItemDistributor_OnGetItem;
    }

    private void ItemDistributor_OnGetItem()
    {
        if(destr) Destroy(gameObject);
    }
}