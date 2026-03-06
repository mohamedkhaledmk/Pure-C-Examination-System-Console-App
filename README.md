## Examination Management System (ConsoleApp1)

This project is a **console-based Examination Management System** that demonstrates core OOP concepts in C#:

- **Inheritance & Polymorphism**
- **Generics with constraints**
- **Events & Delegates**
- **Interfaces** (`ICloneable`, `IComparable<T>`)
- **Overriding** (`ToString`, `Equals`, `GetHashCode`)
- **File I/O** (logging questions)
- **Enums, composition & aggregation**

The domain is managing practice and final exams with multiple question types, automatic correction, and student notifications when exams start.

---

## Domain Model Overview

- **Answer**
  - Represents a single answer option.
  - Properties: `Id`, `Text`.
  - Implements `IComparable<Answer>` (compares by `Id`).
  - Overrides `ToString`, `Equals`, `GetHashCode` to behave like a value object.

- **AnswerList**
  - A collection of `Answer` objects.
  - Provides:
    - `Add(Answer answer)`
    - `GetById(int id)`
    - Indexer `this[int index]`
    - `Count` property
  - Implements `IEnumerable<Answer>` for `foreach` support.
  - (Assignment requirement note: internal storage is currently a `List<Answer>`; can be refactored to an `Answer[]` to fully comply with the spec.)

- **Question (abstract)**
  - Base for all question types.
  - Properties:
    - `Header`, `Body`, `Marks`
    - `AnswerList Answers`
    - `Answer CorrectAnswer`
  - Validates inputs (non-null strings, `Marks > 0`).
  - Methods:
    - `Display()` (abstract)
    - `CheckAnswer(Answer studentAnswer)` (abstract – current code uses `CheckAnswers`, easy to rename)
    - Overrides `ToString`, `Equals`, `GetHashCode`.

- **TrueFalseQuestion**
  - Question with exactly two possible answers (True/False).
  - Inherits `Question`.
  - `Display()` prints question and options.
  - `CheckAnswer` compares student answer with the correct answer.

- **ChooseOneQuestion**
  - Multiple options, student selects a single answer.
  - Inherits `Question`.
  - `Display()` shows header, body, and all options.
  - `CheckAnswer` compares student answer with the correct answer.

- **ChooseAllQuestion**
  - Multiple options, student is expected to select multiple correct answers.
  - Inherits `Question`.
  - Currently behaves like a single-answer question for grading.
  - **To-do for full spec compliance:** implement true multi-selection and set comparison in `CheckAnswer`.

- **QuestionList**
  - `public class QuestionList : List<Question>`
  - Hides `Add(Question q)` and:
    - Calls `base.Add(q)`
    - Logs each added question to a file using `StreamWriter` in **append** mode.
  - File name is supplied via constructor, so different `QuestionList` instances can log to different files.

---

## Exam Hierarchy

- **ExamMode (enum)**
  - `Starting`, `Queued`, `Finished`

- **Exam (abstract)**
  - Core abstraction for all exams.
  - Properties:
    - `int Time`
    - `int NumberOfQuestions`
    - `List<Question> Questions` (spec asks for `Question[]`, planned refactor)
    - `Dictionary<Question, Answer> QuestionAnswerDictionary` (named `QuestionAnswerDirectory` in code)
    - `Subject Subject`
    - `ExamMode Mode`
  - When `Mode` is set to `Starting`, raises the `ExamStarted` event.
  - Methods:
    - `abstract void ShowExam()`
    - `virtual void Start()` – sets `Mode = Starting` and writes “Exam Started”
    - `virtual void Finish()` – prints each question and the student’s answer
    - `int CorrectExam()` – iterates over the question–answer dictionary and sums marks for correct answers
    - Overrides `ToString`, `Equals`, `GetHashCode`
  - Interfaces:
    - `ICloneable` – shallow clone via `MemberwiseClone`
    - `IComparable<Exam>` – compares by `Time` then `NumberOfQuestions`
  - Events:
    - `public event ExamStartedHandler ExamStarted;`
    - Event is raised when `Mode` transitions to `Starting`.

- **PracticeExam**
  - Inherits `Exam`.
  - `ShowExam()` prints subject, time and all questions.
  - `Finish()`:
    - Calls base `Finish()` (shows questions + student answers).
    - Prints per-question correctness and final grade.
    - **To-do for full compliance:** explicitly print correct answers as well.

- **FinalExam**
  - Inherits `Exam`.
  - `ShowExam()` prints the exam and questions.
  - `Finish()` intentionally only shows questions and student answers (no correct answers), via `base.Finish()`.

