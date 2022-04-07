using Microsoft.EntityFrameworkCore.Migrations;

namespace TTechTask.DataAccess.Migrations
{
    public partial class seedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                insert into AspNetRoles (id,Name,NormalizedName)
                values 
                (1,'Admin','ADMIN'),
                (2,'User','USER'),
                (3,'Tester','TESTER')
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"delete from AspNetRoles");
        }
    }
}
