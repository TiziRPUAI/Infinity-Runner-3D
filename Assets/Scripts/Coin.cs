using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Score ScoreText;
    public GameObject ParticulaPrefab;
    public AudioClip SonidoManzana;

    private static AudioSource audioSource;

    private void Start()
    {
        ScoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Score>();
        if (audioSource == null)
            audioSource = Camera.main.GetComponent<AudioSource>();
    }

    private void Update()
    {
        gameObject.transform.Rotate(0, 0, 0.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        ScoreText.ScorePlusOne();

        // Particula
        GameObject particula = Instantiate(ParticulaPrefab, transform.position, Quaternion.identity);
        Destroy(particula, 1f);

        // Sonido manzana
        AudioSource.PlayClipAtPoint(SonidoManzana, transform.position);

        Destroy(gameObject);
    }
}