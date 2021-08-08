using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;
public class Gameadmin : MonoBehaviour
{
    public GameObject longtetrimino;
    public GameObject squaretetrimino;
    public GameObject stetrimino;
    public GameObject ztetrimino;
    public GameObject ltetrimino;
    public GameObject jtetrimino;
    public GameObject ttetrimino;

    public Sprite Isprite;
    public Sprite Lsprite;
    public Sprite Osprite;
    public Sprite Jsprite;
    public Sprite Zsprite;
    public Sprite Ssprite;
    public Sprite Tsprite;

    public Image Holdmino;


    public Image queueone;
    public Image queuetwo;
    public Image queuethree;
    public Image nextqueue;

    public Text leveltext;
    public Text scoretext;

    public int score;
    public int level;
    public int clearedlinecount=0;

    public bool Swaplock = true;

    public GameObject spawnlocation;//to spawn tetriminos.dont forget to make non I tetriminos spawn one line up
    //spawn tetriminos at line 666

public GameObject perfectchecker;//just checks if the bottom line is empty. something might be off with this and i probably should change this

public GameObject heldtetrimino;// used for next tetrimino i think?


    void Start()
    {
        movepopupscript = movepopup.GetComponent<Rewardpopupper>();
        Setupbagsystem();
        Spawntetrimino();
    }


    public GameObject ghostobject;
    private int ghostcounter=0;

    private int counternew = 0;
    private float usedghostpos = -9.5f;

    private Vector3 blockone;
    private Vector3 blocktwo;
    private Vector3 blockthree;
    private Vector3 blockfour;

    private float firstghost;
    private float secondghost;
    private float thirdghost;
    private float fourthghost;

    public void ResetGhost() 
    {
     ghostobject.transform.GetChild(0).transform.position= new Vector3(0, 6666, 0); 
       ghostobject.transform.GetChild(1).transform.position = new Vector3(0, 6666, 0); 
       ghostobject.transform.GetChild(2).transform.position = new Vector3(0, 6666, 0); 
       ghostobject.transform.GetChild(3).transform.position = new Vector3(0, 6666, 0); 

    }

    public void Ghosttetriminocalled(float ghostpos, Vector3 originalpos) 
    {
        if (ghostpos>usedghostpos) { usedghostpos = ghostpos; }
     //   Debug.Log("ghospos is:"+ghostpos+"usedghostis:"+usedghostpos);
        counternew++;
        if(counternew == 1)  { blockone = originalpos; firstghost = ghostpos; }
        if (counternew == 2) { blocktwo = originalpos; secondghost = ghostpos; }
        if (counternew == 3) { blockthree = originalpos; thirdghost = ghostpos; }
        if (counternew == 4) { blockfour = originalpos; fourthghost = ghostpos; Newfunctiontobindthemall(usedghostpos); counternew = 0;usedghostpos = -20; }
     //   Debug.Log(blockone+""+ blocktwo + "" + blockthree + ""+ blockfour);
       
    }



    bool Checkifempty(Vector3 Checkthis)  
    {
        if (Physics2D.OverlapPoint(Checkthis))
        {
            return false;
        }
        else
        {
            return true;// can spawn
        }
    }


