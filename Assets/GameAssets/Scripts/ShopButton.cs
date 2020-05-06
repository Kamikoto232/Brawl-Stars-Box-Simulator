using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    private Button button;
    public bool ToCrystal;
    public int Amount, Revenue;
    public TextMeshProUGUI Rev, Amnt;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Click);
    }

    private void Click()
    {
        if (ToCrystal)
            Shop.ConvertToCrystals(Amount, Revenue);
        else
            Shop.ConvertToMoney(Amount, Revenue);
    }

    private void OnValidate()
    {
        Rev.text = Revenue.ToString();
        Amnt.text = Amount.ToString();
    }
}