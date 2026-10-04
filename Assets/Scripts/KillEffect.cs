using UnityEngine;

public class KillEffect : MonoBehaviour
{
    private void Start()
    {
        float lifeTime = GetComponent<ParticleSystem>().main.duration;
        Destroy(gameObject, lifeTime);
    }
}
