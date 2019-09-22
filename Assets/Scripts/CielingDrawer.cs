using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CielingDrawer : MonoBehaviour
{
    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;

        Mesh mesh = GetComponent<MeshFilter>().sharedMesh;
        Gizmos.DrawWireMesh(mesh, transform.position, transform.rotation, transform.localScale);
    }
}
