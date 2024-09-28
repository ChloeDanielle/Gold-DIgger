using UnityEngine;
using UnityEngine.SceneManagement;  // Required for scene management

public class SceneLoader : MonoBehaviour
{
    // Method to load the next scene by its name or index
    public void LoadScene(string SampleScene)
    {
        SceneManager.LoadScene(SampleScene);
    }
}
