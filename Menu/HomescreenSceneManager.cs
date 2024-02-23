using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using System.IO;

public class HomescreenSceneManager : MonoBehaviour
{
    public GameObject menu;
    public GameObject loadingInterface;
    public Image progressBar;

    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();

    public List<string> completedLevels;
    private string filePath;
    public string completedLevelTextFilePath;
    public string currentLevel;

    List<string> levelOrder = new List<string> {"NumberCounting", "NumberCountingScattered", "BasicAdditionV", "BasicSubtractionV", "ShapePatterns", "SmallerOrBigger", "PlaceValues", "Clock", "AdditionV", "AdditionFunctionBox", "SubtractionFunctionBox", "NormalAddition", "NormalSubtraction", "MultiplicationV", "DivisionV", "LongMultiplication", "FractionIdentification", "FractionEqualize", "FractionEqualizeHard", "LongDivision"};


    private void Start()
    {
        filePath = Path.Combine(Application.dataPath, completedLevelTextFilePath);
        completedLevels = GetUniqueValuesFromFile();
    }

    List<string> GetUniqueValuesFromFile()
    {
        List<string> allValues = GetStringListFromFile();

        // Use a HashSet to efficiently remove duplicates
        HashSet<string> uniqueSet = new HashSet<string>(allValues);

        // Convert back to a List if needed
        List<string> completedLevels = new List<string>(uniqueSet);

        return completedLevels;
    }
    public void StartFirstScene()
    {
        HideMenu();
        ShowLoadingBar();
        if (completedLevels.Count == 0)
            scenesToLoad.Add(SceneManager.LoadSceneAsync("NumberCounting"));
        else
        { 
            currentLevel = completedLevels[completedLevels.Count - 1];
            int index = levelOrder.IndexOf(currentLevel);
            currentLevel = levelOrder[index + 1];

            scenesToLoad.Add(SceneManager.LoadSceneAsync(currentLevel));
        }
        StartCoroutine(LoadingScreen());
    }
    public void speedLevelMenu()
    {
        HideMenu();
        ShowLoadingBar();
        scenesToLoad.Add(SceneManager.LoadSceneAsync("Speed"));
        StartCoroutine(LoadingScreen());
    }

    public void StartAnyScene(string levelName)
    {
        scenesToLoad.Add(SceneManager.LoadSceneAsync(levelName));
        StartCoroutine(LoadingScreen());
    }

    public void HideMenu()
    {
        menu.SetActive(false);
    }

    public void ShowLoadingBar()
    {
        loadingInterface.SetActive(true);
    }

    IEnumerator LoadingScreen()
    {
        float progressValue = 0;
        for(int i=0; i<scenesToLoad.Count; ++i)
        {
            while (!scenesToLoad[i].isDone)
            {
                progressValue += scenesToLoad[i].progress;
                progressBar.fillAmount = progressValue / scenesToLoad.Count;
                yield return null;
            }
        }
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    List<string> GetStringListFromFile()
    {
        if (File.Exists(filePath))
        {
            return new List<string>(File.ReadAllLines(filePath));
        }
        return new List<string>();
    }
}
