using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonMonobehaviour<AudioManager>
{
    [SerializeField] private AudioSource faceHappy;
    [SerializeField] private AudioSource faceSad;
    [SerializeField] private AudioSource faceNoBad;

    private new void Awake()
    {
        base.Awake();
    }

    internal void PlayBadSound() => PlayAudioSource(faceSad);

    internal void PlayHappySound() => PlayAudioSource(faceHappy);

    internal void PlayNormalSound() => PlayAudioSource(faceNoBad);


    private void PlayAudioSource(AudioSource source) => source.Play();
}
