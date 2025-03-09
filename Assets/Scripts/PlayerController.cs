using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    //-----------------------------------------------
    // Instance objects and the 'Awake' functions, allow for the classes and the class functions to be called into other classes
    //-----------------------------------------------

    public static PlayerController instance;

    private void Awake()
    {
        instance = this;
    }

    //-----------------------------------------------



    //-----------------------------------------------
    // LIST OF VARIABLES BELOW
    //-----------------------------------------------



    [SerializeField] Rigidbody2D playerRigidBody; //References the RigidBody component on the player object
    public float moveSpeed; //creating variable for setting in the editor
    public InputActionReference movementInput; //Defaults movement keys to traditional movement (WASD), and Arrows
    public Animator myPlayerAnimator;   //Creates an animator object in the editor, which can be assigned to the player object
    public SpriteRenderer mySpriteRenderer; //Sprite renderer is used for updating images, this is used for updating player direction

    

    public enum playerToolEnum { boxTool, productTool, boxTapeTool, shipTool} //Creates a drop down list of tools that will be referenced on the toolbar

    public playerToolEnum playerTool; //initiates the enum into the editor

    public bool noBox, boxOpen, boxPacked, boxSealed, readyToShip; //these booleans are used to check the phase of product packing, so you cannot skip steps
    public bool inProgress = false; //enforces gameplay to finish packing
    public int packagesSold = 0; //defaults game to starting from scratch


    //-----------------------------------------------
    // END LIST OF VARIABLES
    //-----------------------------------------------



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        CheckToolBoolean(); //sets business booleans to start

    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        SelectTool();
        PlaceBox();
        PackageBox();
        SealBox();
        ShipBox();

    }

    public void PlayerMovement() //Primary move function
    {
        playerRigidBody.linearVelocity = movementInput.action.ReadValue<Vector2>() * moveSpeed; //sets 

        myPlayerAnimator.SetFloat("speed", playerRigidBody.linearVelocity.magnitude); //plays the move animation when the movement is > 0

       
        //Flips the players when changing direction
        
        if (playerRigidBody.linearVelocity.x < 0)
        {
            mySpriteRenderer.flipX = true;
        }
        else if (playerRigidBody.linearVelocity.x > 0)
        {
            mySpriteRenderer.flipX = false;
        }

        //End of Flip Function

        //Stops movement if game is paused

        if (UIController.instance.pauseScreen.activeSelf == true)
        {
            moveSpeed = 0;
        } else
        {
            moveSpeed = 5f;
        }

    } //WASD Keys

    public void SelectTool() //Uses the TAB key or the number keys to select the product tool
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            playerTool++;
            CheckToolBoolean();

            if ((int)playerTool >= 4) //function to set back to first tool if you exceed the final one
            {
                playerTool = playerToolEnum.boxTool;
                CheckToolBoolean();

            }
        }

        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            playerTool = playerToolEnum.boxTool;
            CheckToolBoolean();

        }

        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            playerTool = playerToolEnum.productTool;
            CheckToolBoolean();

        }

        if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            playerTool = playerToolEnum.boxTapeTool;
            CheckToolBoolean();

        }

        if (Keyboard.current.digit4Key.wasPressedThisFrame)
        {
            playerTool = playerToolEnum.shipTool;
            CheckToolBoolean();

        }


        UIController.instance.SwitchToolIcon((int)playerTool); //calls the UI component, so the game can indicate to the player which tool is selected.

    } //TabKey


    public void PlaceBox() //Function for setting the cardboard box
    {
        
        if (Keyboard.current.eKey.wasPressedThisFrame && noBox == true && inProgress == false) //When E key is pressed
        { 
            OrderPackage.instance.NextPackageStage(); //Update Sprite
            inProgress = true; //begins process as true
            AudioManager.instance.PlaySFX(3); //plays an audio file

        }
       
    }

    

    public void PackageBox() //repeat function but updates stage checks
    {

        if (Keyboard.current.eKey.wasPressedThisFrame && boxOpen == true)
        {
            if (OrderPackage.instance.packageStage == OrderPackage.PackageStageEnum.openBox)
            {
                OrderPackage.instance.NextPackageStage();
                AudioManager.instance.PlaySFX(4);

            }
        }
       
    }

    public void SealBox() //repeat function but updates stage checks
    {
        

        if (Keyboard.current.eKey.wasPressedThisFrame && boxPacked == true)
        {
            if (OrderPackage.instance.packageStage == OrderPackage.PackageStageEnum.packedBox)
            {
                OrderPackage.instance.NextPackageStage();
                AudioManager.instance.PlaySFX(3);

            }
        }


    }


    public void ShipBox() //repeat function but updates stage checks
    {
        if (Keyboard.current.eKey.wasPressedThisFrame && readyToShip == true && WebstoreController.instance.productsInStock >= 1)
        {
            if (OrderPackage.instance.packageStage == OrderPackage.PackageStageEnum.sealedBox)
            {
                OrderPackage.instance.NextPackageStage();
                inProgress = false; //resets process stage
                packagesSold++; //adds increment to sold units variable, which is used for revenue
                CurrencyController.instance.SellBox(); //calls a math function for business revenue
                

            }

            
        }
        
    }
    
    //Below Booleans are set to ensure you cannot skip business steps

    public void CheckToolBoolean()
    {
        if((int)playerTool == 0)
        {
            noBox = true;
            boxOpen = false;
            boxPacked = false;
            boxSealed = false;
            readyToShip = false;
        } 
        else if((int)playerTool == 1)
        {
            noBox = false;
            boxOpen = true;
            boxPacked = false;
            boxSealed = false;
            readyToShip = false;
        } 
        else if ((int)playerTool == 2)
        {
            noBox = false;
            boxOpen = false;
            boxPacked = true;
            boxSealed = false;
            readyToShip = false;
        }

        else if ((int)playerTool == 3)
        {
            noBox = false;
            boxOpen = false;
            boxPacked = false;
            boxSealed = false;
            readyToShip = true;
        }

    } 


}



