using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public enum STATE
{
    IDLE,
    BATTLE,
    HIT,
    MOVE
}

public enum ATTACK_TYPE
{
    NORMAL,
    SKILLATK,
    ULTIMATEATK
}


public interface IStateMachine
{
    State CurState { get; }
    Animator Animator { get; }
    object GetOwner();
    void SetState(STATE state);
}

public abstract class State
{
    public IStateMachine sm;
    public virtual void Init(IStateMachine sm)
    {
        this.sm = sm;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}

public abstract class PlayerState : State
{
    public Playerable player;
}

public abstract class MonsterState : State
{
    public Monster monster;
}


public class PlayerIdleState : PlayerState
{
    public override void Init(IStateMachine sm)
    {
        base.Init(sm);
        player = (Playerable)this.sm.GetOwner();
    }
    public override void Enter()
    {

    }

    public override void Exit()
    {
    }

    public override void Update()
    {
    }
}

public class PlayerBattleState : PlayerState
{
    public override void Init(IStateMachine sm)
    {
        base.Init(sm);
        player = (Playerable)this.sm.GetOwner();
    }
    public override void Enter()
    {
    }

    public override void Exit()
    {
    }

    public override void Update()
    {
    }
}

public class PlayerHitState : PlayerState
{
    public override void Init(IStateMachine sm)
    {
        base.Init(sm);
        player = (Playerable)this.sm.GetOwner();
    }
    public override void Enter()
    {
    }
    public override void Update()
    {
        sm.Animator.SetBool("Hit", true);
    }

    public override void Exit()
    {
        sm.Animator.SetBool("Hit", false);
    }
}

public class PlayerMoveState : PlayerState
{
    public override void Init(IStateMachine sm)
    {
        base.Init(sm);
        player = (Playerable)this.sm.GetOwner();
    }
    public override void Enter()
    {

    }

    public override void Update()
    {

    }
    public override void Exit()
    {

    }
}

public class MonsterIdleState : MonsterState
{
    public override void Init(IStateMachine sm)
    {
        base.Init(sm);
        monster = (Monster)this.sm.GetOwner();
    }
    public override void Enter()
    {

    }
    public override void Update()
    {

    }

    public override void Exit()
    {
    }

}

public class MonsterBattleState : MonsterState
{
    public override void Init(IStateMachine sm)
    {
        base.Init(sm);
        monster = (Monster)this.sm.GetOwner();
    }
    public override void Enter()
    {

    }
    public override void Update()
    {

    }

    public override void Exit()
    {

    }

}


public class MonsterAttack : MonsterBattleState
{
    public override void Init(IStateMachine sm)
    {
        base.Init(sm);
        monster = (Monster)this.sm.GetOwner();
    }
    public override void Enter()
    {
        //monster.skill[0].SetActive(true);
        //sm.Animator.SetBool("Attack", true);

        if (!monster.skill[0].gameObject.activeSelf)
        {
            monster.skill[0].GetComponent<Skill>().Cast();
            monster.skill[0].gameObject.SetActive(true);
            if (monster.skill[0].GetComponent<Skill>().skillDamage != 0)
            {
                BattleManager.instance.TargetCharacter.GetComponent<Character>().Hp -= monster.skill[0].GetComponent<Skill>().skillDamage;
            }
        }
    }
    public override void Update()
    {
        //if (monster.ani.GetCurrentAnimatorStateInfo(0).IsName("Normal") &&
        //    monster.ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        //{
        //    sm.Animator.SetBool("Normal", false);
        //    monster.skill[0].SetActive(false);
        //}

        sm.Animator.SetBool("Attack", true);
    }

    public override void Exit()
    {
        sm.Animator.SetBool("Attack", false);
    }
}

public class MonsterSkillAttack : MonsterBattleState
{
    public override void Init(IStateMachine sm)
    {
        base.Init(sm);
        monster = (Monster)this.sm.GetOwner();
    }
    public override void Enter()
    {
        //sm.Animator.SetBool("SkillAttack", true);
        //monster.skill[1].SetActive(true);

        if (!monster.skill[1].gameObject.activeSelf)
        {
            monster.skill[1].GetComponent<Skill>().Cast();
            monster.skill[1].gameObject.SetActive(true);
            if (monster.skill[1].GetComponent<Skill>().skillDamage != 0)
            {
                BattleManager.instance.TargetCharacter.GetComponent<Character>().Hp -= monster.skill[1].GetComponent<Skill>().skillDamage;
            }
        }
    }

    public override void Update()
    {
        //if (monster.ani.GetCurrentAnimatorStateInfo(0).IsName("SkillAtk") &&
        //    monster.ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        //{
        //    sm.Animator.SetBool("SkillAtk", false);
        //    monster.skill[1].SetActive(false);
        //}

        sm.Animator.SetBool("SkillAtk", true);
    }

