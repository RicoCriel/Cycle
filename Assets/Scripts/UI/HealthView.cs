using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class HealthView : MonoBehaviour
{
    private TextMeshProUGUI _healthText;

    private void Awake()
    {
        _healthText = GetComponent<TextMeshProUGUI>();
    }

    public void SetText(int health)
    {
        _healthText.text = health.ToString();
    }

}
