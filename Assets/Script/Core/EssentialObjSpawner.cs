using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialObjSpawner : MonoBehaviour
{
    [SerializeField] GameObject essentialObjectPrefab;

    private void Awake()
    {
        var existingObj = FindObjectsOfType<EssentialObjs>();
        if (existingObj.Length == 0)
        {
            Instantiate(essentialObjectPrefab, new Vector3(0,0,0), Quaternion.identity);
        }
    }
}
