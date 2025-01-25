using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //botones
    public List<RoundSO> rounds;
    public List<MachineBubble> machineBubbles;
    public int currentRound;//Current Round number
    public float initialTimer;//Initial round Timer

    public int points;
    public float timer;

    public int pendingRoundWaves; //Waves to complete ultil the round ends

    public bool isPlayingRound;
    public TextMeshProUGUI pointsText;

    public static GameManager instance;

    public List<MachineBubble> chosenButtons;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        currentRound = 0;
        StartCoroutine(GameStart());
    }

    private IEnumerator GameStart()
    {
        points = 0;
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(RoundStart());
    }

    public IEnumerator RoundStart()
    {
        yield return null;
        RoundSO RoundData = rounds[currentRound];

        pendingRoundWaves = RoundData.waves;

        TimeBehaviour.instance.tiempoRestante = RoundData.totalTime;
        TimeBehaviour.instance.timeSlider.maxValue = RoundData.totalTime;
        TimeBehaviour.instance.timeSlider.value = RoundData.totalTime;

        StartCoroutine(NextWave(RoundData.bubblesPerWave));
        isPlayingRound = true;

        if (RoundData.dialogueAudio != null) {
            AudioManager.instance.parentsAudioSource.clip = RoundData.dialogueAudio;
            AudioManager.instance.parentsAudioSource.Play();
        }
    }

    public IEnumerator NextWave(int bubblesPerWave)
    {
        yield return null;
        if (pendingRoundWaves > 0)
        {
            pendingRoundWaves--;
            StartCoroutine(CreatePattern(bubblesPerWave));
        } else
        {
            StartCoroutine(EndRound());
        }

    }

    private IEnumerator EndRound()
    {
        Debug.Log("Round finished");
        isPlayingRound = false;
        AudioManager.instance.PlayMachineAudio();

        if (currentRound < rounds.Count - 1)
        {
            currentRound++;

            RoundSO roundData = rounds[currentRound];

            if (roundData.dialogueAudio != null)
            {
                //Audiosource con el clip de audio
                while (AudioManager.instance.parentsAudioSource.isPlaying)
                {
                    yield return new WaitForSeconds(0.2f);
                }
            }
            if (roundData.increasesTension)
            {
                Debug.Log("A");
                StartCoroutine(AudioManager.instance.IncreaseTension());
            }
            Debug.Log("Loading new round");

            yield return new WaitForSeconds(1);
            StartCoroutine(RoundStart());
        }
        else
        {
            GameEnding();
        }

        yield return null;
    }

    public IEnumerator CreatePattern(int numberOfButtons)
    {
        List<MachineBubble> freeButtons = new List<MachineBubble>(machineBubbles);
        for (int i = 0; i < numberOfButtons; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, freeButtons.Count);
            MachineBubble chosenButton = freeButtons[randomIndex];
            freeButtons.Remove(chosenButton);
            chosenButtons.Add(chosenButton);

            chosenButton.isLighted = true;
            //Para probar de manera temporal
            chosenButton.gameObject.GetComponent<Image>().color = Color.red;
        }
        yield return null;
    }

    public void CorrectButtonClicked(GameObject button)//Restore Button
    {
        //SUSTITUIR POR ANIMACION
        button.GetComponent<Image>().color = Color.white;
        MachineBubble bubbleButton = button.GetComponent<MachineBubble>();

        bubbleButton.isLighted = false;
        AudioManager.instance.PlaySFXClip(AudioManager.instance.buttonSound);

        chosenButtons.Remove(bubbleButton);
        AddPoint();

        //Change pattern?
        if(chosenButtons.Count == 0) {
            Debug.Log("WaveFinished");
            StartCoroutine(NextWave(rounds[currentRound].bubblesPerWave)); 
        }
    }

    public void WrongButtonClicked()//Skips time as penalization
    {
        if (isPlayingRound)
        {
            TimeBehaviour.instance.tiempoRestante -= rounds[currentRound].totalTime * 0.1f;
            AudioManager.instance.PlaySFXClip(AudioManager.instance.buttonSoundWrong);
        }
    }

    public void AddPoint()
    {
        points++;
        pointsText.text = "Puntos:" + points;
    }

    public void GameEnding()
    {
        Debug.Log("Game ending");
        StartCoroutine(AudioManager.instance.GameEnding());
       
    }

    public IEnumerator FadeOutWhite()
    {
        Debug.Log("yield return null");
        yield return null;
    }
}
