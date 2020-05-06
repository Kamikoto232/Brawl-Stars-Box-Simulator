using UnityEngine;
using System.Collections;

public static class ItemDistributor
{
    public delegate void GetItem(ItemData itemData);
    public static event GetItem OnGetItem;

    public static void AddItem(ItemData itemData)
    {
        if (itemData.ItemType == ItemData.Type.Brawler && BrawlersInventory.HasBrawler(itemData.Brawler))
            itemData = new ItemData(ItemData.Type.BrawlerPower, 100, itemData.Brawler);
        OnGetItem(itemData);
    }
}