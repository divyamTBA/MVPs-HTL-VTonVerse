using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class PlayerManager : MonoBehaviour
{
    public InputManager inputManager;
    public CameraManager cameraManager;
    public PlayerLocomotion playerLocomotion;

    public Button button;
    public Transform theatreSpawn;
    public Transform appleSpawn;
    public Transform adsSpawn;
    public Transform nikeSpawn;
    public Transform cafeSpawn;
    public Transform lobbySpawn;
    public CanvasGroup blackScreen;
    public void Teleport(Transform spawn)
    {
        transform.position = spawn.position;
    }
    public IEnumerator TeleportFade(Transform spawn)
    {

        blackScreen.DOFade(1, 0.0f);

        Teleport(spawn);
        yield return new WaitForSeconds(2f);
        blackScreen.DOFade(0, 2);

    }
    private void Start()
    {
        // button = GameObject.Find("tesstButton").GetComponent<Button>();
        // spawn = GameObject.Find("spawntest").transform;
        //   theatreSpawn = GameObject.Find("theatreSpawn").transform;
        appleSpawn = GameObject.Find("appleSpawn").transform;
        // adsSpawn = GameObject.Find("adsSpawn").transform;
        nikeSpawn = GameObject.Find("nikeSpawn").transform;
        //   cafeSpawn = GameObject.Find("cafeSpawn").transform;
        lobbySpawn = GameObject.Find("lobbySpawn").transform;
        blackScreen = GameObject.Find("blackScreen").GetComponent<CanvasGroup>();
        cameraManager = FindObjectOfType<CameraManager>();

        playerLocomotion = GetComponent<PlayerLocomotion>();

        inputManager = GetComponent<InputManager>();

        playerLocomotion.cameraObject = cameraManager.cameraTransform;

        cameraManager.inputManager = inputManager;

        cameraManager.targetTransform = transform;
    }


    private void Update()
    {
        Application.targetFrameRate = 70;


        inputManager.HandleAllInputs();

    }
    private void FixedUpdate()
    {
        playerLocomotion.HandleAllMovement();

    }

    private void LateUpdate()
    {

        cameraManager.HandleAllCameraMovement();

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Spawntrigger-apple"))
        {
            StartCoroutine(TeleportFade(appleSpawn));
        }
        else if (other.CompareTag("Spawntrigger-nike"))
        {
            StartCoroutine(TeleportFade(nikeSpawn));
        }


        else if (other.CompareTag("Spawntrigger-lobby"))
        {
            StartCoroutine(TeleportFade(lobbySpawn));
        }
        else if (other.CompareTag("AiNFT"))
        {
            SetCameraPerms.instance.StartWebView("https://nft.chaingpt.org");
        }
    }




}
