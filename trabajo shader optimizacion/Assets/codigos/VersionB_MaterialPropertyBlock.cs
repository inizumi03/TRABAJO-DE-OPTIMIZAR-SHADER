using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VersionB_MaterialPropertyBlock : MonoBehaviour
{
    [SerializeField] GameObject prefabEnemigo;
    [SerializeField] Material materialCompartido;
    [SerializeField, Min(1)] int cantidad = 5000;
    [SerializeField, Min(1)] int columnas = 100;
    [SerializeField] float separacion = 1.5f;
    static readonly int ColorEstado = Shader.PropertyToID("_BaseColor");

    void Start()
    {
        var bloque = new MaterialPropertyBlock();
        for (int i = 0; i < cantidad; i++)
        {
            Vector3 p = new Vector3((i % columnas) * separacion, 0f, (i / columnas) * separacion);
            var go = Instantiate(prefabEnemigo, p, Quaternion.identity, transform);
            var r = go.GetComponent<Renderer>();
            if (!r) { Debug.LogError("El prefab necesita Renderer."); enabled = false; return; }
            r.sharedMaterial = materialCompartido;
            bloque.Clear();
            bloque.SetColor(ColorEstado, i % 5 == 0 ? Color.red : Color.green);
            r.SetPropertyBlock(bloque);
        }
    }
}
