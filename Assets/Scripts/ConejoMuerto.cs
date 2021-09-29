using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConejoMuerto : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.transform.CompareTag("Cuerpo"))
            Destroy(gameObject);
    }
}
