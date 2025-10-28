using UnityEngine;

public class AutoSlidingDoor : MonoBehaviour
{
    public Transform player;
    public Transform leftDoor;
    public Transform rightDoor;
    public Vector3 leftOpenOffset = new Vector3(-2f, 0f, 0f);
    public Vector3 rightOpenOffset = new Vector3(2f, 0f, 0f);
    public float doorSpeed = 2f;
    public float activationDistance = 3f;

    private Vector3 leftClosedPos;
    private Vector3 rightClosedPos;
    private Vector3 leftOpenPos;
    private Vector3 rightOpenPos;

    void Start()
    {
        leftClosedPos = leftDoor.position;
        rightClosedPos = rightDoor.position;
        leftOpenPos = leftClosedPos + leftOpenOffset;
        rightOpenPos = rightClosedPos + rightOpenOffset;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        bool shouldOpen = distanceToPlayer <= activationDistance;

        Vector3 leftTarget = shouldOpen ? leftOpenPos : leftClosedPos;
        Vector3 rightTarget = shouldOpen ? rightOpenPos : rightClosedPos;

        leftDoor.position = Vector3.MoveTowards(leftDoor.position, leftTarget, doorSpeed * Time.deltaTime);
        rightDoor.position = Vector3.MoveTowards(rightDoor.position, rightTarget, doorSpeed * Time.deltaTime);
    }
}