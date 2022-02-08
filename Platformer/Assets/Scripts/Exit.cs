using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Exit : MonoBehaviour
{
    public void CloseGame()
    {
            Application.Quit();    // закрыть приложение
            Debug.Log("Exit");
    }
}
