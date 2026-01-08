# 📊 Interactive Task Management Dashboard (Unity)

## 📌 Overview

This project is a **responsive, animated task management dashboard** built in **Unity (UGUI)** as part of a **Technical Artist assignment**.

The implementation focuses on:
- Clean UI architecture
- Custom shader development for UI
- Responsive layouts (desktop & mobile)
- Tween-based UI interactions
- Theme management (Light / Dark)

The goal was to demonstrate **technical UI craftsmanship**, **shader integration**, and **scalable system design** within a limited development timeframe.

---

## 🧱 Architecture Overview

### Core Responsibilities

| Component | Responsibility |
|--------|----------------|
| **Control_manager** | Central controller for tasks, UI refresh, progress logic, navigation |
| **Task_feeder** | Task creation flow, input handling, animated confirmation |
| **ResponsiveSidebarController** | Adaptive sidebar behavior for desktop & mobile |
| **ThemeManager** | Global theme switching (Light / Dark) |
| **Custom UI Shader** | Gradient progress bar with animated shine |
| **RoundedCorners** | Procedural rounded UI elements without sprite slicing |

All UI state updates flow through a **single source of truth**, ensuring consistency and avoiding duplicated logic.

---

## 📋 Task System

### Task Model
Each task consists of:
- Name
- Date
- Status (**Pending / In Progress / Completed**)

### Status Updates
- Task status can be updated via dropdowns
- Status changes immediately update:
  - Dashboard counters
  - Progress visualization
  - Report views

The system is designed to be **data-driven and extensible**.

---

## 📈 Progress Bar (Custom UI Shader)

A custom **ShaderLab UI shader** is used to visualize task completion.

### Features
- Fill amount based on `Completed / Total Tasks`
- Three-color horizontal gradient
- Animated shine constrained to filled area
- Full **Stencil / Mask support** for ScrollRect compatibility
- Optimized for Unity UI rendering

Shader values are updated from script, with explicit UI redraw handling to ensure immediate visual updates.

---

## 📱 Responsive Layout

The interface adapts automatically based on screen size:

### Desktop
- Sidebar always visible
- Expanded spacing and layout

### Mobile
- Sidebar hidden by default
- Hamburger menu toggle
- Compact spacing and touch-friendly UI

This behavior is handled programmatically without duplicating layouts.

---

## 🎨 Theme System (Light / Dark)

Themes are implemented using **ScriptableObjects**.

### Theme Features
- Backgrounds
- Cards
- Text
- Buttons
- Icons
- Accent colors

UI elements automatically respond to theme changes at runtime, with no scene reloads required.

---

## ✨ UI Animations

All UI animations are implemented using **DOTween**, including:
- Sidebar slide and resize
- Task panel pop-in animation
- Confirmation feedback transitions

Tween-based animations were chosen over keyframes for:
- Better control
- Cleaner code
- Performance-friendly behavior

---

## 🧩 UI Components

### RoundedCorners
- Procedural rounded UI cards
- Resolution-independent
- No 9-slice sprites
- Optional borders

### Notification Badge
- Dynamic unread count
- Auto-hide when count reaches zero

---

## 📁 Key Scripts

- `Control_manager.cs`
- `Task_feeder.cs`
- `ResponsiveSidebarController.cs`
- `RoundedCorners.cs`
- `ThemeManager.cs`
- `ThemeImage.cs`
- `ThemeText.cs`
- `UITheme.cs`
- `UI_GradientProgressBar_3Color.shader`

---

## 🚀 How to Run

1. Open the project in **Unity (6000.0.59 or newer recommended)**
2. Load the main dashboard scene
3. Enter Play Mode
4. Test:
   - Sidebar navigation
   - Task status updates
   - Progress bar animation
   - Theme switching
   - Mobile aspect ratio (9:16)

---

## 🏁 Notes

This project emphasizes:
- **UI system design**
- **Shader-driven visualization**
- **Responsive UX**
- **Clean separation of responsibilities**

The architecture is intentionally modular, allowing easy extension for real data sources, persistence, or analytics.

---


