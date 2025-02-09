using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnswerManager16 : AnswerManagerBase
{
    public int answer; // Public variable to store the answer
    public CircleColumnManipulation CircleColumnManipulation;

    // Start is called before the first frame update
    void Start()
    {
        // Generate two random numbers between 1 and 10
        int num1 = Random.Range(1, 11);
        int num2 = Random.Range(1, 11);
    }

    public override void checkStringInput()
    {
        // Get the answer
        answer = CircleColumnManipulation.answer;
        answerString = answer.ToString();

        // Call the base class's checkStringInput method to handle the rest of the logic
        base.checkStringInput();
    }
}