---

## Subject & Student Model

- **Subject**
  - Represents a course/subject that owns exams and enrolled students.
  - Properties:
    - `string Name`
    - `List<Student> EnrolledStudents` (assignment specifies `Student[]`; can be refactored)
  - Methods:
    - `Enroll(Student stud)` – adds a student if not already enrolled.
    - `NotifyStudents()` – currently writes a console message for each student; can be wired more tightly to the event system.

- **Student**
  - Properties:
    - `string Name`
    - `int Id`
  - Methods:
    - `OnExamStarted(object sender, ExamEventArgs e)` – called when an exam starts; prints a notification containing the subject and exam.

- **ExamEventArgs**
  - Custom `EventArgs` for exam start events.
  - Properties:
    - `Subject Subject { get; }`
    - `Exam Exam { get; }`
  - Used by the `ExamStarted` event to notify students.

---

## Generic Repository

- **Repository<T> where T : ICloneable, IComparable<T>**
  - A simple generic repository for entities that can be cloned and compared.
  - Internally holds a collection (currently `List<T>`, but spec calls for `T[]`).
  - Methods:
    - `Add(T item)` – adds an item.
    - `GetAll()` – returns the internal collection.
    - `Sort()` – sorts items using `List<T>.Sort()` / `IComparable<T>`.
    - `Clone()` – creates a deep copy of the repository by cloning each item.
  - **To-do for full compliance:**
    - Use a `T[]` internally instead of `List<T>`.
    - Add a `Remove(T item)` method as required by the spec.

---

## Events & Notification Flow

1. Each `Exam` exposes an `ExamStarted` event of type `ExamStartedHandler`.
2. `Student.OnExamStarted` matches the event signature and can be subscribed to the event.
3. When `Exam.Mode` is set to `Starting` (e.g., via `Start()`):
   - `ExamStarted` is raised with `ExamEventArgs` containing the `Subject` and `Exam`.
   - All subscribed students write a console notification.
4. This models the requirement: “When Mode becomes Starting, raise event and notify all students.”

In `Program.Main`, each enrolled `Student` is subscribed to the chosen exam’s `ExamStarted` event.

---

## File I/O Logging

- `QuestionList` is responsible for logging questions:
  - Constructor receives a file name.
  - Overridden `Add`:
    - Calls `base.Add(question)`.
    - Opens a `StreamWriter` in **append** mode with a `using` block.
    - Writes the question’s `ToString()` output to the file.
  - Different `QuestionList` instances can log to different files by passing different file names.

---

## Main Program Flow

The `Program.Main` method (entry point) demonstrates:

1. Creating a `Subject`.
2. Creating and enrolling `Student` objects.
3. Creating answers and questions using all three question types:
   - `TrueFalseQuestion`
   - `ChooseOneQuestion`
   - `ChooseAllQuestion` (currently behaves as single-answer until multi-selection is added).
4. Creating both:
   - `PracticeExam`
   - `FinalExam`
5. Subscribing all students to each exam’s `ExamStarted` event.
6. Asking the user to select:
   - `1` – Practice exam
   - `2` – Final exam
7. Based on selection:
   - Calling `Start()` (which sets `Mode = Starting` and raises the event).
   - Calling `ShowExam()`.
   - Calling `CorrectExam()` and printing the total grade.
   - Calling `Finish()` (behavior differs between practice/final).

This flow satisfies the “Main Method Requirements” from the assignment.

---

## How to Build and Run

From the project directory (`ConsoleApp1`):

```bash
dotnet build
dotnet run
```

You will be prompted to choose the exam type, then the selected exam will start, notify students, display questions, correct the exam, and show the final output.

---

## Known Gaps vs Assignment Spec (To‑Do)

To fully match the written assignment, the following improvements are recommended:

- **Arrays instead of List<T> internally**
  - Refactor:
    - `AnswerList` to use `Answer[]`.
    - `Exam.Questions` to be `Question[]`.
    - `Subject.EnrolledStudents` to be `Student[]`.
    - `Repository<T>` to use `T[]` with manual resizing.

- **ChooseAllQuestion**
  - Implement true multi-selection:
    - Represent multiple correct answers and multiple student answers.
    - In `CheckAnswer`, compare selections as sets (order-independent, no missing or extra answers).

- **PracticeExam.Finish**
  - After each question, explicitly print the **correct answer(s)**, not just whether the student was correct.

- **Repository<T>**
  - Add `Remove(T item)` and switch to array-based storage.

These changes are localized and can be implemented without changing the overall architecture.

