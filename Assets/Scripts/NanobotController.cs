using System.Collections.Generic;
using UnityEngine;

public class NanobotController : MonoBehaviour {
    // ===== Constants =====
    // Nanobots
    [SerializeField] GameObject nanobotPrefab;
    // Identity
    private string nanobotPrefix = "N";
    private int nextNanobotID = 0;
    // Thresholds
    [SerializeField] private int minimumNanobotCount = 4;
    [SerializeField] private float replicatePercentage;

    // Blueprint
    private string blueprintPrefix = "B";
    private int nextBlueprintID = 0;

    // ===== Controller Runtime =====

    // States

    // Nanobot data
    private Dictionary<string, NanobotData> nanobotData;
    private Queue<string> avaliableNanobots;
    private int replicatingNanobots;

    // Blueprint data
    private Dictionary<string, BlueprintData> blueprintData;
    private Queue<string> blueprintQueue;
    private HashSet<string> completedBlueprints;

    // Temp Data
    private string targetBlueprint = null;

    // Init & Testing
    void Start() {
        // Init
        // Nanobots
        nanobotData = new Dictionary<string, NanobotData>();
        avaliableNanobots = new Queue<string>();
        // Blueprints
        blueprintData = new Dictionary<string, BlueprintData>();
        blueprintQueue = new Queue<string>();
        completedBlueprints = new HashSet<string>();

        // Testing
        List<Blueprint> blueprints = Blueprint.avaliableBlueprints;
        addBlueprintToQueue(blueprints[0], new Vector3(10, 10, 10)); // 2D Sheet
        addBlueprintToQueue(blueprints[1], new Vector3(-10, 10, -10)); // 3D Full Cube
    }

    void addBlueprintToQueue(Blueprint blueprint, Vector3 targetCenter) {
        string blueprintID = blueprintPrefix + nextBlueprintID++;
        BlueprintData blueprintData = new BlueprintData(blueprintID, blueprint, targetCenter);
        this.blueprintData.Add(blueprintID, blueprintData);
        blueprintQueue.Enqueue(blueprintID);
    }

    void FixedUpdate() {
        // Getting Blueprint
        if (targetBlueprint == null) {
            // Not working on blueprint
            if (blueprintQueue.Count > 0) {
                // There is a blueprint to work on
                targetBlueprint = blueprintQueue.Dequeue();
                updateBlueprintState(targetBlueprint, BlueprintState.Constructing);
            } else {
                // No blueprints to work on
                return;
            }
        }

        // Assigning Nanobots
        AssignNanobots();
    }

    // ===== Nanobot Management =====
    /// <summary>
    /// Creates a new nanobot from the nanobot controller
    /// <\summary>
    void createNanobot() {
        
    }

    /// <summary>
    /// Assigns nanobots based on priorities
    /// - First: replicate nanobots until the minimum number is reached
    ///     - (or until equal to number of targets)
    /// - Second: keep replicating minimum number up to a total percentage of avaliable nanobots
    ///     - (or until equal to number of targets)
    /// - Third: The rest of the nanobots will begin to move to their positions
    /// - Fourth: Continue until blueprint is completed
    /// <\summary>
    void AssignNanobots() {
        // Checking if there is at least one nanobot
        


        // Replicating nanbots



        int totalNanobotTargets = getTotalBlueprintNanobotTargetsCount(targetBlueprint);
    }

    // ===== Nanobot Commands =====
    void moveNanobot() {

    }

    void replicateNanobot() {

    }

    // ===== Utility =====
    /// <sumamry>
    /// Returns the blueprint data associated with the given ID
    /// </summary>
    /// <param name="blueprintID"> The blueprint to retrieve <\param>
    BlueprintData getBlueprintData(string blueprintID) {
        return blueprintData[blueprintID];
    }

    /// <summary>
    /// Updates the state of the blueprint assosiated with the given ID.
    /// </summary>
    /// <param name="blueprintID"> The blueprint to update the state of <\param>
    /// <param name="newState"> The new state of the blueprint <\param>
    void updateBlueprintState(string blueprintID, BlueprintState newState) {
        BlueprintData newBlueprintData = getBlueprintData(blueprintID);
        newBlueprintData.blueprintState = newState;
        blueprintData[blueprintID] = newBlueprintData;
    }

    /// <summary>
    /// Returns the total number of nanobot targets of the blueprint
    /// </summary>
    /// <param name="blueprintID"> The blueprint to count from <\param>
    int getTotalBlueprintNanobotTargetsCount(string blueprintID) {
        return getBlueprintData(blueprintID).totalNanobotTargetCount;
    }
}
