using System.Collections;
using UnityEngine;

public class SpawnNpc : MonoBehaviour
{
    [SerializeField] GameObject npc;
    [SerializeField] float minRandom, maxRandom; 
    public int countNpc;
    void Start()
    {
       // StartCoroutine(nameof(SpawnNpcTimer));
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.V)) 
        {
            SpawnNpcLabirint();
        }
    }

    //IEnumerator SpawnNpcTimer()
    //{
    //    while (countNpc <= 5)
    //    {
    //        Vector3 randomOffset = new Vector3(Random.Range(minRandom, maxRandom), transform.position.y, Random.Range(minRandom, maxRandom));
    //        Instantiate(npc, transform.position + randomOffset, Quaternion.identity);
    //        countNpc++;
    //        yield return new WaitForSeconds(5f);
    //    }
        
    //    countNpc = 0;
    //    Debug.Log("spawnenemy " + countNpc);

    //    yield return null;
    //}

    public void SpawnNpcLabirint()
    {
        Instantiate(npc, transform.position, Quaternion.identity);
        countNpc++;
    }

}
