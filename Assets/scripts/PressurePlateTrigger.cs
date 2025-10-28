using UnityEngine;

public class PressurePlateTrigger : MonoBehaviour
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
        if (isTriggered)
        {
            leftDoor.position = Vector3.MoveTowards(leftDoor.position, leftOpenPos, doorSpeed * Time.deltaTime);
            rightDoor.position = Vector3.MoveTowards(rightDoor.position, rightOpenPos, doorSpeed * Time.deltaTime);
        }
        else
        {
            leftDoor.position = Vector3.MoveTowards(leftDoor.position, leftClosedPos, doorSpeed * Time.deltaTime);
            rightDoor.position = Vector3.MoveTowards(rightDoor.position, rightClosedPos, doorSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CanPickUp"))
        {
            isTriggered = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CanPickUp"))
        {
            isTriggered = false;
        }
    }
}