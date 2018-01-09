using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {

    public int n;
    public float g;
    public float v0;
    public float m;

    public GameObject target;

    Vector3[] trajectory;
    LineRenderer trajectoryPath;

    // Use this for initialization
    void Start () {
        trajectory = new Vector3[n];
        trajectoryPath = GetComponent<LineRenderer>();
        trajectoryPath.useWorldSpace = false;
        trajectoryPath.material.color = Color.grey;
        target.GetComponent<SpriteRenderer>().material.color = Color.grey;
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void DrawTrajectory(Vector3 p, Vector3 v)
    {
        trajectoryPath.positionCount = n;
        bool onSurface = false;
        for (int i=0; i<n; i++)
        {
            trajectory[i] = p;
            if (i > 0 && !onSurface)
            {
                RaycastHit hit = new RaycastHit();
                if (Physics.Linecast(trajectory[i - 1], trajectory[i], out hit))
                {
                    Vector3 pos = hit.transform.InverseTransformPoint(transform.position);
                    onSurface = true;
                    target.transform.localPosition = pos + hit.normal * 0.01f;
                    target.transform.forward = hit.normal;
                }
                else
                {
                    Vector3 a = Vector3.down * (m * g);
                    v += a * Time.fixedDeltaTime;
                    p += v * Time.fixedDeltaTime;
                }
            }
        }
        target.SetActive(onSurface);
        trajectoryPath.SetPositions(trajectory);
    }

    public void ClearTrajectory()
    {
        trajectoryPath.positionCount = 2;
        trajectoryPath.SetPositions(new Vector3[2] { Vector3.zero, Vector3.zero });
        target.SetActive(false);
    }

    public Vector3 GetDestination()
    {
        return trajectory[n-1];
    }
}
