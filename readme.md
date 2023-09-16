# Study Buddy v2

## Student project management application.

Studdy Buddy is an application that allows the management of student projects.

**Projects**: The application revolves around creating, editing, and managing projects. Projects have various attributes such as topic, description, requirements, technology stack, programming languages, level of difficulty, and dates associated with them.

**Teams**: Each project is associated with a team that consists of members. Teams have names, membership information, and a list of completed projects.

**Roles and Leadership**: Users within the system can have different roles within a project, such as developer, tester, leader, or devops. The user who creates a project initially becomes its leader but can transfer this role to another member of the project team.

**User Management**: The application includes user management features like registration verification and profile editing.

## Entities

**User:**

**Project:**
- Id
- Topic
- Description
- EstimatedTime
- Deadline
- Team
- Requirements
- RepositoryLink
- ProgrammingLanguages
- ProjectDifficulty
- ProjectState

**Team:**
- Id
- Name
- Description
- Memberships
- IsOpenTeam