    public override void Exit()
    {
        sm.Animator.SetBool("SkillAtk", false);
    }
}

public class MonsterUltimateAtk : MonsterBattleState
{
    public override void Init(IStateMachine sm)
    {
        base.Init(sm);
        monster = (Monster)this.sm.GetOwner();
    }
    public override void Enter()
    {
        //sm.Animator.SetBool("UltimateAttack", true);
        //monster.skill[2].SetActive(true);

        if (!monster.skill[2].gameObject.activeSelf)
        {
            monster.skill[2].GetComponent<Skill>().Cast();
            monster.skill[2].gameObject.SetActive(true);
        }
    }

    public override void Update()
    {
        //if (monster.ani.GetCurrentAnimatorStateInfo(0).IsName("UltimateAtk") &&
        //    monster.ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        //{
        //    sm.Animator.SetBool("UltimateAtk", false);
        //    monster.skill[2].SetActive(false);
        //}

        sm.Animator.SetBool("UltimateAtk", true);
    }

    public override void Exit()
    {
        sm.Animator.SetBool("UltimateAtk", false);
    }
}

public class MonsterHitState : MonsterState
{
    public override void Init(IStateMachine sm)
    {
        base.Init(sm);
        monster = (Monster)this.sm.GetOwner();
    }
    public override void Enter()
    {
    }
    public override void Update()
    {

    }

    public override void Exit()
    {

    }

}

public class PlayerAttack : PlayerBattleState
{
    public override void Init(IStateMachine sm)
    {
        base.Init(sm);
    }
    public override void Enter()
    {
        if (!player.skill[0].gameObject.activeSelf)
        {
            player.skill[0].GetComponent<Skill>().Cast();
            player.skill[0].gameObject.SetActive(true);
            if (player.skill[0].GetComponent<Skill>().skillDamage != 0)
            {
                BattleManager.instance.TargetCharacter.GetComponent<Character>().Hp -= player.skill[0].GetComponent<Skill>().skillDamage;
            }
        }
    }

    public override void Update()
    {
        sm.Animator.SetBool("Attack", true);
    }

    public override void Exit()
    {
        sm.Animator.SetBool("Attack", false);

    }
} //플레이어블 캐릭터의 일반 공격
public class PlayerSkillAttack : PlayerBattleState
{
    public override void Init(IStateMachine sm)
    {
        base.Init(sm);
    }
    public override void Enter()
    {
        if (!player.skill[1].gameObject.activeSelf)
        {
            player.skill[1].GetComponent<Skill>().Cast();
            player.skill[1].gameObject.SetActive(true);
            if (player.skill[1].GetComponent<Skill>().skillDamage != 0)
            {
                BattleManager.instance.TargetCharacter.GetComponent<Character>().Hp -= player.skill[1].GetComponent<Skill>().skillDamage;
            }
        }
    }

    public override void Update()
    {
        sm.Animator.SetBool("SkillAttack", true);
    }

    public override void Exit()
    {
        sm.Animator.SetBool("SkillAttack", false);
    }
}
public class PlayerUltimateAttack : PlayerBattleState
{
    public override void Init(IStateMachine sm)
    {
        base.Init(sm);
    }
    public override void Enter()
    {
        if (!player.skill[2].gameObject.activeSelf)
        {
            player.skill[2].GetComponent<Skill>().Cast();
            player.skill[2].gameObject.SetActive(true);
        }
    }

    public override void Update()
    {
        sm.Animator.SetBool("UltimateAttack", true);
    }

    public override void Exit()
    {
        sm.Animator.SetBool("UltimateAttack", false);
        //player.effectSkill[2].Stop();
    }
} //플레이어블 캐릭터의 궁극기 공격

public class StateMachine<T> : IStateMachine where T : Character
{
    public Animator animator = null;
    T owner;
    State curState;
    int stateEnumint;
    public State CurState
    {
        get { return curState; }
    }
    public Animator Animator
    {
        get { return animator; }
    }
    public Dictionary<STATE, State> stateDic = new Dictionary<STATE, State>();
    public StateMachine(T owner)
    {
        this.owner = owner;
    }
    public void SetAnimator(in Animator animator)
    {
        this.animator = animator;
    }
    public object GetOwner()
    {
        return owner;
    }
    public void SetState(STATE state)
    {
        curState?.Exit();
        curState = stateDic[state];
        curState.Enter();

        stateEnumint = (int)stateDic.FirstOrDefault(stateenum => stateenum.Value == curState).Key;
        owner.GetAnimator().SetInteger("State", stateEnumint);
    }
    public void AddState(STATE state_type, State state)
    {
        if (stateDic.ContainsKey(state_type))
            return;
        state.Init(this);
        stateDic.Add(state_type, state);
    }
    public void Update()
    {
        curState.Update();
    }
}
