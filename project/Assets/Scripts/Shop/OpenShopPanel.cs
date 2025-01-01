using UnityEngine;

public class OpenShopPanel : MonoBehaviour
{
    [SerializeField] GameObject shopPanel;

    bool isOpen = false;
    private void Awake()
    {
        shopPanel.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            shopPanel.SetActive(true);
            isOpen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            shopPanel.SetActive(false);
            isOpen = false;
        }
    }

    public void TogleShopPanel()
    {
        shopPanel.SetActive(isOpen = !isOpen); 
    }
}
