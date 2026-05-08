using UnityEngine;

[System.Serializable]
public class Phase
{
    public int phaseId;
    public float start;
    public float finish;
    public int[] enemyId;
    public int[] spawnRow;
    public float[] spawnTime;
}
