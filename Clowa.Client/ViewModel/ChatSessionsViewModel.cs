﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Clowa.Client.Events;
using Clowa.Models;

namespace Clowa.Client.ViewModel
{
    public class ChatSessionsViewModel
    {
        public event EventHandler<ChatSessionEventArgs> SendMessage;
        private readonly Dictionary<string, ChatSessionViewModel> _chatSessions = new Dictionary<string, ChatSessionViewModel>();
        public ChatSessionViewModel StartNewSession(User contact, User initiator)
        {
            var viewModel = new ChatSessionViewModel(contact);
            viewModel.Initiator = initiator;
            viewModel.SendMessage += OnSendMessage;
            viewModel.ChatSessionClosed += OnChatSessionClosed;
            _chatSessions.Add(contact.Name, viewModel);
            return viewModel;
        }

        public void AddMessage(Message message, User initiator)
        {
            ChatSessionViewModel chatSession;
            if (!_chatSessions.TryGetValue(message.From.Name, out chatSession))
            {
                chatSession = StartNewSession(message.From, initiator);
                chatSession.OpenChat();
            }
            chatSession.MessageReceived(message);
        }

        private void OnSendMessage(object sender, ChatSessionEventArgs e)
        {
            if (e != null)
            {
                SendMessage(sender, e);
            }
        }

        private void OnChatSessionClosed(object sender, ChatSessionEventArgs e)
        {
            if (e != null && e.Contact != null)
            {
                _chatSessions.Remove(e.Contact.Name);
            }
        }
    }
}
