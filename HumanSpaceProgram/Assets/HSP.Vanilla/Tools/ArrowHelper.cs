using HSP.Vanilla.Components;
using System;
using UnityEngine;

namespace HSP.Vanilla.Tools
{
    public static class ArrowHelper
    {
        /// <summary>
        /// Creates a directional arrow at the given position and orientation.
        /// </summary>
        public static GameObject CreateArrow(
            String name,
            Vector3 position,
            Vector3 direction,
             FControlFrame controlFrame,
            Transform parent = null,
            Color? color = null
        ) {
            GameObject arrowPrefab = Resources.Load<GameObject>( "Meshes/translate_handle_1d" );
            if( arrowPrefab == null )
            {
                Debug.LogError( "Arrow prefab not found at Resources/meshes/translate_handle_1d" );
                return null;
            }

            GameObject arrow = GameObject.Instantiate( arrowPrefab, position, Quaternion.identity, parent );
            arrow.name = name;

            // Attach control frame storage
            var gizmo = arrow.AddComponent<ArrowGizmo>();
            gizmo.ControlFrame = controlFrame;

            // Set color
            var renderer = arrow.GetComponentInChildren<MeshRenderer>();
            if( renderer != null )
            {
                Material mat = new Material( Shader.Find( "Standard" ) );
                mat.color = color ?? Color.red;
                renderer.material = mat;
            }

            // Direction
            if( controlFrame != null )
            {
                direction = controlFrame.GetRotation() * direction;
            }

            if( direction != Vector3.zero )
            {
                arrow.transform.rotation = Quaternion.LookRotation( direction.normalized );
            }

            return arrow;
        }

        public static void UpdateArrowDirection( GameObject arrow, Vector3 direction)
        {
            if( arrow == null )
            {
                Debug.LogWarning( "UpdateArrowDirection called with null arrow." );
                return;
            }

            direction = arrow.GetComponent<ArrowGizmo>().ControlFrame.GetRotation() * direction;

            if( direction != Vector3.zero )
            {
                arrow.transform.rotation = Quaternion.LookRotation( direction.normalized );
            }
        }

    }

    public class ArrowGizmo : MonoBehaviour
    {
        public FControlFrame ControlFrame;
        public void DestroyArrow()
        {
            Destroy( gameObject );
        }
    }
}
