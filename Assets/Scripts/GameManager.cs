using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //botones
    public List<MachineBubble> machineBubbles;
    public int currentRound;
    public float initialTimer;
    public float currentTimer;

    public static GameManager instance;

    public List<MachineBubble> chosenButtons;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(GameStart());
    }

    private IEnumerator GameStart()
    {
        currentTimer = initialTimer;
        yield return new WaitForSeconds(1);
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
        }


        yield return null;
    }

}
