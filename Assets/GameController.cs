using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    normal,
    retry
}
public class GameController : MonoBehaviour
{
    public GameState GameState = GameState.normal;

    public void TimeStop(bool stop)
    {
        if (stop)
        {
            Time.timeScale = 0; 
        }
        else
        {
            Time.timeScale = 1; 
        }
    }
}
