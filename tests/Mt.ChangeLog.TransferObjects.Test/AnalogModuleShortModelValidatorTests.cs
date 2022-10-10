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
        [TestCase("����-000")]
        [TestCase("����-999")]
        [TestCase("����-100N")]
        [TestCase("����-100�")]
        [TestCase("����-�4")]
        [TestCase("����-�4�")]
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
        [TestCase("����-0")]
        [TestCase("����-00000")]
        [TestCase(" ����-100N ")]
        [TestCase("\t����-100N\t")]
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