using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mobilecontroller : MonoBehaviour
{
    GameObject currenttetrimino;
    PlayerTetrimino ptetrimino;
    private bool resetinputs = true;
    private bool resetinputw = true;
    private bool resetinputa = true;
    private bool resetinputd = true;
    private bool resetinputr = true;
    private bool resetinputq = true;
    private bool resetinputf = true;
    private bool instaads = false;

    public void ChangeCurrentplayertetrimino(GameObject changetetrimino)
    {
        currenttetrimino = changetetrimino;
        ptetrimino = currenttetrimino.GetComponent<PlayerTetrimino>();
        // resetinputs = true;
        resetinputa = true;
        resetinputd = true;
        resetinputr = true;
        resetinputq = true;
        Instaadsclose();
    }

    public void Instaads() //if instaads is true it means there is no active piece being controlled 
    {
        instaads = true;
    } 
    public void Instaadsclose() 
    {
        instaads = false;
        CancelInvoke("Instaadsclose");
    }

    bool prerun = false;

    void Update()
    {
        if (instaads) 
        {
            if (Input.GetKey("a"))   { prerun = true; }
            if (Input.GetKey("d"))   { prerun = true; }
            if (Input.GetKeyUp("a")) { prerun = false; }
            if (Input.GetKeyUp("d")) { prerun = false; }
        }

     

        if (prerun&&!instaads) 
        {
            if (Input.GetKey("a")) { Callads(true); prerun = false; }
            if (Input.GetKey("d")) { Callads(false); prerun = false; }

        }

        

            if (Input.GetKey("s") && resetinputs) { if (ptetrimino != null) { DoSoftdrop(); } resetinputs = false; }
            if (Input.GetKeyUp("s")) { if (ptetrimino != null) { Softdroprelease(); } resetinputs = true; }

            if (Input.GetKey("w") && resetinputw) { if (ptetrimino != null) { Doharddrop(); } resetinputw = false; }
            if (Input.GetKeyUp("w")) { resetinputw = true; }



            if (Input.GetKey("a") && resetinputa) {  if (ptetrimino != null) { Goleftpressed(); }resetinputa = false;  }
         

            if (Input.GetKeyUp("a")) { if (ptetrimino != null) { Goleftreleased(); } resetinputa = true;  }



            if (Input.GetKey("d") && resetinputd) {  if (ptetrimino != null) { Gorightpressed(); }  resetinputd = false;  }
       

            if (Input.GetKeyUp("d")) { if (ptetrimino != null) { Gorightreleased(); } resetinputd = true; }



            if (Input.GetKey("r") && resetinputr) { if (ptetrimino != null) { Rotateright(); } resetinputr = false; }
            if (Input.GetKeyUp("r")) { resetinputr = true; }

            if (Input.GetKey("q") && resetinputq) { if (ptetrimino != null) { Rotateleft(); } resetinputq = false; }
            if (Input.GetKeyUp("q")) { resetinputq = true; }

            if (Input.GetKey("f") && resetinputf) { if (ptetrimino != null) { Swaptetrimino(); } resetinputf = false; }
            if (Input.GetKeyUp("f")) { resetinputf = true; }


        //for fucks sake 

        if (mobileleft) { if (instaads) { prerun = true; } if (prerun && !instaads)   { Callads(true); prerun = false; }  }
        if (mobileright) { if (instaads) { prerun = true; }  if (prerun && !instaads) { Callads(false); prerun = false;}                      }

    }
    private bool mobileleft=false;
    private bool mobileright=false;

    //public void Goleftpressed() {  if (instaads) { prerun = true; }  if (prerun && !instaads) { Callads(true); prerun = false; } else if (!instaads) {ptetrimino.Goleftpressed(); }  }  
    //   public void Goleftreleased() { if (instaads) { prerun = false; } else if (ptetrimino != null) { ptetrimino.Goleftreleased(); } }

    public void Goleftpressed() { mobileleft = true; if (!instaads) { ptetrimino.Goleftpressed(); } }
       public void Goleftreleased() { mobileleft = false; if (instaads) { prerun = false; } if (ptetrimino != null) { ptetrimino.Goleftreleased(); } }

    public void Gorightpressed() { mobileright = true; if (!instaads) { ptetrimino.Gorightpressed(); } }
    public void Gorightreleased() { mobileright = false; if (instaads)  { prerun = false; } if (ptetrimino != null) { ptetrimino.Gorightreleased(); } }

    private void Callads(bool x) { Debug.Log("instainvoked to " + x); ptetrimino.Instainvoke(x); }


    public void Rotateleft() { ptetrimino.Rotateleft(); }

    public void Rotateright() { ptetrimino.Rotateright(); }

    public void Swaptetrimino() { ptetrimino.Swaptetrimino(); }

    public void Doharddrop() { ptetrimino.Doharddrop(); }
    public void DoSoftdrop() { ptetrimino.DoSoftdrop(); }
    public void Softdroprelease() { ptetrimino.Softdroprelease(); }

   


}
