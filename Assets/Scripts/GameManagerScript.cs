using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// The game manager script will ONLY be assigned to the Global Game Object.
/// The global game object will be set to not destroy on load.
/// </summary>
public class GameManagerScript : MonoBehaviour
{
    #region Fields

    /// <summary>
    /// Used to create a player with a specific class.
    /// TODO: Unserialize this field and use it to create the player.
    /// </summary>
    [SerializeField] private CharacterClass _characterClass;

    #endregion

    #region Properties

    /// <summary>
    /// The instance allows us to access the game manager from any script.
    /// </summary>
    public static GameManagerScript Instance { get; private set; }

    public CharacterClass CharacterClass => _characterClass;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        // Prevent the game manager from being destroyed on load so we can access it from any scene
        DontDestroyOnLoad(gameObject);

        // Set the instance of the game manager
        if (Instance == null)
            Instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        // Load the level
        LoadLevel();

        // Create the character
        CreateCharacter();
    }

    #endregion Unity Methods

    #region Methods

    private void LoadLevel()
    {
        // TODO: Implement a level library and load the appropriate level
        // TODO: Load the level
    }

    private void CreateCharacter()
    {
        Debug.Log($"The character is a {_characterClass} class!");
    }

    public void SaveGame()
    {
        Debug.Log("Saving game...");


        Dictionary<IFileSaver, string> savers = new();

        // Try to create the file savers
        try
        {
            savers.Add(new JsonFileSaver(), "Player Data.json");
            savers.Add(new XmlFileSaver(), "Player Data.xml");
            savers.Add(new TxtFileSaver(), "Player Data.txt");
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            throw;
        }

        // Save to each type of file saver
        foreach (var saver in savers)
        {
            var fileSaver = saver.Key;
            string savePath = $"{Application.persistentDataPath}/{saver.Value}";

            Debug.Log($"Saving to {savePath}");

            using (fileSaver.OpenFileStream(savePath))
            {
                // Try to save
                try
                {
                    fileSaver.WriteText(TestSaveInfo.Instance);
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                    throw;
                }
            }
        }
    }

    #endregion Methods
}