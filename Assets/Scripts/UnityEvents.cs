using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// A containter class for all UnityEvents.
/// </summary>
#region Generic
[System.Serializable]
public class UnityIntEvent : UnityEvent<int> { }

[System.Serializable]
public class UnityFloatEvent : UnityEvent<float> { }

[System.Serializable]
public class UnityStringEvent : UnityEvent<string> { }

[System.Serializable]
public class UnityBoolEvent : UnityEvent<bool> { }
#endregion

#region Unity

#endregion

#region Custom

#endregion