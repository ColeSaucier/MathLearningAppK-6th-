using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnswerManager15 : MonoBehaviour
{
    public bool SceneComplete;
    public SceneCompleteMenu sceneCompleteScript;
    public bool isInputActive = false;
    public bool minuteInputBool = false;

    public int copiedHour;
    public int copiedMinute;
    public string copiedMinuteString;

    public TextMeshProUGUI hour;
    public TextMeshProUGUI minutes;
    public CanvasGroup popUpCanvasGroup;
    public ClockGenerator clockGenerator;
    public Button Button;
    public GameObject square1;
    public GameObject square2;

    public string userInput;

    public void activateInput()
    {
        copiedHour = clockGenerator.randomHour;
        copiedMinute = clockGenerator.randomMinute * 5;
        if (copiedMinute < 10)
            copiedMinuteString = $"0{copiedMinute}";
        else
            copiedMinuteString = copiedMinute.ToString();
        isInputActive = true;
        minuteInputBool = false;
        userInput = "";

        // Show the pop-up canvas by setting its alpha to 1 (fully opaque)
        popUpCanvasGroup.alpha = 1f;
        popUpCanvasGroup.interactable = true; // Enable interactions with the pop-up canvas
        Button.interactable = false;
        square1.SetActive(true);
        square2.SetActive(true);


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
                if (minuteInputBool == true)
                {
                    if (hour.text == copiedHour.ToString() && minutes.text == copiedMinuteString) 
                    {
                        SceneComplete = true;
                        sceneCompleteScript.SceneComplete = true;
                        Button.image.color = Color.green;
                    }

                    isInputActive = false;
                    // Hide the pop-up canvas by setting its alpha to 0 (fully transparent)
                    popUpCanvasGroup.alpha = 0f;
                    popUpCanvasGroup.interactable = false; // Disable interactions with the pop-up canvas
                    square1.SetActive(false);
                    square2.SetActive(false);
                    Button.interactable = true;
                    // Code to deactivate mobile keyboard here (if mobile device)
                }

                else
                {
                    minuteInputBool = true;
                    userInput = "";
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

            if (minuteInputBool == true)
                minutes.text = userInput;
            else
                hour.text = userInput;
        }
    }
}
