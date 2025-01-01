using UnityEngine;

public class ClosedRewardPanel : MonoBehaviour
{
    [SerializeField] GameObject rewardPanel;
   

    public void ClosedReward()
    {
        rewardPanel.SetActive(false);
    }
}
