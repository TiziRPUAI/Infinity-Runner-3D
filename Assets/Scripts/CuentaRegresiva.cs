using System.Collections;
using UnityEngine;
using TMPro;

public class CuentaRegresiva : MonoBehaviour
{
    public TextMeshProUGUI TextCuenta;
    public Movement MovimientoJugador;
    public LevelGenerator GeneradorNivel;
    public GameObject PanelInicio;
    public GameObject HighScoreText; // AGREGA ESTO

    private void Start()
    {
        MovimientoJugador.enabled = false;
        GeneradorNivel.enabled = false;
        PanelInicio.SetActive(true);
        HighScoreText.SetActive(false); // AGREGA ESTO
        StartCoroutine(Contar());
    }

    private IEnumerator Contar()
    {
        yield return StartCoroutine(MostrarNumero("3"));
        yield return StartCoroutine(MostrarNumero("2"));
        yield return StartCoroutine(MostrarNumero("1"));
        yield return StartCoroutine(MostrarNumero("¡YA!"));

        yield return StartCoroutine(DesvanecePanel());

        MovimientoJugador.enabled = true;
        GeneradorNivel.enabled = true;
        PanelInicio.SetActive(false);
        HighScoreText.SetActive(true); // AGREGA ESTO
    }

    private IEnumerator MostrarNumero(string numero)
    {
        TextCuenta.gameObject.SetActive(true);
        TextCuenta.text = numero;

        float tiempo = 0f;
        Vector3 tamañoGrande = new Vector3(1.5f, 1.5f, 1f);
        Vector3 tamañoNormal = new Vector3(1f, 1f, 1f);
        TextCuenta.transform.localScale = tamañoGrande;

        while (tiempo < 0.8f)
        {
            tiempo += Time.deltaTime;
            TextCuenta.transform.localScale = Vector3.Lerp(tamañoGrande, tamañoNormal, tiempo / 0.8f);
            yield return null;
        }

        yield return new WaitForSeconds(0.2f);
    }

    private IEnumerator DesvanecePanel()
    {
        TextCuenta.gameObject.SetActive(false);

        UnityEngine.UI.Image imagenPanel = PanelInicio.GetComponent<UnityEngine.UI.Image>();
        Color color = imagenPanel.color;
        float tiempo = 0f;

        while (tiempo < 0.5f)
        {
            tiempo += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, tiempo / 0.5f);
            imagenPanel.color = color;
            yield return null;
        }
    }
}