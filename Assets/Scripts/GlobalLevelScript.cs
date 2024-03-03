using TMPro;
using UnityEngine;

public class GlobalLevelScript : MonoBehaviour
{
    private const KeyCode PauseKey = KeyCode.Escape;

    #region Fields

    // The player's score
    private int _score;

    // The player object
    private GameObject _player;
    
    /// <summary>
    /// A text object to display the player's score
    /// </summary>
    [SerializeField] private TMP_Text _scoreText;
    
    // A boolean to check if the game is paused
    private bool _isPaused;

    #endregion

    #region Properties

    /// <summary>
    /// The instance allows us to access the level script from any script.
    /// </summary>
    public static GlobalLevelScript Instance { get; private set; }

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

    private void Awake()
    {
        // Set the instance of the level script
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        // Set the score to 0
        SetScore(0);
    }
    
    // Update is called once per frame
    private void Update()
    {
        // Receive input from the player
        UpdateInput();
    }


    #endregion

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

    private void SetScore(int newValue)
    {
        // Set the score to the amount
        _score = newValue;
        
        // Update the score text
        UpdateScoreText();
    }

    public void ChangeScore(int changeBy)
    {
        // Change the score by the amount
        SetScore(_score + changeBy);
    }
    

    private void UpdateScoreText()
    {
        Debug.Log($"Score text: {_scoreText}");
        
        // If the score text is null, return
        if (_scoreText == null)
            return;
        
        // Update the score text        
        _scoreText.text = $"Score: {_score}";
    }
    
    #endregion
    
}
