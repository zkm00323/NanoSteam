using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCtrl : MonoBehaviour{
    public struct Ladder{
        public int UpFloor;
        public Transform UpPos;

        public int DownFloor;
        public Transform DownPos;

        public int AngleType;

        public Ladder(int baseFloot, Transform upPos, Transform downPos, int angleType) : this(){
            DownFloor = baseFloot - 1;
            UpFloor = baseFloot + 1;

            UpPos = upPos;
            DownPos = downPos;

            AngleType=angleType;
        }

        public bool GetLoR(bool upDown){
            return upDown ? AngleType>0 : AngleType<0;
        }

        public int GetFloor(bool upDown){
            return upDown ? DownFloor : UpFloor;
        }
    }
    public static List<Ladder> LadderList = new List<Ladder>();

    public static void AddLadder(Ladder ladder){
        LadderList.Add(ladder);
    }

    public static GameObject Player;
    public static Human PlayerCtrl;
    public void Awake(){
        Player = Instantiate(Resources.Load(GlobalVar.Prefeb.Player) as GameObject);
        PlayerCtrl = Player.GetComponent<Human>();
    }

    private static List<Entity> EntityList = new List<Entity>();

    public static void AddEntity(Entity entity){
        EntityList.Add(entity);
    }

    public static List<Entity> GetEntityList(){
        return EntityList;
    }

    public static void DeletEntity(Entity entity){
        EntityList.Remove(entity);
    }
}
