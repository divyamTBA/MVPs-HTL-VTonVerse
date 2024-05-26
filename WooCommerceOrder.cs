using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class WooCommerceOrder : MonoBehaviour
{
    private string consumerKey = "ck_786f28bb18086d17433ff0c99801cccc395e7ee0";
    private string consumerSecret = "cs_b04dcddbf11dde1ba84bc6d13034698dccfe705c";
    private string storeUrl = "https://twis.in/shop";
    public static WooCommerceOrder instance;
    private void Awake()
    {
        instance = this;
    }
  public  void StartCreateOrder(int id, int quantity = 1)
    {
        StartCoroutine(CreateOrder(id,quantity));
    }

    IEnumerator CreateOrder(int id, int quantity=1)
    {
        // Order data
        OrderData orderData = new OrderData
        {
            payment_method = "Web3",
            payment_method_title = "DePay",
            set_paid = false,
            billing = new Billing
            {
                first_name = "John",
                last_name = "Doe",
                address_1 = "969 Market",
                city = "San Francisco",
                state = "CA",
                postcode = "94103",
                country = "US",
                email = "john.doe@example.com",
                phone = "(555) 555-5555"
            },
            line_items = new LineItem[]
            {
                new LineItem
                {
                    product_id = id,
                    quantity = quantity
                }
            }
        };

        string json = JsonUtility.ToJson(orderData);
        string url = $"{storeUrl}/wp-json/wc/v3/orders";
        UnityWebRequest request = UnityWebRequest.Post(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        string auth = System.Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(consumerKey + ":" + consumerSecret));
        request.SetRequestHeader("Authorization", "Basic " + auth);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Order creation failed: " + request.error);
        }
        else
        {
            Debug.Log("Order created: " + request.downloadHandler.text);
            OrderResponse orderResponse = JsonUtility.FromJson<OrderResponse>(request.downloadHandler.text);
            if (orderResponse != null && orderResponse.id > 0 && !string.IsNullOrEmpty(orderResponse.order_key))
            {
                string paymentUrl = GeneratePaymentUrl(orderResponse.id, orderResponse.order_key);
                Debug.Log("Payment link: " + paymentUrl);
                SetCameraPerms.instance.StartWebView(paymentUrl);
                // Here you can handle the payment URL, like opening it in a browser or in a WebView
            }
        }
    }

    private string GeneratePaymentUrl(int orderId, string orderKey)
    {
        return $"{storeUrl}/checkout/order-pay/{orderId}/?pay_for_order=true&key={orderKey}";
    }
}

[System.Serializable]
public class OrderData
{
    public string payment_method;
    public string payment_method_title;
    public bool set_paid;
    public Billing billing;
    public LineItem[] line_items;
}

[System.Serializable]
public class Billing
{
    public string first_name;
    public string last_name;
    public string address_1;
    public string city;
    public string state;
    public string postcode;
    public string country;
    public string email;
    public string phone;
}

[System.Serializable]
public class LineItem
{
    public int product_id;
    public int quantity;
}

[System.Serializable]
public class OrderResponse
{
    public int id;
    public string order_key;
}