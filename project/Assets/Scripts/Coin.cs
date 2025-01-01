using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] float speedRotete;
    int addCountCoin;
    [SerializeField] int minRandom,maxRandom;
    void Start()
    {
        
    }

   
    void Update()
    {
        transform.Rotate(speedRotete * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {   int random = Random.Range(minRandom,maxRandom);
            addCountCoin = random;
            AudioMagager.Instance.SoundFx(0);
            GamePLay.instance.AddCoin(addCountCoin);
            Leaderbrd.Instance.UpdateScore();
            Destroy(gameObject);
        }
    }
}
