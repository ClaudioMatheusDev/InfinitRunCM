using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem; 
using System.Collections;

public class PlayerJump : MonoBehaviour
{
    [Header("Configurações de Pulo")]
    public float jumpForce = 10f; 
    
    private bool isGrounded = true; 
    private Rigidbody2D rb;
    private Animator anim;
    private bool estaMorto = false;

    [Header("Configurações de Game Over")]
    public GameObject telaGameOver; 

    [Header("Configurações de Áudio (BeepBox)")]
    private AudioSource audioSource; // O aparelho de som do Player
    public AudioClip somPulo;        // O arquivo do som de pulo
    public AudioClip somDerrota;     // O arquivo do som de derrota

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
        // Pega o componente de áudio que vamos colocar no Player
        
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
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

        // 🎵 Toca o som do pulo!
        if (audioSource != null && somPulo != null)
        {
            audioSource.PlayOneShot(somPulo);
        }
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
        if (collision.CompareTag("Obstacle") && !estaMorto)
        {
            StartCoroutine(SequenciaMorte());
        }
    }

    IEnumerator SequenciaMorte()
    {
        estaMorto = true;
        Debug.Log("Game Over! Iniciando animação de derrota.");


        ScoreManager scoreManager = Object.FindFirstObjectByType<ScoreManager>();
        if (scoreManager != null)
        {
            scoreManager.SalvarEExibirRecorde();
        }

    Debug.Log("Game Over! Iniciando animação de derrota.");
        
        // 🎵 Toca o som de derrota!
        if (audioSource != null && somDerrota != null)
        {
            audioSource.PlayOneShot(somDerrota);
        }

        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast"); 
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        anim.SetTrigger("isDead"); 

        yield return new WaitForSeconds(1f);

        if (telaGameOver != null)
        {
            telaGameOver.SetActive(true);
        }
        Time.timeScale = 0f;
    }

    public void ReiniciarJogo()
    {
        MainMenu.devePularMenu = true;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void VoltarAoMenuInicial()
    {
        MainMenu.devePularMenu = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}