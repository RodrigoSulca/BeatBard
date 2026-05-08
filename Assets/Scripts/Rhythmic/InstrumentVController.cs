using UnityEngine;

public class InstrumentVController : MonoBehaviour
{
    public bool active;
    public Material activeMaterial;
    private Material defaultMaterial;
    private Renderer mRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mRenderer = GetComponent<Renderer>();
        defaultMaterial = mRenderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            mRenderer.material = activeMaterial;
        }
        else
        {
            mRenderer.material = defaultMaterial;
        }
    }
}
