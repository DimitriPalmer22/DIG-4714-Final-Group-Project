using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The game manager script will ONLY be assigned to the Global Game Object.
/// The global game object will be set to not destroy on load.
/// </summary>
public class GameManagerScript : MonoBehaviour
{
    #region Fields

    // The player's score
    private int _score;

    // The player object
    private GameObject _player;
    
    #endregion

    #region Properties

    /// <summary>
    /// The instance allows us to access the game manager from any script.
    /// </summary>
    public static GameManagerScript Instance { get; private set; }

    /// <summary>
    /// The player's score
    /// </summary>
    public int Score => _score;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // Prevent the game manager from being destroyed on load so we can access it from any scene
        DontDestroyOnLoad(gameObject);

        // Set the instance of the game manager
        if (Instance == null)
            Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
    }
    
}