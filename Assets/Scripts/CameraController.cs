using UnityEngine;

public class CameraController : MonoBehaviour
{
    public int targetFOV = 25;
    public float adjustFOVSpeed = 0.3f;
    public float moveSpeed = 1;
    public float minSpeed = 0.4f;
    private Transform player;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player").transform;

        transform.position = player.transform.position;
        SetFOV(0);
    }

    private void Update()
    {
        float moveDistance = Mathf.Clamp(Vector3.Distance(transform.position, player.position), 1, float.MaxValue) * moveSpeed;
        moveDistance *= moveDistance;
        transform.position = Vector3.MoveTowards(transform.position, player.position, moveDistance * Time.deltaTime);

        float newFieldOfView = Mathf.Lerp(mainCamera.fieldOfView, targetFOV, adjustFOVSpeed * Time.deltaTime);
        mainCamera.fieldOfView = newFieldOfView;
    }

    public void SetFOV(int amount)
    {
        amount = Mathf.Clamp(amount, 1, 120);
        mainCamera.fieldOfView = amount;
    }
}
