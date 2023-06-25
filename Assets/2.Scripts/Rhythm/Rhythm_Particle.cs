using UnityEngine;
using System.Collections;

public class Rhythm_Particle : MonoBehaviour
{
    private ParticleSystem ps;


    public void Start()
    {
        Destroy(gameObject, 5f);
    }
}
