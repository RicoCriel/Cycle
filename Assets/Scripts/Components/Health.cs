using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int _currentHealth;
    [SerializeField] private int _maxHealth;
    [SerializeField] private bool _isGod;

    public UnityEvent<int> OnHealthDecreased;

    public UnityEvent OnDeath;

    public int CurrentHealth
    {
        get { return _currentHealth; }
        private set
        {
            _currentHealth = Mathf.Max(0, value);
        }
    }

    private void OnEnable()
    {
        _currentHealth = _maxHealth;
    }

    public void DecreaseHealth(int damageAmount)
    {
        if (_isGod)
        {
            return;
        }

        int oldHealth = CurrentHealth;
        CurrentHealth -= damageAmount;
        OnHealthDecreased?.Invoke(damageAmount);

        if (CurrentHealth <= 0 && oldHealth > 0)
            OnDeath?.Invoke();
    }
}
