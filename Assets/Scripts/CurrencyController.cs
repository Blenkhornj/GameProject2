using TMPro;
using UnityEngine;

public class CurrencyController : MonoBehaviour
{

    public static CurrencyController instance;
    private void Awake()
    {
        instance = this;
    }

    public float businessDollars;
    public float gamePrice;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gamePrice = 60f; //default game setup
        businessDollars = -500f;
    }

    // Update is called once per frame
    void Update()
    {
        if (UIController.instance != null) //updates UI screen
        {
            
            UIController.instance.UpdateCurrencyDisplay(businessDollars);
            UIController.instance.UpdateSalesDisplay(PlayerController.instance.packagesSold, gamePrice);
        }
    }

    public void SellBox() //called when final tool is finished. Increases bsuiness revenue and stock
    {

        
        businessDollars += gamePrice;
        WebstoreController.instance.productsInStock--;
        AudioManager.instance.PlaySFX(2);



    }



}
