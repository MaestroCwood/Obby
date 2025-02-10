using KinematicCharacterController.Examples;
using TMPro;
using UnityEngine;

public class JumpBoost : MonoBehaviour
{
    [SerializeField] public ExampleCharacterController playerController;
    [SerializeField] float addBoostJump;
    [SerializeField] TextMeshProUGUI textCurrentJump;

    public static JumpBoost Instance;
    private void Awake()
    {   
        Instance = this;

        if(PlayerPrefs.HasKey("Jump"))
        {
            playerController.JumpUpSpeed = PlayerPrefs.GetFloat("Jump");
        }
    }
    private void Start()
    {
        UpdateTextForceJump();
    }

    public void AddJumpForce(float addJumpForce)
    {
        playerController.JumpUpSpeed += addJumpForce;
        UpdateTextForceJump();
        PlayerPrefs.SetFloat("Jump", playerController.JumpUpSpeed);
    }

    public void UpdateTextForceJump()
    {
        textCurrentJump.text = playerController.JumpUpSpeed.ToString("F1");
    }
}
