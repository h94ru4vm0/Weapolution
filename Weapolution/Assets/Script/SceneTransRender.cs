﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SceneTransRender : MonoBehaviour {
    #region Variables  
    public bool switchON;
    public Shader curShader;
    public float grayScaleAmount = 1.0f;

    bool startTrans;
    float totalTime, lastTime, gapTimeOffset;
    float glitchColorTime = 0.3f, glitchMoveTime = 0.3f, inGlitchMoveTime, inGlitchColorTime;
    float glitchY, glitchHeight, glitchColumnGap, glitchOffsetX, glitchWhiteChange;
    float glitchColR, glitchColG, glitchColB;
    private Material curMaterial;

    public enum shaderType {
        glitch,
        blackOut
    };
    shaderType currentType;
    #endregion

    #region Properties  
    public Material material
    {
        get
        {
            if (curMaterial == null)
            {
                curMaterial = new Material(curShader);
                curMaterial.hideFlags = HideFlags.HideAndDontSave;
            }
            return curMaterial;
        }
    }
    #endregion

    // Use this for initialization  
    void Start()
    {
        if (SystemInfo.supportsImageEffects == false)
        {
            enabled = false;
            return;
        }

        if (curShader != null && curShader.isSupported == false)
        {
            enabled = false;
        }
    }

    void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
    {
        
        if (curShader != null)
        {
            
            if (switchON) {

                switch (currentType) {
                    case shaderType.glitch:
                        OnGlitch();
                        break;
                }

                totalTime += Time.unscaledDeltaTime;
                Graphics.Blit(sourceTexture, destTexture, material);
            }
            
        }
        else
        {
            Graphics.Blit(sourceTexture, destTexture);
        }
    }

    // Update is called once per frame  
    void Update()
    {
        grayScaleAmount = Mathf.Clamp(grayScaleAmount, 0.0f, 1.0f);
    }

    void OnDisable()
    {
        if (curMaterial != null)
        {
            DestroyImmediate(curMaterial);
        }
    }

    void OnGlitch() {
        inGlitchMoveTime += Time.unscaledDeltaTime;
        if (totalTime - lastTime > 0.8f)
        {
            lastTime = totalTime;
            if (gapTimeOffset < 1.1f) gapTimeOffset += 0.35f;
            else gapTimeOffset = 1.1f;
        }
        if (inGlitchMoveTime >= glitchMoveTime)
        {
            Debug.Log("inGlitchMoveTime" + inGlitchMoveTime + "  glitchMoveTime" + glitchMoveTime);
            inGlitchMoveTime = 0.0f;
            glitchMoveTime = Random.Range(0.7f - 0.6f*gapTimeOffset , 1.2f - gapTimeOffset);
            glitchY = Random.Range(0.0f, 150.0f) * Random.Range(50.0f, 200.0f);
            glitchHeight = Random.Range(0.05f, 0.15f);
            glitchColumnGap = Random.Range(0.2f, 0.35f);
            glitchOffsetX = Random.Range(-0.3f, 0.3f);
            material.SetFloat("_glitchY", glitchY);
            material.SetFloat("_glitchHeight", glitchHeight);
            material.SetFloat("_glitchColumnGap", glitchColumnGap);
            material.SetFloat("_glitchOffsetX", glitchOffsetX);
        }
        inGlitchColorTime += Time.unscaledDeltaTime;
        if (inGlitchColorTime >= glitchColorTime)
        {
            inGlitchColorTime = 0.0f;
            glitchColorTime = Random.Range(0.8f - 0.6f * gapTimeOffset, 1.5f - gapTimeOffset);
            glitchColR = Random.Range(-0.15f, 0.15f);
            glitchColG = Random.Range(-0.15f, 0.15f);
            glitchColB = Random.Range(-0.15f, 0.15f);
            material.SetFloat("_glitchColOffestR", glitchColR);
            material.SetFloat("_glitchColOffestG", glitchColG);
            material.SetFloat("_glitchColOffestB", glitchColB);
        }
        if (startTrans) {
            glitchWhiteChange += Time.unscaledDeltaTime;
            //Debug.Log("asdasdsadsadasdsadasdasd" + glitchWhiteChange);
            material.SetFloat("_glitchWhiteChange", glitchWhiteChange);
        }
    }

    void OnBlack() {

    }
    public void SetTransRenderOn(shaderType _type) {
        switchON = true;
        currentType = _type;

    }
    public void SetStartTrans() {
        startTrans = true;
    }
}
