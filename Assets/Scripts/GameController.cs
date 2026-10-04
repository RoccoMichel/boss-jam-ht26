using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public bool debug;

    [Header("References")]
    public Settings settings;
    public static GameController instance;
    public GameObject[] respawnItems;

    private Player player;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        respawnItems = GameObject.FindGameObjectsWithTag("Respawn");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1)) debug = !debug;
    }

    public void ResetLevel()
    {
        foreach (GameObject go in respawnItems) go.SetActive(true);        
    }
    private void OnGUI()
    {
        if (!debug) return;

        // Text
        GUI.Label(new Rect(10, 10, 200, 20), $"ms per frame: {System.Decimal.Round((decimal)(Time.deltaTime * 1000), 2)}");
        GUI.Label(new Rect(10, 40, 200, 20), $"frame per second: {1f / Time.deltaTime}");

        // Buttons
        if (GUI.Button(new Rect(10, 70, 100, 20), "Reload")) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if (GUI.Button(new Rect(10, 100, 100, 20), "Exit")) Application.Quit();
    }

    private void Reset()
    {
        transform.position = Vector3.zero;
        gameObject.tag = "GameController";
        gameObject.name = "--- GameController ---";
    }
}