var configuration = "Release";
var artifactsDirectory = Directory("../artifacts");
var sourceDirectory = Directory("../src");
var solutionFile = sourceDirectory + File("Contrib.MSBuild.TypeLibrary.sln");

Task("Build")
  .IsDependentOn("Clean")
  .Does(() =>
{
  Information($"Building {MakeAbsolute(solutionFile)}");

  MSBuild(solutionFile,
          settings => settings.SetConfiguration(configuration)
                              .WithRestore()
                              .WithProperty("PackageOutputPath", MakeAbsolute(artifactsDirectory).FullPath));
});

Task("Clean")
  .Does(() =>
{
  Information($"Cleaning {MakeAbsolute(artifactsDirectory)}");

  if (DirectoryExists(artifactsDirectory))
  {
    DeleteDirectory(artifactsDirectory,
                    new DeleteDirectorySettings
                    {
                      Recursive = true
                    });
  }

  CreateDirectory(artifactsDirectory);
});

var targetArgument = Argument("target", "Build");
RunTarget(targetArgument);
