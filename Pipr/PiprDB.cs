namespace Pipr
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Pipr.Models;

    public partial class PiprDB : DbContext
    {
        public PiprDB()
            : base("name=PiprDB")
        {
        }

        public DbSet<ShellCommand> ShellCommands { get; set; }
        public DbSet<DefaultCommand> DefaultCommands { get; set; }
        public DbSet<SocialCommand> SocialCommands { get; set; }
        public DbSet<TaskList> TaskLists { get; set; }
        public DbSet<InternalTask> Tasks { get; set; }
        public DbSet<Word> Words { get; set; }
        public DbSet<CasedWord> CasedWords { get; set; }
        public DbSet<Sense> Senses { get; set; }
        public DbSet<Synset> Synsets { get; set; }
        public DbSet<LinkType> LinkTypes { get; set; }
        public DbSet<SemLink> SemLinks { get; set; }
        public DbSet<LexLink> LexLinks { get; set; }
        public DbSet<PosType> PosTypes { get; set; }
        



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }

    public partial class Dictionary : DbContext
    {
        public Dictionary()
            : base("name=Dictionary")
        { 
        }

        public DbSet<Word> words { get; set; }

    }
}
