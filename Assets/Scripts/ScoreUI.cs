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
    
    public SaveToken SaveToken => _saveToken;

    public void AssignSaveToken(SaveToken saveToken, int saveIndex)
    {
        _saveToken = saveToken;
        _saveIndex = saveIndex;

        UpdateText();
    }

    private void UpdateText()
    {
        var timeSpan = TimeSpan.FromSeconds(_saveToken.timeSurvived);

        var saveIndexString =
            (_saveIndex + 1 != SaveTokenManager.MAX_SAVES)
                ? $"Save #{_saveIndex + 1}\n"
                : $"This Save\n";

        text.text =
            saveIndexString +
            $"Score: {_saveToken.score}\n" +
            $"Level: {_saveToken.level}\n" +
            $"Time:  {timeSpan:hh\\:mm\\:ss}\n";
    }
}