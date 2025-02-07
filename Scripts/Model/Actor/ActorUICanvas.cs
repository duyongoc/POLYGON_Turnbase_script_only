using UnityEngine;
using UnityEngine.UI;

public class ActorUICanvas : MonoBehaviour
{


    [Header("UI")]
    [SerializeField] private GameObject uiCanvas;
    [SerializeField] private Slider slider;
    [SerializeField] private Image sliderFill;



    #region UNITY
    // private void Start()
    // {
    // }

    private void LateUpdate()
    {
        if (Camera.main != null)
        {
            var camRotation = Camera.main.transform.rotation;
            transform.LookAt(transform.position + camRotation * Vector3.forward, camRotation * Vector3.up);
        }
    }
    #endregion



    public void Init(ERole role)
    {
        slider.value = 1;
        ShowUICanvas(true);

        if (role.Equals(ERole.BOT))
            sliderFill.color = Color.red;

        if (role.Equals(ERole.PLAYER))
            sliderFill.color = Color.blue;
    }


    public void SetSliderValue(float value)
    {
        slider.value = value;
    }


    public void ShowUICanvas(bool value)
    {
        uiCanvas.SetActive(value);
    }


}
