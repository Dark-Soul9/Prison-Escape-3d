using UnityEngine;

public class PlayerStates : MonoBehaviour
{
    public enum States { walking, sneaking, sprinting, idle}
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
    public void Walk()
    {
        currentState = States.walking;
        //Debug.Log("Normal");
    }
    public void Idle()
    {
        currentState = States.idle;
    }
}
