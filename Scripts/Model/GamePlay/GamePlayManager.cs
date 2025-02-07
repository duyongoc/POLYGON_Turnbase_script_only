using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GamePlayManager : BaseGamePlayManager
{


    [Header("STAGE")]
    public Stage CastedStage;
    public CharacterEntity ActiveCharacter;

    [Header("Formation/Spawning")]
    public Camera inputCamera;
    public GamePlayFormation playerFormation;
    public GamePlayFormation foeFormation;
    public Transform mapCenter;
    public float spawnOffset = 5f;

    [Header("Speed/Delay")]
    public float formationMoveSpeed = 5f;
    public float doActionMoveSpeed = 8f;
    public float actionDoneMoveSpeed = 10f;
    public float beforeMoveToNextWaveDelay = 2f;
    public float moveToNextWaveDelay = 1f;

    [Header("UI")]
    public Transform uiCharacterStatsContainer;
    public UICharacterActionManager uiCharacterActionManager;
    public UICharacterStats uiCharacterStatsPrefab;
    public static GamePlayManager Singleton;

    [Header("UI")]
    public int CurrentWave;
    public int MaxWave { get { return CastedStage.waves.Length; } }




    public Vector3 MapCenterPosition
    {
        get
        {
            if (mapCenter == null)
                return Vector3.zero;
            return mapCenter.position;
        }
    }


    #region UNITY
    private void Awake()
    {
        Singleton = this;
    }

    private void Start()
    {
        CastedStage = ConfigManager.Instance.StageCurrent;
        CurrentWave = 0;
        SetupFormation();
        StartCoroutine(StartGame());
    }


    private void Update()
    {
        if (IsAutoPlay != isAutoPlayDirty)
        {
            if (IsAutoPlay)
            {
                uiCharacterActionManager.Hide();
                if (ActiveCharacter != null)
                    ActiveCharacter.RandomAction();
            }
            isAutoPlayDirty = IsAutoPlay;
        }


        Time.timeScale = !isEnding && IsSpeedMultiply ? 2 : 1;
        if (Input.GetMouseButtonDown(0) && ActiveCharacter != null && ActiveCharacter.IsPlayerCharacter)
        {
            RaycastHit hitInfo;
            Ray ray = inputCamera.ScreenPointToRay(InputManager.MousePosition());

            if (!Physics.Raycast(ray, out hitInfo))
                return;

            var targetCharacter = hitInfo.collider.GetComponent<CharacterEntity>();
            if (targetCharacter != null)
            {
                if (ActiveCharacter.DoAction(targetCharacter))
                {
                    playerFormation.SetCharactersSelectable(false);
                    foeFormation.SetCharactersSelectable(false);
                }
            }
        }
    }
    #endregion




    private void SetupFormation()
    {
        // Setup ui
        uiCharacterActionManager.Hide();

        // Setup player formation
        playerFormation.isPlayerFormation = true;
        playerFormation.foeFormation = foeFormation;
        playerFormation.SetFormationCharacters_Char();

        // Setup foe formation
        foeFormation.ClearCharacters();
        foeFormation.isPlayerFormation = false;
        foeFormation.foeFormation = playerFormation;
    }



    private IEnumerator StartGame()
    {
        // yield return playerFormation.MoveCharactersToFormation(true);
        // environmentManager.isPause = false;

        // yield return playerFormation.ForceCharactersPlayMoving(moveToNextWaveDelay);
        // environmentManager.isPause = true;

        // yield return foeFormation.MoveCharactersToFormation(false);
        yield return null;
        NextWave();
        NewTurn();
    }


    public void NextWave()
    {
        StageFoe[] foes;
        CharacterItem[] characters;

        var wave = CastedStage.waves[CurrentWave];
        if (!wave.useRandomFoes && wave.foes.Length > 0)
            foes = wave.foes;
        else
            foes = CastedStage.RandomFoes().foes;


        characters = new CharacterItem[foes.Length];
        for (var i = 0; i < characters.Length; ++i)
        {
            characters[i] = foes[i].character;
        }

        // print("characters: " + characters.Length);
        if (characters.Length == 0)
            Debug.LogError("Missing Foes Data");

        foeFormation.SetCharacters_Char(characters);
        foeFormation.Revive();
        ++CurrentWave;
    }

    private IEnumerator MoveToNextWave()
    {
        foeFormation.ClearCharacters();
        yield return new WaitForSeconds(beforeMoveToNextWaveDelay);
        // playerFormation.SetActiveDeadCharacters(false);
        // environmentManager.isPause = false;

        // yield return playerFormation.ForceCharactersPlayMoving(moveToNextWaveDelay);
        // environmentManager.isPause = true;
        // playerFormation.SetActiveDeadCharacters(true);

        yield return foeFormation.MoveCharactersToFormation(false);
        NextWave();
        NewTurn();
    }


    public void NewTurn()
    {
        if (ActiveCharacter != null)
            ActiveCharacter.currentTimeCount = 0;

        var maxTime = int.MinValue;
        var characters = new List<BaseCharacterEntity>();
        CharacterEntity activatingCharacter = null;

        // print($"playerFormation: {playerFormation.Characters.Count}");
        // print($"foeFormation: {foeFormation.Characters.Count}");
        characters.AddRange(playerFormation.Characters);
        characters.AddRange(foeFormation.Characters);

        for (int i = 0; i < characters.Count; ++i)
        {
            CharacterEntity character = characters[i] as CharacterEntity;
            if (character != null)
            {
                if (character.Hp > 0)
                {
                    int spd = (int)character.GetTotalAttributes().spd;
                    if (spd <= 0)
                        spd = 1;

                    character.currentTimeCount += spd;
                    if (character.currentTimeCount > maxTime)
                    {
                        maxTime = character.currentTimeCount;
                        activatingCharacter = character;
                    }
                }
                else
                {
                    character.currentTimeCount = 0;
                }
            }
        }

        if (activatingCharacter == null)
        {
            print("activatingCharacter is null");
            return;
        }

        ActiveCharacter = activatingCharacter;
        ActiveCharacter.DecreaseBuffsTurn();
        ActiveCharacter.DecreaseSkillsTurn();
        ActiveCharacter.ResetStates();
        if (ActiveCharacter.Hp > 0)
        {
            if (ActiveCharacter.IsPlayerCharacter)
            {
                if (IsAutoPlay)
                    ActiveCharacter.RandomAction();
                else
                    uiCharacterActionManager.Show();
            }
            else
                ActiveCharacter.RandomAction();
        }
        else
            ActiveCharacter.NotifyEndAction();
    }

    /// <summary>
    /// This will be called by Character class to show target scopes or do action
    /// </summary>
    /// <param name="character"></param>
    public void ShowTargetScopesOrDoAction(CharacterEntity character)
    {
        var allyTeamFormation = character.IsPlayerCharacter ? playerFormation : foeFormation;
        var foeTeamFormation = !character.IsPlayerCharacter ? playerFormation : foeFormation;
        allyTeamFormation.SetCharactersSelectable(false);
        foeTeamFormation.SetCharactersSelectable(false);


        if (character.action == CharacterEntity.ACTION_ATTACK)
        {
            foeTeamFormation.SetCharactersSelectable(true);
        }
        else
        {
            switch (character.SelectedSkill.CastedSkill.usageScope)
            {
                case SkillUsageScope.Self:
                    character.selectable = true;
                    break;

                case SkillUsageScope.Ally:
                    allyTeamFormation.SetCharactersSelectable(true);
                    break;

                case SkillUsageScope.Enemy:
                    foeTeamFormation.SetCharactersSelectable(true);
                    break;

                case SkillUsageScope.All:
                    allyTeamFormation.SetCharactersSelectable(true);
                    foeTeamFormation.SetCharactersSelectable(true);
                    break;
            }
        }
    }


    public List<BaseCharacterEntity> GetAllies(CharacterEntity character)
    {
        if (character.IsPlayerCharacter)
            return playerFormation.Characters.Where(a => a.Hp > 0).ToList();
        else
            return foeFormation.Characters.Where(a => a.Hp > 0).ToList();
    }

    public List<BaseCharacterEntity> GetFoes(CharacterEntity character)
    {
        if (character.IsPlayerCharacter)
            return foeFormation.Characters.Where(a => a.Hp > 0).ToList();
        else
            return playerFormation.Characters.Where(a => a.Hp > 0).ToList();
    }


    public void NotifyEndAction(CharacterEntity character)
    {
        if (character != ActiveCharacter)
            return;

        if (!playerFormation.IsAnyCharacterAlive())
        {
            ActiveCharacter = null;
            StartCoroutine(LoseGameRoutine());
        }
        else if (!foeFormation.IsAnyCharacterAlive())
        {
            ActiveCharacter = null;
            if (CurrentWave >= CastedStage.waves.Length)
            {
                StartCoroutine(WinGameRoutine());
                return;
            }
            StartCoroutine(MoveToNextWave());
        }
        else
        {
            NewTurn();
        }
    }


    public override void OnRevive()
    {
        base.OnRevive();
        playerFormation.Revive();
        NewTurn();
    }

    public override int CountDeadCharacters()
    {
        return playerFormation.CountDeadCharacters();
    }



    protected IEnumerator LoseGameRoutine()
    {
        isEnding = true;
        yield return new WaitForSeconds(loseGameDelay);

        SoundManager.Instance.StopMusic();
        SoundManager.Instance.PlaySFX(SoundManager.Instance.SFX_BattleDefeat);
        ViewManager.Instance.ShowPopupStageResultLose(ViewManager.Instance.LoadSceneMenu);
    }


    protected IEnumerator WinGameRoutine()
    {
        isEnding = true;
        yield return new WaitForSeconds(winGameDelay);

        var config = ConfigManager.Instance;
        var stageData = config.GameStages.Find(x => x.key.Equals(config.StageCurrent.stageKey));
        uiCharacterStatsContainer.gameObject.SetActive(false);

        GameManager.Instance.Data.SaveStage(stageData.key);
        GameManager.Instance.Data.SaveCharacterByKey(stageData.rewards);

        SoundManager.Instance.StopMusic();
        SoundManager.Instance.PlaySFX(SoundManager.Instance.SFX_BattleWin);
        ViewManager.Instance.ShowPopupStageResultWin(stageData.rewards, ViewManager.Instance.LoadSceneMenu);
    }


    public void BackToMenu()
    {
        ViewManager.Instance.ShowPopupOkCancel("INFO", "Do you want to exit?", () =>
        {
            isEnding = true;
            SoundManager.Instance.PlaySoundMenu();
            ViewManager.Instance.LoadSceneMenu();
        });
    }


}
