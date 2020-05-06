using Doozy.Engine.Nody;
using Doozy.Engine.Progress;
using Doozy.Engine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxItemPresenter : MonoBehaviour
{
    [ContextMenuItem("ShowBrawlerPower", "ShowBrawlerPower")]

    private static BoxItemPresenter instance;

    public GraphController graphController;
    public UIButton NextItemButton;
    public UIView BoxView, BrawlerView, BrawlerPowerView, CoinsView, GemsView, TicketsView, ObtainedView, MainMenuView;
    public UIView BoxBGView, BrawlerBGView, BrawlerPowerBGView, CoinsBGView, GemsBGView, TicketsBGView, ObtainedBGView;
    public Progressor BrawlerPowerProgr, CoinsProgr, GemsProgr, TicketsProgr, RemaingProgr;
    public Progressor ObtCoinsProgr, ObtGemsProgr, ObtTicketsProgr;
    public Image BoxImage;
    public Action OnNext;
    public ObtainedBrawler ObtBrawler, ObtBrawlerTotal;
    public GameObject BonusRoot;
    public ObtainedBrawlerPower ObtBrawlerPower, ObtBrawlerPowerTotalPrefab;
    public Transform Obtained;
    public delegate void HideObtainedd();
    public static event HideObtainedd OnHideObtained;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        HideAllObtainedItems();
        ItemDistributor.OnGetItem += ShowItem;
    }

    public static void ShowObtained()
    {
        instance.HideAllItemViews();
        instance.graphController.GoToNodeByName("Obtained");
    }

    public static void HideObtained()
    {
        instance.graphController.GoToNodeByName("Main Menu");
        instance.HideAllObtainedItems();
        OnHideObtained?.Invoke();
    }

    private void HideAllObtainedItems()
    {
        ObtCoinsProgr.gameObject.SetActive(false);
        ObtGemsProgr.gameObject.SetActive(false);
        ObtTicketsProgr.gameObject.SetActive(false);
        ObtBrawlerTotal.gameObject.SetActive(false);
        BonusRoot.SetActive(false);
        ObtCoinsProgr.SetValue(0);
        ObtGemsProgr.SetValue(0);
        ObtTicketsProgr.SetValue(0);
    }

    public static void ShowItem(ItemData itemData)
    {
        instance.HideAllItemViews();

        switch (itemData.ItemType)
        {
            case ItemData.Type.Brawler:
                instance.ShowBrawler(itemData.Brawler);
                break;

            case ItemData.Type.BrawlerPower:
                instance.ShowBrawlerPower(itemData);
                break;

            case ItemData.Type.Coins:
                instance.ShowCoins(itemData.Count);
                break;

            case ItemData.Type.Gems:
                instance.ShowGems(itemData.Count);
                break;

            case ItemData.Type.Tickets:
                instance.ShowTickets(itemData.Count);
                break;
        }
    }

    private void ShowBrawler(Brawler b)
    {
        //BonusRoot.SetActive(true);
        instance.graphController.GoToNodeByName("Brawler");

        ObtBrawler.Show(b);
        //BrawlerView.Show();
        ObtBrawlerTotal.gameObject.SetActive(true);
        ObtBrawlerTotal.Show(b);
    }

    public static void ShowBox(Box box)
    {
        instance.graphController.GoToNodeByName("OpenBox");

        instance.BoxImage.sprite = box.BoxSprite;
    }

    private void ShowBrawlerPower(ItemData item)
    {
        graphController.GoToNodeByName("Brawler Power");
        ObtBrawlerPower.Show(item.Brawler, item.Count);
        Instantiate(ObtBrawlerPowerTotalPrefab.gameObject, Obtained).GetComponent<ObtainedBrawlerPower>().Show(item.Brawler, item.Count);
    }

    private void ShowCoins(int Count)
    {
        graphController.GoToNodeByName("Coins");
        CoinsProgr.SetValue(Count);
        ObtCoinsProgr.gameObject.SetActive(true);
        float val = ObtCoinsProgr.Value + Count;
        ObtCoinsProgr.SetMax(val);
        ObtCoinsProgr.SetValue(val);
    }

    private void ShowGems(int Count)
    {
        graphController.GoToNodeByName("Gems");
        GemsProgr.SetValue(Count);
        ObtGemsProgr.gameObject.SetActive(true);
        BonusRoot.SetActive(true);
        float val = ObtGemsProgr.Value + Count;
        ObtGemsProgr.SetMax(val);
        ObtGemsProgr.SetValue(val);
    }

    private void ShowTickets(int Count)
    {
        graphController.GoToNodeByName("Tickets");
        TicketsProgr.SetValue(Count);
        ObtTicketsProgr.gameObject.SetActive(true);
        BonusRoot.SetActive(true);
        float val = ObtTicketsProgr.Value + Count;
        ObtTicketsProgr.SetMax(val);
        ObtTicketsProgr.SetValue(val);
    }

    private void HideAllItemViews()
    {
        BoxView.Hide();
        BrawlerView.Hide();
        BrawlerPowerView.Hide();
        CoinsView.Hide();
        GemsView.Hide();
        TicketsView.Hide();
    }

    public static void OnClickNextSubscribe(Action Act)
    {
        instance.OnNext = Act;
    }

    public static void SetRemaingValue(int Value)
    {
        instance.RemaingProgr.SetValue(Value);
    }

    public void ClickNext() //UnityEventCall
    {
        OnNext.Invoke();
    }
}
