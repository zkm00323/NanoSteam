using System;
using UnityEngine;

public class JumpTimeTest : MonoBehaviour{
    protected readonly float AdsorbSpeed_PosPreSec = 2;
    void Start (){
	    
	}

    private float time;
    private bool LastY;


    float x1;
    float x2;
    void FixedUpdate (){
	    Boolean nowY=Input.GetKeyDown(KeyCode.Y);

        if (nowY && !LastY){
            //                        Vector2 vec = transform.position - GameCtrl.Player.transform.position;
            //            //            float gravity=-Physics2D.gravity.y/2;
            //            //            Debug.Log(vec.y);
            //            //            GetComponent<Rigidbody2D>().velocity = new Vector2(/*(vec.x<0?1:-1)*AdsorbSpeed_PosPreSec*/0,/* 1/AdsorbSpeed_PosPreSec * Mathf.Abs(vec.x) * gravity*/-vec.y * value);
            //            float Gravity = -Physics2D.gravity.y;
            //            float upVec = Mathf.Sqrt(2 * -vec.y * Gravity);
            //            float upVecTime = upVec / Gravity*2;
            //            Debug.Log("Test"+upVecTime);
            //            GetComponent<Rigidbody2D>().velocity = new Vector2(0, Mathf.Sqrt(2 * -vec.y * -Physics2D.gravity.y));
            //
            //            time = Time.time;
            /////////////////////////
            Vector2 dis = GameCtrl.PlayerCtrl.GetCenterPos() - (Vector2)transform.position;
            float g = -Physics2D.gravity.y;
            float vec = 5;
            float a = (Mathf.Pow(dis.y, 2) + Mathf.Pow(dis.x, 2)) / Mathf.Pow(dis.x, 2);
            float b = (Mathf.Pow(vec, 2) + g * dis.y) / Mathf.Pow(vec, 2);
            float c = Mathf.Pow(g, 2) * Mathf.Pow(dis.x, 2) / (4 * Mathf.Pow(vec, 4));

//            x1 = (-b + Mathf.Sqrt(Mathf.Pow(b,2) - 4 * a * c)) / (2 * a);
//            x1 = Mathf.Acos(Mathf.Sqrt(Mathf.Abs(x1)))*Mathf.Rad2Deg;
//            x1 = dis.x>0 ? x1:180-x1;

            x2 = (-b - Mathf.Sqrt(Mathf.Pow(b, 2) - 4 * a * c)) / (2 * a);
            x2 = Mathf.Acos(Mathf.Sqrt(Mathf.Abs(x2)))*Mathf.Rad2Deg;
            x2 = dis.x > 0 ? x2 : 180 - x2;

            GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(x2*Mathf.Deg2Rad), Mathf.Sin(x2 * Mathf.Deg2Rad))* vec;
            /////////////////////////
        }
        Debug.DrawRay(transform.position,new Vector3(Mathf.Cos(x1*Mathf.Deg2Rad), Mathf.Sin(x1 * Mathf.Deg2Rad)),Color.blue);
        Debug.DrawRay(transform.position, new Vector3(Mathf.Cos(x2 * Mathf.Deg2Rad), Mathf.Sin(x2 * Mathf.Deg2Rad)), Color.red);
        LastY =nowY;
	}

    void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("Real"+(Time.time-time));
    }


    float Vec = 10;
    float Gravity = 9.8f;

}
