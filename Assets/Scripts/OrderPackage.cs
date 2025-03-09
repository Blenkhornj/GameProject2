using UnityEngine;
using UnityEngine.InputSystem;

public class OrderPackage : MonoBehaviour
{

    public static OrderPackage instance;

    private void Awake()
    {
        instance = this;
    }

    //---------------------------------------




    public enum PackageStageEnum { noBox, openBox, packedBox, sealedBox } //creates a list of items that can be selected with int capabilities

    public PackageStageEnum packageStage; //adds the enum list to the Editor for visual feedback

    public SpriteRenderer packageSpriteRenderer;
    public Sprite packageOpenSprite, packagePackedSprite, packageSealedSprite; //sets sprite of package based on enum selection


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void NextPackageStage() //sets enum for packing stage to the next stage, and loops back if it exceeds final stage
    {
       
            packageStage++;

            if ((int)packageStage > 3)
            {
                packageStage = PackageStageEnum.noBox;
            }

            placeBoxSprite();
      
    }

    public void placeBoxSprite() //starts business process
    {
        if(packageStage == PackageStageEnum.noBox)
        {
            packageSpriteRenderer.sprite = null;
        }
        else
        {
          // Updates sprite of package as stage increases

            switch (packageStage)
            {
                              
                case PackageStageEnum.openBox:
                    packageSpriteRenderer.sprite = packageOpenSprite; break;


                case PackageStageEnum.packedBox:
                    packageSpriteRenderer.sprite = packagePackedSprite; break;


                case PackageStageEnum.sealedBox:
                    packageSpriteRenderer.sprite = packageSealedSprite; break;

            }
        }
    }



}
