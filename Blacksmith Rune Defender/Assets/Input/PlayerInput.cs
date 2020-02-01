// GENERATED AUTOMATICALLY FROM 'Assets/Input/PlayerInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""Game"",
            ""id"": ""774ba5fe-1c0a-4b6b-b018-bf4ba04afa7b"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""5b823f42-c324-4f6a-9677-7381acf404fc"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""FireRune"",
                    ""type"": ""Button"",
                    ""id"": ""95c9333a-d11f-4f7a-b350-1784dc7aa3f7"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""StrengthRune"",
                    ""type"": ""Button"",
                    ""id"": ""da59ea75-33bb-4203-b8ba-53930e5d4d99"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""OccultRune"",
                    ""type"": ""Button"",
                    ""id"": ""4b65b51e-0480-404b-a592-38b5ecfad0d7"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""EnergyRune"",
                    ""type"": ""Button"",
                    ""id"": ""1da4f749-03c5-4f68-9069-c94e56882fba"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""D-Pad"",
                    ""id"": ""74b22186-bc45-4137-bd7a-366abd4f50ef"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Up"",
                    ""id"": ""9bc5a4c0-665c-476d-a8ff-c78ede3cd1c1"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Down"",
                    ""id"": ""95c8e8cc-7219-408c-a598-39f55f8f961f"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left"",
                    ""id"": ""0958ce56-1ff0-4179-b4f2-fd393617b226"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Right"",
                    ""id"": ""edd6db1b-778c-4574-82e0-d8b463e74587"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows"",
                    ""id"": ""e7807bab-ce4b-4d64-93a8-c8c5a35d6c34"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""9b518112-607b-4d8d-a528-c3fae85fc8e3"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""b20d8b41-4580-4759-9271-0426eed53c90"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Analog"",
                    ""id"": ""61ae8727-a5b7-484b-80c3-381ae9ccc6b8"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""fb104d6b-c572-44ca-b0cc-9f0e50abe90b"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""66f2535a-3b6c-41a5-8568-2718a698ec62"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""7377fa0b-b4f6-4013-b4fb-b3be8452f46e"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FireRune"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""378a0e27-15b8-4c74-a470-e8aa711f564a"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""StrengthRune"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""420f56eb-ab7d-4224-9928-1d4b7c2156ce"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OccultRune"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0165e993-6416-4927-bd82-ad8033401b1b"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""EnergyRune"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Game
        m_Game = asset.FindActionMap("Game", throwIfNotFound: true);
        m_Game_Move = m_Game.FindAction("Move", throwIfNotFound: true);
        m_Game_FireRune = m_Game.FindAction("FireRune", throwIfNotFound: true);
        m_Game_StrengthRune = m_Game.FindAction("StrengthRune", throwIfNotFound: true);
        m_Game_OccultRune = m_Game.FindAction("OccultRune", throwIfNotFound: true);
        m_Game_EnergyRune = m_Game.FindAction("EnergyRune", throwIfNotFound: true);
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

    // Game
    private readonly InputActionMap m_Game;
    private IGameActions m_GameActionsCallbackInterface;
    private readonly InputAction m_Game_Move;
    private readonly InputAction m_Game_FireRune;
    private readonly InputAction m_Game_StrengthRune;
    private readonly InputAction m_Game_OccultRune;
    private readonly InputAction m_Game_EnergyRune;
    public struct GameActions
    {
        private @PlayerInput m_Wrapper;
        public GameActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Game_Move;
        public InputAction @FireRune => m_Wrapper.m_Game_FireRune;
        public InputAction @StrengthRune => m_Wrapper.m_Game_StrengthRune;
        public InputAction @OccultRune => m_Wrapper.m_Game_OccultRune;
        public InputAction @EnergyRune => m_Wrapper.m_Game_EnergyRune;
        public InputActionMap Get() { return m_Wrapper.m_Game; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameActions set) { return set.Get(); }
        public void SetCallbacks(IGameActions instance)
        {
            if (m_Wrapper.m_GameActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_GameActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnMove;
                @FireRune.started -= m_Wrapper.m_GameActionsCallbackInterface.OnFireRune;
                @FireRune.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnFireRune;
                @FireRune.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnFireRune;
                @StrengthRune.started -= m_Wrapper.m_GameActionsCallbackInterface.OnStrengthRune;
                @StrengthRune.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnStrengthRune;
                @StrengthRune.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnStrengthRune;
                @OccultRune.started -= m_Wrapper.m_GameActionsCallbackInterface.OnOccultRune;
                @OccultRune.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnOccultRune;
                @OccultRune.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnOccultRune;
                @EnergyRune.started -= m_Wrapper.m_GameActionsCallbackInterface.OnEnergyRune;
                @EnergyRune.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnEnergyRune;
                @EnergyRune.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnEnergyRune;
            }
            m_Wrapper.m_GameActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @FireRune.started += instance.OnFireRune;
                @FireRune.performed += instance.OnFireRune;
                @FireRune.canceled += instance.OnFireRune;
                @StrengthRune.started += instance.OnStrengthRune;
                @StrengthRune.performed += instance.OnStrengthRune;
                @StrengthRune.canceled += instance.OnStrengthRune;
                @OccultRune.started += instance.OnOccultRune;
                @OccultRune.performed += instance.OnOccultRune;
                @OccultRune.canceled += instance.OnOccultRune;
                @EnergyRune.started += instance.OnEnergyRune;
                @EnergyRune.performed += instance.OnEnergyRune;
                @EnergyRune.canceled += instance.OnEnergyRune;
            }
        }
    }
    public GameActions @Game => new GameActions(this);
    public interface IGameActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnFireRune(InputAction.CallbackContext context);
        void OnStrengthRune(InputAction.CallbackContext context);
        void OnOccultRune(InputAction.CallbackContext context);
        void OnEnergyRune(InputAction.CallbackContext context);
    }
}
