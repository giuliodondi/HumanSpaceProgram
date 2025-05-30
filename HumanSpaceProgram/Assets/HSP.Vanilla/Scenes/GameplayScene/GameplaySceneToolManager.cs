using System;
using System.Collections.Generic;
using UnityEngine;

namespace HSP.Vanilla.Scenes.GameplayScene
{
    /// <summary>
    /// Invoked after the currently active tool in the gameplay scene has changed.
    /// </summary>
    public static class HSPEvent_AFTER_GAMEPLAY_SCENE_TOOL_CHANGED
    {
        public const string ID = HSPEvent.NAMESPACE_HSP + ".gameplayscene.tool.changed";
    }

    /// <summary>
    /// Manages the registered and active gameplay scene viewport tools.
    /// </summary>
    public class GameplaySceneToolManager : SingletonMonoBehaviour<GameplaySceneToolManager>
    {
        private List<GameplaySceneTool> _availableTools = new List<GameplaySceneTool>();
        private GameplaySceneTool _activeTool = null;

        public static Type ActiveToolType { get => instance._activeTool.GetType(); }

        public static bool HasTool<T>() where T : GameplaySceneTool
        {
            if( instance == null )
            {
                throw new InvalidOperationException( $"{nameof( GameplaySceneToolManager )} is accessible only in the gameplay scene." );
            }

            return HasTool( typeof( T ) );
        }

        public static bool HasTool( Type toolType )
        {
            if( instance == null )
            {
                throw new InvalidOperationException( $"{nameof( GameplaySceneToolManager )} is accessible only in the gameplay scene." );
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
        public static void RegisterTool<T>() where T : GameplaySceneTool
        {
            if( instance == null )
            {
                throw new InvalidOperationException( $"{nameof( GameplaySceneToolManager )} is accessible only in the gameplay scene." );
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
        public static T UseTool<T>() where T : GameplaySceneTool
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
                throw new InvalidOperationException( $"{nameof( GameplaySceneToolManager )} is accessible only in the gameplay scene." );
            }

            Type baseToolType = typeof( GameplaySceneTool );
            if( !(baseToolType.IsAssignableFrom( toolType )) )
            {
                throw new ArgumentException( $"Can't register a tool that is not a {baseToolType.FullName}." );
            }

            GameplaySceneTool tool = null;
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
            HSPEvent.EventManager.TryInvoke( HSPEvent_AFTER_GAMEPLAY_SCENE_TOOL_CHANGED.ID );
            return instance._activeTool;
        }
    }
}