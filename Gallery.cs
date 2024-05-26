using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Linq;
using TMPro;
public class Gallery : MonoBehaviour
{
    public string imageURL1;
    public string imageURL2;
    public string imageURL3;
    public string imageURL4;
    [HideInInspector] public byte[] data;
    [SerializeField] string imageName;

    public TMP_Text notification;
    public GameObject notificationUI;
    public Button _sendPostButton;
    private void Start()
    {
        _sendPostButton.onClick.AddListener(StartUpload);

    }
    public void PickImage()
    {
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            Debug.Log("Image path: " + path);
            if (path != null)
            {
                // Create Texture from selected image
                Texture2D texture = NativeGallery.LoadImageAtPath(path);
                data = File.ReadAllBytes(path);
                Debug.Log(data.Length);

                if (texture == null)
                {
                    Debug.Log("Couldn't load texture from " + path);
                    return;
                }

            }
        });

        Debug.Log("Permission result: " + permission);
    }

    public void HandleUpload()
    {
        UploadImage();
        UploadCaption();
    }

    public void UploadCaption()
    {
        //
    }


    IEnumerator UploadImage()
    {
        notification.text = "Uploading Image..";
        WWWForm form = new WWWForm();
        form.AddBinaryData("fileToUpload", data, "12h.jpg");
        
        using (UnityWebRequest www = UnityWebRequest.Post(imageURL1, form))
        {
            www.useHttpContinue = false;
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                notification.text = "Error uploading image";
                Debug.LogError(www.error);
                //Debug.LogError(imageURL);
   
            }
            else
            {
                //  notificationUI.SetActive(true);
                notification.text = "Image uploaded successfully";
                notification.text = "Processing image..";
                Debug.Log("Image uploaded successfully: " + www.downloadHandler.text);
                //Debug.Log(imageURL);
  
            }
        }
        using (UnityWebRequest www = UnityWebRequest.Post(imageURL2, form))
        {
            www.useHttpContinue = false;
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                notification.text = "Error uploading image";
                Debug.LogError(www.error);
                //Debug.LogError(imageURL);

            }
            else
            {
                //  notificationUI.SetActive(true);
                notification.text = "Image uploaded successfully";
                notification.text = "Processing image..";
                Debug.Log("Image uploaded successfully: " + www.downloadHandler.text);
                //Debug.Log(imageURL);
  
            }
        }
        using (UnityWebRequest www = UnityWebRequest.Post(imageURL3, form))
        {
            www.useHttpContinue = false;
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                notification.text = "Error uploading image";
                Debug.LogError(www.error);
                //Debug.LogError(imageURL);
 
            }
            else
            {
                //  notificationUI.SetActive(true);
                notification.text = "Image uploaded successfully";
                notification.text = "Processing image..";
                Debug.Log("Image uploaded successfully: " + www.downloadHandler.text);
                //Debug.Log(imageURL);

            }
        }
        using (UnityWebRequest www = UnityWebRequest.Post(imageURL4, form))
        {
            www.useHttpContinue = false;
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                notification.text = "Error uploading image";
                Debug.LogError(www.error);
                //Debug.LogError(imageURL);
                Invoke("CleanNotification", 2.5f);
            }
            else
            {
                //  notificationUI.SetActive(true);
                notification.text = "Virtual TryON Enabled";
                Debug.Log("Image uploaded successfully: " + www.downloadHandler.text);
                var vtonList = FindObjectsOfType<VTon>();
                foreach(VTon vTon in vtonList)
                {
                    vTon.VtonISEnabled = true;
                }
                //Debug.Log(imageURL);
                Invoke("CleanNotification", 2.5f);
            }
        }
    }

    public void StartUpload()
    {
        PickImage();
        StartCoroutine(UploadImage());
    }
    void CleanNotification()
    {
        notification.text = "";
        notificationUI.SetActive(false);
    }

}
