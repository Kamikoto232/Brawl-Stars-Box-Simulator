using Doozy.Engine.UI;
using TMPro;
using UnityEngine;

public class NickNameWindowPresenter : MonoBehaviour
{
    public UIView NickNameWindow, BG;
    public TMP_InputField NicknameInput;
    public TMP_Text NicknameText;
    public GameObject Warn;

    private void Start()
    {
        Warn.SetActive(false);

        if (string.IsNullOrWhiteSpace(PlayerDataModel.instance.Player.Name))
        {
            Open();
        }
        else NicknameText.text = PlayerDataModel.instance.Player.Name;
        NicknameInput.text = PlayerDataModel.instance.Player.Name;
    }

    public void SetNickName()
    {
        if (NicknameInput.text.Length < 2)
        {
            Warn.SetActive(true);
        }
        else
        {
            PlayerDataModel.instance.Player.Name = NicknameInput.text;
            Warn.SetActive(false);
            NicknameText.text = PlayerDataModel.instance.Player.Name;
            PlayerDataModel.instance.SaveData();
            Close();
        }
    }

    public void Open()
    {
        NickNameWindow.Show();
        BG.Show();
    }
    public void Close()
    {
        NickNameWindow.Hide();
        BG.Hide();
    }
}
