using UnityEngine;
using UnityEngine.Events;

public class TimeLoopManager : MonoBehaviour
{
    [SerializeField] private GameObject _brokenTimeloop;
    [SerializeField] private GameObject _fixedTimeloop;

    public static TimeLoopManager Instance;
    public int UnlockedTerminals;
    public int MaxTerminals = 3;

    public UnityEvent OnTimeLoopBroken;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;

        OnTimeLoopBroken.AddListener(ShowFixedTimeLoop);
    }

    public void UnlockTerminal()
    {
        UnlockedTerminals++;
    }

    public void TriggerGameEnding()
    {
        if (UnlockedTerminals == MaxTerminals)
        {
            OnTimeLoopBroken?.Invoke();

            var timeView = FindFirstObjectByType<TimeView>();
            if (timeView != null)
                timeView.IsTimeLoopFixed = true;
        }
    }

    private void ShowFixedTimeLoop()
    {
        _brokenTimeloop.SetActive(false);
        _fixedTimeloop.SetActive(true);
    }


}
