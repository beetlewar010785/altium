#!/bin/bash

dotnet build "./Altium.FileSorter/" -o "./out/bin/file-sorter" -c "Release"
dotnet build "./Altium.RandomRowsGenerator/" -o "./out/bin/random-rows-generator" -c "Release"
dotnet build "./Altium.FileSorter.E2ETest/" -o "./out/bin/e2e-test" -c "Release"