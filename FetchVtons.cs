using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class FetchVtons : MonoBehaviour
{
    public class ImageResponse
    {
        [JsonProperty("url-image")]
        public string UrlImage { get; set; }
    }

    // URL of the API endpoint
    public string apiUrl;

    // Reference to the GameObject's Renderer
    public RawImage targetRenderer;

    public void StartFetch(string url)
    {
        StartCoroutine(FetchImageFromAPI(url));
    }

    IEnumerator FetchImageFromAPI(string url)
    {
        // Send a request to the API endpoint
        UnityWebRequest apiRequest = UnityWebRequest.Get(url);
        yield return apiRequest.SendWebRequest();

        if (apiRequest.result == UnityWebRequest.Result.ConnectionError || apiRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(apiRequest.error);
            yield break;
        }

        // Parse the JSON response
        string jsonResponse = apiRequest.downloadHandler.text;
        Debug.Log("API Response: " + jsonResponse); // Log the response to check the format
        ImageResponse imageResponse = JsonConvert.DeserializeObject<ImageResponse>(jsonResponse);

        if (imageResponse == null)
        {
            Debug.LogError("Failed to parse JSON response.");
            yield break;
        }

        string imageUrl = imageResponse.UrlImage;
        if (string.IsNullOrEmpty(imageUrl))
        {
            Debug.LogError("Image URL is null or empty.");
            yield break;
        }

        Debug.Log("Image URL: " + imageUrl); // Log the image URL to verify it's correctly parsed

        // Download the image from the URL
        UnityWebRequest imageRequest = UnityWebRequestTexture.GetTexture(imageUrl);
        yield return imageRequest.SendWebRequest();

        if (imageRequest.result == UnityWebRequest.Result.ConnectionError || imageRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(imageRequest.error);
            yield break;
        }

        // Get the downloaded texture
        Texture2D downloadedTexture = ((DownloadHandlerTexture)imageRequest.downloadHandler).texture;

        // Apply the texture to the target Renderer
        targetRenderer.texture= downloadedTexture;
    }
}