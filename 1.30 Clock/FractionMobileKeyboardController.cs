using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FractionMobileKeyboardController : MonoBehaviour
{
    public TextMeshProUGUI hour;
    public TextMeshProUGUI minutes;
    public bool secondInputBool;
    public AnswerManager15 script;

    // Variables for blinking effect
    private float onDuration = 1f; // Duration for image to be visible
    private float offDuration = 0.5f; // Duration for image to be invisible
    private bool isImageVisible = false;
    private float nextActionTime = 0.0f;

    private bool blinkingEnabled_hour = true;
    private bool blinkingEnabled_minutes = true;
    public Image questionMark_hour;
    public Image questionMark_minutes;


    // Call this method in Update to handle blinking
    void Update()
    {
        if (Time.time > nextActionTime)
        { 
            // Toggle visibility bool
            isImageVisible = !isImageVisible;

            if (blinkingEnabled_hour) //Hour questionmark
            {
                questionMark_hour.enabled = isImageVisible;
            }
            if (blinkingEnabled_minutes) //First step questionmark
            {
                questionMark_minutes.enabled = isImageVisible;
            }

            // Set next action time
            nextActionTime = Time.time + (isImageVisible ? onDuration : offDuration);
        }
    }

    // Function to add a number to the text
    public void NumberInput(int number)
    {
        secondInputBool = script.minuteInputBool;
        Vibrator.Vibrate(50);

        if (secondInputBool == true)
        { 
            minutes.text += number.ToString();
            blinkingEnabled_minutes = false;
            questionMark_minutes.enabled = false;
        }
        else
        { 
            hour.text += number.ToString();
            blinkingEnabled_hour = false;
            questionMark_hour.enabled = false;
        }
    }

    // Function to delete the last character in the text
    public void DeleteInput()
    {
        secondInputBool = script.minuteInputBool;
        Vibrator.Vibrate(50);

        if (secondInputBool == true)
        {
            if (minutes.text.Length > 0)
            {
                minutes.text = minutes.text.Substring(0, minutes.text.Length - 1);
            }
        }
        else
        {
            if (hour.text.Length > 0)
            {
                hour.text = hour.text.Substring(0, hour.text.Length - 1);
            }
        }
    }
}