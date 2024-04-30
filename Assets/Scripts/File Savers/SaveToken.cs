using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveToken
{
    [SerializeField] public int score;
    [SerializeField] public float timeSurvived;
    [SerializeField] public int level;

    public SaveToken(int score, float timeSurvived, int level)
    {
        this.score = score;
        this.timeSurvived = timeSurvived;
        this.level = level;
    }
}