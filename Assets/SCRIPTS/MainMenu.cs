using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [Header("Painéis de UI")]
    public GameObject menuInicial;
    public GameObject hudPontuacao;

    [Header("Gerenciadores do Jogo")]
    public MonoBehaviour obstacleSpawner;

    // Essa variável estática "lembra" se o jogo veio de um reinício
    public static bool devePularMenu = false;

    void Start()
    {
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

        if (obstacleSpawner != null)
        {
            obstacleSpawner.enabled = true;
        }

        Time.timeScale = 1f;
    }
}