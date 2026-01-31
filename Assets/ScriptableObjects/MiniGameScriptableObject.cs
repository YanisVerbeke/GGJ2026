using UnityEngine;

[CreateAssetMenu(fileName = "MiniGameScriptableObject", menuName = "Scriptable Objects/MiniGame")]
public class MiniGameScriptableObject : ScriptableObject
{
    public int indexInBuild;
    public string displayName;
    public Controls controls;
    public float timer;

}

public enum Controls { MOUSE, KEYBOARD }
