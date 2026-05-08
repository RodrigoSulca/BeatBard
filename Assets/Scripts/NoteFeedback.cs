using UnityEngine;

public class NoteFeedback : MonoBehaviour
{
    public Material successMaterial;
    public Material failMaterial;
    public float duration = 0.3f;

    private Material originalMaterial;
    private MeshRenderer meshRenderer;

    void Start()
    {

    }

    public void ShowFeedback(bool isCorrect)
    {
       
    }

    System.Collections.IEnumerator FlashMaterial(Material mat)
    {
        
        yield return new WaitForSeconds(duration);
        
    }
}
