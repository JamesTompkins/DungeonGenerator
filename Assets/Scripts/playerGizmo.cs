using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerGizmo : MonoBehaviour
{
    public float distance;
    private void OnDrawGizmos() {
        Gizmos.color = Color.cyan;

        Gizmos.DrawRay(transform.position, transform.up * distance);
    }
}
