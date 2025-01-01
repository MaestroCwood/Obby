using System.Collections;
using UnityEngine;

public class SpawnNpc : MonoBehaviour
{
    [SerializeField] GameObject npc;
    [SerializeField] float minRandom, maxRandom;
    [SerializeField] float waitSpawnTime;
    [SerializeField] int maxCountNpc;
   

    public int countNpc;
    void Start()
    {
        StartCoroutine(nameof(SpawnNpcTimer));
    }


    IEnumerator SpawnNpcTimer()
    {
        while (countNpc <= maxCountNpc)
        {
            Vector3 randomOffset = new Vector3(Random.Range(minRandom, maxRandom), transform.position.y, Random.Range(minRandom, maxRandom));
            Instantiate(npc, transform.position + randomOffset, Quaternion.identity);
            countNpc++;
            yield return new WaitForSeconds(waitSpawnTime);
        }

       

        yield return null;
    }

}
