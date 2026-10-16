using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VersionA_MaterialIndividual : MonoBehaviour
{
    [SerializeField] GameObject prefabEnemigo;
    [SerializeField] Material materialBase;
    [SerializeField, Min(1)] int cantidad = 5000;
    [SerializeField, Min(1)] int columnas = 100;
    [SerializeField] float separacion = 1.5f;
    static readonly int ColorEstado = Shader.PropertyToID("_BaseColor");
    readonly List<Material> materialesCreados = new List<Material>();

    void Start()
    {
        for (int i = 0; i < cantidad; i++)
        {
            Vector3 p = new Vector3((i % columnas) * separacion, 0f, (i / columnas) * separacion);
            var go = Instantiate(prefabEnemigo, p, Quaternion.identity, transform);
            var r = go.GetComponent<Renderer>();
            if (!r) { Debug.LogError("El prefab necesita Renderer."); enabled = false; return; }
            Material m = new Material(materialBase);
            materialesCreados.Add(m);
            r.sharedMaterial = m;
            m.SetColor(ColorEstado, i % 5 == 0 ? Color.red : Color.green);
        }
    }
    void OnDestroy()
    {
        foreach (Material m in materialesCreados)
            if (m) Destroy(m);
    }
}
