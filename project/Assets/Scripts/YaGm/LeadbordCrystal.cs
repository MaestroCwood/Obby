using System.Collections;
using UnityEngine;
using YG;

public class LeadbordCrystal : MonoBehaviour
{
    public static LeadbordCrystal Instance;
    public LeaderboardYG leaderboard;
    

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
            leaderboard.NewScore(GamePLay.instance.crystal);
        
       
    }

    IEnumerator UpdateScoreLb()
    {
        while (true)
        {
            UpdateCrystal();
            leaderboard.UpdateLB();
            yield return new WaitForSeconds(5f);
        }

    }
}
