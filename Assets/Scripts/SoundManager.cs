using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class SoundManager : MonoBehaviour
{
    // Singleton instance
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioSource PopUpSource;
    [SerializeField]private AudioSource PlayerActionSource;
    //Pop ups
    [SerializeField]private AudioClip ArrivedToDestination;
    [SerializeField]private AudioClip PopUpBaby;
    [SerializeField]private AudioClip BabyEnd;
    [SerializeField]private AudioClip PopUpStation;
    [SerializeField]private AudioClip TrainAccident;
    //PlayerActions
    [SerializeField]private AudioClip RailPose;
    [SerializeField]private AudioClip RailUnPose;
    [SerializeField]private AudioClip EndRailPose;
    [SerializeField]private AudioClip Button0;

    //Priority
    public enum SoundPriority{ Low, Medium, High}
    private SoundPriority currentPriority = SoundPriority.Low;

    private void Awake()
    {
        // Si une instance existe déjà et que ce n'est pas celle-ci, détruisez cet objet
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        // Assigner l'instance et la marquer pour persister entre les scènes
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Only for AudioSource = PopUpSource
    public void PlaySoundWithPriority(AudioClip clip, SoundPriority priority)
    {
        if (clip == null) return;

        switch (priority)
        {
            case SoundPriority.High:
                PopUpSource.Stop();
                PopUpSource.PlayOneShot(clip);
                currentPriority = priority;
                break;

            case SoundPriority.Medium:
                if (!PopUpSource.isPlaying || currentPriority < SoundPriority.High)
                {
                    PopUpSource.PlayOneShot(clip);
                    currentPriority = priority;
                }
                break;

            case SoundPriority.Low:
                if (!PopUpSource.isPlaying)
                {
                    PopUpSource.PlayOneShot(clip);
                    currentPriority = priority;
                }
                break;
        }
    }
    //Popup Plays
    public void PlayArrivedToDestination(){
        PlaySoundWithPriority(ArrivedToDestination, SoundPriority.Medium);
    }
    public void PlayPopUpBaby(){
        PlaySoundWithPriority(PopUpBaby, SoundPriority.Medium);
    }
    public void PlayBabyEnd(){
        PlaySoundWithPriority(BabyEnd, SoundPriority.Medium);
    }
    public void PlayPopUpStation(){
        PlaySoundWithPriority(PopUpStation, SoundPriority.Medium);
    }
    public void PlayTrainAccident(){
        PlaySoundWithPriority(TrainAccident, SoundPriority.High);
    }
    
    //Play PlayerActionSounds
    public void PlayRailPose(){
        PlayerActionSource.clip = RailPose;
        PlayerActionSource.loop = true;
        PlayerActionSource.Play();
    }
    public void PlayRailUnPose(){
        PlayerActionSource.clip = RailUnPose;
        PlayerActionSource.loop = false;
        PlayerActionSource.Play();
    }
    public void PlayEndRailPose(){
        PlayerActionSource.clip = EndRailPose;
        PlayerActionSource.loop = false;
        PlayerActionSource.Play();
    }
    public void PlayButton0(){
        PlayerActionSource.clip = Button0;
        PlayerActionSource.loop = false;
        PlayerActionSource.Play();
    }

}
