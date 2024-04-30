using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUIManager : MonoBehaviour
{
    [SerializeField] private GameObject scoreUIPrefab;

    private List<ScoreUI> _scoreUIs = new();

    public void Activate(SaveTokenManager saveTokenManager)
    {
        // Create a new score UI for each Save Token 
        for (var i = 0; i < saveTokenManager.saves.Count; i++)
        {
            var scoreUI = Instantiate(scoreUIPrefab, transform).GetComponent<ScoreUI>();
            scoreUI.AssignSaveToken(saveTokenManager.saves[i], i);
            _scoreUIs.Add(scoreUI);
        }

        // Set the position of the score UIs
        SetScoreUIPositions();
    }

    private void SetScoreUIPositions()
    {
        // Set the highest scoring UI image color to gold
        _scoreUIs.OrderByDescending(scoreUI => scoreUI.SaveToken.score).First().image.color = Color.yellow;

        // The first element will be on the left
        _scoreUIs[0].gameObject.transform.localPosition = new Vector3(-512, 128, 0);

        // If there is only 1 element, return
        if (_scoreUIs.Count <= 1)
            return;

        // The second element will be in the middle
        _scoreUIs[1].gameObject.transform.localPosition = new Vector3(0, 128, 0);

        // If there are only 2 elements, return
        if (_scoreUIs.Count <= 2)
            return;

        // the 3rd element will be on the right
        _scoreUIs[2].gameObject.transform.localPosition = new Vector3(512, 128, 0);
    }
}