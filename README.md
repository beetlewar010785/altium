# Task

When checking the task solution we pay attention to the speed of it, the quality of the code and, in general, the
execution time.
Time reference - a 10 GB file is sorted in about 9 minutes (can be faster), and a 1 GB file is sorted within a minute (
the fastest result is 26 seconds). As an additional reference point - when sorting 1 GB - around 2 or 2.5 GB of memory
is used.

# to generate file with random data

dotnet run --project ".\Altium.RandomRowsGenerator\Altium.RandomRowsGenerator.csproj" -o "$HOME\tmp\in.txt" -s 10737418240 --min 100 --max 1024

# to sort file

dotnet run --project ".\Altium.FileSorter\Altium.FileSorter.csproj" -i "$HOME\tmp\in.txt" -o "$HOME\tmp\out.txt"

# to test all together with 1GB test file (using git bash, for example)
./build.sh
./test.sh 1073741824