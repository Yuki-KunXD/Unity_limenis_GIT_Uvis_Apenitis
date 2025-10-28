using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickup : MonoBehaviour
{
    public GameObject player;
    public Transform holdPos;
    public float throwForce = 500f;
    public float pickUpRange = 5f;
    private float rotationSensitivity = 1f;
    private GameObject heldObj;
    private Rigidbody heldObjRb;
    private bool canDrop = true;
    private int LayerNumber;

    void Start()
    {
        LayerNumber = LayerMask.NameToLayer("HoldLayer");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObj == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange))
                {
                    if (hit.transform.CompareTag("CanPickUp") || hit.transform.CompareTag("PuzzleBlock"))
                    {
                        PickUpObject(hit.transform.gameObject);
                    }
                }
            }
            else
            {
                if (canDrop == true)
                {
                    StopClipping();
                    DropObject();
                }
            }
        }

        if (heldObj != null)
        {
            MoveObject();
            RotateObject();

            if (Input.GetKeyDown(KeyCode.Mouse0) && canDrop == true)
            {
                StopClipping();
                ThrowObject();
            }
        }
    }

    void PickUpObject(GameObject pickUpObj)
    {
        if (pickUpObj.GetComponent<Rigidbody>())
        {
            heldObj = pickUpObj;
            heldObjRb = pickUpObj.GetComponent<Rigidbody>();
            heldObjRb.isKinematic = true;
            heldObjRb.useGravity = false;
            heldObjRb.transform.parent = holdPos.transform;
            heldObj.layer = LayerNumber;
        }
    }

    void DropObject()
    {
        heldObj.layer = 0;
        heldObjRb.isKinematic = false;
        heldObjRb.useGravity = true;
        heldObj.transform.parent = null;
        heldObj = null;
        heldObjRb = null;
    }

    void MoveObject()
    {
        heldObj.transform.position = holdPos.transform.position;
    }

    void RotateObject()
    {
        if (Input.GetKey(KeyCode.R))
        {
            canDrop = false;

            float XaxisRotation = Input.GetAxis("Mouse X") * rotationSensitivity;
            float YaxisRotation = Input.GetAxis("Mouse Y") * rotationSensitivity;

            heldObj.transform.Rotate(Vector3.down, XaxisRotation);
            heldObj.transform.Rotate(Vector3.right, YaxisRotation);
        }
        else
        {
            canDrop = true;
        }
    }

    void ThrowObject()
    {
        heldObj.layer = 0;
        heldObjRb.isKinematic = false;
        heldObjRb.useGravity = true;
        heldObj.transform.parent = null;
        heldObjRb.AddForce(transform.forward * throwForce);
        heldObj = null;
        heldObjRb = null;
    }

    void StopClipping()
    {
        var clipRange = Vector3.Distance(heldObj.transform.position, transform.position);
        RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), clipRange);

        if (hits.Length > 1)
        {
            heldObj.transform.position = transform.position + new Vector3(0f, -0.5f, 0f);
        }
    }
}