using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEdit : Editor
{
    private void OnSceneGUI()
    {
        FieldOfView FOV = (FieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(FOV.transform.position, Vector3.up, Vector3.forward, 360, FOV.Radius);

        Vector3 viewAngleLeft = DirectionFromAngle(FOV.transform.eulerAngles.y, -FOV.Angle / 2);
        Vector3 viewAngleRight = DirectionFromAngle(FOV.transform.eulerAngles.y, FOV.Angle / 2);

        Handles.color = Color.yellow;

        Handles.DrawLine(FOV.transform.position,  FOV.transform.position + viewAngleLeft * FOV.Radius);
        Handles.DrawLine(FOV.transform.position, FOV.transform.position + viewAngleRight * FOV.Radius);

        if (FOV.CanSeePlayer)
        {
            Handles.color = Color.green;
            Handles.DrawLine(FOV.transform.position, FOV.Player.transform.position);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;
        return new Vector3 (Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
