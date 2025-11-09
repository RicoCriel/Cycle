using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _keycard;

    public void SpawnKeyCard()
    {
        GameObject keycard = Instantiate(_keycard, transform.position, Quaternion.identity);
    }
}
