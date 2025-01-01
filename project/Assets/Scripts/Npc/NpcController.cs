using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class NpcController : MonoBehaviour
{
    Vector3 randomPoint;
    Vector3 startPoint;
    NavMeshAgent agent;
    Animator animator;
    NpcStartController npcStartController;

    // Диапазон, в котором NPC будет перемещаться
    public float moveRangeX = 10f;
    public float moveRangeZ = 10f;
    public float navMeshCheckDistance = 5f; // Максимальное расстояние для поиска подходящей точки на NavMesh
    public float stopTime = 3f; // Время остановки на точке (в секундах)

    private void Awake()
    {
        npcStartController = GetComponent<NpcStartController>();
    }

    void Start()
    {   
        npcStartController.enabled = false;
        startPoint = transform.position;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if (agent != null && agent.isOnNavMesh)
        {
            GenerateRandomPoint(); // Генерируем первую случайную точку
        }
       
    }

    void Update()
    {
        if (agent != null && agent.enabled && agent.isOnNavMesh)
        {
            // Если NPC достиг текущей цели, генерируем новую точку
            if (!agent.pathPending && agent.remainingDistance <= 0.1f)
            {
                // Запускаем корутину для остановки
                StartCoroutine(StopAndMoveToNextPoint());
            }

            // Устанавливаем цель для NPC
            if (agent.remainingDistance <= 0.1f && !agent.pathPending)
            {
                agent.SetDestination(randomPoint);
            }

            // Обновление анимации
            UpdateAnimation();
        }
    }

    // Метод для генерации случайной точки, доступной на NavMesh
    void GenerateRandomPoint()
    {
        Vector3 randomPosition;

        // Попробуем генерировать точку на NavMesh
        do
        {
            // Генерация случайной точки в пределах диапазона от стартовой позиции
            float randomX = Random.Range(startPoint.x - moveRangeX, startPoint.x + moveRangeX);
            float randomZ = Random.Range(startPoint.z - moveRangeZ, startPoint.z + moveRangeZ);

            randomPosition = new Vector3(randomX, startPoint.y, randomZ); // Сохраняем новую точку

        } while (!IsPointOnNavMesh(randomPosition)); // Проверяем, находится ли точка на NavMesh

        randomPoint = randomPosition; // Сохраняем новую точку

        if (agent != null && agent.enabled && agent.isOnNavMesh)
        {
            agent.SetDestination(randomPoint); // Устанавливаем точку назначения для NPC
        }
    }

    // Метод для проверки, находится ли точка на NavMesh
    bool IsPointOnNavMesh(Vector3 point)
    {
        NavMeshHit hit;
        // Проверяем, можно ли найти точку на NavMesh с помощью SamplePosition
        return NavMesh.SamplePosition(point, out hit, navMeshCheckDistance, NavMesh.AllAreas);
    }

    // Корутина для остановки NPC на 3 секунды, затем движения к следующей точке
    IEnumerator StopAndMoveToNextPoint()
    {
        if (agent != null && agent.enabled && agent.isOnNavMesh)
        {
            // Изменяем анимацию на "idle" (ожидание)
            animator.SetBool("run", false);
            animator.SetBool("idle", true); // Включаем анимацию ожидания

            // Ожидаем 3 секунды
            yield return new WaitForSeconds(stopTime);

            // Изменяем анимацию на "run"
            animator.SetBool("run", true);
            animator.SetBool("idle", false); // Отключаем анимацию ожидания

            // Генерируем новую точку назначения
            GenerateRandomPoint();
        }
    }

    // Метод для обновления анимации
    public void UpdateAnimation()
    {
        if (agent != null && agent.enabled && agent.isOnNavMesh)
        {
            // Если агент не двигается, анимация "idle", иначе "run"
            if (agent.pathPending || agent.remainingDistance > 0.1f)
            {
                animator.SetBool("run", true);
                animator.SetBool("idle", false);
            }
            else
            {
                animator.SetBool("run", false);
                animator.SetBool("idle", true); // Если NPC не двигается, анимация "idle"
            }
        }
    }
}
