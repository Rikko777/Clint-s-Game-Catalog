# Clint's Game Catalog

A physical PC game collection tracker built as a personal project, themed around [LGR (Lazy Game Reviews)](https://www.youtube.com/@LGR) 

## Overview

Tracks a physical PC game collection — CDs, diskettes, Big Boxes, Jewel Cases and everything in between. Add, edit, delete and search through your collection. Everything runs locally on your machine with no cloud or external services required.

---

## Features

- Browse your collection in a sortable data grid
- Search by title, publisher or developer
- Filter by media type and packaging
- Add, edit and delete games
- LGR-inspired dark UI with amber accents and monospace font
- SQLite database stored locally, created automatically on first run
- Seeded with 50 classic retro PC games out of the box

---

## Tech Stack

- .NET 8 Blazor Web App
- MudBlazor 6.11.1
- Entity Framework Core 8 with SQLite
- xUnit + FluentAssertions for testing
- Visual Studio 2022 Community

---

## Setup & Installation

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Visual Studio 2022 Community

### Steps

1. Clone the repo
   ```bash
   git clone https://github.com/Rikko777/Clint-s-Game-Catalog.git
   ```

2. Open `ClintsCatalog\ClintsCatalog.sln` in Visual Studio

3. Set up user secrets
   ```bash
   cd ClintsCatalog\ClintsCatalog.Web
   dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Data Source=clints_catalog.db"
   ```

4. Set `ClintsCatalog.Web` as the startup project and press `F5`

5. App runs at `http://localhost:5000`

### Running Tests

Open the Developer Command Prompt in Visual Studio and navigate to the `ClintsCatalog.Tests` folder, then run:
```bash
dotnet test
```