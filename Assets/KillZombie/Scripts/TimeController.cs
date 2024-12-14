using System;
using TMPro;
using UnityEngine;

/// <summary>
/// Controls the in-game time system, including the position of the sun and moon and the time display.
/// </summary>
public class TimeController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText; // UI element to display the current time
    [SerializeField] private float timeMultiplier = 60f; // Speed multiplier for time progression
    [SerializeField] private float startHour = 6f; // Initial start hour of the game
    [SerializeField] private float sunSetHour = 18f; // Time at which the sun sets
    [SerializeField] private float sunRiseHour = 6f; // Time at which the sun rises
    [SerializeField] private Light sunLight; // Reference to the directional light representing the sun
    [SerializeField] private Light moonLight; // Reference to the directional light representing the moon

    private DateTime currentTime; // Current in-game time
    private TimeSpan sunRiseTime; // Time of sunrise
    private TimeSpan sunSetTime; // Time of sunset

    private void Start()
    {
        // Initialize the starting time and set sunrise and sunset times
        currentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour);
        sunRiseTime = TimeSpan.FromHours(sunRiseHour);
        sunSetTime = TimeSpan.FromHours(sunSetHour);
    }

    private void Update()
    {
        UpdateTimeOfDay();
        RotateSunAndMoon();
    }

    /// <summary>
    /// Updates the in-game time and displays it in the UI.
    /// </summary>
    private void UpdateTimeOfDay()
    {
        // Increment the current time based on the time multiplier
        currentTime = currentTime.AddSeconds(timeMultiplier * Time.deltaTime);

        // Update the time UI text
        if (timeText != null)
        {
            timeText.text = currentTime.ToString("HH:mm");
        }
    }

    /// <summary>
    /// Rotates the sun and moon based on the current time of day.
    /// </summary>
    private void RotateSunAndMoon()
    {
        float sunLightRotation;

        if (currentTime.TimeOfDay > sunRiseTime && currentTime.TimeOfDay < sunSetTime)
        {
            // Calculate the percentage of the day between sunrise and sunset
            TimeSpan sunriseToSunsetDuration = CalculateTimeDifference(sunRiseTime, sunSetTime);
            TimeSpan timeSinceSunrise = CalculateTimeDifference(sunRiseTime, currentTime.TimeOfDay);

            double percentage = timeSinceSunrise.TotalMinutes / sunriseToSunsetDuration.TotalMinutes;
            sunLightRotation = Mathf.Lerp(0, 180, (float)percentage); // Sun is moving from horizon to zenith
        }
        else
        {
            // Calculate the percentage of the night between sunset and sunrise
            TimeSpan sunsetToSunriseDuration = CalculateTimeDifference(sunSetTime, sunRiseTime);
            TimeSpan timeSinceSunset = CalculateTimeDifference(sunSetTime, currentTime.TimeOfDay);

            double percentage = timeSinceSunset.TotalMinutes / sunsetToSunriseDuration.TotalMinutes;
            sunLightRotation = Mathf.Lerp(180, 360, (float)percentage); // Sun is moving from zenith to horizon
        }

        // Rotate the sun and moon based on the calculated angle
        sunLight.transform.rotation = Quaternion.AngleAxis(sunLightRotation, Vector3.right);
        moonLight.transform.rotation = Quaternion.AngleAxis(sunLightRotation, Vector3.left);
    }

    /// <summary>
    /// Calculates the difference between two times, considering wrap-around at midnight.
    /// </summary>
    /// <param name="fromTime">The starting time.</param>
    /// <param name="toTime">The ending time.</param>
    /// <returns>The time difference.</returns>
    private TimeSpan CalculateTimeDifference(TimeSpan fromTime, TimeSpan toTime)
    {
        TimeSpan difference = toTime - fromTime;

        // Handle wrap-around at midnight
        if (difference.TotalSeconds < 0)
        {
            difference += TimeSpan.FromHours(24);
        }

        return difference;
    }
}
