using UnityEngine;

public class RoomTrapSequence : MonoBehaviour
{
    public Transform backDoorLeft;
    public Transform backDoorRight;
    public Vector3 leftClosedOffset = new Vector3(-2f, 0f, 0f);
    public Vector3 rightClosedOffset = new Vector3(2f, 0f, 0f);
    public float doorSpeed = 2f;

    public Light redLight;
    public GameObject trapDoor;
    public float trapDelay = 5f;

    private Vector3 leftOpenPos;
    private Vector3 rightOpenPos;
    private Vector3 leftClosedPos;
    private Vector3 rightClosedPos;
    private bool sequenceStarted = false;
    private bool doorsClosing = false;

    void Start()
    {
        // Store door positions
        leftOpenPos = backDoorLeft.position;
        rightOpenPos = backDoorRight.position;
        leftClosedPos = leftOpenPos + leftClosedOffset;
        rightClosedPos = rightOpenPos + rightClosedOffset;

        // Turn off red light at start
        if (redLight != null)
            redLight.enabled = false;

        // Make trapdoor invisible but still active
        if (trapDoor != null)
        {
            Renderer rend = trapDoor.GetComponent<Renderer>();
            if (rend != null)
                rend.enabled = false;
        }
    }

    void Update()
    {
        if (doorsClosing)
        {
            backDoorLeft.position = Vector3.MoveTowards(backDoorLeft.position, leftClosedPos, doorSpeed * Time.deltaTime);
            backDoorRight.position = Vector3.MoveTowards(backDoorRight.position, rightClosedPos, doorSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (sequenceStarted) return;

        if (other.CompareTag("Player"))
        {
            sequenceStarted = true;
            doorsClosing = true;

            if (redLight != null)
                redLight.enabled = true;

            Invoke("ActivateTrap", trapDelay);
        }
    }

    void ActivateTrap()
    {
        if (trapDoor != null)
        {
            trapDoor.SetActive(false); // Makes the trapdoor disappear
        }
    }
}