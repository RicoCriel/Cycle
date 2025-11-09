using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class HealthView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _healthText;
    public Action OnHealthChanged;

    private void Awake()
    {
        
    }

}
