using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashBuilding : MonoBehaviour {

    bool _dir;
    Material[] _Newmat = new Material[2];
    Material[] _OldMat;

    // Use this for initialization
    void Start ()
    {
        //Material Switcheroo        
        var renderer = GetComponent<MeshRenderer>();
        _OldMat = renderer.materials;

        //Make new material arr
        for(int i = 0; i < renderer.materials.Length; i++)
        {
            _Newmat[i] = new Material(Shader.Find("Standard")); ;
            SetMatTransparent(_Newmat[i]);
            _Newmat[i].color = Color.yellow;
        }

        renderer.materials = _Newmat;

        StartCoroutine(FadeLoop(0, 1, 1, renderer.materials));        
    }
	

    private IEnumerator FadeLoop(float alphaStart, float alphaFinish, float time, Material[] m)
    {
        float elapsedTime = 0;
        Color c = m[0].color;
        c.a = alphaStart;

        while (elapsedTime < time)
        {
            c.a = Mathf.Lerp(alphaStart, alphaFinish, (elapsedTime / time));
            elapsedTime += Time.deltaTime;

            foreach(Material mat in m)
                mat.color = c;

            yield return new WaitForEndOfFrame();
        }

        if(_dir) //off
            yield return new WaitForSeconds(1);

        _dir = !_dir; //switch direction

        if (_dir)
            StartCoroutine(FadeLoop(1, 0, 1, m));
        else
            StartCoroutine(FadeLoop(0, 1, 1, m));
    }


    public void Stop()
    {
        StopAllCoroutines();
        if(_OldMat != null) //iunno this is null sometimes
            GetComponent<MeshRenderer>().materials = _OldMat;
    }

    //https://sassybot.com/blog/swapping-rendering-mode-in-unity-5-0/
    void SetMatTransparent(Material mat)
    {
        mat.SetFloat("_Mode", 3);
        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        mat.SetInt("_ZWrite", 0);
        mat.DisableKeyword("_ALPHATEST_ON");
        mat.DisableKeyword("_ALPHABLEND_ON");
        mat.EnableKeyword("_ALPHAPREMULTIPLY_ON");
        mat.renderQueue = 3000;
    }

}
