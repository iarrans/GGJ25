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
        int audio = Random.Range(0, machineAudios.Count);
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
            BGMIntensities[currentTension].volume = 0;
            currentTension++;
            BGMIntensities[currentTension].volume = BGMVolume;
        }        
        yield return null;
    }
}
