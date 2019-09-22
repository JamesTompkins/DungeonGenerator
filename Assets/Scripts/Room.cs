using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {
    public RoomInformation roomInformation;

    private static bool bugTesting = false;
    private static float minDistance = 64;
    private static float checkPeriod = 1;
    private Transform player;
    private Vector3 position;

    void Start() {
        player = FindObjectOfType<PlayerController>().transform;
        position = transform.position;

        if (!bugTesting) {
            InvokeRepeating("checkDistance", Random.Range(0, checkPeriod), checkPeriod);
        }
    }

    void checkDistance() {
        float xDistance = Mathf.Abs(player.position.x - position.x);
        float zDistance = Mathf.Abs(player.position.z - position.z);

        if (xDistance > minDistance || zDistance > minDistance) {
            gameObject.SetActive(false);
        } else {
            gameObject.SetActive(true);
        }
    }
}
