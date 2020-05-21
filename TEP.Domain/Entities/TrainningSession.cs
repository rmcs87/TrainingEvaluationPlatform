using System;
using TEP.Domain.ValueObjects;

namespace TEP.Domain.Entities
{
    public class TrainningSession : EntityBase
    {
        /// <summary>
        /// Constructs a new TrainningSession.
        /// </summary>
        /// <param name="targetProcedure">The Procedure taht contains all Steps to performs this Trainning.</param>
        /// <param name="executedProcedure">The Procedure that contains all executed steps for this trainning.</param>
        /// <param name="date">The date when this trainning takes place.</param>
        /// <param name="operator">The person who is performing the trainning session.</param>
        /// <param name="supervisor">The person who is surpevising the trainning session.</param>
        /// <param name="performance">The results of the trainning.</param>
        public TrainningSession(Procedure targetProcedure, Procedure executedProcedure, DateTime date, Operator @operator, Supervisor supervisor, Performance performance)
        {
            TargetProcedure = targetProcedure;
            ExecutedProcedure = executedProcedure;
            Date = date;
            Operator = @operator;
            Supervisor = supervisor;
            Performance = performance;
        }

        //Properties
        /// <summary>
        /// Gets the procedure executed in this trainning.
        /// </summary>
        public Procedure TargetProcedure { get; private set; }
        /// <summary>
        /// Gets the procedure executed in this trainning.
        /// </summary>
        public Procedure ExecutedProcedure { get; private set; }
        /// <summary>
        /// Gets the date when this trainning ocurred.
        /// </summary>
        public DateTime Date { get; private set; }
        /// <summary>
        /// Gets who performed this training.
        /// </summary>
        public Operator Operator { get; private set; }
        /// <summary>
        /// Gets who supervisioned this trainning.
        /// </summary>
        public Supervisor Supervisor { get; private set; }
        /// <summary>
        /// Gets the performance of the operator.
        /// </summary>
        public Performance Performance { get; private set; }

        //Methods
        /// <summary>
        /// Processes the trainning session comparing the execution with the target procedure.
        /// </summary>
        public void ProcessPerformance()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Generates a Report Informing the Performance during this trainning.
        /// </summary>
        public void GenerateReport()
        {
            throw new NotImplementedException();
        }
    }
}
