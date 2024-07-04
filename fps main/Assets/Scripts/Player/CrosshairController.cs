using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairController : MonoBehaviour
{


    
    [SerializeField] RectTransform crosshairRTUp;
    [SerializeField] RectTransform crosshairRTRight;
    [SerializeField] RectTransform crosshairRTDown;
    [SerializeField] RectTransform crosshairRTLeft;
    [SerializeField] RectTransform crosshairRTGap;


    [SerializeField] Image crosshairColor1;
    [SerializeField] Image crosshairColor2;
    [SerializeField] Image crosshairColor3;
    [SerializeField] Image crosshairColor4;

 
    int tmpCG, tmpCH, tmpCW;
    float tmpCR, tmpCGN, tmpCB;


    void Awake()
    {
        tmpCG = PlayerPrefs.GetInt("CrosshairGap");
        tmpCH = PlayerPrefs.GetInt("CrosshairHeight");
        tmpCW = PlayerPrefs.GetInt("CrosshairWidth");
        tmpCR = PlayerPrefs.GetFloat("CrosshairRed");
        tmpCGN = PlayerPrefs.GetFloat("CrosshairGreen");
        tmpCB = PlayerPrefs.GetFloat("CrosshairBlue");



        crosshairColor1.color = new Color(tmpCR,tmpCGN,tmpCB,1);
        crosshairColor2.color = new Color(tmpCR,tmpCGN,tmpCB,1);
        crosshairColor3.color = new Color(tmpCR,tmpCGN,tmpCB,1);
        crosshairColor4.color = new Color(tmpCR,tmpCGN,tmpCB,1);

       // crosshairRTUp.sizeDelta = new Vector2(crosshairRTUp.sizeDelta.x + tmpCW,crosshairRTUp.sizeDelta.y + tmpCH);
       // crosshairRTRight.sizeDelta = new Vector2(crosshairRTRight.sizeDelta.x + tmpCH,crosshairRTRight.sizeDelta.y + tmpCW);
       // crosshairRTDown.sizeDelta = new Vector2(crosshairRTDown.sizeDelta.x + tmpCW,crosshairRTDown.sizeDelta.y + tmpCH);
       // crosshairRTLeft.sizeDelta = new Vector2(crosshairRTLeft.sizeDelta.x + tmpCH,crosshairRTLeft.sizeDelta.y + tmpCW);
       // crosshairRTGap.sizeDelta = new Vector2(crosshairRTGap.sizeDelta.x + tmpCG,crosshairRTGap.sizeDelta.y + tmpCG);

        crosshairRTUp.sizeDelta = new Vector2(tmpCW, tmpCH);
        crosshairRTRight.sizeDelta = new Vector2(tmpCH, tmpCW);
        crosshairRTDown.sizeDelta = new Vector2(tmpCW, tmpCH);
        crosshairRTLeft.sizeDelta = new Vector2(tmpCH, tmpCW);
        crosshairRTGap.sizeDelta = new Vector2(tmpCG, tmpCG);
    }




    public void ChangeCrosshairProperties()
    {
        tmpCG = PlayerPrefs.GetInt("CrosshairGap");
        tmpCH = PlayerPrefs.GetInt("CrosshairHeight");
        tmpCW = PlayerPrefs.GetInt("CrosshairWidth");
        tmpCR = PlayerPrefs.GetFloat("CrosshairRed");
        tmpCGN = PlayerPrefs.GetFloat("CrosshairGreen");
        tmpCB = PlayerPrefs.GetFloat("CrosshairBlue");



        crosshairColor1.color = new Color(tmpCR,tmpCGN,tmpCB,1);
        crosshairColor2.color = new Color(tmpCR,tmpCGN,tmpCB,1);
        crosshairColor3.color = new Color(tmpCR,tmpCGN,tmpCB,1);
        crosshairColor4.color = new Color(tmpCR,tmpCGN,tmpCB,1);

        //crosshairRTUp.sizeDelta = new Vector2(crosshairRTUp.sizeDelta.x + tmpCW,crosshairRTUp.sizeDelta.y + tmpCH);
       // crosshairRTRight.sizeDelta = new Vector2(crosshairRTRight.sizeDelta.x + tmpCH,crosshairRTRight.sizeDelta.y + tmpCW);
       // crosshairRTDown.sizeDelta = new Vector2(crosshairRTDown.sizeDelta.x + tmpCW,crosshairRTDown.sizeDelta.y + tmpCH);
       // crosshairRTLeft.sizeDelta = new Vector2(crosshairRTLeft.sizeDelta.x + tmpCH,crosshairRTLeft.sizeDelta.y + tmpCW);
       // crosshairRTGap.sizeDelta = new Vector2(crosshairRTGap.sizeDelta.x + tmpCG,crosshairRTGap.sizeDelta.y + tmpCG);

        crosshairRTUp.sizeDelta = new Vector2(0 + tmpCW, 0 + tmpCH);
        crosshairRTRight.sizeDelta = new Vector2(0 + tmpCH, 0 + tmpCW);
        crosshairRTDown.sizeDelta = new Vector2(0 + tmpCW, 0 + tmpCH);
        crosshairRTLeft.sizeDelta = new Vector2(0 + tmpCH, 0 + tmpCW);
        crosshairRTGap.sizeDelta = new Vector2(0 + tmpCG, 0 + tmpCG);

    }
}
