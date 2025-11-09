using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoadManager : MonoBehaviour
{
    public static SceneLoadManager Instance;
    public Image Image;

    private Coroutine _sceneLoader;
    private const float _fadeDuration = 1;

    private void OnEnable()
    {
        TimeView.OnTimeExpired += FadeAndLoadScene;
    }

    private void OnDisable()
    {
        TimeView.OnTimeExpired -= FadeAndLoadScene;
    }


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(SceneFadeOut());
    }


    public void FadeAndLoadScene(string sceneName)
    {
        if(_sceneLoader == null)
        {
            _sceneLoader = StartCoroutine(SceneFadeIn(sceneName, _fadeDuration));
        }
        else
        {
            StopCoroutine(_sceneLoader);
        }
    }

    public void ReloadCurrentScene()
    {
        if (_sceneLoader == null)
        {
            var scene = SceneManager.GetActiveScene();
            string sceneName = scene.name;
            _sceneLoader = StartCoroutine(SceneFadeIn(sceneName, _fadeDuration));
        }
        else
        {
            StopCoroutine(_sceneLoader);
        }
    }

    private IEnumerator SceneFadeIn(string sceneName, float duration)
    {
        float t = 0;
        Color color = Image.color;
        while (t < duration)
        {
            t += Time.deltaTime;
            color.a = t / duration;
            Image.color = color;
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator SceneFadeOut()
    {
        float t = 0;
        Color color = Image.color;
        while (t < 1)
        {
            t += Time.deltaTime;
            color.a = 1f - (t/1f);
            Image.color = color;
            yield return null;
        }
    }


}
