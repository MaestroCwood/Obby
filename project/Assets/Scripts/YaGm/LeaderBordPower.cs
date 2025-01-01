using UnityEngine;
using System.Collections;
using YG;

public class LeaderBordPower : MonoBehaviour
{
    public static LeaderBordPower Instance;
    public LeaderboardYG leaderboard;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateLeaderBordPower();
    }

    public void UpdatePower()
    {
        int saveCoin = PlayerPrefs.GetInt("Power");
        int currentCoin = GamePLay.instance.power;

        if (currentCoin >= saveCoin)
        {
            leaderboard.NewScore(GamePLay.instance.power);
        }

    }

    IEnumerator UpdateLeaderBordPower()
    {
        while (true)
        {
            UpdatePower();
            leaderboard.UpdateLB();
            yield return new WaitForSeconds(5f);
        }
    }
}
