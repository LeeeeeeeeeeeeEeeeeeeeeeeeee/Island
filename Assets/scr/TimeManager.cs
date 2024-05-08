using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private void Start()
    {
        string timeStr = PlayerPrefs.GetString("stTime_1");
        DateTime startTime = Convert.ToDateTime(timeStr);

        double offlineTime = (DateTime.Now - startTime).TotalMinutes;
        Debug.Log("시작" + offlineTime);
    }

    private void OnApplicationQuit()
    {
        DateTime startTime = DateTime.Now;
        PlayerPrefs.SetString("stTime_1", startTime.ToString());
        Debug.Log("종료" + startTime);
    }
}
