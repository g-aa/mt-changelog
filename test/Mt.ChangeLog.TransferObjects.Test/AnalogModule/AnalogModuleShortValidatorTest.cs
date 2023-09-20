using FluentValidation.TestHelper;
using Mt.ChangeLog.TransferObjects.AnalogModule;

namespace Mt.ChangeLog.TransferObjects.Test.AnalogModule;

/// <summary>
/// Набор тестов для <see cref="AnalogModuleShortModel"/>.
/// </summary>
[TestFixture]
public sealed class AnalogModuleShortValidatorTest
{
    private AnalogModuleShortValidator validator;

    /// <summary>
    /// Настройка.
    /// </summary>
    [OneTimeSetUp]
    public void Setup()
    {
        this.validator = new AnalogModuleShortValidator();
    }

    /// <summary>
    /// Положительный тест для <see cref="AnalogModuleShortModel.Title"/>.
    /// </summary>
    /// <param name="title">Наименование.</param>
    [TestCase("БМРЗ-000")]
    [TestCase("БМРЗ-999")]
    [TestCase("БМРЗ-100N")]
    [TestCase("БМРЗ-100У")]
    [TestCase("БМРЗ-М4")]
    [TestCase("БМРЗ-М4М")]
    public void TitlePositiveTest(string title)
    {
        // arrange
        var model = new AnalogModuleShortModel
        {
            Title = title,
        };

        // act
        var result = this.validator.TestValidate(model);

        // assert
        result.ShouldNotHaveValidationErrorFor(m => m.Title);
    }

    /// <summary>
    /// Отрицательный тест для <see cref="AnalogModuleShortModel.Title"/>.
    /// </summary>
    /// <param name="title">Наименование.</param>
    [TestCase("")]
    [TestCase(" ")]
    [TestCase("\t")]
    [TestCase("БМРЗ-0")]
    [TestCase("БМРЗ-00000")]
    [TestCase(" БМРЗ-100N ")]
    [TestCase("\tБМРЗ-100N\t")]
    public void TitleNegativeTest(string title)
    {
        // arrange
        var model = new AnalogModuleShortModel
        {
            Title = title,
        };

        // act
        var result = this.validator.TestValidate(model);

        // assert
        result.ShouldHaveValidationErrorFor(m => m.Title);
    }
}