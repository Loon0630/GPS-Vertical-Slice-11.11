﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip arrowOne, rocketOne, winOne, buildOne, startSound;

    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        arrowOne = Resources.Load<AudioClip>("Arrow1");
        rocketOne = Resources.Load<AudioClip>("Rocket1");
        winOne = Resources.Load<AudioClip>("Win1");
        buildOne = Resources.Load<AudioClip>("Build1");
        startSound = Resources.Load<AudioClip>("Startsound");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound (string clip)
    {
        switch (clip)
        {
            case "Arrow1":
                audioSrc.PlayOneShot(arrowOne);
                break;

            case "Rocket1":
                audioSrc.PlayOneShot(rocketOne);
                break;

            case "Win1":
                audioSrc.PlayOneShot(winOne);
                break;

            case "Build1":
                audioSrc.PlayOneShot(buildOne);
                break;

            case "Startsound":
                audioSrc.PlayOneShot(startSound);
                break;
        }
    }
}
