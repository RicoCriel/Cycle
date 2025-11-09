using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _keycard;
    [SerializeField] private GameObject _portal;

    public void SpawnKeyCard()
    {
        if(_keycard != null )
        {
            GameObject keycard = Instantiate(_keycard, transform.position, Quaternion.identity);
        }
    }

    public void SpawnWormHole()
    {
        if (_portal != null)
        {
            GameObject portal = Instantiate(_portal, transform.position, Quaternion.identity);
        }
    }
}
