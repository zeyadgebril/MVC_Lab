<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
  <title>MVC_Lab - ASP.NET MVC Project</title>
  <style>
    :root {
      --primary-color: #2c3e50;
      --secondary-color: #3498db;
      --accent-color: #e74c3c;
      --light-bg: #f8f9fa;
      --dark-text: #333;
      --light-text: #7f8c8d;
    }
    
    body {
      font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
      line-height: 1.8;
      padding: 20px;
      max-width: 1200px;
      margin: 0 auto;
      background-color: var(--light-bg);
      color: var(--dark-text);
    }
    
    h1 {
      color: var(--primary-color);
      border-bottom: 3px solid var(--secondary-color);
      padding-bottom: 10px;
      margin-top: 0;
    }
    
    h2 {
      color: var(--primary-color);
      margin-top: 40px;
      border-left: 4px solid var(--secondary-color);
      padding-left: 15px;
    }
    
    h3 {
      color: var(--primary-color);
    }
    
    a {
      color: var(--secondary-color);
      text-decoration: none;
      transition: all 0.3s ease;
    }
    
    a:hover {
      color: var(--accent-color);
      text-decoration: underline;
    }
    
    pre {
      background-color: #f0f3f6;
      padding: 20px;
      border-radius: 8px;
      overflow-x: auto;
      border-left: 4px solid var(--secondary-color);
      font-family: 'Consolas', monospace;
    }
    
    code {
      font-family: 'Consolas', monospace;
      background-color: #f0f3f6;
      padding: 2px 5px;
      border-radius: 3px;
    }
    
    ul, ol {
      padding-left: 25px;
    }
    
    li {
      margin-bottom: 8px;
    }
    
    hr {
      border: 0;
      height: 1px;
      background-image: linear-gradient(to right, rgba(0,0,0,0), var(--secondary-color), rgba(0,0,0,0));
      margin: 40px 0;
    }
    
    .header {
      display: flex;
      align-items: center;
      gap: 20px;
      margin-bottom: 30px;
    }
    
    .header h1 {
      flex: 1;
      border-bottom: none;
    }
    
    .badge {
      display: inline-block;
      padding: 4px 8px;
      background-color: var(--secondary-color);
      color: white;
      border-radius: 4px;
      font-size: 0.8em;
      margin-left: 10px;
    }
    
    .screenshot-grid {
      display: grid;
      grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
      gap: 20px;
      margin: 30px 0;
    }
    
    .screenshot-item {
      text-align: center;
      background: white;
      padding: 15px;
      border-radius: 8px;
      box-shadow: 0 4px 6px rgba(0,0,0,0.1);
      transition: transform 0.3s ease;
    }
    
    .screenshot-item:hover {
      transform: translateY(-5px);
    }
    
    .screenshot-item img {
      width: 100%;
      border-radius: 5px;
      border: 1px solid #ddd;
    }
    
    .screenshot-item p {
      margin-top: 10px;
      font-weight: bold;
      color: var(--primary-color);
    }
    
    .feature-list {
      display: grid;
      grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
      gap: 20px;
    }
    
    .feature-card {
      background: white;
      padding: 20px;
      border-radius: 8px;
      box-shadow: 0 2px 4px rgba(0,0,0,0.05);
      border-left: 4px solid var(--secondary-color);
    }
    
    .feature-card h3 {
      margin-top: 0;
      color: var(--secondary-color);
    }
    
    .tech-stack {
      display: grid;
      grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
      gap: 15px;
    }
    
    .tech-item {
      background: white;
      padding: 15px;
      border-radius: 8px;
      display: flex;
      align-items: center;
      gap: 10px;
      box-shadow: 0 2px 4px rgba(0,0,0,0.05);
    }
    
    .tech-item::before {
      content: "•";
      color: var(--secondary-color);
      font-size: 1.5em;
    }
    
    @media (max-width: 768px) {
      body {
        padding: 15px;
      }
      
      .header {
        flex-direction: column;
        align-items: flex-start;
        gap: 10px;
      }
      
      .screenshot-grid, .feature-list, .tech-stack {
        grid-template-columns: 1fr;
      }
    }
  </style>