    public void Newfunctiontobindthemall(float ghostposy) 
    {
        Transform demenepos = ghostobject.transform.GetChild(0);
        Transform demenepos1 = ghostobject.transform.GetChild(1);
        Transform demenepos2 = ghostobject.transform.GetChild(2);
        Transform demenepos3 = ghostobject.transform.GetChild(3);


        if (ghostposy == firstghost)
        {
            float newyposition = ghostposy;
         /*   if (blockone.x==blocktwo.x )    { newyposition += 1; }
            if (blockone.x == blockthree.x) { newyposition += 1; }
            if (blockone.x == blockfour.x)  { newyposition += 1; }*/


            bool a, b, c, d;

          a=  Checkifempty (new Vector3(blockone.x, newyposition, 0));
          b=  Checkifempty (new Vector3(blocktwo.x, blocktwo.y - blockone.y + newyposition, 0));
          c=  Checkifempty (new Vector3(blockthree.x, blockthree.y - blockone.y + newyposition, 0));
          d=  Checkifempty (new Vector3(blockfour.x, blockfour.y - blockone.y + newyposition, 0));


            if (a&&b&&c&&d)
            {
                demenepos.position = new Vector3(blockone.x, newyposition, 0);
                demenepos1.position = new Vector3(blocktwo.x, blocktwo.y - blockone.y + newyposition, 0);
                demenepos2.position = new Vector3(blockthree.x, blockthree.y - blockone.y + newyposition, 0);
                demenepos3.position = new Vector3(blockfour.x, blockfour.y - blockone.y + newyposition, 0);
            }
            else 
            {
                newyposition += 1;
                a = Checkifempty(new Vector3(blockone.x, newyposition, 0));
                b = Checkifempty(new Vector3(blocktwo.x, blocktwo.y - blockone.y + newyposition, 0));
                c = Checkifempty(new Vector3(blockthree.x, blockthree.y - blockone.y + newyposition, 0));
                d = Checkifempty(new Vector3(blockfour.x, blockfour.y - blockone.y + newyposition, 0));

                if (a && b && c && d)
                {
                    demenepos.position = new Vector3(blockone.x, newyposition, 0);
                    demenepos1.position = new Vector3(blocktwo.x, blocktwo.y - blockone.y + newyposition, 0);
                    demenepos2.position = new Vector3(blockthree.x, blockthree.y - blockone.y + newyposition, 0);
                    demenepos3.position = new Vector3(blockfour.x, blockfour.y - blockone.y + newyposition, 0);
                }
                else 
                {
                    newyposition += 1;
                    a = Checkifempty(new Vector3(blockone.x, newyposition, 0));
                    b = Checkifempty(new Vector3(blocktwo.x, blocktwo.y - blockone.y + newyposition, 0));
                    c = Checkifempty(new Vector3(blockthree.x, blockthree.y - blockone.y + newyposition, 0));
                    d = Checkifempty(new Vector3(blockfour.x, blockfour.y - blockone.y + newyposition, 0));

                    if (a && b && c && d)
                    {
                        demenepos.position = new Vector3(blockone.x, newyposition, 0);
                        demenepos1.position = new Vector3(blocktwo.x, blocktwo.y - blockone.y + newyposition, 0);
                        demenepos2.position = new Vector3(blockthree.x, blockthree.y - blockone.y + newyposition, 0);
                        demenepos3.position = new Vector3(blockfour.x, blockfour.y - blockone.y + newyposition, 0);
                    }
                    else
                    {
                        newyposition += 1;
                        a = Checkifempty(new Vector3(blockone.x, newyposition, 0));
                        b = Checkifempty(new Vector3(blocktwo.x, blocktwo.y - blockone.y + newyposition, 0));
                        c = Checkifempty(new Vector3(blockthree.x, blockthree.y - blockone.y + newyposition, 0));
                        d = Checkifempty(new Vector3(blockfour.x, blockfour.y - blockone.y + newyposition, 0));

                        if (a && b && c && d)
                        {
                            demenepos.position = new Vector3(blockone.x, newyposition, 0);
                            demenepos1.position = new Vector3(blocktwo.x, blocktwo.y - blockone.y + newyposition, 0);
                            demenepos2.position = new Vector3(blockthree.x, blockthree.y - blockone.y + newyposition, 0);
                            demenepos3.position = new Vector3(blockfour.x, blockfour.y - blockone.y + newyposition, 0);
                        }
                        else
                        {
                            newyposition += 1;
                            a = Checkifempty(new Vector3(blockone.x, newyposition, 0));
                            b = Checkifempty(new Vector3(blocktwo.x, blocktwo.y - blockone.y + newyposition, 0));
                            c = Checkifempty(new Vector3(blockthree.x, blockthree.y - blockone.y + newyposition, 0));
                            d = Checkifempty(new Vector3(blockfour.x, blockfour.y - blockone.y + newyposition, 0));

                          
                            if (a && b && c && d)
                            {
                                Debug.Log("we better be here");
                                demenepos.position = new Vector3(blockone.x, newyposition, 0);
                                demenepos1.position = new Vector3(blocktwo.x, blocktwo.y - blockone.y + newyposition, 0);
                                demenepos2.position = new Vector3(blockthree.x, blockthree.y - blockone.y + newyposition, 0);
                                demenepos3.position = new Vector3(blockfour.x, blockfour.y - blockone.y + newyposition, 0);
                            }
                        }
                    }

                }

            }

          

           

        }
        else if (ghostposy == secondghost)
        {
            float newyposition = ghostposy;
      //      if (blocktwo.x == blockone.x) { newyposition += 1; }
      //      if (blocktwo.x == blockthree.x) { newyposition += 1; }
       //     if (blocktwo.x == blockfour.x) { newyposition += 1; }




            bool a, b, c, d;

            a = Checkifempty(new Vector3(blocktwo.x, newyposition, 0));
            b = Checkifempty(new Vector3(blockone.x, blockone.y - blocktwo.y + newyposition, 0));
            c = Checkifempty(new Vector3(blockthree.x, blockthree.y - blocktwo.y + newyposition, 0));
            d = Checkifempty(new Vector3(blockfour.x, blockfour.y - blocktwo.y + newyposition, 0));


            if (a && b && c && d)
            {
                
                demenepos1.position = new Vector3(blocktwo.x, newyposition, 0);
                demenepos.position = new Vector3(blockone.x, blockone.y - blocktwo.y + newyposition, 0);
                demenepos2.position = new Vector3(blockthree.x, blockthree.y - blocktwo.y + newyposition, 0);
                demenepos3.position = new Vector3(blockfour.x, blockfour.y - blocktwo.y + newyposition, 0);
            }
            else
            {
                newyposition += 1;
                a = Checkifempty(new Vector3(blocktwo.x, newyposition, 0));
                b = Checkifempty(new Vector3(blockone.x, blockone.y - blocktwo.y + newyposition, 0));
                c = Checkifempty(new Vector3(blockthree.x, blockthree.y - blocktwo.y + newyposition, 0));
                d = Checkifempty(new Vector3(blockfour.x, blockfour.y - blocktwo.y + newyposition, 0));

                if (a && b && c && d)
                {
                   
                    demenepos1.position = new Vector3(blocktwo.x, newyposition, 0);
                    demenepos.position = new Vector3(blockone.x, blockone.y - blocktwo.y + newyposition, 0);
                    demenepos2.position = new Vector3(blockthree.x, blockthree.y - blocktwo.y + newyposition, 0);
                    demenepos3.position = new Vector3(blockfour.x, blockfour.y - blocktwo.y + newyposition, 0);
                }
                else
                {
                    newyposition += 1;
                    a = Checkifempty(new Vector3(blocktwo.x, newyposition, 0));
                    b = Checkifempty(new Vector3(blockone.x, blockone.y - blocktwo.y + newyposition, 0));
                    c = Checkifempty(new Vector3(blockthree.x, blockthree.y - blocktwo.y + newyposition, 0));
                    d = Checkifempty(new Vector3(blockfour.x, blockfour.y - blocktwo.y + newyposition, 0));

                    if (a && b && c && d)
                    {
                       
                        demenepos1.position = new Vector3(blocktwo.x, newyposition, 0);
                        demenepos.position = new Vector3(blockone.x, blockone.y - blocktwo.y + newyposition, 0);
                        demenepos2.position = new Vector3(blockthree.x, blockthree.y - blocktwo.y + newyposition, 0);
                        demenepos3.position = new Vector3(blockfour.x, blockfour.y - blocktwo.y + newyposition, 0);
                    }
                    else
                    {
                        newyposition += 1;
                        a = Checkifempty(new Vector3(blocktwo.x, newyposition, 0));
                        b = Checkifempty(new Vector3(blockone.x, blockone.y - blocktwo.y + newyposition, 0));
                        c = Checkifempty(new Vector3(blockthree.x, blockthree.y - blocktwo.y + newyposition, 0));
                        d = Checkifempty(new Vector3(blockfour.x, blockfour.y - blocktwo.y + newyposition, 0));

                        if (a && b && c && d)
                        {
                           
                            demenepos1.position = new Vector3(blocktwo.x, newyposition, 0);
                            demenepos.position = new Vector3(blockone.x, blockone.y - blocktwo.y + newyposition, 0);
                            demenepos2.position = new Vector3(blockthree.x, blockthree.y - blocktwo.y + newyposition, 0);
                            demenepos3.position = new Vector3(blockfour.x, blockfour.y - blocktwo.y + newyposition, 0);
                        }
                        else
                        {
                            newyposition += 1;
                            a = Checkifempty(new Vector3(blocktwo.x, newyposition, 0));
                            b = Checkifempty(new Vector3(blockone.x, blockone.y - blocktwo.y + newyposition, 0));
                            c = Checkifempty(new Vector3(blockthree.x, blockthree.y - blocktwo.y + newyposition, 0));
                            d = Checkifempty(new Vector3(blockfour.x, blockfour.y - blocktwo.y + newyposition, 0));

                            Debug.Log("we should never be here");
                            if (a && b && c && d)
                            {
                                
                                Debug.Log("we better be here");
                                demenepos1.position = new Vector3(blocktwo.x, newyposition, 0);
                                demenepos.position = new Vector3(blockone.x, blockone.y - blocktwo.y + newyposition, 0);
                                demenepos2.position = new Vector3(blockthree.x, blockthree.y - blocktwo.y + newyposition, 0);
                                demenepos3.position = new Vector3(blockfour.x, blockfour.y - blocktwo.y + newyposition, 0);
                            }
                        }
                    }

                }

            }







          

        }









        else if (ghostposy == thirdghost)
        {
            float newyposition = ghostposy;
       //     if (blockthree.x == blocktwo.x) { newyposition += 1; }
       //     if (blockthree.x == blockone.x) { newyposition += 1; }
      //      if (blockthree.x == blockfour.x) { newyposition += 1; }






            bool a, b, c, d;

            a = Checkifempty(new Vector3(blockthree.x, newyposition, 0));
            b = Checkifempty(new Vector3(blocktwo.x, blocktwo.y - blockthree.y + newyposition, 0));
            c = Checkifempty(new Vector3(blockone.x, blockone.y - blockthree.y + newyposition, 0));
            d = Checkifempty(new Vector3(blockfour.x, blockfour.y - blockthree.y + newyposition, 0));


            if (a && b && c && d)
            {
            
                
                demenepos2.position = new Vector3(blockthree.x, newyposition, 0);
                demenepos1.position = new Vector3(blocktwo.x, blocktwo.y - blockthree.y + newyposition, 0);
                demenepos.position = new Vector3(blockone.x, blockone.y - blockthree.y + newyposition, 0);
                demenepos3.position = new Vector3(blockfour.x, blockfour.y - blockthree.y + newyposition, 0);
            }
            else
            {
                newyposition += 1;
                a = Checkifempty(new Vector3(blockthree.x, newyposition, 0));
                b = Checkifempty(new Vector3(blocktwo.x, blocktwo.y - blockthree.y + newyposition, 0));
                c = Checkifempty(new Vector3(blockone.x, blockone.y - blockthree.y + newyposition, 0)) ;
                d = Checkifempty(new Vector3(blockfour.x, blockfour.y - blockthree.y + newyposition, 0));

                if (a && b && c && d)
                {
               
                    demenepos2.position = new Vector3(blockthree.x, newyposition, 0);
                    demenepos1.position = new Vector3(blocktwo.x, blocktwo.y - blockthree.y + newyposition, 0);
                    demenepos.position = new Vector3(blockone.x, blockone.y - blockthree.y + newyposition, 0);
                    demenepos3.position = new Vector3(blockfour.x, blockfour.y - blockthree.y + newyposition, 0);
                }
                else
                {
                    newyposition += 1;
                    a = Checkifempty(new Vector3(blockthree.x, newyposition, 0));
                    b = Checkifempty(new Vector3(blocktwo.x, blocktwo.y - blockthree.y + newyposition, 0));
                    c = Checkifempty(new Vector3(blockone.x, blockone.y - blockthree.y + newyposition, 0));
                    d = Checkifempty(new Vector3(blockfour.x, blockfour.y - blockthree.y + newyposition, 0));

                    if (a && b && c && d)
                    {

                        demenepos2.position = new Vector3(blockthree.x, newyposition, 0);
                        demenepos1.position = new Vector3(blocktwo.x, blocktwo.y - blockthree.y + newyposition, 0);
                        demenepos.position = new Vector3(blockone.x, blockone.y - blockthree.y + newyposition, 0);
                        demenepos3.position = new Vector3(blockfour.x, blockfour.y - blockthree.y + newyposition, 0);
                    }
                    else
                    {
                        newyposition += 1;
                        a = Checkifempty(new Vector3(blockthree.x, newyposition, 0));
                        b = Checkifempty(new Vector3(blocktwo.x, blocktwo.y - blockthree.y + newyposition, 0));
                        c = Checkifempty(new Vector3(blockone.x, blockone.y - blockthree.y + newyposition, 0));
                        d = Checkifempty(new Vector3(blockfour.x, blockfour.y - blockthree.y + newyposition, 0));

                        if (a && b && c && d)
                        {
                            demenepos2.position = new Vector3(blockthree.x, newyposition, 0);
                            demenepos1.position = new Vector3(blocktwo.x, blocktwo.y - blockthree.y + newyposition, 0);
                            demenepos.position = new Vector3(blockone.x, blockone.y - blockthree.y + newyposition, 0);
                            demenepos3.position = new Vector3(blockfour.x, blockfour.y - blockthree.y + newyposition, 0);
                        }
                        else
                        {
                            newyposition += 1;
                            a = Checkifempty(new Vector3(blockthree.x, newyposition, 0));
                            b = Checkifempty(new Vector3(blocktwo.x, blocktwo.y - blockthree.y + newyposition, 0));
                            c = Checkifempty(new Vector3(blockone.x, blockone.y - blockthree.y + newyposition, 0));
                            d = Checkifempty(new Vector3(blockfour.x, blockfour.y - blockthree.y + newyposition, 0));

                            Debug.Log("we should never be here");
                            if (a && b && c && d)
                            {
                                Debug.Log("we better be here");
                                demenepos2.position = new Vector3(blockthree.x, newyposition, 0);
                                demenepos1.position = new Vector3(blocktwo.x, blocktwo.y - blockthree.y + newyposition, 0);
                                demenepos.position = new Vector3(blockone.x, blockone.y - blockthree.y + newyposition, 0);
                                demenepos3.position = new Vector3(blockfour.x, blockfour.y - blockthree.y + newyposition, 0);
                            }
                        }
                    }

                }

            }





          

        }
        else if (ghostposy == fourthghost)
        {
            float newyposition = ghostposy;
          
       //     if (blockfour.x == blocktwo.x) { newyposition += 1; }
       //     if (blockfour.x == blockthree.x) { newyposition += 1; }
       //     if (blockfour.x == blockone.x) { newyposition += 1; }


            bool a, b, c, d;

            a = Checkifempty(new Vector3(blockfour.x, newyposition, 0));
            b = Checkifempty(new Vector3(blocktwo.x, blocktwo.y - blockfour.y + newyposition, 0));
            c = Checkifempty(new Vector3(blockone.x, blockone.y - blockfour.y + newyposition, 0));
            d = Checkifempty(new Vector3(blockthree.x, blockthree.y - blockfour.y + newyposition, 0));


            if (a && b && c && d)
            {
                demenepos3.position = new Vector3(blockfour.x, newyposition, 0);
                demenepos1.position = new Vector3(blocktwo.x, blocktwo.y - blockfour.y + newyposition, 0);
                demenepos.position = new Vector3(blockone.x, blockone.y - blockfour.y + newyposition, 0);
                demenepos2.position = new Vector3(blockthree.x, blockthree.y - blockfour.y + newyposition, 0);
            }
            else
            {
                newyposition += 1;
                a = Checkifempty(new Vector3(blockfour.x, newyposition, 0));
                b = Checkifempty(new Vector3(blocktwo.x, blocktwo.y - blockfour.y + newyposition, 0));
                c = Checkifempty(new Vector3(blockone.x, blockone.y - blockfour.y + newyposition, 0));
                d = Checkifempty(new Vector3(blockthree.x, blockthree.y - blockfour.y + newyposition, 0));

                if (a && b && c && d)
                {
                    demenepos3.position = new Vector3(blockfour.x, newyposition, 0);
                    demenepos1.position = new Vector3(blocktwo.x, blocktwo.y - blockfour.y + newyposition, 0);
                    demenepos.position = new Vector3(blockone.x, blockone.y - blockfour.y + newyposition, 0);
                    demenepos2.position = new Vector3(blockthree.x, blockthree.y - blockfour.y + newyposition, 0);
                }
                else
                {
                    newyposition += 1;
                    a = Checkifempty(new Vector3(blockfour.x, newyposition, 0));
                    b = Checkifempty(new Vector3(blocktwo.x, blocktwo.y - blockfour.y + newyposition, 0));
                    c = Checkifempty(new Vector3(blockone.x, blockone.y - blockfour.y + newyposition, 0));
                    d = Checkifempty(new Vector3(blockthree.x, blockthree.y - blockfour.y + newyposition, 0));

                    if (a && b && c && d)
                    {
                        demenepos3.position = new Vector3(blockfour.x, newyposition, 0);
                        demenepos1.position = new Vector3(blocktwo.x, blocktwo.y - blockfour.y + newyposition, 0);
                        demenepos.position = new Vector3(blockone.x, blockone.y - blockfour.y + newyposition, 0);
                        demenepos2.position = new Vector3(blockthree.x, blockthree.y - blockfour.y + newyposition, 0);
                    }
                    else
                    {
                        newyposition += 1;
                        a = Checkifempty(new Vector3(blockfour.x, newyposition, 0));
                        b = Checkifempty(new Vector3(blocktwo.x, blocktwo.y - blockfour.y + newyposition, 0));
                        c = Checkifempty(new Vector3(blockone.x, blockone.y - blockfour.y + newyposition, 0));
                        d = Checkifempty(new Vector3(blockthree.x, blockthree.y - blockfour.y + newyposition, 0));

                        if (a && b && c && d)
                        {
                            demenepos3.position = new Vector3(blockfour.x, newyposition, 0);
                            demenepos1.position = new Vector3(blocktwo.x, blocktwo.y - blockfour.y + newyposition, 0);
                            demenepos.position = new Vector3(blockone.x, blockone.y - blockfour.y + newyposition, 0);
                            demenepos2.position = new Vector3(blockthree.x, blockthree.y - blockfour.y + newyposition, 0);
                        }
                        else
                        {
                            newyposition += 1;
                            a = Checkifempty(new Vector3(blockfour.x, newyposition, 0));
                            b = Checkifempty(new Vector3(blocktwo.x, blocktwo.y - blockfour.y + newyposition, 0));
                            c = Checkifempty(new Vector3(blockone.x, blockone.y - blockfour.y + newyposition, 0));
                            d = Checkifempty(new Vector3(blockthree.x, blockthree.y - blockfour.y + newyposition, 0));

                            Debug.Log("we should never be here");
                            if (a && b && c && d)
                            {
                                Debug.Log("we better be here");
                                demenepos3.position = new Vector3(blockfour.x, newyposition, 0);
                                demenepos1.position = new Vector3(blocktwo.x, blocktwo.y - blockfour.y + newyposition, 0);
                                demenepos.position = new Vector3(blockone.x, blockone.y - blockfour.y + newyposition, 0);
                                demenepos2.position = new Vector3(blockthree.x, blockthree.y - blockfour.y + newyposition, 0);
                            }
                        }
                    }

                }

            }










           


        }


    }







