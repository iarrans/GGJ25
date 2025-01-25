using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeBehaviour : MonoBehaviour
{
    public TextMeshProUGUI timerText;

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
            timerText.text = "" + tiempoRestante;
            timeSlider.value = tiempoRestante;
        }

        if (tiempoRestante<=0 && GameManager.instance.isPlayingRound)
        {
            tiempoRestante = 0;
            timerText.text = "Derrota";
            Debug.Log("PERDISTEEE");
        }
    }

}
