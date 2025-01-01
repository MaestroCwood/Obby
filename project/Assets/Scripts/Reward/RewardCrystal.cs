using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RewardCrystal : MonoBehaviour
{
    [SerializeField] float totalTime;
    float currentTime;
    [SerializeField] TextMeshProUGUI textTimer;
    [SerializeField] TextMeshProUGUI textGetCoins;
    [SerializeField] TextMeshProUGUI textValue;
    [SerializeField] Button gettedButton;
    [SerializeField] int addCrystal;
    [SerializeField] GameObject gettedImage;

    bool getted = false;
    bool unlocked = false;
    void Start()
    {
        if (PlayerPrefs.GetInt("GettedCrystal", 0) == 1)
        {
            getted = true;
            textGetCoins.enabled = true;
            textGetCoins.text = "Получено";
            gettedButton.interactable = false;
            gettedImage.SetActive(true);
            textTimer.enabled = false;
        }
        else
        {
            // Если награда ещё не получена, начинаем отсчёт
            currentTime = totalTime;
            StartCoroutine(TimerReward());
        }
        textValue.text = addCrystal.ToString();
    }


    public IEnumerator TimerReward()
    {

        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;



            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);


            textTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            yield return null;
        }

        Unlocked();
    }

    public void GettedCrystal()
    {
        if (unlocked && !getted)
        {
            getted = true;
            GamePLay.instance.AddCrystal(addCrystal);
            textGetCoins.text = "Получено";
            PlayerPrefs.SetInt("GettedCrystal", 1);
            gettedButton.interactable = false;
            gettedImage.SetActive(true);

        }
    }

    void Unlocked()
    {
        textTimer.enabled = false;
        gettedButton.interactable = true;
        unlocked = true;
        textGetCoins.enabled = true;
    }
}
