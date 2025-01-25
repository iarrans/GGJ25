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
}
