using Lean.Localization;
using UnityEngine;

[HelpURL("https://brawlstars.fandom.com/ru/wiki/%D0%97%D0%B0%D0%B3%D0%BB%D0%B0%D0%B2%D0%BD%D0%B0%D1%8F_%D1%81%D1%82%D1%80%D0%B0%D0%BD%D0%B8%D1%86%D0%B0")]

[CreateAssetMenu(fileName = "Brawler")]
public class Brawler : ScriptableObject
{
    public Sprite Icon;
    public string BrawlerName;

    public string LocalizedName
    {
        get
        {
            if (LeanLocalization.CurrentTranslations.ContainsKey(BrawlerName))
                return (string)LeanLocalization.CurrentTranslations[BrawlerName].Data;
            else return BrawlerName;
        }
        private set { }
    }

    public string LocalizedDescription
    {
        get
        {
            if (LeanLocalization.CurrentTranslations.ContainsKey(BrawlerName + "_Desc"))
                return (string)LeanLocalization.CurrentTranslations[BrawlerName + "_Desc"].Data;
            else return BrawlerName;
        }
        private set { }
    }

    public enum Rarity { Normal, Rare, SuperRare, Epic, Mythical, Legendary }

    public Rarity RarityType;

    [Header("Прибавляется каждый уровень")]
    public short Health;
    public short Damage, Super;

    [Space]
    public byte Practicality;
}