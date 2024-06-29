using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManagerControl : MonoBehaviour
{
    public int puntaje;
    public Text _texto;
    public Text timerText;               // Texto que muestra el tiempo restante
    public GameObject endGamePanel;      // Panel que se muestra al final del juego
    public Text endGameScoreText;        // Texto en el panel final que muestra el puntaje
    public InputField playerNameInput;   // Campo de entrada para el nombre del jugador
    public Button submitButton;          // Botón para enviar los datos

    private float gameTime = 180;       // Tiempo del juego en segundos (3 minutos)
    private bool isGameOver = false;
    private bool scoreSubmitted = false; // Estado del envío del puntaje

    void Start()
    {
        endGamePanel.SetActive(false);  // Asegúrate de que el panel esté desactivado al inicio
        UpdateUI();
        StartCoroutine(GameTimer());
        submitButton.onClick.AddListener(SubmitScore);
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
        isGameOver = true;
        endGamePanel.SetActive(true);
        endGameScoreText.text = "Final Score: " + puntaje.ToString();
    }

    public void SubmitScore()
    {
        if (!scoreSubmitted) // Verificar si el puntaje no ha sido enviado aún
        {
            string playerName = playerNameInput.text;
            StartCoroutine(SendScoreToDatabase(playerName, puntaje));
            scoreSubmitted = true; // Marcar como enviado después de iniciar la corutina
        }
    }

    private IEnumerator SendScoreToDatabase(string playerName, int score)
    {
        string jsonData = JsonUtility.ToJson(new ScoreData(playerName, score));
        UnityWebRequest www = new UnityWebRequest("http://localhost/update_score_vj1.php", "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Score submitted successfully.");
            RestartGame(); // Reiniciar el juego después de enviar el puntaje
        }
        else
        {
            Debug.LogError("Error submitting score: " + www.error);
        }
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reinicia la escena actual
    }

    [System.Serializable]
    private class ScoreData
    {
        public string username;
        public int score;

        public ScoreData(string username, int score)
        {
            this.username = username;
            this.score = score;
        }
    }
}

