using System.Collections;
using KinematicCharacterController.Examples;
using TMPro;
using UnityEngine;
public class BoostSpeed : MonoBehaviour
{
    bool isActive = true;
    MeshRenderer renderer;
    [SerializeField] float waitBeforCreate;
    [SerializeField] float speedBoost;
    float speedNormal;
    [SerializeField] float timerBoost;
 


    [SerializeField] public ExampleCharacterController playerController;


    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        speedNormal = playerController.MaxStableMoveSpeed;
    }




    private void OnTriggerEnter(Collider other)
    {
        
        if (isActive)
        {
            if (other.CompareTag("Player"))
            {
                AudioMagager.Instance.SoundFx(0);
                playerController.MaxStableMoveSpeed += speedBoost;
                isActive = false;
                renderer.enabled = false;
                StartCoroutine(nameof(TimerCreate));
                StartCoroutine(nameof(TimerBoost));

            }

        }

    }


    IEnumerator TimerBoost()
    {
       yield return new WaitForSeconds(timerBoost);
       playerController.MaxStableMoveSpeed -= speedBoost;
        
      


    }

    IEnumerator TimerCreate()
    {

        yield return new WaitForSeconds(waitBeforCreate);
        isActive = true;
        renderer.enabled = true;
    }

   
}
