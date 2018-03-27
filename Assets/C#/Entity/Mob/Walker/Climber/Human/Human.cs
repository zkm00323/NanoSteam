using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Human : Climber, IHumanMove,IAttack,IDodge{

    protected override void ClimberUpDate(){
        AttackInput();
        CreateInput();
        DodgeInput();

        DodgeCtrl();
    }

     protected override void MoveInput() {
        if (State != GlobalVar.State.Turn && NowInput.MoveAxis != 0 && NowInput.MoveAxis > 0 != Side) {
            Anime.SetBool(GlobalVar.AnimeValue.Turn, true);
            return;
        }
        Anime.SetBool(GlobalVar.AnimeValue.Turn, false);

        float nowSpeed = Mathf.Abs(Rb.velocity.x);

        if (nowSpeed < 0.01f)
            Anime.SetInteger(GlobalVar.AnimeValue.Move, GlobalVar.AnimeValue.MoveState.Idle);
        else if (nowSpeed > 1)
            Anime.SetInteger(GlobalVar.AnimeValue.Move, GlobalVar.AnimeValue.MoveState.Run);
        else
            Anime.SetInteger(GlobalVar.AnimeValue.Move, GlobalVar.AnimeValue.MoveState.Walk);
    }

    private float? LastAttackTriggerTime;
    void AttackInput(){
        if(!LastInput.AttackButton && NowInput.AttackButton){
            LastAttackTriggerTime = Time.time;
        } else 
        if (LastInput.AttackButton && !NowInput.AttackButton) {
            if(LastAttackTriggerTime != null){
                Anime.SetInteger(GlobalVar.AnimeValue.Attack, GlobalVar.AnimeValue.AHoldState.Click);
                LastAttackTriggerTime = null;
            }else
                Anime.SetInteger(GlobalVar.AnimeValue.Attack, GlobalVar.AnimeValue.AHoldState.Null);
        }

        if (LastAttackTriggerTime != null &&
            Time.time - LastAttackTriggerTime > GlobalVar.PlayerValue.AttackHoldTime) {
            LastAttackTriggerTime = null;
            Anime.SetInteger(GlobalVar.AnimeValue.Attack, GlobalVar.AnimeValue.AHoldState.Hold);
        }
    }
    void CreateInput() {
        Anime.SetBool(GlobalVar.AnimeValue.Create, NowInput.Skill2Button);
    }
    void DodgeInput() {
        if(State!= GlobalVar.State.Dodge)Anime.SetInteger(GlobalVar.AnimeValue.Dodge, NowInput.Skill1Button ? 1 : 0);
    }
    private void DodgeCtrl() {
        if (Anime.GetInteger(GlobalVar.AnimeValue.Dodge) == GlobalVar.AnimeValue.DodgeState.Dodge)
            if (Mathf.Abs(Rb.velocity.x) < GlobalVar.PlayerValue.FinishDodgeVec)
                Anime.SetInteger(GlobalVar.AnimeValue.Dodge, GlobalVar.AnimeValue.DodgeState.Null);
    }

    //Run
    public void StartRun() {
        State = GlobalVar.State.Run;
    }
    public void FinishRun() {}

    //Attack
    public void StartAttack() {
        State = GlobalVar.State.Attack;
        LastAttackTriggerTime = null;
        if(Anime.GetInteger(GlobalVar.AnimeValue.Attack)==GlobalVar.AnimeValue.AHoldState.Click) Anime.SetInteger(GlobalVar.AnimeValue.Attack, GlobalVar.AnimeValue.AHoldState.Null);
    }

    public void FinishAttack(){
        LastAttackTriggerTime = null;
        Anime.SetInteger(GlobalVar.AnimeValue.Attack, GlobalVar.AnimeValue.AHoldState.Null);
    }

    //Dodge
    public void StartDodge(){
        State = GlobalVar.State.Dodge;
        Anime.SetInteger(GlobalVar.AnimeValue.Dodge,GlobalVar.AnimeValue.DodgeState.Dodge);
        Rb.AddForce(Main.right * GlobalVar.PlayerValue.DodgeForce);
    }

    public void FinishDodge(){}
}
