using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovement 
{
    //napraviti funkciju za aktivaciju kretanja i deakrivaciju
    //jer se ne moze pristup;ati direktno promenljivoj player razlikovace se

    void ActivateMovement();
    void DeactivateMovement();
}
