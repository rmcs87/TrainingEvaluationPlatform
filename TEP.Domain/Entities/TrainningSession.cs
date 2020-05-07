using System;
using System.Collections.Generic;
using System.Text;

namespace TEP.Domain.Entities
{
    class TrainningSession : EntityBase
    {
        public ProDefinition Procedure { get; private set; }
        public ProExecution Execution { get; private set; }
        public DateTime Date { get; private set; }
        public Operator Operator { get; private set; }
        public Supervisor Supervisor { get; private set; }

        public void Evaluate()
        {
            //2Do (the same as Procedure ??? somehting is missing)
            throw new NotImplementedException();
        }
        public void GetSocre()
        {
            //2Do (the same as Procedure ??? somehting is missing)
            throw new NotImplementedException();
        }
        public void GenerateReport()
        {
            //2Do (the same as Procedure ??? somehting is missing)
            throw new NotImplementedException();
        }
    }
}
