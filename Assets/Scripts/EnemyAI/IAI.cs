using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAI
{
    void MoveTowardsPlayer(Vector2Int playerPosition);
    void StopMovement();
}

