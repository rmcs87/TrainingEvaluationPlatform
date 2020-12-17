using Moq;
using System.Collections.Generic;
using TEP.Domain.Entities;
using TEP.Domain.ValueObjects;
using TEP.Shared.ValueObjects;

namespace TEP.Domain.Tests
{
    public class Setup
    {
        protected readonly RecursiveStep _MultinivelStep;

        protected readonly LeafStep _takeKeyStep;
        protected readonly LeafStep _insertKeyStep;
        protected readonly LeafStep _openDoorStep;
        protected readonly LeafStep _grabHugStep;

        protected readonly Interaction _keyInteraction;
        protected readonly Interaction _keyholeInteraction;
        protected readonly Interaction _doorInteraction;
        protected readonly Interaction _grabHugInteraction;
        public Setup()
        {
            var keyAssetMock = new Asset("key", "key.jpg", "key.fbx");

            var hugAssetMock = new Asset("hug", "hug.jpg", "hug.fbx");

            var doorssetMock = new Asset("door", "door.jpg", "door.fbx");

            Description takeKeyDescription = new Description("Take the Key.");
            Duration takeKeyExpected = new Duration(1000);
            Duration takeKeyLimit = new Duration(2000);
            _keyInteraction = new Interaction(Act.Grab, takeKeyDescription, takeKeyExpected, takeKeyLimit, keyAssetMock);
            _takeKeyStep = new LeafStep(Standard.Mandatory, "Taking Key", _keyInteraction);

            Description insertKeyDescription = new Description("Insert key into door.");
            Duration insertKeyExpected = new Duration(1000);
            Duration insertKeyLimit = new Duration(2000);
            _keyholeInteraction = new Interaction(Act.Grab, insertKeyDescription, insertKeyExpected, insertKeyLimit, keyAssetMock, doorssetMock);
            _insertKeyStep = new LeafStep(Standard.Mandatory, "Insert key.", _keyholeInteraction);

            Description openDoorDescription = new Description("Open door.");
            Duration openDoorExpected = new Duration(1000);
            Duration openDoorLimit = new Duration(2000);
            _doorInteraction = new Interaction(Act.Grab, openDoorDescription, openDoorExpected, openDoorLimit, doorssetMock);
            _openDoorStep = new LeafStep(Standard.Mandatory, "Insert key.", _doorInteraction);

            Description grabHugDescription = new Description("Grab HUg.");
            Duration grabHugExpected = new Duration(1000);
            Duration grabHugLimit = new Duration(2000);
            _grabHugInteraction = new Interaction(Act.Grab, grabHugDescription, grabHugExpected, grabHugLimit, hugAssetMock);
            _grabHugStep = new LeafStep(Standard.Mandatory, "Insert key.", _grabHugInteraction);


            List<Step> hugSteps = new List<Step>();
            RecursiveStep hugSequentialStep = new RecursiveStep(Standard.Mandatory, "Hug.", hugSteps);
            hugSequentialStep.AddSubStep(_grabHugStep);
            hugSequentialStep.AddSubStep(_takeKeyStep);
            List<Step> doorSteps = new List<Step>();
            RecursiveStep doorSequentialStep = new RecursiveStep(Standard.Mandatory, "Door", doorSteps);
            doorSequentialStep.AddSubStep(_insertKeyStep);
            doorSequentialStep.AddSubStep(_openDoorStep);
            List<Step> multiNivelSequentialSteps = new List<Step>();
            _MultinivelStep = new RecursiveStep(Standard.Mandatory, "Unlock Door", multiNivelSequentialSteps);
            _MultinivelStep.AddSubStep(hugSequentialStep);
            _MultinivelStep.AddSubStep(doorSequentialStep);
        }

    }
}
