using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeBehaviour : MonoBehaviour
{
    public float tiempoRestante;

    public Slider timeSlider;

    public static TimeBehaviour instance;

    private void Awake()
    {
        instance = this;
    }

    public void Update()
    {
        if (GameManager.instance.isPlayingRound)
        {       
            tiempoRestante -= Time.deltaTime;
            timeSlider.value = tiempoRestante;
        }

        if (tiempoRestante<=0 && GameManager.instance.isPlayingRound)
        {
            tiempoRestante = 0;
            Debug.Log("PERDISTEEE");
        }
    }

}
