using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnswerManager14 : AnswerManagerBase
{
    public TextMeshProUGUI multiplication;
    public int product; // Public variable to store the product

    // Start is called before the first frame update
    void Start()
    {
        // Generate two random numbers between 1 and 10
        int num1 = Random.Range(1, 11);
        int num2 = Random.Range(1, 11);

        // Calculate the product
        product = num1 * num2;
        answerString = product.ToString();

        // Format the string and set it in the TextMeshProUGUI
        string multiplicationText = $"{num1} x {num2}";
        multiplication.text = multiplicationText;
    }
}