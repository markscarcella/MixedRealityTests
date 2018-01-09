using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class InputHandling : MonoBehaviour
{
    public GameObject player;
    public GameObject leftHand;
    public GameObject rightHand;

    Vector3 pos = new Vector3();
    Quaternion rot = new Quaternion();
    Vector3 point = new Vector3();

    LineRenderer leftRay;
    Teleport rightTrajectory;

    void Start()
    {
        InteractionManager.InteractionSourceDetected += InteractionManager_SourceDetected;
        InteractionManager.InteractionSourceUpdated += InteractionManager_SourceUpdated;
        InteractionManager.InteractionSourceLost += InteractionManager_SourceLost;
        InteractionManager.InteractionSourcePressed += InteractionManager_SourcePressed;
        InteractionManager.InteractionSourceReleased += InteractionManager_SourceReleased;

        leftRay = leftHand.GetComponent<LineRenderer>();
        leftRay.material.color = Color.blue;

        rightTrajectory = rightHand.GetComponent<Teleport>();
    }

    void OnDestroy()
    {
        InteractionManager.InteractionSourceDetected -= InteractionManager_SourceDetected;
        InteractionManager.InteractionSourceUpdated -= InteractionManager_SourceUpdated;
        InteractionManager.InteractionSourceLost -= InteractionManager_SourceLost;
        InteractionManager.InteractionSourcePressed -= InteractionManager_SourcePressed;
        InteractionManager.InteractionSourceReleased -= InteractionManager_SourceReleased;
    }

    void InteractionManager_SourceDetected(InteractionSourceDetectedEventArgs args)
    {
        // Source was detected
        // state has the current state of the source including id, position, kind, etc.
        //Debug.Log("Source Detected");
    }

    void InteractionManager_SourceLost(InteractionSourceLostEventArgs args)
    {
        // Source was lost. This will be after a SourceDetected event and no other events for this source id will occur until it is Detected again
        // state has the current state of the source including id, position, kind, etc.
        //Debug.Log("Source Lost");
    }

    void InteractionManager_SourceUpdated(InteractionSourceUpdatedEventArgs args)
    {
        // Source was updated. The source would have been detected before this point
        // state has the current state of the source including id, position, kind, etc.
        //Debug.Log("Source Updated");
        args.state.sourcePose.TryGetPosition(out pos);
        args.state.sourcePose.TryGetRotation(out rot);
        args.state.sourcePose.TryGetForward(out point, InteractionSourceNode.Pointer);
        if (args.state.source.handedness == InteractionSourceHandedness.Left)
        {
            leftHand.transform.localPosition = pos;
            leftHand.transform.localRotation = rot;
            leftRay.positionCount = 2;
            leftRay.SetPositions(new Vector3[2] { pos, pos + point });
        }
        else if (args.state.source.handedness == InteractionSourceHandedness.Right)
        {
            rightHand.transform.localPosition = pos;
            rightHand.transform.localRotation = rot;
            if (args.state.selectPressedAmount > 0.0f)
            {
                rightTrajectory.DrawTrajectory(pos, (pos + point * 10));
                if (args.state.touchpadPressed)
                {
                    player.transform.position = rightTrajectory.GetDestination();
                    rightTrajectory.ClearTrajectory();
                }
            }
            else
            {
                rightTrajectory.ClearTrajectory();
            }
        }
    }

    void InteractionManager_SourcePressed(InteractionSourcePressedEventArgs args)
    {
        // Source was pressed. This will be after the source was detected and before it is released or lost
        // state has the current state of the source including id, position, kind, etc.
        Debug.Log("Source Pressed");

        if (args.state.selectPressedAmount > 0.0f)
        {
            if (args.state.source.handedness == InteractionSourceHandedness.Left)
            {
                leftRay.material.color = Color.red;
            }
        }
    }

    void InteractionManager_SourceReleased(InteractionSourceReleasedEventArgs args)
    {
        // Source was released. The source would have been detected and pressed before this point. This event will not fire if the source is lost
        // state has the current state of the source including id, position, kind, etc.
        //Debug.Log("Source Released");
        if (args.state.source.handedness == InteractionSourceHandedness.Left)
        {
            leftRay.material.color = Color.blue;
        }
    }
}