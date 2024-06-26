using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Reflection;

public class SceneCompleteMenu : MonoBehaviour
{
    public Button Button1;
    public Button Button2;
    public Button Button3;
    public TextMeshProUGUI ratingText;
    public TextMeshProUGUI timeText;
    //public GameObject SceneCompleteCanvas;
    public TextMeshProUGUI repCounter;
    public CanvasGroup sceneCanvasGroup;
    public CanvasGroup buttonCanvasGroup;
    public string completionRating = "";

    public bool SceneComplete = false;
    public float startTime;

    //For Data Collection/Saving
    public string scenejsonFilePath;
    private string sceneJsonString;
    public string variablejsonFilePath;
    private string variableJsonString;
    public string allSceneRatingsjsonFilePath;
    private string allSceneRatingsJsonString;
    private string filePath; 
    private SceneData sceneObject;
    private VariableData variableObject;
    private AllSceneRatingsData allSceneRatingObject;
    public string completedLevelTextFilePath;//"1.01 SubtractionV"
    List<string> levelOrder = new List<string> {"NumberCounting", "NumberCountingScattered", "BasicAdditionV", "BasicSubtractionV", "ShapePatterns", "SmallerOrBigger", "PlaceValues", "Clock", "AdditionV", "AdditionFunctionBox", "SubtractionFunctionBox", "NormalAddition", "NormalSubtraction", "MultiplicationV", "DivisionV", "LongMultiplication", "FractionFromShape", "FractionEqualize", "FractionEqualizeHard", "LongDivision"};
    public string currentScene;// = text.gameObject.name;

    public GameObject barHolder;
    public Image beatScoreBar;
    public float repPaceTime;
    public float totalTimeToBeatScore;
    public float elapsedTime;

    public GameObject improveAnimation1;
    public GameObject improveAnimation2;
    public GameObject correctAnimation;

