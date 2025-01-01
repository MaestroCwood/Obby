using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class RewardPets : MonoBehaviour
{
    [SerializeField] float totalTime;
    float currentTime;
    [SerializeField] TextMeshProUGUI textTimer;
    [SerializeField] TextMeshProUGUI textGetPets;
    [SerializeField] Button gettedButton;
    [SerializeField] FolovingPlayer folovingPlayer;
    [SerializeField] GameObject gettedImage;
    [SerializeField] TextMeshProUGUI textValue;

    [NonSerialized] public bool getted = false;
    bool unlocked = false;
    void Start()
    {
        if (PlayerPrefs.GetInt("GettedPet", 0) == 1)
        {
            getted = true;
            textGetPets.enabled = true;
            textGetPets.text = "Получено";
            gettedButton.interactable = false;
            gettedImage.SetActive(true);
            textTimer.enabled = false;
            folovingPlayer.enabled = true;
        }
        else
        {
            // Если награда ещё не получена, начинаем отсчёт
            currentTime = totalTime;
            StartCoroutine(TimerReward());
        }
       
    }

    public IEnumerator TimerReward()
    {

        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;



            int hours = Mathf.FloorToInt(currentTime / 3600); 
            int minutes = Mathf.FloorToInt((currentTime % 3600) / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60); 


            textTimer.text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);

            yield return null;
        }

        Unlocked();
    }

    public void GettedPets()
    {
        if (unlocked && !getted)
        {
            getted = true;
           
            textGetPets.text = "Получено";
            PlayerPrefs.SetInt("GettedPet", 1);
            gettedButton.interactable = false;
            gettedImage.SetActive(true);
            folovingPlayer.enabled = true;

        }
    }

    void Unlocked()
    {
        textTimer.enabled = false;
        gettedButton.interactable = true;
        unlocked = true;
        textGetPets.enabled = true;
    }
}