</head>
<body>

  <div class="header">
    <h1>MVC_Lab <span class="badge">ASP.NET MVC</span></h1>
  </div>

  <p>
    Welcome to <strong>MVC_Lab</strong> — an ASP.NET MVC educational playground by <strong>Zeyad Gebril</strong>.
    Dive into controllers, models, and views as you build your .NET MVC skills!
  </p>

  <hr />

  <h2>🚀 Table of Contents</h2>
  <ul>
    <li><a href="#about">About</a></li>
    <li><a href="#screenshots">Screenshots</a></li>
    <li><a href="#features">Features</a></li>
    <li><a href="#prerequisites">Prerequisites</a></li>
    <li><a href="#project-structure">Project Structure</a></li>
    <li><a href="#tech-stack">Tech Stack</a></li>
    <li><a href="#contact">Contact</a></li>
  </ul>

  <hr />

  <h2 id="about">🧠 About</h2>
  <p>
    This project is part of Zeyad Gebril's learning journey with ASP.NET MVC at ITI, featuring:
  </p>
  <ul>
    <li>Clean MVC architecture implementation</li>
    <li>Entity Framework Core integration</li>
    <li>Razor views with Bootstrap styling</li>
    <li>Repository pattern for data access</li>
  </ul>

  <hr />

  <h2 id="screenshots">🖼️ Screenshots</h2>
  
  <div class="screenshot-grid">
    <div class="screenshot-item">
      <img src="https://github.com/zeyadgebril/MVC_Lab/blob/master/Project%20Images/landingPage%20(1).png?raw=true" alt="Home Page">
      <p>Home Page</p>
    </div>
    <div class="screenshot-item">
      <img src="https://github.com/zeyadgebril/MVC_Lab/blob/master/Project%20Images/Screenshot%202025-06-25%20171302.png?raw=true" alt="Courses Page">
      <p>Courses Management</p>
    </div>
    <div class="screenshot-item">
      <img src="https://github.com/zeyadgebril/MVC_Lab/blob/master/Project%20Images/Screenshot%202025-06-25%20171352.png?raw=true" alt="Instructors Page">
      <p>Instructors View</p>
    </div>
    <div class="screenshot-item">
      <img src="https://github.com/zeyadgebril/MVC_Lab/blob/master/Project%20Images/Screenshot%202025-06-25%20171553.png?raw=true" alt="Departments Page">
      <p>Departments</p>
    </div>
    <div class="screenshot-item">
      <img src="https://github.com/zeyadgebril/MVC_Lab/blob/master/Project%20Images/Screenshot%202025-06-25%20171621.png?raw=true" alt="Form Example">
      <p>Data Entry Form</p>
    </div>
    <div class="screenshot-item">
      <img src="https://github.com/zeyadgebril/MVC_Lab/blob/master/Project%20Images/Screenshot%202025-06-25%20171634.png?raw=true" alt="Admin Panel">
      <p>Admin Dashboard</p>
    </div>
  </div>

  <hr />

  <h2 id="features">✨ Features</h2>
  <div class="feature-list">
    <div class="feature-card">
      <h3>MVC Architecture</h3>
      <p>Clean separation of concerns with Models, Views, and Controllers</p>
    </div>
    <div class="feature-card">
      <h3>Entity Framework</h3>
      <p>Data access layer with code-first migrations</p>
    </div>
    <div class="feature-card">
      <h3>Razor Views</h3>
      <p>Dynamic HTML generation with Razor syntax</p>
    </div>
    <div class="feature-card">
      <h3>Bootstrap 5</h3>
      <p>Responsive, mobile-first UI components</p>
    </div>
    <div class="feature-card">
      <h3>Repository Pattern</h3>
      <p>Abstracted data access layer for maintainability</p>
    </div>
    <div class="feature-card">
      <h3>Form Validation</h3>
      <p>Client-side and server-side validation</p>
    </div>
  </div>

  <hr />

  <h2 id="prerequisites">📋 Prerequisites</h2>
  <ul>
    <li><strong>.NET 6 SDK</strong> or later</li>
    <li><strong>Visual Studio 2022</strong> (or VS Code with C# extensions)</li>
    <li><strong>SQL Server Express</strong> or LocalDB</li>
    <li><strong>Git</strong> for version control</li>
  </ul>

  <hr />

  <h2 id="project-structure">🗂️ Project Structure</h2>
  <pre>
MVC_Lab/
│
├── <strong>wwwroot/</strong>                  # Static files (CSS, JS, images)
│   ├── css/                  # Custom stylesheets
│   ├── images/               # Application images
│   ├── js/                   # JavaScript files
│   └── lib/                  # Client-side libraries
│
├── <strong>Controllers/</strong>              # Application controllers
│   ├── HomeController.cs     # Main controller
│   ├── CourseController.cs   # Course management
│   └── ...                   # Other controllers
│
├── <strong>Models/</strong>                   # Domain models
│   ├── Course.cs             # Course entity
│   ├── Instructor.cs         # Instructor entity
│   └── ...                   # Other models
│
├── <strong>Views/</strong>                    # Razor views
│   ├── Home/                 # Home views
│   ├── Course/               # Course views
│   ├── Shared/               # Layouts and partials
│   └── ...                   # Other views
│
├── <strong>Repository/</strong>               # Data access layer
│   ├── ICourseRepository.cs  # Course repository interface
│   └── ...                   # Implementation classes
│
├── <strong>ViewModels/</strong>               # View-specific models
│   ├── CourseVM.cs           # Course view model
│   └── ...                   # Other view models
│
├── appsettings.json          # Configuration
├── Program.cs                # Application entry point
└── Startup.cs                # Startup configuration
  </pre>

  <hr />

  <h2 id="tech-stack">🧭 Tech Stack</h2>
  <div class="tech-stack">
    <div class="tech-item">ASP.NET Core MVC</div>
    <div class="tech-item">Entity Framework Core</div>
    <div class="tech-item">Razor View Engine</div>
    <div class="tech-item">SQL Server</div>
    <div class="tech-item">Bootstrap 5</div>
    <div class="tech-item">jQuery (optional)</div>
    <div class="tech-item">LINQ</div>
    <div class="tech-item">Dependency Injection</div>
  </div>

  <hr />

  <h2 id="contact">📫 Contact</h2>
  <p>
    <strong>Zeyad Gebril</strong><br/>
    <strong>Email:</strong> <a href="mailto:zeyadgebril@outlook.com">zeyadgebril@outlook.com</a><br/>
    <strong>GitHub:</strong> <a href="https://github.com/zeyadgebril" target="_blank">github.com/zeyadgebril</a><br/>
    <strong>LinkedIn:</strong> <a href="#" target="_blank">(Add your profile)</a>
  </p>

</body>
</html>
