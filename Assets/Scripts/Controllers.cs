using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.WSA.Input;

public class Controllers : MonoBehaviour {

    public GameObject leftHand;
    public GameObject rightHand;     

    Vector3 leftPos;
    Vector3 rightPos;
    Quaternion leftRot;
    Quaternion rightRot;

    Material rightMat;
    Color rightColour;

    Material leftMat;
    Color leftColour;

	// Use this for initialization
	void Start () {
        leftPos = new Vector3();
        rightPos = new Vector3();
        leftRot = new Quaternion();
        rightRot = new Quaternion();

        leftMat = leftHand.GetComponentInChildren<Renderer>().material;
        leftColour = leftMat.color;
        rightMat = rightHand.GetComponentInChildren<Renderer>().material;
        rightColour = rightMat.color;
	}
	
	// Update is called once per frame
	void Update () {

        leftPos = InputTracking.GetLocalPosition(XRNode.LeftHand);
        rightPos = InputTracking.GetLocalPosition(XRNode.RightHand);

        leftRot = InputTracking.GetLocalRotation(XRNode.LeftHand);
        rightRot = InputTracking.GetLocalRotation(XRNode.RightHand);

        leftHand.transform.position = leftPos;
        leftHand.transform.rotation = leftRot;

        rightHand.transform.position = rightPos;
        rightHand.transform.rotation = rightRot;

        if (Input.GetAxis("RightTrigger") > 0.0f)
        {
            rightMat.color = Color.red;
        }
        else
        {
            rightMat.color = rightColour;
        }

        if (Input.GetAxis("LeftTrigger") > 0.0f)
        {
            leftMat.color = Color.blue;
        }
        else
        {
            leftMat.color = leftColour;
        }

    }

}
