using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Rotator : MonoBehaviour
{

    public bool firstCheckpointCleared;
    [SerializeField]
    private Transform[] checkPoints = new Transform[3];

    public int checkPointToMove = 0;
    [SerializeField]
    private GameObject right;
    [SerializeField]
    private GameObject left;
    public List<GameObject> armorPiece;
    [SerializeField]
    private int selectedItem = 0;
    [SerializeField]
    private float speed;

    public TMP_Text itemName;

    public static string selectedItemCategory;


    void Start()
    {

        Debug.Log("rotator start function called");

        MeshRenderer[] allChildren = this.gameObject.GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer child in allChildren)

            if (child.gameObject.CompareTag(selectedItemCategory))
            {
                armorPiece.Add(child.gameObject);
                Debug.Log("TAVARA LUETTELOSSA: " + child.gameObject.name +  " TAG: " + child.gameObject.tag.ToString());
            }
            else
            {
                Debug.Log("TAVARA EI LUETTELOSSA: " + child.gameObject.name + " TAG: " + child.gameObject.tag.ToString());
            }
    }




    public void Left()
    {
        if (selectedItem + 1 < armorPiece.Count)
        {
            selectedItem++;
            checkPointToMove = 0;
            firstCheckpointCleared = false;

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

        }
        else
        {
            Debug.Log("Lopeta rightin klikkaus");
        }

    }


    void FixedUpdate()
    {

        itemName.text = armorPiece[selectedItem].gameObject.name;

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

    public static void setItemCategory(string s)
    {
        selectedItemCategory = s;
    }


}