    public void Resetupghosttetrimino(float ghostpos,float originalpos) 
    {
        if (ghostcounter == 4) { ghostcounter = 0; }
        Transform childtransform= ghostobject.transform.GetChild(ghostcounter);

        childtransform.position = new Vector3(originalpos, ghostpos,0);
        ghostcounter++;
        


          
    }




    //calls event after line clear to call all locked minos a tile down
    public event EventHandler<OnOnRemovedTileEventArgs> OnRemovedTile;
    public class OnOnRemovedTileEventArgs : EventArgs
    {
        public float removedtile;
    }
    public void Removetile(float tiley)
    {
        OnRemovedTile?.Invoke(this, new OnOnRemovedTileEventArgs { removedtile = tiley });
    }


    //this method is for swapping tetriminos and somewhat stupid maybe
    void Checkwhichtetrimino(GameObject asd)// to be used with swaptetrimino
    {



        var asdf = asd.GetComponent<RotationSystem>();


        switch (asdf.tetriminotype)
        {

            case "T":

                heldtetrimino = ttetrimino;
                Holdmino.sprite = Tsprite;
                break;

            case "L":
                heldtetrimino = ltetrimino;
                Holdmino.sprite = Lsprite;
                break;

            case "I":
                heldtetrimino = longtetrimino;
                Holdmino.sprite = Isprite;
                break;

            case "Z":
                heldtetrimino = ztetrimino;
                Holdmino.sprite = Zsprite;
                break;

            case "S":
                heldtetrimino = stetrimino;
                Holdmino.sprite = Ssprite;
                break;

            case "J":
                heldtetrimino = jtetrimino;
                Holdmino.sprite = Jsprite;
                break;

            case "O":
                heldtetrimino = squaretetrimino;
                Holdmino.sprite = Osprite;
                break;

            default:
                Debug.Log("HOW DID YOU BREAK THIS:");
                break;
        }


    }
    public void Swaptetrimino(GameObject asd)// add a limit(one) to swapping tetriminos until new tetrimino spawn
    {
        if (heldtetrimino.name == "emptyhold") { Checkwhichtetrimino(asd); Spawntetrimino(); }
        else
        {

            Instantiate(heldtetrimino, spawnlocation.transform);
            Checkwhichtetrimino(asd);
          
        }
    }



