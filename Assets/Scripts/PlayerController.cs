using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float vertical = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        Vector3 moveSpeed = new Vector3(horizontal, 0, vertical);
        GetComponent<CharacterController>().Move(moveSpeed);
    }
}
