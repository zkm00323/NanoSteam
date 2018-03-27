using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public abstract class Entity : MonoBehaviour, IIdle,IDead {
    public struct CtrlInput {
        public float MoveAxis;
        public int UpDnAxis;
        public bool JumpButton;
        public bool AttackButton;
        public bool Skill1Button;
        public bool Skill2Button;
    }

    protected GlobalVar.State State;
    protected GlobalVar.State LastState;
    [HideInInspector]
    public Animator Anime;
    [HideInInspector]
    public Rigidbody2D Rb;
    [HideInInspector]
    public Collider2D Col;
    protected Transform Main;
    protected Transform Center;
    protected bool Side = true;
    protected CtrlInput NowInput;
    protected CtrlInput LastInput;

    protected virtual void Awake(){
        Main = transform;
        Center = transform.Find(GlobalVar.Tag.Center);
        Rb = GetComponent<Rigidbody2D>();
        Col = GetComponent<Collider2D>();
        Anime = GetComponent<Animator>();
        Col = GetComponent<Collider2D>();


    }

    protected virtual void Start(){

    }

    protected virtual void FixedUpdate(){
        NowInput = Input();
        EntityUpDate();
        LastState = State;
        LastInput = NowInput;
    }

    protected virtual void OnCollisionStay2D(Collision2D col) {
        ColStay(col);
    }

    protected virtual void OnTriggerStay2D(Collider2D col) {
        TriStay(col);
    }

    protected abstract void EntityUpDate();

    protected virtual CtrlInput Input(){
        return new CtrlInput();
    }
    protected virtual void ColStay(Collision2D col) {}
    protected virtual void TriStay(Collider2D col) {}
    public GlobalVar.State GetState(){
        return State;
    }

    public Boolean GetSide(){
        return Side;
    }

    public Vector2 GetCenterPos(){
        return (Vector2)transform.position+new Vector2(Col.offset.x*(Side?1:-1),Col.offset.y);
    }

    public void StartDead() {
        Anime.Play(GlobalVar.AnimeValue.Dead);
    }
    public void FinishDead() {
        Destroy(gameObject);
    }

    public void StartIdle(){
        State= GlobalVar.State.Idle;
    }
    public void FinishIdle() {}
}
