using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] float speedRotete;
    [SerializeField] int addCountCoin;
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
        {
            GamePLay.instance.AddCoin(addCountCoin);
            Destroy(gameObject);
        }
    }
}
