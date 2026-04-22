using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Collision : MonoBehaviour
{
    public GameObject PanelGameOver;
    public TextMeshProUGUI TextScoreFinal;
    public TextMeshProUGUI TextHighScoreFinal;
    public Score ScoreScript;
    public AudioClip SonidoChoque;

    private void Start()
    {
        PanelGameOver.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            MostrarGameOver();
        }
    }

    private void MostrarGameOver()
    {
        Time.timeScale = 0f;
        PanelGameOver.SetActive(true);

        TextScoreFinal.text = "Score: " + ScoreScript.ScoreInt.ToString();

        // Muestra el highscore
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        TextHighScoreFinal.text = "Best: " + highScore.ToString();

        // Si es record nuevo lo marca
        if (ScoreScript.ScoreInt >= highScore)
            TextHighScoreFinal.text = "★ Nuevo Record: " + highScore.ToString();

        // Oculta el Best durante game over
        ScoreScript.HighScoreText.gameObject.SetActive(false);
        ScoreScript.ScoreText.gameObject.SetActive(false);
        AudioSource.PlayClipAtPoint(SonidoChoque, Camera.main.transform.position);
    }

    public void Reiniciar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main");
    }
}