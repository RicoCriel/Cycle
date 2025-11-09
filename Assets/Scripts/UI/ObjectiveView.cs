using TMPro;
using UnityEngine;

public class ObjectiveView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _objectiveText;
    [SerializeField] private string _endGameText = "ENTER THE PORTAL";

    public void DisplayEndObjective()
    {
        _objectiveText.text = _endGameText;
    }


}
