using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SettingsImage : MonoBehaviour 
{
    private bool active = false;
    [SerializeField] private Slider speedSlider;

    private void Start()
    {
        speedSlider.value = PlayerPrefs.GetFloat("koef", 1);
    }

    public void Open()
    {
        gameObject.SetActive(true);
        active = true;
    }

    public void Close()
    {
        gameObject.SetActive(false);
        active = false;
    }

    /// <summary>
    /// Display true if menu (picture) are opened, else return false
    /// </summary>
    /// <returns></returns>
    public bool Status()
    {
        return active;
    }

    public void OnSubmitName(string name)
    {
        Debug.Log(name);
    }

    public void OnSpeedValue(float speed)
    {
        Messenger<float>.Broadcast(GameEvent.SPEED_CHANGED, speed);
    }
}
