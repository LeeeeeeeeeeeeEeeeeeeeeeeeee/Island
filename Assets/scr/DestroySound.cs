using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySound : MonoBehaviour
{
    void Start()
    {
        if (gameObject.name != "SFX")
        {
            Destroy(gameObject, Time.deltaTime * 1000f);
        }
    }
}
