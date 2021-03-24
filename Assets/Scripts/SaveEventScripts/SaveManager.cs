using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private const string RESSOURCE_NAME = "SaveManager";
    private const string SAVE_FILE = "SaveGame";

    public PlayerData _playerData;

    private static SaveManager _instance;
    public static SaveManager Instance
    {
        get
        {
            if(_instance == null)
            {
                GameObject prefab = Resources.Load<GameObject>(RESSOURCE_NAME);
                Instantiate(prefab);
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if(_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Using JSON Text File
    public void Save()
    {
        // Applique modifs avant écriture du fichier / Mise a jours du Data
        EventManager.Instance.DispatchEvent(EventID.SaveGame);

        // Écriture du fichier
        string json = JsonUtility.ToJson(_playerData);
        File.WriteAllText(SAVE_FILE, json);
    }

    public void Load()
    {
        // Lecture du fichier et réassignation du Data
        string json = File.ReadAllText(SAVE_FILE);
        _playerData = JsonUtility.FromJson<PlayerData>(json);

        // Applique modifs nouveau Data
        EventManager.Instance.DispatchEvent(EventID.LoadGame);
    }


    // Using PlayerPrefs /////////////////////
    public void SavePP()
    {
        // Possible to save: float, int, string
        PlayerPrefs.SetFloat("hp", _playerData._hp);
    }

    public void LoadPP()
    {
        if(PlayerPrefs.HasKey("hp"))
        {
            _playerData._hp = PlayerPrefs.GetFloat("hp");
        }
    }
}
