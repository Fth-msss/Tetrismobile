using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


using UnityEngine.SceneManagement;// REMOVE THİS
public class PlayerTetrimino : MonoBehaviour
{
    // Start is called before the first frame update

   

    Vector3 down  = new Vector3(0,-1,0);

    Vector3 left  = new Vector3(-1, 0, 0);
    Vector3 right = new Vector3(1, 0, 0);
   

    private GameObject gameadmin;
    private Gameadmin adminscript;
    private Mobilecontroller Mobilecontrols;
    public string tetriminotype;
    public string lastmove;
    public float dropdownspeed = 0.5f;
    
    void Start()
    {
        Ghostsetter();
        gameadmin = GameObject.Find("GameAdmin");
        adminscript = gameadmin.GetComponent<Gameadmin>();
        Mobilecontrols = gameadmin.GetComponent<Mobilecontroller>();
        Mobilecontrols.ChangeCurrentplayertetrimino(gameObject);
        dropdownspeed = adminscript.ddownspeed;
       // InvokeRepeating("Dropdown",0,1);
        InvokeDropdown(dropdownspeed);
        Invoke("Stupidinvoke",0.03f);
     
    }

    private void Stupidinvoke() 
    {
        Ghostsetter();

    }

    public void InvokeDropdown(float droptimer) 
    {
        CancelInvoke("Dropdown");
        InvokeRepeating("Dropdown",0.1f,droptimer);
    }

    public void ChangeDropdownSpeed(float droptimer) 
    {
        CancelInvoke("Dropdown");
        InvokeRepeating("Dropdown", 0, droptimer);
    }

    public bool issoftdropping=false;
    public void Dropdown() //gravity.soft drop increases gravity
    {
        if (CheckDownTiles()) { gameObject.transform.position += down; lastmove = "dropdown";if (issoftdropping) { adminscript.Finalizescore(1); } ; }
        else { LockDelay(); /*Locktetrimino(); */}
    
    }

    public void Harddrop() //every time 
    {
        while (CheckDownTiles()) { gameObject.transform.position += down; lastmove = "harddrop";adminscript.Finalizescore(2); }
        Locktetrimino();
    }

    public void Rotate(bool direction) // true is clockwise
    {
        var asd = GetComponent<RotationSystem>();
        

        if (direction) { if (asd.currentposition == 3) { asd.CheckRotation(0); } else { asd.CheckRotation(asd.currentposition+1);} }
        else { if (asd.currentposition == 0) { asd.CheckRotation(3); } else { asd.CheckRotation(asd.currentposition - 1);} }     
    }




    public float time= 0;

    public void ResetLockDelay() //restarts the timer also resets ghost object
    {
        if (markedforlock) 
        {
            lockdelaylimit += 1;
            if (lockdelaylimit <= 12)
            {
                CancelInvoke("Deathcheck");
              
                markedforlock = false;
            }
        }
    }

    private int lockdelaylimit = 0;
    private bool markedforlock = false;
    public void LockDelay() 
    {
        /*
         after the piece touches the ground:they have an extra 0.5 second to move.
        this resets on every successful move.i also limited it to 12 delays   
         */

        /*
        this function gets called when the piece gets marked for lock. 

         */

        //alright. this seems to work.it is 1 am i am leaving this project to rot for another month

        if (markedforlock) { }//dont get anymore invokes
        else { Invoke("Deathcheck", 0.5f);markedforlock = true; }

        

    }

  private void Deathcheck() { if (markedforlock) { Locktetrimino();} }

