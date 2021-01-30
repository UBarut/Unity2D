using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseCollider : MonoBehaviour
{
    // Comments
    CheckPoints checkPoints;

    public void Update()
    {
        checkPoints = GameObject.FindObjectOfType<CheckPoints>();
    }
  
    public void OnTriggerEnter2D(Collider2D collision)
    {
       checkPoints.ChangePlayer(collision);
    } 
}