    int addbagtimer=0;
    public void Lockedmino() 
    {
       
        if (isalive)
        {
            bool cleardelay = false;
            Swaplock = true;

            if (lineclearcount == 0) { cccombo = 0; }//if no lineclear and ready to spawn tetrimino remove combo

            if (lineclearcount > 0) { /*CheckPerfectclear();*/ ScoreCalculate(false); lineclearcount = 0; cleardelay = true; }//if there are line clears check score and perfectclear

            addbagtimer++; if (addbagtimer == 7) { Nwaddrange();addbagtimer = 0; }// add blocks to bag

            
            if (cleardelay) { Invoke("Spawntetrimino", 0.15f); }
            else { Invoke("Spawntetrimino", 0.02f); }
            
        }


    }

    public void Spawntetrimino() 
    {
        Instantiate(bagofminos.Peek(), spawnlocation.transform);// spawn tetrimino
        bagofminos.Dequeue();

        var asdf = bagofminos.Peek().GetComponent<RotationSystem>();
        nextqueue.sprite = asdf.forui;

    }

 /*
    public void Spawnnewtetrimino()
    {
        if (isalive)
        {
            if (lineclearcount == 0) { cccombo = 0; }//if no lineclear and ready to spawn tetrimino remove combo

            if (lineclearcount > 0) {  ScoreCalculate(false); lineclearcount = 0; }//if there are line clears check score and perfectclear

            //no more perfect clears for you Ragey(until i fix it)
          
            Instantiate(bagofminos.Peek(), spawnlocation.transform);// spawn tetrimino

            bagofminos.Dequeue();

            var asdf = bagofminos.Peek().GetComponent<RotationSystem>();//get sprite to show in next tetrimino
            nextqueue.sprite = asdf.forui;

            a++;   if (a == 3) { Nwaddrange(); }// add blocks to bag
            




        }
       
    }
    */








