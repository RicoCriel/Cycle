using System;
using TMPro;
using UnityEngine;

public class TimeView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _timerText;
    [SerializeField] private float _remainingTime;

    public static event Action<string> OnTimeExpired;

    private void Update()
    {
        if (_remainingTime > 0)
        {
            _remainingTime -= Time.deltaTime;
        }
        else if(_remainingTime < 0)
        {
            _remainingTime = 0;
            OnTimeExpired?.Invoke("RobotLab_A");
        }
        int minutes = Mathf.FloorToInt(_remainingTime / 60);
        int seconds = Mathf.FloorToInt(_remainingTime % 60);
        _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
