using System;
using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using Microsoft.Data.SqlClient;
namespace Socket_LTMCB.Services
{
    public class DatabaseService
    {
        // ✅ CONNECTION STRING - Sửa ở đây nếu cần
        private readonly string connectionString = "Server=localhost;Database=USERDB;Trusted_Connection=True;TrustServerCertificate=True;";
        private ConcurrentDictionary<string, (string Otp, DateTime ExpireTime)> otps = new();
        // ✅ KIỂM TRA USER TỒN TẠI
        public bool IsUserExists(string username, string email, string phone)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"SELECT COUNT(*) FROM NGUOIDUNG 
                            WHERE USERNAME = @Username 
                            OR (@Email IS NOT NULL AND EMAIL = @Email) 
                            OR (@Phone IS NOT NULL AND PHONE = @Phone)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Email", (object)email ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Phone", (object)phone ?? DBNull.Value);

                    return (int)command.ExecuteScalar() > 0;
                }
            }
        }

        // ✅ LƯU USER VÀO DATABASE
        public bool SaveUserToDatabase(string username, string email, string phone, string hash, string salt)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"INSERT INTO NGUOIDUNG (USERNAME, EMAIL, PHONE, PASSWORDHASH, SALT) 
                                VALUES (@Username, @Email, @Phone, @Hash, @Salt)";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Email", (object)email ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Phone", (object)phone ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Hash", hash);
                        command.Parameters.AddWithValue("@Salt", salt);

                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần
                Console.WriteLine($"Error saving user: {ex.Message}");
                return false;
            }
        }

        // ✅ XÁC THỰC ĐĂNG NHẬP
        // ✅ XÁC THỰC ĐĂNG NHẬP - BẢN CẢI TIẾN
        public bool VerifyUserLogin(string username, string password)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT PASSWORDHASH, SALT FROM NGUOIDUNG WHERE USERNAME = @Username";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // ✅ Kiểm tra NULL trước khi ToString()
                                string storedHash = reader["PASSWORDHASH"]?.ToString();
                                string salt = reader["SALT"]?.ToString();

                                if (string.IsNullOrEmpty(storedHash) || string.IsNullOrEmpty(salt))
                                    return false;

                                string verifyHash = HashPassword_Sha256(password, salt);
                                return verifyHash == storedHash;
                            }
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Login error: {ex.Message}");
                return false;
            }
        }

        // ✅ TÌM USERNAME BẰNG EMAIL/PHONE
        // ✅ TÌM USERNAME BẰNG EMAIL/PHONE - BẢN CẢI TIẾN
        public string GetUsernameByContact(string contact, bool isEmail)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = isEmail
                        ? "SELECT USERNAME FROM NGUOIDUNG WHERE EMAIL = @Contact"
                        : "SELECT USERNAME FROM NGUOIDUNG WHERE PHONE = @Contact";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Contact", contact);
                        var result = command.ExecuteScalar();
                        return result?.ToString(); // ✅ Xử lý NULL an toàn
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Get username error: {ex.Message}");
                return null;
            }
        }

        // ✅ RESET PASSWORD
        public bool ResetPassword(string username, string newPassword)
        {
            try
            {
                string salt = CreateSalt();
                string hash = HashPassword_Sha256(newPassword, salt);

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"UPDATE NGUOIDUNG 
                                SET PASSWORDHASH = @Hash, SALT = @Salt 
                                WHERE USERNAME = @Username";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Hash", hash);
                        command.Parameters.AddWithValue("@Salt", salt);
                        command.Parameters.AddWithValue("@Username", username);

                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        // ✅ GIỮ NGUYÊN CÁC HÀM CŨ

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


        // ✅ TẠO OTP - BẢN CẢI TIẾN
        public string GenerateOtp(string username)
        {
            try
            {
                // Kiểm tra user tồn tại
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM NGUOIDUNG WHERE USERNAME = @Username";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        if ((int)command.ExecuteScalar() == 0)
                            return null;
                    }
                }

                // ✅ Dùng RandomNumberGenerator thay vì Random (bảo mật hơn)
                var bytes = new byte[4];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(bytes);
                }
                int randomNumber = Math.Abs(BitConverter.ToInt32(bytes, 0));
                string otp = (randomNumber % 900000 + 100000).ToString(); // Đảm bảo 6 chữ số

                otps[username] = (otp, DateTime.Now.AddMinutes(5));
                return otp;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Generate OTP error: {ex.Message}");
                return null;
            }
        }

        public (bool IsValid, string Message) VerifyOtp(string username, string otp)
        {
            if (!otps.ContainsKey(username))
                return (false, "OTP not found!");

            var (storedOtp, expireTime) = otps[username];

            if (DateTime.Now > expireTime)
            {
                otps.TryRemove(username, out _);  // ✅ Đúng cách
                return (false, "OTP expired!");
            }

            if (storedOtp != otp)
                return (false, "Wrong OTP, try again!");

            otps.TryRemove(username, out _);  // ✅ Đúng cách
            return (true, "Verify OTP successfully");
        }
        // ✅ THÊM METHOD NÀY ĐỂ TEST KẾT NỐI
        public bool TestConnection()
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    return connection.State == System.Data.ConnectionState.Open;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Connection failed: {ex.Message}");
                return false;
            }
        }
        // ✅ XÓA OTP HẾT HẠN (Gọi định kỳ hoặc khi cần)
        public void CleanExpiredOtps()
        {
            var now = DateTime.Now;
            var expiredKeys = otps
                .Where(kvp => kvp.Value.ExpireTime < now)
                .Select(kvp => kvp.Key)
                .ToList();

            foreach (var key in expiredKeys)
            {
                otps.TryRemove(key, out _);
            }
        }
    }
}
