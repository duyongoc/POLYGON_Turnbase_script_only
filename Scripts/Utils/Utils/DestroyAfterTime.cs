using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{


    [Space]
    [SerializeField] private float timeDestroy = 2;


    #region UNITY
    private void Start()
    {
        Destroy(gameObject, timeDestroy);
    }

    // private void Update()
    // {
    // }
    #endregion

}
