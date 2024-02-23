using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnswerManager16 : MonoBehaviour
{
    public int answer; // Public variable to store the answer
    public CircleColumnManipulation CircleColumnManipulation;
    
    // References needed for answer button
    public Button Button;
    public string userInput = "";
    private bool isInputActive = false;
    public TextMeshProUGUI inputText;
    public CanvasGroup popUpCanvasGroup;

    // Scene Variables
    public string answerString;
    public bool SceneComplete;
    public SceneCompleteMenu sceneCompleteScript;

    // Start is called before the first frame update
    void Start()
    {
        // Generate two random numbers between 1 and 10
        int num1 = Random.Range(1, 11);
        int num2 = Random.Range(1, 11);
    }

    public void Update()
    {
        if (isInputActive)
        {
            // Get the answer
            answer = CircleColumnManipulation.answer;
            answerString = answer.ToString();
            // Code to activate mobile keyboard here (if mobile device)

            // Check for input and handle it
            if (Input.GetKeyDown(KeyCode.Return))
            {
                checkStringInput();
            }
            else if (Input.GetKeyDown(KeyCode.Backspace) && userInput.Length > 0)
            {
                userInput = userInput.Substring(0, userInput.Length - 1);
            }
            else
            {
                userInput += Input.inputString;
            }
            inputText.text = userInput;
        }
    }

    void checkStringInput()
    {
        if (inputText.text == answerString)
        {
            SceneComplete = true;
            sceneCompleteScript.SceneComplete = true;
            Button.image.color = Color.green;
        }

        // Hide the pop-up canvas by setting its alpha to 0 (fully transparent)
        popUpCanvasGroup.alpha = 0f;
        isInputActive = false;
        // Code to deactivate mobile keyboard here (if mobile device)
    }
    public void activateInput()
    {
        isInputActive = !isInputActive;

        if (isInputActive == true)
        {
            userInput = "";
            // Show the pop-up canvas by setting its alpha to 1 (fully opaque)
            popUpCanvasGroup.alpha = 1f;    

            // Later code
            // keyboard = TouchScreenKeyboard.Open(userInput, TouchScreenKeyboardType.Default);
        }
        if (isInputActive == false)
        {
            checkStringInput();
        }
    }
}