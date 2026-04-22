using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject Tile1;
    public GameObject Tile2;
    public GameObject StartTile;

    private float Index = 0;
    private float VelocidadActual = 4f;
    private float VelocidadMaxima = 12f;
    private float AumentoVelocidad = 0.3f;
    private float TiempoJuego = 0f;

    // Los tiles se generan en Awake (antes que Start)
    private void Awake()
    {
        GameObject StartPlane1 = Instantiate(StartTile, transform);
        StartPlane1.transform.position = new Vector3(7, 0, 0);
        GameObject StartPlane2 = Instantiate(StartTile, transform);
        StartPlane2.transform.position = new Vector3(-1, 0, 0);
        GameObject StartPlane3 = Instantiate(StartTile, transform);
        StartPlane3.transform.position = new Vector3(-9, 0, 0);
        GameObject StartPlane4 = Instantiate(StartTile, transform);
        StartPlane4.transform.position = new Vector3(-17, 0, 0);
        GameObject StartPlane5 = Instantiate(StartTile, transform);
        StartPlane5.transform.position = new Vector3(-25, 0, 0);
    }

    private void Start() { }

    private void Update()
    {
        TiempoJuego += Time.deltaTime;
        VelocidadActual = Mathf.Min(
            4f + TiempoJuego * AumentoVelocidad,
            VelocidadMaxima
        );

        gameObject.transform.position += new Vector3(VelocidadActual * Time.deltaTime, 0, 0);

        if (transform.position.x >= Index)
        {
            int RandomInt1 = Random.Range(0, 2);
            if (RandomInt1 == 1)
            {
                GameObject TempTile1 = Instantiate(Tile1, transform);
                TempTile1.transform.position = new Vector3(-16, 0, 0);
            }
            else
            {
                GameObject TempTile1 = Instantiate(Tile2, transform);
                TempTile1.transform.position = new Vector3(-16, 0, 0);
            }

            int RandomInt2 = Random.Range(0, 2);
            if (RandomInt2 == 1)
            {
                GameObject TempTile2 = Instantiate(Tile1, transform);
                TempTile2.transform.position = new Vector3(-24, 0, 0);
            }
            else
            {
                GameObject TempTile2 = Instantiate(Tile2, transform);
                TempTile2.transform.position = new Vector3(-24, 0, 0);
            }

            Index = Index + 15.95f;
        }
    }

    public float GetVelocidad()
    {
        return VelocidadActual;
    }
}