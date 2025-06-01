using UnityEngine;
using UnityEngine.SceneManagement;  // Required for scene switching

public class LevelSwitch : MonoBehaviour
{
    private GameManager gameManager;
    public string nextLevelName;  // Fixed scene name

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        if (gameManager == null)
        {
            Debug.LogError("GameManager reference is NULL in LevelSwitch!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (gameManager == null) return;

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player reached level switch!");

            gameManager.levelComplete = true;  // Now properly accesses `levelComplete`
            gameManager.LevelComplete();  // Calls the function to trigger level completion

            // ✅ Load next level dynamically
            if (SceneExists(nextLevelName))
            {
                Debug.Log($"Loading next level: {nextLevelName}");
                SceneManager.LoadScene(nextLevelName);
            }
            else
            {
                Debug.LogError($"Scene '{nextLevelName}' not found in Build Settings!");
            }
        }
    }

    // ✅ Checks if the scene exists in Build Settings before loading it
    bool SceneExists(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            if (SceneUtility.GetScenePathByBuildIndex(i).Contains(sceneName))
                return true;
        }
        return false;
    }
}