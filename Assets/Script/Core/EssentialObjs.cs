using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialObjs : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
