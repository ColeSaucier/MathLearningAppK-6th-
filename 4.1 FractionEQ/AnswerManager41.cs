using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnswerManager41 : MonoBehaviour
{
    public bool SceneComplete;
    public SceneCompleteMenu sceneCompleteScript;
    public bool isInputActive = false;
    public bool secondInput = false;

    public FractionHandler fractionHandler;
    public int copiedNumerator;
    public int copiedDenominator;

    public TextMeshProUGUI numerator;
    public TextMeshProUGUI denominator;
    
    public CanvasGroup popUpCanvasGroup;
    public Button Button;
    public string userInput;

    public GameObject ansPanel1;
    public GameObject ansPanel2;
    public void activateInput()
    {
        copiedNumerator = fractionHandler.numeratorAns;
        copiedDenominator = fractionHandler.denominatorAns;

        isInputActive = true;
        secondInput = false;
        userInput = "";

        // Show the pop-up canvas by setting its alpha to 1 (fully opaque)
        popUpCanvasGroup.alpha = 1f;
        popUpCanvasGroup.interactable = true; // Enable interactions with the pop-up canvas
        Button.interactable = false;

        // keyboard = TouchScreenKeyboard.Open(userInput, TouchScreenKeyboardType.Default);
    }

    // Update is called once per frame
    void Update()
    {
        if (isInputActive)
        {
            // Check for input and handle it
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (secondInput == true)
                {
                    if (numerator.text == copiedNumerator.ToString() && denominator.text == copiedDenominator.ToString()) 
                    {
                        SceneComplete = true;
                        sceneCompleteScript.SceneComplete = true;
                        Button.image.color = Color.green;
                    }

                    isInputActive = false;
                    // Hide the pop-up canvas by setting its alpha to 0 (fully transparent)
                    popUpCanvasGroup.alpha = 0f;
                    popUpCanvasGroup.interactable = false; // Disable interactions with the pop-up canvas
                    Button.interactable = true;
                    // Code to deactivate mobile keyboard here (if mobile device)
                }

                else
                {
                    secondInput = true;
                    userInput = "";
                    ansPanel1.SetActive(false);
                    ansPanel2.SetActive(true);
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

            if (secondInput == true)
                denominator.text = userInput;
            else
                numerator.text = userInput;
        }
    }
}
