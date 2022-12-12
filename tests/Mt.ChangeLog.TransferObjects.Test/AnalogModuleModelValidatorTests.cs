using FluentValidation.TestHelper;
using Mt.ChangeLog.TransferObjects.AnalogModule;
using Mt.ChangeLog.TransferObjects.Platform;
using NUnit.Framework;
using System.Collections.Generic;

namespace Mt.ChangeLog.TransferObjects.Test
{
    /// <summary>
    /// Набор тестов для <see cref="AnalogModuleModel"/>.
    /// </summary>
    [TestFixture]
    public sealed class AnalogModuleModelValidatorTests
    {
        private AnalogModuleModelValidator validator;

        /// <summary>
        /// Настройка.
        /// </summary>
        [OneTimeSetUp]
        public void Setup()
        {
            validator = new AnalogModuleModelValidator();
        }

        /// <summary>
        /// Положительные тесты для <see cref="AnalogModuleTableModel.DIVG"/>.
        /// </summary>
        /// <param name="divg">ДИВГ.</param>
        [Test]
        [TestCase("ДИВГ.00000-00")]
        [TestCase("ДИВГ.12345-67")]
        [TestCase("ДИВГ.99999-99")]
        public void DIVGPositiveTest(string divg)
        {
            var model = new AnalogModuleModel()
            {
                DIVG = divg,
            };
            var result = validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(m => m.DIVG);
        }

        /// <summary>
        /// Отрицательные тесты для <see cref="AnalogModuleTableModel.DIVG"/>.
        /// </summary>
        /// <param name="divg">ДИВГ.</param>
        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("\t")]
        [TestCase("дивг.00000-00")]
        [TestCase("ДИВГ.0000-000")]
        [TestCase("ДИВГ.00000-000")]
        [TestCase(" ДИВГ.00000-00 ")]
        [TestCase("\tДИВГ.00000-00\t")]
        public void DIVGNegativeTest(string divg)
        {
            var model = new AnalogModuleModel()
            {
                DIVG = divg,
            };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(m => m.DIVG);
        }

        /// <summary>
        /// Положительные тесты для <see cref="AnalogModuleTableModel.Current"/>.
        /// </summary>
        /// <param name="current">Номинальный ток.</param>
        [Test]
        [TestCase("0A")]
        [TestCase("1A")]
        [TestCase("9A")]
        public void CurrentPositiveTest(string current)
        {
            var model = new AnalogModuleModel()
            {
                Current = current,
            };
            var result = validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(m => m.Current);
        }

        /// <summary>
        /// Отрицательные тесты для <see cref="AnalogModuleTableModel.Current"/>.
        /// </summary>
        /// <param name="current">Номинальный ток.</param>
        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("\t")]
        [TestCase("10A")]
        public void CurrentNegativeTest(string current)
        {
            var model = new AnalogModuleModel()
            {
                Current = current,
            };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(m => m.Current);
        }

        /// <summary>
        /// Положительные тесты для <see cref="AnalogModuleTableModel.Description"/>.
        /// </summary>
        /// <param name="desc">Описание.</param>
        /// <param name="count">Количестно символов.</param>
        [Test]
        [TestCase("", 0)]
        [TestCase(" ", 1)]
        [TestCase("\t", 1)]
        [TestCase("A", 1)]
        [TestCase("A", 500)]
        public void DescriptionPositiveTest(string desc, int count)
        {
            var model = new AnalogModuleModel()
            {
                Description = desc.PadRight(count, 'B'),
            };
            var result = validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(m => m.Description);
        }

        /// <summary>
        /// Отрицательные тесты для <see cref="AnalogModuleTableModel.Description"/>.
        /// </summary>
        /// <param name="desc">Описание.</param>
        /// <param name="count">Количестно символов.</param>
        [Test]
        [TestCase(null, 0)]
        [TestCase("A", 501)]
        public void DescriptionNegativeTest(string desc, int count)
        {
            var model = new AnalogModuleModel()
            {
                Description = desc?.PadRight(count, 'B'),
            };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(m => m.Description);
        }

        /// <summary>
        /// Отрицательные тесты для <see cref="AnalogModuleModel.Platforms"/>.
        /// </summary>
        /// <param name="platforms">Перечень платформ.</param>
        [Test]
        [TestCase(null)]
        public void DescriptionNegativeTest(IEnumerable<PlatformShortModel> platforms)
        {
            var model = new AnalogModuleModel()
            {
                Platforms = platforms,
            };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(m => m.Platforms);
        }
    }
}