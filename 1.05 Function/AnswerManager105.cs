using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AnswerManager105 : MonoBehaviour
{
    public int rule;
    public int firstNumber;
    public int secondNumber;
    public int thirdNumber;
    public int fourthNumber;
    public int firstAnswer;
    public int secondAnswer;
    public int thirdAnswer;
    public int fourthAnswer;

    public TextMeshProUGUI ruleT;
    public TextMeshProUGUI firstNumberT;
    public TextMeshProUGUI secondNumberT;
    public TextMeshProUGUI thirdNumberT;
    public TextMeshProUGUI fourthNumberT;
    public TextMeshProUGUI firstAnswerT;
    public TextMeshProUGUI secondAnswerT;
    public TextMeshProUGUI thirdAnswerT;
    public TextMeshProUGUI fourthAnswerT;

    public int problemCurrent = 0;
    public string answerString;
    public SceneCompleteMenu sceneCompleteScript;

    // References needed for answer button
    public Button Button;
    public string userInput = "";
    private bool isInputActive = false;
    public TextMeshProUGUI inputText;
    public CanvasGroup popUpCanvasGroup;

    public GameObject Step1;
    public GameObject Step2;
    public GameObject Step3;
    public GameObject Step4;

    void Start()
    {
        rule = Random.Range(1, 11);
        ruleT.text = rule.ToString();
        int previousNumber = 0;

        for (int i = 0; i < 4; i++)
        {
            int randomNumber = previousNumber + Random.Range(1, 4);

            switch (i)
            {
                case 0:
                    firstNumber = randomNumber;
                    firstNumberT.text = firstNumber.ToString();
                    firstAnswer = firstNumber + rule;
                    break;
                case 1:
                    secondNumber = randomNumber;
                    secondNumberT.text = secondNumber.ToString();
                    secondAnswer = secondNumber + rule;
                    break;
                case 2:
                    thirdNumber = randomNumber;
                    thirdNumberT.text = thirdNumber.ToString();
                    thirdAnswer = thirdNumber + rule;
                    break;
                case 3:
                    fourthNumber = randomNumber;
                    fourthNumberT.text = fourthNumber.ToString();
                    fourthAnswer = fourthNumber + rule;
                    break;
            }
            previousNumber = randomNumber;
        }
        determineAnswer();
    }

    public void determineAnswer()
    {
        if (problemCurrent == 0)
        {
            answerString = firstAnswer.ToString();
        }
        else if (problemCurrent == 1)
        {
            answerString = secondAnswer.ToString();
            Step1.SetActive(false);
            Step2.SetActive(true);
        }
        else if (problemCurrent == 2)
        {
            answerString = thirdAnswer.ToString();
            Step2.SetActive(false);
            Step3.SetActive(true);
        }
        else if (problemCurrent == 3)
        {
            answerString = fourthAnswer.ToString();
            Step3.SetActive(false);
            Step4.SetActive(true);
        }
    }

    public void fillAnswer()
    {
        if (problemCurrent == 0)
            firstAnswerT.text = answerString;
        else if (problemCurrent == 1)
            secondAnswerT.text = answerString;
        else if (problemCurrent == 2)
            thirdAnswerT.text = answerString;
        else if (problemCurrent == 3)
            fourthAnswerT.text = answerString;
    }

    public void Update()
    {
        if (isInputActive)
        {
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
            fillAnswer();
            problemCurrent++;
            determineAnswer();
            if (problemCurrent == 4)
            {
                Step4.SetActive(false);
                sceneCompleteScript.SceneComplete = true;
                Button.image.color = Color.green;
            }
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