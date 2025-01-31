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

    public void UpdateLbJumping()
    {
        if (PowerJumping.Instance == null) return;
        float currentPowerJump = PowerJumping.Instance.maxJumpHeight;
        float maxPowerJump = PlayerPrefs.GetFloat("MaxJump");

        if(currentPowerJump > maxPowerJump )
        {
            leaderboard.NewScore((long)currentPowerJump);
            leaderboard.UpdateLB();
            PlayerPrefs.SetFloat("MaxJump", currentPowerJump); 
            PlayerPrefs.Save();
            Debug.Log("New Score Jump " + currentPowerJump);
        }

    }

    public void SetNewScoreLb()
    {
        leaderboard.NewScore(10);
    }

}
