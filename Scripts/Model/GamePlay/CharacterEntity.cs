﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(TargetingRigidbody))]
public class CharacterEntity : BaseCharacterEntity
{


    public const int ACTION_ATTACK = -100;
    public const string ANIM_KEY_HURT = "Hurt";
    public const string ANIM_KEY_SPEED = "Speed";
    public const string ANIM_KEY_IS_DEAD = "IsDead";
    public const string ANIM_KEY_DO_ACTION = "DoAction";
    public const string ANIM_KEY_ACTION_STATE = "ActionState";


    [Header("[Character]")]
    public int action;
    public int currentTimeCount;
    public CharacterEntity ActionTarget;
    public CharacterEntity targetCharacter;
    public UICharacterStats uiCharacterStats;
    public Vector3 targetPosition;
    public List<Damage> Damages = new List<Damage>();
    public bool IsDead;
    public bool IsShowModel;
    public bool IsDoingAction;
    public bool IsMovingToTarget;
    public bool isReachedTargetCharacter;
    public bool forcePlayMoving;
    public bool forceHideCharacterStats;
    public bool selectable;


    public Coroutine movingCoroutine;
    public CharacterSkill SelectedSkill { get { return Skills[action] as CharacterSkill; } }
    public GamePlayFormation CastedFormation { get { return Formation as GamePlayFormation; } }
    public bool IsActiveCharacter { get { return GamePlayManager.Singleton.ActiveCharacter == this; } }
    public bool IsPlayerCharacter { get { return Formation != null && CastedFormation.isPlayerFormation; } }




    #region Temp components
    private Rigidbody cacheRigidbody;
    private CapsuleCollider cacheCapsuleCollider;
    private TargetingRigidbody cacheTargetingRigidbody;


    public Rigidbody CacheRigidbody
    {
        get
        {
            if (cacheRigidbody == null)
                cacheRigidbody = GetComponent<Rigidbody>();
            return cacheRigidbody;
        }
    }
    public CapsuleCollider CacheCapsuleCollider
    {
        get
        {
            if (cacheCapsuleCollider == null)
                cacheCapsuleCollider = GetComponent<CapsuleCollider>();
            return cacheCapsuleCollider;
        }
    }
    public TargetingRigidbody CacheTargetingRigidbody
    {
        get
        {
            if (cacheTargetingRigidbody == null)
                cacheTargetingRigidbody = GetComponent<TargetingRigidbody>();
            return cacheTargetingRigidbody;
        }
    }
    #endregion



    #region Unity Functions
    protected override void Awake()
    {
        base.Awake();
        CacheCapsuleCollider.isTrigger = true;
        cacheAnimatorController = new AnimatorOverrideController(animatorController);
        cacheAnimator.runtimeAnimatorController = cacheAnimatorController;
    }

