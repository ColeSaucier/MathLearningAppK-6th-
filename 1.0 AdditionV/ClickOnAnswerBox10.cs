using System;
using System.Reflection; // Add this using directive to access PropertyInfo
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickOnAnswerBox10 : MonoBehaviour
{
    public TextMeshProUGUI inputText;
    public CanvasGroup popUpCanvasGroup; // Reference to the pop-up canvas's CanvasGroup component
    public Button answerButton; // Reference to the object's Renderer component

    private bool isInputActive = false;
    private string userInput = "";
    public SceneCompleteMenu sceneCompleteScript;

    void Start()
    {
        popUpCanvasGroup.alpha = 0f; // Set the pop-up canvas's alpha to 0 (fully transparent) initially
        popUpCanvasGroup.interactable = false; // Disable interactions with the pop-up canvas
    }

    void Update()
    {
        if (isInputActive)
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
    void checkStringInput()
    {
        if (inputText.text == AnswerCalc.correctAnswer.ToString())
        {
            answerButton.image.color = Color.green;
            sceneCompleteScript.SceneComplete = true;
        }

        // Hide the pop-up canvas by setting its alpha to 0 (fully transparent)
        popUpCanvasGroup.alpha = 0f;
        isInputActive = false;
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