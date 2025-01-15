
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioControllEnemy : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip[] audioClips;
    public Transform targetPlayer;
    public float startDistancePlay;
    public float delayBetweenSounds = 10f; 
    private bool canPlaySound = true; 
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

   public void PlaySoundEnemy()
    {

        int randomIndex = GetRandomSoundIndex();
        AudioClip clipToPlay = audioClips[randomIndex];
        audioSource.PlayOneShot(clipToPlay);
    }

    private void Update()
    {   if (DeathPlayer.Instance.isDeathPlayer || !canPlaySound)
            return;
        float distance = Vector3.Distance(transform.position, targetPlayer.position);
        if(distance <= startDistancePlay && !audioSource.isPlaying)
        {
            PlaySoundEnemy();
            StartCoroutine(TimerFx());
        }
    }

    private int GetRandomSoundIndex()
    {
        return Random.Range(0, audioClips.Length);
    }

    IEnumerator TimerFx()
    {
        canPlaySound = false;
        yield return new WaitForSeconds(delayBetweenSounds); 
        canPlaySound = true; 
    }
}
