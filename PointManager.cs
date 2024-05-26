using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PointManager : MonoBehaviour
{
    private float points;
    private float tempPoints;
    private Coroutine pointsCoroutine;
    private float endTime;

    public TMP_Text timerText; // Reference to the TextMeshPro Text component
    public TMP_Text tempPointText; // Reference to the TextMeshPro Text component
    public TMP_Text pointText; // Reference to the TextMeshPro Text component
    public Button claimButton;
    public float GetCurrentCoins()
    {
        return points;
    }

    public void AddCoins(float points)
    {
        this.points += points;
        PlayerPrefs.SetFloat("Coins", this.points);
    }
    private void Start()
    {
        claimButton.onClick.AddListener(ClaimCoins);
        points = PlayerPrefs.GetFloat("Coins", 0);
    }

    // Function to start the timer
    public void StartAddingPoints()
    {
        // If there's already a coroutine running, stop it
        if (pointsCoroutine != null)
        {
            StopCoroutine(pointsCoroutine);
        }

        // Start a new coroutine to add points
        pointsCoroutine = StartCoroutine(AddPointsOverTime());
    }

    private IEnumerator AddPointsOverTime()
    {
        float interval = 10f; // Interval in seconds
        float duration = 10 * 60 * 60; // 10 hours in seconds
        endTime = Time.time + duration;

        while (Time.time < endTime)
        {
            tempPoints += 0.5f;
            UpdatePointsText();
            yield return new WaitForSeconds(interval);
        }

        // Stop the coroutine after the duration
        pointsCoroutine = null;
    }

    private void Update()
    {
        if (pointsCoroutine != null)
        {
            UpdateTimerText(endTime - Time.time);
        }
        if (tempPoints >= 1)
        {
            claimButton.interactable = true;
        }
        else claimButton.interactable = false;
    }

    private void UpdateTimerText(float remainingTime)
    {
        int hours = Mathf.FloorToInt(remainingTime / 3600);
        int minutes = Mathf.FloorToInt((remainingTime % 3600) / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);

        timerText.text = $"{hours:00}:{minutes:00}:{seconds:00}";
    }
    private void UpdatePointsText()
    {
        tempPointText.text = tempPoints.ToString();
        pointText.text = points.ToString();
    }

    public void ClaimCoins()
    {
        if(tempPoints >= 1)
        {
            AddCoins(tempPoints);
            tempPoints = 0;
            UpdatePointsText();
        }
    }
}
