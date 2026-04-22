using System.Collections;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public int ScoreInt;
    public TextMeshProUGUI ScoreText;

    // Colores
    private Color colorNormal = Color.white;
    private Color colorPunch = new Color(1f, 0.9f, 0.2f); // amarillo dorado

    private Vector3 tamañoNormal = new Vector3(1f, 1f, 1f);
    private Vector3 tamañoGrande = new Vector3(1.5f, 1.5f, 1f);

    public void ScorePlusOne()
    {
        ScoreInt++;
        StopAllCoroutines();
        StartCoroutine(AnimacionScore());
    }

    private IEnumerator AnimacionScore()
    {
        // --- CRECE Y SE PONE DORADO ---
        float tiempo = 0f;
        while (tiempo < 0.12f)
        {
            tiempo += Time.deltaTime;
            float t = tiempo / 0.12f;
            ScoreText.transform.localScale = Vector3.Lerp(tamañoNormal, tamañoGrande, t);
            ScoreText.color = Color.Lerp(colorNormal, colorPunch, t);
            yield return null;
        }

        // --- VUELVE A NORMAL ---
        tiempo = 0f;
        while (tiempo < 0.15f)
        {
            tiempo += Time.deltaTime;
            float t = tiempo / 0.15f;
            ScoreText.transform.localScale = Vector3.Lerp(tamañoGrande, tamañoNormal, t);
            ScoreText.color = Color.Lerp(colorPunch, colorNormal, t);
            yield return null;
        }

        // Asegura valores exactos al final
        ScoreText.transform.localScale = tamañoNormal;
        ScoreText.color = colorNormal;
    }

    private void Update()
    {
        ScoreText.text = ScoreInt.ToString();
    }
}