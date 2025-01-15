using UnityEngine;
using System.Collections;
using YG;

public class LeaderBordPower : MonoBehaviour
{
    public static LeaderBordPower Instance;
    public LeaderboardYG leaderboard;
    [SerializeField] float waitSendRecord;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        
        Invoke("StartUpdateLb", 1f);
    }

    public void UpdatePower()
    {
        int saveCoin = PlayerPrefs.GetInt("Power");
        int currentCoin = GamePLay.instance.power;

        if (currentCoin > saveCoin)
        {
            leaderboard.NewScore(GamePLay.instance.power);
            leaderboard.UpdateLB();
        }

    }

    IEnumerator UpdateLeaderBordPower()
    {
        
        while (true)
        {
           
            UpdatePower();
            
            yield return new WaitForSeconds(waitSendRecord);
        }

        
    }

    void StartUpdateLb()
    {
        StartCoroutine(nameof(UpdateLeaderBordPower));
    }
}
