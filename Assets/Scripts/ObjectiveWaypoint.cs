using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveWaypoint : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 waypointOffset;
    [SerializeField] private Image waypointImg;
    [SerializeField] private Text distanceText;

    void Update()
    {
        float minX = waypointImg.GetPixelAdjustedRect().width / 3.6f;
        float maxX = Screen.width - minX;

        float minY = waypointImg.GetPixelAdjustedRect().height / 3.6f;
        float maxY = Screen.height - minY;

        var waypointPos = Camera.main.WorldToScreenPoint(target.position + waypointOffset);

        if (targetIsBehindPlayer)
        {
            waypointPos.y = maxY;

            if (waypointPos.x < Screen.width / 2) // left side of the screen
            {
                waypointPos.x = maxX; // place the waypoint in the right (opposite)
            }
            else // right side of the screen
            {
                waypointPos.x = minX; // place in the left if it is on the right side (opposite)
            }
        }

        // limit the where the waypoint can stay
        waypointPos.x = Mathf.Clamp(waypointPos.x, minX, maxX);
        waypointPos.y = Mathf.Clamp(waypointPos.y, minY, maxY);

        waypointImg.transform.position = waypointPos; // update the waypoint position
        distanceText.text = ((int)Vector3.Distance(target.position, transform.position)).ToString() + "m"; // update the distance in meters 'm'
    }

    private bool targetIsBehindPlayer => Vector3.Dot((target.position - transform.position), transform.forward) < 0;
}
