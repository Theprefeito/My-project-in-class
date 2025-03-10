using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerWanimations : MonoBehaviour
{
     public float velocidade = 10f;
  public float focaPulo = 10f;

    public bool noChao = false;

    public bool andando = false;

  private Rigidbody2D _rigidbody2D;
  private SpriteRenderer  _spriteRenderer;
  private Animator _animator;

    void Start()
    {
                _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _animator = gameObject.GetComponent<Animator>();

    }

        void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "chao")
            {
                noChao = true;
            }
        }

        void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "chao")
            {
                noChao = false;
            }
        }
    

    
    void Update()
    {
        
            andando = false;
            
            
            _animator.SetBool("IsJump", !noChao); 



      if(Input.GetKey(KeyCode.LeftArrow))
      {
            gameObject.transform.position += new Vector3(-velocidade*Time.deltaTime,0,0);
            //rigidbody2D.AddForce(new Vector2(-velocidade,0));
            _spriteRenderer.flipX = true;
            Debug.Log("LeftArrow");

            if (noChao == true)
            {
                andando = true;
            }
      }

       if(Input.GetKey(KeyCode.RightArrow))
      {
        gameObject.transform.position += new Vector3(velocidade*Time.deltaTime,0,0);
        //rigidbody2D.AddForce(new Vector2(velocidade,0));
         _spriteRenderer.flipX = false;
         Debug.Log("RightArrow");
         
         if (noChao == true)
         {
             
            andando = true;
         }
      }

        if (Input.GetKeyDown(KeyCode.Space) && noChao == true)
        {
            _rigidbody2D.AddForce(new Vector2(0, 1) * focaPulo,ForceMode2D.Impulse);
            
            Debug.Log("Jump");
        }
        
        //resetar por circunstâmcia
        if(transform.position.y <= - 15)
        {
            //jogador caiu
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        _animator.SetBool("Andando",andando);
        
    }
    


}
