using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{


    [Header("[Setting]")]
    [SerializeField] private GameData data;
    [SerializeField] private GameSetting setting;
    [SerializeField] private GameGraphic graphic;


    [Header("[Config]")]
    [SerializeField] private bool debugMode;
    [SerializeField] private bool runGameLocal;


    // [private]
    private bool isLoaded = false;


    // [properties]
    public bool IsLoaded { get => isLoaded; set => isLoaded = value; }
    public bool DebugMode { get => debugMode; set => debugMode = value; }
    public bool RunGameLocal { get => runGameLocal; set => runGameLocal = value; }


    public GameData Data { get => data; set => data = value; }
    public GameSetting Setting { get => setting; set => setting = value; }
    public GameGraphic Graphic { get => graphic; set => graphic = value; }




    #region UNITY
    public override void Awake()
    {
        base.Awake();

        if (isLoaded == true)
            return;

        Init();
        Application.targetFrameRate = 60;
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Time.timeScale = 1;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Time.timeScale = 2;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Time.timeScale = 3;
        }
    }
    #endregion




    private void Init()
    {
        data.Init();
        setting.Init();
        graphic.Init();
        isLoaded = true;
    }


    // public void DebugWireSphere(Vector3 position, Color color, float radius = 1.0f, float duration = 0)
    // {
    //     if (!debugMode)
    //         return;
    //     // DebugExtension.DebugWireSphere(position, color, radius, duration);
    // }


}




// God bless my code to be bug free 
//
//                       _oo0oo_
//                      o8888888o
//                      88" . "88
//                      (| -_- |)
//                      0\  =  /0
//                    ___/`---'\___
//                  .' \\|     |// '.
//                 / \\|||  :  |||// \
//                / _||||| -:- |||||- \
//               |   | \\\  -  /// |   |
//               | \_|  ''\---/''  |_/ |
//               \  .-\__  '-'  ___/-. /
//             ___'. .'  /--.--\  `. .'___
//          ."" '<  `.___\_<|>_/___.' >' "".
//         | | :  `- \`.;`\ _ /`;.`/ - ` : | |
//         \  \ `_.   \_ __\ /__ _/   .-` /  /
//     =====`-.____`.___ \_____/___.-`___.-'=====
//                       `=---='
//
//
//     ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//
//               佛祖保佑         永无BUG
//
