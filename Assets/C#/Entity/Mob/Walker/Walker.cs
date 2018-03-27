using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Walker : Mob,IMove{
    public AnimationCurve MoveForce;
    protected bool OnGround;
    [HideInInspector]
    public bool OnLadder;

    protected override void MobUpDate() {
        MoveInput();
        JumpInput();
        
        JumpCtrl();
        MoveCtrl();
        WalkerUpDate();
    }

    protected abstract void WalkerUpDate();

    protected virtual void MoveInput() {
        if (State != GlobalVar.State.Turn && NowInput.MoveAxis != 0 && NowInput.MoveAxis > 0 != Side) {
            Anime.SetBool(GlobalVar.AnimeValue.Turn, true);
            return;
        }
        Anime.SetBool(GlobalVar.AnimeValue.Turn, false);

        if (Mathf.Abs(Rb.velocity.x) < 0.01f)
            Anime.SetInteger(GlobalVar.AnimeValue.Move, GlobalVar.AnimeValue.MoveState.Idle);
        else
            Anime.SetInteger(GlobalVar.AnimeValue.Move, GlobalVar.AnimeValue.MoveState.Walk);
    }

    protected virtual void JumpInput() {
        Anime.SetBool(GlobalVar.AnimeValue.Jump, NowInput.JumpButton && Anime.GetInteger(GlobalVar.AnimeValue.Fly) == GlobalVar.AnimeValue.FlyState.Ground);
    }

    public virtual void MoveCtrl() {
        RaycastHit2D hit = Physics2D.Raycast(Main.position, Vector2.down, 1, GlobalVar.Mask.LayerObj);
        float slopeAngle = (hit ? Vector2.Angle(hit.normal, Vector2.up) : 0) * (Side ? -1 : 1);
        if (State == GlobalVar.State.Run || State == GlobalVar.State.Walk || State == GlobalVar.State.Idle)
            Rb.AddForce(Math.PosRot(Main.right * (Mathf.Abs(NowInput.MoveAxis) * GlobalVar.PlayerValue.RunForce * MoveForce.Evaluate(Mathf.Abs(Rb.velocity.x) / GlobalVar.PlayerValue.RunForce)), slopeAngle)*Rb.mass);
    }

    public virtual void JumpCtrl() {
        if (OnGround) Anime.SetInteger(GlobalVar.AnimeValue.Fly, GlobalVar.AnimeValue.FlyState.Ground);
        else
        if (Rb.velocity.y > GlobalVar.PlayerValue.JumpUpAnimeSence)
            Anime.SetInteger(GlobalVar.AnimeValue.Fly, GlobalVar.AnimeValue.FlyState.Up);
        else
        if (Rb.velocity.y < -GlobalVar.PlayerValue.JumpDownAnimeSence) {
            if (FallHight < GlobalVar.PlayerValue.FallHight)
                Anime.SetInteger(GlobalVar.AnimeValue.Fly, GlobalVar.AnimeValue.FlyState.Down);
            else
                Anime.SetInteger(GlobalVar.AnimeValue.Fly, GlobalVar.AnimeValue.FlyState.Fall);
        }
        else
            Anime.SetInteger(GlobalVar.AnimeValue.Fly, GlobalVar.AnimeValue.FlyState.Ground); 
    }

    public virtual void StartWalk(){
        State = GlobalVar.State.Walk;
    }
    public virtual void FinishWalk(){}

    public virtual void StartTurn(){
        State = GlobalVar.State.Turn;
    }

    public virtual void FinishTurn(){
        Side = !Side;
        Main.eulerAngles = new Vector3(0, Side ? 0 : 180, 0);
        Main.position+=Main.right*Mathf.Abs(GetComponent<Collider2D>().offset.x * 2);
    }

    protected float xVec;
    public virtual void StartJump(){
        State = GlobalVar.State.Jump;
        xVec = Rb.velocity.x;
        Rb.AddForce(Vector2.up * GlobalVar.PlayerValue.JumpUpForce);
        OnGround = false;
    }

    public virtual void StartFall(){
        State = GlobalVar.State.Jump;
    }

    public virtual void FinishJump(){
        int moveState = Anime.GetInteger(GlobalVar.AnimeValue.Move);
        if (moveState == GlobalVar.AnimeValue.MoveState.Walk ||
            moveState == GlobalVar.AnimeValue.MoveState.Run)
            Rb.velocity = new Vector2(xVec * GlobalVar.PlayerValue.JumpDownVecBouns, Rb.velocity.y);
    }

    public GameCtrl.Ladder Ladder;
    protected bool UpDown;
    public virtual void SlapeMove(GameCtrl.Ladder ladder, bool upDown) {
        Ladder = ladder;
        UpDown = upDown;
        int myFloor = GlobalVar.Mask.GetFloor(gameObject.layer);

        if(Rb.velocity.x == 0)return;
        if (ladder.GetFloor(upDown)-myFloor==1 && Rb.velocity.x < 0 == ladder.GetLoR(upDown)) {
            ChangeLayer(ladder.GetFloor(upDown));
            OnLadder = false;
        }else 
        if(myFloor == ladder.GetFloor(upDown) && Rb.velocity.x > 0 == ladder.GetLoR(upDown) && NowInput.UpDnAxis != 0 && upDown == NowInput.UpDnAxis > 0) {
            ChangeLayer(myFloor + (upDown ? 1 : -1));
            OnLadder = true;
        }
            
    }
    public void AnimeMove(float move) {
        Main.position += Main.right * move * GlobalVar.PlayerValue.AnimeMoveScale;
    }

    public virtual void SetOnGround(){
        OnGround = true;
    }

    public virtual bool IsOnGround(){
        return OnGround;
    }
}
