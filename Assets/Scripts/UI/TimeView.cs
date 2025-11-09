using System;
using TMPro;
using UnityEngine;

public class TimeView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _timerText;
    [SerializeField] private float _remainingTime;

    public static event Action OnTimeExpired;

    public bool IsTimeLoopFixed;

    private void Update()
    {
        if(IsTimeLoopFixed)
        {
            return;
        }

        if (_remainingTime > 0 && !IsTimeLoopFixed)
        {
            _remainingTime -= Time.deltaTime;
        }
        else if(_remainingTime < 0)
        {
            _remainingTime = 0;
            OnTimeExpired?.Invoke();
        }
        int minutes = Mathf.FloorToInt(_remainingTime / 60);
        int seconds = Mathf.FloorToInt(_remainingTime % 60);
        _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }


    public void AddTime(float amount)
    {
        _remainingTime += amount;
    }
}
