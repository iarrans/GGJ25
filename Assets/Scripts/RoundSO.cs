using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Round", menuName = "SOs/Round")]
public class RoundSO : ScriptableObject
{
    public int bubblesPerWave;
    public int waves;
    public float totalTime;
    public AudioClip dialogueAudio;   
}
