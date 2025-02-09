using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnswerManager07 : AnswerManagerBase
{
    public BabySubtractionCircleGenerator babySubtractionCircles;

    public override void checkStringInput()
    {
        answerString = babySubtractionCircles.resultObjects.ToString();

        // Call the base class's checkStringInput method to handle the rest of the logic
        base.checkStringInput();
    }
}