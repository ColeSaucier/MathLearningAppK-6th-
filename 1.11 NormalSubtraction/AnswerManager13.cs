using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnswerManager13 : MonoBehaviour
{
	public static int number1 = 5;
    public static int number2 = 7;
    public static int number3 = 3;
    public static int number4 = 4;

	public string answerString;
    public bool SceneComplete;
    public SceneCompleteMenu sceneCompleteScript;

    public TextMeshProUGUI tens1;
    public TextMeshProUGUI ones1;
    public TextMeshProUGUI tens2;
    public TextMeshProUGUI ones2;

    //public TextMeshPro playerInput;

    // References needed for answer button
    public Button Button;
    public string userInput = "";
    private bool isInputActive = false;

    public TextMeshProUGUI inputText;
    public CanvasGroup popUpCanvasGroup; // Reference to the pop-up canvas's CanvasGroup component


    //private TouchScreenKeyboard keyboard;
    // Start is called before the first frame update
    void Start()
    {
        int number1sum = Random.Range(10, 51);
        int number2sum = Random.Range(0, number1sum);
        number1 = number1sum / 10;
        number2 = number1sum % 10;
        number3 = number2sum / 10;
        number4 = number2sum % 10;

        tens1.text = number1.ToString();
        ones1.text = number2.ToString();
        tens2.text = number3.ToString();
        ones2.text = number4.ToString();

        int result = number1sum - number2sum;
        answerString = result.ToString();
    }

    /*
    public void TestPlayerInput()
    {
        if (userInput == answerString)
        {
            SceneComplete = true;
            Button.image.color = Color.green;
        }
    }
    */

    public void activateInput()
    {
        isInputActive = true;
        userInput = "";

        // Show the pop-up canvas by setting its alpha to 1 (fully opaque)
        popUpCanvasGroup.alpha = 1f;
        popUpCanvasGroup.interactable = true; // Enable interactions with the pop-up canvas


        // keyboard = TouchScreenKeyboard.Open(userInput, TouchScreenKeyboardType.Default);
    }
    public void Update()
    {
        if (isInputActive)
        {
            // Code to activate mobile keyboard here (if mobile device)

            // Check for input and handle it
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (inputText.text == answerString)
                {
                    SceneComplete = true;
                    sceneCompleteScript.SceneComplete = true;
                    Button.image.color = Color.green;
                }

                // Hide the pop-up canvas by setting its alpha to 0 (fully transparent)
                popUpCanvasGroup.alpha = 0f;
                popUpCanvasGroup.interactable = false; // Disable interactions with the pop-up canvas
                isInputActive = false;
                // Code to deactivate mobile keyboard here (if mobile device)

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
/*
   
    public void Update()
    {
        if (isInputActive)
        {
            // Check if the mobile keyboard is active
            if (keyboard != null && keyboard.active)
            {
                // Update userInput with the keyboard's text
                userInput = keyboard.text;
                inputText.text = userInput;

                if (keyboard.done)
                {
                    if (inputText.text == answerString)
                    {
                        SceneComplete = true;
                        Button.image.color = Color.green;
                    }

                    // Hide the pop-up canvas by setting its alpha to 0 (fully transparent)
                    popUpCanvasGroup.alpha = 0f;
                    popUpCanvasGroup.interactable = false; // Disable interactions with the pop-up canvas
                    isInputActive = false;

                    // Close the mobile keyboard
                    keyboard = null;
                }
            }
        }
    } 
*/
}