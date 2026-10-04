using UnityEngine;

public class FloorMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10;
    private Vector3 startLocation;
    private Transform player;
    private void Start()
    {
        startLocation = transform.position;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        float distance = Vector3.Distance(startLocation, player.position);
        float targetPosition = Mathf.Lerp(startLocation.y, -200, Mathf.InverseLerp(5, 15, distance));

        transform.position = new()
        {
            x = transform.position.x,
            y = Mathf.Lerp(transform.position.y, targetPosition, moveSpeed * Time.deltaTime),
            z = transform.position.z,
        };
    }
}
