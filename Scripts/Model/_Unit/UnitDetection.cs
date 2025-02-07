using UnityEngine;

public class UnitDetection : MonoBehaviour
{


    [SerializeField] private UnitBase owner;


    #region UNITY
    // private void Start()
    // {
    // }

    private void FixedUpdate()
    {
        for (int i = owner.Detected.Count - 1; i >= 0; i--)
        {
            var enemy = owner.Detected[i];
            if (enemy == null || enemy.IsDead)
            // if (enemy == null || enemy.IsDead || enemy.IsDownOnGround)
            {
                owner.Detected.RemoveAt(i);
            }
        }
    }
    #endregion



    private void OnTriggerEnter(Collider other)
    {
        var actor = other.GetComponent<UnitBase>();
        if (actor == null)
            return;

        // incase owner and its detected same type bot or player -> skip it
        if (owner.Role == actor.Role)
            return;

        // // check the list if we can add to enemy list or not
        // foreach (var enemy in owner.EnemiesClass)
        // {
        //     // if the actor's character type exist in enemy list -> add it to list
        //     if (enemy == EClassEntity.All || enemy == actor.EntityClass)
        //     {
        //     }
        // }

        owner.Detected.Add(actor);
    }


    private void OnTriggerExit(Collider other)
    {
        owner.Detected.Remove(other.GetComponent<UnitBase>());
    }

}
