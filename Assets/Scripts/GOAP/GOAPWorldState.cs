using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOAPWorldState
{
    Comp comp = new Comp();
    HashSet<WorldData> hashSet;

    List<string> names = new List<string>();

    public GOAPWorldState()
    {
        hashSet = new HashSet<WorldData>(comp);
    }

    public GOAPWorldState(GOAPWorldState other)
    {
        hashSet = new HashSet<WorldData>(comp);

        for (int i = 0; i < other.names.Count; i++)
        {
            var data = other.GetData(other.names[i]);
            CreateElement(data.name, data.value);
        }
    }

    public bool CreateElement(string name, object value)
    {
        if (hashSet.Add(new WorldData(name, value)))
        {
            names.Add(name);
            return true;
        }
        return false;
    }

    public List<string> GetNames()
    {
        return new List<string>(names);
    }

    public void SetElementValue(string name, object value)
    {
        WorldData result = GetData(name);
        result.value = value;
    }

    public WorldData GetData(string name)
    {
        // We only compare names so the actual value is unimportant

        // Unity doesn't use a framework that has TryGetValue as a hashSet method >=(
        //hashSet.TryGetValue(new WorldData(name, 0), out WorldData result);
        WorldData result = null;
        WorldData target = new WorldData(name, 0);
        foreach (WorldData data in hashSet)
        {
            if (comp.Equals(target, data))
            {
                result = data;
                break;
            }
        }
        return result;
    }

    public object GetElementValue(string name)
    {
        return GetData(name).value;
    }

    public Type GetElementValue<Type>(string name)
    {
        //dynamic value = Convert.ChangeType(GetData(name).value, typeof(Type));
        var value = GetData(name).value;
        //Type friendly = value;
        return (Type)value;
    }

    // Compares this world state value with another worldstate value of the same name
    public bool CompareValue(GOAPWorldState comparator, string name)
    {
        var worldVal = this.GetElementValue(name);
        var compareVal = comparator.GetElementValue(name);

        //return (worldVal == compareVal);
        return worldVal.Equals(compareVal);
    }

    public bool CheckState(GOAPWorldState comparator)
    {
        foreach (var name in comparator.GetNames())
        {
            if (!CompareValue(comparator, name))
            {
                return false;
            }
        }
        // this Should almost definitely not be returning true as default but I will leave it for now as not to make errors
        return true;
    }
}

public class WorldData
{
    public string name;
    public object value;

    public WorldData(string name, object value)
    {
        this.name = name;
        this.value = value;
    }

    public Type ConvertValue<Type>()
    {
        return (Type)value;
    }
}

class Comp : IEqualityComparer<WorldData>
{
    public bool Equals(WorldData first, WorldData second)
    {
        return first.name.GetHashCode() == second.name.GetHashCode();
    }

    public int GetHashCode(WorldData pair)
    {
        return pair.name.GetHashCode();
    }
}
