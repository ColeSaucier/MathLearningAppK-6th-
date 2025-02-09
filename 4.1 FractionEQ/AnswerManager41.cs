using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnswerManager41 : AnswerManagerBase
{
    public bool secondInput = false;

    public FractionHandler fractionHandler;
    public int copiedNumerator;
    public int copiedDenominator;

    public TextMeshProUGUI numerator;
    public TextMeshProUGUI denominator;
    
    public TextMeshProUGUI keyboardNumerator;
    public TextMeshProUGUI keyboardDenominator;

    public override void checkStringInput()
    {
        copiedNumerator = fractionHandler.numeratorAns;
        copiedDenominator = fractionHandler.denominatorAns;
        if (mobileVersion)
        {
            if (secondInput == true)
            {
                if (keyboardNumerator.text == copiedNumerator.ToString() && keyboardDenominator.text == copiedDenominator.ToString()) 
                {
                    SceneComplete = true;
                    sceneCompleteScript.SceneComplete = true;
                    Button.image.color = Color.green;
                }
                else
                {
                    Handheld.Vibrate();
                    Color32 shiftColor = new Color32(210, 0, 0, 50);
                    base.DisplayColoredImage(shiftColor, 0.2f);
                    secondInput = false;
                    keyboardNumerator.text = "";
                    keyboardDenominator.text = "";
                }
            }
            else
            {
                secondInput = true;
            }
        }
        else
        {
            if (secondInput == true)
            {
                if (numerator.text == copiedNumerator.ToString() && denominator.text == copiedDenominator.ToString()) 
                {
                    SceneComplete = true;
                    sceneCompleteScript.SceneComplete = true;
                    Button.image.color = Color.green;
                    activateInput();
                }
                else
                {
                    activateInput();
                    Color32 shiftColor = new Color32(210, 0, 0, 50);
                    base.DisplayColoredImage(shiftColor, 0.2f);
                }
            }
            else
            {
                secondInput = true;
                userInput = "";
            }
        }
    }
}
