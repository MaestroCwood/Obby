using UnityEngine;

public class ShopSkin : MonoBehaviour
{
    [SerializeField] Material[] skins;
    int currentSkin;
    
    SkinnedMeshRenderer skinnedMeshRenderer;
    

    private void Awake()
    {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
    }

    private void Start()
    {
        if (skins.Length > 0)
        {
            skinnedMeshRenderer.material = skins[currentSkin];
        }
    }

    void SmenaSkins()
    {
        currentSkin++;

        if (currentSkin >= skins.Length)
        {
            currentSkin = 0;
        }

        skinnedMeshRenderer.material = skins[currentSkin];
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            SmenaSkins();
        }
    }

}
