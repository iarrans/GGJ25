using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //botones
    public List<GameObject> machineButtons;
    public int currentRound;
    public float initialTimer;
    public float currentTimer;

    public static GameManager instance;

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
        throw new NotImplementedException();
    }
}
