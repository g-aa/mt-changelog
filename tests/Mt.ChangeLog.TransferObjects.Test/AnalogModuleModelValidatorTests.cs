using FluentValidation.TestHelper;
using Mt.ChangeLog.TransferObjects.AnalogModule;
using NUnit.Framework;

namespace Mt.ChangeLog.TransferObjects.Test
{
    public sealed class AnalogModuleModelValidatorTests
    {
        private AnalogModuleModelValidator validator;

        [OneTimeSetUp]
        public void Setup()
        {
            validator = new AnalogModuleModelValidator();
        }

        [Test]
        [TestCase("БМРЗ-000")]
        [TestCase("БМРЗ-999")]
        [TestCase("БМРЗ-100N")]
        [TestCase("БМРЗ-100У")]
        [TestCase("БМРЗ-М4")]
        [TestCase("БМРЗ-М4М")]
        public void TitlePositiveTest(string title)
        {
            var model = new AnalogModuleModel()
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
        [TestCase("БМРЗ-0")]
        [TestCase("БМРЗ-00000")]
        [TestCase(" БМРЗ-100N ")]
        [TestCase("\tБМРЗ-100N\t")]
        public void TitleNegativeTest(string title)
        {
            var model = new AnalogModuleModel()
            {
                Title = title,
            };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(m => m.Title);
        }
    }
}
