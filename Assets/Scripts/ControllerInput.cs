using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.WSA.Input;

public class ControllerInput : MonoBehaviour
{

    public GameObject leftHand;
    public GameObject rightHand;


    // Use this for initialization
    void Start()
    {

        InteractionManager.InteractionSourceDetected += InteractionManager_SourceDetected;
        InteractionManager.InteractionSourceUpdated += InteractionManager_SourceUpdated;
        InteractionManager.InteractionSourceLost += InteractionManager_SourceLost;
        InteractionManager.InteractionSourcePressed += InteractionManager_SourcePressed;
        InteractionManager.InteractionSourceReleased += InteractionManager_SourceReleased;
    }

    void OnDestroy()
    {

        InteractionManager.InteractionSourceDetected -= InteractionManager_SourceDetected;
        InteractionManager.InteractionSourceUpdated -= InteractionManager_SourceUpdated;
        InteractionManager.InteractionSourceLost -= InteractionManager_SourceLost;
        InteractionManager.InteractionSourcePressed -= InteractionManager_SourcePressed;
        InteractionManager.InteractionSourceReleased -= InteractionManager_SourceReleased;
    }
    // Update is called once per frame
    void Update()
    {


    }

    void InteractionManager_SourcePressed(InteractionSourcePressedEventArgs args)
    {

        Debug.Log("Source pressed");
    }
    void InteractionManager_SourceDetected(InteractionSourceDetectedEventArgs args)
    {

        Debug.Log("Source detected");
    }
    void InteractionManager_SourceUpdated(InteractionSourceUpdatedEventArgs args)
    {
        Vector3 p;
        Quaternion r;

        args.state.sourcePose.TryGetPosition(out p);
        args.state.sourcePose.TryGetRotation(out r);
        // string acc = args.state.sourcePose.positionAccuracy.ToString();

        Debug.Log("Hand " + args.state.source.handedness.ToString());

        if (args.state.source.handedness.ToString() == "Left")
        {
            leftHand.transform.position = p;
            leftHand.transform.rotation = r;
        }
        else if (args.state.source.handedness.ToString() == "Right")
        {
            rightHand.transform.position = p;
            rightHand.transform.rotation = r;
        }

        Debug.Log("Source updated " + p);
    }
    void InteractionManager_SourceLost(InteractionSourceLostEventArgs args)
    {

        Debug.Log("Source lost");
    }
    void InteractionManager_SourceReleased(InteractionSourceReleasedEventArgs args)
    {

        Debug.Log("Source released");
    }

}