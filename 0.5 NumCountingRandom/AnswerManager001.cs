using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnswerManager001 : MonoBehaviour
{
    // References needed for answer button
    public Button Button;
    public string userInput = "";
    private bool isInputActive = false;
    public TextMeshProUGUI inputText;
    public CanvasGroup popUpCanvasGroup;
    private TouchScreenKeyboard keyboard;
    private bool mobileVersion = true;

    // Scene Variables
    public string answerString;
    public bool SceneComplete;

    public SceneCompleteMenu sceneCompleteScript;

    public void Update()
    {
        if (isInputActive)
        {
            answerString = NotRowObject.answer.ToString();

            if (mobileVersion)
            {
                inputText.text = keyboard.text;
                
                if (keyboard.status == TouchScreenKeyboard.Status.Done)
                    checkStringInput();
            }
            else
            {
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
        //Button.interactable = true;

        // Close the mobile keyboard
        if (keyboard != null)
        {
            keyboard.active = false;
        }
    }
    public void activateInput()
    {
        isInputActive = !isInputActive;
        userInput = "";
        //Button.interactable = false;

        if (isInputActive == true)
        { 
            // Show the pop-up canvas by setting its alpha to 1 (fully opaque)
            popUpCanvasGroup.alpha = 1f;

            if (mobileVersion)
                keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.NumberPad);
        }
        else
        {
            // Show the pop-up canvas by setting its alpha to 1 (fully opaque)
            popUpCanvasGroup.alpha = 0f;
            checkStringInput();

            // Close the mobile keyboard
            if (keyboard != null)
            {
                keyboard.active = false;
            }
        }
    }
}
