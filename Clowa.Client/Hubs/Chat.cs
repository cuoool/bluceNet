﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Clowa.Models;
using System.Threading.Tasks;
using SignalR.Client.Hubs;

namespace Clowa.Client.Hubs
{
    /// <summary>
    /// Strongly typed client side proxy for the server side Hub (Similar to add service reference).
    /// </summary>
    public class Chat
    {
        private readonly IHubProxy _chat;

        public event Action<User> UserOnline;

        public event Action<User> UserOffline;

        public event Action<Message> Message;

        public Chat(HubConnection connection)
        {
            _chat = connection.CreateProxy("Chat");

            _chat.On<User>("markOnline", user =>
            {
                if (UserOnline != null)
                {
                    UserOnline(user);
                }
            });

            _chat.On<User>("markOffline", user =>
            {
                if (UserOffline != null)
                {
                    UserOffline(user);
                }
            });

            _chat.On<Message>("addMessage", message =>
            {
                if (Message != null)
                {
                    Message(message);
                }
            });
        }

        public Task<IEnumerable<User>> GetUsers()
        {
            return _chat.Invoke<IEnumerable<User>>("GetUsers");
        }

        public Task<User> GetUser(string userName)
        {
            return _chat.Invoke<User>("GetUser", userName);
        }

        public Task<Message> SendMessage(string userName, string message)
        {
            return _chat.Invoke<Message>("Send", userName, message);
        }
    }
}
