using UnityEngine;

public class AdController : MonoBehaviour
{
    [SerializeField] float speedRotate;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, speedRotate * Time.deltaTime, 0);
    }
}
