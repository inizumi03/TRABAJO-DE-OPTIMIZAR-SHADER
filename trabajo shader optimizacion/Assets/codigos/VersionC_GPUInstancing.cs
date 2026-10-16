using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VersionC_GPUInstancing : MonoBehaviour
{
    [SerializeField] Mesh mallaEnemigo;
    [SerializeField] Material materialNormal;
    [SerializeField] Material materialDanado;
    [SerializeField, Min(1)] int cantidad = 5000;
    [SerializeField, Min(1)] int columnas = 100;
    [SerializeField] float separacion = 1.5f;
    const int Lote = 500;
    Matrix4x4[] normales;
    Matrix4x4[] danados;

    void Start()
    {
        if (!SystemInfo.supportsInstancing || !mallaEnemigo || !materialNormal || !materialDanado)
        { Debug.LogError("Verificar soporte de instancing y referencias."); enabled = false; return; }
        materialNormal.enableInstancing = true;
        materialDanado.enableInstancing = true;
        int nDanados = (cantidad + 4) / 5;
        normales = new Matrix4x4[cantidad - nDanados];
        danados = new Matrix4x4[nDanados];
        int n = 0, d = 0;
        for (int i = 0; i < cantidad; i++)
        {
            var p = new Vector3((i % columnas) * separacion, 0f, (i / columnas) * separacion);
            var matriz = Matrix4x4.TRS(p, Quaternion.identity, Vector3.one);
            if (i % 5 == 0) danados[d++] = matriz;
            else normales[n++] = matriz;
        }
    }
    void Update()
    {
        Dibujar(normales, materialNormal);
        Dibujar(danados, materialDanado);
    }
    void Dibujar(Matrix4x4[] matrices, Material material)
    {
        var parametros = new RenderParams(material);
        for (int inicio = 0; inicio < matrices.Length; inicio += Lote)
            Graphics.RenderMeshInstanced(parametros, mallaEnemigo, 0, matrices,
                Mathf.Min(Lote, matrices.Length - inicio), inicio);
    }
}