    public GameObject[] alltetriminos;//=new GameObject[14];//this stays

   
    public GameObject[] Secondbag;//to add 7 tiles to actual game

    private Queue<GameObject> bagofminos;
    void Setupbagsystem() 
    {

        Reshuffle(Secondbag);
        bagofminos = new Queue<GameObject>();

        
        foreach (GameObject mino in Secondbag)
        {
            bagofminos.Enqueue(mino);
           
        }
        Reshuffle(Secondbag);
        foreach (GameObject mino in Secondbag)
        {
            bagofminos.Enqueue(mino);
            
        }

    }


    void Nwaddrange() //base for new 7bag system
    {
       
            Reshuffle(Secondbag);
            foreach (GameObject mino in Secondbag)
            {
                bagofminos.Enqueue(mino);

            }
           // a = 0; 
    }

























    //anything about 7bag system further on is horrible 

    
   


    //actually important
    void Reshuffle(GameObject[] bag)
    {
       
        // Knuth shuffle algorithm
        for (int t = 0; t < bag.Length; t++)
        {
            GameObject tmp = bag[t];

            int r = UnityEngine.Random.Range(t, bag.Length);
            //Debug.Log("bagt is: " + bag[t] + "bagr is:" + bag[r]+"first call");
            bag[t] = bag[r];
            
            bag[r] = tmp;
           // Debug.Log("bagt is: "+ bag[t] +"bagr is:"+ bag[r]);
        }
    }


