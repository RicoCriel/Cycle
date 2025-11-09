using System.Collections;
using UnityEngine;

public class BulletTrail : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(WaitAndDestroy());
    }

    IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
