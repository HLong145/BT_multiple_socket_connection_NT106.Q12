using System.Text.RegularExpressions;

namespace Socket_LTMCB.Services
{
    public class ValidationService
    {
        public bool IsValidUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username)) return false;
            return Regex.IsMatch(username, @"^[a-zA-Z0-9_]{3,20}$");
        }

        public bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        public bool IsValidPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return false;
            return Regex.IsMatch(phone, @"^0\d{9}$"); 
        }
        public bool IsValidPassword(string password)
        {
            if (string.IsNullOrEmpty(password)) return false;
            // Min 8 ký tự, có chữ hoa, chữ thường, số, ký tự đặc biệt
            return Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_\-+=\[\]{};:'"",.<>/?\\|`~]).{8,}$");
        }

        public (bool IsValid, string Message) ValidateRegistration(
            string username,
            string email,
            string phone,
            string password)
        {
            if (!IsValidUsername(username))
                return (false, "Username phải 3-20 ký tự, chỉ chứa chữ, số, gạch dưới");

            if (!string.IsNullOrEmpty(email) && !IsValidEmail(email))
                return (false, "Email không hợp lệ");

            if (!string.IsNullOrEmpty(phone) && !IsValidPhone(phone))
                return (false, "Số điện thoại phải 10 chữ số");

            if (!IsValidPassword(password))
                return (false, "Mật khẩu phải có tối thiểu 8 ký tự, bao gồm chữ hoa, chữ thường, số và ký tự đặc biệt");

            return (true, "Valid");
        }
    }
}