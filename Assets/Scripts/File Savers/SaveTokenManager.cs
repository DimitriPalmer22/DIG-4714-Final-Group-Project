using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class SaveTokenManager
{
    /// <summary>
    /// The maximum number of saves that can be stored
    /// </summary>
    public const int MAX_SAVES = 3;
    
    public const string FILE_NAME = "Player Data.json";

    [SerializeField] public List<SaveToken> saves = new();

    public void AddSaveToken(SaveToken token)
    {
        // Add the token to the list
        saves.Add(token);
        
        // If there are more than the maximum number of saves, remove the oldest ones
        if (saves.Count > MAX_SAVES)
            saves.RemoveRange(0, saves.Count - MAX_SAVES);
    }

    
    public static SaveTokenManager Load()
    {
        // get the file location
        var fileLocation = $"{Application.persistentDataPath}/{FILE_NAME}";

        // check if the file exists
        if (!System.IO.File.Exists(fileLocation))
        {
            Debug.Log($"No save file found @ {fileLocation}. Creating a new one.");
            return new SaveTokenManager();
        }

        // read the file using the JSON utility
        var json = System.IO.File.ReadAllText(fileLocation);
        var saveTokenManager = JsonUtility.FromJson<SaveTokenManager>(json);

        // Debug.Log($"Successfully loaded save token manager! {saveTokenManager.tokens.Count} saves found.");

        // If there are more than the maximum number of saves, remove the oldest ones
        if (saveTokenManager.saves.Count > MAX_SAVES)
            saveTokenManager.saves.RemoveRange(0, saveTokenManager.saves.Count - MAX_SAVES);
        
        return saveTokenManager;
    }
    
}