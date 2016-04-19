using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class JournalManager01 : MonoBehaviour
{
    public GameObject bookPage;
    public List<TextAsset> textFiles = new List<TextAsset>();

    private List<ButtonData> buttonDatas = new List<ButtonData>();
    private List<Button> inactiveButtons = new List<Button>();

    public struct ButtonData
    {
        public Button button;
        public int index;

        public ButtonData(Button button, int index)
        {
            this.button = button;
            this.index = index;
        }

    }

    void Awake()
    {
        bookPage.SetActive(false);
        GameObject pageButton = gameObject.FindChildObject("PageButton");

        for (int i = 0; i < textFiles.Count; i++)
        {
            GameObject newPageButton = Instantiate(pageButton, Vector3.zero, Quaternion.identity) as GameObject;
            newPageButton.name = "Button " + i;
            newPageButton.transform.SetParent(transform);
            newPageButton.transform.localScale = new Vector3(1, 1, 1);
            newPageButton.transform.position = Vector3.zero;
            newPageButton.transform.localPosition = new Vector3(pageButton.transform.localPosition.x, pageButton.transform.localPosition.y - (56 * i), pageButton.transform.localPosition.z);

            string[] lines = textFiles[i].text.Split('\n');
            Text buttonText = newPageButton.GetComponentInChildren<Text>();
            buttonText.text = lines[0];

            Button buttonScrips = newPageButton.GetComponent<Button>();
            inactiveButtons.Add(buttonScrips);
            buttonScrips.onClick.AddListener(delegate { OnButtonPress(buttonScrips); });

            newPageButton.SetActive(false);
        }

        activateButton(0);
        bookPage.SetActive(false);

        pageButton.SetActive(false);
    }

    void OnButtonPress(Button button)
    {
        for (int i = 0; i < buttonDatas.Count; i++)
        {
            if (buttonDatas[i].button == button)
            {
                bookPage.SetActive(true);
                bookPage.GetComponentInChildren<Text>().text = textFiles[buttonDatas[i].index].text;
            }
        }
    }

    public void activateButton(int i)
    {
        if (i < inactiveButtons.Count)
        {
            ButtonData buttonD = new ButtonData(inactiveButtons[i], i);
            if (!buttonDatas.Contains(buttonD))
            {
                buttonD.button.gameObject.SetActive(true);
                buttonDatas.Add(buttonD);
                bookPage.SetActive(true);
                bookPage.GetComponentInChildren<Text>().text = textFiles[i].text;
            }
        }
    }
}
