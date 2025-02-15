using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BooksApi.Models;

[Table("Book")]
public partial class Book
{
    [Key]
    public int Id { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string Title { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string Author { get; set; } = null!;

    public DateOnly PublicationDate { get; set; }

    public int? EditionNumber { get; set; }

    [Column("ISBN")]
    [StringLength(50)]
    [Unicode(false)]
    public string Isbn { get; set; } = null!;
}
