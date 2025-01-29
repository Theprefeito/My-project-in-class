using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControllerV2DashCD : MonoBehaviour
{
    public float forcapulo = 10f;
    public float moveSpeed = 5f;         // Velocidade normal de movimento
    public float dashSpeed = 15f;        // Velocidade do dash
    public float dashDuration = 0.2f;    // Duração do dash
    public float dashCooldown = 1f;      // Tempo de cooldown entre os dashes
    public int maxDashes = 1;            // Número máximo de dashes permitidos

    private int currentDashes = 0;       // Contador de dashes realizados
    private float dashTime = 0f;         // Contador de tempo do dash
    private float dashCooldownTime = 0f; // Contador de cooldown
    private bool isDashing = false;      // Verifica se o jogador está em dash
    private bool canDash = true;         // Verifica se o jogador pode realizar o próximo dash

    private SpriteRenderer spriteRenderer;

    public bool noChao = false;
    private Rigidbody2D rb;              // Referência ao Rigidbody2D do jogador
    private Vector2 moveDirection;       // Direção do movimento normal
    private Vector2 dashDirection;       // Direção do dash

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Obtém a referência do Rigidbody2D
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "chao")
        {
            noChao = true;
        }    
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "chao")
        {
            noChao = false;
        }    
    }

    void Update()
    {
        // Verifica cooldown para o dash
        if (!canDash)
        {
            dashCooldownTime -= Time.deltaTime;
            if (dashCooldownTime <= 0f)
            {
                canDash = true; // O jogador pode realizar outro dash
            }
        }

        // Detecta se o botão de dash (ex: "Fire1") foi pressionado
        if (Input.GetKeyDown(KeyCode.X) && canDash && currentDashes < maxDashes)
        {
            Dash();
        }

        // Verifica se está em dash e conta o tempo
        if (isDashing)
        {
            dashTime -= Time.deltaTime;
            if (dashTime <= 0f)
            {
                isDashing = false; // Termina o dash
                currentDashes++;   // Incrementa o número de dashes realizados
                canDash = false;   // Inicia o cooldown
                dashCooldownTime = dashCooldown; // Reseta o tempo de cooldown
            }
        }

        // Armazena a direção de movimento (horizontal e vertical)
        moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        UpdateSpriteDirection();

        if (Input.GetKeyDown(KeyCode.C) && noChao == true)
        {
            rb.AddForce(new Vector2(0, 1) * forcapulo, ForceMode2D.Impulse);
            Debug.Log("C");
        }

        //resetar por circunstância
        if (transform.position.y <= - 15)
        {
            //jogador caiu
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            // Se estiver em dash, aplica a velocidade de dash
            rb.velocity = dashDirection * dashSpeed;
        }
        else
        {
            // Movimento normal: usa a física para mover o jogador
            // Aplique o movimento na direção de movimento (usando força ou velocidade)
            rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y); // Mantém a física no eixo Y (como gravidade)
        }
    }

    void Dash()
    {
        // Inicia o dash: obtém a direção atual do movimento
        isDashing = true;
        dashTime = dashDuration;

        // Verifica a direção do movimento
        if (moveDirection != Vector2.zero)
        {
            dashDirection = moveDirection.normalized; // Normaliza a direção para garantir a velocidade consistente
        }
        else
        {
            // Caso não haja movimento (caso o jogador esteja parado), faz o dash para frente
            dashDirection = Vector2.right; // Dash para a direita (você pode alterar isso conforme necessário)
        }
    }

    void UpdateSpriteDirection()
    {
        // Se o movimento é para a direita, vira o sprite para a direita
        // Se o movimento é para a esquerda, vira o sprite para a esquerda
        if (moveDirection.x > 0)
        {
            spriteRenderer.flipX = false; // Sprite vira para a direita
        }
        else if (moveDirection.x < 0)
        {
            spriteRenderer.flipX = true; // Sprite vira para a esquerda
        }
    }
}

