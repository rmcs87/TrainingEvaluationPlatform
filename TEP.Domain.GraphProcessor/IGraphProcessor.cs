namespace TEP.Domain.GraphProcessor
{
    public interface IGraphProcessor
    {

        //Estudar como dois dominios se comunicam!!!!


        /// <summary>
        /// Processes tow procedures and returns the similarity level of the execution compared to the procedure description;
        /// </summary>
        /// <param name="targetProcedure">The Traget procedure that contains the correct steps to be performed.</param>
        /// <param name="executedProcedure">The Executed procedure that contains the steps performed during a traiining.</param>
        /// <returns>Degree of similarity between both.</returns>
        /*public int CompareProcedures(Procedure targetProcedure, Procedure executedProcedure);*/
    }
}
