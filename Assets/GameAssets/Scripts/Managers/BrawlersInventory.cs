using Doozy.Engine.Progress;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BrawlersInventory : MonoBehaviour
{
    public static BrawlersInventory instance;
    public Brawler[] brawlers { get; private set; }
    public InventoryData inventory;
    public BrawlerCard BrawlerCardPrefab;
    public Transform NormalRoot, RareRoot, SuperRareRoot, EpicRoot, MythicalRoot, LegendaryRoot;
    public Progressor OpenBrawlersCountProg, TotalBrawlersCount;

    public static event Action<Brawler, BrawlerData> OnAddBrawler;
    public static event Action<Brawler> OnAddPower;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        brawlers = Resources.LoadAll<Brawler>("");
        inventory = new InventoryData(new List<BrawlerData> { new BrawlerData(brawlers.First(b => b.BrawlerName == "Shelly")) }) ;
        ItemDistributor.OnGetItem += ItemDistributor_OnGetItem;
        Load();
        Init();
        UpdateBrawlersCount();
    }

    private void Init()
    {
        Brawler[] Normal = brawlers.Where(x => x.RarityType == Brawler.Rarity.Normal).ToArray();
        Brawler[] Rare = brawlers.Where(x => x.RarityType == Brawler.Rarity.Rare).ToArray();
        Brawler[] SuperRare = brawlers.Where(x => x.RarityType == Brawler.Rarity.SuperRare).ToArray();
        Brawler[] Epic = brawlers.Where(x => x.RarityType == Brawler.Rarity.Epic).ToArray();
        Brawler[] Mythical = brawlers.Where(x => x.RarityType == Brawler.Rarity.Mythical).ToArray();
        Brawler[] Legendary = brawlers.Where(x => x.RarityType == Brawler.Rarity.Legendary).ToArray();

        InstantiateCard(Normal, NormalRoot);
        InstantiateCard(Rare, RareRoot);
        InstantiateCard(SuperRare, SuperRareRoot);
        InstantiateCard(Epic, EpicRoot);
        InstantiateCard(Mythical, MythicalRoot);
        InstantiateCard(Legendary, LegendaryRoot);
    }

    private void InstantiateCard(Brawler[] brawlers, Transform root)
    {
        for (int i = 0; i < brawlers.Length; ++i)
        {
            Instantiate(BrawlerCardPrefab.gameObject, root).GetComponent<BrawlerCard>().Init(brawlers[i], GetOpenBrawler(brawlers[i]));
        }
    }

    private void ItemDistributor_OnGetItem(ItemData itemData)
    {
        if ((itemData.ItemType == ItemData.Type.BrawlerPower || itemData.ItemType == ItemData.Type.Brawler) && itemData.Brawler)
        {
            BrawlerData brawlerData = GetOpenBrawler(itemData.Brawler);

            if (brawlerData == null)
            {
                AddNewBrawler(itemData.Brawler);
                Save();
            }
            else
            {
                brawlerData.AddPower(itemData.Count);
                OnAddPower?.Invoke(itemData.Brawler);
                Save();
            }
        }
    }

    private void AddNewBrawler(Brawler brawler)
    {
        inventory.Add(brawler);
        OnAddBrawler(brawler, inventory.OpenBrawlerDatas.Find((br) => br.BrawlerName == brawler.BrawlerName));
        UpdateBrawlersCount();
    }

    public static BrawlerData GetOpenBrawler(Brawler b)
    {
        return instance.inventory.OpenBrawlerDatas.Find((br) => br.BrawlerName == b.BrawlerName);
    }

    public static bool HasBrawler(Brawler b)
    {
        // bool has;
        BrawlerData brawlerData = null;
        if (b == null)
        {
            return false;
        }

        brawlerData = instance.inventory.OpenBrawlerDatas.Find((br) => br.BrawlerName == b.BrawlerName);
        
        // if (brawlerData != null)
        //   has = !string.IsNullOrEmpty(instance.inventory.OpenBrawlerDatas.Find((br) => br.BrawlerName == b.BrawlerName).BrawlerName); //true мб пофиксит
        //  else has = false;
        return brawlerData != null;
    }

    public static void BrawlerLevelUp(Brawler brawler)
    {
        GetOpenBrawler(brawler).LevelUp();
        instance.Save();
    }

    private void UpdateBrawlersCount()
    {
        OpenBrawlersCountProg.SetMax(inventory.OpenBrawlerDatas.Count);
        OpenBrawlersCountProg.InstantSetValue(inventory.OpenBrawlerDatas.Count);
        TotalBrawlersCount.SetMax(brawlers.Length);
        TotalBrawlersCount.SetValue(brawlers.Length);
    }

    private void Save()
    {
        PlayerPrefs.SetString("sav", JsonUtility.ToJson(inventory));
    }

    private void Load()
    {
        if (PlayerPrefs.HasKey("sav"))
            JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("sav"), inventory);
    }
}

[System.Serializable]
public struct InventoryData
{
    public List<BrawlerData> OpenBrawlerDatas;

    public InventoryData(List<BrawlerData> openBrawlerDatas)
    {
        OpenBrawlerDatas = openBrawlerDatas;
    }

    public void Add(Brawler brawler)
    {
        OpenBrawlerDatas.Add(new BrawlerData(brawler));
    }
}

[System.Serializable]
public class BrawlerData
{
    public string BrawlerName;
    public int Power;
    public int Level;

    public BrawlerData(Brawler brawler)
    {
        BrawlerName = brawler.BrawlerName;
    }

    public void AddPower(int count)
    {
        Power += count;
    }

    public void LevelUp()
    {
        Level++;
    }
}
