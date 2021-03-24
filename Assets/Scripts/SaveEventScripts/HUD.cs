using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    private Slider _hpSlider;

    [SerializeField] private Button _saveButton;
    [SerializeField] private Button _loadButton;


    private void Awake()
    {
        _hpSlider = GetComponentInChildren<Slider>();

        _saveButton.onClick.AddListener(Save);
        _loadButton.onClick.AddListener(Load);
    }

    private void OnEnable()
    {
        EventManager.Instance.RegisterEvent(EventID.UpdateHP, OnUpdateHP);
    }

    private void OnUpdateHP(object obj)
    {
        _hpSlider.value = (float)obj;
    }

    private void Save()
    {
        SaveManager.Instance.Save();
    }

    private void Load()
    {
        SaveManager.Instance.Load();
    }

    private void OnDisable()
    {
        if (EventManager.Instance)
        {
            EventManager.Instance.UnregisterEvent(EventID.UpdateHP, OnUpdateHP);
        }
    }
}