    //score calculating


    /// <summary>
    /// 
    /// Single	100 × level
    /// Double	300 × level
    /// Triple	500 × level
    /// Tetris	800 × level; difficult
    /// T-Spin Mini no lines	100 × level
    /// T-Spin no lines	400 × level
    /// T-Spin Mini Single	200 × level; difficult
    /// T-Spin Single	800 × level; difficult
    /// T-Spin Mini Double (if present)	400 × level; difficult
    /// T-Spin Double	1200 × level; difficult
    /// T-Spin Triple	1600 × level; difficult
    /// Back-to-Back difficult line clears	*3/2 (for example, back to back Tetris: 1200 × level)
    /// Combo	50 × combo count × level
    /// Soft drop	1 per cell
    /// Hard drop	2 per cell
    /// 
    /// 
    /// 
    /// Single-line perfect clear	800 × level
    /// Double-line perfect clear	1200 × level
    /// Triple-line perfect clear	1800 × level
    /// Tetris perfect clear	2000 × level
    /// Back-to-back Tetris perfect clear	3200 × level

    //after a line clear:check how many lines are cleared (and send the count to calculator)
    //check if it is a perfect clear (just checks the bottom tiles(depending of how many tiles are cleared) if its empty it is a perfect clear)
    //check if it is a t spin(do something with a "last action"string that changes with rotation and dropping down/going left right)
    //check in a different method if it is back to back(just a bool which gets updated after every score calculation) this just checks if a bool is true and if it is just multiplies score you would get 
    //also checks combo for last






