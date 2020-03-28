using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using QuizForms.Data.Models.Contact;
using System;
using System.Collections.Generic;
using System.IO;

namespace QuizForms.Data.Repositories
{
    /// <summary>
    /// Contact messages repository
    /// </summary>
    public sealed class ContactMessagesRepository : QuizFormsBaseRepository, IContactMessagesRepository
    {
        /// <summary>
        /// Gets the number of messages
        /// </summary>
        public int Count
        {
            get
            {
                return Directory.GetFiles(ContactPath, "*.json").Length;
            }
        }

        /// <summary>
        /// Constuctor
        /// </summary>
        /// <param name="settings">quiz forms settings</param>
        public ContactMessagesRepository(IOptions<QuizFormsSettings> settings)
            : base(settings)
        { }

        /// <summary>
        /// Create a new contact message
        /// </summary>
        /// <param name="emailAddress">email address</param>
        /// <param name="messageText">message text</param>
        /// <returns>unique message id</returns>
        public Guid Create(string emailAddress, string messageText)
        {            
            // Combine the data into a model
            ContactMessage message = new ContactMessage()
            {
                Id = Guid.NewGuid(),
                EmailAddress = emailAddress,
                Message = messageText,
                DateCreated = DateTime.UtcNow
            };

            // Serialize the data to JSON
            string json = JsonConvert.SerializeObject(message, Formatting.Indented);

            // Construct the filename for the new contact message
            string filename = Path.Combine(ContactPath, string.Format("{0}.json", message.Id));

            // Write the JSON to the new contact message file
            File.WriteAllText(filename, json);

            // Return the unqiue id of the new message
            return message.Id;
        }

        /// <summary>
        /// Get all contact messages
        /// </summary>
        /// <returns>list with contact messages</returns>
        public List<ContactMessage> GetAll()
        {
            // Create a list for the found messages
            List<ContactMessage> messages = new List<ContactMessage>();

            // Loop through the JSON files in the contact message directory
            foreach (string filename in Directory.GetFiles(ContactPath, "*.json"))
            {
                // Read the JSON from the file
                string json = File.ReadAllText(filename);

                // Deserialize the message
                ContactMessage message = JsonConvert.DeserializeObject<ContactMessage>(json);

                // Add the message to the results list
                messages.Add(message);
            }

            // Return the list with found messages
            return messages;
        }

        /// <summary>
        /// Get a message by its id
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns>contact message</returns>
        public ContactMessage GetById(Guid messageId)
        {
            // Construct the filename of the (existing) contact message file
            string filename = Path.Combine(ContactPath, string.Format("{0}.json", messageId));

            // Check if the file exists
            if (File.Exists(filename))
            {
                // Read the JSON from the file
                string json = File.ReadAllText(filename);

                // Deserialize the message
                ContactMessage message = JsonConvert.DeserializeObject<ContactMessage>(json);

                // Return the message
                return message;
            }

            // Return null if the contact message does not exist
            return null;
        }

        /// <summary>
        /// Archive a message
        /// </summary>
        /// <param name="messageId">message id</param>
        /// <returns>
        ///     true = found and archived;
        ///     false = not found
        /// </returns>
        public bool Archive(Guid messageId)
        {
            // Construct the existing and archived filename for the contact message
            string filename = Path.Combine(ContactPath, string.Format("{0}.json", messageId));
            string newFilename = Path.Combine(ContactPath, string.Format("{0}.archived", messageId));

            // Check if the file exists
            if (File.Exists(filename))
            {
                // Rename the file
                File.Move(filename, newFilename);

                // File found and deleted
                return true;
            }
            else
                // File not found
                return false;
        }

        /// <summary>
        /// Delete a message
        /// </summary>
        /// <param name="messageId">message id</param>
        /// <returns>
        ///     true = found and deleted;
        ///     false = not found
        /// </returns>
        public bool Delete(Guid messageId)
        {
            // Construct the filename of the (existing) contact message file
            string filename = Path.Combine(ContactPath, string.Format("{0}.json", messageId));

            // Check if the file exists
            if (File.Exists(filename))
            {
                // Delete the file
                File.Delete(filename);

                // File found and deleted
                return true;
            }
            else
                // File not found
                return false;
        }

        /// <summary>
        /// Delete all contact messgaes.
        /// </summary>
        public void DeleteAll()
        {
            // Delete all JSON files in the directory
            foreach (string filename in Directory.GetFiles(ContactPath, "*.json"))
                File.Delete(filename);
        }        
    }
}
