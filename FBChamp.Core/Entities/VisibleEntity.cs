﻿namespace FBChamp.Core.Entities;

public abstract class VisibleEntity : Entity<Guid>
{
    public string Name { get; private set; }

    public int VisualOrder { get; private set; }

    public string Description { get; private set; }

    protected VisibleEntity()
    {
    }

    protected VisibleEntity(string name, int visualOrder, string description)
    {
        Name = name;
        VisualOrder = visualOrder;
        Description = description;
    }

    public void Update(string name, int visualOrder, string description)
    {
        Name = name;
        VisualOrder = visualOrder;
        Description = description;
    }
}