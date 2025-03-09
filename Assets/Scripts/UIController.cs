using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour

{

    //-----------------------------------------------
    // Instance objects and the 'Awake' functions, allow for the classes and the class functions to be called into other classes
    //-----------------------------------------------
    public static UIController instance;

    private void Awake()
    {
        instance = this;
    }


    //-----------------------------------------------
    // LIST OF VARIABLES BELOW
    //-----------------------------------------------


    public GameObject[] toolsIcons;
    public TMP_Text currencyText;
    public TMP_Text stockText, listPriceText, productInStockText, EbayPriceText, EbayTrendingText;
    public WebstoreController webstoreController;
    public GameObject webPage;
    public GameObject pauseScreen;

    //-----------------------------------------------
    // END LIST OF VARIABLES
    //-----------------------------------------------



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SwitchToolIcon(0);
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Keyboard.current.iKey.wasPressedThisFrame) //opens the business performance screen and webstore
        {
            if(webPage.gameObject.activeSelf == true)
            {
                webPage.gameObject.SetActive(false);
            } else
            {
                webPage.gameObject.SetActive(true);
            }

            WebstoreController.instance.SetEbayRandomPrice(); //randomizes the webstore pricing on open and close
            AudioManager.instance.PlaySFX(1); //plays audio

        }

        if (Keyboard.current.mKey.wasPressedThisFrame) //key used to buy product
        {
            WebstoreController.instance.BuyProduct();
            AudioManager.instance.PlaySFX(1); 
        }


        PauseButton(); //called every frame so the player can pause whenever they want
    }

    public void SwitchToolIcon(int selectedIcon) //Blue indicator for showing which tool is selected
    {
        foreach(GameObject icon  in toolsIcons)
        {
            icon.SetActive(false);
        }

        toolsIcons[selectedIcon].SetActive(true);
    }


    //These functions update the display based on business functions
    public void UpdateCurrencyDisplay(float currentCurrency)
    {
       currencyText.text = "$" + currentCurrency.ToString() +" USD";
    }

    public void UpdateSalesDisplay(float currentSold, float listPrice)
    {
        stockText.text = currentSold.ToString() + "Packages Sold";
        listPriceText.text = "Sell Price $" + listPrice.ToString() + " USD";
    }

    public void UpdateStockDisplay(int currentStock)
    {
        productInStockText.text = currentStock.ToString() + " Products In Stock";
        
    }

    public void UpdateEbayStorePrices(float ebayListPrice, float ebayTrendingPrice)
    {
        EbayPriceText.text = "Store Price: \n$ " + ebayListPrice.ToString() + " USD";
        EbayTrendingText.text = "Trending at \n$ " + ebayTrendingPrice.ToString() + " USD";
        WebstoreController.instance.TrendingColorSetter();

        
    }


    public void MainMenuButton() //opens the main menu by opening a new scene
    {
        SceneManager.LoadScene("Menu");
        
    }

    public void Resume() //resumes game closing pause screen
    {
        pauseScreen.gameObject.SetActive(false);
        AudioManager.instance.PlaySFX(0);

    }

    public void PauseButton() //resumes game closing pause screen
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            pauseScreen.gameObject.SetActive(true);
            AudioManager.instance.PlaySFX(1);

        }
        
        
    }



}


