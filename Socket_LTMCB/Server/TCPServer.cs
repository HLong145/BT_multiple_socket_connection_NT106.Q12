using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Socket_LTMCB.Services;

namespace Socket_LTMCB.Server
{
    public class TcpServer
    {
        private TcpListener listener;
        private bool isRunning;
        private List<ClientHandler> connectedClients = new List<ClientHandler>();
        private DatabaseService dbService;
        private TokenManager tokenManager;
        private ValidationService validationService;  
        private SecurityService securityService;      

        public event Action<string> OnLog;
        public bool IsRunning => isRunning;

        public TcpServer()
        {
            dbService = new DatabaseService();
            tokenManager = new TokenManager();
            validationService = new ValidationService();  
            securityService = new SecurityService();     
        }

        public void Start(int port)
        {
            try
            {
                listener = new TcpListener(IPAddress.Any, port);
                listener.Start();
                isRunning = true;

                Log($"✅ Server started on port {port}");

                Task.Run(() => AcceptClients());
            }
            catch (Exception ex)
            {
                Log($"❌ Error starting server: {ex.Message}");
                throw;
            }
        }

        public void Stop()
        {
            try
            {
                isRunning = false;

                foreach (var client in connectedClients.ToArray())
                {
                    client.Close();
                }
                connectedClients.Clear();

                listener?.Stop();
                Log("🛑 Server stopped");
            }
            catch (Exception ex)
            {
                Log($"❌ Error stopping server: {ex.Message}");
            }
        }

        private async Task AcceptClients()
        {
            while (isRunning)
            {
                try
                {
                    TcpClient client = await listener.AcceptTcpClientAsync();
                    var clientHandler = new ClientHandler(client, this, dbService, tokenManager);

                    lock (connectedClients)
                    {
                        connectedClients.Add(clientHandler);
                    }

                    Log($"📱 New client connected. Total: {connectedClients.Count}");

                    Task.Run(() => clientHandler.Handle());
                }
                catch (Exception ex)
                {
                    if (isRunning)
                    {
                        Log($"❌ Error accepting client: {ex.Message}");
                    }
                }
            }
        }

        public void RemoveClient(ClientHandler client)
        {
            lock (connectedClients)
            {
                connectedClients.Remove(client);
            }
            Log($"📴 Client disconnected. Remaining: {connectedClients.Count}");
        }

        public void Log(string message)
        {
            string logMessage = $"[{DateTime.Now:HH:mm:ss}] {message}";
            OnLog?.Invoke(logMessage);
        }
    }

    public class ClientHandler
    {
        private TcpClient tcpClient;
        private NetworkStream stream;
        private TcpServer server;
        private DatabaseService dbService;
        private TokenManager tokenManager;
        private string currentToken;
        private ValidationService validationService; 
        private SecurityService securityService;   

        public ClientHandler(TcpClient client, TcpServer server, DatabaseService dbService, TokenManager tokenManager)
        {
            tcpClient = client;
            this.server = server;
            this.dbService = dbService;
            this.tokenManager = tokenManager;
            stream = client.GetStream();
            validationService = validationService;  
            securityService = securityService;  
        }

        public async Task Handle()
        {
            try
            {
                byte[] buffer = new byte[8192];

                while (tcpClient.Connected)
                {
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);

                    if (bytesRead == 0) break;

                    string requestJson = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    server.Log($"📨 Received: {requestJson.Substring(0, Math.Min(100, requestJson.Length))}...");

                    var response = ProcessRequest(requestJson);

                    byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                    await stream.WriteAsync(responseBytes, 0, responseBytes.Length);

                    server.Log($"📤 Sent response");
                }
            }
            catch (Exception ex)
            {
                server.Log($"❌ Client handler error: {ex.Message}");
            }
            finally
            {
                Close();
            }
        }

        private string ProcessRequest(string requestJson)
        {
            try
            {
                var request = JsonSerializer.Deserialize<Request>(requestJson);

                if (request == null)
                {
                    return CreateResponse(false, "Invalid request format");
                }

                switch (request.Action?.ToUpper())
                {
                    case "REGISTER":
                        return HandleRegister(request);

                    case "LOGIN":
                        return HandleLogin(request);

                    case "VERIFY_TOKEN":
                        return HandleVerifyToken(request);

                    case "GENERATE_OTP":
                        return HandleGenerateOTP(request);

                    case "VERIFY_OTP":
                        return HandleVerifyOTP(request);

                    case "RESET_PASSWORD":
                        return HandleResetPassword(request);

                    case "GET_USER_BY_CONTACT":
                        return HandleGetUserByContact(request);


                    default:
                        return CreateResponse(false, "Unknown action");
                }
            }
            catch (Exception ex)
            {
                server.Log($"❌ Process error: {ex.Message}");
                return CreateResponse(false, $"Server error: {ex.Message}");
            }
        }

        private string HandleRegister(Request request)
        {
            try
            {
                var username = request.Data?["username"]?.ToString();
                var email = request.Data.ContainsKey("email") ? request.Data["email"]?.ToString() : null;
                var phone = request.Data.ContainsKey("phone") ? request.Data["phone"]?.ToString() : null;
                var password = request.Data?["password"]?.ToString();

                // ✅ VALIDATE INPUT
                var validationResult = validationService.ValidateRegistration(username, email, phone, password);
                if (!validationResult.IsValid)
                {
                    return CreateResponse(false, validationResult.Message);
                }

                if (dbService.IsUserExists(username, email, phone))
                {
                    return CreateResponse(false, "Username, email or phone already exists");
                }

                string salt = dbService.CreateSalt();
                string hash = dbService.HashPassword_Sha256(password, salt);

                bool success = dbService.SaveUserToDatabase(username, email, phone, hash, salt);

                return CreateResponse(success, success ? "Registration successful" : "Registration failed");
            }
            catch (Exception ex)
            {
                return CreateResponse(false, $"Registration error: {ex.Message}");
            }
        }

