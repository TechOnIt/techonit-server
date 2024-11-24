﻿using TechOnIt.Domain.Entities.Catalogs;

namespace TechOnIt.Domain.Events.Structures
{
    public class StructureUpdatedEvent : BaseEvent
    {
        public StructureUpdatedEvent(Structure structure)
        {
            Structure = structure;
        }

        public Structure Structure { get; set; }
    }
}