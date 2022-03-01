

public enum NanobotState {
    Idle,
    Moving,
    Replicating,
    Locked
}

public struct NanobotData {
    string uniqueID = "Nxxxxx";
    Nanobot nanobot = null;
    NanobotState nanobotState = NanobotState.Idle;
    string associatedBlueprintID = "Bxxxxx";   // Blueprint the nanobot is apart of

    // Init
    public NanobotData(string uniqueID, Nanobot nanobot,
    string associatedBlueprintID, NanobotState nanobotState = NanobotState.Idle) {
        this.uniqueID = uniqueID;
        this.nanobot = nanobot;
        this.nanobotState = nanobotState;
        this.associatedBlueprintID = associatedBlueprintID;
    }
}
