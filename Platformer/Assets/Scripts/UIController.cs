using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour
{
    [SerializeField] private SettingsImage img;

    private void Start()
    {
        img.Close();
    }

    private void Update()
    {
      
    }

    public void OnOpenSettings()
    {
        img.Open();
    }

}
