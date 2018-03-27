using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Climber : Walker, IClimb {
    protected override void WalkerUpDate(){
        ClimbInput();
        ClimberUpDate();
    }
    protected abstract void ClimberUpDate();

    protected virtual void ClimbInput() {
        Anime.SetFloat(GlobalVar.AnimeValue.CState, NowInput.UpDnAxis == 0 ? 0 : NowInput.UpDnAxis > 0 ? 1 : -1);
        if (State == GlobalVar.State.Climb)
            Main.position += Vector3.up * NowInput.UpDnAxis * GlobalVar.PlayerValue.ClimbSpeed * Time.fixedDeltaTime;
    }
    public virtual void LadderMove(GameCtrl.Ladder ladder, bool upDown) {
        Ladder = ladder;
        UpDown = upDown;
        if (State == GlobalVar.State.Climb && upDown == NowInput.UpDnAxis < 0) {
            Anime.SetBool(GlobalVar.AnimeValue.ClimbOut, true);
            return;
        } else
            Anime.SetBool(GlobalVar.AnimeValue.ClimbOut, false);
        if (State != GlobalVar.State.Climb && upDown == NowInput.UpDnAxis > 0 && Rb.velocity.magnitude < 1) {
            Anime.SetBool(GlobalVar.AnimeValue.ClimbIn, true);
        } else
            Anime.SetBool(GlobalVar.AnimeValue.ClimbIn, false);
    }

    private FixedJoint2D Hook;
    public void ReadyClimb() {
        ChangeLayer(Ladder.GetFloor(UpDown) + (UpDown ? 1 : -1));

        Rb.velocity = Vector2.zero;
        Vector3 pos = Main.position;
        Main.position = new Vector3(Ladder.DownPos.position.x + Mathf.Abs(GetComponent<Collider2D>().offset.x) * (Side ? 1 : -1), pos.y, pos.z);

        Hook = gameObject.AddComponent<FixedJoint2D>();
        OnLadder = true;
    }
    public void StartClimb() {
        State = GlobalVar.State.Climb;
    }
    public void EndClimb() {
        State = GlobalVar.State.Null;
    }

    public void FinishClimb() {
        ChangeLayer(Ladder.GetFloor(UpDown));
        Destroy(Hook);
        OnLadder = false;
    }

    public void AnimeUpDown(float move) {
        Main.position += Vector3.up * move * GlobalVar.PlayerValue.AnimeMoveScale;
    }
}
