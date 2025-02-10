using UnityEngine;
using YG;

public class LeaderBordJump : MonoBehaviour
{
    [SerializeField] LeaderboardYG leaderboard;
    [SerializeField] YandexGame sdk;
    public static LeaderBordJump Instance;

    private void Awake()
    {
        Instance = this;
    }


    public void SetNewScoreLb()
    {
        float newScore = PlayerPrefs.GetFloat("MaxJump");
        leaderboard.NewScore((long)newScore);
        leaderboard.UpdateLB();
       
    }



}
