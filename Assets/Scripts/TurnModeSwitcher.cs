using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class TurnModeSwitcher : MonoBehaviour
{
    public Dropdown turnModeDropdown;
    public ActionBasedSnapTurnProvider snapTurnProvider;
    public ActionBasedContinuousTurnProvider continuousTurnProvider;

    void Start()
    {
        // Add listener to the dropdown to detect changes
        turnModeDropdown.onValueChanged.AddListener(OnDropdownValueChange);
        
        // Set default turn mode
        SetTurnMode(turnModeDropdown.value);
    }

    // Method that triggers when dropdown value changes
    public void OnDropdownValueChange(int index)
    {
        SetTurnMode(index);
    }

    // Method to set turn mode based on dropdown index
    private void SetTurnMode(int index)
    {
        if (index == 0) // Snap Turn
        {
            snapTurnProvider.enabled = true;
            continuousTurnProvider.enabled = false;
        }
        else if (index == 1) // Continuous Turn
        {
            snapTurnProvider.enabled = false;
            continuousTurnProvider.enabled = true;
        }
    }
}
