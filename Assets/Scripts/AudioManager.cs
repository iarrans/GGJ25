using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource parentsAudioSource;
    public static AudioManager instance;

    private void Awake()
    {
        instance = this;
    }
}
