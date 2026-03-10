# Clint's Game Catalog

A physical PC game collection tracker built as a personal project, themed around [LGR (Lazy Game Reviews)](https://www.youtube.com/@LGR).

The app runs locally, stores everything in a SQLite database, and has a dark retro UI with amber accents inspired by the LGR aesthetic.

---

## Overview

Tracks a physical PC game collection — CDs, diskettes, Big Boxes, Jewel Cases. You can add, edit, delete and search through your games. Everything runs on your machine, no cloud or external services involved.

---

## Features

- Browse your collection in a sortable data grid
- Search by title, publisher or developer
- Filter by media type (CD, DVD, Diskette, Tape) and packaging (BigBox, JewelCase, Sleeve, etc.)
- Add, edit and delete games
- SQLite database — stored locally, created automatically on first run
- Seeded with classic retro PC games out of the box

---

## Tech Stack

- .NET 8 Blazor Web App
- MudBlazor 6.11.1
- Entity Framework Core 8 with SQLite
- Visual Studio 2022 Community
- Multi-project solution: Core, Data, Web, Tests

---

## Setup & Installation

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Visual Studio 2022 Community

### Steps

1. Clone the repo
   ```bash
   git clone https://github.com/your-username/Clint-s-Game-Catalog.git
   ```

2. Open `ClintsCatalog\ClintsCatalog.sln` in Visual Studio

3. Set `ClintsCatalog.Web` as the startup project and press `F5`

4. App runs at `http://localhost:5000` — the database is created and seeded automatically on first run
