using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonnelManagement.Infrastructure.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class Add_Person_Class : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameEn = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    NameFa = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    FamilyEn = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FamilyFa = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    FatherName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Gender = table.Column<bool>(type: "bit", nullable: false),
                    Birthday = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Marriage = table.Column<bool>(type: "bit", nullable: false),
                    PassportNo = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: true),
                    NationalCode = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: true),
                    NationalCodeOfFather = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: true),
                    BirthCityId = table.Column<long>(type: "bigint", nullable: false),
                    CityId = table.Column<long>(type: "bigint", nullable: false),
                    Attachment = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    EducationalDegree = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    EducationalField = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IDCartNo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AddressId = table.Column<long>(type: "bigint", nullable: false),
                    MobileNo1 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    MobileNo2 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PhoneNo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    MailBoxAddress1 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    MailBoxAddress2 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SocialAddress1 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SocialAddress2 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    WorkingStartDate = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Activate = table.Column<bool>(type: "bit", nullable: false),
                    PersonGroupId = table.Column<long>(type: "bigint", nullable: false),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persons_PersonGroups_PersonGroupId",
                        column: x => x.PersonGroupId,
                        principalTable: "PersonGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Alpha2Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Alpha3Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UNCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DialCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PictureId = table.Column<long>(type: "bigint", nullable: false),
                    TailCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MetaDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersonId = table.Column<long>(type: "bigint", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Country_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Province",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DialCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MetaDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Keywords = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryId = table.Column<long>(type: "bigint", nullable: false),
                    PersonId = table.Column<long>(type: "bigint", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Province", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Province_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Province_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DialCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MetaDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Keywords = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProvinceId = table.Column<long>(type: "bigint", nullable: false),
                    PersonId = table.Column<long>(type: "bigint", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                    table.ForeignKey(
                        name: "FK_City_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_City_Province_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Province",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_City_PersonId",
                table: "City",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_City_ProvinceId",
                table: "City",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Country_PersonId",
                table: "Country",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_PersonGroupId",
                table: "Persons",
                column: "PersonGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Province_CountryId",
                table: "Province",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Province_PersonId",
                table: "Province",
                column: "PersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "Province");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
