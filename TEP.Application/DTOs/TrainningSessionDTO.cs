using System;
using System.Collections.Generic;
using System.Text;

namespace TEP.Appication.DTO
{
    public class TrainningSessionDTO : DTOBase
    {
        public ProcedureDTO TargetProcedure { get; set; }
        public ProcedureDTO ExecutedProcedure { get; set; }
        public DateTime Date { get; set; }
        public OperatorDTO Operator { get; set; }
        public SupervisorDTO Supervisor { get; set; }
        public float Score { get; set; }
        public double TimeExecution { get; set; }
    }
}