    //CHECK THIS PLACE

    public bool islastdifficult=false;
    public int addedscore;
    public int lineclearcount;
    public int cccombo;
    public void ScoreCalculate(bool istspin) // something is definitely wrong here and or can be optimized
    {
       

        if (istspin) 
        {
            if      (lineclearcount == 0) { Debug.Log("gz you did a t spin which is completely useless"); }
            else if (lineclearcount == 3) { Debug.Log("oh baby a triple(tspin)"); CalculateScore("tspintriple"); }
            else if (lineclearcount == 2) { Debug.Log("double tspin"); CalculateScore("tspindouble"); }
            else if (lineclearcount == 1) { Debug.Log("single tspin"); CalculateScore("tspinsingle"); }
        }
        else 
        {
            if      (lineclearcount == 4) { CalculateScore("tetris"); }
            else if (lineclearcount == 3) { CalculateScore("tripleline"); }
            else if (lineclearcount == 2) { CalculateScore("doubleline"); }
            else if (lineclearcount == 1) { CalculateScore("singleline"); }
        }



        clearedlinecount += lineclearcount;//as same as they sound one is for holding all cleared lines count 

        if (lineclearcount > 0) { cccombo++;Addcombopoint(); }
        else { cccombo = 0; }
        lineclearcount = 0;
        Checklevelup();

    }


    //this has no limit.add a limit and get it faster with every limit
    public float ddownspeed=0.5f;
    public void Checklevelup() { if (clearedlinecount >= 10) 
        {
            if (level < 20) {
                level += 1; leveltext.text = level.ToString();
                if (ddownspeed >= 0.06f) { ddownspeed -= 0.05f; }
            }
            clearedlinecount -= 10; 
           

        } 
    
                               }


    
    

