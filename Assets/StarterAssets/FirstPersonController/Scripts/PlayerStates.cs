using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates : MonoBehaviour
{
    public enum States { normal, sneaking, sprinting}
    public States currentState;

    public void Sprint()
    {
        currentState = States.sprinting;
        //Debug.Log("Sprinting");
    }
    public void Sneak()
    {
        currentState = States.sneaking;
        //Debug.Log("Sneaking");
    }
    public void ReturnToNormal()
    {
        currentState = States.normal;
        //Debug.Log("Normal");
    }
}
