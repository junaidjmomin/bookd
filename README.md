# AR Virtual Service Preview

This is an **Augmented Reality (AR)-based virtual service preview** application built using **Unity3D** and **AR Foundation** (supporting both ARCore and ARKit). It allows customers to preview hairstyles, hair colors, and makeup in real time using their phone's camera. The system leverages real-time face tracking to apply 3D models and textures for a realistic preview of salon services before booking.

## Features

- **Real-Time AR Hairstyle Preview**: Users can view and switch between different 3D hairstyles in real-time.
- **Hair Color Customization**: Change hair colors dynamically through the user interface.
- **Makeup Application**: Apply various makeup styles (e.g., lipstick) on the user’s face, adjusted automatically to fit facial features.
- **Face Tracking**: Uses AR face tracking technology to detect facial landmarks and accurately place AR assets.
- **Interactive UI**: Switch between different hairstyles, colors, and makeup styles with easy-to-use buttons.

## Technologies Used

- **Unity3D**: Core engine for building and rendering the AR application.
- **AR Foundation**: Unity's cross-platform AR API, supporting both ARCore (Android) and ARKit (iOS).
- **C# Scripts**: For managing AR interactions, user input, and dynamic model updates.
- **3D Modeling**: Hairstyle models created in Blender or sourced from online libraries.
- **2D Textures**: Makeup textures applied using UV mapping.

## Setup Guide

### Prerequisites

1. **Unity**: Download and install [Unity Hub](https://unity.com/) and the latest version of Unity with AR support.
2. **Android/iOS Device**: Ensure you have a device that supports ARCore (for Android) or ARKit (for iOS).
3. **AR Foundation**: In Unity, install the AR Foundation package, ARCore, and ARKit plugins using the Package Manager.

### Project Setup

1. Clone the repository:
   ```bash
   git clone https://github.com/junaidjmomin/AR-Virtual-Service.git
