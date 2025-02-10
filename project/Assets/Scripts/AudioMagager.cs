using System.Collections;
using KinematicCharacterController;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class AudioMagager : MonoBehaviour
{
    public static AudioMagager Instance;

    public AudioClip[] audioClips;
    public AudioClip[] audioFx;
    
    public Sprite[] spriteIcon;
    public Image imageIcon;
    public bool isMute;
    private int currentTrackIndex = 0;
    [SerializeField] float offsetZMin, offsetZMax;
    public AudioSource musicSource; 
    public AudioSource sfxSource;
    public KinematicCharacterMotor kinematicCharacter;

    private bool wasManuallyStopped = false; 

    private void Awake()
    {
        Instance = this;
        
    }

    void Start()
    {
        imageIcon.sprite = isMute ? spriteIcon[1] : spriteIcon[0];
        musicSource.mute = isMute;

        PlayNextTrack();
    }

    public void TogleMute()
    {
        isMute = !isMute;
        musicSource.mute = isMute;
        imageIcon.sprite = isMute ? spriteIcon[1] : spriteIcon[0];
    }

    void PlayNextTrack()
    {
        if (audioClips.Length == 0) return;

        musicSource.clip = audioClips[currentTrackIndex];
        musicSource.Play();

        currentTrackIndex = (currentTrackIndex + 1) % audioClips.Length;

        // Вызываем PlayNextTrack через время, равное длине текущего трека
        Invoke(nameof(PlayNextTrack), musicSource.clip.length);
    }

    void StartSoundBg()
    {
        AudioClip clip = audioClips[Random.Range(0, audioClips.Length)];
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void SoundFx(int nameClip)
    {

        sfxSource.PlayOneShot(audioFx[nameClip]);
        
    }

    private void OnEnable()
    {
        YandexGame.onVisibilityWindowGame += OnVisibilityWindowGame;
    }

    private void OnDisable()
    {
        YandexGame.onVisibilityWindowGame -= OnVisibilityWindowGame;
    }

    void OnVisibilityWindowGame(bool visible)
    {
        if (musicSource != null)
        {
            musicSource.mute = !visible;
        }

        Time.timeScale = visible ? 1 : 0;
    }

    public void DisableMusicBg()
    {
        musicSource.Stop();
    }

    public void EnabledMusicBg()
    {
        
        if(!musicSource.isPlaying)
        {
            PlayNextTrack();
        }
    }
}