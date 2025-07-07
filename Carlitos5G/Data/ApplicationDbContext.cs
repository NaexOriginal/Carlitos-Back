using System;
using System.Collections.Generic;
using Carlitos5G.Data.Seeders;
using Carlitos5G.Models;
using Microsoft.EntityFrameworkCore;

namespace Carlitos5G.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }


    public virtual DbSet<Admin> Admins { get; set; }
    public virtual DbSet<AdminTicket> AdminTickets { get; set; }
    public virtual DbSet<Avance> Avances { get; set; }
    public virtual DbSet<Bookmark> Bookmarks { get; set; }
    public virtual DbSet<Certificate> Certificates { get; set; }
    public virtual DbSet<Comment> Comments { get; set; }
    public virtual DbSet<Contact> Contacts { get; set; }
    public virtual DbSet<Content> Contents { get; set; }
    public virtual DbSet<Download> Downloads { get; set; }
    public virtual DbSet<Eventoscalendar> Eventoscalendars { get; set; }
    public virtual DbSet<Flipbook> Flipbooks { get; set; }
    public virtual DbSet<Like> Likes { get; set; }
    public virtual DbSet<Ova> Ovas { get; set; }
    public virtual DbSet<Playlist> Playlists { get; set; }
    public virtual DbSet<Prequest> Prequests { get; set; }
    public virtual DbSet<Tblresult> Tblresults { get; set; }
    public virtual DbSet<Tblsubject> Tblsubjects { get; set; }
    public virtual DbSet<Ticket> Tickets { get; set; }
    public virtual DbSet<Tutor> Tutors { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<UserTicket> UserTickets { get; set; }
    public virtual DbSet<Usercheck> Userchecks { get; set; }
    public virtual DbSet<Videogame> Videogames { get; set; }
    public virtual DbSet<AuditLog> AuditLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        OnModelCreatingPartial(modelBuilder);

        AdminSeeder.Seed(modelBuilder);
        TutorSeeder.Seed(modelBuilder);
        UserSeeder.Seed(modelBuilder);

    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