    public void Locktetrimino() //ends control for this piece and calls to gameadmin instantiates new piece
    {
        Mobilecontrols.Instaads();
        bool istspin = false;
        if (tetriminotype == "T" && lastmove == "rotation") { var asd = GetComponent<RotationSystem>();istspin=asd.CheckTspin(); }

        foreach (Transform child in transform)
        {
            if (child.transform.position.y >= 10) { Debug.Log("gameover"); adminscript.Enableoldun(); }
            //child is your child transform

            var asd = child.GetComponent<BoxCollider2D>();
            asd.enabled = true;
            ActiveBlock asdf = asd.gameObject.GetComponent<ActiveBlock>();
            asdf.Checklineclear();
          
        }

        if (istspin) { adminscript.ScoreCalculate(istspin); }

        Ghostreset();
        adminscript.Lockedmino();
        Destroy(this,0.01f);
        

    }
   
   
    /*
    void Update()//bunlardan iki tane olması o kadar saçma ki 
    {
       
        if (Input.GetKeyDown("s")) { DoSoftdrop(); }

        if (Input.GetKeyUp("s")) { Softdroprelease(); }

        if (Input.GetKeyDown("w")) {  Doharddrop(); }

        if (Input.GetKeyDown("a")) {Goleftpressed(); }
        if (Input.GetKeyUp("a")) { Goleftreleased(); }

     

        if (Input.GetKeyDown("d")) { Gorightpressed(); }
        if (Input.GetKeyUp("d")) { Gorightreleased(); }

        if (Input.GetKeyDown("r")) {Rotateright(); }

        if (Input.GetKeyDown("q")) {Rotateleft(); }

        if (Input.GetKeyDown("f")) {Swaptetrimino(); }

        if (Input.GetKeyDown("l")) { SceneManager.LoadScene(0); }// GET THİS TO GAMEADMİN
    }
    */
   

  


    public void Goleftpressed() { if (Checkifplaced(left)) { gameObject.transform.position += left; lastmove = "goneleft"; ResetLockDelay(); Ghostsetter(); } Normalinvoke(true, 0.3f, 0.06f);  }
    public void Goleftreleased() { CancelInvoke("GoleftInvoked"); Ghostsetter(); }

    public void Gorightpressed() { if (Checkifplaced(right)) { gameObject.transform.position += right; lastmove = "goneright"; ResetLockDelay(); Ghostsetter(); } Normalinvoke(false,0.3f,0.06f);  }
    public void Gorightreleased() { CancelInvoke("GorightInvoked"); Ghostsetter(); }

    public void Normalinvoke(bool x,float time,float repeat) 
    {
        if (x) { CancelInvoke("GorightInvoked"); InvokeRepeating("GoleftInvoked", time, repeat); }
        else   { CancelInvoke("GoleftInvoked");  InvokeRepeating("GorightInvoked", time, repeat); }
    }

    public void Instainvoke(bool x) 
    {
        if (x) { CancelInvoke("GorightInvoked"); CancelInvoke("GoleftInvoked"); InvokeRepeating("GoleftInvoked", 0f, 0.06f); }
        else   { CancelInvoke("GoleftInvoked"); CancelInvoke("GorightInvoked"); InvokeRepeating("GorightInvoked", 0f, 0.06f); }
    }

    public void GoleftInvoked() { if (Checkifplaced(left)) { gameObject.transform.position += left; lastmove = "goneleft"; ResetLockDelay(); Ghostsetter(); } }

    public void GorightInvoked() { if (Checkifplaced(right)) { gameObject.transform.position += right; lastmove = "goneright"; ResetLockDelay(); Ghostsetter(); } }

    public void Rotateleft() { Rotate(false); Ghostsetter(); }

    public void Rotateright() { Rotate(true); Ghostsetter(); }

    
    public void Swaptetrimino() { if (adminscript.Swaplock) { adminscript.Swaptetrimino(gameObject); Destroy(gameObject);adminscript.Swaplock = false;}  }

    public void Doharddrop() { Harddrop(); Ghostreset();  }

    public void DoSoftdrop() { if (Checkifplaced(down)&&0.04f<dropdownspeed) { ChangeDropdownSpeed(0.04f); issoftdropping = true; ResetLockDelay(); Ghostsetter(); } }

    public void Softdroprelease() { ChangeDropdownSpeed(dropdownspeed); issoftdropping = false; ResetLockDelay(); Ghostsetter(); }





    void Ghostreset() 
    {
        adminscript.ResetGhost();

    }

    void Ghostsetter() 
    {
        foreach (Transform child in transform)
        {
           

            var asd = child.GetComponent<ActiveBlock>();
            asd.Checkghostobject();

        }
    }


    bool Checkifplaced(Vector3 checkthis) 
    {

        foreach (Transform child in transform)
        {
            //child is your child transform
           
         var asd=  child.GetComponent<ActiveBlock>();
           if(asd.Checkmovement(checkthis)) {  return false; }

        }
        return true;
    }

    bool CheckDownTiles() 
    {
        foreach (Transform child in transform)
        {
            //child is your child transform

            var asd = child.GetComponent<ActiveBlock>();
            if (asd.Checkmovement(new Vector3(0,-1,0))) { return false; }

        }
        return true;

    }

}
