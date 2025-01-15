using KinematicCharacterController.Examples;
using YG;
using UnityEngine;

public class RewardBoostSpeed : MonoBehaviour
{
    public ExampleCharacterController playerController;
    public YandexGame _sdk;
    [SerializeField]float boostSpeed;


    private void Awake()
    {
        if(PlayerPrefs.HasKey("Boost"))
        {
            playerController.MaxStableMoveSpeed = PlayerPrefs.GetFloat("Boost");
        }
    }
    void Start()
    {
        
    }

    
    public void RewardBoost()
    {
        playerController.MaxStableMoveSpeed += boostSpeed;
        PlayerPrefs.SetFloat("Boost",playerController.MaxStableMoveSpeed);
        PlayerPrefs.Save();
    }
}
