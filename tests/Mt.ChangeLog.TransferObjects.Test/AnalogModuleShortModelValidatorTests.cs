using FluentValidation.TestHelper;
using Mt.ChangeLog.TransferObjects.AnalogModule;
using NUnit.Framework;

namespace Mt.ChangeLog.TransferObjects.Test
{
    public sealed class AnalogModuleShortModelValidatorTests
    {
        private AnalogModuleShortModelValidator validator;

        [OneTimeSetUp]
        public void Setup()
        {
            validator = new AnalogModuleShortModelValidator();
        }

        [Test]
        [TestCase("ÁÌÐÇ-000")]
        [TestCase("ÁÌÐÇ-999")]
        [TestCase("ÁÌÐÇ-100N")]
        [TestCase("ÁÌÐÇ-100Ó")]
        [TestCase("ÁÌÐÇ-Ì4")]
        [TestCase("ÁÌÐÇ-Ì4Ì")]
        public void TitlePositiveTest(string title)
        {
            var model = new AnalogModuleShortModel()
            {
                Title = title,
            };
            var result = validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(m => m.Title);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("ÁÌÐÇ-0")]
        [TestCase("ÁÌÐÇ-00000")]
        [TestCase(" ÁÌÐÇ-100N ")]
        [TestCase("\tÁÌÐÇ-100N\t")]
        public void TitleNegativeTest(string title)
        {
            var model = new AnalogModuleShortModel()
            {
                Title = title,
            };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(m => m.Title);
        }
    }
}