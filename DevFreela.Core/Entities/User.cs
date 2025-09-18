﻿namespace DevFreela.Core.Entities;

public class User : BaseEntity
{
    public User(string fullName, string email, DateTime birthDate, bool active = false) : base()
    {
        FullName = fullName;
        Email = email;
        BirthDate = birthDate;
        Active = true;

        Skills = [];
        OwnedProjects = [];
        FreelancerProjects = [];
        Comments = [];
    }

    public string FullName { get; private set; }
    public string Email { get; private set; }
    public DateTime BirthDate { get; private set; }
    public bool Active { get; private set; }
    public List<UserSkill> Skills { get; private set; }
    public List<Project> OwnedProjects { get; private set; }
    public List<Project> FreelancerProjects { get; private set; }
    public List<ProjectComment> Comments { get; private set; }

}