        private string HandleLogin(Request request)
        {
            try
            {
                var username = request.Data?["username"]?.ToString();
                var password = request.Data?["password"]?.ToString();

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    return CreateResponse(false, "Username and password are required");
                }

                // ✅ CHECK BRUTE-FORCE
                if (!securityService.CheckLoginAttempts(username))
                {
                    int remainingMinutes = securityService.GetLockoutMinutes(username);
                    return CreateResponse(false, $"Account locked. Try again in {remainingMinutes} minutes.");
                }

                bool loginSuccess = dbService.VerifyUserLogin(username, password);

                // ✅ RECORD ATTEMPT
                securityService.RecordLoginAttempt(username, loginSuccess);

                if (loginSuccess)
                {
                    string token = tokenManager.GenerateToken(username);
                    currentToken = token;

                    return CreateResponse(true, "Login successful", new Dictionary<string, object>
            {
                { "token", token },
                { "username", username }
            });
                }
                else
                {
                    int remaining = securityService.GetRemainingAttempts(username);
                    return CreateResponse(false, $"Invalid credentials. {remaining} attempts remaining.");
                }
            }
            catch (Exception ex)
            {
                return CreateResponse(false, $"Login error: {ex.Message}");
            }
        }

        private string HandleVerifyToken(Request request)
        {
            try
            {
                var token = request.Data?["token"]?.ToString();

                if (string.IsNullOrEmpty(token))
                {
                    return CreateResponse(false, "Token is required");
                }

                bool isValid = tokenManager.ValidateToken(token);

                if (isValid)
                {
                    var username = tokenManager.GetUsernameFromToken(token);
                    return CreateResponse(true, "Token valid", new Dictionary<string, object>
                    {
                        { "username", username }
                    });
                }
                else
                {
                    return CreateResponse(false, "Invalid or expired token");
                }
            }
            catch (Exception ex)
            {
                return CreateResponse(false, $"Token verification error: {ex.Message}");
            }
        }

        private string HandleGenerateOTP(Request request)
        {
            try
            {
                var username = request.Data?["username"]?.ToString();

                if (string.IsNullOrEmpty(username))
                {
                    return CreateResponse(false, "Username is required");
                }

                string otp = dbService.GenerateOtp(username);

                return CreateResponse(true, "OTP generated", new Dictionary<string, object>
                {
                    { "otp", otp }
                });
            }
            catch (Exception ex)
            {
                return CreateResponse(false, $"OTP generation error: {ex.Message}");
            }
        }

        private string HandleVerifyOTP(Request request)
        {
            try
            {
                var username = request.Data?["username"]?.ToString();
                var otp = request.Data?["otp"]?.ToString();

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(otp))
                {
                    return CreateResponse(false, "Username and OTP are required");
                }

                var result = dbService.VerifyOtp(username, otp);

                return CreateResponse(result.IsValid, result.Message);
            }
            catch (Exception ex)
            {
                return CreateResponse(false, $"OTP verification error: {ex.Message}");
            }
        }

        private string HandleResetPassword(Request request)
        {
            try
            {
                var username = request.Data?["username"]?.ToString();
                var newPassword = request.Data?["newPassword"]?.ToString();

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(newPassword))
                {
                    return CreateResponse(false, "Username and new password are required");
                }

                bool success = dbService.ResetPassword(username, newPassword);

                return CreateResponse(success, success ? "Password reset successful" : "Password reset failed");
            }
            catch (Exception ex)
            {
                return CreateResponse(false, $"Password reset error: {ex.Message}");
            }
        }

        private string HandleGetUserByContact(Request request)
        {
            try
            {
                var contact = request.Data?["contact"]?.ToString();
                var isEmail = request.Data.ContainsKey("isEmail") &&
                             bool.Parse(request.Data["isEmail"].ToString());

                if (string.IsNullOrEmpty(contact))
                {
                    return CreateResponse(false, "Contact is required");
                }

                string username = dbService.GetUsernameByContact(contact, isEmail);

                if (!string.IsNullOrEmpty(username))
                {
                    return CreateResponse(true, "User found", new Dictionary<string, object>
                    {
                        { "username", username }
                    });
                }
                else
                {
                    return CreateResponse(false, "User not found");
                }
            }
            catch (Exception ex)
            {
                return CreateResponse(false, $"Get user error: {ex.Message}");
            }
        }

        private string CreateResponse(bool success, string message, Dictionary<string, object> data = null)
        {
            var response = new Response
            {
                Success = success,
                Message = message,
                Data = data ?? new Dictionary<string, object>()
            };

            return JsonSerializer.Serialize(response);
        }

        public void Close()
        {
            try
            {
                if (!string.IsNullOrEmpty(currentToken))
                {
                    tokenManager.RevokeToken(currentToken);
                }

                stream?.Close();
                tcpClient?.Close();
                server.RemoveClient(this);
            }
            catch { }
        }
    }

    public class Request
    {
        public string Action { get; set; }
        public Dictionary<string, object> Data { get; set; }
    }

    public class Response
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Dictionary<string, object> Data { get; set; }
    }
}