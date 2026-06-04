using UnityEngine;
using UnityEngine.InputSystem; // Trazendo o Novo Sistema de Input de volta!

public class PlayerJump : MonoBehaviour
{
    [Header("Configurações de Pulo")]
    public float jumpForce = 10f; 
    
    private bool isGrounded = true; 
    private Rigidbody2D rb;
    private Animator anim;

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
}