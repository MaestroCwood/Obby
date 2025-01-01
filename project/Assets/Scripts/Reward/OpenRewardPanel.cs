using UnityEngine;

public class OpenRewardPanel : MonoBehaviour
{
    [SerializeField] GameObject rewardPanel;


    private void Start()
    {
        rewardPanel.SetActive(false);
    }
    public void OpenReward()
    {
        rewardPanel.SetActive(true);
    }
}
