using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
    public void ToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void SendToNetwork(string url)
    {
        Application.OpenURL(url);
    }
}
