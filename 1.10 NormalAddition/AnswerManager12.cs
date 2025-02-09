using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnswerManager12 : AnswerManagerBase
{
    public static int number1 = 5;
    public static int number2 = 7;
    public static int number3 = 3;
    public static int number4 = 4;

    public TextMeshProUGUI tens1;
    public TextMeshProUGUI ones1;
    public TextMeshProUGUI tens2;
    public TextMeshProUGUI ones2;


    // Start is called before the first frame update
    void Start()
    {
        number1 = Random.Range(0, 5);
        number2 = Random.Range(0, 10);
        number3 = Random.Range(0, 5);
        number4 = Random.Range(0, 10);
        
        tens1.text = number1.ToString();
        ones1.text = number2.ToString();
        tens2.text = number3.ToString();
        ones2.text = number4.ToString();
        int result = 10 * (number1 + number3) + number2 + number4;
        answerString = result.ToString();
    }
}