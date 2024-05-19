using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager TimeSystem;

    public string timeStr;
    public double offlineTime;

    private void Awake()
    {
        TimeSystem = this;
        timeStr = PlayerPrefs.GetString("stTime_1");
        DateTime startTime = Convert.ToDateTime(timeStr);

        offlineTime = (DateTime.Now - startTime).TotalMinutes;
        Debug.Log("시작" + offlineTime);
    }

    private void OnApplicationQuit()
    {
        DateTime startTime = DateTime.Now;
        PlayerPrefs.SetString("stTime_1", startTime.ToString());
        Debug.Log("종료" + startTime);
    }
}
