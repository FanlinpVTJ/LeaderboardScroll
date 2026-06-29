using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class LeaderboardIconProviderBase : ScriptableObject
{
    [SerializeField] private List<LeaderboardIconMapping> _iconMappings;

    protected Sprite GetIcon(int id)
    {
        for (int i = 0; i < _iconMappings.Count; i++)
        {
            if (_iconMappings[i].Id == id)
            {
                return _iconMappings[i].Icon;
            }
        }

        throw new ArgumentOutOfRangeException(nameof(id), id, $"Icon mapping does not contain icon for id {id}.");
    }
}
