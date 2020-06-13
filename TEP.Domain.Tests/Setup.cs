using Moq;
using System.Collections.Generic;
using TEP.Domain.Entities;
using TEP.Domain.Entities.Assets;
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
            var keyAssetMock = new Mock<IAsset>();
            keyAssetMock.Setup(m => m.Name).Returns("key");
            keyAssetMock.Setup(m => m.Path).Returns("key.fbx");

            var hugAssetMock = new Mock<IAsset>();
            hugAssetMock.Setup(m => m.Name).Returns("hug");
            hugAssetMock.Setup(m => m.Path).Returns("hug.fbx");

            var doorssetMock = new Mock<IAsset>();
            doorssetMock.Setup(m => m.Name).Returns("door");
            doorssetMock.Setup(m => m.Path).Returns("door.fbx");

            List<Category> takeKeyCategories = new List<Category>();
            takeKeyCategories.Add(Category.Operational);
            Description takeKeyDescription = new Description("Take the Key.");
            Duration takeKeyExpected = new Duration(1000);
            Duration takeKeyLimit = new Duration(2000);
            _keyInteraction = new Interaction(takeKeyCategories, Act.Grab, takeKeyDescription, takeKeyExpected, takeKeyLimit, keyAssetMock.Object);
            _takeKeyStep = new LeafStep(Standard.Mandatory, "Taking Key", _keyInteraction);

            List<Category> insertKeyCategories = new List<Category>();
            insertKeyCategories.Add(Category.Operational);
            Description insertKeyDescription = new Description("Insert key into door.");
            Duration insertKeyExpected = new Duration(1000);
            Duration insertKeyLimit = new Duration(2000);
            _keyholeInteraction = new Interaction(insertKeyCategories, Act.Grab, insertKeyDescription, insertKeyExpected, insertKeyLimit, (SimpleAsset)keyAssetMock.Object, (SimpleAsset)doorssetMock.Object);
            _insertKeyStep = new LeafStep(Standard.Mandatory, "Insert key.", _keyholeInteraction);

            List<Category> openDoorCategories = new List<Category>();
            openDoorCategories.Add(Category.Operational);
            Description openDoorDescription = new Description("Open door.");
            Duration openDoorExpected = new Duration(1000);
            Duration openDoorLimit = new Duration(2000);
            _doorInteraction = new Interaction(openDoorCategories, Act.Grab, openDoorDescription, openDoorExpected, openDoorLimit, doorssetMock.Object);
            _openDoorStep = new LeafStep(Standard.Mandatory, "Insert key.", _doorInteraction);

            List<Category> grabHugCategories = new List<Category>();
            grabHugCategories.Add(Category.Operational);
            grabHugCategories.Add(Category.Security);
            Description grabHugDescription = new Description("Grab HUg.");
            Duration grabHugExpected = new Duration(1000);
            Duration grabHugLimit = new Duration(2000);
            _grabHugInteraction = new Interaction(grabHugCategories, Act.Grab, grabHugDescription, grabHugExpected, grabHugLimit, hugAssetMock.Object);
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
