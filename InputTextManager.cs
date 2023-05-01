using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputTextManager : Singleton<InputTextManager>
{
    public TMP_InputField inputField;

    public string GetTag()
    {
        string tag = inputField.text;
        inputField.text = "";
        return tag;
    }
}
