using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Task {

    public readonly string taskName;
    public int Priority;
    protected Mob Self;

    protected Task(Mob self, string tasknameIn) {
        taskName = tasknameIn;

        Self = self;
    }

    public abstract void JustDo(ref Entity.CtrlInput input);
    public abstract bool WhileDo(ref Entity.CtrlInput input);
    public abstract void StartDo(ref Entity.CtrlInput input);
    public abstract bool UpdateDo(ref Entity.CtrlInput input);
    public abstract void OnFinish(ref Entity.CtrlInput input);

    public bool FindTargetAround(List<Entity> type) {
        List<Entity> targetList = GetEnityAround(Self, type);
        if (targetList.Count > 0) {
            float minDis = 0;
            int index = 0;
            for (int i = 0; i < targetList.Count; i++) {
                float dis = (Self.transform.position - targetList[i].transform.position).magnitude;
                if (minDis > dis || minDis == 0) {
                    minDis = dis;
                    index = i;
                }
            }
            Self.Target = targetList[index];
            return true;
        }
        return false;
    }

    public static List<Entity> GetEnityAround(Mob self, List<Entity> type) {
        List<Entity> enityList = new List<Entity>();
        foreach (Entity enity in GameCtrl.GetEntityList()) {
            if (type != null) {
                bool ctn = false;
                foreach (Entity enityType in type) {
                    if (enity.GetType() == enityType.GetType()) {
                        ctn = true;
                        break;
                    }
                }
                if (!ctn) continue;
            }
            if (IsInSeeView(self, enity)) {
                enityList.Add(enity);
            }
        }
        return enityList;
    }

    public static bool IsInSeeView(Mob self, Entity target) {
        Vector2 selfPos = self.transform.position;
        Vector2 targetPos = target.transform.position;
        return self.GetSide()
            ? targetPos.x > selfPos.x && targetPos.x < selfPos.x + self.SeeRange.x
            : targetPos.x < selfPos.x && targetPos.x > selfPos.x - self.SeeRange.x
            &&
            targetPos.y < selfPos.y + self.SeeRange.y &&
            targetPos.y > selfPos.y - self.SeeRange.y
        ;
    }
}