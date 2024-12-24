using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] float rangeCast = 10f;  // Дальность луча
    [SerializeField] float boxWidth = 1f;    // Ширина коробки
    [SerializeField] float boxHeight = 2f;   // Высота коробки
    [SerializeField] Transform startCast;    // Точка начала луча (например, передняя часть врага)

    [SerializeField] Transform targetPlayer;  // Игрок, за которым следует враг
    [SerializeField] Transform targetNpc;  
    [SerializeField] Vector3 startPos;        // Стартовая позиция врага
    [SerializeField] LayerMask obstacleLayer;

    RaycastHit hit;  // Переменная для хранения результатов BoxCast
    bool playerInSight = false; 
    bool npcInSight = false; 

    Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        startPos = transform.position;  // Запоминаем стартовую позицию
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        RaycastEnemy();
        StartEnemy();

        float distance = Vector3.Distance(transform.position, startPos);
       


        if (distance <= 0.3f && !agent.hasPath && !playerInSight)
        {

            animator.SetBool("run", false);
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }

        
    }

    // Метод для выполнения BoxCast и проверки, видит ли враг игрока
    void RaycastEnemy()
    {
        // Позиция луча (начало)
        Vector3 origin = startCast.position;
        // Размер коробки (ширина, высота, глубина)
        Vector3 boxSize = new Vector3(boxWidth, boxHeight, rangeCast);
        // Направление луча
        Vector3 direction = transform.forward;

        // Выполнение BoxCast
        if (Physics.BoxCast(origin, boxSize / 2f, direction, out hit, Quaternion.identity, rangeCast, obstacleLayer))
        {
            if (hit.collider.CompareTag("Npc"))
            {
                npcInSight = true;
                targetNpc = hit.collider.transform; // Устанавливаем текущего NPC как цель
            }
            else
            {
                npcInSight = false;
            }

            if (hit.collider.CompareTag("Player"))
            {
                playerInSight = true;
            }
            else
            {
                playerInSight = false;
            }
        }
        else
        {
            playerInSight = false;
            npcInSight = false;
            targetNpc = null; // Сбрасываем цель, если NPC не обнаружен
        }
    }

    private void OnDrawGizmos()
    {
        // Если мы уже получили результат Raycast, рисуем визуализацию
        if (hit.collider != null)
        {
            // Отрисовываем сам луч
            Gizmos.color = Color.red;
            Gizmos.DrawLine(startCast.position, hit.point);  // От точки начала до точки попадания

            // Отрисовываем место попадания
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(hit.point, 0.2f);  // Маленькая сфера в точке попадания

            // Отрисовываем коробку в позиции попадания
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(hit.point, new Vector3(boxWidth, boxHeight, rangeCast)); // Коробка в точке попадания
        }
        else
        {
            // Если попадания нет, рисуем BoxCast область на расстоянии rangeCast
            Gizmos.color = Color.green;

            // Рисуем коробку на пути луча, не на точке попадания, а на расстоянии rangeCast
            Vector3 boxCenter = startCast.position + transform.forward * (rangeCast / 2f);  // Центр коробки на пути луча
            Vector3 boxSize = new Vector3(boxWidth, boxHeight, rangeCast);

            Gizmos.DrawWireCube(boxCenter, boxSize);  // Рисуем коробку в центре на пути луча
        }
    }

    // Метод, который контролирует поведение врага
    public void StartEnemy()
    {
        if (playerInSight)
        {
            // Если игрок в поле зрения, враг идет к нему
            agent.SetDestination(targetPlayer.position);
            animator.SetBool("run", true);
        }
        else if (npcInSight && targetNpc != null)
        {
            // Если NPC в поле зрения, враг идет к нему
            agent.SetDestination(targetNpc.position);
            animator.SetBool("run", true);
        }
        else
        {
            // Если никого нет в поле зрения, враг возвращается на стартовую позицию
            if (!agent.hasPath)
            {
                agent.SetDestination(startPos);
               
            }
        }
    }
}
