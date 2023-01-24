//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Script/InputActions/Input_Main.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @Input_Main : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Input_Main()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Input_Main"",
    ""maps"": [
        {
            ""name"": ""Main"",
            ""id"": ""933ad1f5-d627-48c7-88ca-87fc7371e7ab"",
            ""actions"": [
                {
                    ""name"": ""Set Desination"",
                    ""type"": ""Button"",
                    ""id"": ""c3840287-a590-4797-a656-9ed4b371ba2b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""2b72a753-60c5-4a77-9af9-95c619a06b25"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Set Desination"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Main
        m_Main = asset.FindActionMap("Main", throwIfNotFound: true);
        m_Main_SetDesination = m_Main.FindAction("Set Desination", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Main
    private readonly InputActionMap m_Main;
    private IMainActions m_MainActionsCallbackInterface;
    private readonly InputAction m_Main_SetDesination;
    public struct MainActions
    {
        private @Input_Main m_Wrapper;
        public MainActions(@Input_Main wrapper) { m_Wrapper = wrapper; }
        public InputAction @SetDesination => m_Wrapper.m_Main_SetDesination;
        public InputActionMap Get() { return m_Wrapper.m_Main; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MainActions set) { return set.Get(); }
        public void SetCallbacks(IMainActions instance)
        {
            if (m_Wrapper.m_MainActionsCallbackInterface != null)
            {
                @SetDesination.started -= m_Wrapper.m_MainActionsCallbackInterface.OnSetDesination;
                @SetDesination.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnSetDesination;
                @SetDesination.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnSetDesination;
            }
            m_Wrapper.m_MainActionsCallbackInterface = instance;
            if (instance != null)
            {
                @SetDesination.started += instance.OnSetDesination;
                @SetDesination.performed += instance.OnSetDesination;
                @SetDesination.canceled += instance.OnSetDesination;
            }
        }
    }
    public MainActions @Main => new MainActions(this);
    public interface IMainActions
    {
        void OnSetDesination(InputAction.CallbackContext context);
    }
}