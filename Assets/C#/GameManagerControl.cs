using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerControl : MonoBehaviour
{
    public int puntaje;
    public Text _texto;
    public Text timerText;       // Texto que muestra el tiempo restante
    private float gameTime = 30f;
    private bool isGameOver = false;
    public SubmitScoreShooter submit;

    void Start()
    {
        UpdateUI();
        StartCoroutine(GameTimer());
    }

    void UpdateUI()
    {
        timerText.text = "Time: " + Mathf.CeilToInt(gameTime).ToString();
        _texto.text = "Puntuacion: " + puntaje.ToString();
    }

    private IEnumerator GameTimer()
    {
        while (gameTime > 0 && !isGameOver)
        {
            gameTime -= Time.deltaTime;
            UpdateUI();
            yield return null;
        }
        EndGame();
    }

    public void UpdatePuntaje()
    {
        if (!isGameOver)
        {
            puntaje += 20;
            UpdateUI();
        }
    }
    private void EndGame()
    {
        Time.timeScale = 0;
        isGameOver = true;
        submit.gameObject.SetActive(true);
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reinicia la escena actual
    }
}