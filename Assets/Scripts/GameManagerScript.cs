using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The game manager script will ONLY be assigned to the Global Game Object.
/// The global game object will be set to not destroy on load.
/// </summary>
public class GameManagerScript : MonoBehaviour
{

    private const KeyCode PauseKey = KeyCode.Escape;
    
    #region Fields

    // The player's score
    private int _score;

    // The player object
    private GameObject _player;
    
    // A boolean to check if the game is paused
    private bool _isPaused;
    
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
    
    /// <summary>
    /// A boolean to check if the game is paused
    /// </summary>
    public bool IsPaused => _isPaused;

    #endregion
    
    #region Unity Methods

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
        // Receive input from the player
        UpdateInput();
    }
    
    #endregion Unity Methods
    
    #region Methods

    private void UpdateInput()
    {
        // If the player presses the pause key
        if (Input.GetKeyDown(PauseKey))
        {
            // If the game is pause, unpause the game
            if (_isPaused)
                UnpauseGame();

            // If the game is not paused, pause the game
            else
                PauseGame();
        }
    }

    private void PauseGame()
    {
        Debug.Log("Pausing the game!");
        
        // Pause the game
        Time.timeScale = 0;
        
        // Set the paused boolean to true
        _isPaused = true;
        
        // TODO: Show the pause menu
    }

    private void UnpauseGame()
    {
        Debug.Log("Unpausing the game!");
        
        // Unpause the game
        Time.timeScale = 1;
        
        // Set the paused boolean to false
        _isPaused = false;
        
        // TODO: Hide the pause menu
    }

    #endregion Methods

}