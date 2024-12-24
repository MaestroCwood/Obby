using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NpcStartController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    private Transform[] points;
    private int currentPointIndex = 0;
    private bool isWaiting = false;

    [SerializeField] private string pointsTag = "Waypoint"; // Тег для поиска точек
    [SerializeField] private float waitTime = 3f; // Время ожидания на каждой точке

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        // Поиск точек по тегу
        GameObject[] pointObjects = GameObject.FindGameObjectsWithTag(pointsTag);
        points = new Transform[pointObjects.Length];

        for (int i = 0; i < pointObjects.Length; i++)
        {
            points[i] = pointObjects[i].transform;
        }

        // Если точки найдены, начинаем движение
        if (points.Length > 0)
        {
            MoveToNextPoint();
        }
        else
        {
            Debug.LogWarning("Точки с тегом " + pointsTag + " не найдены!");
        }
    }

    private void Update()
    {
        // Проверяем, достиг ли агент цели, и запускаем переход к следующей точке
        if (!isWaiting && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            StartCoroutine(WaitAtPoint());
        }
    }

    private void MoveToNextPoint()
    {
        if (points.Length == 0) return;

        // Устанавливаем следующую точку назначения
        agent.SetDestination(points[currentPointIndex].position);

        // Анимация движения (если есть)
        if (animator != null)
        {
            animator.SetBool("idle", false);
            animator.SetBool("run", true);
        }
    }

    private IEnumerator WaitAtPoint()
    {
        isWaiting = true;

        // Остановка движения
        agent.isStopped = true;

        // Анимация ожидания (если есть)
        if (animator != null)
        {
            animator.SetBool("run", false);
            animator.SetBool("idle", true);
            
        }

        // Ждём заданное время
        yield return new WaitForSeconds(waitTime);

        // Переход к следующей точке
        currentPointIndex = (currentPointIndex + 1) % points.Length;

        agent.isStopped = false;
        isWaiting = false;
        MoveToNextPoint();
    }
}
