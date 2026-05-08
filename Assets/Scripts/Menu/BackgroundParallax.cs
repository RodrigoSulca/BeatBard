using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    public Vector2 speed = new Vector2(10f, 0f);        // velocidad de movimiento
    public Vector2 maxOffset = new Vector2(100f, 0f);   // cuánto se moverá antes de reiniciar

    private RectTransform rt;
    private Vector2 initialPosition;

    void Start()
    {
        rt = GetComponent<RectTransform>();
        initialPosition = rt.anchoredPosition;
    }

    void Update()
    {
        rt.anchoredPosition += speed * Time.deltaTime;

        // Loop en X
        if (Mathf.Abs(rt.anchoredPosition.x - initialPosition.x) >= maxOffset.x)
        {
            rt.anchoredPosition = new Vector2(initialPosition.x, rt.anchoredPosition.y);
        }

        // Loop en Y (si usas desplazamiento vertical)
        if (Mathf.Abs(rt.anchoredPosition.y - initialPosition.y) >= maxOffset.y)
        {
            rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, initialPosition.y);
        }
    }
}
