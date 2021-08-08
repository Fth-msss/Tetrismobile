using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InactiveBlock : MonoBehaviour
{

   
    Gameadmin adminscript;
    void Start()
    {
        
        adminscript = GameObject.Find("GameAdmin").GetComponent<Gameadmin>();

        adminscript.OnRemovedTile += Adminscript_OnRemovedTile;

    }

    private void Adminscript_OnRemovedTile(object sender, Gameadmin.OnOnRemovedTileEventArgs e)
    {
       
        if (e.removedtile <= transform.position.y) {/*go down one tile*/gameObject.transform.position += new Vector3(0, -1, 0); }
    }

    private void OnDestroy()
    {
        adminscript.OnRemovedTile -= Adminscript_OnRemovedTile;
    }
    public void Removeevents() 
    {
        adminscript.OnRemovedTile -= Adminscript_OnRemovedTile;
    }


}
