![Testably.Abstractions.FluentAssertions](https://raw.githubusercontent.com/Testably/Testably.Abstractions.FluentAssertions/main/Docs/Images/social-preview.png)  
[![Nuget](https://img.shields.io/nuget/v/Testably.Abstractions.FluentAssertions)](https://www.nuget.org/packages/Testably.Abstractions.FluentAssertions)
[![Build](https://github.com/Testably/Testably.Abstractions.FluentAssertions/actions/workflows/build.yml/badge.svg)](https://github.com/Testably/Testably.Abstractions.FluentAssertions/actions/workflows/build.yml)
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/5b9b2f79950447a69d69037b43acd590)](https://app.codacy.com/gh/Testably/Testably.Abstractions.FluentAssertions/dashboard?utm_source=gh&utm_medium=referral&utm_content=&utm_campaign=Badge_grade)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=Testably_Testably.Abstractions.FluentAssertions&branch=main&metric=coverage)](https://sonarcloud.io/summary/overall?id=Testably_Testably.Abstractions.FluentAssertions&branch=main)
[![Mutation testing badge](https://img.shields.io/endpoint?style=flat&url=https%3A%2F%2Fbadge-api.stryker-mutator.io%2Fgithub.com%2FTestably%2FTestably.Abstractions.FluentAssertions%2Fmain)](https://dashboard.stryker-mutator.io/reports/github.com/Testably/Testably.Abstractions.FluentAssertions/main)

This library is an extension to [FluentAssertions](https://github.com/fluentassertions/fluentassertions) for the [`IFileSystem` interface](https://github.com/TestableIO/System.IO.Abstractions).

## Getting Started

- Install `Testably.Abstractions.FluentAssertions` as nuget package.
  ```ps
  dotnet add package Testably.Abstractions.FluentAssertions
  ```

- Add the following `using` statement:
  ```csharp
  using Testably.Abstractions.FluentAssertions;
  ```
  This brings the extension methods in the current scope.

## Examples

1. Verify, that a directory "foo" exists under the current directory in the file system:
   ```csharp
   fileSystem.Should().HaveDirectory("foo");
   ```
   or
   ```csharp
   IDirectoryInfo directoryInfo = fileSystem.DirectoryInfo.New(".");
   directoryInfo.Should().HaveDirectory("foo");
   ```

3. Verify, that the file "foo.txt" has text content "bar":
   ```csharp
   fileSystem.Should().HaveFile("foo.txt")
       .Which.HasContent("bar");
   ```
