using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Test : MonoBehaviour {

    public Material MAT_ETC2;
    public Material MAT_EGB32;
    public Material MAT_CHROMA1;
    public Material MAT_CHROMA1_P;
    public Material MAT_CHROMA2;
    public Material MAT_CHROMA3;
    public Material MAT_CHROMA3_S;

    public GameObject BG_Root;
    public GameObject BG_Image;

    public UnityEngine.UI.Text Obj_Count;

    //// Use this for initialization
    //void Start () {
	
    //}
	
    //// Update is called once per frame
    //void Update () {
	
    //}

    public void OnAdd()
    {
        for (int i = 0; i < 10; ++i)
        {
            GameObject go = GameObject.Instantiate(BG_Image);
            //go.transform.parent = BG_Root.transform;
            (go.transform as RectTransform).SetParent(BG_Root.transform, false);
        }

        if (Obj_Count != null)
        {
            Obj_Count.text = string.Format("Obj {0} ", BG_Root.transform.childCount );
        }
    }

    void SetMaterial(Material m)
    {
        UnityEngine.UI.RawImage[] rawimages = BG_Root.GetComponentsInChildren<RawImage>();
        for (int i = 0; i < rawimages.Length; ++i)
        {
            rawimages[i].material = m;
        }
    }
    public  void OnRGB32()
    {
        SetMaterial(MAT_EGB32);
    }

    public void OnETC()
    {
        SetMaterial(MAT_ETC2);
    }

    public void OnChroma1()
    {
        SetMaterial(MAT_CHROMA1);
    }

    public void OnChroma1_P()
    {
        SetMaterial(MAT_CHROMA1_P);
    }

    public void OnChroma2()
    {
        SetMaterial(MAT_CHROMA2);
    }

    public void OnChroma3()
    {
        SetMaterial(MAT_CHROMA3);
    }

    public void OnChroma3_S()
    {
        SetMaterial(MAT_CHROMA3_S);
    }
}
