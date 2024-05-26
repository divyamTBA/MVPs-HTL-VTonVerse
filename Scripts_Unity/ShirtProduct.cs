using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShirtProduct : MonoBehaviour
{
    public ProductSO so;
    public TMP_Text costtext;
    public Button buyButton;
    // Start is called before the first frame update
    void Start()
    {
        costtext.text = "Rs. "+so.cost.ToString();
        buyButton.onClick.AddListener(Buy); 
    }

    public void Buy()
    {
        WooCommerceOrder.instance.StartCreateOrder((int)so.id, so.quantity);
    }
}