    private void Update()
    {
        if (Data == null)
        {
            // For show in viewers
            cacheAnimator.SetBool(ANIM_KEY_IS_DEAD, false);
            cacheAnimator.SetFloat(ANIM_KEY_SPEED, 0);
            return;
        }

        cacheAnimator.SetBool(ANIM_KEY_IS_DEAD, Hp <= 0);
        if (Hp > 0)
        {
            var moveSpeed = CacheRigidbody.velocity.magnitude;

            // Assume that character is moving by set moveSpeed = 1
            if (forcePlayMoving)
                moveSpeed = 1;

            cacheAnimator.SetFloat(ANIM_KEY_SPEED, moveSpeed);
            if (uiCharacterStats != null)
            {
                if (forceHideCharacterStats)
                    uiCharacterStats.Hide();
                else
                    uiCharacterStats.Show();
            }
        }
        else
        {
            if (uiCharacterStats != null)
                uiCharacterStats.Hide();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (targetCharacter != null && targetCharacter == other.GetComponent<CharacterEntity>())
            isReachedTargetCharacter = true;
    }

    private void OnDestroy()
    {
        if (uiCharacterStats != null)
            Destroy(uiCharacterStats.gameObject);
    }
    #endregion




    #region Damage/Dead/Revive/Turn/Buff
    public void Attack(CharacterEntity target, float pAtkRate = 1f, float mAtkRate = 1f, int hitCount = 1, int fixDamage = 0)
    {
        if (target == null)
            return;

        var attribute = GetTotalAttributes();
        var pAtk = Mathf.CeilToInt(attribute.pAtk * pAtkRate);
        var mAtk = Mathf.CeilToInt(attribute.mAtk * mAtkRate);
        target.ReceiveDamage(pAtk, mAtk, (int)attribute.acc, attribute.critChance, attribute.critDamageRate, hitCount, fixDamage);
    }


    public void Attack(CharacterEntity target, AttackAnimationData data, float pAtkRate = 1f, float mAtkRate = 1f, int hitCount = 1, int fixDamage = 0)
    {
        // play sound
        data.PlaySoundAttack();


        // close attack
        var damage = (Damage)data.GetDamage();
        if (damage == null)
        {
            Attack(target, pAtkRate, mAtkRate, hitCount, fixDamage);
            return;
        }

        // range attack
        var dmgRange = Instantiate(damage, damageContainer.position, damageContainer.rotation);
        dmgRange.Setup(this, target, pAtkRate, mAtkRate, hitCount, fixDamage);
        dmgRange.SpawnEffect(data.hitProjectile, data.hitEffect, data.soundHit);
    }



    public void ReceiveDamage(int pAtk, int mAtk, int acc, float critChance, float critDamageRate, int hitCount = 1, int fixDamage = 0)
    {
        if (hitCount <= 0)
            hitCount = 1;

        var attributes = GetTotalAttributes();
        var pDmg = pAtk - attributes.pDef;
        var mDmg = mAtk - attributes.mDef;

        if (pDmg < 0)
            pDmg = 0;
        if (mDmg < 0)
            mDmg = 0;

        var totalDmg = pDmg + mDmg;
        var isCritical = false;
        var isBlock = false;

        totalDmg += Mathf.CeilToInt(totalDmg) + fixDamage;

        // Critical occurs
        if (Random.value <= critChance)
        {
            totalDmg = Mathf.CeilToInt(totalDmg * critDamageRate);
            isCritical = true;
        }
        // Block occurs
        if (Random.value <= attributes.blockChance)
        {
            totalDmg = Mathf.CeilToInt(totalDmg / attributes.blockDamageRate);
            isBlock = true;
        }

        var hitChance = 0f;
        if (acc > 0)
            hitChance = acc / attributes.eva;

        // cannot evade, receive damage
        if (hitChance < 0 || Random.value > hitChance)
        {
            GamePlayManager.Singleton.SpawnMissText(this);
        }
        else
        {
            if (isBlock)
                GamePlayManager.Singleton.SpawnBlockText((int)totalDmg, this);
            else if (isCritical)
                GamePlayManager.Singleton.SpawnCriticalText((int)totalDmg, this);
            else
                GamePlayManager.Singleton.SpawnDamageText((int)totalDmg, this);

            Hp -= (int)totalDmg;
        }

        // play hurt animation
        cacheAnimator.ResetTrigger(ANIM_KEY_HURT);
        cacheAnimator.SetTrigger(ANIM_KEY_HURT);
    }


    public override void Dead()
    {
        base.Dead();
        ClearActionState();
        IsDead = true;
    }


    public void DecreaseBuffsTurn()
    {
        var keys = new List<string>(Buffs.Keys);
        for (var i = keys.Count - 1; i >= 0; --i)
        {
            var key = keys[i];
            if (!Buffs.ContainsKey(key))
                continue;

            var buff = Buffs[key] as CharacterBuff;
            buff.IncreaseTurnsCount();
            if (buff.IsEnd())
            {
                buff.BuffRemove();
                Buffs.Remove(key);
            }
        }
    }


    public void DecreaseSkillsTurn()
    {
        for (var i = Skills.Count - 1; i >= 0; --i)
        {
            var skill = Skills[i] as CharacterSkill;
            skill.IncreaseTurnsCount();
        }
    }
    #endregion



    #region Movement/Actions
    public Coroutine MoveTo(Vector3 position, float speed)
    {
        if (IsMovingToTarget)
            StopCoroutine(movingCoroutine);

        IsMovingToTarget = true;
        isReachedTargetCharacter = false;
        targetPosition = position;
        movingCoroutine = StartCoroutine(MoveToRoutine(position, speed));
        return movingCoroutine;
    }


    private IEnumerator MoveToRoutine(Vector3 position, float speed)
    {
        CacheTargetingRigidbody.StartMove(position, speed);
        while (true)
        {
            if (!CacheTargetingRigidbody.IsMoving || isReachedTargetCharacter)
            {
                IsMovingToTarget = false;
                CacheTargetingRigidbody.StopMove();
                if (targetCharacter == null)
                {
                    TurnToEnemyFormation();
                    TempTransform.position = targetPosition;
                }
                targetCharacter = null;
                break;
            }
            yield return 0;
        }
    }


    public Coroutine MoveTo(CharacterEntity character, float speed)
    {
        targetCharacter = character;
        return MoveTo(character.TempTransform.position, speed);
    }


    public void TurnToEnemyFormation()
    {
        Quaternion headingRotation;
        if (CastedFormation.TryGetHeadingToFoeRotation(out headingRotation))
            TempTransform.rotation = headingRotation;
    }


    public void ClearActionState()
    {
        cacheAnimator.SetInteger(ANIM_KEY_ACTION_STATE, 0);
        cacheAnimator.SetBool(ANIM_KEY_DO_ACTION, false);
    }


    public bool SetAction(int action)
    {
        if (action == ACTION_ATTACK || (action >= 0 && action < Skills.Count))
        {
            this.action = action;
            GamePlayManager.Singleton.ShowTargetScopesOrDoAction(this);
            return true;
        }
        return false;
    }



    public bool DoAction(CharacterEntity target)
    {
        if (target == null || target.Hp <= 0)
            return false;

        if (action == ACTION_ATTACK)
        {
            // Cannot attack self or same team character
            if (target == this || IsSameTeamWith(target))
                return false;
        }
        else
        {
            if (SelectedSkill == null || !SelectedSkill.IsReady())
                return false;

            switch (SelectedSkill.CastedSkill.usageScope)
            {
                case SkillUsageScope.Self:
                    if (target != this)
                        return false;
                    break;

                case SkillUsageScope.Enemy:
                    if (target == this || IsSameTeamWith(target))
                        return false;
                    break;

                case SkillUsageScope.Ally:
                    if (!IsSameTeamWith(target))
                        return false;
                    break;
            }
        }

        ActionTarget = target;
        DoAction();
        return true;
    }


    public void RandomAction()
    {
        Dictionary<int, int> actions = new Dictionary<int, int>(); // dictionary of actionId, weight
        actions.Add(ACTION_ATTACK, 5);

        for (var i = 0; i < Skills.Count; ++i)
        {
            var skill = Skills[i] as CharacterSkill;
            if (skill == null || !skill.IsReady())
                continue;

            actions.Add(i, 5);
        }


        // Random Target
        action = WeightedRandomizer.From(actions).TakeOne();
        if (action == ACTION_ATTACK)
        {
            var foes = GamePlayManager.Singleton.GetFoes(this);
            Random.InitState(System.DateTime.Now.Millisecond);
            ActionTarget = foes[Random.Range(0, foes.Count - 1)] as CharacterEntity;
        }
        else
        {
            switch (SelectedSkill.CastedSkill.usageScope)
            {
                case SkillUsageScope.Enemy:
                    var foes = GamePlayManager.Singleton.GetFoes(this);
                    Random.InitState(System.DateTime.Now.Millisecond);
                    ActionTarget = foes[Random.Range(0, foes.Count)] as CharacterEntity;
                    break;

                case SkillUsageScope.Ally:
                    var allies = GamePlayManager.Singleton.GetAllies(this);
                    Random.InitState(System.DateTime.Now.Millisecond);
                    ActionTarget = allies[Random.Range(0, allies.Count)] as CharacterEntity;
                    break;

                default:
                    ActionTarget = null;
                    break;
            }
        }
        DoAction();
    }


    private void DoAction()
    {
        if (IsDoingAction)
            return;

        if (action == ACTION_ATTACK)
        {
            StartCoroutine(DoAttackActionRoutine());
        }
        else
        {
            SelectedSkill.OnUseSkill();
            StartCoroutine(DoSkillActionRoutine());
        }
    }


    private IEnumerator DoAttackActionRoutine()
    {
        IsDoingAction = true;
        AttackAnimationData attackAnimation = null;

        if (AttackAnimations.Count > 0)
        {
            attackAnimation = AttackAnimations[Random.Range(0, AttackAnimations.Count - 1)] as AttackAnimationData;
        }

        // move to target character
        if (!attackAnimation.GetIsRangeAttack())
        {
            yield return MoveTo(ActionTarget, GamePlayManager.Singleton.doActionMoveSpeed);
        }

        // play attack animation
        if (attackAnimation != null)
        {
            switch (attackAnimation.type)
            {
                case AnimationDataType.ChangeAnimationByState:
                    cacheAnimator.SetInteger(ANIM_KEY_ACTION_STATE, attackAnimation.GetAnimationActionState());
                    break;

                    // case AnimationDataType.ChangeAnimationByClip:
                    //     ChangeActionClip(attackAnimation.GetAnimationClip());
                    //     cacheAnimator.SetBool(ANIM_KEY_DO_ACTION, true);
                    //     break;
            }
        }

        yield return new WaitForSeconds(attackAnimation.GetHitDuration());

        // apply damage
        Attack(ActionTarget, attackAnimation);

        // wait damages done
        while (Damages.Count > 0)
        {
            yield return 0;
        }


        // delay end action attack
        yield return new WaitForSeconds(CONST.DELAY_ATTACK);
        ClearActionState();


        yield return MoveTo(Container.position, GamePlayManager.Singleton.actionDoneMoveSpeed);
        NotifyEndAction();
        IsDoingAction = false;
    }


    private IEnumerator DoSkillActionRoutine()
    {
        IsDoingAction = true;
        var skill = SelectedSkill.CastedSkill;
        var skillCastAnimation = skill.castAnimation as SkillCastAnimationData;

        // cast
        if (skillCastAnimation.GetCastAtMapCenter())
            yield return MoveTo(GamePlayManager.Singleton.MapCenterPosition, GamePlayManager.Singleton.doActionMoveSpeed);

        var effects = new List<GameEffect>();
        var castEffects = skillCastAnimation.GetCastEffects();
        if (castEffects != null)
            effects.AddRange(castEffects.InstantiatesTo(this));


        // play cast animation
        if (skillCastAnimation != null)
        {
            switch (skillCastAnimation.type)
            {
                case AnimationDataType.ChangeAnimationByState:
                    cacheAnimator.SetInteger(ANIM_KEY_ACTION_STATE, skillCastAnimation.GetAnimationActionState());
                    break;

                    // case AnimationDataType.ChangeAnimationByClip:
                    //     ChangeActionClip(skillCastAnimation.GetAnimationClip());
                    //     cacheAnimator.SetBool(ANIM_KEY_DO_ACTION, true);
                    //     break;
            }
        }

        yield return new WaitForSeconds(skillCastAnimation.GetAnimationDuration());
        ClearActionState();

        foreach (var effect in effects)
        {
            effect.DestroyEffect();
        }
        effects.Clear();


        // buffs
        yield return StartCoroutine(ApplyBuffsRoutine());

        // attacks
        yield return StartCoroutine(SkillAttackRoutine());

        // move back to formation
        yield return MoveTo(Container.position, GamePlayManager.Singleton.actionDoneMoveSpeed);

        NotifyEndAction();
        IsDoingAction = false;
    }


    private IEnumerator ApplyBuffsRoutine()
    {
        var level = SelectedSkill.Level;
        var skill = SelectedSkill.CastedSkill;
        for (var i = 0; i < skill.buffs.Length; ++i)
        {
            var buff = skill.buffs[i];
            if (buff == null)
                continue;

            var allies = GamePlayManager.Singleton.GetAllies(this);
            var foes = GamePlayManager.Singleton.GetFoes(this);
            if (buff.RandomToApply(level))
            {
                // Apply buffs to selected targets
                switch (buff.buffScope)
                {
                    case BuffScope.SelectedTarget:
                    case BuffScope.SelectedAndOneRandomTargets:
                    case BuffScope.SelectedAndTwoRandomTargets:
                    case BuffScope.SelectedAndThreeRandomTargets:
                        ActionTarget.ApplyBuff(this, level, skill, i);
                        break;
                }

                int randomAllyCount = 0;
                int randomFoeCount = 0;
                // Buff scope
                switch (buff.buffScope)
                {
                    case BuffScope.Self:
                        ApplyBuff(this, level, skill, i);
                        continue;

                    case BuffScope.SelectedAndOneRandomTargets:
                        if (ActionTarget.IsSameTeamWith(this))
                            randomAllyCount = 1;
                        else if (!ActionTarget.IsSameTeamWith(this))
                            randomFoeCount = 1;
                        break;

                    case BuffScope.SelectedAndTwoRandomTargets:
                        if (ActionTarget.IsSameTeamWith(this))
                            randomAllyCount = 2;
                        else if (!ActionTarget.IsSameTeamWith(this))
                            randomFoeCount = 2;
                        break;

                    case BuffScope.SelectedAndThreeRandomTargets:
                        if (ActionTarget.IsSameTeamWith(this))
                            randomAllyCount = 3;
                        else if (!ActionTarget.IsSameTeamWith(this))
                            randomFoeCount = 3;
                        break;

                    case BuffScope.OneRandomAlly:
                        randomAllyCount = 1;
                        break;

                    case BuffScope.TwoRandomAllies:
                        randomAllyCount = 2;
                        break;

                    case BuffScope.ThreeRandomAllies:
                        randomAllyCount = 3;
                        break;

                    case BuffScope.FourRandomAllies:
                        randomAllyCount = 4;
                        break;

                    case BuffScope.AllAllies:
                        randomAllyCount = allies.Count;
                        break;

                    case BuffScope.OneRandomEnemy:
                        randomFoeCount = 1;
                        break;

                    case BuffScope.TwoRandomEnemies:
                        randomFoeCount = 2;
                        break;

                    case BuffScope.ThreeRandomEnemies:
                        randomFoeCount = 3;
                        break;

                    case BuffScope.FourRandomEnemies:
                        randomFoeCount = 4;
                        break;

                    case BuffScope.AllEnemies:
                        randomFoeCount = foes.Count;
                        break;

                    case BuffScope.All:
                        randomAllyCount = allies.Count;
                        randomFoeCount = foes.Count;
                        break;
                }

                // End buff scope
                // Don't apply buffs to character that already applied
                if (randomAllyCount > 0)
                {
                    allies.Remove(ActionTarget);
                    while (allies.Count > 0 && randomAllyCount > 0)
                    {
                        Random.InitState(System.DateTime.Now.Millisecond);
                        var randomIndex = Random.Range(0, allies.Count - 1);
                        var applyBuffTarget = allies[randomIndex];
                        applyBuffTarget.ApplyBuff(this, level, skill, i);
                        allies.RemoveAt(randomIndex);
                        --randomAllyCount;
                    }
                }

                // Don't apply buffs to character that already applied
                if (randomFoeCount > 0)
                {
                    foes.Remove(ActionTarget);
                    while (foes.Count > 0 && randomFoeCount > 0)
                    {
                        Random.InitState(System.DateTime.Now.Millisecond);
                        var randomIndex = Random.Range(0, foes.Count - 1);
                        var applyBuffTarget = foes[randomIndex];
                        applyBuffTarget.ApplyBuff(this, level, skill, i);
                        foes.RemoveAt(randomIndex);
                        --randomFoeCount;
                    }
                }
            }
        }
        yield return 0;
    }


    private IEnumerator SkillAttackRoutine()
    {
        var level = SelectedSkill.Level;
        var skill = SelectedSkill.CastedSkill;
        if (skill.attacks.Length > 0)
        {
            var isAlreadyReachedTarget = false;
            foreach (var attack in skill.attacks)
            {
                var foes = GamePlayManager.Singleton.GetFoes(this);
                var attackDamage = attack.attackDamage;
                var attackAnimation = attack.attackAnimation as AttackAnimationData;
                LookAtZeroCurrentEnemy();


                // Move to target character
                if (!attackAnimation.GetIsRangeAttack() && !isAlreadyReachedTarget)
                {
                    yield return MoveTo(ActionTarget, GamePlayManager.Singleton.doActionMoveSpeed);
                    isAlreadyReachedTarget = true;
                }

                // Play attack animation
                if (attackAnimation != null)
                {
                    switch (attackAnimation.type)
                    {
                        case AnimationDataType.ChangeAnimationByState:
                            cacheAnimator.SetInteger(ANIM_KEY_ACTION_STATE, attackAnimation.GetAnimationActionState());
                            break;

                            // case AnimationDataType.ChangeAnimationByClip:
                            //     ChangeActionClip(attackAnimation.GetAnimationClip());
                            //     cacheAnimator.SetBool(ANIM_KEY_DO_ACTION, true);
                            //     break;
                    }
                }
                yield return new WaitForSeconds(attackAnimation.GetHitDuration());


                // Apply damage
                // Attack to selected target
                switch (attack.attackScope)
                {
                    case AttackScope.SelectedTarget:
                    case AttackScope.SelectedAndOneRandomTargets:
                    case AttackScope.SelectedAndTwoRandomTargets:
                    case AttackScope.SelectedAndThreeRandomTargets:
                        Attack(ActionTarget, attackAnimation, attackDamage.GetPAtkDamageRate(level), attackDamage.GetMAtkDamageRate(level), attackDamage.hitCount, (int)attackDamage.GetFixDamage(level));
                        break;
                }

                // Attack to random targets - Attack scope
                int randomFoeCount = 0;
                switch (attack.attackScope)
                {
                    case AttackScope.SelectedAndOneRandomTargets:
                    case AttackScope.OneRandomEnemy:
                        randomFoeCount = 1;
                        break;
                    case AttackScope.SelectedAndTwoRandomTargets:
                    case AttackScope.TwoRandomEnemies:
                        randomFoeCount = 2;
                        break;
                    case AttackScope.SelectedAndThreeRandomTargets:
                    case AttackScope.ThreeRandomEnemies:
                        randomFoeCount = 3;
                        break;
                    case AttackScope.FourRandomEnemies:
                        randomFoeCount = 4;
                        break;
                    case AttackScope.AllEnemies:
                        randomFoeCount = foes.Count;
                        break;
                }

                // end attack scope
                while (foes.Count > 0 && randomFoeCount > 0)
                {
                    Random.InitState(System.DateTime.Now.Millisecond);
                    var randomIndex = Random.Range(0, foes.Count - 1);
                    var attackingTarget = foes[randomIndex] as CharacterEntity;

                    Attack(attackingTarget, attackAnimation, attackDamage.GetPAtkDamageRate(level), attackDamage.GetMAtkDamageRate(level), attackDamage.hitCount, (int)attackDamage.GetFixDamage(level));
                    foes.RemoveAt(randomIndex);
                    --randomFoeCount;
                }


                // delay end action attack
                yield return new WaitForSeconds(CONST.DELAY_ATTACK);
                ClearActionState();
                ResetLookAt();

                yield return 0;
            }

            // end attack loop - wait damages done
            while (Damages.Count > 0)
            {
                yield return 0;
            }
        }
    }

    public void ResetStates()
    {
        action = ACTION_ATTACK;
        ActionTarget = null;
        IsDoingAction = false;
    }

    public void NotifyEndAction()
    {
        GamePlayManager.Singleton.NotifyEndAction(this);
    }
    #endregion



    public void LookAtZeroCurrentEnemy()
    {
        if (ActionTarget == null)
            return;

        var lookPos = ActionTarget.transform.position - transform.position;
        var lookRot = Quaternion.LookRotation(lookPos, Vector3.up);
        transform.rotation = Quaternion.Euler(0, lookRot.eulerAngles.y, 0);
    }


    public void ResetLookAt()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }



    public void ChangeActionClip(AnimationClip clip)
    {
        cacheAnimatorController[ANIM_ACTION_STATE] = clip;
    }



    #region Misc
    public override void SetFormation(BaseGamePlayFormation formation, int position, Transform container)
    {
        if (container == null)
            return;

        base.SetFormation(formation, position, container);

        Quaternion headingRotation;
        if (CastedFormation.TryGetHeadingToFoeRotation(out headingRotation))
        {
            TempTransform.rotation = headingRotation;
            // if (GamePlayManager.Singleton != null)
            //     TempTransform.position -= GamePlayManager.Singleton.spawnOffset * TempTransform.forward;
        }
    }


    public bool IsSameTeamWith(CharacterEntity target)
    {
        return target != null && Formation == target.Formation;
    }

    public override BaseCharacterSkill NewSkill(int level, BaseSkill skill)
    {
        return new CharacterSkill(level, skill);
    }

    public override BaseCharacterBuff NewBuff(int level, BaseSkill skill, int buffIndex, BaseCharacterEntity giver, BaseCharacterEntity receiver)
    {
        return new CharacterBuff(level, skill, buffIndex, giver, receiver);
    }
    #endregion


}
