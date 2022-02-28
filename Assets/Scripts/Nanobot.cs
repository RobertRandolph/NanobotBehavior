using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nanobot : MonoBehaviour {
    // Constants
    [SerializeField] private float speed = 12f;

    // States
    private bool isMoving;

    // Temp Data
    [SerializeField] Vector3 targetPosition;

    void Start() {
        // Testing
        MoveCommand(targetPosition);
    }

    void FixedUpdate() {
        if(isMoving) {
            Move();
        }
    }

    // Commands
    void MoveCommand(Vector3 position) {
        isMoving = true;
        targetPosition = position;
    }

    // Behavior
    void Move() {
        // Determine new position
        Vector3 distanceFromTarget = targetPosition - transform.position;
        Vector3 directionToTarget = distanceFromTarget.normalized;
        Vector3 moveAmount = directionToTarget * speed * Time.deltaTime;
        Vector3 newPosition = transform.position + moveAmount;

        // Determine if nanobot reached target position
        if (moveAmount.magnitude > distanceFromTarget.magnitude) {
            // Reached target position
            newPosition = targetPosition;
            isMoving = false;
        }

        transform.position = newPosition;
    }
}
