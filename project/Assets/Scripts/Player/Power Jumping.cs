using TMPro;
using KinematicCharacterController;
using UnityEngine;
using KinematicCharacterController.Examples;


public class PowerJumping : MonoBehaviour
{
    public static PowerJumping Instance;
    [SerializeField] private TextMeshProUGUI textPower;
    [SerializeField] private KinematicCharacterMotor kinematicCharacter;
    [SerializeField] private ExampleCharacterController controller;

    [SerializeField] LeaderBordJump LeaderBordJump;
    private float groundLevel = 0f;
    public float maxJumpHeight = 0f;
    [SerializeField] float offsetZMin, offsetzMax;
    [SerializeField]private float heightCorrection = 0.5f;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        float playerY = kinematicCharacter.transform.position.y;
        float playerHeightOffset = kinematicCharacter.Capsule.height / 2;

        
        float playerFeetY = playerY - playerHeightOffset + heightCorrection;

        if (kinematicCharacter.transform.position.z >= offsetZMin && kinematicCharacter.transform.position.z <= offsetzMax)
        {
            if (kinematicCharacter.GroundingStatus.FoundAnyGround)
            {
                groundLevel = kinematicCharacter.GroundingStatus.GroundCollider.transform.position.y;
                
                maxJumpHeight = 0f;
            }

            float jumpHeight = playerFeetY - groundLevel;

            if (jumpHeight > maxJumpHeight)
            {
                maxJumpHeight = jumpHeight;
                LeaderBordJump.Instance.SetNewScoreLb();
            }
                
            

            textPower.text = maxJumpHeight.ToString("F1");
            if(PlayerPrefs.GetFloat("MaxJump",0) < maxJumpHeight)
            {
                LeaderBordJump.UpdateLbJumping();
            }
        }
        else maxJumpHeight = 0;

       

        

    }

    
}
