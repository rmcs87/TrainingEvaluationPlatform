using System;
using System.Collections.Generic;
using TEP.Domain.Common;
using TEP.Domain.ValueObjects;

namespace TEP.Domain.Entities
{

    public abstract class Step : AuditableEntity
    {     
        protected Step(Standard standard, string name)
        {
            Standard = standard;
            Name = name;
            ExecutionTime = new Duration(0);
        }

        protected Duration _expectedDuration;
        protected Duration _limitDuration;

        public int Id { get; private set; }
        public bool Active { get; protected set; }
        public bool Completed { get; protected set; }
        public Duration ExecutionTime{ get; protected set; }      
        public Duration ExpectedDuration { get => _expectedDuration ?? ThrowInvalidOperation(); }
        public string Name { get; private set; }        
        public Duration LimitDuration { get => _limitDuration ?? ThrowInvalidOperation(); }
        public Standard Standard { get; private set; }

        public abstract void UpdateDuration();
        public abstract LeafStep AdvanceStep(DateTime now);
        public abstract List<Step> GetSubSteps();

        private static Duration ThrowInvalidOperation()
        {
            throw new InvalidOperationException(message: $"{nameof(UpdateDuration)} must be executed before getting Time.");
        }
    }
}
