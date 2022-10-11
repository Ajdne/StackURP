using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    #region Singleton
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    private void Start()
    {
        
    }
        
    // Update is called once per frame
    void Update()
    {
        
    }
}
