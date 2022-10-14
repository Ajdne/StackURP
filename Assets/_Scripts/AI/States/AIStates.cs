using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIStates : MonoBehaviour
{
    public abstract AIStates RunCurrentState();
}
