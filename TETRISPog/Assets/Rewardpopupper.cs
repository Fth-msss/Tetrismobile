using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rewardpopupper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        popup = GetComponent<Image>();
       // Changepopup(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Image popup;

    public Sprite Tetrispopup;
    public Sprite Tspin;
    public Sprite Tspindouble;
    public Sprite Tspintriple;
    public void Changepopup(int swit) 
    {
        switch (swit)
            
            {
            case 1://tetris
                popup.sprite = Tetrispopup;
                popup.SetNativeSize();

                break;
            case 2:
                popup.sprite = Tspin;
                popup.SetNativeSize();

                break;
            case 3:
                popup.sprite = Tspindouble;
                popup.SetNativeSize();

                break;
            case 4:
                popup.sprite = Tspintriple;
                popup.SetNativeSize();

                break;
            case 5:
                popup.sprite = Tspintriple;
                popup.SetNativeSize();

                break;
            case 6:
                popup.sprite = Tspintriple;
                popup.SetNativeSize();

                break;
                
        }

        Animator asd = GetComponent<Animator>();
        asd.SetTrigger("popup");



      

    }

}
