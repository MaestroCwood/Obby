using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;


public class Coin : MonoBehaviour
{
    [SerializeField] float speedRotete;
    int addCountCoin;
    bool isActive = true;
    MeshRenderer renderer;
    [SerializeField] int minRandom,maxRandom;
    [SerializeField] float waitBeforCreate;
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
    }


   
    void Update()
    {
        transform.Rotate(speedRotete * Time.deltaTime, 0, 0);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isActive)
        {
            if (other.CompareTag("Player"))
            {
                int random = Random.Range(minRandom, maxRandom);
                addCountCoin = random;
                AudioMagager.Instance.SoundFx(0);
                GamePLay.instance.AddCoin(addCountCoin);
               // Leaderbrd.Instance.UpdateScore();
                // Destroy(gameObject);
                isActive = false;
                renderer.enabled = false;
                StartCoroutine(nameof(TimerCoin));


            }

        }
       
    }

    IEnumerator TimerCoin()
    {
        if(!isActive)
        {
            yield return new WaitForSeconds(waitBeforCreate);
            isActive = true;
            renderer.enabled = true;
        }
    }
}
