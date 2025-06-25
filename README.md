<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
  <title>MVC_Lab - README</title>
  <style>
    body {
      font-family: Arial, sans-serif;
      line-height: 1.7;
      padding: 40px;
      max-width: 960px;
      margin: auto;
      background-color: #fdfdfd;
      color: #333;
    }
    pre {
      background-color: #f4f4f4;
      padding: 15px;
      overflow-x: auto;
      border-left: 4px solid #ccc;
    }
    h1, h2, h3 {
      color: #2c3e50;
    }
    hr {
      margin: 40px 0;
    }
    a {
      color: #3498db;
      text-decoration: none;
    }
    a:hover {
      text-decoration: underline;
    }
  </style>
</head>
<body>

  <h1>MVC_Lab</h1>

  <p>
    Welcome to <strong>MVC_Lab</strong> 🐊 — an ASP.NET MVC educational playground by <strong>Zeyad Gebril</strong>.
    Dive into controllers, models, and views as you build your .NET MVC skills!
  </p>

  <hr />

  <h2>🚀 Table of Contents</h2>
  <ul>
    <li><a href="#about">About</a></li>
    <li><a href="#screenshots">Screenshots</a></li>
    <li><a href="#features">Features</a></li>
    <li><a href="#project-structure">Project Structure</a></li>
    <li><a href="#tech-stack">Tech Stack</a></li>
    <li><a href="#contact">Contact</a></li>
  </ul>

  <hr />

  <h2 id="about">🧠 About</h2>
  <p>
    This project is part of Zeyad Gebril's learning journey with ASP.NET MVC at ITI, featuring a swamp of controllers, models, and Razor views ready to explore.
  </p>

  <hr />

 <h2 id="screenshots">🖼️ Screenshots</h2>
<p>(Add your own screenshots here to demonstrate key pages or features)</p>

<!-- Centered Home Page Image -->
<div style="text-align: center;">
  <img
    src="https://github.com/zeyadgebril/MVC_Lab/blob/master/Project%20Images/landingPage%20(1).png?raw=true"
    alt="Home Page"
    style="max-width:80%; border: 1px solid #ccc; border-radius: 10px;"
  />
</div>

<br/>

<!-- 2x3 Image Grid -->
<div style="
  display: grid;
  grid-template-columns: repeat(3, 1fr);
  gap: 15px;
  margin-top: 20px;
">
  <img src="https://github.com/zeyadgebril/MVC_Lab/blob/master/Project%20Images/Screenshot%202025-06-25%20171302.png?raw=true" alt="Screenshot 1" style="width: 100%; border-radius: 8px;" />
  <img src="https://github.com/zeyadgebril/MVC_Lab/blob/master/Project%20Images/Screenshot%202025-06-25%20171352.png?raw=true" alt="Screenshot 2" style="width: 100%; border-radius: 8px;" />
  <img src="https://github.com/zeyadgebril/MVC_Lab/blob/master/Project%20Images/Screenshot%202025-06-25%20171553.png?raw=true" alt="Screenshot 3" style="width: 100%; border-radius: 8px;" />
  <img src="https://github.com/zeyadgebril/MVC_Lab/blob/master/Project%20Images/Screenshot%202025-06-25%20171621.png?raw=true" alt="Screenshot 4" style="width: 100%; border-radius: 8px;" />
  <img src="https://github.com/zeyadgebril/MVC_Lab/blob/master/Project%20Images/Screenshot%202025-06-25%20171634.png?raw=true" alt="Screenshot 5" style="width: 100%; border-radius: 8px;" />
</div>
<hr/>

  <h2 id="features">✨ Features</h2>
  <ul>
    <li><strong>Controller-Based Routing</strong>: Simple but effective action methods</li>
    <li><strong>ViewBag/ViewData</strong> usage for dynamic data in views</li>
    <li><strong>Forms & HttpPost Handling</strong></li>
    <li>Add your distinctive features here</li>
  </ul>

  <hr />

  <h2 id="prerequisites">📋 Prerequisites</h2>
  <ul>
    <li>.NET Framework</li>
    <li>Visual Studio</li>
    <li>LocalDB</li>
  </ul>

  <hr />

  <h2 id="project-structure">🗂️ Project Structure</h2>
  <pre>
MVC_Lab/
│
├── wwwroot/                  # Public static files
│   ├── css/                  # Custom stylesheets
│   ├── Image/                # Static images
│   ├── js/                   # JavaScript files
│   └── lib/                  # Third-party libraries (Bootstrap, jQuery, etc.)
│
├── Controllers/              # MVC Controllers
├── CustomAttribute/          # Custom attributes for filters or validation
├── Filter/                   # Custom filters (e.g., authorization, logging)
├── Migrations/               # EF Core migration history (if used)
├── Models/                   # Entity and domain models
├── Repository/               # Data access layer (repository pattern)
├── ViewModel/                # View-specific models (DTOs or composite models)
│
├── Views/                    # Razor views organized by controller
│   ├── Account/
│   ├── Course/
│   ├── Department/
│   ├── Home/
│   ├── Instructor/
│   ├── Role/
│   ├── Shared/               # Layouts, partials, error views
│   └── Trainee/
│   ├── _ViewImports.cshtml   # Razor global imports
│   └── _ViewStart.cshtml     # Layout definition
│
├── appsettings.json          # Configuration settings
├── libman.json               # Library Manager config
└── Program.cs                # Entry point and app setup
  </pre>

  <hr />

  <h2 id="tech-stack">🧭 Tech Stack</h2>
  <ul>
    <li>C#, ASP.NET MVC</li>
    <li>Entity Framework (if used)</li>
    <li>Razor View Engine</li>
    <li>SQL Server (LocalDB)</li>
    <li>Bootstrap (if applicable)</li>
  </ul>

  <hr />

  <h2 id="contact">📫 Contact</h2>
  <p>
    <strong>Zeyad Gebril</strong><br/>
    Email: <a href="mailto:zeyadgebril@outlook.com">zeyadgebril@outlook.com</a><br/>
    GitHub: <a href="https://github.com/zeyadgebril" target="_blank">zeyadgebril</a>
  </p>

</body>
</html>
