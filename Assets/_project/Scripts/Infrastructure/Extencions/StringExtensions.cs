using System.Text;

namespace CodeBase.Extencions
{
    public static class StringExtensions
    {
        public static string ToKebabCase(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            var output = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsUpper(input[i]) && i > 0)
                    output.Append('-');

                output.Append(char.ToLower(input[i]));
            }

            return output.ToString();
        }
    }
}