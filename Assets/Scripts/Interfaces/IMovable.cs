using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovable 
{
    [SerializeField]
    public float movementSpeed { get; set; }
    public List<TileEntity> path { get; set; }
    IEnumerator MoveToNextTile(GameObject pObjectToMove, Vector3 pEnd, float pSeconds);
    
}
