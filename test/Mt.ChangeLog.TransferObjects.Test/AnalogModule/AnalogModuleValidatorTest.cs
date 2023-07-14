using FluentValidation.TestHelper;
using Mt.ChangeLog.TransferObjects.AnalogModule;
using Mt.ChangeLog.TransferObjects.Platform;

namespace Mt.ChangeLog.TransferObjects.Test.AnalogModule;

/// <summary>
/// Набор тестов для <see cref="AnalogModuleModel"/>.
/// </summary>
[TestFixture]
public sealed class AnalogModuleValidatorTest
{
    private AnalogModuleValidator validator;

    /// <summary>
    /// Настройка.
    /// </summary>
    [OneTimeSetUp]
    public void Setup()
    {
        this.validator = new AnalogModuleValidator(new PlatformShortValidator());
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
        var model = new AnalogModuleModel
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
    [TestCase(null)]
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
        var model = new AnalogModuleModel
        {
            Title = title,
        };

        // act
        var result = this.validator.TestValidate(model);

        // assert
        result.ShouldHaveValidationErrorFor(m => m.Title);
    }


    /// <summary>
    /// Положительный тест для <see cref="AnalogModuleTableModel.DIVG"/>.
    /// </summary>
    /// <param name="divg">ДИВГ.</param>
    [TestCase("ДИВГ.00000-00")]
    [TestCase("ДИВГ.12345-67")]
    [TestCase("ДИВГ.99999-99")]
    public void DIVGPositiveTest(string divg)
    {
        // arrange
        var model = new AnalogModuleModel
        {
            DIVG = divg,
        };

        // act
        var result = this.validator.TestValidate(model);

        // assert
        result.ShouldNotHaveValidationErrorFor(m => m.DIVG);
    }

    /// <summary>
    /// Отрицательный тест для <see cref="AnalogModuleTableModel.DIVG"/>.
    /// </summary>
    /// <param name="divg">ДИВГ.</param>
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
        // arrange
        var model = new AnalogModuleModel
        {
            DIVG = divg,
        };

        // act
        var result = this.validator.TestValidate(model);

        // assert
        result.ShouldHaveValidationErrorFor(m => m.DIVG);
    }

    /// <summary>
    /// Положительный тест для <see cref="AnalogModuleTableModel.Current"/>.
    /// </summary>
    /// <param name="current">Номинальный ток.</param>
    [TestCase("0A")]
    [TestCase("1A")]
    [TestCase("9A")]
    public void CurrentPositiveTest(string current)
    {
        // arrange
        var model = new AnalogModuleModel
        {
            Current = current,
        };

        // act
        var result = this.validator.TestValidate(model);

        // assert
        result.ShouldNotHaveValidationErrorFor(m => m.Current);
    }

    /// <summary>
    /// Отрицательный тест для <see cref="AnalogModuleTableModel.Current"/>.
    /// </summary>
    /// <param name="current">Номинальный ток.</param>
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    [TestCase("\t")]
    [TestCase("10A")]
    public void CurrentNegativeTest(string current)
    {
        // arrange
        var model = new AnalogModuleModel
        {
            Current = current,
        };

        // act
        var result = this.validator.TestValidate(model);

        // assert
        result.ShouldHaveValidationErrorFor(m => m.Current);
    }

    /// <summary>
    /// Положительный тест для <see cref="AnalogModuleTableModel.Description"/>.
    /// </summary>
    /// <param name="desc">Описание.</param>
    /// <param name="count">Количестно символов.</param>
    [TestCase("", 0)]
    [TestCase("A", 1)]
    [TestCase("A", 500)]
    public void DescriptionPositiveTest(string desc, int count)
    {
        // arrange
        var model = new AnalogModuleModel
        {
            Description = desc.PadRight(count, 'B'),
        };

        // act
        var result = this.validator.TestValidate(model);

        // assert
        result.ShouldNotHaveValidationErrorFor(m => m.Description);
    }

    /// <summary>
    /// Отрицательный тест для <see cref="AnalogModuleTableModel.Description"/>.
    /// </summary>
    /// <param name="desc">Описание.</param>
    /// <param name="count">Количестно символов.</param>
    [TestCase(null, 0)]
    [TestCase(" ", 1)]
    [TestCase("\t", 1)]
    [TestCase("A", 501)]
    public void DescriptionNegativeTest(string desc, int count)
    {
        // arrange
#pragma warning disable CS8601 // Possible null reference assignment.
        var model = new AnalogModuleModel
        {
            Description = desc?.PadRight(count, 'B'),
        };
#pragma warning restore CS8601 // Possible null reference assignment.

        // act
        var result = this.validator.TestValidate(model);

        // assert
        result.ShouldHaveValidationErrorFor(m => m.Description);
    }

    /// <summary>
    /// Отрицательный тест для <see cref="AnalogModuleModel.Platforms"/>.
    /// </summary>
    /// <param name="platforms">Перечень платформ.</param>
    [TestCase(null)]
    public void DescriptionNegativeTest(IEnumerable<PlatformShortModel> platforms)
    {
        // arrange
        var model = new AnalogModuleModel
        {
            Platforms = platforms,
        };

        // act
        var result = this.validator.TestValidate(model);

        // assert
        result.ShouldHaveValidationErrorFor(m => m.Platforms);
    }
}