using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class RestartGame : MonoBehaviour
{
    private Button resetButton;

    private void Start()
    {
        resetButton = GetComponent<Button>();
    }
    public void buttonCallBack()
    {
        //Get current scene name
        string scene = SceneManager.GetActiveScene().name;
        //Load it
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}
