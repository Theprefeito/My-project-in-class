using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
     private Transform alvo;
    public Vector2 offset;
    public int suavidade = 5;

    void Start()
    {
        alvo = GameObject.FindWithTag("Player").transform;
        offset = alvo.position + transform.position;
    }

    
    void Update()
    {
        Vector2 posfinal = offset;
        transform.position = Vector2.Lerp(transform.position,posfinal,suavidade * Time.deltaTime);
    }
}
