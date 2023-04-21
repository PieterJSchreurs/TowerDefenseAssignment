using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileState")]
public class TileState : ScriptableObject
{
    public Color TileColor;
    public bool CanBuildOnTile;
}
