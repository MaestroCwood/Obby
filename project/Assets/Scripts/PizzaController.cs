using System.Collections;
using UnityEngine;


public class PizzaController : MonoBehaviour
{
    [SerializeField] float speedRotation;
    [SerializeField] float timeActive;
    [SerializeField] float addJumpBoost;
    float startTimer;
    bool isActive = true;
    MeshRenderer renderer;
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        startTimer = timeActive;
    
    }


    void Update()
    {
        transform.rotation = Quaternion.Euler(0, Time.time * speedRotation, 0);
    }

    private void OnTriggerEnter(Collider other)
    {   
        if (!isActive) return;
        if(other.CompareTag("Player"))
        {
            isActive = false;
            renderer.enabled = false;
            JumpBoost.Instance.AddJumpForce(addJumpBoost);
            AudioMagager.Instance.SoundFx(3);
            
            StartCoroutine(nameof(TimerRenderer));
        }
    }

    IEnumerator TimerRenderer()
    {
        while (timeActive > 0)
        {
            timeActive--;
            yield return new WaitForSeconds(1f);
        }
        timeActive = startTimer;
        renderer.enabled = true;
        isActive = true;
    }
}
