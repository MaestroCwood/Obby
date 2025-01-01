using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PriceItem : MonoBehaviour
{
    public int idCardSkin;
    [SerializeField] public Material skins;
  
    [SerializeField]SkinnedMeshRenderer skinnedMeshRenderer;
    public int priceCard;
    [SerializeField] TextMeshProUGUI textPrice;
    [SerializeField] GameObject buyButton;
    [SerializeField] GameObject gettedText;
    
    public GameObject gettedImage;
    

    bool isPurchased = false;
    public bool isSelected = false;
    public bool isDefaultSkin = false;

    private void Awake()
    {
       
       
    }
    void Start()
    {   
        textPrice.text = priceCard.ToString();
        LoadSkinState();
        if (isDefaultSkin)
        {
            isPurchased = true;
            isSelected = true;
            skinnedMeshRenderer.material = skins; // ѕримен€ем материал по умолчанию
        }
        UpdateUI();


    }

    
    public void BuySkin()
    {
        int currentCoin = GamePLay.instance.coin;
        if(currentCoin >= priceCard)
        {
            isPurchased = true;
            buyButton.SetActive(false);
            gettedText.SetActive(true);
            UpdateUI();
            SelectSkin();
        }
    }

    public void SelectSkin()
    {
        if (isPurchased)
        {
            // —нимаем выбор со всех остальных карточек
            ChekengSelect.Instance.DeselectAllSkins();

            // ”станавливаем выбранный материал
            skinnedMeshRenderer.material = skins;
            isSelected = true;

            SaveSkinState();
            ChekengSelect.Instance.UpdateSelectionStates();
        }
       
    }

    private void OnEnable()
    {
        string mat = skinnedMeshRenderer.material.ToString();
        string skin = skins.ToString();
        if (mat != skin)
        {
            isSelected = false;
        }
        else isSelected = true;
       
       
    }

    private void LoadSkinState()
    {
        string keyPurchased = $"Skin_{idCardSkin}_Purchased";
        string keySelected = $"Skin_{idCardSkin}_Selected";

        isPurchased = PlayerPrefs.GetInt(keyPurchased, 0) == 1; // 1 = куплен, 0 = не куплен
        isSelected = PlayerPrefs.GetInt(keySelected, 0) == 1; // 1 = выбран, 0 = не выбран

        if (isSelected)
        {
            skinnedMeshRenderer.material = skins; // ѕримен€ем материал, если скин выбран
        }
    }

    public void SaveSkinState()
    {
        string keyPurchased = $"Skin_{idCardSkin}_Purchased";
        string keySelected = $"Skin_{idCardSkin}_Selected";

        PlayerPrefs.SetInt(keyPurchased, isPurchased ? 1 : 0);
        PlayerPrefs.SetInt(keySelected, isSelected ? 1 : 0);
        PlayerPrefs.Save(); // —охран€ем изменени€
    }

    public void UpdateUI()
    {
        buyButton.SetActive(!isPurchased); 
        gettedText.SetActive(isPurchased); 
        gettedImage.SetActive(isSelected); 
    }
}
