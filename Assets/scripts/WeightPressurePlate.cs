using UnityEngine;
using System.Collections.Generic;

public class WeightPressurePlate : MonoBehaviour
{
    public Transform platform;
    public Vector3 raisedOffset = new Vector3(0f, 3f, 0f);
    public float liftSpeed = 2f;
    public float requiredMass = 4f; // Exact mass required to trigger

    private Vector3 startPos;
    private Vector3 targetPos;
    private HashSet<Rigidbody> boxesOnPlate = new HashSet<Rigidbody>();
    private bool isActivated = false;

    void Start()
    {
        startPos = platform.position;
        targetPos = startPos + raisedOffset;
    }

    void Update()
    {
        float totalMass = 0f;
        foreach (Rigidbody rb in boxesOnPlate)
        {
            if (rb != null)
                totalMass += rb.mass;
        }

        isActivated = Mathf.Approximately(totalMass, requiredMass);

        Vector3 goal = isActivated ? targetPos : startPos;
        platform.position = Vector3.MoveTowards(platform.position, goal, liftSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb != null && (other.CompareTag("CanPickUp")))
        {
            boxesOnPlate.Add(rb);
        }
    }

    void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb != null && boxesOnPlate.Contains(rb))
        {
            boxesOnPlate.Remove(rb);
        }
    }
}