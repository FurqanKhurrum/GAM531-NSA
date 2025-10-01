# GAM531 - Assignment 4: Texture Mapping with OpenGL and OpenTK

A 3D texture mapping demonstration using OpenGL through the OpenTK library in C#. This project renders a rotating cube with texture mapping applied to all faces.

<img width="1354" height="674" alt="image" src="https://github.com/user-attachments/assets/d6eb8852-8603-42e6-aa0e-8b8bfe151022" />

## ✨ Features
- 3D cube rendering with OpenGL
- 2D texture mapping on all cube faces
- Vertex and fragment shader implementation
- Animated rotation (with toggle control)
- Procedural texture fallback
- Clean object-oriented architecture

## 🔧 Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later
- A text editor or IDE (Visual Studio, VS Code, or Rider recommended)
- Git (for cloning the repository)

## 📦 Installation

### Step 1: Clone the Repository
```bash
# Clone the repository
git clone https://github.com/[your-username]/GAM531-TextureMapping.git

# Navigate to the project directory
cd GAM531-TextureMapping
```

### Step 2: Restore Dependencies
```bash
# Restore NuGet packages
dotnet restore
```

This will automatically install:
- **OpenTK 4.9.4** - OpenGL bindings for C#
- **StbImageSharp 2.30.15** - Image loading library

### Step 3: Build the Project
```bash
# Build the project
dotnet build
```

## 🚀 Running the Project

### Option 1: Using dotnet CLI
```bash
dotnet run
```

### Option 2: Using Visual Studio
1. Open `GAM531.sln` in Visual Studio
2. Press `F5` or click the "Start" button

### Option 3: Using VS Code
1. Open the folder in VS Code
2. Press `F5` (ensure C# extension is installed)

## 🎮 Controls
| **SPACE** - Toggle animation on/off
| **ESCAPE** - Exit application 

## 💻 Code Overview

### Core Components

#### **Program.cs**
The entry point that creates the OpenGL window with specified settings and starts the application loop.

#### **TexturedCube.cs**
Main game window class that:
- Initializes OpenGL context and enables depth testing
- Manages the render loop and frame updates
- Handles user input (keyboard controls)
- Coordinates all components (cube, shader, texture, camera)

#### **Cube.cs**
Manages the 3D cube geometry:
- Defines vertex positions and texture coordinates for all 6 faces
- Creates and manages Vertex Array Object (VAO), Vertex Buffer Object (VBO), and Element Buffer Object (EBO)
- Implements the Draw() method for rendering

#### **Shader.cs**
Handles GPU shader programs:
- Compiles vertex and fragment shaders from GLSL source
- Links shaders into a program
- Provides methods to set uniform variables (matrices, textures)
- Includes fallback default shaders if files are missing

#### **Texture.cs**
Manages texture loading and binding:
- Loads image files using StbImageSharp
- Configures texture parameters (wrapping, filtering)
- Generates procedural checkerboard texture as fallback
- Handles texture binding to OpenGL texture units

#### **Camera.cs**
Manages the 3D view:
- Creates view matrix (camera position and orientation)
- Creates projection matrix (perspective transformation)
- Handles aspect ratio updates on window resize

### Shader Implementation

#### **Vertex Shader**
- Transforms vertex positions from model space to screen space
- Passes texture coordinates to fragment shader

#### **Fragment Shader**
- Samples the texture at interpolated coordinates
- Outputs the final pixel color

## 📚 Dependencies

### NuGet Packages
| Package | Version | Purpose |
|---------|---------|---------|
| **OpenTK** | 4.9.4 | OpenGL bindings for C#, window management, input handling |
| **StbImageSharp** | 2.30.15 | Loading various image formats (PNG, JPG, etc.) |

### Installing Dependencies Manually
If dependencies don't restore automatically:
```bash
# Install OpenTK
dotnet add package OpenTK --version 4.9.4

# Install StbImageSharp
dotnet add package StbImageSharp --version 2.30.15
```

### Black screen or no cube visible
**Solution:** Check that:
- OpenGL context is created successfully
- Shaders compile without errors (check console output)
- Matrix multiplication order is correct in shaders

### Build errors
**Solution:**
```bash
# Clean and rebuild
dotnet clean
dotnet restore
dotnet build
```

## 👤 Author
**Student Name:** Furqan Khurrum  
**Student ID:** 151694239  
**Class:** GAM531 - NSA - Fall 25  
**Professor:** Leonardo Moura  
**Date:** 10/01/2025

---
