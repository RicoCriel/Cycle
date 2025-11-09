using UnityEngine;
using UnityEngine.Events;

public class TimeLoopManager : MonoBehaviour
{
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


}
