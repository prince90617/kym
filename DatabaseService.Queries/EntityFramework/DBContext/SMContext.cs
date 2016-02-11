using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DatabaseService.Queries
{
    public class SMContext : DbContext
    {
        public SMContext()
            : base("DefaultConnection")
        {
           // Database.SetInitializer<SMContext>(new DropCreateDatabaseIfModelChanges<SMContext>());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }

    [Table("user")]
    public class User
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int user_id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string photo { get; set; }
        public string email { get; set; }
        public int role_id { get; set; }
        public DateTime last_updated { get; set; }
        public DateTime created_on { get; set; }
        public int created_by { get; set; }
        public int status { get; set; }
    }
    [Table("role")]
    public class Role
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int role_id { get; set; }
        public string role { get; set; }
    }
    [Table("client")]
    public class Client
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int client_id { get; set; }
        public string client_secret { get; set; }
        public string name { get; set; }
        public string application_type { get; set; }
        public bool active { get; set; }
        public int refresh_token_life_time { get; set; }
        public string allowed_origin { get; set; }
    }
    [Table("refresh_token")]
    public class RefreshToken
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int token_id { get; set; }
        public int client_id { get; set; }
        public string token { get; set; }
        public string username { get; set; }
        public DateTime issued_on { get; set; }
        public DateTime expires_on { get; set; }
        public string protected_ticket { get; set; }
    }
}
