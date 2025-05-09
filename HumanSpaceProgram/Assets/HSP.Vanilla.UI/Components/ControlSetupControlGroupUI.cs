﻿using HSP.ControlSystems;
using HSP.ControlSystems.Controls;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityPlus.AssetManagement;
using UnityPlus.UILib;
using UnityPlus.UILib.UIElements;

namespace HSP.Vanilla.UI.Components
{
    /// <summary>
    /// UI for <see cref="ControlGroup"/>-derived classes.
    /// </summary>
    public class ControlSetupControlGroupUI : MonoBehaviour
    {
        public const float ROW_HEIGHT = 15.0f;
        public const float VERTICAL_PADDING = 2f;

        /// <summary>
        /// The group that is the parent of this group. May be null.
        /// </summary>
        public ControlSetupControlGroupUI ParentGroup { get; private set; }

        /// <summary>
        /// The component UI that this group belongs to. Every nested group belongs to the same node.
        /// </summary>
        public ControlSetupWindowComponentUI ComponentUI { get; private set; }

        /// <summary>
        /// The vertical height of the group as calculated from its contents, in [px].
        /// </summary>
        public float Height { get; private set; }

        object _target; // control group instance or component instance.
        NamedControlAttribute _attr;

        ControlSetupControlUI[] _inputUIs;
        ControlSetupControlUI[] _outputUIs;

        ControlSetupControlGroupUI[] _childGroups;

        internal UIPanel panel; // this is ugly. UI should provide a way of creating new UI elements easily, without boilerplate, and with restrictions what can be a child, etc.

        internal IEnumerable<ControlSetupControlUI> GetInputsRecursive()
        {
            List<ControlSetupControlUI> inputs = new List<ControlSetupControlUI>();

            inputs.AddRange( _inputUIs );

            foreach( var group in _childGroups )
            {
                inputs.AddRange( group.GetInputsRecursive() );
            }
            return inputs;
        }

        internal IEnumerable<ControlSetupControlUI> GetOutputsRecursive()
        {
            List<ControlSetupControlUI> outputs = new List<ControlSetupControlUI>();

            outputs.AddRange( _outputUIs );

            foreach( var group in _childGroups )
            {
                outputs.AddRange( group.GetOutputsRecursive() );
            }
            return outputs;
        }

