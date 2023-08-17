using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotRotation : MonoBehaviour
{
    private List<GameObject> activeSide;
    private Vector3 localForward;
    private Vector3 mouseRef;
    private bool dragging = false;
    private bool autoRotating = false;
    private float sensitivity = 0.3f;
    private float speed = 300;
    private Vector3 rotation;

    private Quaternion targetQuaternion;

    private ReadCube readCube;
    private CubeState cubeState;

    // Start is called before the first frame update
    void Start()
    {
        readCube = FindObjectOfType<ReadCube>();
        cubeState = FindObjectOfType<CubeState>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dragging)
        {
            SpinSide(activeSide);

            if (Input.GetMouseButtonUp(0))
            {
                dragging = false;
                RotateToRightAngle();
            }
        }

        if (autoRotating)
        {
            AutoRotate();
        }
    }

    public void Rotate(List<GameObject> side)
    {
        activeSide = side;

        mouseRef = Input.mousePosition;
        dragging = true;

        // create a vector to rotate around
        localForward = Vector3.zero - side[4].transform.parent.transform.parent.localPosition;
    }

    private void SpinSide(List<GameObject> side)
    {
        // reset rotation
        rotation = Vector3.zero;

        //current mouse pos minus last mous pos
        Vector3 mouseOffSet = (Input.mousePosition - mouseRef);

        if(side == cubeState.front)
        {
            rotation.x = (mouseOffSet.x + mouseOffSet.y) * sensitivity * -1;
        }
        if(side == cubeState.back)
        {
            rotation.x = (mouseOffSet.x + mouseOffSet.y) * sensitivity * 1;
        }
        if(side == cubeState.up)
        {
            rotation.y = (mouseOffSet.x + mouseOffSet.y) * sensitivity * 1;
        }
        if (side == cubeState.down)
        {
            rotation.y = (mouseOffSet.x + mouseOffSet.y) * sensitivity * -1;
        }
        if (side == cubeState.left)
        {
            rotation.z = (mouseOffSet.x + mouseOffSet.y) * sensitivity * 1;
        }
        if (side == cubeState.right)
        {
            rotation.z = (mouseOffSet.x + mouseOffSet.y) * sensitivity * -1;
        }

        //rotating
        transform.Rotate(rotation, Space.Self);

        //store mouse ref
        mouseRef = Input.mousePosition;
    }

    public void StartAutoRotate(List<GameObject> side, float angle)
    {
        cubeState.PickUp(side);

        Vector3 localForward = Vector3.zero - side[4].transform.parent.transform.localPosition;
        targetQuaternion = Quaternion.AngleAxis(angle, localForward) * transform.localRotation;
        activeSide = side;

        autoRotating = true;

    }

    public void RotateToRightAngle()
    {
        Vector3 vec = transform.localEulerAngles;
        // round vec to nearest 90 degrees
        vec.x = Mathf.Round(vec.x / 90) * 90;
        vec.y = Mathf.Round(vec.y / 90) * 90;
        vec.z = Mathf.Round(vec.z / 90) * 90;

        targetQuaternion.eulerAngles = vec;

        autoRotating = true;
    }

    private void AutoRotate()
    {
        dragging = false;
        var step = speed * Time.deltaTime;

        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetQuaternion, step);

        // if within one degree, set angle to targetAngle and end rotation
        if(Quaternion.Angle(transform.localRotation, targetQuaternion) <= 1)
        {
            transform.localRotation = targetQuaternion;

            //unparent the little cubes
            cubeState.PutDown(activeSide, transform.parent);
            readCube.ReadState();
            CubeState.autoRotating = false;

            autoRotating = false;
            dragging = false;
        }
    }
}
