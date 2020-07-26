using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTriggerController : MonoBehaviour
{
    public ParticleSystem winParticles;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            winParticles.Play();
        }
    }
}
