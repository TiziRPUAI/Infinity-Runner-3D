using System.Collections;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public int ScoreInt;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI HighScoreText;

    private int HighScore;

    private void Start()
    {
        // Carga el highscore guardado
        HighScore = PlayerPrefs.GetInt("HighScore", 0);
        ActualizarHighScore();
    }

    public void ScorePlusOne()
    {
        ScoreInt++;

        // Si supera el record lo guarda
        if (ScoreInt > HighScore)
        {
            HighScore = ScoreInt;
            PlayerPrefs.SetInt("HighScore", HighScore);
            PlayerPrefs.Save();
        }

        StopAllCoroutines();
        StartCoroutine(AnimacionScore());
    }

    private void ActualizarHighScore()
    {
        if (HighScoreText != null)
            HighScoreText.text = "Best: " + HighScore.ToString();
    }

    private IEnumerator AnimacionScore()
    {
        Vector3 tamañoNormal = new Vector3(1f, 1f, 1f);
        Vector3 tamañoGrande = new Vector3(1.5f, 1.5f, 1f);
        Color colorNormal = Color.white;
        Color colorPunch = new Color(1f, 0.9f, 0.2f);

        float tiempo = 0f;
        while (tiempo < 0.12f)
        {
            tiempo += Time.deltaTime;
            float t = tiempo / 0.12f;
            ScoreText.transform.localScale = Vector3.Lerp(tamañoNormal, tamañoGrande, t);
            ScoreText.color = Color.Lerp(colorNormal, colorPunch, t);
            yield return null;
        }

        tiempo = 0f;
        while (tiempo < 0.15f)
        {
            tiempo += Time.deltaTime;
            float t = tiempo / 0.15f;
            ScoreText.transform.localScale = Vector3.Lerp(tamañoGrande, tamañoNormal, t);
            ScoreText.color = Color.Lerp(colorPunch, colorNormal, t);
            yield return null;
        }

        ScoreText.transform.localScale = tamañoNormal;
        ScoreText.color = colorNormal;
    }

    private void Update()
    {
        ScoreText.text = ScoreInt.ToString();
        ActualizarHighScore();
    }
}   