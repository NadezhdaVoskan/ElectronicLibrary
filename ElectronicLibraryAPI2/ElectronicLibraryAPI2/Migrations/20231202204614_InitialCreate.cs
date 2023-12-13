using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectronicLibraryAPI2.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    ID_Author = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    First_Name_Author = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    Second_Name_Author = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    Middle_Name_Author = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true, defaultValueSql: "('-')"),
                    Deleted_Author = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.ID_Author);
                });

            migrationBuilder.CreateTable(
                name: "Basket",
                columns: table => new
                {
                    ID_Basket = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cost = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Rider_Ticket_ID = table.Column<int>(type: "int", nullable: true),
                    Book_ID = table.Column<int>(type: "int", nullable: true),
                    Promocode_ID = table.Column<int>(type: "int", nullable: true),
                    Deleted_Basket = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Basket", x => x.ID_Basket);
                });

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    ID_Book = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_Book = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Publication_Date = table.Column<DateTime>(type: "date", nullable: false),
                    Brief_Plot = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    Number_Pages = table.Column<int>(type: "int", nullable: true),
                    Cover_Photo = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(38,2)", nullable: true, defaultValueSql: "((0.0))"),
                    Deleted_Book = table.Column<bool>(type: "bit", nullable: true),
                    FormatFB2 = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    FormatTXT = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ID_Book);
                });

            migrationBuilder.CreateTable(
                name: "Card",
                columns: table => new
                {
                    ID_Card = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Card_Number = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Card_Holder = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Validity = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CVC_Code = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Deleted_Card = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Card", x => x.ID_Card);
                });

            migrationBuilder.CreateTable(
                name: "Feedback",
                columns: table => new
                {
                    ID_Feedback = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    NameUserMessage = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    EmailUserMessage = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    User_ID = table.Column<int>(type: "int", nullable: true),
                    Done = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedback", x => x.ID_Feedback);
                });

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    ID_Genre = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_Genre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Deleted_Genre = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.ID_Genre);
                });

            migrationBuilder.CreateTable(
                name: "Logging",
                columns: table => new
                {
                    ID_Logging = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date_Form = table.Column<DateTime>(type: "date", nullable: false),
                    Cost_Issue = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Status_Logging_ID = table.Column<int>(type: "int", nullable: true),
                    Issue_Product_ID = table.Column<int>(type: "int", nullable: true),
                    Rider_Ticket_ID = table.Column<int>(type: "int", nullable: true),
                    Time_Notes = table.Column<TimeSpan>(type: "time", nullable: false),
                    User_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log", x => x.ID_Logging);
                });

            migrationBuilder.CreateTable(
                name: "Promocode",
                columns: table => new
                {
                    ID_Promocode = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_Promocode = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Discount = table.Column<int>(type: "int", nullable: false),
                    Deleted_Promocode = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promocode", x => x.ID_Promocode);
                });

            migrationBuilder.CreateTable(
                name: "Publisher",
                columns: table => new
                {
                    ID_Publisher = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_Publisher = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Deleted_Publisher = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publisher", x => x.ID_Publisher);
                });

            migrationBuilder.CreateTable(
                name: "Rider_Ticket",
                columns: table => new
                {
                    ID_Rider_Ticket = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number_Rider_Ticket = table.Column<string>(type: "varchar(21)", unicode: false, maxLength: 21, nullable: false),
                    Date_Term = table.Column<DateTime>(type: "date", nullable: false),
                    User_ID = table.Column<int>(type: "int", nullable: true),
                    Deleted_Rider_Ticket = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rider_Ticket", x => x.ID_Rider_Ticket);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    ID_Role = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_Role = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Deleted_Role = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.ID_Role);
                });

            migrationBuilder.CreateTable(
                name: "Status_Logging",
                columns: table => new
                {
                    ID_Status_Logging = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status_Log_name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status_Logging", x => x.ID_Status_Logging);
                });

            migrationBuilder.CreateTable(
                name: "Type_Literature",
                columns: table => new
                {
                    ID_Type_Literature = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_Type_Literature = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Deleted_Type_Literature = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ Literature", x => x.ID_Type_Literature);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID_User = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    First_Name = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    Second_Name = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    Middle_Name = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true, defaultValueSql: "('-')"),
                    Login = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: false),
                    Password = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: false),
                    Salt_Password = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Passport_Series = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    Passport_Number = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: true),
                    Birth_Date = table.Column<DateTime>(type: "date", nullable: true),
                    Card_ID = table.Column<int>(type: "int", nullable: true),
                    Role_ID = table.Column<int>(type: "int", nullable: true),
                    Deleted_User = table.Column<bool>(type: "bit", nullable: true),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID_User);
                });

            migrationBuilder.CreateTable(
                name: "Author_View",
                columns: table => new
                {
                    ID_Author_View = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Book_ID = table.Column<int>(type: "int", nullable: true),
                    Author_ID = table.Column<int>(type: "int", nullable: true),
                    Deleted_Author_View = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author_View", x => x.ID_Author_View);
                    table.ForeignKey(
                        name: "FK_Author_View_Author_Author_ID",
                        column: x => x.Author_ID,
                        principalTable: "Author",
                        principalColumn: "ID_Author");
                    table.ForeignKey(
                        name: "FK_Author_View_Book_Book_ID",
                        column: x => x.Book_ID,
                        principalTable: "Book",
                        principalColumn: "ID_Book");
                });

            migrationBuilder.CreateTable(
                name: "Issue_Product",
                columns: table => new
                {
                    ID_Issue_Product = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date_Issue = table.Column<DateTime>(type: "date", nullable: true, defaultValueSql: "(getdate())"),
                    Barcode = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Rider_Ticket_ID = table.Column<int>(type: "int", nullable: true),
                    Book_ID = table.Column<int>(type: "int", nullable: true),
                    Cost_Issue_Fix = table.Column<decimal>(type: "decimal(38,2)", nullable: true),
                    Deleted_Issue_Product = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issue_Product", x => x.ID_Issue_Product);
                    table.ForeignKey(
                        name: "FK_Issue_Product_Book_Book_ID",
                        column: x => x.Book_ID,
                        principalTable: "Book",
                        principalColumn: "ID_Book");
                });

            migrationBuilder.CreateTable(
                name: "ReturnBook",
                columns: table => new
                {
                    ID_ReturnBook = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_ID = table.Column<int>(type: "int", nullable: true),
                    Book_ID = table.Column<int>(type: "int", nullable: true),
                    ReasonReturn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameUserForReturn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailUserForReturn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted_Return = table.Column<bool>(type: "bit", nullable: false),
                    Return_Agree = table.Column<bool>(type: "bit", nullable: true),
                    Reason_No_Agree = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReturnBook", x => x.ID_ReturnBook);
                    table.ForeignKey(
                        name: "FK_ReturnBook_Book_Book_ID",
                        column: x => x.Book_ID,
                        principalTable: "Book",
                        principalColumn: "ID_Book");
                });

            migrationBuilder.CreateTable(
                name: "Genre_View",
                columns: table => new
                {
                    ID_Genre_View = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Book_ID = table.Column<int>(type: "int", nullable: true),
                    Genre_ID = table.Column<int>(type: "int", nullable: true),
                    Deleted_Genre_View = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre_View", x => x.ID_Genre_View);
                    table.ForeignKey(
                        name: "FK_Genre_View_Book_Book_ID",
                        column: x => x.Book_ID,
                        principalTable: "Book",
                        principalColumn: "ID_Book");
                    table.ForeignKey(
                        name: "FK_Genre_View_Genre_Genre_ID",
                        column: x => x.Genre_ID,
                        principalTable: "Genre",
                        principalColumn: "ID_Genre");
                });

            migrationBuilder.CreateTable(
                name: "Publisher_View",
                columns: table => new
                {
                    ID_Publisher_View = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Book_ID = table.Column<int>(type: "int", nullable: true),
                    Publisher_ID = table.Column<int>(type: "int", nullable: true),
                    Deleted_Publisher_View = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publisher_View", x => x.ID_Publisher_View);
                    table.ForeignKey(
                        name: "FK_Publisher_View_Book_Book_ID",
                        column: x => x.Book_ID,
                        principalTable: "Book",
                        principalColumn: "ID_Book");
                    table.ForeignKey(
                        name: "FK_Publisher_View_Publisher_Publisher_ID",
                        column: x => x.Publisher_ID,
                        principalTable: "Publisher",
                        principalColumn: "ID_Publisher");
                });

            migrationBuilder.CreateTable(
                name: "Type_Literature_View",
                columns: table => new
                {
                    ID_Type_Literature_View = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Book_ID = table.Column<int>(type: "int", nullable: true),
                    Type_Literature_ID = table.Column<int>(type: "int", nullable: true),
                    Deleted_Type_Literature_View = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Type_Literature_View", x => x.ID_Type_Literature_View);
                    table.ForeignKey(
                        name: "FK_Type_Literature_View_Book_Book_ID",
                        column: x => x.Book_ID,
                        principalTable: "Book",
                        principalColumn: "ID_Book");
                    table.ForeignKey(
                        name: "FK_Type_Literature_View_Type_Literature_Type_Literature_ID",
                        column: x => x.Type_Literature_ID,
                        principalTable: "Type_Literature",
                        principalColumn: "ID_Type_Literature");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Author_View_Author_ID",
                table: "Author_View",
                column: "Author_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Author_View_Book_ID",
                table: "Author_View",
                column: "Book_ID");

            migrationBuilder.CreateIndex(
                name: "UQ_Name_Product",
                table: "Book",
                column: "Name_Book",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Card_Number",
                table: "Card",
                column: "Card_Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Name_Genre",
                table: "Genre",
                column: "Name_Genre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Genre_View_Book_ID",
                table: "Genre_View",
                column: "Book_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Genre_View_Genre_ID",
                table: "Genre_View",
                column: "Genre_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Issue_Product_Book_ID",
                table: "Issue_Product",
                column: "Book_ID");

            migrationBuilder.CreateIndex(
                name: "UQ_Barcode",
                table: "Issue_Product",
                column: "Barcode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Name_Promocode",
                table: "Promocode",
                column: "Name_Promocode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_ Name_Publisher",
                table: "Publisher",
                column: "Name_Publisher",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Publisher_View_Book_ID",
                table: "Publisher_View",
                column: "Book_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Publisher_View_Publisher_ID",
                table: "Publisher_View",
                column: "Publisher_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnBook_Book_ID",
                table: "ReturnBook",
                column: "Book_ID");

            migrationBuilder.CreateIndex(
                name: "UQ_Name_Role",
                table: "Role",
                column: "Name_Role",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Name_Type_Literature",
                table: "Type_Literature",
                column: "Name_Type_Literature",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Type_Literature_View_Book_ID",
                table: "Type_Literature_View",
                column: "Book_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Type_Literature_View_Type_Literature_ID",
                table: "Type_Literature_View",
                column: "Type_Literature_ID");

            migrationBuilder.CreateIndex(
                name: "UQ_Login",
                table: "User",
                column: "Login",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Author_View");

            migrationBuilder.DropTable(
                name: "Basket");

            migrationBuilder.DropTable(
                name: "Card");

            migrationBuilder.DropTable(
                name: "Feedback");

            migrationBuilder.DropTable(
                name: "Genre_View");

            migrationBuilder.DropTable(
                name: "Issue_Product");

            migrationBuilder.DropTable(
                name: "Logging");

            migrationBuilder.DropTable(
                name: "Promocode");

            migrationBuilder.DropTable(
                name: "Publisher_View");

            migrationBuilder.DropTable(
                name: "ReturnBook");

            migrationBuilder.DropTable(
                name: "Rider_Ticket");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Status_Logging");

            migrationBuilder.DropTable(
                name: "Type_Literature_View");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Author");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropTable(
                name: "Publisher");

            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "Type_Literature");
        }
    }
}
