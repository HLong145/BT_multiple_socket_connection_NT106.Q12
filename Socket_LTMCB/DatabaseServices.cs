using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ServerApp.Services
{
    public class DatabaseService
    {
        private Dictionary<string, (string Hash, string Salt, string Email, string Phone)> users = new();
        private Dictionary<string, string> otps = new();

        public bool IsUserExists(string username, string email, string phone)
        {
            foreach (var u in users)
            {
                if (u.Key == username || u.Value.Email == email || u.Value.Phone == phone)
                    return true;
            }
            return false;
        }

        public string CreateSalt()
        {
            var bytes = new byte[16];
            RandomNumberGenerator.Fill(bytes);
            return Convert.ToBase64String(bytes);
        }

        public string HashPassword_Sha256(string password, string salt)
        {
            using var sha256 = SHA256.Create();
            var combined = Encoding.UTF8.GetBytes(password + salt);
            var hash = sha256.ComputeHash(combined);
            return Convert.ToBase64String(hash);
        }

        public bool SaveUserToDatabase(string username, string email, string phone, string hash, string salt)
        {
            if (users.ContainsKey(username)) return false;
            users[username] = (hash, salt, email, phone);
            return true;
        }

        public bool VerifyUserLogin(string username, string password)
        {
            if (!users.ContainsKey(username)) return false;
            var (hash, salt, _, _) = users[username];
            var verify = HashPassword_Sha256(password, salt);
            return verify == hash;
        }

        public string GenerateOtp(string username)
        {
            if (!users.ContainsKey(username)) return null;
            var otp = new Random().Next(100000, 999999).ToString();
            otps[username] = otp;
            return otp;
        }

        public (bool IsValid, string Message) VerifyOtp(string username, string otp)
        {
            if (!otps.ContainsKey(username))
                return (false, "Không tìm thấy OTP");
            if (otps[username] != otp)
                return (false, "OTP không đúng");
            otps.Remove(username);
            return (true, "Xác thực OTP thành công");
        }

        public bool ResetPassword(string username, string newPassword)
        {
            if (!users.ContainsKey(username)) return false;
            var salt = CreateSalt();
            var hash = HashPassword_Sha256(newPassword, salt);
            var (_, _, email, phone) = users[username];
            users[username] = (hash, salt, email, phone);
            return true;
        }

        public string GetUsernameByContact(string contact, bool isEmail)
        {
            foreach (var u in users)
            {
                if (isEmail && u.Value.Email == contact)
                    return u.Key;
                if (!isEmail && u.Value.Phone == contact)
                    return u.Key;
            }
            return null;
        }

    }
}
