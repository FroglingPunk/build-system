using System;
using System.Collections.Generic;
using UnityEngine;

public static class EBuildRuleExtensions
{
    public static IReadOnlyList<EBuildRule> AllValues => _allValues;

    private static readonly List<EBuildRule> _allValues = new();


    static EBuildRuleExtensions()
    {
        for (var i = 1; i < (int)Mathf.Pow(2, Enum.GetNames(typeof(EBuildRule)).Length); i *= 2)
        {
            _allValues.Add((EBuildRule)i);
        }
    }
}