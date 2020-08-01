using System;
using TEP.Domain.Common;
using TEP.Domain.ValueObjects;

namespace TEP.Domain.Entities
{
    public class TrainningSession : AuditableEntity
    {
        private TrainningSession()
        {

        }

        public TrainningSession(Procedure targetProcedure, Procedure executedProcedure, DateTime date, Operator @operator, Supervisor supervisor, Performance performance)
        {
            TargetProcedure = targetProcedure;
            ExecutedProcedure = executedProcedure;
            Date = date;
            Operator = @operator;
            Supervisor = supervisor;
            Performance = performance;
        }

        public int Id { get; private set; }
        public Procedure TargetProcedure { get; private set; }       
        public Procedure ExecutedProcedure { get; private set; }       
        public DateTime Date { get; private set; }       
        public Operator Operator { get; private set; }      
        public Supervisor Supervisor { get; private set; }       
        public Performance Performance { get; private set; }
      
        public static void ProcessPerformance()
        {
            throw new NotImplementedException();
        }
        
        public static void GenerateReport()
        {
            throw new NotImplementedException();
        }
    }
}
