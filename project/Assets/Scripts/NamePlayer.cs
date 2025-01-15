using TMPro;
using UnityEngine;
using YG;

public class NamePlayer : MonoBehaviour
{
    public float offset;
    public Transform target;
    public Camera mainCamera;
    public TextMeshProUGUI textName;
    private void LateUpdate()
    {
        transform.position = target.position + Vector3.up * offset; ;
       
        transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.transform.position);
    }

    private void Start()
    {
        string currentName = YandexGame.playerName;
        string currentLang = YandexGame.lang;
        if(currentName == "anonymous" || currentName == "unauthorized")
        {   if(currentLang == "ru")
            {
                textName.text = "Гость";
            } else
                textName.text = "Guest";

        } else 
            textName.text = YandexGame.playerName;
       Debug.Log(YandexGame.playerName);
       Debug.Log(YandexGame.lang);
    }

   
}
