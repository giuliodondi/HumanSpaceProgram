﻿using HSP.Input;
using HSP.Time;
using UnityEngine;
using UnityPlus.Input;

namespace HSP.Vanilla.Scenes.GameplayScene
{
    /// <summary>
    /// Invoked when the player toggles the escape (pause) menu in the gameplay scene.
    /// </summary>
    public static class HSPEvent_ON_ESCAPE_GAMEPLAY
    {
        public const string ID = HSPEvent.NAMESPACE_HSP + ".escape.gameplay";
    }

    /// <summary>
    /// Controls the invocation of the `escape` / pause event in the `gameplay` scene.
    /// </summary>
    [DisallowMultipleComponent]
    public class GameplaySceneEscapeInputController : MonoBehaviour
    {
        void OnEnable()
        {
            HierarchicalInputManager.AddAction( InputChannel.ESCAPE, InputChannelPriority.MEDIUM, Input_Escape );
        }

        void OnDisable()
        {
            HierarchicalInputManager.RemoveAction( InputChannel.ESCAPE, Input_Escape );
        }

        private bool Input_Escape( float value )
        {
            if( !TimeManager.LockTimescale )
            {
                if( TimeManager.IsPaused )
                {
                    TimeManager.Unpause();
                    HSPEvent.EventManager.TryInvoke( HSPEvent_ON_ESCAPE_GAMEPLAY.ID );
                }
                else
                {
                    TimeManager.Pause();
                    HSPEvent.EventManager.TryInvoke( HSPEvent_ON_ESCAPE_GAMEPLAY.ID );
                }
            }
            return false;
        }
    }
}