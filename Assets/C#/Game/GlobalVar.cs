using System;
using UnityEngine;

public class GlobalVar{

    public class Prefeb{
        public const string Player = "Prefebs/Player";
        public const string SlopeTrigger = "Prefebs/SlopeTrigger";
        public const string LadderTrigger = "Prefebs/LadderTrigger";
    }

    public enum State {
        Null,Idle, Walk, Run, Turn, Jump, Attack, Climb, Create, Dodge, Dead, Ridicule, Shield
    }

    public class Mask{
        public const int FloorAmount = 6;

        public enum LayerType{
            Obj, Enemy, Allies
        }

        public const string ObjLayer = "Obj";
        public const string EnemyLayer = "Enemy";
        public const string AlliesLayer = "Allies";

        public static readonly string[] ListObj = { ObjLayer + "0", ObjLayer + "1", ObjLayer + "2", ObjLayer + "3", ObjLayer + "4", ObjLayer + "5"};
        public static readonly string[] ListEnemy = { EnemyLayer + "0", EnemyLayer + "1", EnemyLayer + "2", EnemyLayer + "3", EnemyLayer + "4", EnemyLayer + "5" };
        public static readonly string[] ListAllies = { AlliesLayer + "0", AlliesLayer + "1", AlliesLayer + "2", AlliesLayer + "3", AlliesLayer + "4", AlliesLayer + "5" };

        public static readonly LayerMask LayerObj = LayerMask.GetMask(ListObj);
        public static readonly LayerMask LayerEnemy = LayerMask.GetMask(ListEnemy);
        public static readonly LayerMask LayerAllies = LayerMask.GetMask(ListAllies);

        public static int GetFloor(int layer){
            string layerName = LayerMask.LayerToName(layer);
            for (int i = 0; i < FloorAmount; i++){
                if(layerName == ListObj[i] || layerName == ListEnemy[i] || layerName == ListAllies[i])
                    return i;
            }
            throw new Exception("GetFloor:Unknow input string!");
        }

        public static int GetLayer(int floor, LayerType type) {
            if(floor<0||floor>FloorAmount) throw new Exception("GetLayer:Unknow input floor!");
            switch (type){
                case LayerType.Obj:
                    return LayerMask.NameToLayer(ListObj[floor]);
                case LayerType.Allies:
                    return LayerMask.NameToLayer(ListAllies[floor]);
                case LayerType.Enemy:
                    return LayerMask.NameToLayer(ListEnemy[floor]);
            }
            throw new Exception("GetLayer:Unknow input floor!");
        }
    }

    public class Tag {
        public const string Ground = "Ground";
        public const string Ladder = "Ladder";
        public const string Center = "Center";
    }

    public class InterFace{
        public const string IClimb = "IClimb";
    }

    public class EntityValue{
        public const string Ground = "Ground";
    }

    public class PlayerValue{
        public const float WalkToRunTime = 1;
        public const int AttackMaxCount=3;
        public const float JumpUpForce = 200f;
        public const float JumpUpAnimeSence = 0.01f;
        public const float JumpDownAnimeSence = 0.01f;
        public const float FallHight = 1f;
        public const float WalkForce = 5f;
        public const float SpeedUpBonus=1.2f;
        public const float RunForce = 6f;
        public const float JumpDownVecBouns=0.7f;
        public const float StartRunVec = 0.6f;
        public const float AttackHoldTime = 0.2f;
        public const float AnimeMoveScale = 0.05f;
        public const float DodgeForce = 100;
        public const float FinishDodgeVec = 1.6f;
        public const float ClimbSpeed = 0.5f;
    }

    public class MobValue {
        public class Our {
            public class Sprider {
                public const float JumpVec = 5;
                public const float SafeRange = 0.5f;
            }
            public class Archer {
                public const float SafeRange = 3;
            }
        }
        public class Enemy{
            public class Lancer {
                public const float SafeRange = 2;
            }
        }
    }


    public class AnimeValue{
        public const string Move = "Move";
        public const string Fly = "Fly";
        public const string Weapon = "Weapon";
        public const string Turn = "Turn";
        public const string Attack = "Attack";
        public const string ClimbIn = "ClimbIn";
        public const string ClimbOut = "ClimbOut";
        public const string Create = "Create";
        public const string Dodge = "Dodge";
        public const string Jump = "Jump";
        public const string CState = "CState";
        public const string Dead = "Dead";
        public const string Skill1 = "Skill1";
        public const string Skill2 = "Skill2";
        public const string Skill3 = "Skill3";

        public class AHoldState {
            public const int Null = 0;
            public const int Click = 1;
            public const int Hold = 2;
        }

        public class MoveState {
            public const int Idle = 0;
            public const int Walk = 1;
            public const int Run = 2;
        }

        public class FlyState {
            public const int Ground = 0;
            public const int Up = 1;
            public const int Down = 2;
            public const int Fall = 3;
        }

        public class WeaponState{
            public const int Hand = 0;
            public const int Glove = 1;
            public const int Gun = 2;
        }
        public class DodgeState {
            public const int Null = 0;
            public const int Click = 1;
            public const int Dodge = 2;
        }
    }
}
