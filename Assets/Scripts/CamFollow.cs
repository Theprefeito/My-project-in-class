using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform Player; //Referência ao transform do jogador
    public float smoothSpeed = 0.125f;//A suavizaç~]ao do movimento da câmera 
    public Vector3 offset;//O deslocamento da câmera em relação ao jogador

    void Start()
    {

    }

    
    void Update()
    {
      
    }

    void LateUpdate()
    {
        //Posição desejada da câmera (posição do jogador + deslocamento)
        Vector3 desiredPosition = new Vector3(Player.position.x + offset.x, Player.position.y + offset.y, transform.position.z);

        //Suaviza o movimento da câmera entre a posição atual e a desejada
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        //Atualiza a posição da câmera 
        transform.position = smoothedPosition;

    }
}
