using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public void Start()
    {
        gameObject.SetActive(false);
    }
    public void OnOpenGO()
    {
        gameObject.transform.position = new Vector3(Screen.width/2, Screen.height/2, 0);
        gameObject.SetActive(true);
    }
}
