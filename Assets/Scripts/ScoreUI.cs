using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public Image image;
    public TMP_Text text;

    private SaveToken _saveToken;
    private int _saveIndex;

    public void AssignSaveToken(SaveToken saveToken, int saveIndex)
    {
        _saveToken = saveToken;
        _saveIndex = saveIndex;

        UpdateText();
    }

    private void Update()
    {
    }

    private void UpdateText()
    {
        var timeSpan = TimeSpan.FromSeconds(_saveToken.timeSurvived);
        
        text.text =
            $"Save #{_saveIndex}\n" +
            $"\n" +
            $"Score: {_saveToken.score}\n" +
            $"Level: {_saveToken.level}\n" +
            $"Time Survived: {timeSpan}\n";
    }
}