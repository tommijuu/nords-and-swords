using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnchantRotator : MonoBehaviour
{
    [Header("Setting correct armor and weapons on")]
    public GameObject axes;
    public GameObject swords;
    public GameObject hammers;
    [Header("Enchanted Materials")]
    public Material[] swordMaterials;
    public Material[] hammerMaterials;
    public Material[] axeMaterials;
    public bool firstCheckpointCleared;
    [SerializeField]
    private Transform[] checkPoints = new Transform[3];

    public int checkPointToMove = 0;
    [SerializeField]
    private GameObject right;
    [SerializeField]
    private GameObject left;
    public List<GameObject> armorPiece;
    public int selectedItem;
    [SerializeField]
    private float speed;



    public static string selectedItemCategory;
    [Header("Item Prices:")]
    public int[] axePrices = new int[5];
    public int[] swordPrices = new int[5];
    public int[] hammerPrices = new int[5];
    [Header("Item Stats:")]
    public string[] axeStats = new string[5];
    public string[] swordStats = new string[5];
    public string[] hammerStats = new string[5];
    [Header("Text fields")]
    public TMP_Text itemName;
    public TMP_Text cost;
    public TMP_Text stats;
    public TMP_Text currentGold;
    [Header("Enchanted Materials:")]
    public Material enchantedAxe;
    public Material enchantedSword;
    public Material enchantedHammer;
    void Start()
    {

        Debug.Log("rotator start function called");

        //MeshRenderer[] allChildren = this.gameObject.GetComponentsInChildren<MeshRenderer>();

        //foreach (MeshRenderer child in allChildren)

        //    if (child.gameObject.CompareTag(selectedItemCategory))
        //    {
        //        armorPiece.Add(child.gameObject);
        //        Debug.Log("TAVARA LUETTELOSSA: " + child.gameObject.name + " TAG: " + child.gameObject.tag.ToString());
        //    }
        //    else
        //    {
        //        Debug.Log("TAVARA EI LUETTELOSSA: " + child.gameObject.name + " TAG: " + child.gameObject.tag.ToString());
        //    }


        selectedItem = 0;


    }
    public void TurnOffOldweapons()
    {
        for (int i = 0; i < axes.transform.childCount; i++)
        {
            axes.transform.GetChild(i).gameObject.SetActive(false);
        }
        for (int i = 0; i < swords.transform.childCount; i++)
        {
            swords.transform.GetChild(i).gameObject.SetActive(false);
        }
        for (int i = 0; i < hammers.transform.childCount; i++)
        {
            hammers.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    public void SpawnOwnedItems()
    {
        Debug.Log("rotator SpawnOwnedItems function called");
        armorPiece.Clear();

        if (PlayerAttributes.pAxe > 0 && PlayerAttributes.pWeaponCategoryInUse == 2)
        {
            axes.transform.GetChild(PlayerAttributes.pAxe - 1).gameObject.SetActive(true);
            armorPiece.Add(axes.transform.GetChild(PlayerAttributes.pAxe - 1).gameObject);
        }
        if (PlayerAttributes.pSwords > 0 && PlayerAttributes.pWeaponCategoryInUse == 1)
        {
            swords.transform.GetChild(PlayerAttributes.pSwords - 1).gameObject.SetActive(true);
            armorPiece.Add(swords.transform.GetChild(PlayerAttributes.pSwords - 1).gameObject);

        }
        if (PlayerAttributes.pHammers > 0 && PlayerAttributes.pWeaponCategoryInUse == 3)
        {
            hammers.transform.GetChild(PlayerAttributes.pHammers - 1).gameObject.SetActive(true);
            armorPiece.Add(hammers.transform.GetChild(PlayerAttributes.pHammers - 1).gameObject);
        }
        if (armorPiece.Count > 0)
        {
            Switcher4();
        }


    }
    public void Left()
    {
        if (selectedItem + 1 < armorPiece.Count)
        {
            selectedItem++;
            checkPointToMove = 0;
            firstCheckpointCleared = false;
            Switcher4();
        }
        else
        {
            Debug.Log("Lopeta leftin klikkaus");
        }

    }
    public void Right()
    {

        if (selectedItem > 0)
        {
            selectedItem--;
            checkPointToMove = 0;
            firstCheckpointCleared = false;
            Switcher4();
        }
        else
        {
            Debug.Log("Lopeta rightin klikkaus");
        }

    }
    void Switcher4()
    {
        itemName.text = "NAME: " + armorPiece[selectedItem].gameObject.name;
        cost.text = "COST: " + armorPiece[selectedItem].GetComponent<MouseRotator>().enchantCost;
        stats.text = armorPiece[selectedItem].GetComponent<MouseRotator>().enchantStats;
    }
    public void Buy()
    {
        if (armorPiece.Count > 0)
        {
            if (PlayerAttributes.currentGold >= armorPiece[selectedItem].GetComponent<MouseRotator>().enchantCost)
            {
                if (armorPiece[selectedItem].GetComponent<Transform>().parent.gameObject.name == "Axes")
                {

                    PlayerAttributes.pAxeEnchanted = armorPiece[selectedItem].GetComponent<Transform>().GetSiblingIndex() + 1;
                    Debug.Log("pAxeEnchanted: " + PlayerAttributes.pAxeEnchanted);
                    axes.transform.GetChild(PlayerAttributes.pAxe - 1).gameObject.GetComponent<Renderer>().material = axeMaterials[PlayerAttributes.pAxeEnchanted - 1];
                }

                if (armorPiece[selectedItem].GetComponent<Transform>().parent.gameObject.name == "Swords")
                {


                    PlayerAttributes.pSwordsEnchanted = armorPiece[selectedItem].GetComponent<Transform>().GetSiblingIndex() + 1;
                    swords.transform.GetChild(PlayerAttributes.pSwords - 1).gameObject.GetComponent<Renderer>().material = swordMaterials[PlayerAttributes.pSwordsEnchanted - 1];
                }
                if (armorPiece[selectedItem].GetComponent<Transform>().parent.gameObject.name == "Hammers")
                {


                    PlayerAttributes.pHammersEnchanted = armorPiece[selectedItem].GetComponent<Transform>().GetSiblingIndex() + 1;
                    hammers.transform.GetChild(PlayerAttributes.pHammers - 1).gameObject.GetComponent<Renderer>().material = hammerMaterials[PlayerAttributes.pHammersEnchanted - 1];

                }
                PlayerAttributes.currentGold -= armorPiece[selectedItem].GetComponent<MouseRotator>().enchantCost;
            }
            else
            {
                Debug.Log("Not Enough Money");
            }
        }

    }
    void FixedUpdate()
    {
        if (currentGold.gameObject.activeSelf == true)
        {
            currentGold.text = "CURRENT GOLD: " + PlayerAttributes.currentGold;
        }

        //  itemName.text = armorPiece[selectedItem].gameObject.name;
        if (armorPiece.Count > 0)
        {
            for (int i = 0; i < armorPiece.Count; i++)
            {
                if (i == selectedItem && checkPointToMove != 2)
                {
                    armorPiece[i].GetComponent<CapsuleCollider>().enabled = true;



                    armorPiece[i].GetComponent<Rigidbody>().velocity = (checkPoints[checkPointToMove].position - armorPiece[i].transform.position)
                    * speed;

                }
                if (i == selectedItem && checkPointToMove == 2)
                {
                    armorPiece[i].GetComponent<CapsuleCollider>().enabled = true;
                    if (Vector3.Distance(this.gameObject.transform.position, armorPiece[i].transform.position) > 0.09f)
                    {


                        armorPiece[i].GetComponent<Rigidbody>().velocity += (this.gameObject.transform.position - armorPiece[i].transform.position).normalized
                        * speed * Time.deltaTime;
                    }

                }
                if (i < selectedItem)
                {
                    armorPiece[i].GetComponent<CapsuleCollider>().enabled = false;
                    armorPiece[i].transform.position +=
                    (left.transform.position - armorPiece[i].transform.position).normalized
                    * speed * Time.deltaTime;


                }
                if (i > selectedItem)
                {
                    armorPiece[i].GetComponent<CapsuleCollider>().enabled = false;
                    armorPiece[i].transform.position +=
                   (right.transform.position - armorPiece[i].transform.position).normalized
                   * speed * Time.deltaTime;
                }
            }
        }
    }

    public static void setItemCategory(string s)
    {
        selectedItemCategory = s;
    }


}
