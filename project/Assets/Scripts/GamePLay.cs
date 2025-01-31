
using TMPro;
using UnityEngine;

public class GamePLay : MonoBehaviour
{
    public static GamePLay instance;

    public int coin;
    public int crystal;
    public int power;
    [SerializeField] TextMeshProUGUI textCoin;
    [SerializeField] TextMeshProUGUI textCrystal;
    [SerializeField] TextMeshProUGUI powerText;
    [SerializeField] ParticleSystem particleSystem;

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

        if (PlayerPrefs.HasKey("Power"))
        {
            power = PlayerPrefs.GetInt("Power");
        }
        else power = 0;

        UpdateCountCoin();
        UpdateCrystal();
        UpdatePowerText();
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
        PlayerPrefs.SetInt("Crystal", crystal);
        PlayerPrefs.Save();
    }

    public void AddCrystal(int crystals)
    {
        crystal += crystals;
       
        UpdateCrystal();
    }

    public void AddPower (int count)
    {
        power += count;
        particleSystem.Play();
        AudioMagager.Instance.SoundFx(2);
        PlayerPrefs.SetInt("Power", power);
        PlayerPrefs.Save();
        UpdatePowerText();
    }

    public void UpdatePowerText()
    {
        powerText.text = power.ToString();
    }
}
