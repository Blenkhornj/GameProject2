using UnityEngine;

public class WebstoreController : MonoBehaviour
{
    //-----------------------------------------------
    // Instance objects and the 'Awake' functions, allow for the classes and the class functions to be called into other classes
    //-----------------------------------------------

    public static WebstoreController instance;

    private void Awake()
    {
        instance = this;
    }


    //-----------------------------------------------
    // LIST OF VARIABLES BELOW
    //-----------------------------------------------

    public int productsInStock; 
    public float EbayPrice; //game utilizes a similar online purchasing store similar to Ebay, so Ebay is used as an example name.
    public float webStoreRevenue; //primary gameplay goal is to increase this
    public float EbayTrendingPrice; //used for color coding on ebay price


    //-----------------------------------------------
    // END LIST OF VARIABLES
    //-----------------------------------------------


    void Start() //sets game start variables
    {
        productsInStock = 5;
        EbayPrice = 45f;
        webStoreRevenue = 0f;
        EbayTrendingPrice = EbayPrice;
    }



    void Update()
    {
        if (UIController.instance != null) //updates display whenever the webscreen is opened
        {

            UIController.instance.UpdateStockDisplay(productsInStock);
            UIController.instance.UpdateEbayStorePrices(EbayPrice, EbayTrendingPrice);
            
        }
    }

    public void BuyProduct() //buying product function
    {
        
        productsInStock++;
        CurrencyController.instance.businessDollars -= EbayPrice;
        SetEbayRandomPrice();
    }

    public void SetEbayRandomPrice() //simulates a used webstore such as ebay, where prices are rarely the same
    {
        EbayPrice = Random.Range(15f, 100f);
    }

    public void TrendingColorSetter() //used to showcase whether the ebay product is cheap or overpriced
    {
        if(EbayPrice > EbayTrendingPrice)
        {
            UIController.instance.EbayPriceText.color = Color.red;
        }
        else
        {
            UIController.instance.EbayPriceText.color = Color.green;
        }
    }



}
