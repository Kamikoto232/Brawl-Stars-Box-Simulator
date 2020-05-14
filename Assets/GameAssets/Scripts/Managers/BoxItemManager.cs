using System.Collections.Generic;
using UnityEngine;

public class BoxItemManager : MonoBehaviour
{
    private ItemData[] itemDatas;
    private byte currentShowedItemIndex;
    private byte BoxOpenNumberToAds;

    private void Start()
    {
        BoxItemPresenter.OnClickNextSubscribe(NextItem);
    }

    private bool closeObtained;

    private void NextItem()
    {
        if (closeObtained)
        {
            if (Application.installMode != ApplicationInstallMode.DeveloperBuild || Application.installMode != ApplicationInstallMode.Editor)
                BoxOpenNumberToAds++;

            if (!PlayerDataModel.instance.Dev && BoxOpenNumberToAds > 9)
            {
                ADSManager.ShowInterstitialAds();
                BoxOpenNumberToAds = 0;
            }

            BoxItemPresenter.HideObtained();
            return;
        }

        if (currentShowedItemIndex > itemDatas.Length - 1)
        {
            BoxItemPresenter.ShowObtained();
            closeObtained = true;
        }
        else
        {
            ItemDistributor.AddItem(itemDatas[currentShowedItemIndex]);
        }

        currentShowedItemIndex++;
        BoxItemPresenter.SetRemaingValue(itemDatas.Length - currentShowedItemIndex);
    }

    public void OpenBox(Box box) //UnityEventCall
    {
        if (CheckPossibleOpenBox(box))
        {
            TakeCostFromBox(box);
            ItemsInBox = Random.Range(box.MinItems, box.MaxItems);
            BoxItemPresenter.ShowBox(box);
            Statistics.OpenBox(box);
            SetShowedItems(GetItemsFromBox(box.Items));
            PlayerDataModel.AddXp(box.XP);
        }
        else
        {
            ShowNotEnoughWindow(box);
        }
    }

    private bool CheckPossibleOpenBox(Box box)
    {
        switch (box.CostMethod)
        {
            case Box.CostType.Coins:
                return TitlePanelManager.TryTakeCoins(box.Cost);

            case Box.CostType.Gems:
                return TitlePanelManager.TryTakeGems(box.Cost);

            case Box.CostType.Free:
                return true;

            case Box.CostType.ADS:
                return ADSManager.IsRewardedAdReady();

            default:
                return false;
        }
    }

    private void TakeCostFromBox(Box box)
    {
        switch (box.CostMethod)
        {
            case Box.CostType.Coins:
                TitlePanelManager.TakeCoins(box.Cost);
                break;

            case Box.CostType.Gems:
                TitlePanelManager.TakeGems(box.Cost);
                break;
        }
    }

    private void ShowNotEnoughWindow(Box box)
    {
        switch (box.CostMethod)
        {
            case Box.CostType.Coins:
                WindowsManager.ShowCoinWarn();
                break;

            case Box.CostType.Gems:

            case Box.CostType.ADS:
                WindowsManager.ShowGemsWarn();
                break;
        }
    }

    private void SetShowedItems(ItemData[] itemDatas)
    {
        closeObtained = false;
        this.itemDatas = itemDatas;
        currentShowedItemIndex = 0;
        BoxItemPresenter.SetRemaingValue(itemDatas.Length);
    }

    private int ItemsInBox;
    private float totalChance;
    private bool wasBrawler = false;

    private ItemData[] GetItemsFromBox(BoxItemData[] boxItemData)
    {
        List<ItemData> items = new List<ItemData>();
        int tryingCount = 0;

        foreach (BoxItemData item in boxItemData)
        {
            totalChance += item.Chance;
        }

        while (items.Count < ItemsInBox)
        {
            tryingCount++;

            if (tryingCount > 40)
            {
                break;
            }

            for (int i = 0; i < boxItemData.Length; i++)
            {
                if (IsGetted(boxItemData[i]))
                {
                    ItemData newItem = GetItemData(boxItemData[i]);

                    if (newItem.ItemType != ItemData.Type.Brawler)
                    {
                        items.Add(newItem);
                    }
                    else
                    {
                        items.Add(newItem);
                        wasBrawler = true;
                    }
                }
            }
        }

        if (items.Count == 0)
        {
            Debug.LogError("߷߷ Что то пошло не так ߷߷");
        }

        wasBrawler = false;
        totalChance = 0;
        return items.ToArray();
    }

    private ItemData GetItemData(BoxItemData item) //генерируем инфу о выпавшем предмете
    {
        if (item.ItemType == ItemData.Type.Brawler)
            print("BRAWLER");

        ItemData i = new ItemData
        {
            Count = Random.Range(item.CountMin, item.CountMax),
            ItemType = item.ItemType,
        };

        if (item.ItemType == ItemData.Type.Brawler)
        {
            i.Brawler = BrawlersManager.GetBrawler();
        }
        else if (item.ItemType == ItemData.Type.BrawlerPower)
        {
            i.Brawler = BrawlersManager.GetOpenBrawler();
        }
        if (item.ItemType == ItemData.Type.Brawler && i.Brawler == null)
        {
            print(2132135555);
        }
        //if (i.Brawler) wasBrawler = true;

        return i; //когда выпадает бравлер то бравлер = нулл или нет..
    }

    private bool IsGetted(BoxItemData boxItemData)
    {
        float randomPoint = boxItemData.ItemType == ItemData.Type.Brawler
            ? GetBrawlerChance() + Random.Range(0, totalChance) : Random.Range(0, totalChance);

        return randomPoint <= boxItemData.Chance;
    }

    private float GetBrawlerChance() //߷߷߷߷߷߷
    {
        return -BrawlersManager.GetChance();
    }
}