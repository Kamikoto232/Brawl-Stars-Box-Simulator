using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObtainedBrawler : MonoBehaviour
{
    public Image BrawlerImg;
    public TMP_Text Name;

    public void Show(Brawler brawler)
    {
        Name.text = brawler.BrawlerName;
        BrawlerImg.sprite = brawler.Icon;
    }
}