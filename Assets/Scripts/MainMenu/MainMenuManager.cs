using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public AudioSource BGMSource;
    public AudioClip preLoop;
    public AudioClip loop;


    public void StartGame()
    {
        SceneManager.LoadScene("Gameplay");
    }

    private void Awake()
    {
        StartCoroutine(BGMManagement());
    }

    private IEnumerator BGMManagement()
    {
        BGMSource.clip = preLoop;
        BGMSource.Play();
        yield return new WaitForSeconds(preLoop.length);
        BGMSource.clip = loop;
        BGMSource.Play();
        BGMSource.loop = true;
    }
}
