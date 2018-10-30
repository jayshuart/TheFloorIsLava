using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraEffect : MonoBehaviour {

        public float intensity;
        private Material material;
        [SerializeField] private Shader shader;

        // Creates a private material used to the effect
        void Awake ()
        {
            material = new Material( shader);
        }

        // Postprocess the image
        void OnRenderImage (RenderTexture source, RenderTexture destination)
        {
            //check if intensity is off
            if (intensity == 0)
            {
                //no material = no effect for post process
                Graphics.Blit (source, destination);
                return;
            }
            
            material.SetFloat("_Intensity", intensity); //scale intensity of shader
            Graphics.Blit (source, destination, material); //apply shader via material
        }
    }
