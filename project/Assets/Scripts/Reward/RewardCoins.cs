using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class RewardCoins : MonoBehaviour
{
    [SerializeField] float totalTime;
    float currentTime;  
    [SerializeField] TextMeshProUGUI textTimer;
    [SerializeField] TextMeshProUGUI textGetCoins;
    [SerializeField] Button gettedButton;
    [SerializeField] int addCoins;
    [SerializeField] GameObject gettedImage;
    [SerializeField] TextMeshProUGUI textValue;
     
    bool getted = false;
    bool unlocked = false; 

    void Start()
    {
       
        if (PlayerPrefs.GetInt("GettedCoin", 0) == 1)
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
        textValue.text = addCoins.ToString();
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

    public void GettedCoins()
    {
        if(unlocked && !getted)
        {
            getted = true;
            GamePLay.instance.AddCoin(addCoins);
            textGetCoins.text = "Получено";
            PlayerPrefs.SetInt("GettedCoin", 1);
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
