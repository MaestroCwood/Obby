using UnityEngine;
using UnityEngine.AI;

public class FolovingPlayer : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] Transform targetPlayer;
   
    [SerializeField]float stopDistance;
  

   

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        if (agent != null)
        {

            float distance = Vector3.Distance(transform.position, targetPlayer.position);

            if (distance > stopDistance)
            {
                RunPets();
            }
        }
       
    }

    void RunPets()
    {
        agent.SetDestination(targetPlayer.position);
    }
}
