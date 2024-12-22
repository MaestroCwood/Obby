using KinematicCharacterController;
using UnityEngine;

public class TeleportHome : MonoBehaviour
{
    [SerializeField] KinematicCharacterMotor motor;
    [SerializeField] Transform newPosition;


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            motor.SetPosition(newPosition.position);
        }
    }

}
