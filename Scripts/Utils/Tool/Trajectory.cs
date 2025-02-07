using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Trajectory : MonoBehaviour
{

    // launch variables
    [SerializeField] private Rigidbody rigid;
    [SerializeField] private Transform tranTarget;
    [SerializeField] private Transform tranOrigin;
    [Range(20.0f, 75.0f)] public float launchAngle;

    // state
    private bool bTargetReady;
    // private bool bTouchingGround;

    // cache
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    //-----------------------------------------------------------------------------------------------



    // Use this for initialization
    private void Start()
    {
        // rigid = GetComponent<Rigidbody>();
        // bTargetReady = false;
        // bTouchingGround = true;
        // initialPosition = transform.position;
        // initialRotation = transform.rotation;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!bTargetReady)
        {
            transform.localRotation = Quaternion.LookRotation(new Vector3(rigid.velocity.x, rigid.velocity.y, rigid.velocity.z)) * initialRotation;
            return;
        }

        Launch();
    }


    // returns the distance between the red dot and the TargetObject's y-position
    // this is a very little offset considered the ranges in this demo so it shouldn't make a big difference.
    // however, if this code is tested on smaller values, the lack of this offset might introduce errors.
    // to be technically accurate, consider using this offset together with the target platform's y-position.
    // resets the projectile to its initial position
    private void ResetToInitialState()
    {
        rigid.velocity = Vector3.zero;
        transform.SetPositionAndRotation(initialPosition, initialRotation);
        bTargetReady = false;
    }


    // launches the object towards the TargetObject with a given LaunchAngle
    private void Launch()
    {
        // think of it as top-down view of vectors: 
        //   we don't care about the y-component(height) of the initial and target position.
        Vector3 projectileXZPos = new Vector3(tranOrigin.position.x, 0.0f, tranOrigin.position.z);
        Vector3 targetXZPos = new Vector3(tranTarget.position.x, 0.0f, tranTarget.position.z);
        // print("targetXZPos:  " + targetXZPos);

        // rotate the object to face the target
        transform.LookAt(targetXZPos);

        // shorthands for the formula
        float R = Vector3.Distance(projectileXZPos, targetXZPos);
        // print("R: " + R);
        // if (R > 1)
        //     R -= -1;

        float G = Physics.gravity.y;
        float tanAlpha = Mathf.Tan(launchAngle * Mathf.Deg2Rad);
        float H = tranTarget.position.y - transform.position.y;

        // calculate the local space components of the velocity 
        // required to land the projectile on the target object 
        float Vz = Mathf.Sqrt(G * R * R / (2.0f * (H - R * tanAlpha)));
        float Vy = tanAlpha * Vz;

        // create the velocity vector in local space and get it in global space
        Vector3 localVelocity = new Vector3(0f, Vy, Vz);
        Vector3 globalVelocity = transform.TransformDirection(localVelocity);

        // launch the object by setting its initial velocity and flipping its state
        rigid.velocity = globalVelocity;
        bTargetReady = false;
    }
    

    // Sets a random target around the object based on the TargetRadius
    public void SetTarget(Transform newTarget, Transform origin)
    {
        bTargetReady = false;
        // bTouchingGround = true;
        initialPosition = origin.position;
        initialRotation = origin.localRotation;

        tranTarget = newTarget;
        tranOrigin = origin;
        bTargetReady = true;
    }


}
