using KinematicCharacterController;
using UnityEngine;
using UnityEngine.AI;

public class TeleportHome : MonoBehaviour
{
    [SerializeField] KinematicCharacterMotor motor;
    [SerializeField] Transform newPosition;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform targetPoint;
    [SerializeField] GameObject pet;
    public RewardPets rewardPets;


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
                    
            motor.SetPosition(newPosition.position);         
            AudioMagager.Instance.EnabledMusicBg();
            if (rewardPets.getted)
            {
                agent.enabled = false;
                pet.transform.position = targetPoint.position;
                agent.enabled = true;
            }
        }
    }
}
