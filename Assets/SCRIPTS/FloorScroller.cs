using UnityEngine;

public class FloorScroller : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    public float speed = 5f; // Velocidade com que o chão se move para a esquerda

    [Header("Configurações de Reposição")]
    public float endX;   // Ponto X onde o chão sai da tela (ex: -20)
    public float startX; // Ponto X onde o chão deve reaparecer (ex: 20)

    void Update()
    {
        // 1. Move o chão para a esquerda constantemente
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        // 2. Verifica se o chão passou do limite esquerdo (saiu da tela)
        if (transform.position.x <= endX)
        {
            // 3. Teletransporta o chão de volta para a direita
            Vector2 newPos = new Vector2(startX, transform.position.y);
            transform.position = newPos;
        }
    }
}