using System.Collections;
using KinematicCharacterController.Examples;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BuyBoost : MonoBehaviour
{
    
    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] int priceBoost;
    [SerializeField]float timerBoost;
    float startTimer;
    float speedPlayer;
    [SerializeField] int boostRange;
    [SerializeField] public ExampleCharacterController playerController;
    bool isActive = false;
    void Start()
    {
        priceText.text = priceBoost.ToString();
        startTimer = timerBoost;
        speedPlayer = playerController.MaxStableMoveSpeed;
    }

    
    void Update()
    {
        
    }

    public void ButBoost()
    {   if (isActive)
            return;
       if(GamePLay.instance.power >= priceBoost)
        {
            playerController.MaxStableMoveSpeed += boostRange;
            StartCoroutine(nameof(TimerBoost));
            GamePLay.instance.power -= priceBoost;
            PlayerPrefs.SetInt("Power", GamePLay.instance.power);
            PlayerPrefs.Save();
            GamePLay.instance.UpdatePowerText();
            isActive = true;
        }
       
    }

    IEnumerator TimerBoost()
    {
       

        while(timerBoost >= 0f)
        {
            priceText.text = timerBoost.ToString();
            timerBoost--;
            yield return new WaitForSeconds(1f);
        }
        isActive = false;
        timerBoost = startTimer;
        priceText.text = priceBoost.ToString();
        playerController.MaxStableMoveSpeed -= boostRange;
    }
}
