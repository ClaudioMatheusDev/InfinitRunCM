using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    [Header("Configurações")]
    public float destroyX = -15f;  // Ponto na esquerda fora da tela onde o obstáculo some

    void Update()
    {
        // Puxa a velocidade em tempo real que o MainMenu está calculando
        float velocidadeDoJogo = MainMenu.velocidadeAtual;

        // Move para a esquerda usando a velocidade dinâmica
        transform.Translate(Vector2.left * velocidadeDoJogo * Time.deltaTime);

        // Se saiu da tela, se destrói para não pesar no jogo
        if (transform.position.x <= destroyX)
        {
            Object.Destroy(gameObject);
        }
    }
}