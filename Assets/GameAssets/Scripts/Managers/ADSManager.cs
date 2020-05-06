using System.Collections;
using UnityEngine;

//using EasyMobile;
using UnityEngine.Advertisements;

public class ADSManager : MonoBehaviour
{
    private static ADSManager instance;

    private void Awake()
    {
        instance = this;

        if (Advertisement.isSupported)
            Advertisement.Initialize("3212016", false);
    }

    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(420);
            Advertisement.Show();
        }
    }

    public static void HideBanner()     //TODO:сделать показ баннеров в определенных местах
    {
        //Advertising.HideBannerAd(BannerAdNetwork.UnityAds, AdPlacement.Default);
    }

    public static void ShowBannerLeft()
    {
        //Advertising.ShowBannerAd(BannerAdNetwork.UnityAds, BannerAdPosition.BottomLeft, BannerAdSize.Banner);
    }

    public enum RewardType { Coins, Gems, Tickets, None }

    private int countToAdd;
    //public static void ShowRewardedAds(RewardType rewardType, int Count)
    //{
    //    if (IsRewardedAdReady() == false) return;

    //    instance.countToAdd = Count;
    //    switch (rewardType)
    //    {
    //        case RewardType.Coins:
    //            Advertising.RewardedAdCompleted += instance.RewardCoins;
    //            break;

    //        case RewardType.Gems:
    //            Advertising.RewardedAdCompleted += instance.RewardGems;
    //            break;

    //        case RewardType.Tickets:
    //            Advertising.RewardedAdCompleted += instance.RewardTickets;
    //            break;
    //    }

    //    Advertising.ShowRewardedAd(RewardedAdNetwork.UnityAds, AdPlacement.Default);
    //}

    public void ShowRewardedCoinsAds(int Count)
    {
        instance.countToAdd = Count;

        if (IsRewardedAdReady())
        {
            Advertisement.Show();
            PlayerDataModel.AddCoins(countToAdd);
        }
    }

    public void ShowRewardedGemsAds(int Count)
    {
        instance.countToAdd = Count;

        if (IsRewardedAdReady())
        {
            Advertisement.Show();
            PlayerDataModel.AddGems(countToAdd);
        }
    }

    public void ShowRewardedTicketsAds(int Count)
    {
        instance.countToAdd = Count;

        if (IsRewardedAdReady())
        {
            Advertisement.Show("rewardedVideo");
            PlayerDataModel.AddTickets(countToAdd);
        }
    }

    //private void RewardCoins(RewardedAdNetwork rewardedAdNet, AdPlacement adPlacement)
    //{
    //    PlayerDataModel.AddCoins(countToAdd);
    //    Advertising.RewardedAdCompleted -= RewardCoins;
    //}

    //private void RewardGems(RewardedAdNetwork rewardedAdNet, AdPlacement adPlacement)
    //{
    //    PlayerDataModel.AddGems(countToAdd);
    //}

    //private void RewardTickets(RewardedAdNetwork rewardedAdNet, AdPlacement adPlacement)
    //{
    //    PlayerDataModel.AddTickets(countToAdd);
    //}

    public static void ShowInterstitialAds()
    {
        if (IsInterstitialAdReady())
        {
            Advertisement.Show("video");

            //Advertising.ShowInterstitialAd(InterstitialAdNetwork.UnityAds, AdPlacement.Default);
        }
    }

    public static bool IsRewardedAdReady()
    {
        return Advertisement.IsReady("rewardedVideo"); //Advertising.IsRewardedAdReady(RewardedAdNetwork.UnityAds, AdPlacement.Default);
    }

    public static bool IsInterstitialAdReady()
    {
        //        Advertising.LoadInterstitialAd();

        return Advertisement.IsReady("video"); // Advertising.IsInterstitialAdReady(InterstitialAdNetwork.UnityAds, AdPlacement.Default);
    }
}