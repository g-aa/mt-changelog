using FluentValidation.TestHelper;
using Mt.ChangeLog.TransferObjects.AnalogModule;
using NUnit.Framework;

namespace Mt.ChangeLog.TransferObjects.Test
{
    /// <summary>
    /// ����� ������ ��� <see cref="AnalogModuleShortModel"/>.
    /// </summary>
    [TestFixture]
    public sealed class AnalogModuleShortModelValidatorTests
    {
        private AnalogModuleShortModelValidator validator;

        /// <summary>
        /// ���������.
        /// </summary>
        [OneTimeSetUp]
        public void Setup()
        {
            validator = new AnalogModuleShortModelValidator();
        }

        /// <summary>
        /// ������������� ����� ��� <see cref="AnalogModuleShortModel.Title"/>.
        /// </summary>
        /// <param name="title">������������.</param>
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

        /// <summary>
        /// ������������� ����� ��� <see cref="AnalogModuleShortModel.Title"/>.
        /// </summary>
        /// <param name="title">������������.</param>
        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("\t")]
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