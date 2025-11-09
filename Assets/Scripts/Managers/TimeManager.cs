using UnityEngine;
using UnityEngine.Events;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;
    public RectTransform TimeAddedIcon;

    private Camera _camera;
    private Canvas _canvas;

    private float _bonusTime;

    public UnityEvent <float> OnTimeAdded;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        _bonusTime = 1f;
        _camera = Camera.main;
        _canvas = TimeAddedIcon.GetComponentInParent<Canvas>();
        TimeAddedIcon.gameObject.SetActive(false);
    }

    public void DisplayTimeAdded(Vector3 worldPosition)
    {
        OnTimeAdded?.Invoke(_bonusTime);
        Vector3 screenPosition = _camera.WorldToScreenPoint(worldPosition);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _canvas.transform as RectTransform,
            screenPosition,
            _canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : _camera,
            out Vector2 canvasPos
        );

        TimeAddedIcon.anchoredPosition = canvasPos;
        TimeAddedIcon.gameObject.SetActive(true);

        CancelInvoke(nameof(HideIcon));
        Invoke(nameof(HideIcon), 0.5f);
    }

    private void HideIcon()
    {
        TimeAddedIcon.gameObject.SetActive(false);
    }
}
