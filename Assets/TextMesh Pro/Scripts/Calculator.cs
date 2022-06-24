using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Calculator : MonoBehaviour
{
    public string buttonString;
    public Text resultText;

    public void OnButtonClick()
    {
        resultText.text += buttonString;
    }
}
