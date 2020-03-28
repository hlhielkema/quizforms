using QuizForms.Data.Models.Contact;
using System;
using System.Collections.Generic;

namespace QuizForms.Data.Repositories
{
    /// <summary>
    /// Contact messages repository
    /// </summary>
    public interface IContactMessagesRepository
    {
        /// <summary>
        /// Gets the number of messages
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Create a new contact message
        /// </summary>
        /// <param name="emailAddress">email address</param>
        /// <param name="messageText">message text</param>
        /// <returns>unique message id</returns>
        Guid Create(string emailAddress, string messageText);

        /// <summary>
        /// Get all contact messages
        /// </summary>
        /// <returns>list with contact messages</returns>
        List<ContactMessage> GetAll();        

        /// <summary>
        /// Get a message by its id
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns>contact message</returns>
        ContactMessage GetById(Guid messageId);

        /// <summary>
        /// Archive a message
        /// </summary>
        /// <param name="messageId">message id</param>
        /// <returns>
        ///     true = found and archived;
        ///     false = not found
        /// </returns>
        bool Archive(Guid messageId);

        /// <summary>
        /// Delete a message
        /// </summary>
        /// <param name="messageId">message id</param>
        /// <returns>
        ///     true = found and deleted;
        ///     false = not found
        /// </returns>
        bool Delete(Guid messageId);       

        /// <summary>
        /// Delete all contact messgaes.
        /// </summary>
        void DeleteAll();
    }
}
