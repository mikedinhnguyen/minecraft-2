using UnityEngine;
using UnityEngine.UI;

public class TabSwitch : MonoBehaviour
{
    public Image tabs;
    public Sprite tab1;
    public Sprite tab2;
    public Sprite tab3;
    public Sprite tab4;
    
    public void ChangeTabs(int num)
    {
        switch (num) {
            case 1:
                tabs.sprite = tab1;
                break;
            case 2:
                tabs.sprite = tab2;
                break;
            case 3:
                tabs.sprite = tab3;
                break;
            case 4:
                tabs.sprite = tab4;
                break;
            default:
                break;
        }
    }
}
