using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnswerManager001 : AnswerManagerBase
{
    public BabyAdditionCircleGenerator babyAdditionCircles;

    public override void checkStringInput()
    {
        // Set the answer string using the specific logic for this class
        answerString = NotRowObject.answer.ToString();

        // Call the base class's checkStringInput method to handle the rest of the logic
        base.checkStringInput();
    }
}