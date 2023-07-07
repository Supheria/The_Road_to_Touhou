﻿using LocalUtilities.Interface;

namespace LocalUtilities.GeneralSerialization;

public abstract class Serialization<T> : ISerialization<T>
{
    public T? Source { get; set; } = default;

    public string LocalRootName { get; }

    protected Serialization(string localRootName) => LocalRootName = localRootName;
}