using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nanobot : MonoBehaviour {
    // Constants
    [SerializeField] GameObject nanobotPrefab;
    [SerializeField] private float speed = 12f;
    [SerializeField] private float replicationTimeSec = 5f;

    // States
    private bool isMoving;
    private bool isReplicating;

    // Temp Data
    [SerializeField] Vector3 targetPosition;
    [SerializeField] float targetTime;

    void Start() {
        // Testing
        // MoveCommand(targetPosition);
        ReplicateCommand();

    }

    void FixedUpdate() {
        if (isReplicating) {
            Replicate();
        }
        else if (isMoving) {
            Move();
        }
    }

    // ===== Commands =====
    /// <summary>
    /// Command recieved from the nanobot controller to move the nanobot to the target position.
    /// </summary
    /// <param name="targetPosition"> Target position the nanobot will move to. </param>
    public void MoveCommand(Vector3 targetPosition) {
        isMoving = true;
        this.targetPosition = targetPosition;
    }

    /// <summary>
    /// Command recieved from the nanobot controller to replicate itself.
    /// Sets the target time based on the replicationTimeSec field.
    /// </summary>
    void ReplicateCommand() {
        isReplicating = true;
        targetTime = Time.time + replicationTimeSec;
    }

    // ===== Behavior =====
    /// <summary>
    /// Behavior of the MoveCommand method.
    /// Moves the nanobot towards the target position.
    /// If the nanobot will overshoot the target, then it will settle on the target instead.
    /// When the nanobot reaches the target position the movement command has succeeded.
    /// </summary>
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

    /// <summary>
    /// Behavior of the ReplicateCommand method.
    /// After reaching or passing the target time, a new nanobot is instantiated above itself.
    /// After replicating the replication command has succeeded.
    /// </summary>
    void Replicate() {
        if (Time.time > targetTime) {
            Vector3 nanobotPosition = transform.position + new Vector3(0, 1, 0);
            Instantiate(nanobotPrefab, nanobotPosition, transform.rotation);
            isReplicating = false;
        }
    }
}
