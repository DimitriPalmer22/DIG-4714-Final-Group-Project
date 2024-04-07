using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TestSaveInfo
{
    [SerializeField] public string playerName;
    [SerializeField] public int money;
    [SerializeField] public int lives;
    [SerializeField] public int score;
    [SerializeField] public long timePlayed;
    [SerializeField] public List<int> levelsCompleted;
    [SerializeField] public List<int> levelsUnlocked;

    private static TestSaveInfo _instance;

    public static TestSaveInfo Instance
    {
        get
        {
            if (_instance == null)
                _instance = new TestSaveInfo();

            return _instance;
        }
    }

    public TestSaveInfo()
    {
        playerName = "Player";
        money = 10_000;
        lives = 3;
        score = 500_000;
        timePlayed = (long) (DateTime.Now - DateTime.UnixEpoch).TotalSeconds;
        levelsCompleted = new List<int>();
        levelsUnlocked = new List<int>();

        for (int i = 0; i < 10; i++)
            levelsUnlocked.Add(i);

        for (int i = 0; i < 8; i++)
            levelsCompleted.Add(i);
    }

    public override string ToString()
    {
        return $"Player Name: {playerName}\n" +
               $"Money: {money}\n" +
               $"Lives: {lives}\n" +
               $"Score: {score}\n" +
               $"Time Played: {timePlayed}\n" +
               $"Levels Completed: {string.Join(" ", levelsCompleted)}\n" +
               $"Levels Unlocked: {string.Join(" ", levelsUnlocked)}";
    }
}
