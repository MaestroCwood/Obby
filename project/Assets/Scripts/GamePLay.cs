using TMPro;
using UnityEngine;

public class GamePLay : MonoBehaviour
{
    public static GamePLay instance;

    public int coin;
    public int crystal;
    [SerializeField] TextMeshProUGUI textCoin;
    [SerializeField] TextMeshProUGUI textCrystal;

    private void Start()
    {
        instance = this;

        if (PlayerPrefs.HasKey("Coin"))
        {
            coin = PlayerPrefs.GetInt("Coin");
        }
        else coin = 0;

        if(PlayerPrefs.HasKey("Crystal"))
        {
            crystal = PlayerPrefs.GetInt("Crystal");
        } else crystal = 0;

        UpdateCountCoin();
        UpdateCrystal();
    }

    public void AddCoin(int coins)
    {
        coin += coins;
        PlayerPrefs.SetInt("Coin",coin);
        PlayerPrefs.Save();
        UpdateCountCoin();
    }

    public void UpdateCountCoin()
    {
        textCoin.text = coin.ToString();
    }

    public void UpdateCrystal()
    {
        textCrystal.text = crystal.ToString();
    }

    public void AddCrystal(int crystals)
    {
        crystal += crystals;
        PlayerPrefs.SetInt("Crystal", crystal);
        PlayerPrefs.Save();
        UpdateCrystal();
    }
}
