using System;
using System.Reflection; // Add this using directive to access PropertyInfo
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnswerManager10 : AnswerManagerBase
{
    public AnswerCalc AnswerCalc;

    public override void checkStringInput()
    {
        // Set the answer string using the specific logic for this class
        answerString = AnswerCalc.correctAnswer.ToString();

        // Call the base class's checkStringInput method to handle the rest of the logic
        base.checkStringInput();
    }
}