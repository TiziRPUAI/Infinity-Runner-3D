using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Collision : MonoBehaviour
{
    public GameObject PanelGameOver;
    public TextMeshProUGUI TextScoreFinal;
    public Score ScoreScript;

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

        // Oculta el score del juego
        ScoreScript.ScoreText.gameObject.SetActive(false);
    }

    public void Reiniciar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main");
    }
}