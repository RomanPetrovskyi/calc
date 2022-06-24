using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;


public class Calc : MonoBehaviour
{
    [SerializeField] private Text resultText;   // ������ �� ������ ���������� ����������
    [SerializeField] private string buttonText; // ������ �� ���������� ������

    private string tempStr;
    private string lastSymbol;

    // ������� ��������� ������� ������ � ������� ��� � ���������� 
    public void buttonNumber_OnClick()
    {
        lastSymbol = "/";
        if (resultText.text == "0" || resultText.text == "������ �����" || resultText.text == "�������������") resultText.text = "";
        tempStr = resultText.text;
        
        if (tempStr.Length > 0)
        {
            lastSymbol = tempStr[tempStr.Length - 1].ToString(); // �������� ��������� �������� ������ ��� ������� �� ������
        }

        if (buttonText == "0" || buttonText == "1" || buttonText == "2" || buttonText == "3" || buttonText == "4" ||
            buttonText == "5" || buttonText == "6" || buttonText == "7" || buttonText == "8" || buttonText == "9")
        {
            resultText.text += buttonText;
        }
        else if(buttonText == "+" || buttonText == "-" || buttonText == "*" || buttonText == "/")
        {
            if (lastSymbol != "+" && lastSymbol != "-" && lastSymbol != "*" && lastSymbol != "/")
            {   
                resultText.text += buttonText;
            }
            if (resultText.text == "" && buttonText == "-") resultText.text = "-";
            else if(resultText.text == "") resultText.text = "0";
        }
        else if(buttonText == "(")
        {
            if(resultText.text == "" || lastSymbol == "+" || lastSymbol == "-" || lastSymbol == "*" || lastSymbol == "/")
            {
                resultText.text += buttonText;
            }
        }
        else if(buttonText == ")")
        {
            int brackets = 0;
            // ���������� ���-�� �������� � �������� ������
            for (int i = 0; i < resultText.text.Length; i++)
            {
                if (resultText.text[i].ToString() == "(") brackets++;
                if (resultText.text[i].ToString() == ")") brackets--;
            }

            if (brackets > 0 && lastSymbol != "(" && lastSymbol != "+" && lastSymbol != "-" && lastSymbol != "*" && lastSymbol != "/")
            {
                resultText.text += buttonText;
            }
        }
        else if(buttonText == ".")
        {
            bool flagPoint1 = false;
            bool flagPoint2 = false;

            if (resultText.text == "") { resultText.text += "0."; }
            else
            {
                for (int i = 0; i < resultText.text.Length; i++)
                {
                    string s = resultText.text[i].ToString();
                    if (s == ".") { flagPoint1 = true; flagPoint2 = true; }
                    if (s == "+" || s == "-" || s == "*" || s == "/") flagPoint1 = false;
                    if (s == "0" || s == "1" || s == "2" || s == "3" || s == "4" || s == "5" || s == "6" || s == "7" || s == "8" || s == "9") flagPoint2 = false;
                }

               

                if (!flagPoint1 && !flagPoint2 && lastSymbol != "(" && lastSymbol != ")" && lastSymbol != "+" && lastSymbol != "-" && lastSymbol != "*" && lastSymbol != "/")
                {
                    resultText.text += buttonText;
                }
            }
        }
    }

    // ������� ��������� ������� ������ ��� ������ ������ ������ ����������
    public void buttonClear_OnClick()
    {
        resultText.text = "0";
        lastSymbol = "/";
    }

    // ������� ��������� ������� ������ ��������� ���������� ����������
    public void buttonEquals_OnClick()
    {
        // ��������� ���������� ���������� � ������ ����� ������������� ��������� �� ���� "2+4+-." � �.�.
        try
        {
            DataTable dt = new DataTable();
            string res = (dt.Compute(resultText.text, "")).ToString();
            res = res.Replace(",", ".");    // �������� ��� ������� ��� ������� � ��������� � �������� ������� �� ����� ��� ����������� ����������
            resultText.text = res;
            if (res == "�������������") res = "0";
        }
        catch
        {
            resultText.text = "0";
        }
        
    }

    // ������� ��������� ������� ������ �������� ��������� ����������� �������� � ������ ����������
    public void buttomBack_OnClick()
    { 
        if (resultText.text == "0" || resultText.text == "������ �����" || resultText.text == "�������������") resultText.text = "0";
        else
        {
            if(resultText.text.Length > 1) resultText.text = resultText.text.Substring(0, resultText.text.Length - 1);
            else resultText.text = "0";
        }
    }
}
