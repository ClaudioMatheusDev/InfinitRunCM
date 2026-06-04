using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    [Header("Configurações")]
    public float speed = 5f;       // Deve ser a mesma velocidade do FloorScroller
    public float destroyX = -15f;  // Ponto na esquerda fora da tela onde o obstáculo some

    void Update()
    {
        // Move para a esquerda
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        // Se saiu da tela, se destrói para não pesar no jogo
        if (transform.position.x <= destroyX)
        {
            Destroy(gameObject);
        }
    }
}