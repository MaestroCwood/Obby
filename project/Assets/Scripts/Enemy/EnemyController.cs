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
    [SerializeField] Vector3 startPos;        // Стартовая позиция врага

    RaycastHit hit;  // Переменная для хранения результатов BoxCast
    bool playerInSight = false;  // Флаг, указывающий, что игрок виден

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
       


        if (distance <= 0.1f && !agent.hasPath && !playerInSight)
        {

            animator.SetBool("run", false);
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
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

        // Выполнение BoxCast (параметры: начало, размер коробки, направление, вращение, дальность)
        if (Physics.BoxCast(origin, boxSize / 2f, direction, out hit, Quaternion.identity, rangeCast))
        {
            // Если объект с тэгом "Player" был обнаружен
            if (hit.collider.CompareTag("Player"))
            {
                playerInSight = true;
                Debug.Log("Player detected: " + hit.collider.name);
            }
            else
            {
                playerInSight = false;  // Игрок не в зоне видимости
            }
        }
        else
        {
            playerInSight = false;  // Игрок не в зоне видимости
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
            Debug.Log("Moving towards player");
        }
        else
        {
            // Если игрок не в поле зрения, враг возвращается на стартовую позицию
            if (!agent.hasPath)
            {
                agent.SetDestination(startPos);
              
               
            }
        }
    }
}
