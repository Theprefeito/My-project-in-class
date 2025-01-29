using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public float moveSpeed = 5f; //Velocidade normal de movimento
    public float dashSpeed = 15f; //Velocidade do Dash
    public float dashDuration = 0.2f; //Duração do dash
    private float dashTime = 0f; //Contador de tempo do dash
    private bool isDashing = false; //Verifica se o jogador está em dash

    private Rigidbody2D rb; //referência ao RigidBody2d do jogador

    void Start()
    {
            rb = GetComponent<Rigidbody2D>(); //Obtém a referência do RigidBody do Jogador
    }

    
    void Update()
    {
            //Detecta se o botão do dash foi pressionado
            if(Input.GetKeyDown(KeyCode.X))    
            {
              Dash();
            }

            //Verifica se está em dash e conta o tempo
            if(isDashing)
            {
                dashTime -= Time.deltaTime;
                if(dashTime <= 0f)
                {
                    isDashing = false; //Termina o dash
                }
            }
    }

    void FixedUpdate()
    {
            if(isDashing)
            {
                //Jogador se movimentará mais rápido quando tiver em dash
                rb.velocity = new Vector2(0f, 0f); //Garante que o dash substitua qualquer outro movimento de entrada
            }

            else
            {
                //Movimento normal
                float horizontalInput = Input.GetAxisRaw("Horizontal"); //Obtém a entrada do teclado
                float verticalInput = Input.GetAxisRaw("Vertical");

                //Movimento normal (não está em dash)
                rb.velocity = new Vector2(horizontalInput * moveSpeed, verticalInput * moveSpeed);
            }
    }


    void Dash()
    {
         // Inicia o dash, aplica a velocidade no eixo X ou Y dependendo da direção
        isDashing = true;
        dashTime = dashDuration;

        //Pega a direção de movimento atual
        Vector2 dashDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //Se a direção não for zero, normaliza para garantir que a velocidade do dash seja consistente
        if(dashDirection != Vector2.zero)
        {
            dashDirection.Normalize();
        }

        //Aplica o dash na direção nomralizada com a velocidade do dash
        rb.velocity = dashDirection * dashSpeed;
    }
}
