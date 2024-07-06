using GuardClausule.Guard;

string? str = " ";
string? str2 = "anddddddddddd";
string? str3 = "and";
string? str4 = "and11111111111111";
string? str5 = "and11111111111111";
string? str6 = "and11111111111111@";

var result = Guard.For()
    .IsNullOrWhiteSpace(str)
    .MaxLength(str2, 7)
    .MinLength(str3, 5)
    .InRange(str4, 2, 8)
    .AllNumeric(str5)
    .IsAlphanumeric(str6);

var errors = result.Errors.Select(x => x.Description);
var codes = result.Errors.Select(x => x.Code);

Console.WriteLine(string.Join(", ", errors));
Console.WriteLine(string.Join(", ", codes));
Console.WriteLine(result.ToValidationDictionary());