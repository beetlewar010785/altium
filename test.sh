#!/bin/bash

FILE_SIZE=${1:-10485760}

mkdir "./out/bin/e2e-test/suites/"

./out/bin/e2e-test/Altium.FileSorter.E2ETest.exe -s "./out/bin/file-sorter/Altium.FileSorter.exe" -g "./out/bin/random-rows-generator/Altium.RandomRowsGenerator.exe" -d "./out/bin/e2e-test/suites/" --size $FILE_SIZE