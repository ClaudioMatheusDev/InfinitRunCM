using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem; 

public class PlayerJump : MonoBehaviour
{
    [Header("Configurações de Pulo")]
    public float jumpForce = 10f; 
    
    private bool isGrounded = true; 
    private Rigidbody2D rb;
    private Animator anim;

    [Header("Configurações de Game Over")]
    // Espaço para arrastar a sua "TelaGameOver" no Inspector
    public GameObject telaGameOver; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Verifica o teclado no Sistema Novo e se o personagem está no chão
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            Pular();
        }
    }

    void Pular()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        isGrounded = false; 
        
        // Avisa o Animator que o pulo começou (Verdadeiro)
        anim.SetBool("isJumping", true); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; 
            
            // Avisa o Animator que o pulo acabou (Falso) e volta a correr
            anim.SetBool("isJumping", false); 
        }
    }

    // Detecta colisões com objetos que estão com "Is Trigger" marcado
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Se o que nos tocou tiver a Tag Obstacle
        if (collision.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over! Batemos no obstáculo.");
            
            // 1. Ativa o painel de Game Over na tela
            if (telaGameOver != null)
            {
                telaGameOver.SetActive(true);
            }

            // 2. Trava o tempo do jogo (tudo para de se mover!)
            Time.timeScale = 0f;
        }
    }

    // Essa função será chamada pelo botão "JOGAR DE NOVO"
    public void ReiniciarJogo()
    {
        // 1. Volta o tempo do jogo ao normal
        Time.timeScale = 1f;

        // 2. Recarrega a fase do zero
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}