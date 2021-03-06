﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEltLancerAI : EnemyEltLancer {
    protected override void SetTask(ref List<Task> taskList){
        taskList.Add(new TFaceToTarget(this));
        taskList.Add(new TWalkToTarget(this,GlobalVar.MobValue.Enemy.Lancer.SafeRange));
    }
    //    protected override CtrlInput Input() {
//        return new CtrlInput {
//            MoveAxis = InputCtrl.Get.Axis10(InputCtrl.Get.GAME_AXIS.MOVE),
//            UpDnAxis = InputCtrl.Get.Axis10(InputCtrl.Get.GAME_AXIS.UPDN),
//            JumpButton = InputCtrl.Get.Button(InputCtrl.Get.GAME_BUTTON.JUMP, InputCtrl.Get.BUTTON_ACTION.HOLD),
//            AttackButton = InputCtrl.Get.Button(InputCtrl.Get.GAME_BUTTON.ATTACK, InputCtrl.Get.BUTTON_ACTION.HOLD),
//            Skill2Button = InputCtrl.Get.Button(InputCtrl.Get.GAME_BUTTON.CREATE, InputCtrl.Get.BUTTON_ACTION.HOLD),
//            Skill1Button = InputCtrl.Get.Button(InputCtrl.Get.GAME_BUTTON.DODGE, InputCtrl.Get.BUTTON_ACTION.HOLD)
//        };
//    }
}
