using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VTonUI : MonoBehaviour
{
    public List<GameObject> shirtsList;
    public Button prevBUtton;
    public Button nextButton;
    public int index;
    private void Start()
    {
        nextButton.onClick.AddListener(Next);
        prevBUtton.onClick.AddListener(Prev);
    }
    void Next()
    {
        if (index < shirtsList.Count-1)
        {
            shirtsList[index].SetActive(false);
            index += 1;
            shirtsList[index].SetActive(true);
        }
        else
        {
            shirtsList[index].SetActive(false);
            index = 0;
            shirtsList[index].SetActive(true);
        }
    }
    void Prev()
    {
        if (index > 0)
        {
            shirtsList[index].SetActive(false);
            index -= 1;
            shirtsList[index].SetActive(true);
        }
        else
        {
            shirtsList[index].SetActive(false);
            index = shirtsList.Count-1;
            shirtsList[index].SetActive(true);
        }
    }
}
