using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour {

    [System.Serializable]
    public struct DialogContent
    {
        public DialogContent(Sprite _relevantImage, string _text)   //Constructor for usage in function: addDialog()
        {
            relevantImage = _relevantImage;
            text = _text;
        }
        public Sprite relevantImage;                                //The relevant image to display
        public string text;                                         //The text to display
    }
    public List<DialogContent> dialogs;                             //To hold multiple dialogs(i.e. Could be holding sentences, so on tap/click will cycle through them)

    int currDialog = 0;                                             //index of current dialog to display

    /*****Useful holders***/
    Image myImageElement;
    Text  myTextElement;
    /**********************/

    // Use this for initialization
    void Start () {
        //this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, 500);
        //myImageElement = this.gameObject.AddComponent<Image>();
        //myImageElement.sprite = Background;
        myImageElement = this.gameObject.transform.Find("Image").GetComponent<Image>(); //Getting component for usage
        myTextElement = this.gameObject.transform.Find("Text").GetComponent<Text>();    //Getting component for usage
        ChangeDialog();                                                                 //Calls the function for the first time, to assign the first dialog
    }
	
	// Update is called once per frame
	void Update () {
        if (currDialog > dialogs.Count - 1)     //if currDialog is more than the number of dialogs 
        {
            this.gameObject.SetActive(false);   //Sets the Dialog box GameObject to false (hides from the player's view)
        }
		else if(Input.GetMouseButtonUp(0))      //Gets input
        {
            ++currDialog;                       //Increment currDialog
            ChangeDialog();                     //Calls function to do the necessary
        }

	}
    void ChangeDialog()
    {
        if (currDialog >= dialogs.Count)                                                                                                    //Don't do anything if currDialog is more than the number of preset dialogs
            return;
        if (!this.gameObject.activeInHierarchy)
            this.gameObject.SetActive(true);
        if (dialogs[currDialog].relevantImage != null)                                                                                      //If there is image for the dialog
        {
            myImageElement.gameObject.SetActive(true);                                                                                      //Set the image GameObject to Active
            myImageElement.sprite = dialogs[currDialog].relevantImage;                                                                      //Assign the relevant image
            Vector2 ImageScaling = new Vector2(dialogs[currDialog].relevantImage.rect.width, dialogs[currDialog].relevantImage.rect.height);//Get the scaling of the provided image
            if(ImageScaling.y > 283)                                                                                                        //If the provided image size is too big
            {
                ImageScaling.x *= 283f / ImageScaling.y ;                                                                                   //scale x down using the ratio(max height / image's height) multiplied by x, this will scale the x to the appropriate width
                ImageScaling.y = 283f;                                                                                                      //scale y to the max height value
            }
            myImageElement.rectTransform.sizeDelta = ImageScaling;                                                                          //Assign the proper scaling to the image object
        }
        else
        {
            myImageElement.sprite = null;                                                                                                   //Assign no null to Image when there is no provided image
            myImageElement.gameObject.SetActive(false);                                                                                     //Set Image to not active so there will not be white patch
        }
        myTextElement.text = dialogs[currDialog].text;                                                                                      //Set the text of the Text element to the provided string of words
    }
    //Function to allow adding of dialog to existing dialogs externally
    public void addDialog(string textForDialog, Sprite relevantImage = null)
    {
        dialogs.Add(new global::DialogBox.DialogContent(relevantImage, textForDialog));     //Adding new dialog entity
        if (!this.gameObject.activeInHierarchy)                                             //In the event the DialogBox is not active in hierarchy
        {
            this.gameObject.SetActive(true);                                                //Set DialogBox to active
            ChangeDialog();                                                                 //Set-up the information so DialogBox will display appropriate informations
        }
    }
}
