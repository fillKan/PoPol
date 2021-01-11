using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateSaver : Singleton<GameStateSaver>
{
    public Color CameraColor;

    private void Awake()
    {
        if (FindObjectsOfType(typeof(GameStateSaver)).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            CameraColor = Color.white;

            DontDestroyOnLoad(gameObject);
        }
    }
}
