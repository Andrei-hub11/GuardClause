using GuardClause.Errors;
using GuardClausule.Errors;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace GuardClausule.Guard;

public partial class Guard
{
    private List<IError> ErrorList { get; set; } = [];
    public IReadOnlyList<IError> Errors => new ReadOnlyCollection<IError>(ErrorList);

    private Guard()
    {

    }

    public static Guard For()
    {
        return new Guard();
    }

    //public Guard IsNull<T>(T? value, string fieldName)
    //{
    //    if (value is null)
    //    {
    //        _Errors.Add(Error.Failure($"O {fieldName} não pode ser null", "ERR_CAN_NOT_BE_NULL"));
    //    }

    //    return this;
    //}

    public Guard IsNullOrWhiteSpace(string value, 
        [CallerArgumentExpression("value")] string valueExpression = "Não fornecido")
    {

        if (string.IsNullOrWhiteSpace(value))
        {
            ErrorList.Add(Error.Failure($"{valueExpression} não pode ser null nem vazio", "ERR_IS_NULL_OR_EMPTY"));
        }

        return this;
    }

    public Guard MaxLength(string value, int max,
      [CallerArgumentExpression("value")] string valueExpression = "Não fornecido")
    {

        if (value.Length > max)
        {
            ErrorList.Add(Error.Failure($"{valueExpression} deve possuir no máximo {max} caracteres", 
                "ERR_TOO_LOOG"));
        }

        return this;
    }

    public Guard MinLength(string value, int min,
        [CallerArgumentExpression("value")] string valueExpression = "Não fornecido") 
    {

        if (value.Length < min)
        {
            ErrorList.Add(Error.Failure($"{valueExpression} deve possuir no mínimo {min} caracteres", 
                "ERR_TOO_SHORT"));
        }

        return this;
    }

    public Guard InRange(string value, int min, int max,
        [CallerArgumentExpression("value")] string valueExpression = "Não fornecido") 
    {

        if (value.Length < min || value.Length > max)
        {
            ErrorList.Add(Error.Failure($"{valueExpression} deve possuir no mínimo {min} caracteres e no máximo {max}",
                "ERR_LENGTH_OUT_OF_RANGE"));
        }

        if (value.Length < min || value.Length > max)
        {
            ErrorList.Add(Error.Validation($"{valueExpression} deve possuir no mínimo {min} caracteres e no máximo {max}", 
                "ERR_LENGTH_OUT_OF_RANGE", valueExpression));
        }

        return this;
    }

    public Guard AllNumeric(string value, [CallerArgumentExpression("value")] string valueExpression = "Não fornecido")
    {
        Regex regex = new Regex("^[0-9]*$");

        if (!regex.IsMatch(value))
        {
            ErrorList.Add(Error.Failure($"{valueExpression} deve conter apena números",
                "ERR_NOT_NUMERIC"));
        }

        return this;
    }

    public Guard IsAlphanumeric(string value, [CallerArgumentExpression("value")] string valueExpression = "Não fornecido")
    {
        Regex regex = new Regex("^[a-zA-Z0-9]*$");

        if (!regex.IsMatch(value))
        {
            ErrorList.Add(Error.Failure($"{valueExpression} deve conter apenas letras e números",
                "ERR_NOT_ALPHANUMERIC"));
        }

        return this;
    }

    public Dictionary<string, string[]> ToValidationDictionary()
    {
        return Errors.OfType<ValidationError>()
                     .GroupBy(error => error.Field)
                     .ToDictionary(
                         group => group.Key,
                         group => group.Select(error => error.Description).ToArray());
    }
}
