using UnityEngine;

public class ParticleCleanup : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, GetComponent<ParticleSystem>().main.duration);
    }
}
