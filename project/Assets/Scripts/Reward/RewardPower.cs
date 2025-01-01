using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RewardPower : MonoBehaviour
{
    [SerializeField] float totalTime;
    float currentTime;
    [SerializeField] TextMeshProUGUI textTimer;
    [SerializeField] TextMeshProUGUI textGetPower;
    [SerializeField] Button gettedButton;
    [SerializeField] int addPower;
    [SerializeField] GameObject gettedImage;
    [SerializeField] TextMeshProUGUI textValue;

    bool getted = false;
    bool unlocked = false;
    void Start()
    {
        if (PlayerPrefs.GetInt("GettedPower", 0) == 1)
        {
            getted = true;
            textGetPower.enabled = true;
            textGetPower.text = "Получено";
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

        textValue.text = addPower.ToString();
    }


    public IEnumerator TimerReward()
    {

        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;



            int hours = Mathf.FloorToInt(currentTime / 3600); // 1 час = 3600 секунд
            int minutes = Mathf.FloorToInt((currentTime % 3600) / 60); // Остаток от часов, делим на 60 для получения минут
            int seconds = Mathf.FloorToInt(currentTime % 60); // Остаток от минут, получаем секунды


            textTimer.text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);

            yield return null;
        }

        Unlocked();
    }

    public void GettedPower()
    {
        if (unlocked && !getted)
        {
            getted = true;
            GamePLay.instance.AddPower(addPower);
            textGetPower.text = "Получено";
            PlayerPrefs.SetInt("GettedPower", 1);
            gettedButton.interactable = false;
            gettedImage.SetActive(true);

        }
    }

    void Unlocked()
    {
        textTimer.enabled = false;
        gettedButton.interactable = true;
        unlocked = true;
        textGetPower.enabled = true;
    }
}
