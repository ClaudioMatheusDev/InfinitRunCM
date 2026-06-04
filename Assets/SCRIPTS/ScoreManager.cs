using UnityEngine;
using TMPro; // Obrigatório para usar TextMeshPro

public class ScoreManager : MonoBehaviour
{
    [Header("Componentes de UI")]
    public TextMeshProUGUI textoPontosAtuais;
    public TextMeshProUGUI textoRecordeGameOver; // Texto que ficará dentro do painel de Game Over

    private float pontos = 0f;
    private bool jogoRodando = true;

    void Start()
    {
        pontos = 0f;
        jogoRodando = true;
    }

    void Update()
    {
        // Só conta pontos se o jogo não estiver pausado e o jogador estiver vivo
        if (Time.timeScale > 0f && jogoRodando)
        {
            // Ganha 10 pontos por segundo (multiplicado pela velocidade para dar um bônus por correr mais rápido!)
            pontos += Time.deltaTime * MainMenu.velocidadeAtual;
            
            // Atualiza o texto na HUD do jogo
            if (textoPontosAtuais != null)
            {
                textoPontosAtuais.text = "PONTOS: " + Mathf.FloorToInt(pontos).ToString();
            }
        }
    }

    // Essa função DEVE ser chamada pelo script do Player no momento exato em que ele bate no obstáculo
    public void SalvarEExibirRecorde()
        {
            // 1. Descobre quantos pontos o jogador fez
            // (Nota: Se a sua variável de pontos no script tiver outro nome, mude 'pontos' para o nome dela!)
            int pontuacaoFinal = Mathf.FloorToInt(pontos); 

            // 2. Busca o recorde antigo salvo no computador
            int recordeSalvo = PlayerPrefs.GetInt("HighScore", 0);

            // 3. Se a pontuação de agora for maior, atualiza o recorde
            if (pontuacaoFinal > recordeSalvo)
            {
                PlayerPrefs.SetInt("HighScore", pontuacaoFinal);
                PlayerPrefs.Save();
                recordeSalvo = pontuacaoFinal;
            }

            // 4. Mostra o recorde no texto da Tela de Game Over
            // (Certifique-se de criar essa variável 'textoRecordeGameOver' no topo do seu script se não tiver!)
            if (textoRecordeGameOver != null)
            {
                textoRecordeGameOver.text = "RECORDE: " + recordeSalvo;
            }
        }
}