    public void CheckPerfectclear() 
    {
        bool perfect = true;
        Collider2D[] ad = Physics2D.OverlapAreaAll(new Vector2(-10, perfectchecker.transform.position.y - 0.1f), new Vector2(10, perfectchecker.transform.position.y + 0.1f));

        foreach (Collider2D blcoks in ad)
        {
            if (blcoks.gameObject.tag.Equals("block")) { perfect = false;  }

        }
       
        if (perfect) {
            Debug.Log("definitely perfect yeah");
            if (lineclearcount==1) { CalculateScore("singleperfect"); }
            if (lineclearcount == 2) { CalculateScore("doubleperfect"); }
            if (lineclearcount == 3) { CalculateScore("tripleperfect"); }
            if (lineclearcount == 4) { CalculateScore("tetrisperfect"); }


            }
    }

    public GameObject movepopup;
    Rewardpopupper movepopupscript;
    public void CalculateScore(string a) //perfectly fine DONT DEAD OPEN INSIDE
    {
        bool isdifficult;
       
     
        switch (a) 
        {


            case "singleline":
                addedscore = 100 * level;
                islastdifficult = false;
                isdifficult = false;

                Debug.Log("scoredsingleline");

               
                Finalizescore(addedscore);

                return;


            case "doubleline":
                addedscore = 300 * level;
                islastdifficult = false;
                isdifficult = false;

                Debug.Log("scoreddoubleline");

               
                Finalizescore(addedscore);
                return;

            case "tripleline":
                addedscore = 500 * level;
                islastdifficult = false;
                isdifficult = false;

                Debug.Log("scoredtripleline");

              
                Finalizescore(addedscore);
                return;

            case "tetris"://difficult
                movepopupscript.Changepopup(1);

                addedscore = 800 * level;
                isdifficult = true;


                Checkbacktoback(isdifficult);
                
              
                return;
                                                    //tspins
            case "tspinmininoline":
                addedscore = 100 * level;
               
                Finalizescore(addedscore);
                return;

            case "tspinnoline":
                addedscore = 400 * level;
               
                Finalizescore(addedscore);
                return;

            case "tspinminisingle"://difficult
                addedscore = 200 * level;
                isdifficult = true;
                Checkbacktoback(isdifficult);
              
                return;

            case "tspinsingle"://difficult
                addedscore = 800 * level;
                isdifficult = true;
                Checkbacktoback(isdifficult);
               
                return;

            case "tspinminidouble"://difficult
                addedscore = 400 * level;
                isdifficult = true;
                Checkbacktoback(isdifficult);
              
                return;

            case "tspindouble"://difficult
                movepopupscript.Changepopup(3);
                addedscore = 1200 * level;
                isdifficult = true;
                Checkbacktoback(isdifficult);
                
                return;

            case "tspintriple"://difficult
                movepopupscript.Changepopup(4);
                addedscore = 1600 * level;
                isdifficult = true;
                Checkbacktoback(isdifficult);
               
                return;

                                            //perfects
            case "singleperfect":
                
                addedscore = 800 * level;
                Finalizescore(addedscore);
                return;

            case "doubleperfect":
               
                addedscore = 1200 * level;
                Finalizescore(addedscore);
                return;

            case "tripleperfect":
                
                addedscore = 1800 * level;
                Finalizescore(addedscore);
                return;

            case "tetrisperfect":
                
                addedscore = 2000 * level;
                isdifficult = true;
               Checkbacktoback(isdifficult);
               
                return;

                /*   default:
                       Debug.Log("someting broke really hard");
                       return;*/

        }
        Debug.Log("what");
     
    }
   





    //combo might be a little op beause it starts from actual first level instead of second line clear
    //also broken because i dont know where i should check for combos(t spins break it)
    void Addcombopoint() { int a = 50 * cccombo * level;Debug.Log("combo is:"+cccombo+" points gained from combo:"+a); Finalizescore(a); }



   public Animator btbanim;
    
   
    void Checkbacktoback(bool isdifficult) 
    {
        Debug.Log("please help:"+islastdifficult+""+isdifficult);
        if (islastdifficult==true&&isdifficult == true) { addedscore = addedscore * 3 / 2; btbanim.SetTrigger("b2b"); Finalizescore(addedscore); }
        else { Finalizescore(addedscore); }
        islastdifficult = isdifficult;
    }


   //also works
   public void Finalizescore(int addedscore) {
        
        
        score += addedscore;
        scoretext.text = score.ToString();
    }




    //death...also works 

    public Text oldun;// TAM BIR OMEGALUL ANI
    private bool isalive = true;
   public void Enableoldun() { oldun.enabled = true;isalive = false; }


  
    


}
