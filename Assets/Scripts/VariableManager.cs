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

    //public const int maxIncrement = 5;
}
