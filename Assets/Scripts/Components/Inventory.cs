using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public int KeyCardCount;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    public void IncreaseKeyCardCount()
    {
        KeyCardCount++;
    }

    public void DecreaseKeyCardCount()
    {
        KeyCardCount--;
    }


}