        private void RedrawControls()
        {
            IEnumerable<(object member, NamedControlAttribute attr)> controls = ControlUtils.GetControlsAndGroups( _target );

            List<ControlSetupControlUI> inputUIs = new();
            List<ControlSetupControlUI> outputUIs = new();
            List<ControlSetupControlGroupUI> childGroups = new();

            float currentY = VERTICAL_PADDING;
            foreach( var (member, attr) in controls )
            {
                if( member is ControlGroup group )
                {
                    var groupUI = ControlSetupControlGroupUI.Create( this, currentY, group, attr );
                    childGroups.Add( groupUI );

                    currentY += groupUI.Height;
                }
                else if( member is ControlGroup[] groupArray )
                {
                    for( int i = 0; i < groupArray.Length; i++ )
                    {
                        // Do we want this 'create on null' behaviour? seems kinda like it's not the responsibility of this class.
                        groupArray[i] ??= (ControlGroup)Activator.CreateInstance( groupArray.GetType().GetElementType() );

                        var groupUI = ControlSetupControlGroupUI.Create( this, currentY, groupArray[i], attr );
                        childGroups.Add( groupUI );

                        currentY += groupUI.Height;
                    }
                }
                else if( ControlUtils.IsSubtypeOf( member.GetType(), typeof( List<> ), typeof( ControlGroup ) ) )
                {
                    System.Collections.IList groupList = (System.Collections.IList)member;

                    for( int i = 0; i < groupList.Count; i++ )
                    {
                        groupList[i] ??= (ControlGroup)Activator.CreateInstance( groupList.GetType().GetGenericArguments()[0] );

                        var groupUI = ControlSetupControlGroupUI.Create( this, currentY, groupList[i], attr );
                        childGroups.Add( groupUI );

                        currentY += groupUI.Height;
                    }
                }
                //

                if( member is ControlleeInputBase input )
                {
                    inputUIs.Add( ControlSetupControlUI.Create( this, currentY, input, attr ) );
                    currentY += ROW_HEIGHT;
                }
                else if( member is ControlleeInputBase[] inputArray )
                {
                    for( int i = 0; i < inputArray.Length; i++ )
                    {
                        inputUIs.Add( ControlSetupControlUI.Create( this, currentY, inputArray[i], attr ) );
                        currentY += ROW_HEIGHT;
                    }

                }

                //

                else if( member is ControllerOutputBase output )
                {
                    outputUIs.Add( ControlSetupControlUI.Create( this, currentY, output, attr ) );
                    currentY += ROW_HEIGHT;
                }
                else if( member is ControllerOutputBase[] outputArray )
                {
                    for( int i = 0; i < outputArray.Length; i++ )
                    {
                        outputUIs.Add( ControlSetupControlUI.Create( this, currentY, outputArray[i], attr ) );
                        currentY += ROW_HEIGHT;
                    }
                }

                //

                else if( member is ControlParameterInputBase paramInput )
                {
                    inputUIs.Add( ControlSetupControlUI.Create( this, currentY, paramInput, attr ) );
                    currentY += ROW_HEIGHT;
                }
                else if( member is ControlParameterInputBase[] paramInputArray )
                {
                    for( int i = 0; i < paramInputArray.Length; i++ )
                    {
                        inputUIs.Add( ControlSetupControlUI.Create( this, currentY, paramInputArray[i], attr ) );
                        currentY += ROW_HEIGHT;
                    }
                }

                //

                else if( member is ControlParameterOutputBase paramOutput )
                {
                    outputUIs.Add( ControlSetupControlUI.Create( this, currentY, paramOutput, attr ) );
                    currentY += ROW_HEIGHT;
                }
                else if( member is ControlParameterOutputBase[] paramOutputArray )
                {
                    for( int i = 0; i < paramOutputArray.Length; i++ )
                    {
                        outputUIs.Add( ControlSetupControlUI.Create( this, currentY, paramOutputArray[i], attr ) );
                        currentY += ROW_HEIGHT;
                    }
                }
            }

            Height = currentY + VERTICAL_PADDING;

            _inputUIs = inputUIs.ToArray();
            _outputUIs = outputUIs.ToArray();
            _childGroups = childGroups.ToArray();
        }

        internal static ControlSetupControlGroupUI Create( ControlSetupWindowComponentUI node, Component component )
        {
            UIPanel panel = node.panel.AddPanel( new UILayoutInfo( UIFill.Horizontal(), UIAnchor.Top, -20, ROW_HEIGHT ), null );

            ControlSetupControlGroupUI groupUI = panel.gameObject.AddComponent<ControlSetupControlGroupUI>();
            groupUI.panel = panel;
            groupUI._target = component;
            groupUI._attr = null;
            groupUI.ComponentUI = node;

            groupUI.RedrawControls();

            panel.rectTransform.sizeDelta = new Vector2( panel.rectTransform.sizeDelta.x, groupUI.Height );

            return groupUI;
        }

        internal static ControlSetupControlGroupUI Create( ControlSetupControlGroupUI group, float verticalOffset, object target, NamedControlAttribute attr )
        {
            UIPanel panel = group.panel.AddPanel( new UILayoutInfo( UIFill.Horizontal(), UIAnchor.Top, -verticalOffset, ROW_HEIGHT ), AssetRegistry.Get<Sprite>( "builtin::Resources/Sprites/UI/control_group" ) );

            ControlSetupControlGroupUI groupUI = panel.gameObject.AddComponent<ControlSetupControlGroupUI>();
            groupUI.panel = panel;
            groupUI._target = target;
            groupUI._attr = attr;
            groupUI.ParentGroup = group;
            groupUI.ComponentUI = group.ComponentUI;

            groupUI.RedrawControls();

            panel.rectTransform.sizeDelta = new Vector2( panel.rectTransform.sizeDelta.x, groupUI.Height );

            return groupUI;
        }
    }
}