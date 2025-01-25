using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineBubble : MonoBehaviour
{
    public bool isLighted = false;

    public void ButtonClicked()
    {
        if (isLighted)
        {
            Debug.Log("CorrectlyClicked");
            gameObject.GetComponent<Animator>().Play("ButtonPressed");
            GameManager.instance.CorrectButtonClicked(gameObject);
        } else
        {
            Debug.Log("Mistake! Yikes!");
            GameManager.instance.WrongButtonClicked();
        }
    }
}
