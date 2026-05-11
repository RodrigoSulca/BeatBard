using UnityEngine;

[System.Serializable]
public class Note
{
    public enum NoteType{Normal,Hold}
    public float spawnTime;
    public int line;
    public NoteType type;
    public float holdTime;
}
