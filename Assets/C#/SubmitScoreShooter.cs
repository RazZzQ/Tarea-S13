using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class SubmitScoreShooter : MonoBehaviour
{
    public InputField nameInput;
    public int score;
    public string postURL = "http://localhost/submit_shooter_score.php";
    public GameManagerControl gameManager;

    public void Submit()
    {
        StartCoroutine(PostScore(nameInput.text, score));
    }

    IEnumerator PostScore(string playerName, int score)
    {
        ShooterScore shooterScore = new ShooterScore();
        shooterScore.name = playerName;
        score =  gameManager.puntaje;
        shooterScore.score = score;

        string json = JsonUtility.ToJson(shooterScore);

        using (UnityWebRequest www = new UnityWebRequest(postURL, "POST"))
        {
            byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(json);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
            }
            else
            {
                Debug.Log("Score submitted successfully");
            }
        }
    }
}
[System.Serializable]
public class ShooterScore
{
    public string name;
    public int score;
}
