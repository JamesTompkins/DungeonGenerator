using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObjectHider : MonoBehaviour {
    public bool testing;
    // Start is called before the first frame update
    void Start() {
        foreach (Transform obj in transform) {
            obj.gameObject.SetActive(false);
        }
    }
}
