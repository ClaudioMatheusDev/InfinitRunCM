using UnityEngine;
using TMPro; // LINHA OBRIGATÓRIA para conseguir mexer no TextMeshPro por código

public class ScoreManager : MonoBehaviour
{
    [Header("Componente de Interface")]
    public TextMeshProUGUI scoreText; // Arrastaremos o nosso texto para cá

    [Header("Configurações")]
    public float scoreMultiplier = 10f; // Quantos pontos o jogador ganha por segundo

    private float currentScore = 0f;

    void Update()
    {
        // 1. Aumenta a pontuação de acordo com o tempo que passou
        currentScore += Time.deltaTime * scoreMultiplier;

        // 2. Transforma o número quebrado em inteiro e atualiza o texto na tela
        int scoreInteiro = Mathf.FloorToInt(currentScore);
        scoreText.text = "Pontos: " + scoreInteiro.ToString();
    }
}