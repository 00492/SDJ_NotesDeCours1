using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;
using UnityEngine;

public class ActionExample : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _firstText;
    [SerializeField] private TextMeshProUGUI _secondText;

    private Action _autoChangeText;
    private Action<string> _manualChangeText;


    private void Start()
    {
        _autoChangeText += ChangeFirstText;
        _autoChangeText += ChangeSecondText;

        _manualChangeText += ManuallyChangeText;
    }

    private void ChangeFirstText()
    {
        _firstText.SetText("Text has been changed");
    }

    private void ChangeSecondText()
    {
        _secondText.SetText("This text changed too !");
    }

    private void ManuallyChangeText(string newText)
    {
        _firstText.SetText(newText);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {

            _autoChangeText?.Invoke();
        }
        else if(Input.GetKeyDown(KeyCode.M))
        {
            _manualChangeText?.Invoke("PaulEtJulieVontALaPlage");
        }
    }

    private void OnDestroy()
    {
        _autoChangeText -= ChangeFirstText;
        _autoChangeText -= ChangeSecondText;

        _manualChangeText -= ManuallyChangeText;
    }
}
