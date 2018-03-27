using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mob : Entity,IHealth{
    private List<Task> TaskList = new List<Task>();
    private Task NowTask;
    public Entity Target;
    public Vector2 SeeRange = new Vector2(1,1);

    protected Vector2 LastVec;
    protected Vector3 LastPos;
    protected float FallHight;
    private float? StartFallY;

    protected override void Start(){
        base.Start();
        SetTask(ref TaskList);
        SetTaskPriority();
    }

    protected virtual void SetTask(ref List<Task> taskList) { }
    private void SetTaskPriority(){
        for(int i = 0; i < TaskList.Count; i++){
            TaskList[i].Priority = i;
        }
    }

    protected override void EntityUpDate(){
        TaskCtrl();
        MobUpDate();
        FallCtrl();

        LastVec = Rb.velocity;
        LastPos = Main.position;
    }

    private void TaskCtrl(){
        foreach (Task task in TaskList) {
            task.JustDo(ref NowInput);
            if (NowTask != null) {
                if (task.Priority > NowTask.Priority && task.WhileDo(ref NowInput)) {
                    NowTask.OnFinish(ref NowInput);
                    NowTask = task;
                    NowTask.StartDo(ref NowInput);
                    return;
                }
            } else
            if (task.WhileDo(ref NowInput)) {
                NowTask = task;
                NowTask.StartDo(ref NowInput);
                return;
            }
        }
        if (NowTask != null) {
            if (!NowTask.UpdateDo(ref NowInput)) {
                NowTask.OnFinish(ref NowInput);
                NowTask = null;
            }
        }
    }

    private void FallCtrl(){
        if(LastVec.y>=0 && Rb.velocity.y<0)
            StartFallY = LastPos.y;
        else
        if (LastVec.y<0 && Rb.velocity.y>=0)
            StartFallY = null;

        FallHight = StartFallY==null ? 0 : Mathf.Abs((float)(StartFallY - Main.position.y));
    }

    protected abstract void MobUpDate();

    public virtual Task GetTask() {
        return NowTask;
    }

    protected override void ColStay(Collision2D col){
        base.ColStay(col);
        if(col.collider.tag==GlobalVar.Tag.Ground){
            Anime.SetInteger(GlobalVar.AnimeValue.Fly, GlobalVar.AnimeValue.FlyState.Ground);
        }
    }

    public void SetHealth(int health){
        throw new System.NotImplementedException();
    }

    public void AddHealth(int add){
        throw new System.NotImplementedException();
    }
    
    public int GetHealth(){
        throw new System.NotImplementedException();
    }

    public void ChangeLayer(int floor){
        gameObject.layer = GlobalVar.Mask.GetLayer(floor, GlobalVar.Mask.LayerType.Allies);
    }
}
