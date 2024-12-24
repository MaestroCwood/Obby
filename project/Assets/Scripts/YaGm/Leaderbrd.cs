using UnityEngine;
using YG;

public class Leaderbrd : MonoBehaviour
{   
    public static Leaderbrd Instance;   
    public LeaderboardYG leaderboard;
    void Awake()
    {
        Instance = this;
    }

   public void UpdateScore()
    {
        int saveCoin = PlayerPrefs.GetInt("Coin");
        int currentCoin = GamePLay.instance.coin;

        if(currentCoin >= saveCoin)
        {
            leaderboard.NewScore(GamePLay.instance.coin);
        }
        
    }
    
}
