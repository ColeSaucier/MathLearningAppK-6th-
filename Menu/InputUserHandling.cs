using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;
using Client = Supabase.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

public class InputUserHandling : MonoBehaviour
{
    public TMP_InputField firstNameInputField;
    public TMP_InputField ageInputField;
    public TMP_Dropdown dropdown;
    public TMP_Dropdown dropdownGender;
    public TMP_InputField usernameInputField;
    public TMP_InputField haveUsernameInputField;

    public Image firstNameImageC;
    public Image ageImageC;
    public Image usernameImageC;
    public Image dropdownImageC;
    public Image alreadyusernameImageC;

    public Image firstNameImageX;
    public Image ageImageX;
    public Image usernameImageX;
    public Image alreadyusernameImageX;
    private Client supabase;

    public CanvasGroup canvas;

    private VariableData variableObject;
    public string variablejsonFilePath;
    private string variableJsonString;
    private string filePath;
    public string playerjsonFilePath;
    private string playerjsonString;
    private PlayerData playerObject;

    public TextMeshProUGUI MenuGradeText;
    public TextMeshProUGUI MenuUsername;

    public List<string> completedLevelTextList;
    public string completedLevelTextFilePath;

    private void Start()
    {
        playerObject = new PlayerData();
        LoadPlayerData();

        supabase = new Client("https://acpornqddkzqsdppbabw.supabase.co", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImFjcG9ybnFkZGt6cXNkcHBiYWJ3Iiwicm9sZSI6ImFub24iLCJpYXQiOjE3MTM2NTU5MzcsImV4cCI6MjAyOTIzMTkzN30.UQ73w2nx-UxmXhxBF2_jSTl19aZ1bjb9LjYY4eraMtY");
        if (playerObject.user == "null")
        {
            canvas.alpha = 1f;
            canvas.interactable = true;
            canvas.blocksRaycasts = true;
        }
        else
        {
            MenuGradeText.text = playerObject.menuText;
            playerObject.menuText = MenuGradeText.text;
            SavePlayerData();
            MenuUsername.text = $"Player/Username: {playerObject.user}";
            //Debug.LogError($"Player/Username: {playerObject.user}");
            //THIS SETS GO BUTTON'S "CURRENT LEVEL"
            variableObject = new VariableData();
            LoadVariableData();
            variableObject.user = playerObject.user;
            string currentScene = variableObject.currentScene;
            SaveVariableData();
            //Debug.LogError(variableObject.currentScene);
            //Debug.LogError(currentScene);
            
            filePath = Path.Combine(Application.persistentDataPath, completedLevelTextFilePath);
            List<string> completedLevelTextList = GetStringListFromFile();
            string gradeText = MenuGradeText.text;
            // Check if the grade level text needs an update based on the length of the current text
            if (gradeText.Length < 5)
            {
                // Determine grade level based on completed levels
                if (completedLevelTextList.Contains("LongDivision"))
                {
                    MenuGradeText.text = "Math Goat";
                }
                else if (completedLevelTextList.Contains("LongMultiplication"))
                {
                    MenuGradeText.text = "4th";
                }
                else if (completedLevelTextList.Contains("MultiplicationV"))
                {
                    MenuGradeText.text = "3rd";
                }
                else if (completedLevelTextList.Contains("SmallerOrBigger"))
                {
                    MenuGradeText.text = "2nd";
                }
                else if (completedLevelTextList.Contains("BasicSubtractionV"))  // Fixed incorrect string check
                {
                    MenuGradeText.text = "1st";
                }
            }
        }
    }
    List<string> GetStringListFromFile()
    {
        if (File.Exists(filePath))
        {
            return new List<string>(File.ReadAllLines(filePath));
        }
        return new List<string>();
    }

    public void ValidateFirstName()
    {
        // Check if first name is not empty
        if (!string.IsNullOrEmpty(firstNameInputField.text))
        {
            // Capitalize the first letter
            string capitalizedFirstName = char.ToUpper(firstNameInputField.text[0]) + firstNameInputField.text.Substring(1).ToLower();
            
            // Set the input field text to the capitalized version
            firstNameInputField.text = capitalizedFirstName;

            // Activate the checkmark image
            firstNameImageC.gameObject.SetActive(true);
        }
    }

    public void ValidateAge()
    {
        // Check if age is a valid integer
        if (ageInputField.text != "")
        {
            if (int.TryParse(ageInputField.text, out int parsedAge))
            {
                // Check if the parsed age is within a reasonable range, e.g., 0 to 120 years
                if (parsedAge >= 0 && parsedAge <= 120) 
                {
                    ageImageC.gameObject.SetActive(true);
                    ageImageX.gameObject.SetActive(false);
                }
                else
                {
                    ageImageX.gameObject.SetActive(true);
                    ageImageC.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            ageImageX.gameObject.SetActive(true);
            ageImageC.gameObject.SetActive(false);
        }
    }

    public void CreatePossibleUsername()
    {
        // Autofill username if first name and age are complete
        if (firstNameInputField.text != "" && ageInputField.text != "") 
        {
            usernameInputField.text = firstNameInputField.text + ageInputField.text;
        }
        ValidateUsername();
    }

    public async void ValidateUsername()
    {
        if (firstNameInputField.text != "" && ageInputField.text != "") 
        {
            if (!(await ValidateUsernameSupabase(usernameInputField)))
            {
                Debug.LogError("User not exists");
                usernameImageC.gameObject.SetActive(true);
                usernameImageX.gameObject.SetActive(false);
            }
            else
            {
                Debug.LogError("User exists");    
                usernameImageX.gameObject.SetActive(true);
                usernameImageC.gameObject.SetActive(false);
            }
        }
    }

    public async void ValidateAlreadyUsername()
    {
        bool exists = await ValidateUsernameSupabase(haveUsernameInputField);
        if (exists)
        {
            Debug.LogError("User exists"); 
            alreadyusernameImageC.gameObject.SetActive(true);
            alreadyusernameImageX.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("User not exists");
            alreadyusernameImageX.gameObject.SetActive(true);
            alreadyusernameImageC.gameObject.SetActive(false);
        }
    }

    // Helper method to validate inputs for EnterGame()
    private bool IsInputValid()
    {
        return !string.IsNullOrEmpty(firstNameInputField.text) &&
               !string.IsNullOrEmpty(ageInputField.text) &&
               !string.IsNullOrEmpty(usernameInputField.text) &&
               int.TryParse(ageInputField.text, out _); // Check if age is a valid integer
    }

    public async void EnterGame()
    {
        if (usernameImageC.gameObject.activeSelf || alreadyusernameImageC.gameObject.activeSelf)
        {
            //Handle updates to persistance files
            variableObject = new VariableData();
            LoadVariableData();
            //Player data should be loaded from start()

            //case when player already has a username (and fills it correctly)
            if (alreadyusernameImageC.gameObject.activeSelf)
            {
                variableObject.user = haveUsernameInputField.text;
                playerObject.user = haveUsernameInputField.text;
                MenuGradeText.text = playerObject.menuText; //Default "math goat"
                MenuUsername.text = $"Player/Username: {playerObject.user}";

                SaveVariableData();
            }
            //case when player fills out new user form and already user not filled
            else if (IsInputValid() & usernameImageC.gameObject.activeSelf)
            {
                // Assuming InsertUserAsync takes a username, firstname, and age
                await InsertUserAsync();
                variableObject.user = usernameInputField.text;
                playerObject.user = usernameInputField.text;
                MenuUsername.text = $"Player/Username: {playerObject.user}";

                int value = dropdown.value;
                // Get the text of the current selected item, based on the index
                string GradeText = dropdown.options[value].text;

                if (GradeText != "n/a")
                {
                    MenuGradeText.text = GradeText;
                    playerObject.menuText = GradeText;
                    if (GradeText == "1st")
                    {
                        variableObject.currentScene = "BasicAdditionV";
                    }
                    else if (GradeText == "2nd")
                    {
                        variableObject.currentScene = "SmallerOrBigger";
                    }
                    else if (GradeText == "3rd")
                    {
                        variableObject.currentScene = "NormalAddition";
                    }
                    else if (GradeText == "4th")
                    {
                        variableObject.currentScene = "LongMultiplication";
                    }
                }
                else
                {
                    MenuGradeText.text = "Math Goat";
                    playerObject.menuText = "Math Goat";
                    variableObject.currentScene = "NormalAddition";
                }
            }

            //saveData ####
            SaveVariableData();
            SavePlayerData();

            //Both
            canvas.alpha = 0f;
            canvas.interactable = false;
            canvas.blocksRaycasts = false;
        }
    }
    private void LoadPlayerData()
    {
        //Load the data
        string filePath = Path.Combine(Application.persistentDataPath, playerjsonFilePath);
        playerjsonString = File.ReadAllText(filePath);
        playerObject = JsonUtility.FromJson<PlayerData>(playerjsonString);
    }

    private void SavePlayerData()
    {
        //save the data
        string filePath = Path.Combine(Application.persistentDataPath, playerjsonFilePath);
        playerjsonString = JsonUtility.ToJson(playerObject);
        File.WriteAllText(filePath, playerjsonString);
    }
    private void LoadVariableData()
    {
        //Load the data
        string filePath = Path.Combine(Application.persistentDataPath, variablejsonFilePath);
        variableJsonString = File.ReadAllText(filePath);
        variableObject = JsonUtility.FromJson<VariableData>(variableJsonString);
        Debug.LogError("variableJsonString - Load: " + variableJsonString);
    }

    private void SaveVariableData()
    {
        //save the data
        string filePath = Path.Combine(Application.persistentDataPath, variablejsonFilePath);
        variableJsonString = JsonUtility.ToJson(variableObject);
        File.WriteAllText(filePath, variableJsonString);
        Debug.LogError("variableJsonString - Save: " + variableJsonString);
    }

    public async Task InsertUserAsync()
    {
        Debug.LogError($"line inserted!!");
        int index = dropdown.value;
        // Get the text of the current selected item, based on the index
        string GradeselectedText = dropdown.options[index].text;

        index = dropdownGender.value;
        // Get the text of the current selected item, based on the index
        string GenderselectedText = dropdownGender.options[index].text;

        var parameters = new Dictionary<string, object>
        {
            { "p_user_name", usernameInputField.text },
            { "p_firstname", firstNameInputField.text },
            { "p_age", ageInputField.text },
            { "p_grade", GradeselectedText },
            { "p_gender", GenderselectedText }
        };

        try
        {
            var response = await supabase.Rpc("insert_player", parameters);
            Debug.LogError("User inserted successfully.");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error inserting user: {ex.Message}");
        }
    }

    public void UpdateDropdownOptions()
    {
        dropdownImageC.gameObject.SetActive(true);
    }

    public async Task<bool> ValidateUsernameSupabase(TMP_InputField input)
    {
        string username = input.text.ToString();
        // Call the CheckUserNameValid function using Supabase's RPC
        var response = await supabase.Rpc("checkusernamevalidtest", new Dictionary<string, object>
        {
            { "username_param", username} 
        });
        if (response != null)
        {
            Debug.LogError($"Making RPC Call with: {JsonConvert.SerializeObject(new Dictionary<string, object> { { "username_param", username } })}");
            // Parse the result from the response
            bool isValid = JsonConvert.DeserializeObject<bool>(response.Content.ToString());
            Debug.LogError($"Output: {isValid}");
            return isValid;
        }
        else
        {
            // Handle error - response is null
            Debug.LogError("Failed to call CheckUserNameValid function.");
            return false;
        }
    }

    [Serializable]
    public class VariableData
    {
        public string user;
        public string currentScene;
        public int counterScene;
        public float timeElapsed;
        public int setId;
    }

    [Serializable]
    public class PlayerData
    {
        public string user;
        public string menuText;
        public int gemTotal;
        public bool timeEnabled;
        public bool timeEnabledNotPace;
        public bool leaderboardEnabled;
        public string swipeRight;
        public string swipeLeft;
        public string swipeDown;
        public string swipeUp;
    }
}