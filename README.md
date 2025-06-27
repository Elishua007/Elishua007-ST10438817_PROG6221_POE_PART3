## 🧠 BRIEF DESCRIPTION

JARVIS is a GUI-based cybersecurity awareness chatbot built in C# using Windows Forms (WinForms) and XAML. JARVIS is a friendly, interactive, conversational chatbot, designed to educate users on cybersecurity topics like password safety, phishing, and privacy settings.

To make this bot feel user-friendly to interact with, I have added personalization, console styling, and even voice interaction. JARVIS comes with new GUI components, a cybersecurity quiz, task assistant, and basic natural language processing (NLP) capabilities.

---

## FEATURES

- Conversational GUI with stylized bot and user dialogues  
- Personalizes interaction using user’s name  
- Voice greeting using `.wav` audio playback (via NAudio)  
- Sentiment detection, chatbot can adjust its responses based on the user’s sentiment to create an empathetic interaction  
- Chatbot can recognise keywords and generate random responses for each keyword  
- Memory recall for favourite topics and past interactions  
- Code is modular consisting of 6 core classes  
- GUI built using XAML and WPF controls  
- Answers main topic questions about:
  - Password Security  
  - Phishing  
  - Privacy Settings  
- ASCII art logo on startup and exit  
- Graphical User Interface (GUI) built using Windows Forms and XAML  
- Add, delete, and complete cybersecurity tasks  
- Set reminders with time intervals  
- GUI panel for viewing, managing, and tracking tasks  
- Cybersecurity Mini-Game (Quiz) with 15 questions (including 5 multiple choice and long questions), score tracking, and feedback  
- Chatbot uses FuzzySharp package to check the user’s answer against the correct answer and then provides feedback  
- The answer must meet a certain percentage compared to the correct/actual answer  
- Logs and displays key events like tasks added, reminders set, and quiz attempts  
- GUI button to view the latest activity log entries  
- Summarized display for recent chatbot-user interactions  
- Natural Language Processing (NLP) using basic keyword matching and string manipulation  

---

## HOW TO SETUP PROJECT

### 1. Prerequisites
- Visual Studio 2022  
- C# (.NET Framework + WPF (XAML))  
- NAudio (NuGet Package)  
- FuzzySharp (NuGet Package)  

### 2. Cloning the Project
Clone from GitHub or download and unzip the folder.

### 3. Project Structure
Ensure the following files are present:
- ChatBot_Activity_Log.cs  
- UserDetails.cs  
- ChatBot_Dialogue.cs  
- DataDictionary.cs  
- ChatBot_Characteristics.cs  
- ChatBot_Logo.cs  
- ChatBot_Quiz.cs  
- ChatBot_Task.cs  
- MainWindow.xaml & MainWindow.xaml.cs  
- App.xaml & App.xaml.cs  
- JARVIS.wav audio file  
- ST10438817_POE_PART3_CHATBOT.csproj  

### 4. Install NuGet Packages
Open the NuGet Package Manager Console and install:

- NAudio  
- FuzzySharp  

> The chatbot uses FuzzySharp to check user answers against correct answers. A minimum similarity percentage is required for an answer to be accepted.

### 5. Run the Program
Build and run the project from Visual Studio. Interact with the chatbot through the GUI.

---

## FLOW OF ACTIVITIES

### 1. Startup Sequence
1. The application launches with an ASCII logo display  
2. A voice greeting plays (e.g., “Hello! Welcome to the Cybersecurity Awareness Bot.”)  
3. The chatbot asks for your name to personalize the session  

### 2. Main Menu Options (GUI Interface)
You are presented with buttons or options in the GUI:
- Start Conversing with Chatbot  
- Cybersecurity Quiz  
- Task Assistant  
- View Activity Log  
- Exit (say “goodbye”)  

These activities can be done in any order.

### 3. Chatbot Interaction
1. Type a message or question in the chatbot input box  
2. The chatbot uses keyword recognition and NLP to determine the context  
3. Responds with stylized, personalised messages  
4. Uses sentiment detection to adapt tone  

**Examples of accepted questions:**
- “Tell me about phishing.”  
- “How do I make a strong password?”  
- “What is your purpose?”  

### 4. Task Assistant (GUI)
- Add a new task (e.g., “Enable two-factor authentication”)  
- Specify a reminder (e.g., `2025-06-31 19:00`)  
- View the list of pending tasks  
- See the status of tasks  
- Update task details  
- Mark tasks as completed or delete them  

### 5. Cybersecurity Quiz
- Launch the quiz with 15 questions  
- Includes multiple choice and long answer formats  
- Get instant feedback after each answer  
- At the end, see your total score and summary  
- View results of past quiz attempts  

### 6. View Activity Log
Click on **Activity Log** to view:
- Tasks added, updated, or deleted  
- Reminders set  
- Quiz attempts  

Shows the 5 most recent actions in a summarized list.

### 7. Exit
When the user says **“goodbye”**, the chatbot exits and displays the final ASCII art logo.

---

## REQUIRED PACKAGES

- **NAudio** – for playing the `.wav` audio greeting during chatbot startup  
- **FuzzySharp** – to compare quiz answers using fuzzy string matching  
  > The chatbot only accepts answers that are sufficiently close to the correct answer (based on a similarity percentage threshold)

---

## DEVELOPER INFORMATION

- **Developer Name**: Elishua Emmanuel Naidoo  
- **Student Number**: ST10438817  
- **Institution**: The IIE Varsity College, Westville  
- **Course**: PROG6221 – Programming 2A  
- **IDE**: Visual Studio 2022  
- **Language**: C# (.NET Framework)  
- **UI Framework**: WPF (XAML)  
- **YouTube Demo**: https://youtu.be/CDPFyJvXAmU
