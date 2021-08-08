using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ActiveBlock : MonoBehaviour
{
  
    public void Checklineclear() 
    {
       Collider2D[]ad=  Physics2D.OverlapAreaAll(new Vector2(-10,transform.position.y-0.1f), new Vector2(10,transform.position.y+0.1f));

       

        int isitaline = 0;
        foreach (Collider2D blcoks in ad)
        {
            if (blcoks.gameObject.CompareTag("block")) { isitaline += 1; }

          

        }

        if (isitaline >= 10) 
        {
           Gameadmin asdf=  GameObject.Find("GameAdmin").GetComponent<Gameadmin>();
            asdf.lineclearcount += 1;
            
            foreach (Collider2D blcoks in ad)
            {
                if (blcoks.gameObject.CompareTag("block")) { Destroy(blcoks.gameObject); }
                



            }
            Gameadmin asd = GameObject.Find("GameAdmin").GetComponent<Gameadmin>();
            asd.Removetile(transform.position.y);
            //asd.CheckPerfectclear();


        } 
           
    }

public void Ghostreset() 
    {

        transform.position =new Vector3 (0,666,0);

               

    }

 public void Checkghostobject() 
    {
        Collider2D[] ad = Physics2D.OverlapAreaAll(new Vector2(transform.position.x - 0.1f, -20), new Vector2(transform.position.x + 0.1f, transform.position.y));


        float tallestblock=-0.5f;
       
        foreach (Collider2D blcoks in ad)
        {
            
            if (blcoks.gameObject.CompareTag("block"))
            {
                float tempp;
                if (Math.Abs(blcoks.transform.position.y) > 0) { tempp = blcoks.transform.position.y + 10; } else { tempp = blcoks.transform.position.y; }

                if (tempp > tallestblock) { tallestblock =tempp;}
            }



        }
      //  Debug.Log("tallest block y is:" + tallestblock) ;

        Gameadmin asd = GameObject.Find("GameAdmin").GetComponent<Gameadmin>();


       

        asd.Ghosttetriminocalled(tallestblock-9f, transform.position);
    }





    public bool Checkmovement(Vector3 checkhere) 
    {
     //   Checkghostobject();
        Vector3 checkedpos =checkhere + transform.position;

     
 
        if (Physics2D.OverlapPoint(checkedpos))
        {
            return true;  // found something  
        }
        else
        {
          
         
            return false;
        }


    }


}
