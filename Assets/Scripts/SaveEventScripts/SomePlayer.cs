using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomePlayer : MonoBehaviour
{
    public float _currentHP;
    public float _maxHP = 100f;

    private void Awake()
    {
        _currentHP = _maxHP;
    }

    private void OnEnable()
    {
        EventManager.Instance.RegisterEvent(EventID.SaveGame, OnSaveGame);
        EventManager.Instance.RegisterEvent(EventID.LoadGame, OnLoadGame);
    }

    private void OnLoadGame(object obj)
    {
        PlayerData data = SaveManager.Instance._playerData;

        _currentHP = data._hp;
        UpdateHP();

        transform.position = data._pos;
    }

    private void OnSaveGame(object obj)
    {
        // Much more wows!
        PlayerData data = SaveManager.Instance._playerData;

        data._hp = _currentHP;
        data._pos = transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            _currentHP -= 10f;
            UpdateHP();
        }
    }

    public void UpdateHP()
    {
        float hpPercent = 0f;
        if (_maxHP > 0)
        {
            hpPercent = _currentHP / _maxHP;
        }
        EventManager.Instance.DispatchEvent(EventID.UpdateHP, hpPercent);
    }

    private void OnDisable()
    {
        if (EventManager.Instance != null)
        {
            EventManager.Instance.UnregisterEvent(EventID.SaveGame, OnSaveGame);
            EventManager.Instance.UnregisterEvent(EventID.LoadGame, OnLoadGame);
        }
    }
}
