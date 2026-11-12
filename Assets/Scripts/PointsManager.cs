using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PointsManager : MonoBehaviour
{
    [Header("Texts")]
	public TMP_Text ducksText;
	public TMP_Text ducksPerClickText;
	public TMP_Text ducksPerSecondText;
	
	public TMP_Text upgradeQuackCostText;
	public TMP_Text upgradeQuackCountText;
	
	public TMP_Text upgradeMamaCostText;
	public TMP_Text upgradeMamaCountText;
	
	public TMP_Text upgradeShopCostText;
	public TMP_Text upgradeShopCountText;
	
	public TMP_Text upgradeInvestorCostText;
	public TMP_Text upgradeInvestorCountText;
	
	public TMP_Text upgradeFactoryCostText;
	public TMP_Text upgradeFactoryCountText;
	
	public TMP_Text upgradeOfficeCostText;
	public TMP_Text upgradeOfficeCountText;
	
	[Header("Buttons")]
	public Button quackButton;
	public Button mamaButton;
	public Button shopButton;
	public Button investorButton;
	public Button factoryButton;
	public Button officeButton;
	
	[Header("Lock Overlays")]
	public GameObject mamaLock;
	public GameObject shopLock;
	public GameObject investorLock;
	public GameObject factoryLock;
	public GameObject officeLock;
	
	[Header("Values")]
    public int ducks = 0;
	public int ducksPerSecond;
	public int ducksPerClick = 1;
	
	[Header("Balancing - Starting Upgrade Costs")]
    public int upgradeQuackCost;
	private int upgradeQuackCount;
	
	public int upgradeMamaCost;
	private int upgradeMamaCount;
	
	public int upgradeShopCost;
	private int upgradeShopCount;
	
	public int upgradeInvestorCost;
	private int upgradeInvestorCount;
	
	public int upgradeFactoryCost;
	private int upgradeFactoryCount;
	
	public int upgradeOfficeCost;
	private int upgradeOfficeCount;
	
    private float pointsAddInterval = 1;
    private float timer;
	
	private int buildingCount;

	[Header("Balancing - Upgrade Values")]
	public int upgradeQuackDPC = 1;
	public int upgradeMamaDPS = 1;
	public int upgradeShopDPS = 10;
	public int upgradeInvestorDPC = 50;
	public int upgradeInvestorDPSPerBuilding =8;
	public int upgradeFactoryDPS = 100;
	public int upgradeOfficeDPS = 1000;
	public float priceMultiplierPerPurchase = 1.1f;

    void Start() 
    {
        ducksText.text = ducks.ToString();
		ducksPerClickText.text = ducksPerClick.ToString();
		ducksPerSecondText.text = ducksPerSecond.ToString() + " Ducks per second";
		
		upgradeQuackCostText.text = upgradeQuackCost.ToString();
		upgradeMamaCostText.text = upgradeMamaCost.ToString();
		upgradeShopCostText.text = upgradeShopCost.ToString();
		upgradeInvestorCostText.text = upgradeInvestorCost.ToString();
		upgradeFactoryCostText.text = upgradeFactoryCost.ToString();
		upgradeOfficeCostText.text = upgradeOfficeCost.ToString();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > pointsAddInterval)
        {
            AddPoints(ducksPerSecond);
            timer = 0;
        }
		
		quackButton.interactable = ducks >= upgradeQuackCost;
		mamaButton.interactable = ducks >= upgradeMamaCost;
		shopButton.interactable = ducks >= upgradeShopCost;
		investorButton.interactable = ducks >= upgradeInvestorCost;
		factoryButton.interactable = ducks >= upgradeFactoryCost;
		officeButton.interactable = ducks >= upgradeOfficeCost;
		
		UnlockMama();
		UnlockShop();
		UnlockInvestor();
		UnlockFactory();
		UnlockOffice();
    }


    public void AddPoints(int addedPoints)
    {
        ducks += addedPoints;
        ducksText.text = ducks.ToString();
    }

    public void AddPointsPerClick()
    {
        AddPoints(ducksPerClick);
    }

	public void UpgradeQuack()
    {
        if (ducks >= upgradeQuackCost)
        {
            ducks -= upgradeQuackCost;
            upgradeQuackCost = Mathf.RoundToInt(upgradeQuackCost * priceMultiplierPerPurchase);
			upgradeQuackCount ++;
			ducksPerClick += upgradeQuackDPC;
			
			ducksText.text = ducks.ToString();
			ducksPerClickText.text = ducksPerClick.ToString();
            upgradeQuackCostText.text = upgradeQuackCost.ToString();
            upgradeQuackCountText.text = upgradeQuackCount.ToString();
        }
    }
	
	public void UpgradeMama()
    {
        if (ducks >= upgradeMamaCost)
        {
            ducks -= upgradeMamaCost;
            upgradeMamaCost = Mathf.RoundToInt(upgradeMamaCost * priceMultiplierPerPurchase);
			upgradeMamaCount ++;
			ducksPerSecond += upgradeMamaDPS;
			
			ducksText.text = ducks.ToString();
			ducksPerSecondText.text = ducksPerSecond.ToString() + " Ducks per second";
            upgradeMamaCostText.text = upgradeMamaCost.ToString();
            upgradeMamaCountText.text = upgradeMamaCount.ToString();
        }
    }
	
	public void UpgradeShop()
    {
        if (ducks >= upgradeShopCost)
        {
            ducks -= upgradeShopCost;
            upgradeShopCost = Mathf.RoundToInt(upgradeShopCost * priceMultiplierPerPurchase);
			upgradeShopCount ++;
			buildingCount ++;
			ducksPerSecond += upgradeShopDPS;
			
			ducksText.text = ducks.ToString();
			ducksPerSecondText.text = ducksPerSecond.ToString() + " Ducks per second";
            upgradeShopCostText.text = upgradeShopCost.ToString();
            upgradeShopCountText.text = upgradeShopCount.ToString();
        }
    }
	
	public void UpgradeInvestor()
    {
        if (ducks >= upgradeInvestorCost)
        {
            ducks -= upgradeInvestorCost;
            upgradeInvestorCost = Mathf.RoundToInt(upgradeInvestorCost * priceMultiplierPerPurchase);
			upgradeInvestorCount ++;
			ducksPerClick += upgradeInvestorDPC;
			ducksPerSecond += upgradeInvestorDPSPerBuilding * buildingCount;
			
			
			ducksText.text = ducks.ToString();
			ducksPerClickText.text = ducksPerClick.ToString();
			ducksPerSecondText.text = ducksPerSecond.ToString() + " Ducks per second";
            upgradeInvestorCostText.text = upgradeInvestorCost.ToString();
            upgradeInvestorCountText.text = upgradeInvestorCount.ToString();
        }
    }
	
	public void UpgradeFactory()
    {
        if (ducks >= upgradeFactoryCost)
        {
            ducks -= upgradeFactoryCost;
            upgradeFactoryCost = Mathf.RoundToInt(upgradeFactoryCost * priceMultiplierPerPurchase);
			upgradeFactoryCount ++;
			buildingCount ++;
			ducksPerSecond += upgradeFactoryDPS;
			
			ducksText.text = ducks.ToString();
			ducksPerSecondText.text = ducksPerSecond.ToString() + " Ducks per second";
            upgradeFactoryCostText.text = upgradeFactoryCost.ToString();
            upgradeFactoryCountText.text = upgradeFactoryCount.ToString();
        }
    }
	
	public void UpgradeOffice()
    {
        if (ducks >= upgradeOfficeCost)
        {
            ducks -= upgradeOfficeCost;
            upgradeOfficeCost = Mathf.RoundToInt(upgradeOfficeCost * priceMultiplierPerPurchase);
			upgradeOfficeCount ++;
			buildingCount ++;
			ducksPerSecond += upgradeOfficeDPS;
			
			ducksText.text = ducks.ToString();
			ducksPerSecondText.text = ducksPerSecond.ToString() + " Ducks per second";
            upgradeOfficeCostText.text = upgradeOfficeCost.ToString();
            upgradeOfficeCountText.text = upgradeOfficeCount.ToString();
        }
    }
	
	public void UnlockMama()
	{
		if (ducks >= upgradeMamaCost)
		{
			mamaLock.SetActive(false);
		}
	}
	
	public void UnlockShop()
	{
		if (ducks >= upgradeShopCost)
		{
			shopLock.SetActive(false);
		}
	}
	
	public void UnlockInvestor()
	{
		if (ducks >= upgradeInvestorCost)
		{
			investorLock.SetActive(false);
		}
	}
	
	public void UnlockFactory()
	{
		if (ducks >= upgradeFactoryCost)
		{
			factoryLock.SetActive(false);
		}
	}
	
	public void UnlockOffice()
	{
		if (ducks >= upgradeOfficeCost)
		{
			officeLock.SetActive(false);
		}
	}
}