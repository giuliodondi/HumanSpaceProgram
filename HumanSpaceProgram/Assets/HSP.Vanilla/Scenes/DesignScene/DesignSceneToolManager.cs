﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace HSP.Vanilla.Scenes.DesignScene
{
    /// <summary>
    /// Invoked after the currently active tool in the design scene has changed.
    /// </summary>
    public static class HSPEvent_AFTER_DESIGN_SCENE_TOOL_CHANGED
    {
        public const string ID = HSPEvent.NAMESPACE_HSP + ".designscene.tool.changed";
    }

    /// <summary>
    /// Manages the registered and active design scene viewport tools.
    /// </summary>
    public class DesignSceneToolManager : SingletonMonoBehaviour<DesignSceneToolManager>
    {
        private List<DesignSceneTool> _availableTools = new List<DesignSceneTool>();
        private DesignSceneTool _activeTool = null;

        public static Type ActiveToolType { get => instance._activeTool.GetType(); }

        public static bool HasTool<T>() where T : DesignSceneTool
        {
            if( instance == null )
            {
                throw new InvalidOperationException( $"{nameof( DesignSceneToolManager )} is accessible only in the design scene." );
            }

            return HasTool( typeof( T ) );
        }

        public static bool HasTool( Type toolType )
        {
            if( instance == null )
            {
                throw new InvalidOperationException( $"{nameof( DesignSceneToolManager )} is accessible only in the design scene." );
            }

            foreach( var tool in instance._availableTools )
            {
                if( tool.GetType() == toolType )
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Registers a tool with the specified type for future use.
        /// </summary>
        public static void RegisterTool<T>() where T : DesignSceneTool
        {
            if( instance == null )
            {
                throw new InvalidOperationException( $"{nameof( DesignSceneToolManager )} is accessible only in the design scene." );
            }

            foreach( var tool in instance._availableTools )
            {
                if( tool.GetType() == typeof( T ) )
                {
                    throw new InvalidOperationException( $"The tool of type {typeof( T ).FullName} has already been registered." );
                }
            }

            // Disabling the parent GameObject before adding the component (and disabling the component itself)
            //   ensures that Awake/Start of the tool aren't invoked until the tool is selected.
            bool wasActive = instance.gameObject.activeSelf;
            instance.gameObject.SetActive( false );
            T comp = instance.gameObject.AddComponent<T>();
            comp.enabled = false;
            instance.gameObject.SetActive( wasActive );

            instance._availableTools.Add( comp );
        }

        public static object UseDefaultTool()
        {
            try
            {
                return UseTool( instance._availableTools[0].GetType() );
            }
            catch
            {
                //
                return null;
            }
        }

        /// <summary>
        /// Selects a tool of a given type.
        /// </summary>
        /// <remarks>
        /// Tool instances are persisted. Selecting a tool, and going back to a previous one keeps its data.
        /// </remarks>
        /// <returns>The instance of the tool that was enabled.</returns>
        public static T UseTool<T>() where T : DesignSceneTool
        {
            return (T)UseTool( typeof( T ) );
        }

        /// <summary>
        /// Selects a tool of a given type.
        /// </summary>
        /// <remarks>
        /// Tool instances are persisted. Selecting a tool, and going back to a previous one keeps its data.
        /// </remarks>
        /// <returns>The instance of the tool that was enabled.</returns>
        public static object UseTool( Type toolType )
        {
            if( instance == null )
            {
                throw new InvalidOperationException( $"{nameof( DesignSceneToolManager )} is accessible only in the design scene." );
            }

            Type baseToolType = typeof( DesignSceneTool );
            if( !(baseToolType.IsAssignableFrom( toolType )) )
            {
                throw new ArgumentException( $"Can't register a tool that is not a {baseToolType.FullName}." );
            }

            DesignSceneTool tool = null;
            foreach( var t in instance._availableTools )
            {
                if( t.GetType() == toolType )
                {
                    tool = t;
                    break;
                }
            }

            if( tool == null )
            {
                throw new InvalidOperationException( $"A tool of type {toolType.FullName} has not been registered. Please register a tool type before trying to use it." );
            }

            // Tool already being used.
            if( instance._activeTool == tool )
            {
                return instance._activeTool;
            }

            if( instance._activeTool != null )
            {
                instance._activeTool.enabled = false;
            }

            instance._activeTool = tool;
            instance._activeTool.enabled = true;
            HSPEvent.EventManager.TryInvoke( HSPEvent_AFTER_DESIGN_SCENE_TOOL_CHANGED.ID );
            return instance._activeTool;
        }

        void Start()
        {
            try
            {
                UseTool( _availableTools[0].GetType() );
            }
            catch( InvalidOperationException )
            {
                Debug.LogError( $"Couldn't find any registered design scene tools." );
            }
        }

        void Update()
        {
            TrySelectToolByNumberKey();
        }

        private void TrySelectToolByNumberKey()
        {
            if( UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() )
                return;

            if( UnityEngine.Input.GetKeyDown( KeyCode.Alpha1 ) )
            {
                try
                {
                    UseTool( _availableTools[0].GetType() );
                }
                catch { }
            }
            if( UnityEngine.Input.GetKeyDown( KeyCode.Alpha2 ) )
            {
                try
                {
                    UseTool( _availableTools[1].GetType() );
                }
                catch { }
            }
            if( UnityEngine.Input.GetKeyDown( KeyCode.Alpha3 ) )
            {
                try
                {
                    UseTool( _availableTools[2].GetType() );
                }
                catch { }
            }
            if( UnityEngine.Input.GetKeyDown( KeyCode.Alpha4 ) )
            {
                try
                {
                    UseTool( _availableTools[3].GetType() );
                }
                catch { }
            }
            if( UnityEngine.Input.GetKeyDown( KeyCode.Alpha5 ) )
            {
                try
                {
                    UseTool( _availableTools[4].GetType() );
                }
                catch { }
            }
            if( UnityEngine.Input.GetKeyDown( KeyCode.Alpha6 ) )
            {
                try
                {
                    UseTool( _availableTools[5].GetType() );
                }
                catch { }
            }
            if( UnityEngine.Input.GetKeyDown( KeyCode.Alpha7 ) )
            {
                try
                {
                    UseTool( _availableTools[6].GetType() );
                }
                catch { }
            }
            if( UnityEngine.Input.GetKeyDown( KeyCode.Alpha8 ) )
            {
                try
                {
                    UseTool( _availableTools[7].GetType() );
                }
                catch { }
            }
            if( UnityEngine.Input.GetKeyDown( KeyCode.Alpha9 ) )
            {
                try
                {
                    UseTool( _availableTools[8].GetType() );
                }
                catch { }
            }
            if( UnityEngine.Input.GetKeyDown( KeyCode.Alpha0 ) )
            {
                try
                {
                    UseTool( _availableTools[9].GetType() );
                }
                catch { }
            }
        }
    }
}