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
    bool isSpawn = false;
    [SerializeField] SkinnedMeshRenderer skinnedMeshRenderer;
    [SerializeField] Material[] materials;
    
    [SerializeField] GameObject particalFx;
    
  

    [SerializeField] private string pointsTag = "Waypoint"; // Тег для поиска точек
   
    [SerializeField] private float waitTime = 3f; // Время ожидания на каждой точке

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
       


    }

    private void Start()
    {
        
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


        SetRandomSkin();

        SetRandomPriorety();

    }

    public void SetRandomPriorety()
    {
        int random = Random.Range(1, 100);
        agent.avoidancePriority = random;
    }
    public void SetRandomSkin()
    {
        Material randomaterial = materials[Random.Range(0, materials.Length)];
        skinnedMeshRenderer.material = randomaterial;
        Debug.Log(randomaterial);
    }
      
    private void Update()
    {
        if (isWaiting) return; // Если NPC уже ждет, ничего не делаем

        // Проверяем, достиг ли NPC текущей точки
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (currentPointIndex == points.Length - 1 && !isSpawn)
            {
                
                OnLastPointReached();
            }
            else
            {
                // Если достиг промежуточной точки
                StartCoroutine(WaitAtPoint());
            }
        }
    }


    private void MoveToNextPoint()
    {
        if (points.Length == 0) return;

        Vector3 random = new Vector3(Random.Range(-5,5) ,0 , Random.Range(-5,5));
        agent.SetDestination(points[currentPointIndex].position + random);

        // Анимация движения (если есть)
        if (animator != null)
        {
            animator.SetBool("idle", false);
            animator.SetBool("run", true);
        }
    }

    private void OnLastPointReached()
    {
        isSpawn = true;

      
        agent.isStopped = true; 
        if (animator != null)
        {
            animator.SetBool("run", false);
        }

        Instantiate(particalFx,transform.position, Quaternion.identity);
        GetComponent<NpcController>().enabled = true;
        agent.enabled = false;
        GameObject labPosition = GameObject.FindGameObjectWithTag("labPositionTransform");
        transform.position = labPosition.transform.position;
        Invoke("EnabledNpcControler", 0.1f);
    }

    void EnabledNpcControler()
    {
        agent.enabled = true;
    
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
