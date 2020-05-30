[System.Serializable]
public struct ItemData
{
    public enum Type { Brawler, BrawlerPower, Tickets, Gems, Coins }

    public Type ItemType;

    [System.NonSerialized]
    public Brawler Brawler;

    public int Count;

    public ItemData(Type ItemType, int Count, Brawler BrawlerCard)
    {
        this.ItemType = ItemType;
        this.Count = Count;
        this.Brawler = BrawlerCard;
    }
}