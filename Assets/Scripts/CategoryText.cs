using UnityEngine;
using TMPro;

public class CategoryText : MonoBehaviour
{
    public GameObject tab1;
    public GameObject tab2;
    public GameObject tab3;
    public GameObject tab4;
    public TextMeshProUGUI category;
    public ObjectiveCheck objCheck;

    // Update is called once per frame
    public void ChangeCategoryName()
    {
        if (tab1.activeInHierarchy)
        {
            category.text = tab1.name;
        }
        else if (tab2.activeInHierarchy)
        {
            category.text = tab2.name;
        }
        else if (tab3.activeInHierarchy)
        {
            category.text = tab3.name;
        }
        else if (tab4.activeInHierarchy)
        {
            category.text = tab4.name;
        }
        objCheck.CheckForBaseItem();
    }
}
