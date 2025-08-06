using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TileBoard tileBoard;

    public CanvasGroup gameOver;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;

    private int score;  

    private void Start()
    {
        NewGame();
    }

    public void NewGame()
    {
        SetScore(0);
        bestScoreText.text = LoadHighScore().ToString();

        gameOver.alpha = 0f;
        gameOver.interactable = false;

        tileBoard.ClearBoard();
        tileBoard.CreateTile();
        tileBoard.CreateTile();
        tileBoard.enabled = true;
    }

    public void GameOver()
    {
        tileBoard.enabled = false;
        gameOver.interactable = true;

        StartCoroutine(Fade(gameOver, 1f, 1f));
    }

    private IEnumerator Fade(CanvasGroup canvasGroup, float to, float delay)
    {
        yield return new WaitForSeconds(delay);

        float elapsedTime = 0f;
        float duration = 1f;
        float from = canvasGroup.alpha;

        while (elapsedTime < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(from, to, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = to;
    }

    public void IncreaseScore(int points)
    {
        SetScore(score + points);
    }

    public void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString();

        SaveHighScore();

    }

    private void SaveHighScore()
    {
        int highScore = LoadHighScore();
        if (score > highScore)
        {
            PlayerPrefs.SetInt("Best Score", score);
        }
    }

    private int LoadHighScore()
    {
        return PlayerPrefs.GetInt("Best Score", 0);
    }
}
