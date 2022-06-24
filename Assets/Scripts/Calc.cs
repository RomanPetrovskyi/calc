using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;


public class Calc : MonoBehaviour
{
    [SerializeField] private Text resultText;   // Ссылка на строку результата вычислений
    [SerializeField] private string buttonText; // Ссылка на функционал кнопки

    private string tempStr;
    private string lastSymbol;

    // Функция обработки нажатия кнопок с цифрами или с операндами 
    public void buttonNumber_OnClick()
    {
        lastSymbol = "/";
        if (resultText.text == "0" || resultText.text == "Ошибка ввода" || resultText.text == "бесконечность") resultText.text = "";
        tempStr = resultText.text;
        
        if (tempStr.Length > 0)
        {
            lastSymbol = tempStr[tempStr.Length - 1].ToString(); // Получаем последний введённый символ при нажатии на кнопку
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
            // Определяем кол-во открытых и закрытых скобок
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

    // Функция обработки нажатия кнопки для очитки строки вывода результата
    public void buttonClear_OnClick()
    {
        resultText.text = "0";
        lastSymbol = "/";
    }

    // Функция обработки нажатия кнопки получения результата вычислений
    public void buttonEquals_OnClick()
    {
        // Обработка исключения необходима в случае ввода неправильного выражения по типу "2+4+-." и т.п.
        try
        {
            DataTable dt = new DataTable();
            string res = (dt.Compute(resultText.text, "")).ToString();
            res = res.Replace(",", ".");    // Заменяем при наличии все запятые в выражении с дробными числами на точки для корректного вычисления
            resultText.text = res;
            if (res == "бесконечность") res = "0";
        }
        catch
        {
            resultText.text = "0";
        }
        
    }

    // Функция обработки нажатия кнопки удаления последних добавленных символов в строке вычисления
    public void buttomBack_OnClick()
    { 
        if (resultText.text == "0" || resultText.text == "Ошибка ввода" || resultText.text == "бесконечность") resultText.text = "0";
        else
        {
            if(resultText.text.Length > 1) resultText.text = resultText.text.Substring(0, resultText.text.Length - 1);
            else resultText.text = "0";
        }
    }
}
