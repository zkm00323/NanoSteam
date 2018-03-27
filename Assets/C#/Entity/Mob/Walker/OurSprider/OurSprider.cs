using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OurSprider : Walker{
    protected FixedJoint2D Binding;

    protected override void WalkerUpDate(){
        BindCtrl();
        OurSpriderUpDate();
    }

    protected abstract void OurSpriderUpDate();

    public override void StartJump() {
        State = GlobalVar.State.Jump;

        OnGround = false;
        if (GameCtrl.PlayerCtrl.GetState() == GlobalVar.State.Climb) {
            Vector2 jumpVec = JumpToPos(GameCtrl.PlayerCtrl.GetCenterPos());
            if (jumpVec != Vector2.zero) {
                Rb.velocity = jumpVec;
                ChangeLayer(GlobalVar.Mask.GetFloor(GameCtrl.Player.gameObject.layer));
                return;
            }
        }
        Rb.AddForce(Vector2.up * GlobalVar.PlayerValue.JumpUpForce);
    }

    public Vector2 JumpToPos(Vector2 pos) {
        float g = -Physics2D.gravity.y;
        float maxVec = GlobalVar.MobValue.Our.Sprider.JumpVec;
        float upMin = maxVec / 4;
        Vector2 dis = pos - GetCenterPos();

        Vector2 vec = Vector2.up * (dis.y > 0 ? Mathf.Sqrt(2 * dis.y * g) : upMin);
        float a = g / 2;
        float b = -vec.y;
        float c = dis.y;
        float t = 0;
        for (int i = 0; i < 2; i++) {
            t = (-b + Mathf.Sqrt(Mathf.Abs(Mathf.Pow(b, 2) - 4 * a * c)) * (i == 0 ? -1 : 1)) / (2 * a);
            if (t > 0) break;
        }
        vec += Vector2.right * (dis.x / t);
        return vec.magnitude < maxVec ? vec : Vector2.zero;
    }


    private void BindCtrl() {
        if (Binding!=null && GameCtrl.PlayerCtrl.GetState() == GlobalVar.State.Idle){
            Destroy(Binding);
            Binding = null;
            ChangeLayer(GlobalVar.Mask.GetFloor(GameCtrl.Player.gameObject.layer));
            float jumpRange = GlobalVar.MobValue.Our.Sprider.SafeRange/2;
            Rb.velocity = JumpToPos(GetCenterPos() + new Vector2(Random.Range(-jumpRange, jumpRange), Random.Range(0, jumpRange)));
        }
    }

    protected void OnTriggerEnter2D(Collider2D col) {
        Human player = col.gameObject.GetComponent<Human>();
        if (State == GlobalVar.State.Jump && Binding==null && player != null) {
            Binding = gameObject.AddComponent<FixedJoint2D>();
            Binding.connectedBody = player.Rb;
            Binding.autoConfigureConnectedAnchor = false;
        }
    }

    public virtual bool isBinding(){
        return Binding != null;
    }
//    protected Vector2 JumpToPlayer(float force) {
//        Vector2 dis = GameCtrl.PlayerCtrl.GetCenterPos() - GetCenterPos();
//        float g = -Physics2D.gravity.y;
//        float vec = force;
//        float a = (Mathf.Pow(dis.y, 2) + Mathf.Pow(dis.x, 2)) / Mathf.Pow(dis.x, 2);
//        float b = -((Mathf.Pow(vec, 2) + g * dis.y) / Mathf.Pow(vec, 2));
//        float c = Mathf.Pow(g, 2) * Mathf.Pow(dis.x, 2) / (4 * Mathf.Pow(vec, 4));
//        float[] ang = new float[2];
//        for (int i = 0; i < 2; i++) {
//            ang[i] = (-b + Mathf.Sqrt(Mathf.Pow(b, 2) - 4 * a * c) * (i == 0 ? 1 : -1)) / (2 * a);
//            if (ang[i] == float.NaN) return Vector2.zero;
//            ang[i] = Mathf.Acos(Mathf.Sqrt(Mathf.Abs(ang[i]))) * Mathf.Rad2Deg;
//            ang[i] = dis.x > 0 ? ang[i] : 180 - ang[i];
//        }
//        return
//                new Vector2(
//                    Mathf.Cos(ang[1] * Mathf.Deg2Rad),
//                    Mathf.Sin(ang[1] * Mathf.Deg2Rad)
//                ) * vec;
//        //            new Vector2(
//        //                Mathf.Cos(ang[0] * Mathf.Deg2Rad) + Mathf.Cos(ang[1] * Mathf.Deg2Rad),
//        //                Mathf.Sin(ang[0] * Mathf.Deg2Rad) + Mathf.Sin(ang[1] * Mathf.Deg2Rad)
//        //            ) * vec / 2;
//    }
}

