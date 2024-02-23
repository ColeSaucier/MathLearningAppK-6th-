using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnswerManager001KeyboardVS : MonoBehaviour
{
    // References needed for answer button
    public Button Button;
    public string userInput = "";
    private bool isInputActive = false;
    public TextMeshProUGUI inputText;
    public CanvasGroup popUpCanvasGroup;
    private TouchScreenKeyboard keyboard;

    // Scene Variables
    public string answerString;
    public bool SceneComplete;

    public SceneCompleteMenu sceneCompleteScript;

    public void Update()
    {
        if (isInputActive)
        {
            answerString = NotRowObject.answer.ToString();

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
                Button.interactable = true;

                // Close the mobile keyboard
                if (keyboard != null)
                {
                    keyboard.active = false;
                }
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

    public void activateInput()
    {
        isInputActive = !isInputActive;
        userInput = "";
        Button.interactable = false;

        if (isInputActive == true)
        { 
            // Show the pop-up canvas by setting its alpha to 1 (fully opaque)
            popUpCanvasGroup.alpha = 1f;
            popUpCanvasGroup.interactable = true; // Enable interactions with the pop-up canvas

        // Open Keyboard
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.NumberPad);
        }
        else
        {
            // Show the pop-up canvas by setting its alpha to 1 (fully opaque)
            popUpCanvasGroup.alpha = 0f;
            popUpCanvasGroup.interactable = false; // Enable interactions with the pop-up canvas

            // Close the mobile keyboard
            if (keyboard != null)
            {
                keyboard.active = false;
            }
        }
    }
}
