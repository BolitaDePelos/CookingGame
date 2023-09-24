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

    private new void Awake() => base.Awake();
    internal void PlayBadSound() => PlayAudioSource(faceSad);
    internal void PlayHappySound() => PlayAudioSource(faceHappy);
    internal void PlayNormalSound() => PlayAudioSource(faceNoBad);
    internal void PlayGrabSound() => PlayAudioSource(grabSound);
    internal void PlayBuySound() => PlayAudioSource(buySound);
    internal void PlayHoverSound() => PlayAudioSource(hoverSound);
    internal void PlayNextPageSound() => PlayAudioSource(nextPageSound);
    internal void PlayPauseSound() => PlayAudioSource(pauseSound);
    private void PlayAudioSource(AudioSource source) => source.Play();

}
