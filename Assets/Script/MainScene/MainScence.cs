using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScence : MonoBehaviour
{
    private Animator ScrollAnimator;

    private void Update()
    {
        if (!Application.isEditor)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }
}
