using System.Collections;
using UnityEngine;
using YG;

public class Leaderbrd : MonoBehaviour
{   
    public static Leaderbrd Instance;   
    public LeaderboardYG leaderboard;
    [SerializeField] float waitSendRecord;
    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Invoke("StartUpdateLb", 2f);
    }

    public void UpdateScore()
    {
        int saveCoin = PlayerPrefs.GetInt("Coin");
        int currentCoin = GamePLay.instance.coin;

        if(currentCoin > saveCoin)
        {
            leaderboard.NewScore(GamePLay.instance.coin);
            leaderboard.UpdateLB();
        }
        
    }

   IEnumerator UpdateLeaderBord()
    {
        while (true)
        {
            UpdateScore();
            
            yield return new WaitForSeconds(waitSendRecord);
        }
    }
    
    void StartUpdateLb()
    {
        StartCoroutine(nameof(UpdateLeaderBord));

    }
}
