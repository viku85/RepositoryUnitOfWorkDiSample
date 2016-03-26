using MyProject.Model.ModelConstraint;

namespace MyProject.Model.DataModel
{
    /// <summary>
    /// Book Information
    /// </summary>
    public class Book
        : IPrimaryKey<int>
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the book title.
        /// </summary>
        /// <value>
        /// The book title.
        /// </value>
        public string BookTitle { get; set; }

        /// <summary>
        /// Gets or sets the ISBN.
        /// </summary>
        /// <value>
        /// The ISBN.
        /// </value>
        public string ISBN { get; set; }

        /// <summary>
        /// Gets or sets the name of the publisher.
        /// </summary>
        /// <value>
        /// The name of the publisher.
        /// </value>
        public string PublisherName { get; set; }
    }
}