using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerTrigger : MonoBehaviour
{
    [Tooltip("Log when triggered")]
    public bool debug;
    public bool oneShot = true;
    public UnityEvent events = new();
    private bool triggered;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (oneShot && triggered) return;

        if (debug) Debug.Log(gameObject.name + " triggered.");
        TriggerEvents();
        triggered = true;
    }

    public void TriggerEvents() => events.Invoke();

    // Events

    public void Log(string message)
    {
        Debug.Log(message);
    }

    public void DeathZone()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;

        GameObject source = new(clip.name + " | Event SFX");
        AudioSource audioSource = source.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.loop = false;
        audioSource.Play();

        Destroy(source, clip.length);
    }
    public void ToggleGameObjects(GameObject gameObject)
    {
        if (gameObject == null) return;

        gameObject.SetActive(!gameObject.activeInHierarchy);
    }

    public void DestroyGameObject(GameObject gameObject)
    {
        Destroy(gameObject);
    }

    public void TriggerEffect(ParticleSystem effect)
    {
        effect.Play();
    }

    public void LoadScene(string sceneName)
    {
        try { SceneManager.LoadScene(sceneName); }
        catch { Debug.LogError($"{gameObject.name} is trying to load scene \"{sceneName}\" but it failed"); }
    }
}