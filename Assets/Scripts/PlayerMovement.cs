using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float playerSpeed = 10f;
    public float laneChangeSpeed = 10f;

    [Header("Lane Limits")]
    public float leftLimit = -5f;
    public float rightLimit = 5f;

    private int currentLane = 1; // 0 = left, 1 = middle, 2 = right
    private float[] lanes = new float[3];

    void Start()
    {
        // Define exact lane positions
        lanes[0] = leftLimit;
        lanes[1] = 0f;
        lanes[2] = rightLimit;
    }

    void Update()
    {
        MoveForward();
        HandleInput();
        MoveToLane();
    }

    void MoveForward()
    {
        transform.Translate(Vector3.forward * playerSpeed * Time.deltaTime, Space.World);
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentLane--;
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentLane++;
        }

        // Clamp to 3 lanes only
        currentLane = Mathf.Clamp(currentLane, 0, 2);
    }

    void MoveToLane()
    {
        Vector3 targetPosition = new Vector3(
            lanes[currentLane],
            transform.position.y,
            transform.position.z
        );

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            laneChangeSpeed * Time.deltaTime
        );
    }
}
