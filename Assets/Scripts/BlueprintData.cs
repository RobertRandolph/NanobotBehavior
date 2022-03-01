using UnityEngine;

public enum BlueprintState {
    Idle,
    Constructing,
    Completed
}

public struct BlueprintData {
    // Identity
    public string uniqueID {get;} = "Bxxxxx";

    // Blueprint data
    public Blueprint blueprint {get;} = null;
    public Vector3 center {get;} = Vector3.zero;
    public int totalNanobotTargetCount {get;} = 0;

    // State    
    public BlueprintState blueprintState = BlueprintState.Idle;

    public BlueprintData(string uniqueID, Blueprint blueprint,
    Vector3 targetPosition) {
        this.uniqueID = uniqueID;
        this.blueprint = blueprint;
        this.center = targetPosition;
        totalNanobotTargetCount = blueprint.getTotalNanobotTargetCount();
    }
}
