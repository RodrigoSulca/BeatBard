
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class FlatRombo : MonoBehaviour
{
    public float width = 1f;
    public float height = 0.5f;
    public float thickness = 0.1f;

    void Start()
    {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        Vector3[] vertices = {
            new Vector3(0, 0, height),
            new Vector3(width, 0, 0),
            new Vector3(0, 0, -height),
            new Vector3(-width, 0, 0),

            new Vector3(0, -thickness, height),
            new Vector3(width, -thickness, 0),
            new Vector3(0, -thickness, -height),
            new Vector3(-width, -thickness, 0),
        };

        int[] triangles = {
            0,1,2,
            0,2,3,

            4,6,5,
            4,7,6,

            0,4,1,
            1,4,5,

            1,5,2,
            2,5,6,

            2,6,3,
            3,6,7,

            3,7,0,
            0,7,4
        };

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
