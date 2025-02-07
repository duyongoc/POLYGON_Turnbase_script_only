using System.Collections.Generic;
using UnityEngine;

public class CameraBattle : MonoBehaviour
{


    [Header("[Setting]")]
    [SerializeField] private ActorBase actorMain;
    [SerializeField] private List<ActorBase> actorList;

    [Header("[Param]")]
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothSpeed = 20;
    [SerializeField] private float rotationSpeed = 5;
    [SerializeField] private bool followRotation = true;




    #region UNITY
    // private void Start()
    // {
    // }

    private void Update()
    {
        if (MapController.Instance.IsGameFinished)
            return;

        if (actorMain == null || actorMain.IsDead)
            return;

        OnUpdate();
    }

    private void LateUpdate()
    {
        if (actorMain == null || actorMain.IsDead)
            return;

        OnUpdateLate();
        OnUpdateFader();
    }
    #endregion




    public void Init(ActorBase actor, List<ActorBase> list)
    {
        actorMain = actor;
        actorList = list;

        var indexPosition = 0;
        var positionList = GetPositionListAround(actorMain.transform.position, 1.5f, actorList.Count);
        foreach (var player in actorList)
        {
            if (player.name.Equals(actorMain.name))
                continue;

            player.transform.position = positionList[indexPosition];
            indexPosition = (indexPosition + 1) % positionList.Count;
        }
    }


    private void OnUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var mousePosition = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mousePosition, out RaycastHit hit, 300, LayerMask.GetMask("Ground")))
            {
                // if (playerList.Count == 1)
                actorMain.SetMove(hit.point);

                var indexPosition = 0;
                var positionList = GetPositionListAround(hit.point, 1.35f, actorList.Count);
                foreach (var player in actorList)
                {
                    if (player.name.Equals(actorMain.name))
                        continue;

                    player.SetMove(positionList[indexPosition]);
                    indexPosition = (indexPosition + 1) % positionList.Count;
                }
            }
        }
    }


    private void OnUpdateLate()
    {
        var desiredPosition = actorMain.transform.position + offset;
        var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        if (followRotation)
        {
            var desiredRotation = Quaternion.LookRotation(actorMain.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            transform.LookAt(actorMain.transform);
        }
    }


    private void OnUpdateFader()
    {
        if (actorMain != null)
        {
            var dir = actorMain.transform.position - transform.position;
            var ray = new Ray(transform.position, dir);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider == null)
                    return;

                var fader = hit.collider.gameObject.GetComponent<ObjectFader>();
                if (hit.collider.gameObject == actorMain)
                {
                    fader.DoFade = false;
                }
                else
                {
                    if (fader != null)
                    {
                        fader.DoFade = true;
                    }
                }
            }
        }
    }


    private List<Vector3> GetPositionListAround(Vector3 startPosition, float distance, int positionCount)
    {
        var positionList = new List<Vector3>();
        for (int i = 0; i < positionCount; i++)
        {
            var angle = i * (360f / positionCount);
            var dir = ApplyRotationToVector(new Vector3(1, 0, 0), angle);
            var position = startPosition + dir * distance;
            positionList.Add(position);
        }
        return positionList;
    }


    private Vector3 ApplyRotationToVector(Vector3 vec, float angle)
    {
        return Quaternion.Euler(0, angle, 0) * vec;
    }


}