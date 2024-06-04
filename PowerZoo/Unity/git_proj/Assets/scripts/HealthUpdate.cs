using System;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class TimeManager : MonoBehaviour
{
    private const string timeApiUrl = "http://localhost:9000/api/players/time";
    private const string updateHealthApiUrl = "http://localhost:9000/api/animals/updateHealth";

    private void Start()
    {
        StartCoroutine(UpdateTimeAndHealth());
    }

    private IEnumerator UpdateTimeAndHealth()
    {
        // Step 1: Get time data from the API using PUT request
        UnityWebRequest timeRequest = UnityWebRequest.Put(timeApiUrl, "");
        yield return timeRequest.SendWebRequest();

        if (timeRequest.result != UnityWebRequest.Result.Success)
        {
            UnityEngine.Debug.LogError("Failed to fetch time data: " + timeRequest.error);
            yield break;
        }

        string timeJson = timeRequest.downloadHandler.text;
        TimeData timeData = JsonUtility.FromJson<TimeData>(timeJson);

        // Step 2: Calculate time difference
        DateTime oldTime = DateTime.Parse(timeData.oldTime);
        DateTime newTime = DateTime.Parse(timeData.newTime);
        TimeSpan timeDifference = newTime - oldTime;
        int timeDifferenceMinutes = (int)timeDifference.TotalMinutes;

        // Step 3: Make PUT request to update health with time difference
        string updateHealthUrl = updateHealthApiUrl + "?delta=" + (-timeDifferenceMinutes);
        UnityWebRequest updateHealthRequest = UnityWebRequest.Put(updateHealthUrl, "");
        yield return updateHealthRequest.SendWebRequest();

        if (updateHealthRequest.result != UnityWebRequest.Result.Success)
        {
            UnityEngine.Debug.LogError("Failed to update health: " + updateHealthRequest.error);
            yield break;
        }

        UnityEngine.Debug.Log("Health updated successfully.");
    }

    [Serializable]
    private class TimeData
    {
        public string oldTime;
        public string newTime;
    }
}
