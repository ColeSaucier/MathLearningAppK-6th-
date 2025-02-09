using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartUp_Transitions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CheckIfOnlyScene();
    }

    void CheckIfOnlyScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        int sceneCount = SceneManager.sceneCount;
        // Check if there's only one scene loaded
        if (sceneCount == 1)
        {
            if (currentScene == "Menu")
            {
                // Load the scene at build index 0 additively, THIS IS UI
                SceneManager.LoadSceneAsync(0, LoadSceneMode.Additive);
                StartCoroutine(StartUp_EnsureAdditiveScene(0));
            }
            else
            {
                // Load the scene at build index 1 additively, THIS IS MENU
                SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
                StartCoroutine(StartUp_EnsureAdditiveScene(1));
            };
        }
        else
        {
            Debug.LogError("More than one scene is already loaded, not loading additional scene.");
        }
    }
    IEnumerator StartUp_EnsureAdditiveScene(int levelIndex)
    {
        Scene newScene = SceneManager.GetSceneByBuildIndex(levelIndex);
        while (!newScene.isLoaded)
        {
            // If the scene is not loaded, wait for it or handle this situation appropriately
            yield return null; // Wait for the load operation if you're not already doing so
        }

        // Now try to set the scene active
        if (newScene.isLoaded)
        {
            if (levelIndex != 0)
            {
                SceneManager.SetActiveScene(newScene);
            }
        }
        else
        {
            Debug.LogError("Failed to set active scene: Scene not valid or not loaded.");
            yield break; // Exit coroutine since we can't proceed
        }
    }
}
