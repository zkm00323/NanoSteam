using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyEltLancer : Walker,IAttack,IRidicule,IShield,IEnemy{

    protected override void WalkerUpDate(){
        AttackInput();
        ShieldInput();
        RidiculeInput();
    }

    private float? LastAttackTriggerTime;
    void AttackInput() {
        if (!LastInput.AttackButton && NowInput.AttackButton) {
            LastAttackTriggerTime = Time.time;
        } else
            if (LastInput.AttackButton && !NowInput.AttackButton) {
                if (LastAttackTriggerTime != null) {
                    Anime.SetInteger(GlobalVar.AnimeValue.Attack, GlobalVar.AnimeValue.AHoldState.Click);
                    LastAttackTriggerTime = null;
                } else
                    Anime.SetInteger(GlobalVar.AnimeValue.Attack, GlobalVar.AnimeValue.AHoldState.Null);
            }

        if (LastAttackTriggerTime != null &&
            Time.time - LastAttackTriggerTime > GlobalVar.PlayerValue.AttackHoldTime) {
            LastAttackTriggerTime = null;
            Anime.SetInteger(GlobalVar.AnimeValue.Attack, GlobalVar.AnimeValue.AHoldState.Hold);
        }
    }

    public void StartAttack() {
        State = GlobalVar.State.Attack;
        LastAttackTriggerTime = null;
        if (Anime.GetInteger(GlobalVar.AnimeValue.Attack) == GlobalVar.AnimeValue.AHoldState.Click) Anime.SetInteger(GlobalVar.AnimeValue.Attack, GlobalVar.AnimeValue.AHoldState.Null);
    }
    public void FinishAttack() {
        LastAttackTriggerTime = null;
        Anime.SetInteger(GlobalVar.AnimeValue.Attack, GlobalVar.AnimeValue.AHoldState.Null);
    }

    //Skill1
    private void ShieldInput() {
        Anime.SetBool(GlobalVar.AnimeValue.Skill1, NowInput.Skill2Button);
    }
    public void StartShield() {
        State = GlobalVar.State.Shield;
    }
    public void FinishShield() {
    }

    //Skill2
    private bool LastRidicule;
    private void RidiculeInput(){
        bool NowRidicule=NowInput.Skill1Button;
        Anime.SetBool(GlobalVar.AnimeValue.Skill2, !LastRidicule && NowRidicule);
        LastRidicule=NowRidicule;
    }
    public void StartRidicule() {
        State = GlobalVar.State.Ridicule;
    }
    public void FinishRidicule() {
    }
}
