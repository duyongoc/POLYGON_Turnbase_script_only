using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class UIModelRotator : MonoBehaviour
{


    [Header("[Setting]")]
    [SerializeField] private Transform tranModel;
    [SerializeField] private CharacterEntity modelCharacter;


    [Header("[Param]")]
    [SerializeField] private float rotateSpeed = 5f;
    [SerializeField] private bool autoRotate = true;
    [SerializeField] private bool isRotating = false;
    [SerializeField] private bool isClicked = false;


    // [private]
    private Vector3 _rotation;
    private Vector3 _mouseOffset;
    private Vector3 _mouseReference;


    // [properties]
    public CharacterEntity ModelCharacter { get => modelCharacter; }



    #region UNITY
    // private void Start()
    // {
    // }

    private void Update()
    {
        UpdateCharacter();
    }
    #endregion




    public void LoadModel(string key)
    {
        if (string.IsNullOrEmpty(key))
            return;

        ClearCache();

        var model = ConfigManager.Instance.GetCharacterModelByKey(key);
        modelCharacter = Instantiate(model, tranModel);
        modelCharacter.IsShowModel = true;
        tranModel.localRotation = Quaternion.Euler(0, 180, 0);
    }


    private void UpdateCharacter()
    {
        if (!isClicked)
        {
            return;
        }

        if (!autoRotate)
        {
            RotateModel();
            return;
        }

        if (!isRotating)
        {
            AutoRotate();
            return;
        }

        // rotating 
        RotateModel();
    }


    private void AutoRotate()
    {
        tranModel.Rotate(Vector2.down * rotateSpeed * Time.deltaTime);
    }


    private void RotateModel()
    {
        _mouseOffset = Input.mousePosition - _mouseReference;
        _rotation.y = -500f * (_mouseOffset.x / Screen.height);
        _mouseReference = Input.mousePosition;
        tranModel.Rotate(_rotation);
    }


    public void OnPointerDown()
    {
        isClicked = true;
        isRotating = true;
        autoRotate = false;
        _mouseReference = Input.mousePosition;
    }


    public void OnPointerUp()
    {
        isClicked = false;
        isRotating = false;
        autoRotate = true;
    }


    public void Show()
    {
        gameObject.SetActive(true);
    }


    public void Hide()
    {
        gameObject.SetActive(false);
    }


    public void ClearCache()
    {
        if (modelCharacter != null)
        {
            Destroy(modelCharacter.gameObject);
        }
    }


}
