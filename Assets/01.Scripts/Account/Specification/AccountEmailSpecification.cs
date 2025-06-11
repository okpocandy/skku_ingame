using System.Text.RegularExpressions;

public class AccountEmailSpecification : ISpecification<string>
{
    private static readonly Regex EmailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);
    public string ErrorMessage { get; private set; }

    public bool IsSatisfiedBy(string value)
    {
        // 이메일 검증
        if (string.IsNullOrEmpty(value))
        {
            ErrorMessage = "이메일을 입력해주세요.";
            return false;
        }

        if (!EmailRegex.IsMatch(value))
        {
            ErrorMessage = "올바른 이메일 형식이 아닙니다.";
            return false;
        }

        return true;
    }
}
