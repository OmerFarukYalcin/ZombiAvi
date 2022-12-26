using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;
    
    [SerializeField] private float timeMultiplier;
    [SerializeField] private float startHour;
    [SerializeField] private float sunSetHour;
    [SerializeField] private float sunRiseHour;

    [SerializeField] private Light sunLight;
    [SerializeField] private Light moonLight;
    
    private DateTime currentTime;
    private TimeSpan sunRiseTime;
    private TimeSpan sunSetTime;

    private void Start()
    {
        currentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour);
        sunRiseTime = TimeSpan.FromHours(sunRiseHour);
        sunSetTime = TimeSpan.FromHours(sunSetHour);
    }

    void Update()
    {
        UpdateTimeOfDay();
        RotateSun();
    }

    void UpdateTimeOfDay()
    {
        currentTime = currentTime.AddSeconds(timeMultiplier*Time.deltaTime);

        if(timeText!= null )
        {
            timeText.text = currentTime.ToString("HH:mm");
        }
    }

    void RotateSun()
    {
        float sunLightRotation;
        if(currentTime.TimeOfDay>sunRiseTime && currentTime.TimeOfDay < sunSetTime)
        {
            TimeSpan sunriseToSunsetduration = CalculateTimeDifference(sunRiseTime,sunSetTime);
            TimeSpan timeSinceSunrise = CalculateTimeDifference(sunRiseTime,currentTime.TimeOfDay);

            double percentage = timeSinceSunrise.TotalMinutes / sunriseToSunsetduration.TotalMinutes;
            sunLightRotation = Mathf.Lerp(0, 180, (float)percentage);
        }
        else
        {
            TimeSpan sunsetToSunriseduration = CalculateTimeDifference(sunSetTime, sunRiseTime);
            TimeSpan timeSinceSunset = CalculateTimeDifference(sunSetTime, currentTime.TimeOfDay);

            double percentage = timeSinceSunset.TotalMinutes / sunsetToSunriseduration.TotalMinutes;
            sunLightRotation = Mathf.Lerp(180, 360, (float)percentage);
        }
        sunLight.transform.rotation = Quaternion.AngleAxis(sunLightRotation, Vector3.right);
        moonLight.transform.rotation = Quaternion.AngleAxis(sunLightRotation, Vector3.left);
    }

    

    private TimeSpan CalculateTimeDifference(TimeSpan fromTime,TimeSpan toTime)
    {
        TimeSpan difference = toTime- fromTime;
        if(difference.TotalSeconds < 0) 
        {
            difference += TimeSpan.FromHours(24);
        }
        return difference;
    }
}
