using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem; 
using System.Collections; // Obrigatório para usar Coroutines!

public class PlayerJump : MonoBehaviour
{
    [Header("Configurações de Pulo")]
    public float jumpForce = 10f; 
    
    private bool isGrounded = true; 
    private Rigidbody2D rb;
    private Animator anim;
    private bool estaMorto = false; // Evita bugs de bater duas vezes

    [Header("Configurações de Game Over")]
    public GameObject telaGameOver; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Só pula se não estiver morto
        if (!estaMorto && Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            Pular();
        }
    }

    void Pular()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        isGrounded = false; 
        anim.SetBool("isJumping", true); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; 
            anim.SetBool("isJumping", false); 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Se bater no obstáculo e ainda não estiver processando a morte
        if (collision.CompareTag("Obstacle") && !estaMorto)
        {
            StartCoroutine(SequenciaMorte());
        }
    }

    // Processo que espera a animação acontecer ANTES de congelar a tela
    IEnumerator SequenciaMorte()
    {
        estaMorto = true;
        Debug.Log("Game Over! Iniciando animação de derrota.");
        
        // EM VEZ DE DESLIGAR O COLISOR, vamos mudar a Layer do Player!
        // Isso faz ele ignorar os obstáculos, mas CONTINUAR colidindo com o Ground.
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast"); 
        // Nota: Se você não tiver configurado matriz de colisão, mudar para Ignore Raycast 
        // ou simplesmente desativar o script do Spawner de obstáculos resolve.

        // Garante que ele pare de correr para frente no cenário
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);

        // Avisa o Animator para tocar o estado de derrota
        anim.SetTrigger("isDead"); 

        // Espera 1 segundo para o jogador ver o tombo na tela
        yield return new WaitForSeconds(1f);

        // Ativa o painel e congela o tempo de verdade
        if (telaGameOver != null)
        {
            telaGameOver.SetActive(true);
        }
        Time.timeScale = 0f;
    }
    public void ReiniciarJogo()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}