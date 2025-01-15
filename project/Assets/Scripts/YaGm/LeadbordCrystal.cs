using System.Collections;
using UnityEngine;
using YG;

public class LeadbordCrystal : MonoBehaviour
{
    public static LeadbordCrystal Instance;
    public LeaderboardYG leaderboard;
    [SerializeField] float waitSendRecord;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(nameof(UpdateScoreLb));
        
    }

    
    public void UpdateCrystal()
    {
        int currentCrysta = PlayerPrefs.GetInt("Crystal", 0);
        int saveCrystal = GamePLay.instance.crystal;

        if (currentCrysta > saveCrystal )
        {
            leaderboard.NewScore(GamePLay.instance.crystal);
            leaderboard.UpdateLB();
        }
           
        
       
    }

    IEnumerator UpdateScoreLb()
    {
        while (true)
        {
            UpdateCrystal();
            
            yield return new WaitForSeconds(waitSendRecord);
        }

    }
}
