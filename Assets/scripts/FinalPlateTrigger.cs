using UnityEngine;

public class FinalPlateTrigger : MonoBehaviour
{
    public Transform leftDoor;
    public Transform rightDoor;
    public Vector3 leftOpenOffset = new Vector3(-2f, 0f, 0f);
    public Vector3 rightOpenOffset = new Vector3(2f, 0f, 0f);
    public float doorSpeed = 2f;

    private Vector3 leftClosedPos;
    private Vector3 rightClosedPos;
    private Vector3 leftOpenPos;
    private Vector3 rightOpenPos;
    private bool isTriggered = false;

    void Start()
    {
        leftClosedPos = leftDoor.position;
        rightClosedPos = rightDoor.position;
        leftOpenPos = leftClosedPos + leftOpenOffset;
        rightOpenPos = rightClosedPos + rightOpenOffset;
    }

    void Update()
    {
        Vector3 leftTarget = isTriggered ? leftOpenPos : leftClosedPos;
        Vector3 rightTarget = isTriggered ? rightOpenPos : rightClosedPos;

        leftDoor.position = Vector3.MoveTowards(leftDoor.position, leftTarget, doorSpeed * Time.deltaTime);
        rightDoor.position = Vector3.MoveTowards(rightDoor.position, rightTarget, doorSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PuzzleBlock"))
        {
            isTriggered = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PuzzleBlock"))
        {
            isTriggered = false;
        }
    }
}