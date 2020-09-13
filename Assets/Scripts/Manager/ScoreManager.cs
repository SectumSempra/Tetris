using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public int linesPerLevel = 1;
    public Text lineText;
    public Text levetText;
    public Text scoreText;
    public bool isLevelUp = false;

    private int score = 0;
    private int lines;
    public int level = 1;

    private const int minLines = 1;
    private const int maxLines = 4;



    // Start is called before the first frame update
    void Start()
    {
        ResetScore();
    }

    public void ScoreLines(int n)
    {
        isLevelUp = false;

        n = Mathf.Clamp(n, minLines, maxLines);
        switch (n)
        {
            case 1: score += 40 * level; break;
            case 2: score += 100 * level; break;
            case 3: score += 300 * level; break;
            case 4: score += 1200 * level; break;
            default: break;
        }

        lines -= n;
        if (lines <= 0)
            LevelUp();

        UpdateUIText();
    }

    public void ResetScore()
    {
        level = 1;
        lines = linesPerLevel * level;
        UpdateUIText();
    }


    public void UpdateUIText()
    {
        if (lineText)
            lineText.text = lines.ToString();
        if (levetText)
            levetText.text = level.ToString();
        if (scoreText)
            scoreText.text = score.ToString().PadLeft(5, '0');
    }

    public void LevelUp()
    {
        level++;
        lines = linesPerLevel * level;
        isLevelUp = true;
    }


}
