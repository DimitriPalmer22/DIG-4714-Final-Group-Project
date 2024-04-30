using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalLevelScript : MonoBehaviour
{
    private const KeyCode PauseKey = KeyCode.Escape;

    #region Fields

    // The player object
    private GameObject _player;

    [Header("UI")] [SerializeField] private TMP_Text _scoreText;

    [SerializeField] private GameObject _pausedMenuParent;

    [SerializeField] private GameObject _gameOverMenuParent;

    [SerializeField] private XP_Bar xpBar;

    /// <summary>
    /// How long the player has survived
    /// </summary>
    private float _timeSurvived;

    #endregion

    #region Properties

    /// <summary>
    /// The instance allows us to access the level script from any script.
    /// </summary>
    public static GlobalLevelScript Instance { get; private set; }

    /// <summary>
    /// The player's score
    /// </summary>
    public int Score { get; private set; }

    /// <summary>
    /// A boolean to check if the game is paused
    /// </summary>
    public bool IsPaused { get; private set; }

    // A boolean to check if the player is dead
    public bool IsPlayerDead { get; private set; }

    /// <summary>
    /// The player's XP bar 
    /// </summary>
    public XP_Bar XpBar => xpBar;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        // Set the instance of the level script
        if (Instance == null)
            Instance = this;

        // hide the paused menu
        _pausedMenuParent.SetActive(false);

        // hide the game over menu
        _gameOverMenuParent.SetActive(false);
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

        // Update the time survived
        if (!IsPaused && !IsPlayerDead)
            _timeSurvived += Time.deltaTime;
    }

    #endregion

    #region Methods

    private void UpdateInput()
    {
        // If the player presses the pause key
        if (Input.GetKeyDown(PauseKey) && !IsPlayerDead)
        {
            // If the game is pause, unpause the game
            if (IsPaused)
                UnpauseGame();

            // If the game is not paused, pause the game
            else
                PauseGame();
        }

        // If the player is dead and they press the space key
        if (IsPlayerDead && Input.GetKeyDown(KeyCode.Space))
        {
            // Reload the scene
            SceneManager.LoadScene(0);
        }
    }

    private void PauseGame()
    {
        if (IsPaused)
            return;

        // Pause the game
        Time.timeScale = 0;

        // Set the paused boolean to true
        IsPaused = true;

        // Show the pause menu
        _pausedMenuParent.SetActive(true);
    }

    private void UnpauseGame()
    {
        if (!IsPaused)
            return;

        // Unpause the game
        Time.timeScale = 1;

        // Set the paused boolean to false
        IsPaused = false;

        // Hide the pause menu
        _pausedMenuParent.SetActive(false);
    }

    private void SetScore(int newValue)
    {
        // Set the score to the amount
        Score = newValue;

        // Update the score text
        UpdateScoreText();
    }

    public void ChangeScore(int changeBy)
    {
        // Change the score by the amount
        SetScore(Score + changeBy);
    }


    private void UpdateScoreText()
    {
        // If the score text is null, return
        if (_scoreText == null)
            return;

        // Update the score text        
        _scoreText.text = $"Score: {Score}";
    }

    public void OnPlayerDeath(Actor sender)
    {
        // Set the player dead boolean to true
        IsPlayerDead = true;

        // Show the game over menu
        _gameOverMenuParent.SetActive(true);
        
        // Save the player's score and time survived
        GameManagerScript.Instance.SaveGame();
    }
    
    public SaveToken GetSaveToken()
    {
        return new SaveToken(Score, _timeSurvived, XpBar.Level);
    }

    #endregion
}