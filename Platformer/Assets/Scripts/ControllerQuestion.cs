using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerQuestion : MonoBehaviour
{
    [SerializeField] private QuestImage img;
    private void Start()
    {
        img.Close();
    }

    public void OnOpenQuestion()
    {
        img.Open();
    }

    public void OnCloseQuestion()
    {
        img.Close();
    }
}
