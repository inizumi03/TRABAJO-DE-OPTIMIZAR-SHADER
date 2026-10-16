using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VersionPropia : MonoBehaviour
{
    [SerializeField] Mesh mallaEnemigo;
    [SerializeField] Material materialEnemigo;
    [SerializeField, Min(1)] int cantidad = 5000;
    [SerializeField, Min(1)] int columnas = 100;
    [SerializeField] float separacion = 1.5f;

    const int Lote = 500;

    Matrix4x4[] enemigos;

    void Start()
    {
        if (!SystemInfo.supportsInstancing || !mallaEnemigo || !materialEnemigo)
        {
           
            enabled = false;
            return;
        }

        materialEnemigo.enableInstancing = true;
        materialEnemigo.SetFloat("_Semilla", Random.Range(0, 100000));
        enemigos = new Matrix4x4[cantidad];

        for (int i = 0; i < cantidad; i++)
        {
            Vector3 posicion = new Vector3(
                (i % columnas) * separacion,
                0,
                (i / columnas) * separacion
            );

            enemigos[i] = Matrix4x4.TRS(
                posicion,
                Quaternion.identity,
                Vector3.one
            );
        }
    }

    void Update()
    {
        RenderParams parametros = new RenderParams(materialEnemigo);

        for (int i = 0; i < enemigos.Length; i += Lote)
        {
            Graphics.RenderMeshInstanced(
                parametros,
                mallaEnemigo,
                0,
                enemigos,
                Mathf.Min(Lote, enemigos.Length - i),
                i
            );
        }
    }
}
