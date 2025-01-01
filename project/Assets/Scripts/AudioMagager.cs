using UnityEngine;
using UnityEngine.UI;

public class AudioMagager : MonoBehaviour
{
    public static AudioMagager Instance;

    public AudioClip[] audioClips;
    public AudioClip[] audioFx;
    AudioSource audioSource;
    public Sprite[] spriteIcon;
    public Image imageIcon;
    public bool isMute;
    private int currentTrackIndex = 0;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        imageIcon.sprite = isMute ? spriteIcon[1] : spriteIcon[0];
        audioSource.mute = isMute;

        PlayNextTrack();

    }


    public void TogleMute()
    {
        isMute = !isMute;
        audioSource.mute = isMute;
        imageIcon.sprite = isMute ? spriteIcon[1] : spriteIcon[0];
          
        
    }

    void PlayNextTrack()
    {
        if (audioClips.Length == 0) return;

        audioSource.clip = audioClips[currentTrackIndex];
        audioSource.Play();

        
        currentTrackIndex = (currentTrackIndex + 1) % audioClips.Length;

       
        Invoke(nameof(PlayNextTrack), audioSource.clip.length);
    }

    void StartSoundBg()
    {
        AudioClip clip = audioClips[Random.Range(0, audioClips.Length)];
        audioSource.clip = clip;

        audioSource.Play();

    }

    public void SoundFx(int nameClip)
    {
        audioSource.PlayOneShot(audioFx[nameClip]);
    }
}
