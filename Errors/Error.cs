using GuardClause.Errors;

namespace GuardClausule.Errors;

public record Error: IError
{
    public string Description { get; init; }
    public string Code { get; init; }
    public ErrorType ErrorType { get; init; }

    private Error(string description, string code, ErrorType errorType)
    {
        Description = description;
        Code = code;
        ErrorType = errorType;
    }

    public static Error Failure(string description, string code)
    {
        return new Error(description, code, ErrorType.Failure);
    }

    public static ValidationError Validation(string description, string code, string field)
    {
        return ValidationError.Create(description, code, field);
    }
}
