using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurnSystem : MonoBehaviour
{
    public event Action OnTurnChange;
    public static TurnSystem Instance;


    private int _turnNumber = 1;

    private bool _isPlayerTurn = true;



    private void Awake()
    {
        Instance = this;
    }

    public void nextTurn()
    {
        _turnNumber++;

        _isPlayerTurn = !_isPlayerTurn; 
        OnTurnChange?.Invoke();
    }


    public int GetTurnNumber()
    {
        return _turnNumber;
    }

    public bool IsPlayerTurn()
    {
        return _isPlayerTurn;
    }
}
