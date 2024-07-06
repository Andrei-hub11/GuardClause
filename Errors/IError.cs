using GuardClausule.Errors;

namespace GuardClause.Errors;

public interface IError
{
    string Description { get; }
    string Code { get; }
    ErrorType ErrorType { get; }
}
