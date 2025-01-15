using UnityEngine;


public class ChekengSelect : MonoBehaviour
{
    public PriceItem[] priceItem;
    public static ChekengSelect Instance;

   

    [SerializeField] SkinnedMeshRenderer skinnedMeshRenderer;


    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        UpdateSelectionStates();

    }


    void Update()
    {
        
    }

    private void OnEnable()
    {
        UpdateSelectionStates();
    }

    public void DeselectAllSkins()
    {
        foreach (var item in priceItem)
        {
            item.isSelected = false;
            item.SaveSkinState();
            item.UpdateUI(); 
        }
    }

    /// <summary>
    /// Обновляет интерфейс для всех карточек на основе текущего состояния.
    /// </summary>
    public void UpdateSelectionStates()
    {
        foreach (var item in priceItem)
        {
            item.UpdateUI();
        }
    }

}
