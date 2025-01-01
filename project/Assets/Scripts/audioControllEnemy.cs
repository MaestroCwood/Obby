using UnityEngine;

public class audioControllEnemy : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip[] audioClips;
    public Transform targetPlayer;
    public float startDistancePlay;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

   public void PlaySoundEnemy(int name)
    {
        audioSource.PlayOneShot(audioClips[name]);
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, targetPlayer.position);
        if(distance <= startDistancePlay && !audioSource.isPlaying)
        {
            PlaySoundEnemy(0);
        }
    }
}
