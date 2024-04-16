using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTrue : MonoBehaviour
{
    public GameObject[] Animals;

    private void Start()
    {
        int Ra = Random.Range(0, Animals.Length);

        Animals[Ra].SetActive(true);
    }
}
