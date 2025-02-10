using UnityEngine;

public class Crystal : MonoBehaviour
{
    [SerializeField] int addCrystal;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GamePLay.instance.AddCrystal(addCrystal);
            //LeadbordCrystal.Instance.UpdateCrystal();
            AudioMagager.Instance.SoundFx(1);
            Destroy(gameObject);
        }
    }
}
