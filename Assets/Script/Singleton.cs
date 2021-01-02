using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public  static T  Instance
    {
        get
        {
            if (_Instance == null)
            {
                var instanceType = typeof(T);

                _Instance = GameObject.FindObjectOfType(instanceType) as T;

                if (_Instance == null)
                {
                    _Instance = new GameObject(instanceType.ToString(), instanceType).GetComponent<T>();
                }
            }
            return _Instance;
        }
    }
    private static T _Instance;
}
