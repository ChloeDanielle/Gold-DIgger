using UnityEngine;

public class AboutPanelController : MonoBehaviour
{
    public GameObject aboutPanel;  // Reference to the About Panel

    // Method to toggle the visibility of the about panel
    public void TogglePanel()
    {
        bool isActive = aboutPanel.activeSelf;
        aboutPanel.SetActive(!isActive);  // Toggle panel visibility
    }

    // Method to close the panel (for the X button)
    public void ClosePanel()
    {
        aboutPanel.SetActive(false);  // Hide the About panel
    }
}
