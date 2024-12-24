using UnityEngine;
using UnityEngine.AI;

public class NpcDeath : MonoBehaviour
{
    Animator animator;
    NpcController controller;
    NavMeshAgent agent;
    [SerializeField] SpawnNpc spawnNpc;
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<NpcController>();
        agent = GetComponent<NavMeshAgent>();
        //spawnNpc = FindAnyObjectByType<SpawnNpc>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            EnsureSpawnNpc();
            controller.enabled = false;
            if (agent != null && agent.isActiveAndEnabled)
            {
                agent.ResetPath();
            }
            agent.enabled = false;
            animator.SetTrigger("death");
            spawnNpc.countNpc--;
            Destroy(gameObject, 5f);
            
        }
    }


    void EnsureSpawnNpc()
    {
        if (spawnNpc == null)
        {
            spawnNpc = FindAnyObjectByType<SpawnNpc>();

            if (spawnNpc == null)
            {
                Debug.LogError("SpawnNpc object not found!");
            }
        }
    }
}
