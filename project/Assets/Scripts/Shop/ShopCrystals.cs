using TMPro;
using UnityEngine;

public class ShopCrystals : MonoBehaviour
{

    [SerializeField] int priceItem;
    [SerializeField] float addJumpForce = 5;
    [SerializeField] TextMeshProUGUI textPrice;

    private void Awake()
    {
        textPrice.text = priceItem.ToString();
    }

    public void BuyForceJumping()
    {
        if(GamePLay.instance.crystal >= priceItem)
        {
            GamePLay.instance.crystal -= priceItem;
            GamePLay.instance.UpdateCrystal();
            AudioMagager.Instance.SoundFx(4);
            JumpBoost.Instance.AddJumpForce(addJumpForce);
            JumpBoost.Instance.UpdateTextForceJump();

        }
    }


}
