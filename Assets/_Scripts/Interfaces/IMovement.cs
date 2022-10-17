using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovement
{
    public const float baseMoveSpeed = 7;

    // this is used in GM to respawn players
    void ActivateMovement();
    void DeactivateMovement();

    // set move speed for players
    void SetMovementSpeed(float value);
}
