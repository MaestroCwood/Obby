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
        leaderboard.NewScore(GamePLay.instance.crystal);
    }

    IEnumerator UpdateScoreLb()
    {
        while (true)
        {
            leaderboard.UpdateLB();
            yield return new WaitForSeconds(5f);
        }

    }
}
