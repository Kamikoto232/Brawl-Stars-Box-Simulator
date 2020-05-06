using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BrawlersManager : MonoBehaviour
{
    public static BrawlersManager instance;
    private float luckyFactor;

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ItemDistributor.OnGetItem += ChangeLucky;
    }

    private void ChangeLucky(ItemData itemData)
    {
        if (itemData.ItemType == ItemData.Type.Brawler)
        {
            switch (itemData.Brawler.RarityType)
            {
                case Brawler.Rarity.Normal:
                    luckyFactor -= 0.001f;
                    break;
                case Brawler.Rarity.Rare:
                    luckyFactor -= 0.002f;
                    break;
                case Brawler.Rarity.SuperRare:
                    luckyFactor -= 0.003f;
                    break;
                case Brawler.Rarity.Epic:
                    luckyFactor -= 0.005f;
                    break;
                case Brawler.Rarity.Mythical:
                    luckyFactor -= 0.008f;
                    break;
                case Brawler.Rarity.Legendary:
                    luckyFactor -= 0.01f;
                    break;
            }
        }
        else
        {
            luckyFactor += 0.075f;
        }
    }

    public static float GetChance()
    {
        float chance = instance.luckyFactor;
        return chance;
    }

    public static Brawler GetOpenBrawler()
    {
        string name = BrawlersInventory.instance.inventory.OpenBrawlerDatas[Random.Range(0, BrawlersInventory.instance.inventory.OpenBrawlerDatas.Count)].BrawlerName;
        return BrawlersInventory.instance.brawlers.First(br => br.BrawlerName == name);
    }

    public static Brawler GetBrawler()
    {
        float chance = instance.luckyFactor;
        Brawler brawler = GetRandomBrawler(Brawler.Rarity.Normal);

        chance += Random.value;
        int trying = 0;
        do
        {
            if (chance > 1.15f)
                brawler = GetRandomBrawler(Brawler.Rarity.Rare);
            if (chance > 1.17f)
                brawler = GetRandomBrawler(Brawler.Rarity.SuperRare);
            if (chance > 1.2f)
                brawler = GetRandomBrawler(Brawler.Rarity.Epic);
            if (chance > 1.22f)
                brawler = GetRandomBrawler(Brawler.Rarity.Mythical);
            if (chance > 1.23f)
                brawler = GetRandomBrawler(Brawler.Rarity.Legendary);
            trying++;
        }
        while (trying < 15 && BrawlersInventory.HasBrawler(brawler));
        if(brawler == null)
        {
            print(1323);
        }
        return brawler; //тут работает ок
    }

    private static Brawler GetRandomBrawler(Brawler.Rarity rarity)
    {
        switch (rarity)
        {
            case Brawler.Rarity.Normal:
                instance.luckyFactor -= 0.2f;
                break;

            case Brawler.Rarity.Rare:
                instance.luckyFactor -= 0.32f;
                break;

            case Brawler.Rarity.SuperRare:
                instance.luckyFactor -= 0.4f;
                break;

            case Brawler.Rarity.Epic:
                instance.luckyFactor -= 0.45f;
                break;

            case Brawler.Rarity.Mythical:
                instance.luckyFactor -= 0.5f;
                break;

            case Brawler.Rarity.Legendary:
                instance.luckyFactor -= 0.55f;
                break;
        }

        List<Brawler> brawlersSorted = BrawlersInventory.instance.brawlers.Where(b => !BrawlersInventory.HasBrawler(b) && b.RarityType == rarity).ToList();
        if(brawlersSorted.Count == 0)
        {
            return BrawlersInventory.instance.brawlers[Random.Range(0, BrawlersInventory.instance.brawlers.Length)];
        }
        else
        {
            return brawlersSorted[Random.Range(0, brawlersSorted.Count)];
        }
    }
}
