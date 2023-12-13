using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ElectronicLibraryAPI2.Models
{
    public partial class Library_DatabaseContext : DbContext
    {
        public Library_DatabaseContext()
        {
        }

        public Library_DatabaseContext(DbContextOptions<Library_DatabaseContext> options)
            : base(options)
        {
        }

        public virtual IEnumerable<BookReaderCardResult> GetBooksByReaderTicket(string numberRiderTicket)
        {
            var numberRiderTicketParameter = new SqlParameter("@Number_Rider_Ticket", numberRiderTicket);

            return this.Set<BookReaderCardResult>()
                .FromSqlRaw("SELECT [dbo].[Book_Reader_Card](@Number_Rider_Ticket) AS Books", numberRiderTicketParameter)
                .ToList();
        }

        public virtual DbSet<Author> Authors { get; set; } = null!;
        public DbSet<BookReaderCardResult> BookReaderCardResults { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; } = null!;
        public virtual DbSet<AuthorView> AuthorViews { get; set; } = null!;
        public virtual DbSet<Basket> Baskets { get; set; } = null!;

        public virtual DbSet<ReturnBook> ReturnBooks { get; set; } = null!;
        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<BookList> BookLists { get; set; } = null!;
        public virtual DbSet<Card> Cards { get; set; } = null!;
        public virtual DbSet<Genre> Genres { get; set; } = null!;
        public virtual DbSet<GenreView> GenreViews { get; set; } = null!;
        public virtual DbSet<IssueProduct> IssueProducts { get; set; } = null!;
        public virtual DbSet<Logging> Loggings { get; set; } = null!;
        public virtual DbSet<Promocode> Promocodes { get; set; } = null!;
        public virtual DbSet<Publisher> Publishers { get; set; } = null!;
        public virtual DbSet<PublisherView> PublisherViews { get; set; } = null!;
        public virtual DbSet<ReadTicketList> ReadTicketLists { get; set; } = null!;
        public virtual DbSet<RiderTicket> RiderTickets { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<StatusLogging> StatusLoggings { get; set; } = null!;
        public virtual DbSet<TypeLiterature> TypeLiteratures { get; set; } = null!;
        public virtual DbSet<TypeLiteratureView> TypeLiteratureViews { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-MR4G4705\\MYSEVERNAME;Initial Catalog=Library_Database;User ID=sa;Password=123");
            }
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookReaderCardResult>().HasNoKey();

            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(e => e.IdAuthor);

                entity.ToTable("Author");

                entity.Property(e => e.IdAuthor).HasColumnName("ID_Author");

                entity.Property(e => e.FirstNameAuthor)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("First_Name_Author");

                entity.Property(e => e.MiddleNameAuthor)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Middle_Name_Author")
                    .HasDefaultValueSql("('-')");

                entity.Property(e => e.SecondNameAuthor)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Second_Name_Author");

                entity.Property(e => e.Deleted_Author).HasColumnName("Deleted_Author");
            });

            modelBuilder.Entity<AuthorView>(entity =>
            {
                entity.HasKey(e => e.IdAuthorView);

                entity.ToTable("Author_View");

                entity.Property(e => e.IdAuthorView).HasColumnName("ID_Author_View");

                entity.Property(e => e.AuthorId).HasColumnName("Author_ID");

                entity.Property(e => e.BookId).HasColumnName("Book_ID");
                entity.Property(e => e.Deleted_Author_View).HasColumnName("Deleted_Author_View");

            });


            modelBuilder.Entity<Basket>(entity =>
            {
                entity.HasKey(e => e.IdBasket);

                entity.ToTable("Basket");

                entity.Property(e => e.IdBasket).HasColumnName("ID_Basket");

                entity.Property(e => e.BookId).HasColumnName("Book_ID");

                entity.Property(e => e.Cost).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.PromocodeId).HasColumnName("Promocode_ID");

                entity.Property(e => e.RiderTicketId).HasColumnName("Rider_Ticket_ID");
                entity.Property(e => e.Deleted_Basket).HasColumnName("Deleted_Basket");
            });

            modelBuilder.Entity<ReturnBook>(entity =>
            {
                entity.HasKey(e => e.IdReturnBook);

                entity.ToTable("ReturnBook");

                entity.Property(e => e.IdReturnBook).HasColumnName("ID_ReturnBook");

                entity.Property(e => e.BookId).HasColumnName("Book_ID");

                entity.Property(e => e.UserId).HasColumnName("User_ID");

                entity.Property(e => e.NameUserForReturn).HasColumnName("NameUserForReturn");

                entity.Property(e => e.EmailUserForReturn).HasColumnName("EmailUserForReturn");

                entity.Property(e => e.ReasonReturn).HasColumnName("ReasonReturn");

                entity.Property(e => e.DeletedReturn).HasColumnName("Deleted_Return");

                entity.Property(e => e.ReturnAgree).HasColumnName("Return_Agree");

                entity.Property(e => e.ReasonNoAgree).HasColumnName("Reason_No_Agree");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.IdBook)
                    .HasName("PK_Product");

                entity.ToTable("Book");

                entity.HasIndex(e => e.NameBook, "UQ_Name_Product")
                    .IsUnique();

                entity.Property(e => e.IdBook).HasColumnName("ID_Book");

                entity.Property(e => e.BriefPlot)
                    .IsUnicode(false)
                    .HasColumnName("Brief_Plot");

                entity.Property(e => e.CoverPhoto)
                    .IsUnicode(false)
                    .HasColumnName("Cover_Photo");

                entity.Property(e => e.NameBook)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Name_Book");

                entity.Property(e => e.NumberPages).HasColumnName("Number_Pages");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(38, 2)")
                    .HasDefaultValueSql("((0.0))");

                entity.Property(e => e.PublicationDate)
                    .HasColumnType("date")
                    .HasColumnName("Publication_Date");

                entity.Property(e => e.Deleted_Book).HasColumnName("Deleted_Book");

                entity.Property(e => e.FormatFB2)
                    .IsUnicode(false)
                    .HasColumnName("FormatFB2");

                entity.Property(e => e.FormatTXT)
                    .IsUnicode(false)
                    .HasColumnName("FormatTXT");
            });

            modelBuilder.Entity<BookList>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Book_List");

                entity.Property(e => e.Автор)
                    .HasMaxLength(92)
                    .IsUnicode(false);

                entity.Property(e => e.ГодПубликации)
                    .HasColumnType("date")
                    .HasColumnName("Год публикации");

                entity.Property(e => e.Жанр)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Издатель)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.КоличествоСтраниц).HasColumnName("Количество страниц");

                entity.Property(e => e.КраткийСюжет)
                    .IsUnicode(false)
                    .HasColumnName("Краткий сюжет");

                entity.Property(e => e.Название)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ТипЛитературы)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Тип литературы");

                entity.Property(e => e.ФотоОбложки)
                    .IsUnicode(false)
                    .HasColumnName("Фото обложки");

                entity.Property(e => e.Цена).HasColumnType("decimal(38, 2)");


            });

            modelBuilder.Entity<Card>(entity =>
            {
                entity.HasKey(e => e.IdCard);

                entity.ToTable("Card");

                entity.HasIndex(e => e.CardNumber, "UQ_Card_Number")
                    .IsUnique();

                entity.Property(e => e.IdCard).HasColumnName("ID_Card");

                entity.Property(e => e.CardHolder)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Card_Holder");

                entity.Property(e => e.CardNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Card_Number");

                entity.Property(e => e.CvcCode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CVC_Code");

                entity.Property(e => e.Validity)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Deleted_Card).HasColumnName("Deleted_Card");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.HasKey(e => e.IdFeedback)
                    .HasName("PK_Feedback");

                entity.ToTable("Feedback");

                entity.Property(e => e.IdFeedback).HasColumnName("ID_Feedback");

                entity.Property(e => e.Message)
                    .IsUnicode(false)
                    .HasColumnName("Message");

                entity.Property(e => e.NameUserMessage)
                    .IsUnicode(false)
                    .HasColumnName("NameUserMessage");

                entity.Property(e => e.EmailUserMessage)
                    .IsUnicode(false)
                    .HasColumnName("EmailUserMessage");

                entity.Property(e => e.UserId).HasColumnName("User_ID");
                entity.Property(e => e.Done).HasColumnName("Done");



            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasKey(e => e.IdGenre);

                entity.ToTable("Genre");

                entity.HasIndex(e => e.NameGenre, "UQ_Name_Genre")
                    .IsUnique();

                entity.Property(e => e.IdGenre).HasColumnName("ID_Genre");

                entity.Property(e => e.NameGenre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Name_Genre");

                entity.Property(e => e.Deleted_Genre).HasColumnName("Deleted_Genre");
            });

            modelBuilder.Entity<GenreView>(entity =>
            {
                entity.HasKey(e => e.IdGenreView);

                entity.ToTable("Genre_View");

                entity.Property(e => e.IdGenreView).HasColumnName("ID_Genre_View");

                entity.Property(e => e.BookId).HasColumnName("Book_ID");

                entity.Property(e => e.GenreId).HasColumnName("Genre_ID");

                entity.Property(e => e.Deleted_Genre_View).HasColumnName("Deleted_Genre_View");

            });

            modelBuilder.Entity<IssueProduct>(entity =>
            {
                entity.HasKey(e => e.IdIssueProduct);

                entity.ToTable("Issue_Product");

                entity.HasIndex(e => e.Barcode, "UQ_Barcode")
                    .IsUnique();

                entity.Property(e => e.IdIssueProduct).HasColumnName("ID_Issue_Product");

                entity.Property(e => e.Barcode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.BookId).HasColumnName("Book_ID");

                entity.Property(e => e.CostIssueFix)
                    .HasColumnType("decimal(38, 2)")
                    .HasColumnName("Cost_Issue_Fix");

                entity.Property(e => e.DateIssue)
                    .HasColumnType("date")
                    .HasColumnName("Date_Issue")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RiderTicketId).HasColumnName("Rider_Ticket_ID");

                entity.Property(e => e.Deleted_Issue_Product).HasColumnName("Deleted_Issue_Product");
            });

            modelBuilder.Entity<Logging>(entity =>
            {
                entity.HasKey(e => e.IdLogging)
                    .HasName("PK_Log");

                entity.ToTable("Logging");

                entity.Property(e => e.IdLogging).HasColumnName("ID_Logging");

                entity.Property(e => e.CostIssue)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("Cost_Issue");

                entity.Property(e => e.DateForm)
                    .HasColumnType("date")
                    .HasColumnName("Date_Form");

                entity.Property(e => e.IssueProductId).HasColumnName("Issue_Product_ID");

                entity.Property(e => e.RiderTicketId).HasColumnName("Rider_Ticket_ID");

                entity.Property(e => e.StatusLoggingId).HasColumnName("Status_Logging_ID");

                entity.Property(e => e.TimeNotes).HasColumnName("Time_Notes");

                entity.Property(e => e.UserId).HasColumnName("User_ID");


            });

            modelBuilder.Entity<Promocode>(entity =>
            {
                entity.HasKey(e => e.IdPromocode);

                entity.ToTable("Promocode");

                entity.HasIndex(e => e.NamePromocode, "UQ_Name_Promocode")
                    .IsUnique();

                entity.Property(e => e.IdPromocode).HasColumnName("ID_Promocode");

                entity.Property(e => e.NamePromocode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Name_Promocode");

                entity.Property(e => e.Deleted_Promocode).HasColumnName("Deleted_Promocode");
            });

            modelBuilder.Entity<Publisher>(entity =>
            {
                entity.HasKey(e => e.IdPublisher);

                entity.ToTable("Publisher");

                entity.HasIndex(e => e.NamePublisher, "UQ_ Name_Publisher")
                    .IsUnique();

                entity.Property(e => e.IdPublisher).HasColumnName("ID_Publisher");

                entity.Property(e => e.NamePublisher)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Name_Publisher");

                entity.Property(e => e.Deleted_Publisher).HasColumnName("Deleted_Publisher");
            });

            modelBuilder.Entity<PublisherView>(entity =>
            {
                entity.HasKey(e => e.IdPublisherView);

                entity.ToTable("Publisher_View");

                entity.Property(e => e.IdPublisherView).HasColumnName("ID_Publisher_View");

                entity.Property(e => e.BookId).HasColumnName("Book_ID");

                entity.Property(e => e.PublisherId).HasColumnName("Publisher_ID");
                entity.Property(e => e.Deleted_Publisher_View).HasColumnName("Deleted_Publisher_View");
            });

            modelBuilder.Entity<ReadTicketList>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Read_Ticket_List");

                entity.Property(e => e.ВыдачаТовара)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Выдача товара");

                entity.Property(e => e.ДанныеОКлиенте)
                    .HasMaxLength(417)
                    .IsUnicode(false)
                    .HasColumnName("Данные о клиенте");

                entity.Property(e => e.УникальныйНомер)
                    .HasMaxLength(21)
                    .IsUnicode(false)
                    .HasColumnName("Уникальный номер");
            });

            modelBuilder.Entity<RiderTicket>(entity =>
            {
                entity.HasKey(e => e.IdRiderTicket);

                entity.ToTable("Rider_Ticket");

                entity.Property(e => e.IdRiderTicket).HasColumnName("ID_Rider_Ticket");

                entity.Property(e => e.DateTerm)
                    .HasColumnType("date")
                    .HasColumnName("Date_Term");

                entity.Property(e => e.NumberRiderTicket)
                    .HasMaxLength(21)
                    .IsUnicode(false)
                    .HasColumnName("Number_Rider_Ticket");

                entity.Property(e => e.UserId).HasColumnName("User_ID");

                entity.Property(e => e.Deleted_Rider_Ticket).HasColumnName("Deleted_Rider_Ticket");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IdRole);

                entity.ToTable("Role");

                entity.HasIndex(e => e.NameRole, "UQ_Name_Role")
                    .IsUnique();

                entity.Property(e => e.IdRole).HasColumnName("ID_Role");

                entity.Property(e => e.NameRole)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Name_Role");

                entity.Property(e => e.Deleted_Role).HasColumnName("Deleted_Role");
            });

            modelBuilder.Entity<StatusLogging>(entity =>
            {
                entity.HasKey(e => e.IdStatusLogging);

                entity.ToTable("Status_Logging");

                entity.Property(e => e.IdStatusLogging).HasColumnName("ID_Status_Logging");

                entity.Property(e => e.StatusLogName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Status_Log_name");
            });

            modelBuilder.Entity<TypeLiterature>(entity =>
            {
                entity.HasKey(e => e.IdTypeLiterature)
                    .HasName("PK_ Literature");

                entity.ToTable("Type_Literature");

                entity.HasIndex(e => e.NameTypeLiterature, "UQ_Name_Type_Literature")
                    .IsUnique();

                entity.Property(e => e.IdTypeLiterature).HasColumnName("ID_Type_Literature");

                entity.Property(e => e.NameTypeLiterature)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Name_Type_Literature");

                entity.Property(e => e.Deleted_Type_Literature).HasColumnName("Deleted_Type_Literature");
            });

            modelBuilder.Entity<TypeLiteratureView>(entity =>
            {
                entity.HasKey(e => e.IdTypeLiteratureView);

                entity.ToTable("Type_Literature_View");

                entity.Property(e => e.IdTypeLiteratureView).HasColumnName("ID_Type_Literature_View");

                entity.Property(e => e.BookId).HasColumnName("Book_ID");

                entity.Property(e => e.TypeLiteratureId).HasColumnName("Type_Literature_ID");

                entity.Property(e => e.Deleted_Type_Literature_View).HasColumnName("Deleted_Type_Literature_View");

            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser);

                entity.ToTable("User");

                entity.HasIndex(e => e.Login, "UQ_Login")
                    .IsUnique();

                entity.Property(e => e.IdUser).HasColumnName("ID_User");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("date")
                    .HasColumnName("Birth_Date");

                entity.Property(e => e.CardId).HasColumnName("Card_ID");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("First_Name");

                entity.Property(e => e.Login)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Middle_Name")
                    .HasDefaultValueSql("('-')");

                entity.Property(e => e.PassportNumber)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("Passport_Number");

                entity.Property(e => e.PassportSeries)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("Passport_Series");

                entity.Property(e => e.Password)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId).HasColumnName("Role_ID");

                entity.Property(e => e.SaltPassword)
                    .IsUnicode(false)
                    .HasColumnName("Salt_Password");

                entity.Property(e => e.SecondName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Second_Name");

                entity.Property(e => e.Deleted_User).HasColumnName("Deleted_User");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Email");

            });

            OnModelCreatingPartial(modelBuilder);
        }
        
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
