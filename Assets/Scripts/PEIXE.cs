using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PEIXE : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

 private void OnTriggerEnter(Collider other)
 {
   if(other.tag == "Player")
        {
            Destroy(gameObject);
        }
 }
 
    void Update()
    {
       
    }
}
