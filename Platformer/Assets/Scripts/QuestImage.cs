using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestImage : MonoBehaviour
{
    private bool active = false;

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
}
