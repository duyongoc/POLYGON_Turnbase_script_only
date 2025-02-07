using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class UITextDamage : MonoBehaviour
{


    [Header("Setting")]
    [SerializeField] private TMP_Text txtDamage;
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float timeDestroy = 1.5f;



    #region UNITY
    private void Start()
    {
        Destroy(gameObject, timeDestroy);
    }

    private void Update()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);

        if (Camera.main != null)
        {
            var camRotation = Camera.main.transform.rotation;
            transform.LookAt(transform.position + camRotation * Vector3.forward, camRotation * Vector3.up);
        }
    }

    // private void LateUpdate()
    // {
    // }
    #endregion




    public void SetText(float damage, Color color, float scale = 1)
    {
        var strDamage = color == Color.red ? $"-{(int)damage}" : $"+{(int)damage}";
        txtDamage.text = strDamage;
        txtDamage.color = color;
        transform.localScale *= scale;
    }
   
   
    public void SetText(string content, Color color, float scale = 1)
    {
        txtDamage.text = content;
        txtDamage.color = color;
        transform.localScale *= scale;
    }


}
