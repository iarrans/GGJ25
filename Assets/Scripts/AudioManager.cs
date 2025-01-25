using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource parentsAudioSource;
    public static AudioManager instance;
    public float SFXVolume = 1;
    public float BGMVolume = 1;

    public AudioClip buttonSound;
    public AudioClip buttonSoundWrong;
    public List<AudioClip> machineAudios;

    public List<AudioSource> BGMIntensities;
    int currentTension;
    public float FadeTime;

    public AudioClip endGameDialogue;
    public AudioClip kidWon;

    private void Awake()
    {
        instance = this;
    }

    public void PlaySFXClip(AudioClip audioclip)
    {
        //Spawn del GameObject con efecto de sonido. Se crea como hijo del manager del sonido
        AudioSource audioSourceSFX = transform.AddComponent<AudioSource>();

        //asignar clip de audio
        audioSourceSFX.clip = audioclip;

        //Cambiar a volumen seleccionado por usuario
        audioSourceSFX.volume = SFXVolume;

        //ejecutar sonido
        audioSourceSFX.Play();

        //eliminar objeto al acabar el sonido
        float clipLenght = audioSourceSFX.clip.length;

        Destroy(audioSourceSFX, clipLenght);

    }

    public void PlayMachineAudio()
    {
        int audio = UnityEngine.Random.Range(0, machineAudios.Count);
        PlaySFXClip(machineAudios[audio]);
    }


    private void Start()
    {
        currentTension = 0;
        StartCoroutine(StartTension());
    }

    private IEnumerator StartTension()
    {
        yield return null;
        foreach (AudioSource BGMsource in BGMIntensities)
        {
            BGMsource.volume = 0;
        }
        BGMIntensities[0].volume = BGMVolume;
    }

    public IEnumerator IncreaseTension()
    {
        if (currentTension + 1 < BGMIntensities.Count)
        {
            while (BGMIntensities[currentTension].volume > 0)
            {
                BGMIntensities[currentTension].volume -= BGMVolume * Time.deltaTime / FadeTime;
                BGMIntensities[currentTension + 1].volume += BGMVolume * Time.deltaTime / FadeTime;

                yield return null;
            }
            currentTension++;
        }        
        yield return null;
    }

    public IEnumerator GameEnding()
    {
        float waitTime = endGameDialogue.length;
        parentsAudioSource.clip = endGameDialogue;
        parentsAudioSource.Play();
        yield return new WaitForSeconds(waitTime);

        //insertar animacion de ganar del niño
        
        waitTime = kidWon.length;
        parentsAudioSource.clip = kidWon;
        parentsAudioSource.Play();
        yield return new WaitForSeconds(waitTime);

        StartCoroutine(GameManager.instance.FadeOutWhite());

    }
}
