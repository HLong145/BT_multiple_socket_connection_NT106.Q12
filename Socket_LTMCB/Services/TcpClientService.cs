using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace Socket_LTMCB.Services
{
    /// <summary>
    /// Service để kết nối với TCP Server
    /// </summary>
    public class TcpClientService
    {
        private string serverAddress = "127.0.0.1"; // localhost
        private int serverPort = 8080; // Cổng server (thay đổi nếu cần)

        public TcpClientService(string address = "127.0.0.1", int port = 8080)
        {
            serverAddress = address;
            serverPort = port;
        }

        /// <summary>
        /// Gửi request LOGIN đến server
        /// </summary>
        public ServerResponse Login(string username, string password)
        {
            var requestData = new Dictionary<string, object>
            {
                { "username", username },
                { "password", password }
            };

            return SendRequest("LOGIN", requestData);
        }

        /// <summary>
        /// Gửi request REGISTER đến server
        /// </summary>
        public ServerResponse Register(string username, string email, string phone, string password)
        {
            var requestData = new Dictionary<string, object>
            {
                { "username", username },
                { "email", email ?? "" },
                { "phone", phone ?? "" },
                { "password", password }
            };

            return SendRequest("REGISTER", requestData);
        }

        /// <summary>
        /// Verify token với server
        /// </summary>
        public ServerResponse VerifyToken(string token)
        {
            var requestData = new Dictionary<string, object>
            {
                { "token", token }
            };

            return SendRequest("VERIFY_TOKEN", requestData);
        }

        /// <summary>
        /// Hàm chung để gửi request đến server
        /// </summary>
        private ServerResponse SendRequest(string action, Dictionary<string, object> data)
        {
            try
            {
                using (TcpClient client = new TcpClient())
                {
                    // Timeout 5 giây
                    var result = client.BeginConnect(serverAddress, serverPort, null, null);
                    var success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(5));

                    if (!success)
                    {
                        return new ServerResponse
                        {
                            Success = false,
                            Message = "Cannot connect to server. Please check if server is running."
                        };
                    }

                    client.EndConnect(result);

                    using (NetworkStream stream = client.GetStream())
                    {
                        // Tạo request JSON
                        var request = new
                        {
                            Action = action,
                            Data = data
                        };

                        string requestJson = JsonSerializer.Serialize(request);
                        byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);

                        // Gửi request
                        stream.Write(requestBytes, 0, requestBytes.Length);

                        // Nhận response
                        byte[] buffer = new byte[8192];
                        int bytesRead = stream.Read(buffer, 0, buffer.Length);
                        string responseJson = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                        // Parse response
                        var response = JsonSerializer.Deserialize<ServerResponse>(responseJson);
                        return response;
                    }
                }
            }
            catch (SocketException)
            {
                return new ServerResponse
                {
                    Success = false,
                    Message = "Cannot connect to server. Please ensure the server is running on " + serverAddress + ":" + serverPort
                };
            }
            catch (Exception ex)
            {
                return new ServerResponse
                {
                    Success = false,
                    Message = $"Connection error: {ex.Message}"
                };
            }
        }
    }

    /// <summary>
    /// DTO cho response từ server
    /// </summary>
    public class ServerResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Dictionary<string, object> Data { get; set; }

        // Helper method để lấy giá trị từ Data
        public string GetDataValue(string key)
        {
            if (Data != null && Data.ContainsKey(key))
            {
                return Data[key]?.ToString();
            }
            return null;
        }
    }
}