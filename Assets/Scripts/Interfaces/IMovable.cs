using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovable 
{
    public float secondsPerTile { get; set; }
    public List<TileEntity> path { get; set; }
    void MoveNext(int pCurrentIndex, float pTimeItTakes);
}
