using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnswerManager13 : AnswerManagerBase
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
        int number1sum = Random.Range(10, 51);
        int number2sum = Random.Range(0, number1sum);
        number1 = number1sum / 10;
        number2 = number1sum % 10;
        number3 = number2sum / 10;
        number4 = number2sum % 10;

        tens1.text = number1.ToString();
        ones1.text = number2.ToString();
        tens2.text = number3.ToString();
        ones2.text = number4.ToString();

        int result = number1sum - number2sum;
        answerString = result.ToString();
    }
}