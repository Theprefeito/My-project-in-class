using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    public bool noChao = false;
    public float velocidade = 10.09f;                                                                                                                                          
   private Rigidbody2D _rigidbody2D;
   private SpriteRenderer spriteRenderer;
   public float forcapulo = 10f;

    void Start()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
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
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) )
        {
            gameObject.transform.position += new Vector3(-velocidade*Time.deltaTime, 0, 0);
            
            Debug.Log("LeftArrow"+Time.deltaTime);

            spriteRenderer.flipX = true;
        }

        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) )
                {
                    gameObject.transform.position += new Vector3(velocidade*Time.deltaTime, 0, 0);
                    
                    Debug.Log("RightArrow"+Time.deltaTime);

                    spriteRenderer.flipX = false;
                }

        if(Input.GetKeyDown(KeyCode.C) && noChao == true)
        {
            _rigidbody2D.AddForce(new Vector2(0, 1) * forcapulo, ForceMode2D.Impulse);
            Debug.Log("Space");
        }

         //resetar por circunstâmcia
        if(transform.position.y <= - 15)
        {
            //jogador caiu
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

      /*  else if(Input.GetKeyDown(KeyCode.R))
        {
            //jogador apertou R
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }*/
    }
}
