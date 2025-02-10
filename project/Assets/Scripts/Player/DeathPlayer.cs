using KinematicCharacterController;
using KinematicCharacterController.Examples;
using UnityEngine;
using UnityEngine.Audio;

public class DeathPlayer : MonoBehaviour
{
    public static DeathPlayer Instance;
    public Animator animator;
    [SerializeField] KinematicCharacterMotor motor;
    [SerializeField] ExampleCharacterCamera ExampleCharacterCamera;
    [SerializeField] AudioSource audioSource;
    [SerializeField] Transform restartPositionStart;
    
    float targetDistanceCamera;
    public bool isDeathPlayer = false;

    private void Awake()
    {
        Instance = this;
    }


    void Start()
    {
        targetDistanceCamera = ExampleCharacterCamera.TargetDistance;
    }

   
   

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy") && !isDeathPlayer)
        {
            audioSource.Play();
            animator.SetTrigger("deatch");
            motor.enabled = false;
            isDeathPlayer = true;
            ExampleCharacterCamera.TargetDistance = 20;  
            
           
        }
    }

    public void RestartPositionPlayer()
    {
        Vector3 position =  restartPositionStart.position;
        motor.SetPosition(position);
        isDeathPlayer = false;
        motor.enabled = true;
        ExampleCharacterCamera.TargetDistance = targetDistanceCamera;
        AudioMagager.Instance.EnabledMusicBg();
       
       
    }
}
