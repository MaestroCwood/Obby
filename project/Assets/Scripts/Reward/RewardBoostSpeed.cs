using KinematicCharacterController.Examples;
using YG;
using UnityEngine;

public class RewardBoostSpeed : MonoBehaviour
{
    public ExampleCharacterController playerController;
 
    [SerializeField]float boostSpeed;

    private void Awake()
    {   
        if(PlayerPrefs.HasKey("Boost"))
        {
            playerController.MaxStableMoveSpeed = PlayerPrefs.GetFloat("Boost");
        }
    }

    
    public void RewardBoost()
    {
        playerController.MaxStableMoveSpeed += boostSpeed;
        PlayerPrefs.SetFloat("Boost",playerController.MaxStableMoveSpeed);
        PlayerPrefs.Save();
    }

    // Подписываемся на событие открытия рекламы в OnEnable
    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += Rewarded;
    }

    // Отписываемся от события открытия рекламы в OnDisable
    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= Rewarded;
    }

    // Подписанный метод получения награды
    public void Rewarded(int id)
    {
        // Если ID = 1, то выдаём "+100 монет"
        if (id == 2)
            RewardBoost();

    }

    // Метод для вызова видео рекламы
    public void ExampleOpenRewardAd(int id)
    {
        // Вызываем метод открытия видео рекламы
        YandexGame.RewVideoShow(id);
    }

}
