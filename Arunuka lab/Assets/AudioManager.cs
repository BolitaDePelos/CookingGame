using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonMonobehaviour<AudioManager>
{
    [SerializeField] private AudioSource faceHappy;
    [SerializeField] private AudioSource faceSad;
    [SerializeField] private AudioSource faceNoBad;
    [SerializeField] private AudioSource grabSound;
    [SerializeField] private AudioSource buySound;
    [SerializeField] private AudioSource hoverSound;
    [SerializeField] private AudioSource nextPageSound;
    [SerializeField] private AudioSource pauseSound;
    [SerializeField] private AudioSource fridgeOpenSound;
    [SerializeField] private AudioSource fridgeContinuesSound;
    [SerializeField] private AudioSource fridgeCloseSound;
    [SerializeField] private AudioSource dishSound;
    [SerializeField] private AudioSource knifeGrab;
    [SerializeField] private AudioSource knifeCut;
    [SerializeField] private AudioSource frySound;
    [SerializeField] private AudioSource fireSound;
    [SerializeField] private AudioSource turnOnStoveSound;

    private new void Awake() => base.Awake();
    internal void PlayBadSound() => PlayAudioSource(faceSad);
    internal void PlayHappySound() => PlayAudioSource(faceHappy);
    internal void PlayNormalSound() => PlayAudioSource(faceNoBad);
    internal void PlayGrabSound() => PlayAudioSource(grabSound);
    internal void PlayBuySound() => PlayAudioSource(buySound);
    internal void PlayHoverSound() => PlayAudioSource(hoverSound);
    internal void PlayNextPageSound() => PlayAudioSource(nextPageSound);
    internal void PlayPauseSound() => PlayAudioSource(pauseSound);
    internal void PlaySoundDishTake() => PlayAudioSource(dishSound);
    internal void PlaySoundKnifeOut() => PlayAudioSource(knifeGrab);
    internal void PlaySoundKnifeCut() => PlayAudioSource(knifeCut);
    internal void PlaySoundFry() => PlayAudioSource(frySound);
    internal void StopSoundFry() => StopAudioSource(frySound);
    internal void PlaySoundFire() => PlayAudioSource(knifeCut);
    internal void PlaySoundTurnOnStove() => PlayAudioSource(knifeCut);
    internal void PlayFridgeOpenSound()
    {
        PlayAudioSource(fridgeOpenSound);
        PlayAudioSource(fridgeContinuesSound);
    }

    internal void PlayFridgeCloseSound()
    {
        PlayAudioSource(fridgeCloseSound);
        StopAudioSource(fridgeContinuesSound);
    }

    private void StopAudioSource(AudioSource source) => source.Stop();
    private void PlayAudioSource(AudioSource source) => source.Play();

}
