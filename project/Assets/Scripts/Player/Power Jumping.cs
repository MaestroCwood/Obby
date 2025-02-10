using TMPro;
using KinematicCharacterController;
using UnityEngine;
using KinematicCharacterController.Examples;
using System.Collections;


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
    [SerializeField] float delaySetLederBord;

    bool isCoroutineRunning = false;

    private void Awake()
    {
        Instance = this;
        Debug.Log("MaxJumpHieght " + maxJumpHeight);
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
               // SaveScore();
                maxJumpHeight = 0f;
                
            }

            float jumpHeight = playerFeetY - groundLevel;

            if (jumpHeight > maxJumpHeight)
            {
                maxJumpHeight = jumpHeight;
               
                SaveScore();
            }
                       
            textPower.text = maxJumpHeight.ToString("F1");
           
        }
        else maxJumpHeight = 0;     

    }

    public void SaveScore()
    {
        float saveScore = PlayerPrefs.GetFloat("MaxJump", 0);
        float currentScore = maxJumpHeight;

        if(currentScore > saveScore)
        {
            PlayerPrefs.SetFloat("MaxJump", currentScore);
            PlayerPrefs.Save();

            if (!isCoroutineRunning)
            {
                StartCoroutine(DelaySetLederBord());
            }
        }
    }

    IEnumerator DelaySetLederBord()
    {
        isCoroutineRunning = true;
        yield return new WaitForSeconds(delaySetLederBord);
        LeaderBordJump.Instance.SetNewScoreLb();
        isCoroutineRunning = false;
    }
}
