using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationSystem : MonoBehaviour
{

    public int currentposition = 0;
    public int wantedposition = 0;
    public string tetriminotype;
    public Sprite forui;



    //    0
    //  3   1
    //    2
    // a piece  can be in 0,1,2,3 
    //0 is top 1 is right 2 is down 3 is left
    //to change rotation simply sets state from 0 to 1 or 3 etc.
    //states are tetrimino dependant and have a method for everyone of them
    //
    Vector3 middle = new Vector3(0f, 0f, 0);//checks middle important for tspin mechanics
    Vector3 left = new Vector3(-1f, 0.0f, 0);//checks left  -1  0
    Vector3 right = new Vector3(1f, 0.0f, 0);//checks right  1  0
    Vector3 up = new Vector3(0f, 1.0f, 0);//checks up     0  1
    Vector3 down = new Vector3(0f, -1.0f, 0);//checks down   0 -1

    Vector3 topleft = new Vector3(-1f, 1.0f, 0);//checks top left        -1  1
    Vector3 topright = new Vector3(1f, 1.0f, 0);//checks top right        1  1
    Vector3 botleft = new Vector3(-1f, -1.0f, 0);//checks bottom left     -1 -1
    Vector3 botright = new Vector3(1f, -1.0f, 0);//checks bottom right     1 -1

    //bi I bloğu neler çektiriyor bana

    //0.5f 0.5f base is the top right of the base 

    /*
    Vector3 fbf11 = new Vector3(-2f, 2f, 0);
    Vector3 fbf12 = new Vector3(-1f, 2f, 0);
    Vector3 fbf13 = new Vector3(-0f, 2f, 0);
    Vector3 fbf14 = new Vector3( 0f, 2f, 0);
    Vector3 fbf21 = new Vector3(-2f, 1f, 0);
    Vector3 fbf22 = new Vector3(-1f, 1f, 0);
    Vector3 fbf23 = new Vector3(-0f, 1f, 0);
    Vector3 fbf24 = new Vector3( 0f, 1f, 0);
    Vector3 fbf31 = new Vector3(-2f, 0f, 0);
    Vector3 fbf32 = new Vector3(-1f, 0f, 0);
    Vector3 fbf33 = new Vector3(-0f, 0f, 0);
    Vector3 fbf34 = new Vector3( 0f, 0f, 0);
    Vector3 fbf41 = new Vector3(-2f, -0f, 0);
    Vector3 fbf42 = new Vector3(-1f, -0f, 0);
    Vector3 fbf43 = new Vector3(-0f, -0f, 0);
    Vector3 fbf44 = new Vector3( 0f, -0f, 0);
    */
    // 1.5f 0.5f -0.5f -1.5f
    
    Vector3 fbf11 = new Vector3(-1.5f, 1.5f, 0);
    Vector3 fbf12 = new Vector3(-0.5f, 1.5f, 0);
    Vector3 fbf13 = new Vector3( 0.5f, 1.5f, 0);
    Vector3 fbf14 = new Vector3( 1.5f, 1.5f, 0);

    Vector3 fbf21 = new Vector3(-1.5f, 0.5f, 0);
    Vector3 fbf22 = new Vector3(-0.5f, 0.5f, 0);
    Vector3 fbf23 = new Vector3( 0.5f, 0.5f, 0);
    Vector3 fbf24 = new Vector3( 1.5f, 0.5f, 0);

    Vector3 fbf31 = new Vector3(-1.5f, -0.5f, 0);
    Vector3 fbf32 = new Vector3(-0.5f, -0.5f, 0);
    Vector3 fbf33 = new Vector3( 0.5f, -0.5f, 0);
    Vector3 fbf34 = new Vector3( 1.5f, -0.5f, 0);

    Vector3 fbf41 = new Vector3(-1.5f,-1.5f, 0);
    Vector3 fbf42 = new Vector3(-0.5f,-1.5f, 0);
    Vector3 fbf43 = new Vector3( 0.5f,-1.5f, 0);
    Vector3 fbf44 = new Vector3( 1.5f,-1.5f, 0);


    // (-2.5f, 2.5f)(-1.5f, 2.5f)(-0.5f, 2.5f)( 0.5f, 2.5f)
    // (-2.5f, 1.5f)(-1.5f, 1.5f)(-0.5f, 1.5f)( 0.5f, 1.5f)
    // (-2.5f, 0.5f)(-1.5f, 0.5f)(-0.5f, 0.5f)( 0.5f, 0.5f)
    // (-2.5f,-0.5f)(-1.5f,-0.5f)(-0.5f,-0.5f)( 0.5f,-0.5f)

    bool Checkrotation(Vector3 Checkthis)// ORİGİNAL DONT  
    {
        if (Physics2D.OverlapPoint(Checkthis+transform.position))
        {
            return false;
        }
        else
        {
            return true;// can spawn
        }
    }


    bool Wallkicktest(Vector3 first, Vector3 second,Vector3 third,Vector3 fourth,Vector3 test) 
    {
        if (Checkrotation(first + test)) { if (Checkrotation(second +test)) { if (Checkrotation(third + test)) { if (Checkrotation(fourth +test)) { return true; } } } }
       
        return false;
    }

    void ApplyRotation(Vector3 first, Vector3 second, Vector3 third, Vector3 fourth,Vector3 addtoparent) 
    {
        var asdf = GetComponent<PlayerTetrimino>();
       asdf.ResetLockDelay();
        asdf.lastmove = "rotation";
        /*
         for each children set up for top left to bottom right

            simply puts them in positions of first second third and fourth.
            plus the test vector for all of them
        actually maybe not,instead test vector applies to the parent of the blocks

            change childs position first and change parent position after with position+test
        */

        int i = 0;
        foreach (Transform child in transform)
        {
            

           if (i == 0) { child.position = first+transform.position; }
           else if (i == 1) { child.position = second + transform.position; }
            else if (i == 2) { child.position = third + transform.position; }
            else if (i == 3) { child.position = fourth + transform.position; }
            i++;
        }
        transform.position += addtoparent;

        currentposition = wantedposition;
    }

    void CheckRotations(Vector3 first, Vector3 second, Vector3 third, Vector3 fourth, Vector3 firsttest, Vector3 secondtest, Vector3 thirdtest, Vector3 fourthtest,Vector3 fifthtest) 
    {
        
        if (Wallkicktest(first, second, third, fourth, firsttest)) { ApplyRotation(first, second, third, fourth, firsttest); }
        else if (Wallkicktest(first, second, third, fourth, secondtest)) { ApplyRotation(first, second, third, fourth, secondtest); }
        else if (Wallkicktest(first, second, third, fourth, thirdtest)) { ApplyRotation(first, second, third, fourth, thirdtest); }
        else if (Wallkicktest(first, second, third, fourth, fourthtest)) { ApplyRotation(first, second, third, fourth, fourthtest); }
        else if (Wallkicktest(first, second, third, fourth, fifthtest)) { ApplyRotation(first, second, third, fourth, fifthtest); }

    }

    /*
    bool Checkrotation(Vector3 Checkthis)// ORİGİNAL DONT  
    {
        if (Physics2D.OverlapPoint(Checkthis + transform.position))
        {
            return false;
        }
        else
        {
            return true;// can spawn
        }
    }*/

    /*
    bool Wallkicktest(Vector3 first, Vector3 second, Vector3 third, Vector3 fourth, Vector3 test)
    {
        if (Checkrotation(first + test)) { if (Checkrotation(second + test)) { if (Checkrotation(third + test)) { if (Checkrotation(fourth + test)) { return true; } } } }

        return false;
    }*/

    public bool CheckTspin() 
    {// inanilmaz basit
        int a=0;
        if (Checkrotation(topleft+transform.position)){ a++; }
        if (Checkrotation(topright + transform.position)) { a++; }
        if (Checkrotation(botleft + transform.position)) { a++; }
        if (Checkrotation(botright + transform.position)) { a++; }

        if (a >= 3) { Debug.Log("YEP tspin"); return true; }
        Debug.Log("no tspin");
        return false;
    }

    void ZeroToOne(Vector3 first, Vector3 second, Vector3 third,Vector3 fourth) 
    {
        Vector3 firsttest    = new Vector3(0, 0, 0);
        Vector3 secondtest   = new Vector3(-1, 0, 0);
        Vector3 thirdtest    = new Vector3(-1, 1, 0);
        Vector3 fourthtest   = new Vector3(0, -2, 0);
        Vector3 fifthtest    = new Vector3(-1, -2, 0);

        CheckRotations(  first,  second,  third,  fourth,  firsttest,  secondtest,  thirdtest,  fourthtest,  fifthtest);

        /*
             if (Wallkicktest(first,second,third,fourth,firsttest)) { ApplyRotation(first, second, third, fourth, firsttest); }
        else if (Wallkicktest(first, second, third, fourth, secondtest)) { ApplyRotation(first, second, third, fourth, secondtest); }
        else if (Wallkicktest(first, second, third, fourth, thirdtest)) { ApplyRotation(first, second, third, fourth, thirdtest); }
        else if (Wallkicktest(first, second, third, fourth, fourthtest)) { ApplyRotation(first, second, third, fourth, fourthtest); }
        else if (Wallkicktest(first, second, third, fourth, fifthtest)) { ApplyRotation(first, second, third, fourth, fifthtest); }
        */
    }


    void OneToZero(Vector3 first, Vector3 second, Vector3 third, Vector3 fourth)
    {
        Vector3 firsttest = new Vector3(0, 0, 0);
        Vector3 secondtest = new Vector3(1, 0, 0);
        Vector3 thirdtest = new Vector3(1, -1, 0);
        Vector3 fourthtest = new Vector3(0, 2, 0);
        Vector3 fifthtest = new Vector3(1, 2, 0);

        CheckRotations(first, second, third, fourth, firsttest, secondtest, thirdtest, fourthtest, fifthtest);
    }

    void OneToTwo(Vector3 first, Vector3 second, Vector3 third, Vector3 fourth)
    {
        Vector3 firsttest = new Vector3(0, 0, 0);
        Vector3 secondtest = new Vector3(1, 0, 0);
        Vector3 thirdtest = new Vector3(1, -1, 0);
        Vector3 fourthtest = new Vector3(0, 2, 0);
        Vector3 fifthtest = new Vector3(1, 2, 0);



        CheckRotations(first, second, third, fourth, firsttest, secondtest, thirdtest, fourthtest, fifthtest);
    }

    void TwoToOne(Vector3 first, Vector3 second, Vector3 third, Vector3 fourth)
    {
        Vector3 firsttest = new Vector3(0, 0, 0);
        Vector3 secondtest = new Vector3(-1, 0, 0);
        Vector3 thirdtest = new Vector3(-1, 1, 0);
        Vector3 fourthtest = new Vector3(0, -2, 0);
        Vector3 fifthtest = new Vector3(-1, -2, 0);

        CheckRotations(first, second, third, fourth, firsttest, secondtest, thirdtest, fourthtest, fifthtest);
    }

    void TwoToThree(Vector3 first, Vector3 second, Vector3 third, Vector3 fourth)
    {
        Vector3 firsttest = new Vector3(0, 0, 0);
        Vector3 secondtest = new Vector3(1, 0, 0);
        Vector3 thirdtest = new Vector3(1, 1, 0);
        Vector3 fourthtest = new Vector3(0, -2, 0);
        Vector3 fifthtest = new Vector3(1, -2, 0);

        CheckRotations(first, second, third, fourth, firsttest, secondtest, thirdtest, fourthtest, fifthtest);
    }

    void ThreeToTwo(Vector3 first, Vector3 second, Vector3 third, Vector3 fourth)
    {
        Vector3 firsttest = new Vector3(0, 0, 0);
        Vector3 secondtest = new Vector3(-1, 0, 0);
        Vector3 thirdtest = new Vector3(-1, -1, 0);
        Vector3 fourthtest = new Vector3(0, 2, 0);
        Vector3 fifthtest = new Vector3(-1, 2, 0);

        CheckRotations(first, second, third, fourth, firsttest, secondtest, thirdtest, fourthtest, fifthtest);
    }

    void ThreeToZero(Vector3 first, Vector3 second, Vector3 third, Vector3 fourth)
    {
        Vector3 firsttest = new Vector3(0, 0, 0);
        Vector3 secondtest = new Vector3(-1, 0, 0);
        Vector3 thirdtest = new Vector3(-1, -1, 0);
        Vector3 fourthtest = new Vector3(0, 2, 0);
        Vector3 fifthtest = new Vector3(-1, 2, 0);







        CheckRotations(first, second, third, fourth, firsttest, secondtest, thirdtest, fourthtest, fifthtest);
    }


    void ZeroToThree(Vector3 first, Vector3 second, Vector3 third, Vector3 fourth)
    {
        Vector3 firsttest = new Vector3(0, 0, 0);
        Vector3 secondtest = new Vector3(1, 0, 0);
        Vector3 thirdtest = new Vector3(1, 1, 0);
        Vector3 fourthtest = new Vector3(0, -2, 0);
        Vector3 fifthtest = new Vector3(1, -2, 0);



        CheckRotations(first, second, third, fourth, firsttest, secondtest, thirdtest, fourthtest, fifthtest);
    }

   /*

        //0 to 1 tests
        new Vector3( 0, 0,0);
        new Vector3(-1, 0,0);
        new Vector3(-1, 1,0);
        new Vector3( 0,-2,0);
        new Vector3(-1,-2,0);

        //1 to 0 tests 
        new Vector3(0, 0,0);
        new Vector3(1, 0,0);
        new Vector3(1,-1,0);
        new Vector3(0, 2,0);
        new Vector3(1, 2,0);

        //1 to 2 tests
        new Vector3(0, 0,0);
        new Vector3(1, 0,0);
        new Vector3(1,-1,0);
        new Vector3(0, 2,0);
        new Vector3(1, 2,0); 

        //2 to 1 tests
        new Vector3( 0, 0,0);
        new Vector3(-1, 0,0);
        new Vector3(-1, 1,0);
        new Vector3( 0,-2,0);
        new Vector3(-1,-2,0);

        //2 to 3 tests
        new Vector3(0, 0,0);
        new Vector3(1, 0,0);
        new Vector3(1, 1,0);
        new Vector3(0,-2,0);
        new Vector3(1,-2,0);

        //3 to 2 tests
        new Vector3( 0, 0,0);
        new Vector3(-1, 0,0);
        new Vector3(-1,-1,0);
        new Vector3( 0, 2,0);
        new Vector3(-1, 2,0);

        //3 to 0 tests
        new Vector3( 0, 0,0);
        new Vector3(-1, 0,0);
        new Vector3(-1,-1,0);
        new Vector3( 0, 2,0);
        new Vector3(-1, 2,0);

        //0 to 3 tests
        new Vector3(0, 0,0);
        new Vector3(1, 0,0);
        new Vector3(1, 1,0);
        new Vector3(0,-2,0);
        new Vector3(1,-2,0);

        //FFS  REST IS I TETRIMINO

        //0 to 1 tests
        new Vector3(0, 0, 0);
        new Vector3(-2, 0, 0);
        new Vector3(1, 0, 0);
        new Vector3(-2, -1, 0);
        new Vector3(1, 2, 0);

        //1 to 0 tests 
        new Vector3(0, 0, 0);
        new Vector3(2, 0, 0);
        new Vector3(-1,0, 0);
        new Vector3(2, 1, 0);
        new Vector3(-1, -2, 0);

        //1 to 2 tests   ( 0, 0)	(-1, 0)	( 2, 0)	(-1, 2)	( 2,-1)
        new Vector3(0, 0, 0);
        new Vector3(-1, 0, 0);
        new Vector3(2, 0, 0);
        new Vector3(-1, 2, 0);
        new Vector3(2, -1, 0);

        //2 to 1 tests  ( 0, 0)	( 1, 0)	(-2, 0)	( 1,-2)	(-2, 1)
        new Vector3(0, 0, 0);
        new Vector3(1, 0, 0);
        new Vector3(-2, 0, 0);
        new Vector3(1, -2, 0);
        new Vector3(-2, 1, 0);

        //2 to 3 tests  ( 0, 0)	( 2, 0)	(-1, 0)	( 2, 1)	(-1,-2)
        new Vector3(0, 0, 0);
        new Vector3(2, 0, 0);
        new Vector3(-1, 0, 0);
        new Vector3(2, 1, 0);
        new Vector3(-1, -2, 0);

        //3 to 2 tests  ( 0, 0)	(-2, 0)	( 1, 0)	(-2,-1)	( 1, 2)
        new Vector3(0, 0, 0);
        new Vector3(-2, 0, 0);
        new Vector3(1, 0, 0);
        new Vector3(-2, -1, 0);
        new Vector3(1, 2, 0);

        //3 to 0 tests ( 0, 0)	( 1, 0)	(-2, 0)	( 1,-2)	(-2, 1)
        new Vector3(0, 0, 0);
        new Vector3(1, 0, 0);
        new Vector3(-2, 0, 0);
        new Vector3(1, -2, 0);
        new Vector3(-2, 1, 0);

        //0 to 3 tests ( 0, 0)	(-1, 0)	( 2, 0)	(-1, 2)	( 2,-1)
        new Vector3(0, 0, 0);
        new Vector3(-1, 0, 0);
        new Vector3(2, 0, 0);
        new Vector3(-1, 2, 0);
        new Vector3(2, -1, 0);


    
   */
    

  public  void CheckRotation(int wantedpos) // call this function to rotate
    {
        wantedposition = wantedpos;
        /*
        CheckifFilled(new Vector3(transform.position.x + 0.5f, 1, 0));
        new Vector3(transform.position.x - 1f, transform.position.y + 0.0f, 0);//checks left  -1  0
        new Vector3(transform.position.x + 1f, transform.position.y + 0.0f, 0);//checks right  1  0
        new Vector3(transform.position.x + 0f, transform.position.y + 1.0f, 0);//checks up     0  1
        new Vector3(transform.position.x + 0f, transform.position.y - 1.0f, 0);//checks down   0 -1

        new Vector3(transform.position.x - 1f, transform.position.y + 1.0f, 0);//checks top left        -1  1
        new Vector3(transform.position.x + 1f, transform.position.y + 1.0f, 0);//checks top right        1  1
        new Vector3(transform.position.x - 1f, transform.position.y - 1.0f, 0);//checks bottom left     -1 -1
        new Vector3(transform.position.x + 1f, transform.position.y - 1.0f, 0);//checks bottom right     1 -1

        */

        // Vector3 checkthis= transform.position+left;//checks left  -1  0
        

        switch (tetriminotype)
        {
            
            case "T":

              
                switch (wantedposition)
                {
                    case 0:
                        //check for up,left and right
                       
                        if (currentposition == 1) { OneToZero  (up, left, middle, right); }
                        else if (currentposition==3)   { ThreeToZero(up, left, middle, right); }

                        //    0
                        //  3   1
                        //    2
                        break;

                    case 1:
                        //check for up,right and down
                             if (currentposition == 2) { TwoToOne (up, middle, right, down); }
                        else if (currentposition == 0) { ZeroToOne(up, middle, right, down); }
                        break;

                    case 2:
                        //check for right,down and left
                             if (currentposition == 1) { OneToTwo  (left, middle, right, down); }
                        else if (currentposition == 3) { ThreeToTwo(left, middle, right, down); }
                        break;

                    case 3:
                        //check for down,left and up
                             if (currentposition == 0) { ZeroToThree(up, left, middle, down); }
                        else if (currentposition == 2) { TwoToThree (up, left, middle, down); }
                        break;

                    default:
                        Debug.Log("something went VERY wrong");
                        break;
                }
                break;
                                   
                // t is complete  ALL OF THEM ARE COMPLETE AAAAAAAA

            case "S":

              
                switch (wantedposition)
                {
                    case 0:
                        //check for up topright left middle

                        if (currentposition == 1) { OneToZero(up, topright, left, middle); }
                        else if (currentposition == 3) { ThreeToZero(up, topright, left, middle); }

                        //    0
                        //  3   1
                        //    2
                        break;

                    case 1:
                        //check for up middle right botright
                        if (currentposition == 2) { TwoToOne(up, middle, right, botright); }
                        else if (currentposition == 0) { ZeroToOne(up, middle, right, botright); }
                        break;

                    case 2:
                        //check for middle right botleft down
                        if (currentposition == 1) { OneToTwo(middle, right, botleft, down); }
                        else if (currentposition == 3) { ThreeToTwo(middle, right, botleft, down); }
                        break;

                    case 3:
                        //check for topright left middle down
                        if (currentposition == 0) { ZeroToThree(topleft, left, middle, down); }
                        else if (currentposition == 2) { TwoToThree(topleft, left, middle, down); }
                        break;

                    default:
                        Debug.Log("something went VERY wrong");
                        break;
                }
                break;
           

            case "Z":
               

                switch (wantedposition)
                {
                    case 0:
                        //check for topleft top middle right

                        if (currentposition == 1) { OneToZero(topleft, up, middle, right); }
                        else if (currentposition == 3) { ThreeToZero(topleft, up, middle, right); }

                        //    0
                        //  3   1
                        //    2
                        break;

                    case 1:
                        //check for topright middle right down
                        if (currentposition == 2) { TwoToOne(topright,middle,right,down); }
                        else if (currentposition == 0) { ZeroToOne(topright, middle, right, down); }
                        break;

                    case 2:
                        //check for left middle down botright
                        if (currentposition == 1) { OneToTwo(left, middle, down, botright); }
                        else if (currentposition == 3) { ThreeToTwo(left, middle, down, botright); }
                        break;

                    case 3:
                        //check for top left middle botleft
                        if (currentposition == 0) { ZeroToThree(up, left, middle, botleft); }
                        else if (currentposition == 2) { TwoToThree(up, left, middle, botleft); }
                        break;

                    default:
                        Debug.Log("something went VERY wrong");
                        break;
                }
                break;

            case "L":
             

                switch (wantedposition)
                {
                    case 0:
                        //check for topright left middle right

                        if (currentposition == 1) { OneToZero(topright, left, middle, right); }
                        else if (currentposition == 3) { ThreeToZero(topright, left, middle, right); }

                        //    0
                        //  3   1
                        //    2
                        break;

                    case 1:
                        //check for top middle down botright
                        if (currentposition == 2) { TwoToOne(up, middle, down, botright); }
                        else if (currentposition == 0) { ZeroToOne(up, middle, down, botright); }
                        break;

                    case 2:
                        //check for left middle right botleft
                        if (currentposition == 1) { OneToTwo(left, middle, right, botleft); }
                        else if (currentposition == 3) { ThreeToTwo(left, middle, right, botleft); }
                        break;

                    case 3:
                        //check for topleft top middle down
                        if (currentposition == 0) { ZeroToThree(topleft, up, middle, down); }
                        else if (currentposition == 2) { TwoToThree(topleft, up, middle, down); }
                        break;

                    default:
                        Debug.Log("something went VERY wrong");
                        break;
                }
                break;

            case "J":

               
                switch (wantedposition)
                {
                    case 0:
                        //check for topleft left middle right

                        if (currentposition == 1) { OneToZero(topleft, left, middle, right); }
                        else if (currentposition == 3) { ThreeToZero(topleft, left, middle, right); }

                        //    0
                        //  3   1
                        //    2
                        break;

                    case 1:
                        //check for up topright middle down
                        if (currentposition == 2) { TwoToOne(up, topright, middle, down); }
                        else if (currentposition == 0) { ZeroToOne(up, topright, middle, down); }
                        break;

                    case 2:
                        //check for left middle right botright
                        if (currentposition == 1) { OneToTwo(left, middle, right, botright); }
                        else if (currentposition == 3) { ThreeToTwo(left, middle, right, botright); }
                        break;

                    case 3:
                        //check for up middle down botleft
                        if (currentposition == 0) { ZeroToThree(up, middle, down, botleft); }
                        else if (currentposition == 2) { TwoToThree(up, middle, down, botleft); }
                        break;

                    default:
                        Debug.Log("something went VERY wrong");
                        break;
                }
                break;

            case "I":// THE BOSSFİGHT LES GOOOO

               
                switch (wantedposition)
                {
                    case 0:
                        /// zone 0 yapmak istesem I için
                        /// 
                        /// 21,22,23,24 yapmam lazım
                        if (currentposition == 1) { OneToZeroLong(fbf21, fbf22, fbf23, fbf24); }
                        else if (currentposition == 3) { ThreeToZeroLong(fbf21, fbf22, fbf23, fbf24); }
                
                        break;

                    case 1:
                        /// zone 1 için
                        /// 
                        /// 13,23,33,43 yapmam lazım
                        if (currentposition == 2) { TwoToOneLong(fbf13, fbf23, fbf33, fbf43); }
                        else if (currentposition == 0) { ZeroToOneLong(fbf13, fbf23, fbf33, fbf43); }
                        break;

                    case 2:
                        /// zone 2 için
                        /// 
                        /// 31,32,33,34 yapmam lazım
                        /// 
                        if (currentposition == 1) { OneToTwoLong(fbf31, fbf32, fbf33, fbf34); }
                        else if (currentposition == 3) { ThreeToTwoLong(fbf31, fbf32, fbf33, fbf34); }
                        break;

                    case 3:
                     
                        /// zone 3 için
                        /// 
                        /// 12,22,32,42 yapmam lazım
                        if (currentposition == 0) { ZeroToThreeLong(fbf12, fbf22, fbf32, fbf42); }
                        else if (currentposition == 2) { TwoToThreeLong(fbf12, fbf22, fbf32, fbf42); }
                        break;

                    default:
                        Debug.Log("something went VERY wrong");
                        break;
                }
                break;
        }




    }

    /// <summary>
    /// zone 0 yapmak istesem I için
    /// 
    /// 21,22,23,24 yapmam lazım
    /// 
    /// zone 1 için
    /// 
    /// 13,23,33,43 yapmam lazım
    /// 
    /// zone 2 için
    /// 
    /// 31,32,33,34 yapmam lazım
    /// 
    /// zone 3 için
    /// 
    /// 12,22,32,42 yapmam lazım
    /// </summary>
    /// 
    /*
      

        
      
   

    

   

   

   

   

    */

    void ZeroToOneLong(Vector3 first, Vector3 second, Vector3 third, Vector3 fourth)
    {


        //0 to 1 tests
        
        
        
        
       

        Vector3 firsttest = new Vector3(0, 0, 0);
        Vector3 secondtest = new Vector3(-2, 0, 0);
        Vector3 thirdtest = new Vector3(1, 0, 0);
        Vector3 fourthtest = new Vector3(-2, -1, 0);
        Vector3 fifthtest = new Vector3(1, 2, 0);

        CheckRotations(first, second, third, fourth, firsttest, secondtest, thirdtest, fourthtest, fifthtest);  
    }

    void OneToZeroLong(Vector3 first, Vector3 second, Vector3 third, Vector3 fourth)
    {

        //1 to 0 tests 
        
        
        
        
        

        Vector3 firsttest = new Vector3(0, 0, 0);
        Vector3 secondtest = new Vector3(2, 0, 0);
        Vector3 thirdtest = new Vector3(-1, 0, 0);
        Vector3 fourthtest = new Vector3(2, 1, 0);
        Vector3 fifthtest = new Vector3(-1, -2, 0);

        CheckRotations(first, second, third, fourth, firsttest, secondtest, thirdtest, fourthtest, fifthtest);
    }

    //   0
    //  3  1
    //   2
    //

    void OneToTwoLong(Vector3 first, Vector3 second, Vector3 third, Vector3 fourth)
    {
        //1 to 2 tests   ( 0, 0)	(-1, 0)	( 2, 0)	(-1, 2)	( 2,-1)
        
        
       
        
        



        Vector3 firsttest = new Vector3(0, 0, 0);
        Vector3 secondtest = new Vector3(-1, 0, 0);
        Vector3 thirdtest = new Vector3(2, 0, 0);
        Vector3 fourthtest = new Vector3(-1, 2, 0);
        Vector3 fifthtest = new Vector3(2, -1, 0);

        CheckRotations(first, second, third, fourth, firsttest, secondtest, thirdtest, fourthtest, fifthtest);
    }

    void TwoToThreeLong(Vector3 first, Vector3 second, Vector3 third, Vector3 fourth)
    {
        //2 to 3 tests  ( 0, 0)	( 2, 0)	(-1, 0)	( 2, 1)	(-1,-2)
       
       
       
       
        

        Vector3 firsttest = new Vector3(0, 0, 0);
        Vector3 secondtest = new Vector3(2, 0, 0);
        Vector3 thirdtest = new Vector3(-1, 0, 0);
        Vector3 fourthtest = new Vector3(2, 1, 0);
        Vector3 fifthtest = new Vector3(-1, -2, 0);

        CheckRotations(first, second, third, fourth, firsttest, secondtest, thirdtest, fourthtest, fifthtest);
    }

    void TwoToOneLong(Vector3 first, Vector3 second, Vector3 third, Vector3 fourth)
    {
        //2 to 1 tests  ( 0, 0)	( 1, 0)	(-2, 0)	( 1,-2)	(-2, 1)
       
        
        
        
       

        Vector3 firsttest = new Vector3(0, 0, 0);
        Vector3 secondtest = new Vector3(1, 0, 0);
        Vector3 thirdtest = new Vector3(-2, 0, 0);
        Vector3 fourthtest = new Vector3(1, -2, 0);
        Vector3 fifthtest = new Vector3(-2, 1, 0);

        CheckRotations(first, second, third, fourth, firsttest, secondtest, thirdtest, fourthtest, fifthtest);
    }

    void ThreeToTwoLong(Vector3 first, Vector3 second, Vector3 third, Vector3 fourth)
    {
        //3 to 2 tests  ( 0, 0)	(-2, 0)	( 1, 0)	(-2,-1)	( 1, 2)
       
       
        
       
        

        Vector3 firsttest = new Vector3(0, 0, 0);
        Vector3 secondtest = new Vector3(-2, 0, 0);
        Vector3 thirdtest = new Vector3(1, 0, 0);
        Vector3 fourthtest = new Vector3(-2, -1, 0);
        Vector3 fifthtest = new Vector3(1, 2, 0);

        CheckRotations(first, second, third, fourth, firsttest, secondtest, thirdtest, fourthtest, fifthtest);
    }


    void ThreeToZeroLong(Vector3 first, Vector3 second, Vector3 third, Vector3 fourth)
    {
        //3 to 0 tests ( 0, 0)	( 1, 0)	(-2, 0)	( 1,-2)	(-2, 1)
      
       
        
       
       

        Vector3 firsttest = new Vector3(0, 0, 0);
        Vector3 secondtest = new Vector3(1, 0, 0);
        Vector3 thirdtest = new Vector3(-2, 0, 0);
        Vector3 fourthtest = new Vector3(1, -2, 0);
        Vector3 fifthtest = new Vector3(-2, 1, 0);

        CheckRotations(first, second, third, fourth, firsttest, secondtest, thirdtest, fourthtest, fifthtest);
    }

    void ZeroToThreeLong(Vector3 first, Vector3 second, Vector3 third, Vector3 fourth)
    {
        //0 to 3 tests ( 0, 0)	(-1, 0)	( 2, 0)	(-1, 2)	( 2,-1)
       
        
        
       
       

        Vector3 firsttest = new Vector3(0, 0, 0);
        Vector3 secondtest = new Vector3(-1, 0, 0);
        Vector3 thirdtest = new Vector3(2, 0, 0);
        Vector3 fourthtest = new Vector3(-1, 2, 0);
        Vector3 fifthtest = new Vector3(2, -1, 0);

        CheckRotations(first, second, third, fourth, firsttest, secondtest, thirdtest, fourthtest, fifthtest);
    }
}
