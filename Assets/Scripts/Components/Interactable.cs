using StarterAssets;
using TMPro;
using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{
    [SerializeField] private TextMeshProUGUI _interactText;
    [SerializeField] private string _defaultText = "PRESS [E] TO UNLOCK";
    [SerializeField] private string _findKeyText = "FIND KEYCARD";
    [SerializeField] private string _unlockedText = "TERMINAL UNLOCKED";

    [SerializeField] private GameObject _lockedLight;
    [SerializeField] private GameObject _unlockedLight;

    private bool _isUnlocked;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<FirstPersonController>() && !_isUnlocked)
        {
            _interactText.text = _defaultText;
            if(_isUnlocked)
            {
                _interactText.text = _unlockedText;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<FirstPersonController>())
            _interactText.text = "";
    }

    public void Interact()
    {
        if (_isUnlocked)
            return;

        if (Inventory.Instance.KeyCardCount > 0)
        {
            Inventory.Instance.DecreaseKeyCardCount();
            _isUnlocked = true;
            ShowUnlockedLights();
            _interactText.text = _unlockedText;

            TimeLoopManager.Instance.UnlockTerminal();
            TimeLoopManager.Instance.TriggerGameEnding();
        }
        else
        {
            _interactText.text = _findKeyText;
        }
    }

    private void ShowUnlockedLights()
    {
        _lockedLight.SetActive(false);
        _unlockedLight.SetActive(true);
    }
}
