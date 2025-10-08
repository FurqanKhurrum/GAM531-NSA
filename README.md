# GAM531 - Assignment: Phong Lighting Model with OpenGL and OpenTK

A 3D lighting demonstration implementing the Phong lighting model using OpenGL through the OpenTK library in C#. This project renders an interactive cube with realistic ambient, diffuse, and specular lighting effects.

## ✨ Features
- Complete Phong lighting implementation (ambient, diffuse, specular)
- Interactive 3D cube with proper per-face normals
- Dynamic point light source with real-time position control
- First-person camera system with mouse and keyboard controls
- Vertex and fragment shader implementation
- Proper normal transformation for accurate lighting
- Depth testing for correct 3D rendering

## 🔧 Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later
- A text editor or IDE (Visual Studio, VS Code, or Rider recommended)
- Graphics card supporting OpenGL 3.3 or later
- Git (for cloning the repository)

## 📦 Installation

### Step 1: Clone the Repository
```bash
git clone https://github.com/FurqanKhurrum/GAM531.git

# Navigate to the project directory
cd GAM531

# Restore dependencies
dotnet restore

# Build the project
dotnet build

# Run the application
dotnet run
```

### Option 2: Using Visual Studio
1. Open `GAM531.sln` in Visual Studio
2. Press `F5` or click the "Start" button

### Option 3: Using VS Code
1. Open the folder in VS Code
2. Press `F5` (ensure C# extension is installed)

## 🎮 Controls

### Camera Movement
| Key | Action |
|-----|--------|
| **W** | Move camera forward |
| **S** | Move camera backward |
| **A** | Move camera left |
| **D** | Move camera right |
| **SPACE** | Move camera up |
| **LEFT SHIFT** | Move camera down |
| **MOUSE** | Look around (free look) |

### Light Controls
| Key | Action |
|-----|--------|
| **↑ (UP ARROW)** | Move light up |
| **↓ (DOWN ARROW)** | Move light down |
| **← (LEFT ARROW)** | Move light left |
| **→ (RIGHT ARROW)** | Move light right |

### Other
| Key | Action |
|-----|--------|
| **R** | Toggle auto-rotation of cube |
| **ESC** | Exit application |

## 💻 Code Overview

### Core Components

#### **Program.cs**
The entry point that creates the OpenGL window with specified settings, displays control instructions, and starts the application loop.

#### **Game.cs**
Main game window class that:
- Initializes OpenGL context and enables depth testing
- Manages the render loop and frame updates
- Handles user input (keyboard and mouse controls)
- Defines cube geometry with vertex positions and normals
- Coordinates all components (shader, camera, lighting)
- Updates transformation matrices and lighting parameters

#### **Shader.cs**
Handles GPU shader programs:
- Loads and compiles vertex and fragment shaders from GLSL files
- Links shaders into a shader program
- Provides methods to set uniform variables (matrices, vectors, floats)
- Includes error checking and reporting for shader compilation
- Manages shader resource cleanup

#### **Camera.cs**
Manages the 3D camera system:
- Implements first-person camera with position and orientation
- Creates view matrix (camera transformation)
- Creates projection matrix (perspective projection)
- Handles pitch and yaw rotation with gimbal lock prevention
- Updates camera vectors (front, right, up) based on orientation
- Manages field of view and aspect ratio

### Shader Implementation

#### **Vertex Shader (phong.vert)**
- Transforms vertex positions from model space to world space (FragPos)
- Transforms normals using the normal matrix to handle non-uniform scaling
- Outputs positions to clip space using Model-View-Projection matrices
- Passes interpolated data to fragment shader

#### **Fragment Shader (phong.frag)**
- Implements the three components of Phong lighting:
  - **Ambient**: Base lighting (10% strength)
  - **Diffuse**: Directional lighting based on surface normal and light direction
  - **Specular**: Reflective highlights (50% strength, shininess = 32)
- Normalizes interpolated normals
- Calculates view and light directions
- Combines all lighting components with object color
- Outputs final fragment color

### Phong Lighting Model

The implementation follows the classic Phong reflection model:

**Ambient Component:**
```
ambient = ambientStrength × lightColor
```

**Diffuse Component:**
```
diffuse = max(N · L, 0) × lightColor
```
where N is the surface normal and L is the light direction

**Specular Component:**
```
specular = specularStrength × (max(V · R, 0))^shininess × lightColor
```
where V is the view direction, R is the reflection direction, and shininess controls highlight size

**Final Color:**
```
finalColor = (ambient + diffuse + specular) × objectColor
```

## 📚 Dependencies

### NuGet Packages
| Package | Version | Purpose |
|---------|---------|---------|
| **OpenTK** | 4.9.4 | OpenGL bindings for C#, window management, input handling |

### Installing Dependencies Manually
If dependencies don't restore automatically:
```bash
# Install OpenTK
dotnet add package OpenTK --version 4.9.4
```

## 👤 Author
**Student Name:** Furqan Khurrum  
**Student ID:** 151694239  
**Class:** GAM531 - NSA - Fall 2025  
**Professor:** Leonardo Moura  
**Date:** 08/10/2025

---

## 📝 Implementation Report

### Overview
This project successfully implements the Phong lighting model using OpenTK and GLSL shaders in C#. The implementation features a 3D cube with proper per-face normals, allowing each face to be correctly lit based on its orientation relative to the light source. The vertex shader handles transformation of vertex positions and normals to world space, while the fragment shader performs per-pixel lighting calculations combining ambient, diffuse, and specular components for realistic rendering.

### Key Implementation Details
- **Normal Transformation**: Normals are transformed using the normal matrix (transpose of the inverse of the model matrix) to maintain perpendicularity under non-uniform transformations
- **Per-Fragment Lighting**: All lighting calculations occur in the fragment shader for smooth, interpolated results
- **Interactive Controls**: First-person camera with mouse look and keyboard movement, plus dynamic light positioning
- **Proper Depth Testing**: Enabled to ensure correct rendering of overlapping geometry

### Challenges Faced
The primary challenge was ensuring correct normal transformation when applying rotations to the model. Initially, transforming normals with the standard model matrix caused incorrect lighting on rotated objects. This was resolved by implementing the normal matrix (transpose of the inverse of the model matrix), which properly handles normal transformation even under non-uniform scaling and rotation.

Another challenge was implementing smooth camera controls with mouse input while preventing gimbal lock. This was addressed by clamping the pitch angle to ±89 degrees and carefully managing the Yaw-Pitch-Roll rotation order to maintain stable camera orientation throughout movement.