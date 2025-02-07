using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMap : MonoBehaviour
{


    [Header("[Setting]")]
    [SerializeField] private List<GameObject> grounds;



    #region UNITY
    private void Start()
    {
        RandomGround();
    }

    // private void Update()
    // {
    // }
    #endregion



    private void RandomGround()
    {
        var rand = Random.Range(0, grounds.Count);
        grounds.ForEach(x=> x.gameObject.SetActive(false));
        grounds[rand].SetActive(true);
    }



}
