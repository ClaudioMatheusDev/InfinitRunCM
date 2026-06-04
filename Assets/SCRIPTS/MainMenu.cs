using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [Header("Painéis de UI")]
    public GameObject menuInicial;
    public GameObject hudPontuacao;

    [Header("Gerenciadores do Jogo")]
    public MonoBehaviour obstacleSpawner;

    [Header("Configurações de Dificuldade")]
    public float velocidadeInicial = 5f;
    public float aceleracaoPorSegundo = 0.1f;
    public float velocidadeMaxima = 15f;

    // Essa variável estática "lembra" se o jogo veio de um reinício
    public static bool devePularMenu = false;
    public static float velocidadeAtual;

    void Start()
    {
        // Garante que a velocidade resete para o valor inicial SEMPRE que a cena começa
        velocidadeAtual = velocidadeInicial;

        // Se a variável for VERDADEIRA, significa que o jogador clicou em Reiniciar!
        if (devePularMenu)
        {
            // Começa direto na corrida, sem mostrar o menu
            menuInicial.SetActive(false);
            hudPontuacao.SetActive(true);

            if (obstacleSpawner != null)
            {
                obstacleSpawner.enabled = true;
            }

            Time.timeScale = 1f;
        }
        else
        {
            // Comportamento normal de quando abre o jogo pela primeira vez
            menuInicial.SetActive(true);
            hudPontuacao.SetActive(false);

            if (obstacleSpawner != null)
            {
                obstacleSpawner.enabled = false;
            }

            Time.timeScale = 0f;
        }
    }

    public void IniciarJogo()
    {
        menuInicial.SetActive(false);
        hudPontuacao.SetActive(true);

        // Garante que a velocidade comece certinha ao clicar no botão também
        velocidadeAtual = velocidadeInicial;

        if (obstacleSpawner != null)
        {
            obstacleSpawner.enabled = true;
        }

        Time.timeScale = 1f;
    }

    void Update()
    {
        // Só acelera o jogo se o menu inicial não estiver na tela e o jogo não estiver pausado
        if (Time.timeScale > 0f && !menuInicial.activeSelf)
        {
            if (velocidadeAtual < velocidadeMaxima)
            {
                // Aumenta a velocidade aos poucos baseada no tempo real
                velocidadeAtual += aceleracaoPorSegundo * Time.deltaTime;
            }
        }
    }
}