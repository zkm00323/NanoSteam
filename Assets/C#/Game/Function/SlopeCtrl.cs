using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlopeCtrl : GroundCtrl {
    public Transform Pos0;
    public Transform Pos1;
    protected GameCtrl.Ladder Ladder;

    protected override void Awake(){
        base.Awake();
        if (Pos0.position.y>Pos1.position.y){
            Transform save=Pos0;
            Pos0=Pos1;
            Pos1=save;
        }
        Transform[] pos = { Pos0, Pos1 };

        int AngleType=pos[0].position.x==pos[1].position.x ? 0:pos[0].position.x<pos[1].position.x ? 1:-1;

        Ladder = new GameCtrl.Ladder(MyFloor, pos[1], pos[0], AngleType);

        EdgeCollider2D edge = gameObject.AddComponent<EdgeCollider2D>();
        edge.points = new Vector2[]{pos[0].position,pos[1].position};

        if(AngleType==0)
            edge.isTrigger = true;

        for (int i=0;i<2;i++){
            GameObject Trigger = Instantiate(Resources.Load(AngleType == 0?GlobalVar.Prefeb.LadderTrigger:GlobalVar.Prefeb.SlopeTrigger) as GameObject);
            Trigger.transform.parent = transform;
            Trigger.transform.position = pos[i].position;
            Destroy(pos[i].gameObject);
            pos[i] = Trigger.transform;
        }

        Ladder.DownPos = pos[0].transform;
        Ladder.UpPos = pos[1].transform;

        for (int i = 0; i < 2; i++){
            FloorCtrl floor = pos[i].GetComponent<FloorCtrl>();
            floor.UpDown = i == 0;
            floor.Ladder = Ladder;
        }

        GameCtrl.AddLadder(Ladder);
    }

    protected override void OnCollisionEnter2D(Collision2D col){
        Walker walker = col.gameObject.GetComponent<Walker>();
        if (walker != null) {
            walker.ChangeLayer(MyFloor);
            walker.SetOnGround();
        }
    }
}
