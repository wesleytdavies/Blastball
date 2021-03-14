using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableManager : MonoBehaviour
{
    #region Singleton
    public static VariableManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    //public const int numPlayers = 6; //number of players
    //public const int maxIncrement = 5;
}
