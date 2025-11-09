using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private float _startTime;

    public static TimeManager Instance;

    public float StartTime { get {return _startTime; } }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    public void AddTime(float amount)
    {

    }




}
