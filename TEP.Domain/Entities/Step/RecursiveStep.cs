using System;
using System.Collections.Generic;
using System.Text;
using TEP.Domain.ValueObjects;

namespace TEP.Domain.Entities.Step
{
    public class RecursiveStep : Step
    {
        /// <summary>
        /// Creates a Step constituted of other Steps: both leaves or recursives.
        /// </summary>
        /// <param name="standard">The classification for a step, which implies how it will be hadled by the trainning APP.</param>
        /// <param name="name">The name fo this Step.</param>
        /// <param name="subSteps">The SubSteps to be Performed in this Step.</param>
        public RecursiveStep(Standard standard, string name, List<Step> subSteps) : base(standard, name)
        {
            SubSteps = subSteps;
        }

        private int _currentStepInddex = 0;
        /// <summary>
        /// Gets all SubSteps to be performed in this Step.
        /// </summary>
        public List<Step> SubSteps { get; private set; }
        /// <summary>
        /// Gets the Current subStep being executed in this step.
        /// </summary>
        public Step CurrentStep { get; private set; }

        /// <summary>
        /// Calculates ExpectedDuration and LimitDuration for this step, considering all subSteps to be performed during the step.
        /// </summary>
        public override void ProcessDuration()
        {
            foreach (var step in SubSteps)
            {
                if (!step.Processed)
                {
                    step.ProcessDuration();
                }
                Processed = true;

                ExpectedDuration.Increment(step.ExpectedDuration);
                LimitDuration.Increment(step.LimitDuration);                
            }
        }

        /// <summary>
        /// Adds a subStep to this Step.
        /// </summary>
        /// <param name="step">subStep to be executed in this Step.</param>
        public void AddSubStep(Step step)
        {
            SubSteps.Add(step);
        }
        /// <summary>
        /// Removes a subStep from this Step.
        /// </summary>
        /// <param name="step">subStep to be executed in this Step.</param>
        public void RemoveSubStep(Step step)
        {
            SubSteps.Remove(step);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>The Next LeafStep to be executed. Null if all Steps are concluded.</returns>
        public LeafStep AdvanceStep()
        {
            /*
             * Se não ta ativo, ativa. Se o primeiro step não for leaf, ativa, até chegar em um step e retorna-lo.
             * 
             */

            /*
             * Dá um advanceStep no subPasso corrente, se ele terminar, anda com o próximo passo aqui dando advance no proximo subpasso,
             * se não, retorna a interação que ele deu.
             * Isso, se ele não for um Leaf, pois se for, marca ele como completo, setando um tempo (qual? pegar tempos automatico)
             * e incrementar o currentIndex. E ai ativa esse cara com o advanceStep
             */

            if(_currentStepInddex < SubSteps.Count - 1) { 
                _currentStepInddex++;
                CurrentStep = SubSteps[_currentStepInddex];
            }
            else
            {
                CurrentStep = null;
            }
            return null;
        }
        /// <summary>
        /// Mark this Step as completed, if all its subSteps are also completed.
        /// </summary>
       /* public override void MarkAsCompleted()
        {
            throw new NotImplementedException();
        }*/
    }
}
