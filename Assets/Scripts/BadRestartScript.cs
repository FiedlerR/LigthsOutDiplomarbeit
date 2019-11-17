using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadRestartScript : MonoBehaviour
{
    public void script()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

}
