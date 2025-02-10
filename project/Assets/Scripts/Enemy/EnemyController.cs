using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float rangeCast = 10f;  // Дальность BoxCast
    [SerializeField] float boxWidth = 1f;    // Ширина BoxCast
    [SerializeField] float boxHeight = 2f;   // Высота BoxCast
    [SerializeField] float maxFollowDistance = 15f; // Максимальная дистанция для преследования
    [SerializeField] Transform startCast;
    [SerializeField] float timerRestart, randomMin, randomMax;
    float startTimer;

    private NavMeshAgent agent;
    private Vector3 startPos;
    private Transform currentTarget;
    // private Animator animator;
    private Quaternion startRotation;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        startPos = transform.position;
        // animator = GetComponent<Animator>();
        startRotation = transform.rotation;
        startTimer = timerRestart;

        StartCoroutine(nameof(TimerRandomSpeed));
    }

    void Update()
    {
        RaycastForTarget();

        if (currentTarget != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, currentTarget.position);

            if (distanceToTarget <= agent.stoppingDistance)
            {
                // Цель достигнута, возвращаемся на стартовую позицию
                currentTarget = null;
            }
            else if (distanceToTarget <= maxFollowDistance)
            {
                FollowTarget();
            }
            else
            {
                ReturnToStartPosition();
            }
        }
        else
        {
            ReturnToStartPosition();
        }

        HandleIdleState();
    }

    // Выполняет BoxCast для поиска цели
    void RaycastForTarget()
    {
        Vector3 origin = startCast.position;
        Vector3 boxSize = new Vector3(boxWidth, boxHeight, rangeCast);
        Vector3 direction = transform.forward;

        if (Physics.BoxCast(origin, boxSize / 2, direction, out RaycastHit hit, Quaternion.identity, rangeCast))
        {

            if (hit.collider.CompareTag("Player") || hit.collider.CompareTag("Npc") || hit.collider.name == "PlayerMesh")
            {
                currentTarget = hit.collider.transform;
            }
        }
    }

    // Преследует текущую цель
    void FollowTarget()
    {
        if (currentTarget != null)
        {
            agent.SetDestination(currentTarget.position);
            // animator.SetBool("run", true);
        }
    }

    // Возвращается к стартовой позиции
    void ReturnToStartPosition()
    {
        if (Vector3.Distance(transform.position, startPos) > agent.stoppingDistance)
        {
            agent.SetDestination(startPos);
            //animator.SetBool("run", true);
        }
    }

    // Проверяет, нужно ли остановить врага
    void HandleIdleState()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            //animator.SetBool("run", false);
            agent.ResetPath();
            transform.rotation = startRotation;
        }
    }

    
    private void OnDrawGizmos()
    {
        if (startCast == null) return;

        Gizmos.color = currentTarget != null ? Color.red : Color.green;
        Vector3 boxCenter = startCast.position + transform.forward * (rangeCast / 2f);
        Vector3 boxSize = new Vector3(boxWidth, boxHeight, rangeCast);
        Gizmos.matrix = Matrix4x4.TRS(boxCenter, transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, boxSize);
    }

    IEnumerator TimerRandomSpeed()
    {
        while (true)
        {
            while (timerRestart > 0)
            {
                timerRestart--;
              
                yield return new WaitForSeconds(1f);                
            }

            float randomSpeed = Mathf.Round(Random.Range(randomMin, randomMax) * 10) / 10f;
            agent.speed = randomSpeed;
            timerRestart = startTimer;
            yield return null;
        }
    }
}
