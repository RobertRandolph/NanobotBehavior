using System.Collections.Generic;
using UnityEngine;

public abstract class Blueprint {
    // Blueprint Data
    public string name = "Blueprint";
    public Vector3 targetCenter = Vector3.zero;
    public List<Vector3> nanobotTargetPositions {get; protected set;} = null;

    // Blueprint Tree
    public Blueprint parent = null;
    public List<Blueprint> dependencies {get; protected set;} = null;

    // ===== Utility =====
    /// <summary>
    /// Calculates and returns the total number of nanobot targets the blueprint holds
    /// <\summary>
    public int getTotalNanobotTargetCount() {
        int total = 0;

        if (nanobotTargetPositions != null) {
            total += (int) nanobotTargetPositions.Count;
        }

        foreach (Blueprint blueprint in dependencies) {
            total += blueprint.getTotalNanobotTargetCount();
        }

        return total;
    }

    public static List<Blueprint> avaliableBlueprints = new List<Blueprint>() {
        new Blueprint2DSheet(),
        new Blueprint3DFullCube()
    };
}

public class Blueprint2DSheet : Blueprint {
    public Blueprint2DSheet(int width = 3, int length = 3) {
        name = "2DSheet";
        setNanobotTargetsPositions(width, length);
    }

    private void setNanobotTargetsPositions(int width, int length) {
        nanobotTargetPositions = new List<Vector3>();

        for (int i = 0; i < width; i++) {
            for (int j = 0; j < length; j++) {
                Vector3 nanobotTarget = new Vector3(i, 0, j) + targetCenter;
                nanobotTargetPositions.Add(nanobotTarget);
            }
        }
    }
}

public class Blueprint3DFullCube : Blueprint {
    public Blueprint3DFullCube(int width = 2, int length = 2, int height = 2) {
        name = "3DFullCube";
        setDependencies(width, length, height);
    }

    private void setDependencies(int width, int length, int height) {
        dependencies = new List<Blueprint>();

        for (int i = 0; i < height; i++) {
            var temp = new Blueprint2DSheet(width, length);
            temp.targetCenter = new Vector3(0, i, 0);
            temp.parent = this;
            dependencies.Add(temp);
        }
    }
}