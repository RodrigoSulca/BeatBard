using UnityEngine;
using DG.Tweening;
using Unity.Mathematics;
using System;
using Random = UnityEngine.Random;
public class EnemyController : MonoBehaviour
{
    public enum Type { Red, Green, Blue }
    public Type type;
    public float speed;
    [field: Header("BlueMove")]
    public float initMoveDistance;
    public float initSpeed;
    public float finalSpeed;
    [field: Header("GreenMove")]
    public Vector3 targetOffset;
    public Vector3 diagonalTargetPosition;
    private GameObject endPoint;
    private Rigidbody rb;
    void Start()
    {
        endPoint = GameObject.FindWithTag("EndPoint");
        rb = GetComponent<Rigidbody>();
        MoveDown();
    }

    void MoveDown()
    {
        switch (type)
        {
            case Type.Red:
                RedMove();
                break;
            case Type.Blue:
                BlueMove();
                break;
            case Type.Green:
                GreenMove();
                break;
        }

    }


    private void RedMove()
    {
        rb.DOMoveZ(endPoint.transform.position.z, initSpeed).SetEase(Ease.Linear).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
    private void BlueMove()
    {
        rb.DOMoveZ(transform.position.z + initMoveDistance, initSpeed).SetEase(Ease.Linear).OnComplete(() =>
        {
            rb.DOMoveZ(endPoint.transform.position.z, finalSpeed).SetEase(Ease.Linear).OnComplete(() =>
            {
                Destroy(gameObject);
            });
        });
    }

    private void GreenMove()
    {
        // Movemos al enemigo verde por una distancia determinada
        rb.DOMoveZ(transform.position.z + initMoveDistance, initSpeed).SetEase(Ease.Linear).OnComplete(() =>
        {
            // Capturamos la posicion del enemigo verde para hacer comparaciones
            float posX = transform.position.x;
            float posY = transform.position.y;
            float posZ = transform.position.z;
            float[] lanePosX = { 0f, -5.49f, -4.272779f, -3.01715f };
            float epsilon = 0.01f; // Ajustador para problemas de precision
            Debug.Log("posX:" + posX);
            if (Mathf.Abs(posX - lanePosX[1]) < epsilon) // -4.03722f
            { // Si el enemigo verde esta en el carril 1, lo movemos al carril 2
                posX = lanePosX[2];
                Debug.Log("new posX:" + posX);
            }
            else if (Mathf.Abs(posX - lanePosX[2]) < epsilon)
            { // Si el enemigo verde esta en el carril 2, lo movemos al carril 1 o 3 de manera aleatoria
                int random = Random.Range(0, 2);
                if(random == 0)
                    posX = lanePosX[1];
                else
                    posX = lanePosX[3];
                Debug.Log("new posX:" + posX);
            }
            else if (Mathf.Abs(posX - lanePosX[3]) < epsilon)
            {  // Si el enemigo verde esta en el carril 3, lo movemos al carril 2
                posX = lanePosX[2];
                Debug.Log("new posX:" + posX);
            }
            // Movemos Z una distancia aproximada de 1.2364245 hacia adelante
            posZ -= 1.23f;

            // Creamos el Vector destino a mover
            Vector3 diagonalTarget = new Vector3(posX, posY, posZ);

            rb.DOMove(diagonalTarget, finalSpeed).SetEase(Ease.Linear).OnComplete(() =>
            {
                rb.DOMoveZ(endPoint.transform.position.z, initSpeed).SetEase(Ease.Linear).OnComplete(() =>
                {
                    Destroy(gameObject);
                });
            });
        });
    }
    
}