    // Start is called before the first frame update
    void Start()
    {
        sceneCanvasGroup.interactable = false;

        //retrieve data and create dataObject
        sceneObject = new SceneData();
        filePath = Path.Combine(Application.persistentDataPath, scenejsonFilePath);
        //filePath = Path.Combine(Application.dataPath, scenejsonFilePath); // Combine with the Assets folder
        sceneJsonString = File.ReadAllText(filePath);
        sceneObject = JsonUtility.FromJson<SceneData>(sceneJsonString);

        //retrieve data and create dataObject
        variableObject = new VariableData();
        filePath = Path.Combine(Application.persistentDataPath, variablejsonFilePath);
        //filePath = Path.Combine(Application.dataPath, variablejsonFilePath); // Combine with the Assets folder
        variableJsonString = File.ReadAllText(filePath);
        variableObject = JsonUtility.FromJson<VariableData>(variableJsonString);

        //Updates repCounter, current scene, and starts counter
        repCounter.text = $"{variableObject.counterScene}/{sceneObject.numRepetitions}";
        currentScene = SceneManager.GetActiveScene().name;
        variableObject.currentScene = currentScene;
        startTime = Time.time;

        //Vars for beatScoreBar
        totalTimeToBeatScore = sceneObject.bestTime - variableObject.timeElapsed;
        repPaceTime = totalTimeToBeatScore / (sceneObject.numRepetitions - variableObject.counterScene);
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneComplete)
        {
            SceneComplete = false;
            barHolder.SetActive(false);
            sceneComplete();
        }
        // checks if variable 
        elapsedTime = Time.time - startTime;
        // beatScoreBar Updating
        if (elapsedTime > totalTimeToBeatScore)
            {
            beatScoreBar.color = Color.red;
            beatScoreBar.fillAmount = (float)(0.1);
            }
        else if (elapsedTime > repPaceTime)
           beatScoreBar.color = Color.yellow;
        else
            beatScoreBar.fillAmount = (float)(0.1 + 0.9 * ((repPaceTime - elapsedTime) / repPaceTime));

    }

    void sceneComplete()
    {
        //Record the duration of one scene repetition, cumalatively into variableObject
        //Raise rep counter
        float elapsedTimeFinal = Time.time - startTime;
        variableObject.timeElapsed += (int)elapsedTimeFinal;
        //variableObject.counterScene = 0;
        variableObject.counterScene++;
        repCounter.text = $"{variableObject.counterScene}/{sceneObject.numRepetitions}";

        //Determine if Reps are completed or to restart
        if (variableObject.counterScene != sceneObject.numRepetitions)
        {
            //saveData
            filePath = Path.Combine(Application.persistentDataPath, variablejsonFilePath);
            string variableJsonString = JsonUtility.ToJson(variableObject);
            File.WriteAllText(filePath, variableJsonString);
            string thisScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(thisScene);
        }
        if (variableObject.counterScene == sceneObject.numRepetitions)
        {
            RepsCompleted();
        }
    }

    void RepsCompleted()
    {
        // Record time in scene json
        if (variableObject.timeElapsed < sceneObject.bestTime)
        {
            sceneObject.bestTime = variableObject.timeElapsed;
            timeText.color = Color.blue;
            ratingText.color = Color.blue;
            improveAnimation1.SetActive(true);
            improveAnimation2.SetActive(true);
        } 
        else
            correctAnimation.SetActive(true);

        // Determine Rating (+record it)
        completionRating = "Bronze";
        if (sceneObject.bestTime <= sceneObject.goldTime)
            completionRating = "Gold";
        if (sceneObject.bestTime <= sceneObject.perfTime)
            completionRating = "Perfect++";

        UpdateValue_AllSceneRatings(currentScene, completionRating);

        //Assigns repetitions menu complete variables
        sceneObject.bestRating = completionRating;
        ratingText.text = completionRating; 
        int minutes = variableObject.timeElapsed / 60; //sceneObject.bestTime
        int seconds = variableObject.timeElapsed % 60;
        string timeString = $"{minutes}min :{seconds}sec";
        timeText.text = timeString;

        //Reset variableObject
        variableObject.counterScene = 0;
        variableObject.timeElapsed = 0;

        //saveData
        filePath = Path.Combine(Application.persistentDataPath, variablejsonFilePath);
        string variableJsonString = JsonUtility.ToJson(variableObject);
        File.WriteAllText(filePath, variableJsonString);
        
        filePath = Path.Combine(Application.persistentDataPath, scenejsonFilePath);
        string sceneJsonString = JsonUtility.ToJson(sceneObject);
        File.WriteAllText(filePath, sceneJsonString);
        //Debug.LogError("This work");

        //save completed string
        filePath = Path.Combine(Application.persistentDataPath, completedLevelTextFilePath);
        AddCompletedScene(currentScene);
        //Debug.LogError("This didnt work");

        //Introduce the scene canvas
        sceneCanvasGroup.alpha = 1f;
        sceneCanvasGroup.interactable = true;
        buttonCanvasGroup.blocksRaycasts = true;
        //SceneCompleteCanvas.SetActive(true);
    }

    public void RestartScene()
    {
        //Reset variableObject
        variableObject.counterScene = 0;
        variableObject.timeElapsed = 0;
        filePath = Path.Combine(Application.persistentDataPath, variablejsonFilePath);
        string variableJsonString = JsonUtility.ToJson(variableObject);
        File.WriteAllText(filePath, variableJsonString);

        string thisScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(thisScene);
    }
    public void GoToMenu()
    {
        //Reset variableObject
        variableObject.counterScene = 0;
        variableObject.timeElapsed = 0;
        filePath = Path.Combine(Application.persistentDataPath, variablejsonFilePath);
        string variableJsonString = JsonUtility.ToJson(variableObject);
        File.WriteAllText(filePath, variableJsonString);
        SceneManager.LoadScene("Menu");
    }

    public void BeginNextScene()
    {
        if (MixIt.mixBool)
        {
            //Same as MixIt StartRandomMixLevel()
            if (MixIt.mixList.Count > 0)
            {
                // Generate a random index within the bounds of mixList
                int randomIndex = UnityEngine.Random.Range(0, MixIt.mixList.Count);

                // Access the random entry from mixList
                string nextScene = MixIt.mixList[randomIndex];

                MixIt.mixList.Remove(nextScene);

                SceneManager.LoadScene(nextScene);
            }
            else
            {
                //go back to menu
                SceneManager.LoadScene("Speed");
            }
        }
        else
        {
            //determine next scene
            string thisScene = SceneManager.GetActiveScene().name;
            int index = levelOrder.IndexOf(thisScene);
            string nextScene = null;

            // Check if the string was found and has a next element
            if (index != -1 && index < levelOrder.Count - 1)
            {
                // Get the next string in the list
                nextScene = levelOrder[index + 1];
            }

            SceneManager.LoadScene(nextScene);
        }
    }

    void AddCompletedScene(string sceneName)
    {
        //Adds the scene name to .txt list, after reps completed
        List<string> completedLevelTextList = GetStringListFromFile();
        completedLevelTextList.Add(sceneName);
        File.WriteAllLines(filePath, completedLevelTextList);
    }

    List<string> GetStringListFromFile()
    {
        if (File.Exists(filePath))
        {
            return new List<string>(File.ReadAllLines(filePath));
        }
        return new List<string>();
    }


    [Serializable]
    public class SceneData
    {
        public int bestTime;
        public string bestRating;
        public int goldTime;
        public int perfTime;
        public int numRepetitions;
    }

    [Serializable]
    public class VariableData
    {
        public string currentScene;
        public int counterScene;
        public int timeElapsed;
    }

    [Serializable]
    public class AllSceneRatingsData
    {
        public int goldTotal;
        public string NumberCounting;
        public string NumberCountingScattered;
        public string BasicAddition;
        public string BasicSubtraction;
        public string ShapePatterns;
        public string SmallerOrBigger;
        public string Clock;
        public string PlaceValues;
        public string AdditionV;
        public string AdditionFunctionBox;
        public string SubtractionFunctionBox;
        public string NormalAddition;
        public string NormalSubtraction;
        public string MultiplicationV;
        public string DivisionV;
        public string LongMultiplication;
        public string FractionFromShape;
        public string FractionEqualize;
        public string FractionEqualizeHard;
        public string LongDivision;
    }
    
    //For AllSceneRatingsData
    void UpdateValue_AllSceneRatings(string key, string rating)
    {
        // Convert rating to number
        string valueRating;
        valueRating = "1";
        if (rating == "Gold")
            valueRating = "2";
        if (rating == "Perfect++")
            valueRating = "3";

        //retrieve data and create dataObject
        allSceneRatingObject = new AllSceneRatingsData();
        filePath = Path.Combine(Application.persistentDataPath, allSceneRatingsjsonFilePath); // Combine with the Assets folder
        allSceneRatingsJsonString = File.ReadAllText(filePath);
        allSceneRatingObject = JsonUtility.FromJson<AllSceneRatingsData>(allSceneRatingsJsonString);

        Dictionary<string, string> allSceneRatingDictionary = new Dictionary<string, string>
        {
            { "NumberCounting", allSceneRatingObject.NumberCounting},
            { "NumberCountingScattered", allSceneRatingObject.NumberCountingScattered},
            { "BasicAdditionV", allSceneRatingObject.BasicAddition},
            { "BasicSubtractionV", allSceneRatingObject.BasicSubtraction},
            { "ShapePatterns", allSceneRatingObject.ShapePatterns},
            { "SmallerOrBigger", allSceneRatingObject.SmallerOrBigger},
            { "Clock", allSceneRatingObject.Clock},
            { "PlaceValues", allSceneRatingObject.PlaceValues},
            { "AdditionV", allSceneRatingObject.AdditionV},
            { "AdditionFunctionBox", allSceneRatingObject.AdditionFunctionBox},
            { "SubtractionFunctionBox", allSceneRatingObject.SubtractionFunctionBox},
            { "NormalAddition", allSceneRatingObject.NormalAddition},
            { "NormalSubtraction", allSceneRatingObject.NormalSubtraction},
            { "MultiplicationV", allSceneRatingObject.MultiplicationV},
            { "DivisionV", allSceneRatingObject.DivisionV},
            { "LongMultiplication", allSceneRatingObject.LongMultiplication},
            { "FractionFromShape", allSceneRatingObject.FractionFromShape},
            { "FractionEqualize", allSceneRatingObject.FractionEqualize},
            { "FractionEqualizeHard", allSceneRatingObject.FractionEqualizeHard},
            { "LongDivision", allSceneRatingObject.LongDivision}
        };

        allSceneRatingDictionary[key] = valueRating;
        ConvertDictionaryToJson__SaveIt(allSceneRatingDictionary);
    }

    public void ConvertDictionaryToJson__SaveIt(Dictionary<string, string> dictionary)
    {
        allSceneRatingObject.NumberCounting = dictionary["NumberCounting"];
        allSceneRatingObject.NumberCountingScattered = dictionary["NumberCountingScattered"];
        allSceneRatingObject.BasicAddition = dictionary["BasicAdditionV"];
        allSceneRatingObject.BasicSubtraction = dictionary["BasicSubtractionV"];
        allSceneRatingObject.ShapePatterns = dictionary["ShapePatterns"];
        allSceneRatingObject.SmallerOrBigger = dictionary["SmallerOrBigger"];
        allSceneRatingObject.Clock = dictionary["Clock"];
        allSceneRatingObject.PlaceValues = dictionary["PlaceValues"];
        allSceneRatingObject.AdditionV = dictionary["AdditionV"];
        allSceneRatingObject.AdditionFunctionBox = dictionary["AdditionFunctionBox"];
        allSceneRatingObject.SubtractionFunctionBox = dictionary["SubtractionFunctionBox"];
        allSceneRatingObject.NormalAddition = dictionary["NormalAddition"];
        allSceneRatingObject.NormalSubtraction = dictionary["NormalSubtraction"];
        allSceneRatingObject.MultiplicationV = dictionary["MultiplicationV"];
        allSceneRatingObject.DivisionV = dictionary["DivisionV"];
        allSceneRatingObject.LongMultiplication = dictionary["LongMultiplication"];
        allSceneRatingObject.FractionFromShape = dictionary["FractionFromShape"];
        allSceneRatingObject.FractionEqualize = dictionary["FractionEqualize"];
        allSceneRatingObject.FractionEqualizeHard = dictionary["FractionEqualizeHard"];
        allSceneRatingObject.LongDivision = dictionary["LongDivision"];

        //saveData
        filePath = Path.Combine(Application.persistentDataPath, allSceneRatingsjsonFilePath);
        string allSceneRatingsJsonString = JsonUtility.ToJson(allSceneRatingObject);
        File.WriteAllText(filePath, allSceneRatingsJsonString);
    }
}
