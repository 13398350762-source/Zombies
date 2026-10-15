using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BKMusic:MonoBehaviour
{
    private static BKMusic instance;
    public static BKMusic Instance => instance;
    private AudioSource audioSource;

    private void Awake()
    {
        instance = this;

        audioSource= GetComponent<AudioSource>();
        SetMusicIsOpen(GameDataManager.Instance.musicData.isMusicOpen);
        SetMusicValue(GameDataManager.Instance.musicData.musicValue);
    }

    public void SetMusicValue(float value)
    {
        audioSource.volume = value;
    }

    public void SetMusicIsOpen(bool isOpen)
    {
        audioSource.enabled = isOpen;
    }

}
