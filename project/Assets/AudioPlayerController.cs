using UnityEngine;

public class AudioPlayerController : MonoBehaviour
{
    public static AudioPlayerController Instance;
    AudioSource audioSource;
    public AudioClip[] audioClips;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayFxPlayer(int nameclip)
    {
        audioSource.PlayOneShot(audioClips[nameclip]);
    }
}
