using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class DIContainer : MonoBehaviour
{
    [Header("Components for injection")]
    [SerializeField] private MonoBehaviour[] _monoBehaviour;
    
    private readonly Dictionary<Type, object> _services = new();

    private void Awake()
    {
        RegisterServices();

        InjectAllComponents();
    }

    private void InjectAllComponents()
    {
        foreach (MonoBehaviour monoBehaviour in _monoBehaviour)
        {
            Debug.Log($"<color=#FF0000> Injecting: {monoBehaviour.GetType()}</color>");
            Inject(monoBehaviour);
        }
    }

    private void RegisterServices()
    {
        _services[typeof(IInputService)] = new InputService();
    }
    
    private void Inject(MonoBehaviour monoBehaviour)
    {
        Type type = monoBehaviour.GetType();

        var methodsInfo = type.GetMethods(
            BindingFlags.Public |
            BindingFlags.NonPublic |
            BindingFlags.Instance |
            BindingFlags.FlattenHierarchy
        );

        foreach (MethodInfo methodInfo in methodsInfo)
        {
            if (methodInfo.IsDefined(typeof(InjectAttribute)) == false)
                continue;

            var parametersInfo = methodInfo.GetParameters();
            var args = new object[parametersInfo.Length];

            for (int i = 0; i < parametersInfo.Length; i++)
            {
                Type paramType = parametersInfo[i].ParameterType;
                
                if (_services.TryGetValue(paramType, out var service))
                {
                    args[i] = service;
                }
                else
                {
                    Debug.LogError($"Service {paramType} not found for {type.Name}");
                    args[i] = null;
                }
            }

            methodInfo.Invoke(monoBehaviour, args);
        }
    }
}